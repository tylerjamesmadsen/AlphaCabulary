using System;
using AlphaCabulary.ApplicationCore.Catalog.EventArgs;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AlphaCabulary.ApplicationCore.Test.EventArgsTests
{
    [TestClass]
    public class TimerEventArgsShould
    {
        [TestMethod]
        public void ReturnATwoMinuteTimeString()
        {
            //-- arrange
            var e = new TimerEventArgs(120);
            const string EXPECTED = "02:00";

            //-- act
            string actual = e.ToString();

            //-- assert
            Assert.AreEqual(EXPECTED, actual);
        }
    }
}
