using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace HelloWorld
{
    public class MyApp : Android.App.Application
    {

        private static MyApp instance;

        public MyApp() {
            instance = this;
        }

        public static Context getContext() {
            return instance;
        }

    }
}