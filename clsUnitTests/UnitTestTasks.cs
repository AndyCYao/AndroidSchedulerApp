using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ScheduleApp;

namespace clsUnitTests
{
    [TestClass]
    public class UnitTestTasks
    {
        [TestMethod]
        public void CheckingIfNamesAndNotesAreLoaded()
        {
            string TestTaskName = "PickUpBook-HarryPotter";
            string TestTaskNote = "BooksAtLibrary";
            Task GetBooks = new Task(TestTaskName, TestTaskNote, 0);
            //Console.WriteLine(GetBooks.TaskNotes);
            Assert.AreEqual(TestTaskNote, GetBooks.TaskNotes);
            Assert.AreEqual(TestTaskName, GetBooks.TaskName);
        }
        [TestMethod]
        public void CheckingIfTaskNameLessThan40IsShortend()
        {
            string TestTaskName = "PickUpBook-HarryPotterAndTheChamberOfSecrets";
            string TestTaskNote = "BooksAtLibrary";
            Task GetBooks = new Task(TestTaskName, TestTaskNote, 0);
            //Console.WriteLine(GetBooks.TaskNotes);
            Assert.AreEqual(TestTaskNote, GetBooks.TaskNotes);
            Assert.AreEqual(TestTaskName.Substring(0,40), GetBooks.TaskName);
        }
    }
}
