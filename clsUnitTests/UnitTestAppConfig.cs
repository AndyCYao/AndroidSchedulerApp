using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ScheduleApp;

namespace clsUnitTests
{
    [TestClass]
    public class UnitTestAppConfig
    {
        [TestMethod]
        public void AppConfigConstructorTest()
        {
            AppConfig config = new AppConfig();

            Assert.AreEqual(config.Theme.backgroundColour, 0x00FF00);
            Assert.AreEqual(config.Theme.font, "Helvetica");
            Assert.AreEqual(config.Theme.fontSize, 10);
            Assert.AreEqual(config.Theme.fontColour, 0xFFFFFF);
            Assert.AreEqual(config.Theme.defaultNotificationSound, "XGonGiveItToYa.mp3");
        }

        [TestMethod]
        public void AppConfigSerializationTest()
        {
            AppConfig config = new AppConfig();
            config.ShowClosedTasks = true;
            config.MaxTasksPerPage = 50;

            ThemeStruct theme = config.Theme;

            theme.backgroundColour = 0x0000FF;
            theme.font = "Courier";
            theme.fontSize = 20;
            theme.fontColour = 0xFF0000;
            theme.defaultNotificationSound = "ScarboroughFair.wav";
            //string path = Directory.GetCurrentDirectory() + "\\AppConfigSerialization.xml";
            string path = "AppConfigSerialization.xml";
            //config.Write(path);

            AppConfig compare = new AppConfig(path);

            Assert.AreEqual(compare.ShowClosedTasks, config.ShowClosedTasks);
            Assert.AreEqual(compare.MaxTasksPerPage, config.MaxTasksPerPage);
            Assert.AreEqual(compare.Theme.backgroundColour, config.Theme.backgroundColour);
            Assert.AreEqual(compare.Theme.font, config.Theme.font);
            Assert.AreEqual(compare.Theme.fontSize, config.Theme.fontSize);
            Assert.AreEqual(compare.Theme.fontColour, config.Theme.fontColour);
            Assert.AreEqual(compare.Theme.defaultNotificationSound, config.Theme.defaultNotificationSound);
        }
    }
}
