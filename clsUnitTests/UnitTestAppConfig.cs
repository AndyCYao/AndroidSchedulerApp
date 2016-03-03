using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ScheduleApp;
using Xamarin.Forms;

namespace clsUnitTests
{
    [TestClass]
    public class UnitTestAppConfig
    {
        [TestMethod]
        public void AppConfigConstructorTest()
        {
            AppConfig config = new AppConfig();

            Assert.AreEqual(config.Theme.backgroundColour, Color.Black);
            Assert.AreEqual(config.Theme.font, "Helvetica");
            Assert.AreEqual(config.Theme.fontSize, Xamarin.Forms.NamedSize.Default);
            Assert.AreEqual(config.Theme.fontColour, Color.White);
            Assert.AreEqual(config.DefaultNotificationSound, "");
        }

        [TestMethod]
        public void AppConfigSerializationTest()
        {
            AppConfig config = new AppConfig();
            config.ShowClosedTasks = true;
            config.MaxTasksPerPage = 50;
            config.DefaultNotificationSound = "ScarboroughFair.wav";

            ThemeStruct theme = config.Theme;

            theme.backgroundColour = Color.Teal;
            theme.font = "Courier";
            theme.fontSize = Xamarin.Forms.NamedSize.Large;
            theme.fontColour = Color.Red;
            //string path = Directory.GetCurrentDirectory() + "\\AppConfigSerialization.xml";
            config.Theme = theme;
            string path = "AppConfigSerialization.xml";
            config.Write(path);

            AppConfig compare = new AppConfig(path);

            Assert.AreEqual(compare.ShowClosedTasks, config.ShowClosedTasks);
            Assert.AreEqual(compare.MaxTasksPerPage, config.MaxTasksPerPage);
            Assert.AreEqual(compare.Theme.backgroundColour, config.Theme.backgroundColour);
            Assert.AreEqual(compare.Theme.font, config.Theme.font);
            Assert.AreEqual(compare.Theme.fontSize, config.Theme.fontSize);
            Assert.AreEqual(compare.Theme.fontColour, config.Theme.fontColour);
            Assert.AreEqual(compare.DefaultNotificationSound, config.DefaultNotificationSound);
        }
    }
}
