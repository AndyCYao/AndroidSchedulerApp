using NUnit.Framework;
using System;
using SchedulerTask;

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
				Task GetBooks = new Task (TestTaskName,TestTaskNote);
				Console.WriteLine (GetBooks.TaskNotes);
				Assert.AreEqual (TestTaskNote, GetBooks.TaskNotes);
				Assert.AreEqual (TestTaskName, GetBooks.TaskName);
		}
	}
}

