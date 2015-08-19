using NUnit.Framework;
using System;
using ScheduleApp;

namespace nUnitTestSchedulerApp
{
    [TestFixture]
    public class TestClass
    {
        [Test]
        public void TestCreatingTaskObject()
        {
            string TestTaskName = "PickUpBooks";
            string TestTaskNote = "BooksAtLibrary";
            Task GetBooks = new Task(TestTaskName, TestTaskNote, 0);
            Console.WriteLine(GetBooks.TaskNotes);
            Assert.AreEqual(TestTaskNote, GetBooks.TaskNotes);
            Assert.AreEqual(TestTaskName, GetBooks.TaskName);
        }

        [Test]
        public void TestTestTaskNameLength()
        {
            string TestTaskName = "PickUpBooks";
            string TestTaskNote = "BooksAtLibrary";
            double NameLength = TestTaskName.Length;
            Task GetBooks = new Task(TestTaskName, TestTaskNote, 0);
            Assert.AreEqual(NameLength, GetBooks.TaskName.Length);
        }

        //Testing the alternate struct constructor
        // does the struct have to be defined in the Scheduler class? 
        // it looks like i can't create a struct here and pass it to the Task class.
        // not like python where it can be a little vague. 

        [Test]
        public void TestStructConstructor()
        {
			Tasks x = new Task();
			x.TaskInfo.id = 1;
			x.TaskInfo.TaskName = "foo";
			x.TaskInfo.TaskNote = "bar";
        }
    }
}

