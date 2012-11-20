using System;
using System.Collections.Generic;
using System.Text;

namespace ExtendedWebBrowser2
{
  public class ScriptErrorManager
  {
    private ScriptErrorManager()
    {
      _scriptErrors = new NotifyCollection<ScriptError>();
    }

    private NotifyCollection<ScriptError> _scriptErrors;

    private static object lockObject = new object();
    private static ScriptErrorManager _instance;

    public static ScriptErrorManager Instance
    {
      get
      {
        if (_instance == null)
        {
          lock (lockObject)
          {
            if (_instance == null)
              _instance = new ScriptErrorManager();
          }
        }
        return _instance;
      }
    }

    public NotifyCollection<ScriptError> ScriptErrors
    {
      get
      {
        return _scriptErrors;
      }
    }

    //private ScriptErrorWindow _errorWindow;

    public void RegisterScriptError(Uri url, string description, int lineNumber)
    {
        this._scriptErrors.Add(new ScriptError(url, description, lineNumber));
        //if (SettingsHelper.Current.ShowScriptErrors)
        //{
        //    ShowWindow();
        //}
    }

    //public void ShowWindow()
    //{
    //  if (_errorWindow == null || _errorWindow.IsDisposed)
    //  {
    //    _errorWindow = new ScriptErrorWindow();
    //  }
    //  _errorWindow.Show();
    //}
  }
}
