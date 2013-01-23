using System;
using System.Xml;
using System.IO;
using System.Collections;
using System.Reflection;
using System.CodeDom.Compiler;
using System.Text.RegularExpressions;
using ICSharpCode.Core;

namespace SharpWorkbench.Core.ViewContent
{
	public class DisplayBindingDescriptor
	{
		object binding = null;
		Codon codon;
		
		public IDisplayBinding Binding {
			get {
				if (binding == null) {
					binding = codon.AddIn.CreateObject(codon.Properties["class"]);
				}
				return binding as IDisplayBinding;
			}
		}
		
		public Codon Codon {
			get {
				return codon;
			}
		}
		
		public DisplayBindingDescriptor(Codon codon)
		{
			this.codon = codon;
		}
		
		/// <summary>
		/// Gets if the display binding can possibly attach to the file.
		/// If this method returns false, it cannot attach to it; if the method returns
		/// true, it *might* attach to it.
		/// </summary>
		/// <remarks>
		/// This method is used to skip loading addins like the ResourceEditor which cannot
		/// attach to a certain file name for sure.
		/// </remarks>
		public bool CanAttachToFile(string fileName)
		{
			string fileNameRegex = codon.Properties["fileNamePattern"];
			if (fileNameRegex == null || fileNameRegex.Length == 0) // no regex specified
				return true;
			return Regex.IsMatch(fileName, fileNameRegex, RegexOptions.IgnoreCase);
		}
	}
}
