using System;
using System.Collections.Generic;
using System.Text;

namespace ExtendedWebBrowser2
{
  class ScriptError
  {
    public ScriptError(Uri url, string description, int lineNumber)
    {
      _url = url;
      _description = description;
      _lineNumber = lineNumber;
    }

    private int _lineNumber;
    public int LineNumber
    {
      get { return _lineNumber; }
    }

    private string _description;
    public string Description
    {
      get { return _description; }
    }

    private Uri _url;
    public Uri Url
    {
      get { return _url; }
    }

  }
}
