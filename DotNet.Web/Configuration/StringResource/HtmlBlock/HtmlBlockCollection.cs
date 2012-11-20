using System.Collections.ObjectModel;

namespace DotNet.Web.Configuration
{
    /// <summary>
    /// A custome collection class for htmlblock
    /// </summary>
    public class HtmlBlockCollection : KeyedCollection<string, HtmlBlockItem>
    {
        protected override string GetKeyForItem(HtmlBlockItem item)
        {
            return item.Alias;
        }
    }
}
