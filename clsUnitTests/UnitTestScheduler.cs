using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ScheduleApp;

namespace clsUnitTests
{
    [TestClass]
    public class UnitTestScheduler
    {
        [TestMethod]
        public void SchedulerSerializationTest()
        {
            Scheduler scheduler = new Scheduler();
            AppTask task1 = new AppTask();
            AppTask task2 = new AppTask();

            task1.TaskName = "Task1";
            task1.TaskNotes = "Task One!";
            task1.Frequency = 2;
            task1.FrequencyUnit = "Days";
            task1.Done = false;
            task1.ReminderEnd = System.DateTime.Now;
            scheduler.AddTask(task1);

            task2.TaskName = "Task2";
            task2.TaskNotes = "Task Two.";
            task2.Frequency = 4;
            task2.FrequencyUnit = "Months";
            task2.Done = true;
            task2.ReminderEnd = System.DateTime.Now;
            scheduler.AddTask(task2);

            Assert.AreEqual(2, scheduler.TaskCount);

            scheduler.Write("Test.xml");

            Scheduler schedulerToCompare = new Scheduler("Test.xml");
            Assert.AreEqual(2, schedulerToCompare.TaskCount);
            Assert.AreEqual(1, scheduler.GetTasks(false).Count);
            Assert.AreEqual(scheduler.TaskAt(0).TaskName, "Task1");
            Assert.AreEqual(scheduler.TaskAt(1).TaskName, "Task2");
        }
    }
}