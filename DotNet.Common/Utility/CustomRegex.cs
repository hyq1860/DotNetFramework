using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace DotNet.Common.Utility
{
	public class CustomRegex
	{
		private Regex _re;
		private string _pattern = string.Empty;
		private string _data = string.Empty;

		public CustomRegex(string pattern) :
			this(pattern, RegexOptions.None)
		{
		}
		public CustomRegex(string pattern, RegexOptions options) :
			this(pattern, string.Empty, options)
		{
		}
		public CustomRegex(string pattern, string data, RegexOptions options)
		{
			if (string.IsNullOrEmpty(pattern))
			{
				throw new ArgumentNullException("Regex pattern can't be empty!");
			}

			_re = new Regex(pattern, options | RegexOptions.Compiled);
			_pattern = pattern;
			_data = data;
		}

		public Regex Regex
		{
			get { return _re; }
		}

		public bool IsMatch(string data)
		{
			if (!string.IsNullOrEmpty(data))
			{
				return _re.IsMatch(data);
			}
			return false;
		}

		public bool IsMatch(string data, int startat)
		{
			if (!string.IsNullOrEmpty(data))
			{
				return _re.IsMatch(data, startat);
			}
			return false;
		}

		public string Replace(string replacement)
		{
			return Replace(_data, replacement);
		}
		public string Replace(string data, string replacement)
		{
			if (string.IsNullOrEmpty(data))
			{
				return replacement;
			}

			if (_re.IsMatch(data))
			{
				MatchCollection mc = _re.Matches(data);
				foreach (Match m in mc)
				{
					if (!string.IsNullOrEmpty(m.Value))
					{
						data = data.Replace(m.Value, replacement);
					}
				}
			}
			return data;
		}

		public string Result(string replacement)
		{
			return Result(_data, replacement);
		}

		public string Result(string data, string replacement)
		{
			if (string.IsNullOrEmpty(data) ||
				string.IsNullOrEmpty(replacement) ||
				!_re.IsMatch(data))
			{
				return string.Empty;
			}

			return _re.Match(data).Result(replacement);
		}

		public string Group(string data, int index)
		{
			string val = string.Empty;
			if (!string.IsNullOrEmpty(data) && index > 0 && _re.IsMatch(data))
			{
				GroupCollection gc = _re.Match(data).Groups;
				if (index < gc.Count)
				{
					val = gc[index].Value;
				}
			}
			return val;
		}

		public string Group(string data, string name)
		{
			string val = string.Empty;
			if (!string.IsNullOrEmpty(data) && !string.IsNullOrEmpty(name) && _re.IsMatch(data))
			{
				GroupCollection gc = _re.Match(data).Groups;
				val = gc[name].Value;
			}
			return val;
		}
	}
}
