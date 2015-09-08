using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ScheduleApp;

namespace clsUnitTests
{
    [TestClass]
    public class UnitTestCore
    {
        [TestMethod]
        public void CoreGetterTest()
        {
            Core core = Core.GetCore();
            PhraseManager manager = core.GetPhraseManager();
            Assert.AreEqual(manager.PhraseCount(), 0);

            Scheduler scheduler = core.GetScheduler();
            Assert.AreEqual(scheduler.Count, 0);

            AppConfig config = core.GetConfig();
            Assert.AreEqual(config.ShowClosedTasks, false);
        }
    }
}