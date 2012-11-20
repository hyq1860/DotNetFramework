// -----------------------------------------------------------------------
// <copyright file="Events.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace ExtendedWebBrowser2
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public delegate void ScriptErrorEventHandler(object sender, ScriptErrorEventArgs e);
    public class ScriptErrorEventArgs : System.EventArgs
    {
        public int lineNumber;
        public int characterNumber;
        public int errorCode;
        public string errorMessage;
        public string url;
        public bool continueScripts;

        public ScriptErrorEventArgs() { }

        public void SetParameters()
        {
            this.continueScripts = true;
            this.lineNumber = 0;
            this.characterNumber = 0;
            this.errorCode = 0;
            this.errorMessage = "";
            this.url = "";
        }
    }
}
