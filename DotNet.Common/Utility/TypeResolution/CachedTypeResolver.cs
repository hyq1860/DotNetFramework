/*****************************************************************
 * Copyright (C) 2005-2006 Newegg Corporation
 * All rights reserved.
 * 
 * Author:   Allen Wang (Allen.G.Wang@newegg.com)
 * Create Date:  06/30/2009 15:12:41
 * Description:
 *
 * RevisionHistory
 * Date         Author               Description
 * 
*****************************************************************/
using System;
using System.Collections;
using System.Collections.Specialized;

using DotNet.Common.Diagnostics;
using DotNet.Common.Resources;

namespace DotNet.Common.Utility.TypeResolution
{
    /// <summary>
    /// Resolves (instantiates) a <see cref="System.Type"/> by it's (possibly
    /// assembly qualified) name, and caches the <see cref="System.Type"/>
    /// instance against the type name.
    /// </summary>
	public class CachedTypeResolver : ITypeResolver
    {
        #region [ Fields ]

        /// <summary>
        /// The cache, mapping type names (<see cref="System.String"/> instances) against
        /// <see cref="System.Type"/> instances.
        /// </summary>
        private IDictionary _typeCache = new HybridDictionary();

        private ITypeResolver _typeResolver = null;

        #endregion

        #region [ Constructor (s) / Destructor ]

        /// <summary>
        /// Creates a new instance of the <see cref="IBatisNet.Common.Utilities.TypesResolver.CachedTypeResolver"/> class.
        /// </summary>
        /// <param name="typeResolver">
        /// The <see cref="IBatisNet.Common.Utilities.TypesResolver.ITypeResolver"/> that this instance will delegate
        /// actual <see cref="System.Type"/> resolution to if a <see cref="System.Type"/>
        /// cannot be found in this instance's <see cref="System.Type"/> cache.
        /// </param>
        /// <exception cref="System.ArgumentNullException">
        /// If the supplied <paramref name="typeResolver"/> is <see langword="null"/>.
        /// </exception>
        public CachedTypeResolver(ITypeResolver typeResolver)
        {
            _typeResolver = typeResolver;
        }

        #endregion

        #region [ ITypeResolver Members ]

        /// <summary>
        /// Resolves the supplied <paramref name="typeName"/> to a
        /// <see cref="System.Type"/>
        /// instance.
        /// </summary>
        /// <param name="typeName">
        /// The (possibly partially assembly qualified) name of a
        /// <see cref="System.Type"/>.
        /// </param>
        /// <returns>
        /// A resolved <see cref="System.Type"/> instance.
        /// </returns>
        /// <exception cref="System.TypeLoadException">
        /// If the supplied <paramref name="typeName"/> could not be resolved
        /// to a <see cref="System.Type"/>.
        /// </exception>
        public Type Resolve(string typeName)
        {
			Check.Argument.IsNotEmpty("typeName", typeName);

            Type type = null;
            try
            {
                type = _typeCache[typeName] as Type;
                if (type == null)
                {
					lock (_typeCache)
					{
						type = _typeCache[typeName] as Type;
						if (type == null)
						{
							type = _typeResolver.Resolve(typeName);
							_typeCache[typeName] = type;
						}
					}
                }
            }
            catch (Exception ex)
            {
                if (ex is TypeLoadException)
                {
                    throw;
                }

				ThrowHelper.ThrowTypeLoadExcetpion(ex, R.TypeLoadFail, typeName);
            }

            return type;
        }

        #endregion
    }
}