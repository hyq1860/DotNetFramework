using System;
using System.Xml;
using System.IO;
using System.Collections;
using System.Reflection;
using System.CodeDom.Compiler;
using ICSharpCode.Core;
using SharpWorkbench.Core.ViewContent;

namespace SharpWorkbench.UI.Common
{
	/// <summary>
	/// This class handles the installed display bindings
	/// and provides a simple access point to these bindings.
	/// </summary>
	public static class DisplayBindingService
	{
		readonly static string displayBindingPath = "/SharpDevelop/Workbench/DisplayBindings";
		
		static DisplayBindingDescriptor[] bindings = null;
		
		public static IDisplayBinding GetBindingPerFileName(string filename)
		{
			DisplayBindingDescriptor codon = GetCodonPerFileName(filename);
			return codon == null ? null : codon.Binding;
		}

        static DisplayBindingDescriptor GetCodonPerFileName(string filename)
        {
	        foreach (DisplayBindingDescriptor binding in bindings) {
		        if (binding.CanAttachToFile(filename)) {
			        if (binding.Binding != null && binding.Binding.CanCreateContentForFile(filename)) {
				        return binding;
			        }
		        }
	        }
	        return null;
        }
		
		static DisplayBindingService()
		{
			bindings = (DisplayBindingDescriptor[])(AddInTree.GetTreeNode(displayBindingPath).BuildChildItems(null)).ToArray(typeof(DisplayBindingDescriptor));
		}
	}
}
