using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace ExtendedWebBrowser2
{
  /// <summary>
  /// Used in the new navigation events
  /// </summary>
  public class BrowserExtendedNavigatingEventArgs : CancelEventArgs
  {
    private Uri _Url;
    /// <summary>
    /// The URL to navigate to
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
    public Uri Url
    {
      get { return _Url; }
    }

    private string _Frame;
    /// <summary>
    /// The name of the frame to navigate to
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
    public string Frame
    {
      get { return _Frame; }
    }

    private UrlContext navigationContext;
    /// <summary>
    /// The flags when opening a new window
    /// </summary>
    public UrlContext NavigationContext
    {
      get { return this.navigationContext; }
    }

    private object _pDisp;
    /// <summary>
    /// The pointer to ppDisp
    /// </summary>
    public object AutomationObject
    {
      get { return this._pDisp; }
      set { this._pDisp = value; }
    }

    /// <summary>
    /// Creates a new instance of WebBrowserExtendedNavigatingEventArgs
    /// </summary>
    /// <param name="automation">Pointer to the automation object of the browser</param>
    /// <param name="url">The URL to go to</param>
    /// <param name="frame">The name of the frame</param>
    /// <param name="navigationContext">The new window flags</param>
    public BrowserExtendedNavigatingEventArgs(object automation, Uri url, string frame, UrlContext navigationContext)
      : base()
    {
      _Url = url;
      _Frame = frame;
      this.navigationContext = navigationContext;
      this._pDisp = automation;
    }

  }

  // Provides data for the WebBrowser2.NavigateError event. 
  public class WebBrowserNavigateErrorEventArgs : EventArgs
  {
      private String urlValue;
      private String frameValue;
      private Int32 statusCodeValue;
      private Boolean cancelValue;

      public WebBrowserNavigateErrorEventArgs(
          String url, String frame, Int32 statusCode, Boolean cancel)
      {
          urlValue = url;
          frameValue = frame;
          statusCodeValue = statusCode;
          cancelValue = cancel;
      }

      public String Url
      {
          get { return urlValue; }
          set { urlValue = value; }
      }

      public String Frame
      {
          get { return frameValue; }
          set { frameValue = value; }
      }

      public Int32 StatusCode
      {
          get { return statusCodeValue; }
          set { statusCodeValue = value; }
      }

      public Boolean Cancel
      {
          get { return cancelValue; }
          set { cancelValue = value; }
      }
  }
}
