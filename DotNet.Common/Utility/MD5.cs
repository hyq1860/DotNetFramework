/*****************************************************************
 * Copyright (C) 2008 Renative.com
 * 
 * Author:   
 * Create Date:  
 * Usage:
 *
 * Date         Author               Description
 * 
*****************************************************************/

using System;
using System.Text;
using System.Security.Cryptography;

namespace DotNet.Common.Utility
{
	public class MD5
	{
		private byte[] _md5 = null;
		private string _input = string.Empty;
		private bool _igonreCase = false;

		public MD5(string input) :
			this(input, false)
		{		
		}

		public MD5(string input, bool igonreCase)
		{
			_input = input;
			_igonreCase = igonreCase;

			Update(input, igonreCase);
		}

		public byte[] Digest
		{
			get
			{
				return _md5;
			}
		}

		public string HexDigest 
		{
			get
			{
				if (_md5 != null)
				{
					return BitConverter.ToString(_md5).Replace("-", "").ToLower();
				}
				return string.Empty;
			}
		}

		public UInt16 UInt16
		{
			get
			{
				if (_md5 != null)
				{
					return BitConverter.ToUInt16(_md5, 0);
				}
				return 0;				
			}
		}

		public UInt32 UInt32
		{
			get
			{
				if (_md5 != null)
				{
					return BitConverter.ToUInt32(_md5, 0);
				}
				return 0;
			}
		}

		public string HexUInt32
		{
			get
			{
				if (_md5 != null)
				{
					return BitConverter.ToUInt32(_md5, 0).ToString("x");
				}
				return string.Empty;
			}
		}

		public UInt64 UInt64
		{
			get
			{
				if (_md5 != null)
				{
					return BitConverter.ToUInt64(_md5, 0);
				}
				return 0;
			}
		}

		public string HexUInt64
		{
			get
			{
				if (_md5 != null)
				{
					return BitConverter.ToUInt64(_md5, 0).ToString("x");
				}
				return string.Empty;
			}
		}

		public MD5 Update(string input)
		{
			return Update(input, false);
		}

		public MD5 Update(string input, bool igonreCase)
		{
			_input = input;
			_igonreCase = igonreCase;

			if (string.IsNullOrEmpty(input))
			{
				input = string.Empty;
			}

			if (igonreCase)
			{
				input = input.ToUpper();
			}

			try
			{
				_md5 = MD5CryptoServiceProvider.Create().ComputeHash(Encoding.UTF8.GetBytes(input));
			}
			catch { }

			return this;
		}
	}	
}
