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
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace DotNet.Common.Utility.TypeResolution
{
	/// <summary>
	/// Holder for the generic arguments when using type parameters.
	/// </summary>
	/// <remarks>
	/// <p>
	/// Type parameters can be applied to classes, interfaces, 
	/// structures, methods, delegates, etc...
	/// </p>
	/// </remarks>
	internal class GenericArgumentsInfo
	{
		#region [ Constants ]

		/// <summary>
		/// The generic arguments prefix.
		/// </summary>
		public const string GENERIC_ARGUMENTS_PREFIX = "[[";

		/// <summary>
		/// The generic arguments suffix.
		/// </summary>
		public const string GENERIC_ARGUMENTS_SUFFIX = "]]";

		/// <summary>
		/// The character that separates a list of generic arguments.
		/// </summary>
		public const string GENERIC_ARGUMENTS_SEPARATOR = "],[";

		#endregion

		#region [ Fields ]

		private string _unresolvedGenericTypeName = string.Empty;
		private string[] _unresolvedGenericArguments = null;
		private readonly static Regex generic = new Regex(@"`\d*\[\[", RegexOptions.Compiled);

		#endregion

		#region [ Constructor (s) / Destructor ]

		/// <summary>
		/// Creates a new instance of the GenericArgumentsInfo class.
		/// </summary>
		/// <param name="value">
		/// The string value to parse looking for a generic definition
		/// and retrieving its generic arguments.
		/// </param>
		public GenericArgumentsInfo(string value)
		{
			ParseGenericArguments(value);
		}

		#endregion

		#region [ Properties ]

		/// <summary>
		/// The (unresolved) generic type name portion 
		/// of the original value when parsing a generic type.
		/// </summary>
		public string GenericTypeName
		{
			get { return _unresolvedGenericTypeName; }
		}

		/// <summary>
		/// Is the string value contains generic arguments ?
		/// </summary>
		/// <remarks>
		/// <p>
		/// A generic argument can be a type parameter or a type argument.
		/// </p>
		/// </remarks>
		public bool ContainsGenericArguments
		{
			get
			{
				return (_unresolvedGenericArguments != null &&
					_unresolvedGenericArguments.Length > 0);
			}
		}

		/// <summary>
		/// Is generic arguments only contains type parameters ?
		/// </summary>
		public bool IsGenericDefinition
		{
			get
			{
				if (_unresolvedGenericArguments == null)
					return false;

				foreach (string arg in _unresolvedGenericArguments)
				{
					if (arg.Length > 0)
						return false;
				}
				return true;
			}
		}

		#endregion

		#region  [ Methods ]

		/// <summary>
		/// Returns an array of unresolved generic arguments types.
		/// </summary>
		/// <remarks>
		/// <p>
		/// A empty string represents a type parameter that 
		/// did not have been substituted by a specific type.
		/// </p>
		/// </remarks>
		/// <returns>
		/// An array of strings that represents the unresolved generic 
		/// arguments types or an empty array if not generic.
		/// </returns>
		public string[] GetGenericArguments()
		{
			if (_unresolvedGenericArguments == null)
			{
				return new string[] { };
			}

			return _unresolvedGenericArguments;
		}

		private void ParseGenericArguments(string originalString)
		{
			// Check for match
			bool isMatch = generic.IsMatch(originalString);
			if (!isMatch)
			{
				_unresolvedGenericTypeName = originalString;
			}
			else
			{
				int argsStartIndex = originalString.IndexOf(GENERIC_ARGUMENTS_PREFIX);
				int argsEndIndex = originalString.LastIndexOf(GENERIC_ARGUMENTS_SUFFIX);
				if (argsEndIndex != -1)
				{
					SplitGenericArguments(originalString.Substring(
						argsStartIndex + 1, argsEndIndex - argsStartIndex));

					_unresolvedGenericTypeName = originalString.Remove(argsStartIndex, argsEndIndex - argsStartIndex + 2);
				}
			}
		}

		private void SplitGenericArguments(string originalArgs)
		{
			IList<string> arguments = new List<string>();

			if (originalArgs.Contains(GENERIC_ARGUMENTS_SEPARATOR))
			{
				arguments = Parse(originalArgs);
			}
			else
			{
				string argument = originalArgs.Substring(1, originalArgs.Length - 2).Trim();
				arguments.Add(argument);
			}
			_unresolvedGenericArguments = new string[arguments.Count];
			arguments.CopyTo(_unresolvedGenericArguments, 0);
		}

		private IList<string> Parse(string args)
		{
			StringBuilder argument = new StringBuilder();
			IList<string> arguments = new List<string>();

			TextReader input = new StringReader(args);
			int nbrOfRightDelimiter = 0;
			bool findRight = false;
			do
			{
				char ch = (char)input.Read();
				if (ch == '[')
				{
					nbrOfRightDelimiter++;
					findRight = true;
				}
				else if (ch == ']')
				{
					nbrOfRightDelimiter--;
				}
				argument.Append(ch);

				//Find one argument
				if (findRight && nbrOfRightDelimiter == 0)
				{
					string arg = argument.ToString();
					arg = arg.Substring(1, arg.Length - 2);
					arguments.Add(arg);
					input.Read();
					argument = new StringBuilder();
				}
			}
			while (input.Peek() != -1);

			return arguments;
		}

		#endregion
	}
}