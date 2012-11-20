using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace DotNet.Web.Configuration
{
    /// <summary>
    /// A custome collection class for ListGroup
    /// </summary>
    public class ListGroupCollection : KeyedCollection<string ,ListGroup>
    {
        protected override string GetKeyForItem(ListGroup item)
        {
            return item.Name;
        }
    }
}
