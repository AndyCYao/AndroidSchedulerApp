using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SchedulerTask;

namespace clsTaskUnitTest
{
    [TestClass]
    public class UnitTestTasks
    {
        [TestMethod]
        public void CheckingIfNamesAndNotesAreLoaded()
        {
            string TestTaskName = "PickUpBook-HarryPotter";
            string TestTaskNote = "BooksAtLibrary";
            Task GetBooks = new Task(TestTaskName, TestTaskNote);
            //Console.WriteLine(GetBooks.TaskNotes);
            Assert.AreEqual(TestTaskNote, GetBooks.TaskNotes);
            Assert.AreEqual(TestTaskName, GetBooks.TaskName);
        }
        [TestMethod]
        public void CheckingIfTaskNameLessThan40IsShortend()
        {
            string TestTaskName = "PickUpBook-HarryPotterAndTheChamberOfSecrets";
            string TestTaskNote = "BooksAtLibrary";
            Task GetBooks = new Task(TestTaskName, TestTaskNote);
            //Console.WriteLine(GetBooks.TaskNotes);
            Assert.AreEqual(TestTaskNote, GetBooks.TaskNotes);
            Assert.AreEqual(TestTaskName.Substring(0,40), GetBooks.TaskName);
        }
    }
}
