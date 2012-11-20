
using System;
using System.Collections;
using System.Collections.Specialized;

namespace DotNet.Common.Collections
{
	public class NameObjectCollection : NameObjectCollectionBase
	{
		public NameObjectCollection()
			: base(StringComparer.InvariantCultureIgnoreCase)
		{
		}

		public NameObjectCollection(IEqualityComparer equalityComparer)
			: base(equalityComparer)
		{
		}
	}
}