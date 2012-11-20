// -----------------------------------------------------------------------
// <copyright file="ScriptHelper.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace ExtendedWebBrowser2
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Windows.Forms;

    [InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
    public interface IHtmlUIScript
    {
        [DispId(0)]
        void notify();
    }

    [ClassInterface(ClassInterfaceType.None)]
    internal class ScriptHelper : IHtmlUIScript // 80131531}
    {
        public ScriptHelper()
        {

        }

        public void notify()
        {
            MessageBox.Show("Script alert!");
        }
    }
}