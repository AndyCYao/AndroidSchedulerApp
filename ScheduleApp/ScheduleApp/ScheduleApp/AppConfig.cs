using System;
using PCLStorage;
using System.Xml;
using System.Collections.Generic;
using Xamarin.Forms;


namespace ScheduleApp
{
    public struct ThemeStruct
    {
        public Xamarin.Forms.NamedSize fontSize;
        public string font;
        public Xamarin.Forms.Color fontColour;
        public Xamarin.Forms.Color backgroundColour;
    }

    public class AppConfig
    {
        private ThemeStruct m_theme;
        private string m_defaultNotificationSound;
        private uint m_maxTasksPerPage;
        private bool m_showClosedTasks;

        public Dictionary<string, NamedSize> nameToSize = new Dictionary<string, NamedSize>
        {
            { "Default", NamedSize.Default },
            { "Micro", NamedSize.Micro }, { "Small", NamedSize.Small },
            { "Medium", NamedSize.Medium }, { "Large", NamedSize.Large }
        };

        public Dictionary<NamedSize, string> sizeToName = new Dictionary<NamedSize, string>
        {
            { NamedSize.Default, "Default" },
            { NamedSize.Micro, "Micro" }, { NamedSize.Small, "Small" },
            { NamedSize.Medium, "Medium" }, { NamedSize.Large, "Large" }
        };

        public Dictionary<string, Color> nameToColour = new Dictionary<string, Color>
        {
            { "Aqua", Color.Aqua }, { "Black", Color.Black },
            { "Blue", Color.Blue }, { "Fuschia", Color.Fuchsia },
            { "Gray", Color.Gray }, { "Green", Color.Green },
            { "Lime", Color.Lime }, { "Maroon", Color.Maroon },
            { "Navy", Color.Navy }, { "Olive", Color.Olive },
            { "Purple", Color.Purple }, { "Red", Color.Red },
            { "Silver", Color.Silver }, { "Teal", Color.Teal },
            { "White", Color.White }, { "Yellow", Color.Yellow }
        };

        public Dictionary<Color, string> colourToName = new Dictionary<Color, string>
        {
            { Color.Aqua, "Aqua" }, { Color.Black, "Black" },
            { Color.Blue, "Blue" }, { Color.Fuchsia, "Fuschia" },
            { Color.Gray, "Gray" }, { Color.Green, "Green" },
            { Color.Lime, "Lime" }, { Color.Maroon, "Maroon" },
            { Color.Navy, "Navy" }, { Color.Olive, "Olive" },
            { Color.Purple, "Purple" }, { Color.Red, "Red" },
            { Color.Silver, "Silver" }, { Color.Teal, "Teal" },
            { Color.White, "White" }, { Color.Yellow, "Yellow" }
        };

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
            m_theme.backgroundColour = Xamarin.Forms.Color.Black;
            m_theme.font = "Helvetica";
            m_theme.fontColour = Xamarin.Forms.Color.White;
            m_theme.fontSize = Xamarin.Forms.NamedSize.Default;
            m_defaultNotificationSound = DependencyService.Get<iRingTones>().GetDefaultRingTone();
        }

        public async void Write(string path)
        {
            IFile file = await FileSystem.Current.LocalStorage.CreateFileAsync(path, CreationCollisionOption.ReplaceExisting);
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.CloseOutput = true;
            XmlWriter writer = XmlWriter.Create(await file.OpenAsync(FileAccess.ReadAndWrite), settings);
            
            writer.WriteStartDocument();
            writer.WriteStartElement("Config");

            writer.WriteElementString("MaxTasksPerPage", MaxTasksPerPage.ToString());
            writer.WriteElementString("ShowClosedTasks", ShowClosedTasks.ToString());
            writer.WriteElementString("DefaultNotificationSound", DefaultNotificationSound.ToString());

            writer.WriteStartElement("Theme");
                writer.WriteElementString("Font", Theme.font.ToString());
                writer.WriteElementString("FontSize", sizeToName[Theme.fontSize]);
                writer.WriteElementString("FontColour", colourToName[Theme.fontColour]);
                writer.WriteElementString("BackgroundColour", colourToName[Theme.backgroundColour]);
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
                    m_defaultNotificationSound = reader.ReadElementContentAsString();

                    reader.ReadStartElement("Theme");
                        m_theme.font = reader.ReadElementContentAsString();
                        m_theme.fontSize = nameToSize[reader.ReadElementContentAsString()];
                        m_theme.fontColour = nameToColour[reader.ReadElementContentAsString()];
                        m_theme.backgroundColour = nameToColour[reader.ReadElementContentAsString()];
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

        public string DefaultNotificationSound
        {
            get { return m_defaultNotificationSound; }
            set { m_defaultNotificationSound = value; }
        }

        public bool ShowClosedTasks
        {
            get { return m_showClosedTasks; }
            set { m_showClosedTasks = value; }
        }

        public Style GenerateLabelStyle()
        {
            var themeStyle = new Style(typeof(Label))
            {
                Setters = {
                new Setter { Property = Label.BackgroundColorProperty, Value = m_theme.backgroundColour },
                    /*new Setter { Property = Label.FontProperty, Value = m_theme.font },*/
                    new Setter { Property = Label.FontSizeProperty, Value = Device.GetNamedSize(m_theme.fontSize, typeof(Label)) },
                    new Setter { Property = Label.TextColorProperty, Value = m_theme.fontColour }
                }
            };

            return themeStyle;
        }

        public Style GenerateEntryStyle()
        {
            var themeStyle = new Style(typeof(Entry))
            {
                Setters = {
                new Setter { Property = Entry.BackgroundColorProperty, Value = m_theme.backgroundColour },
                    new Setter { Property = Entry.TextColorProperty, Value = m_theme.fontColour }
                }
            };

            return themeStyle;
        }

        public Style GeneratePickerStyle()
        {
            var themeStyle = new Style(typeof(Picker))
            {
                Setters = {
                    new Setter { Property = Picker.BackgroundColorProperty, Value = m_theme.backgroundColour },
                    new Setter { Property = Picker.StyleProperty, Value = GenerateLabelStyle() }
                }
            };

            return themeStyle;
        }

        public Style GenerateButtonStyle()
        {
            var themeStyle = new Style(typeof(Button))
            {
                Setters = {
                    new Setter { Property = Button.BackgroundColorProperty, Value = m_theme.backgroundColour },
                    new Setter { Property = Button.TextColorProperty, Value = m_theme.fontColour },
                    /*new Setter { Property = Button.FontProperty, Value = m_theme.font },*/
                    new Setter { Property = Button.FontSizeProperty, Value = Device.GetNamedSize(m_theme.fontSize, typeof(Button)) },
                    new Setter { Property = Button.BorderColorProperty, Value = Color.White },
                    new Setter { Property = Button.BorderRadiusProperty, Value = 2 },
                    new Setter { Property = Button.BorderWidthProperty, Value = 2 }
                }
            };

            return themeStyle;
        }

        public Style GeneratePageStyle()
        {
            var themeStyle = new Style(typeof(ContentPage))
            {
                Setters = {
                    new Setter { Property = ContentPage.BackgroundColorProperty, Value = m_theme.backgroundColour }
                }
            };

            return themeStyle;
        }
    }
}
