using System;
using System.Collections.Generic;
using System.Text;

namespace DotNet.Common.Utility
{
	public static class Converter
	{
		/// <summary>
		/// Generic Type Convert
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="value"></param>
		/// <returns></returns>
		public static T Convert<T>(object value) where T : IConvertible
		{
			return Convert<T>(value, default(T));
		}

		public static T Convert<T>(object value, T defaultValue) where T : IConvertible
		{
			if (typeof(T).IsEnum)
			{
				return Enum2<T>(value, defaultValue);
			}
			else
			{
				try
				{
					return (T)System.Convert.ChangeType(value.ToString(), typeof(T));
				}
				catch { }
				return defaultValue;
			}
		}

		/// <summary>
		/// Generic Type Convert with FormatProvider
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="value"></param>
		/// <param name="provider"></param>
		/// <returns></returns>
		public static T Convert<T>(object value, IFormatProvider provider) where T : IConvertible
		{
			try
			{
				return (T)System.Convert.ChangeType(value.ToString(), typeof(T), provider);
			}
			catch { }
			return default(T);
		}

		#region Enum
		/// <summary>
		/// Enum parser
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="value"></param>
		/// <returns></returns>
		public static T Enum<T>(object value) where T : struct
		{
			return Enum<T>(value, default(T));
		}

		public static T Enum<T>(object value, T defaultValue) where T : struct
		{
			try
			{
				T val = (T)System.Enum.Parse(typeof(T), value.ToString(), true);
				if (System.Enum.IsDefined(typeof(T), val.ToString()))
				{
					return val;
				}
			}
			catch { }

			return defaultValue;
		}
		private static T Enum2<T>(object value, T defaultValue) where T : IConvertible
		{
			try
			{
				T val = (T)System.Enum.Parse(typeof(T), value.ToString(), true);
				if (System.Enum.IsDefined(typeof(T), val.ToString()))
				{
					return val;
				}
			}
			catch { }

			return defaultValue;
		}
		#endregion

		#region ToInt
		public static int ToInt(object input)
		{
			return ToInt(input, 0);
		}
		public static int ToInt(object input, int defaultValue)
		{
			if (input == null)
			{
				return defaultValue;
			}
			int output = defaultValue;
			int.TryParse(input.ToString(), out output);
			return output;
		}
		#endregion

		#region ToUInt
		public static uint ToUInt(object input)
		{
			return ToUInt(input, 0);
		}
		public static uint ToUInt(object input, uint defaultValue)
		{
			if (input == null)
			{
				return defaultValue;
			}
			uint output = defaultValue;
			uint.TryParse(input.ToString(), out output);

			return output;
		}
		#endregion

		#region ToLong
		public static long ToLong(object input)
		{
			return ToLong(input, 0);
		}
		public static long ToLong(object input, long defaultValue)
		{
			if (input == null)
			{
				return defaultValue;
			}
			long output = defaultValue;
			long.TryParse(input.ToString(), out output);
			return output;
		}
		#endregion

		#region ToULong
		public static ulong ToULong(object input)
		{
			return ToULong(input, 0);
		}
		public static ulong ToULong(object input, ulong defaultValue)
		{
			if (input == null)
			{
				return defaultValue;
			}
			ulong output = defaultValue;
			ulong.TryParse(input.ToString(), out output);
			return output;
		}
		#endregion

		#region ToDecimal
		public static decimal ToDecimal(object input)
		{
			return ToDecimal(input, new decimal());
		}
		public static decimal ToDecimal(object input, decimal defaultValue)
		{
			if (input == null)
			{
				return defaultValue;
			}
			decimal output = defaultValue;
			decimal.TryParse(input.ToString(), out output);
			return output;
		}
		#endregion

		#region ToDateTime
		public static DateTime ToDateTime(object input)
		{
			return ToDateTime(input, DateTime.MinValue);
		}
		public static DateTime ToDateTime(object input, DateTime defaultValue)
		{
			if (input == null)
			{
				return defaultValue;
			}
			DateTime output = defaultValue;
			DateTime.TryParse(input.ToString(), out output);
			return output;
		}
		#endregion

		#region ToBoolean
		public static bool ToBoolean(object value)
		{
			bool output = false;
			if (bool.TryParse(value.ToString(), out output))
			{
				return output;
			}
			return false;
		}
		#endregion

		#region ToList
		public static IList<T> ToList<T>(T[] array)
		{
			IList<T> listT = null;
			if (array != null && array.Length > 0)
			{
				listT = new List<T>(array.Length);
				for (int i = 0; i < array.Length; i++)
				{
					T obj = array[i];
					if (obj == null) continue;

					listT.Add(obj);
				}
			}
			return listT;
		}
		#endregion
	}
}
