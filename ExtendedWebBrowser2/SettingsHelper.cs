using System;
using System.Collections.Generic;
using System.Text;
using ExtendedWebBrowser2.Properties;

namespace ExtendedWebBrowser2
{
  /// <summary>
  /// This class is used for obtaining and storing settings
  /// </summary>
  /// <remarks>
  /// This is a single instance class, so that there is only one
  /// instance needed of the <see cref="Settings"/> class
  /// </remarks>
  class SettingsHelper
  {
    /// <summary>
    /// Creates a new instance of the <see cref="SettingsHelper"/> class
    /// </summary>
    private SettingsHelper()
    {
      _mySettings = new Settings();
    }

    /// <summary>
    /// Stores the instance of the <see cref="Settings"/> class
    /// </summary>
    private Settings _mySettings;

    /// <summary>
    /// Stores the instance of the <see cref="SettingsHelper"/> class
    /// </summary>
    private static SettingsHelper _instance;
    
    /// <summary>
    /// An object for locking the thread, when needed
    /// </summary>
    private static object _lockObject = new object();

    /// <summary>
    /// Obtains the current instance of the <see cref="SettingsHelper"/> class.
    /// </summary>
    /// <remarks>
    /// If there is no instance of the <see cref="SettingsHelper"/> class, one will be created
    /// </remarks>
    public static SettingsHelper Current
    {
      get 
      {
        if (_instance == null)
        {
          lock (_lockObject)
          {
            if (_instance == null)
              _instance = new SettingsHelper();
          }
        }
        return _instance; 
      }
    }

    /// <summary>
    /// Gets or sets the <see cref="PopupBlockerFilterLevel"/>
    /// </summary>
    public PopupBlockerFilterLevel FilterLevel
    {
      get { return _mySettings.FilterLevel; }
      set { _mySettings.FilterLevel = value; }
    }

    /// <summary>
    /// Gets or sets a value indicating if script errors should be shown
    /// </summary>
    public bool ShowScriptErrors
    {
      get { return _mySettings.ShowScriptErrors; }
      set { _mySettings.ShowScriptErrors = value; }
    }

    /// <summary>
    /// Saves the <see cref="Settings"/>
    /// </summary>
    public void Save()
    {
      _mySettings.Save();
    }

  }
}
