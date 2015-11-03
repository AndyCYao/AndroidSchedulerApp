using System;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Media;
using ScheduleApp.Droid;
using ScheduleApp;

[assembly: Xamarin.Forms.Dependency(typeof(LocalRingTones))]

//This is the Android part of the GetRingtones
namespace ScheduleApp.Droid
{
    public class LocalRingTones : RingTones
    {
        public List<String> GetRingTones() {
            List<String> Results = new List<String>();
            //try to show a ringTone picker. per this article
            //http://stackoverflow.com/questions/18732193/how-i-get-the-default-ringtone-list-in-android-on-programmatically

            Android.Media.RingtoneManager RingToneMgm = new Android.Media.RingtoneManager(Application.Context);
            //RingToneMgm.SetType(Android.Media.);
            Android.Database.ICursor cursor = RingToneMgm.Cursor;
            while (cursor.MoveToNext())
            {
                String title = cursor.GetString(0);
                Console.WriteLine(title);
            }
           
            return Results;
        }
    }
}

