using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;

namespace ExtendedWebBrowser2
{
  public class NotifyCollection<T> : Collection<T>
  {
    public event EventHandler CollectionChanged;
    protected virtual void OnCollectionChanged(EventArgs e)
    {
      if (CollectionChanged != null)
        CollectionChanged(this, e);
    }

    protected override void ClearItems()
    {
      base.ClearItems();
      OnCollectionChanged(EventArgs.Empty);
    }

    protected override void InsertItem(int index, T item)
    {
      base.InsertItem(index, item);
      OnCollectionChanged(EventArgs.Empty);
    }

    protected override void RemoveItem(int index)
    {
      base.RemoveItem(index);
      OnCollectionChanged(EventArgs.Empty);
    }

    protected override void SetItem(int index, T item)
    {
      base.SetItem(index, item);
      OnCollectionChanged(EventArgs.Empty);
    }
    
  }
}
