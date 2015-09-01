using System;
using System.Xml.Linq;
using PCLStorage;
using System.Xml;

namespace ScheduleApp
{
    public struct ThemeStruct
    {
        public string defaultNotificationSound;
        public int fontSize;
        public string font;
        public int fontColour;
        public int backgroundColour;
    }

    public class AppConfig
    {
        private ThemeStruct m_theme;
        private uint m_maxTasksPerPage;
        private bool m_showClosedTasks;

        public AppConfig(string fileToLoad = "")
        {
            if (fileToLoad.Length > 0)
            {
                System.Threading.Tasks.Task.Run(() => Read(fileToLoad)).Wait();
            }
            else
            {
                restoreDefaults();
            }
        }

        public void restoreDefaults()
        {
            m_maxTasksPerPage = 10;
            m_showClosedTasks = false;
            m_theme.backgroundColour = 0x00FF00;
            m_theme.font = "Helvetica";
            m_theme.fontColour = 0xFFFFFF;
            m_theme.fontSize = 10;
            m_theme.defaultNotificationSound = "XGonGiveItToYa.mp3";
        }

        public async void Write(string path)
        {
            IFile file = await FileSystem.Current.LocalStorage.CreateFileAsync(path, CreationCollisionOption.ReplaceExisting);
            XmlWriter writer = XmlWriter.Create(await file.OpenAsync(FileAccess.ReadAndWrite));
            XDocument doc = new XDocument();
            writer.WriteStartDocument();
            writer.WriteStartElement("Config");

            writer.WriteElementString("MaxTasksPerPage", MaxTasksPerPage.ToString());
            writer.WriteElementString("ShowClosedTasks", ShowClosedTasks.ToString());

            writer.WriteStartElement("Theme");
            writer.WriteElementString("DefaultNotificationSound", Theme.defaultNotificationSound.ToString());
            writer.WriteElementString("Font", Theme.font.ToString());
            writer.WriteElementString("FontSize", Theme.fontSize.ToString());
            writer.WriteElementString("FontColour", Theme.fontColour.ToString());
            writer.WriteElementString("BackgroundColour", Theme.backgroundColour.ToString());
            writer.WriteEndElement();

            writer.WriteEndElement();
            writer.WriteEndDocument();
            writer.Dispose();
        }

        public async void Read(string path)
        {
            ExistenceCheckResult result = await FileSystem.Current.LocalStorage.CheckExistsAsync(path);

            if (result == ExistenceCheckResult.FileExists)
            {
                IFile file = await FileSystem.Current.LocalStorage.GetFileAsync(path);
                XmlReader reader = XmlReader.Create(await file.OpenAsync(FileAccess.Read));

                reader.ReadStartElement("Config");
                    MaxTasksPerPage = (uint)reader.ReadElementContentAsInt();
                    ShowClosedTasks = Boolean.Parse(reader.ReadElementContentAsString());

                    reader.ReadStartElement("Theme");
                        m_theme.defaultNotificationSound = reader.ReadElementContentAsString();
                        m_theme.font = reader.ReadElementContentAsString();
                        m_theme.fontSize = reader.ReadElementContentAsInt();
                        m_theme.fontColour = reader.ReadElementContentAsInt();
                        m_theme.backgroundColour = reader.ReadElementContentAsInt();
                    reader.ReadEndElement();

                reader.ReadEndElement();
            }
            else
            {
                restoreDefaults();
            }
        }

        public ThemeStruct Theme
        {
            get { return m_theme; }
            set { m_theme = value; }
        }

        public uint MaxTasksPerPage
        {
            get { return m_maxTasksPerPage; }
            set { m_maxTasksPerPage = value; }
        }

        public bool ShowClosedTasks
        {
            get { return m_showClosedTasks; }
            set { m_showClosedTasks = value; }
        }
    }
}
