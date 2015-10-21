using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ScheduleApp;
using System.Xml;
//Added AY Oct 2015

namespace clsUnitTests
{
    [TestClass]
    public class UnitTestUtility
    {
        [TestMethod]
        public void ReturnFolder()
        {
            string TestFolder = "RingTones";
            string CurrentFolder = Utility.GetRingTones();
            
            //Console.WriteLine(GetBooks.TaskNotes);
            Assert.AreEqual(TestFolder, CurrentFolder);
            
        }
    }
}
