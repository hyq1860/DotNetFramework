using System;
using System.Collections.Generic;
using System.Text;

namespace ExtendedWebBrowser2
{
  class TextChangedEventArgs : EventArgs
  {
    public TextChangedEventArgs(string text)
    {
      _text = text;
    }

    string _text;
    public string Text
    {
      get { return _text; }
    }
  }
}
