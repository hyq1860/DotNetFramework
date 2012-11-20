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


namespace DotNet.Common.Utility.TypeResolution
{
	/// <summary>
	/// Holds data about a <see cref="System.Type"/> and it's
	/// attendant <see cref="System.Reflection.Assembly"/>.
	/// </summary>
	internal class TypeAssemblyInfo
	{
		#region [ Constants ]

		/// <summary>
		/// The string that separates a <see cref="System.Type"/> name
		/// from the name of it's attendant <see cref="System.Reflection.Assembly"/>
		/// in an assembly qualified type name.
		/// </summary>
		public const string TYPE_ASSEMBLY_SEPARATOR = ",";
		public const string NULLABLE_TYPE = "System.Nullable";
		public const string NULLABLE_TYPE_ASSEMBLY_SEPARATOR = "]],";

		#endregion

		#region [ Fields ]

		private string _unresolvedAssemblyName = string.Empty;
		private string _unresolvedTypeName = string.Empty;

		#endregion

		#region [ Constructor (s) / Destructor ]

		/// <summary>
		/// Creates a new instance of the TypeAssemblyInfo class.
		/// </summary>
		/// <param name="unresolvedTypeName">
		/// The unresolved name of a <see cref="System.Type"/>.
		/// </param>
		public TypeAssemblyInfo(string unresolvedTypeName)
		{
			SplitTypeAndAssemblyNames(unresolvedTypeName);
		}

		#endregion

		#region [ Properties ]

		/// <summary>
		/// The (unresolved) type name portion of the original type name.
		/// </summary>
		public string TypeName
		{
			get { return _unresolvedTypeName; }
		}

		/// <summary>
		/// The (unresolved, possibly partial) name of the attandant assembly.
		/// </summary>
		public string AssemblyName
		{
			get { return _unresolvedAssemblyName; }
		}

		/// <summary>
		/// Is the type name being resolved assembly qualified?
		/// </summary>
		public bool IsAssemblyQualified
		{
			get { return !StringHelper.IsEmpty(AssemblyName); }
		}

		#endregion

		#region [ Methods ]

		private void SplitTypeAndAssemblyNames(string originalTypeName)
		{
			if (originalTypeName.StartsWith(NULLABLE_TYPE))
			{
				int typeAssemblyIndex = originalTypeName.IndexOf(NULLABLE_TYPE_ASSEMBLY_SEPARATOR);
				if (typeAssemblyIndex < 0)
				{
					_unresolvedTypeName = originalTypeName;
				}
				else
				{
					_unresolvedTypeName = originalTypeName.Substring(0, typeAssemblyIndex + 2).Trim();
					_unresolvedAssemblyName = originalTypeName.Substring(typeAssemblyIndex + 3).Trim();
				}
			}
			else
			{
				int typeAssemblyIndex = originalTypeName.IndexOf(TYPE_ASSEMBLY_SEPARATOR);
				if (typeAssemblyIndex < 0)
				{
					_unresolvedTypeName = originalTypeName;
				}
				else
				{
					_unresolvedTypeName = originalTypeName.Substring(0, typeAssemblyIndex).Trim();
					_unresolvedAssemblyName = originalTypeName.Substring(typeAssemblyIndex + 1).Trim();
				}
			}
		}

		#endregion
	}
}