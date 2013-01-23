using System;
using ICSharpCode.Core;

namespace SharpWorkbench.Core.Pad
{
	/// <summary>
	/// Description of PadDescriptor.
	/// </summary>
	public class PadDescriptor : IDisposable
	{
		Codon       codon;
		IPadContent padContent;
		bool        padContentCreated;
		
		/// <summary>
		/// Returns the title of the pad.
		/// </summary>
		public string Title {
			get {
				return codon.Properties["title"];
			}
		}
		
		/// <summary>
		/// Returns the icon bitmap resource name of the pad. May be null, if the pad has no
		/// icon defined.
		/// </summary>
		public string Icon {
			get {
				return codon.Properties["icon"];
			}
		}
		
		/// <summary>
		/// Returns the category (this is used for defining where the menu item to
		/// this pad goes)
		/// </summary>
		public string Category {
			get {
				return codon.Properties["category"];
			}
		}
		
		/// <summary>
		/// Returns the menu shortcut for the view menu item.
		/// </summary>
		public string Shortcut {
			get {
				return codon.Properties["shortcut"];
			}
		}
		
		public string Class {
			get {
				return codon.Properties["class"];
			}
		}
		
		public bool HasFocus {
			get {
				return (padContent != null) ? padContent.Control.ContainsFocus : false;
			}
		}
		
		public IPadContent PadContent {
			get {
				CreatePad();
				return padContent;
			}
		}
		
		public void Dispose()
		{
			if (padContent != null) {
				padContent.Dispose();
				padContent = null;
			}
		}
		
		public void RedrawContent()
		{
			if (padContent != null) {
				padContent.RedrawContent();
			}
		}
		
		public void CreatePad()
		{
			if (!padContentCreated) {
				padContentCreated = true;
				padContent = (IPadContent)codon.AddIn.CreateObject(Class);
			}
		}
		
		public PadDescriptor(Codon codon)
		{
			this.codon = codon;
		}

        public void BringPadToFront()
        {
            CreatePad();
            if (padContent == null) return;

            if (BringPadToFrontEvent != null)
                BringPadToFrontEvent(this);
        }

        public delegate void BringPadToFrontHnd(PadDescriptor padDesc);
        public static event BringPadToFrontHnd BringPadToFrontEvent;
	}
}