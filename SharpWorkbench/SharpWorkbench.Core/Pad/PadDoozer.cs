using System;
using System.Collections;
using System.Reflection;
using ICSharpCode.Core;

namespace SharpWorkbench.Core.Pad
{
	public class PadDoozer : IDoozer
	{
		/// <summary>
		/// Gets if the doozer handles codon conditions on its own.
		/// If this property return false, the item is excluded when the condition is not met.
		/// </summary>
		public bool HandleConditions {
			get {
				return false;
			}
		}
		
		public object BuildItem(object caller, Codon codon, ArrayList subItems)
		{
			return new PadDescriptor(codon);
		}
	}
}
