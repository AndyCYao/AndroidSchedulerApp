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
        Dictionary<string, Color> nameToColor = new Dictionary<string, Color>
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

        public pageAppConfig ()
		{
            Core core = Core.GetCore();
            AppConfig config = core.GetConfig();
            
            Title = "Application Configuration";

            var defaultNotificationLabel = new Label { Text = "Default Notification Sound" };
            var defaultNotificationLabelDesc = new Label { Text = "Represents the default ring tone to assign for new tasks." };
			var ringTonePicker = new Picker{ };
            //This part we can create a dictionary object that contains all the
            //ringtones, then load it into this picker, until then we just include
            //these three. 
            ringTonePicker.Items.Add ("Crazy Frog");
            ringTonePicker.Items.Add ("Minions");
            ringTonePicker.Items.Add ("Flight of the Valkryie");
            ringTonePicker.SetBinding(Entry.TextProperty, "Default Notification Sound");
            ringTonePicker.Title = "Default Notification Sound";
            ringTonePicker.SelectedIndex = 0;

            var fontLabel = new Label { Text = "Font" };
            var fontPicker = new Picker();

            fontPicker.Items.Add("Arial");
            fontPicker.Items.Add("Courier");
            fontPicker.Items.Add("Helvetica");
            fontPicker.Items.Add("Times New Roman");
            fontPicker.SetBinding (Entry.TextProperty, "Font");
            fontPicker.Title = "Font";
            fontPicker.SelectedIndex = 0;

			var fontSizeLabel = new Label { Text = "Font Size" };
            var fontSizeEntry = new Entry();
            fontSizeEntry.SetBinding(Entry.TextProperty, "Font Size");
            
			var fontColourLabel = new Label{ Text = "Font Colour" };
            var fontColourPicker = new Picker();

            var colourArray = nameToColor.ToArray();
            for (int i = 0; i < colourArray.Length; i++)
            {
                fontColourPicker.Items.Add(colourArray[i].Key);
            }
            fontColourPicker.SetBinding(Entry.TextProperty, "Font Colour");
            fontColourPicker.Title = "Font Colour";
            fontColourPicker.SelectedIndex = 0;

            BoxView fontColourBoxView = new BoxView
            {
                WidthRequest = 150,
                HeightRequest = 150,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.CenterAndExpand
            };

            fontColourPicker.SelectedIndexChanged += (sender, args) =>
            {
                if (fontColourPicker.SelectedIndex == -1)
                {
                    fontColourBoxView.Color = Color.Default;
                }
                else
                {
                    string colorName = fontColourPicker.Items[fontColourPicker.SelectedIndex];
                    fontColourBoxView.Color = nameToColor[colorName];
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

            BoxView backgroundColourBoxView = new BoxView
            {
                WidthRequest = 150,
                HeightRequest = 150,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.CenterAndExpand
            };

            backgroundColourPicker.SelectedIndexChanged += (sender, args) =>
            {
                if (backgroundColourPicker.SelectedIndex == -1)
                {
                    backgroundColourBoxView.Color = Color.Default;
                }
                else
                {
                    string colorName = backgroundColourPicker.Items[backgroundColourPicker.SelectedIndex];
                    backgroundColourBoxView.Color = nameToColor[colorName];
                }
            };

            var SaveButton = new Button {
				Text = "Save Configuration"
					//Font = Font.SystemFontOfSize (NamedSize.Large),
					//BorderWidth = 1,
					//HorizontalOptions = LayoutOptions.Center,
					//VerticalOptions = LayoutOptions.CenterAndExpand
			};
			SaveButton.Clicked += (sender, e) => {
                var themeStruct = config.Theme;

                themeStruct.defaultNotificationSound = ringTonePicker.Items[ringTonePicker.SelectedIndex];
                themeStruct.backgroundColour = Convert.ToInt32(nameToColor[backgroundColourPicker.Items[backgroundColourPicker.SelectedIndex]]);
                themeStruct.font = fontPicker.Items[fontPicker.SelectedIndex];
                themeStruct.fontColour = Convert.ToInt32(nameToColor[fontColourPicker.Items[fontColourPicker.SelectedIndex]]);
                themeStruct.fontSize = Convert.ToInt32(fontSizeEntry.Text);
			};

            var scrollView = new ScrollView
            {
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
                        fontSizeEntry,
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
        }
    }
}

