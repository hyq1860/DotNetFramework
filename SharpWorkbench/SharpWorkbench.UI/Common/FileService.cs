using System;
using System.Diagnostics;
using System.IO;
using System.Xml;
using SharpWorkbench.Core.Workbench;
using SharpWorkbench.Core.ViewContent;
using ICSharpCode.Core;
using SharpWorkbench.UI.WorkBench;

namespace SharpWorkbench.UI.Common
{
	public class FileService
	{
		class LoadFileWrapper
		{
			IDisplayBinding binding;
			
			public LoadFileWrapper(IDisplayBinding binding)
			{
				this.binding = binding;
			}
			
			public void Invoke(string fileName)
			{
				IViewContent newContent = binding.CreateContentForFile(fileName);
				if (newContent != null) {
					//DisplayBindingService.AttachSubWindows(newContent, false);
					WorkbenchSingleton.Workbench.ShowView(newContent);
				}
			}
		}
		
		public static bool IsOpen(string fileName)
		{
			return GetOpenFile(fileName) != null;
		}
		
		public static IWorkbenchWindow OpenFile(string fileName)
		{
			LoggingService.Info("Open file " + fileName);
			
			IWorkbenchWindow window = GetOpenFile(fileName);
			if (window != null) {
				window.SelectWindow();
				return window;
			}

            IDisplayBinding binding = DisplayBindingService.GetBindingPerFileName(fileName);
			
			if (binding != null) {
				if (FileUtility.ObservedLoad(new NamedFileOperationDelegate(new LoadFileWrapper(binding).Invoke), fileName) == FileOperationResult.OK) {
					//FileService.RecentOpen.AddLastFile(fileName);
				}
			} else {
				throw new ApplicationException("Can't open " + fileName + ", no display codon found.");
			}
			return GetOpenFile(fileName);
		}		
		
		public static IWorkbenchWindow GetOpenFile(string fileName)
		{
			if (fileName != null && fileName.Length > 0) {
				foreach (IViewContent content in WorkbenchSingleton.Workbench.ViewContentCollection) {
					string contentName = content.FileName;
					if (contentName != null) {
						if (FileUtility.IsEqualFileName(fileName, contentName))
							return content.WorkbenchWindow;
					}
				}
			}
			return null;
		}
	}
}
