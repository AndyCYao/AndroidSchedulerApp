using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Diagnostics;

//Controls are listed in this document below
//https://developer.xamarin.com/guides/cross-platform/xamarin-forms/controls/views/
namespace ScheduleApp
{
	public class pageAppConfig:ContentPage
	{
        public pageAppConfig()
		{
            Core core = Core.GetCore();
            AppConfig config = core.GetConfig();
            
            Title = "Application Configuration";

            var defaultNotificationLabel = new Label { Text = "Default Notification Sound" };
            var defaultNotificationLabelDesc = new Label { Text = "Represents the default ring tone to assign for new tasks." };
			var ringTonePicker = new Picker{ };

            List<Tuple<String, String>> rings = DependencyService.Get<iRingTones>().GetRingTones();

            foreach (var ring in rings)
            {
                ringTonePicker.Items.Add(ring.Item1);
            }
            
            ringTonePicker.SetBinding(Entry.TextProperty, "Default Notification Sound");
            ringTonePicker.Title = "Default Notification Sound";
            ringTonePicker.SelectedIndex = 0;

            for (int i = 0; i < ringTonePicker.Items.Count; i++)
            {
                if (config.Theme.defaultNotificationSound == ringTonePicker.Items[i])
                {
                    ringTonePicker.SelectedIndex = i;
                    break;
                }
            }

            //Dec 21st 2015
            ringTonePicker.SelectedIndexChanged += (sender, args) =>
            {
                //print what is the URI of the selected ringtone
                string searchFor = ringTonePicker.Items[ringTonePicker.SelectedIndex];
                foreach (var ring in rings)
                {
                    if (ring.Item1 == searchFor)
                    {
                        string searchURI = ring.Item2;
                        DisplayAlert("Check", searchURI, "Ok");
                        DependencyService.Get<playRingTones>().playRingTones(searchURI);
                    }
                }
            };
            //Mediaplayer

            

            var fontLabel = new Label { Text = "Font" };
            var fontPicker = new Picker();

            fontPicker.Items.Add("Arial");
            fontPicker.Items.Add("Courier");
            fontPicker.Items.Add("Helvetica");
            fontPicker.Items.Add("Times New Roman");
            fontPicker.SetBinding (Entry.TextProperty, "Font");
            fontPicker.Title = "Font";
            fontPicker.SelectedIndex = 0;

            for (int i = 0; i < fontPicker.Items.Count; i++)
            {
                if (config.Theme.font == fontPicker.Items[i])
                {
                    fontPicker.SelectedIndex = i;
                    break;
                }
            }

            var fontSizeLabel = new Label { Text = "Font Size" };
            var fontSizePicker = new Picker();
            fontSizePicker.Items.Add("Default");
            fontSizePicker.Items.Add("Micro");
            fontSizePicker.Items.Add("Small");
            fontSizePicker.Items.Add("Medium");
            fontSizePicker.Items.Add("Large");

            fontSizePicker.SetBinding(Entry.TextProperty, "Font Size");
            fontSizePicker.SelectedIndex = 0;

            string sizeToFind = config.sizeToName[config.Theme.fontSize];
            var sizeArray = config.nameToSize.ToArray();

            for (int i = 0; i < fontSizePicker.Items.Count; i++)
            {
                if (sizeToFind == fontSizePicker.Items[i])
                {
                    fontSizePicker.SelectedIndex = i;
                    break;
                }
            }

            var fontColourLabel = new Label{ Text = "Font Colour" };
            var fontColourPicker = new Picker();

            var colourArray = config.nameToColour.ToArray();
            for (int i = 0; i < colourArray.Length; i++)
            {
                fontColourPicker.Items.Add(colourArray[i].Key);
            }
            fontColourPicker.SetBinding(Entry.TextProperty, "Font Colour");
            fontColourPicker.Title = "Font Colour";
            fontColourPicker.SelectedIndex = 0;

            string colourToFind = config.colourToName[config.Theme.fontColour];

            for (int i = 0; i < colourArray.Length; i++)
            {
                if (colourToFind == colourArray[i].Key)
                {
                    fontColourPicker.SelectedIndex = i;
                    break;
                }
            }

            BoxView fontColourBoxView = new BoxView
            {
                WidthRequest = 20,
                HeightRequest = 20,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.CenterAndExpand
            };

            fontColourBoxView.Color = config.nameToColour[colourToFind];

            fontColourPicker.SelectedIndexChanged += (sender, args) =>
            {
                if (fontColourPicker.SelectedIndex == -1)
                {
                    fontColourBoxView.Color = Color.Default;
                }
                else
                {
                    string colourName = fontColourPicker.Items[fontColourPicker.SelectedIndex];
                    fontColourBoxView.Color = config.nameToColour[colourName];
                }
            };

            var backgroundColourLabel = new Label { Text = "Background Colour" };
            var backgroundColourPicker = new Picker();

            for (int i = 0; i < colourArray.Length; i++)
            {
                backgroundColourPicker.Items.Add(colourArray[i].Key);
            }
            backgroundColourPicker.SetBinding(Entry.TextProperty, "Background Colour");
            backgroundColourPicker.Title = "Background Colour";
            backgroundColourPicker.SelectedIndex = 0;

            colourToFind = config.colourToName[config.Theme.backgroundColour];

            for (int i = 0; i < colourArray.Length; i++)
            {
                if (colourToFind == colourArray[i].Key)
                {
                    backgroundColourPicker.SelectedIndex = i;
                    break;
                }    
            }

            BoxView backgroundColourBoxView = new BoxView
            {
                WidthRequest = 20,
                HeightRequest = 20,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.CenterAndExpand
            };

            backgroundColourBoxView.Color = config.nameToColour[colourToFind];

            backgroundColourPicker.SelectedIndexChanged += (sender, args) =>
            {
                if (backgroundColourPicker.SelectedIndex == -1)
                {
                    backgroundColourBoxView.Color = Color.Default;
                }
                else
                {
                    string colorName = backgroundColourPicker.Items[backgroundColourPicker.SelectedIndex];
                    backgroundColourBoxView.Color = config.nameToColour[colorName];
                }
            };

            var SaveButton = new Button {
				Text = "Save Configuration"
			};
			SaveButton.Clicked += (sender, e) => {
                ThemeStruct themeStruct = config.Theme;

                if (backgroundColourPicker.Items[backgroundColourPicker.SelectedIndex] 
                    == fontColourPicker.Items[fontColourPicker.SelectedIndex])
                {
                    DisplayAlert("Error", "You cannot select the same background and foreground colour. Please correct your selection and try again.", "Ok");
                }
                else
                {
                    themeStruct.defaultNotificationSound = ringTonePicker.Items[ringTonePicker.SelectedIndex];
                    themeStruct.backgroundColour = config.nameToColour[backgroundColourPicker.Items[backgroundColourPicker.SelectedIndex]];
                    themeStruct.font = fontPicker.Items[fontPicker.SelectedIndex];
                    themeStruct.fontColour = config.nameToColour[fontColourPicker.Items[fontColourPicker.SelectedIndex]];
                    themeStruct.fontSize = config.nameToSize[fontSizePicker.Items[fontSizePicker.SelectedIndex]];
                    config.Theme = themeStruct;
                }                

                config.Write(core.SCHEDULEAPP_CONFIG_FILE);
                Navigation.PopToRootAsync();
			};

            ScrollView scrollView = new ScrollView
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                Content = new StackLayout
                {
                    VerticalOptions = LayoutOptions.CenterAndExpand,
                    Padding = new Thickness(20),
                    Children = {
                        defaultNotificationLabel,
                        ringTonePicker,
                        fontLabel,
                        fontPicker,
                        fontSizeLabel,
                        fontSizePicker,
                        fontColourLabel,
                        fontColourPicker,
                        fontColourBoxView,
                        backgroundColourLabel,
                        backgroundColourPicker,
                        backgroundColourBoxView,
                        SaveButton
                    }
                }
            };

            this.Content = scrollView;
        }
    }
}

