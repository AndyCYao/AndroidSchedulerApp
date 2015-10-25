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
using ScheduleApp.Droid;
using ScheduleApp;

[assembly: Xamarin.Forms.Dependency(typeof(LocalRingTones))]

namespace ScheduleApp.Droid
{
    public class LocalRingTones : RingTones
    {
        public List<String> GetRingTones() {
            List<String> Results = new List<String>();
            return Results;
        }
    }
}

