using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using Xamarin.Forms;


//AY 4-Sept-15.
//Reorganize the pages under the "View" folder,
//because navigation between those pages have to go through the INavigate interface
//per reference here https://developer.xamarin.com/guides/cross-platform/xamarin-forms/getting-started/introduction-to-xamarin-forms/
namespace ScheduleApp
{
    public class App : Application
    {
        public App()
        {
			
			MainPage = new NavigationPage (new Main ());
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
