// -----------------------------------------------------------------------
// <copyright file="Class1.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using Moq;
using NUnit.Framework;

namespace DotNet.UnitTest
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class Class1
    {
        //define interface to be mocked
        public interface IFake
        {
            bool DoSomething(string actionname);
        }

        //define the test method
        [Test]
        public void Test_Interface_IFake()
        {
            //make a mock Object by Moq
            var mo = new Mock<IFake>();

            //Setup our mock object
            mo.Setup(foo => foo.DoSomething("Ping")).Returns(true);

            //Assert it!
            Assert.AreEqual(true, mo.Object.DoSomething("Ping"));
        }
    }
}
