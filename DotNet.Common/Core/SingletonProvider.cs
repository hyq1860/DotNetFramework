using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace DotNet.Common.Core
{
	public class SingletonProvider<T> where T : class
	{
		SingletonProvider() { }

		class SingletonCreator
		{
			static SingletonCreator() { }

			private static T CreateInstance()
			{
				ConstructorInfo constructorInfo = typeof(T).GetConstructor(
					BindingFlags.Instance | BindingFlags.NonPublic,
					Type.DefaultBinder,
					Type.EmptyTypes,
					null);

				if (constructorInfo != null)
				{
					return constructorInfo.Invoke(null) as T;
				}
				else
				{
					// alternatively, throw an exception indicating the type parameter
					// should have a private parameterless constructor
					throw new NotImplementedException("Type parameter should have a private parameterless constructor.");
				}
			}

			internal static readonly T instance = CreateInstance();
		}

		public static T UniqueInstance
		{
			get { return SingletonCreator.instance; }
		}
	}
}
