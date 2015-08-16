using NUnit.Framework;
using System;
using ScheduleApp;

namespace nUnitTestSchedulerApp
{
	[TestFixture]
	public class TestClass
	{
		[Test]
		public void TestCreatingTaskObject ()
		{
				string TestTaskName = "PickUpBooks";
				string TestTaskNote = "BooksAtLibrary";
				Task GetBooks = new Task(TestTaskName,TestTaskNote, 0);
				Console.WriteLine(GetBooks.TaskNotes);
				Assert.AreEqual(TestTaskNote, GetBooks.TaskNotes);
				Assert.AreEqual(TestTaskName, GetBooks.TaskName);
		}

		[Test]
		public void TestTestTaskNameLength ()
		{
			string TestTaskName = "PickUpBooks";
			string TestTaskNote = "BooksAtLibrary";
			double NameLength = TestTaskName.Length;
			Task GetBooks = new Task(TestTaskName,TestTaskNote, 0);
			Assert.AreEqual(NameLength, GetBooks.TaskName.Length);
		}
	}
}

