using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ScheduleApp;

/* I've been using the the nUnitTestSchedulerApp.test for unit testing Tasks, because I couldnt get Visual Studio
on my Mac. So i can't use the Visual Studio's test tools. 
i have not been updating this file
if no one is using it I suggest we delete it -AY
*/


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
