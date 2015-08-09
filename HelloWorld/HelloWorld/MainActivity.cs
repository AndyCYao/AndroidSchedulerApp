using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;



namespace HelloWorld
{
    [Activity(Label = "HelloWorld", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        int count = 1;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it
            Button button = FindViewById<Button>(Resource.Id.myButton);

            button.Click += delegate
            {
                Notifications not = new Notifications(this);
                //button.Text = string.Format ("{0} clicks!", count++);
                if (count == 1)
                {
                    button.Text = string.Format ("Click me again for notification!");
                }
                else
                {
                    button.SetBackgroundColor(Android.Graphics.Color.Green);
                    button.Text = string.Format("A notification appears!");

                    not.createStandardNotification("This is a title", "This is my text!");

                    
                }
                count++;
            };
        }
    }
}


