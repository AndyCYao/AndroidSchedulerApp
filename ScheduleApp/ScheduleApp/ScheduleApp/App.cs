using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using Xamarin.Forms;

namespace ScheduleApp
{
    public class App : Application
    {
        public App()
        {
            // The root page of your application
            MainPage = new ContentPage
            {
                Content = new StackLayout
                {
                    VerticalOptions = LayoutOptions.Center,
                    Children = {
                        new Label {
                            XAlign = TextAlignment.Center,
                            Text = "Schedule App 2015"
                        },
                        new Button{
                            Text = "Add Task",
                            Font = Font.SystemFontOfSize(NamedSize.Large),
                            BorderWidth = 1,
                            HorizontalOptions = LayoutOptions.Center,
                            VerticalOptions = LayoutOptions.CenterAndExpand
                        }
                    }
                }
            };
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
