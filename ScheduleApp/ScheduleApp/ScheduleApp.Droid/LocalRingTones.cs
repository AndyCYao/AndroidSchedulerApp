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
        //need an empty parameterless constructor so dependency service can create new instances. 
        public LocalRingTones(){}
        public void GetRingTones() {
            List<String> Results = new List<String>();
            //try to show a ringTone picker. per this article
            //http://stackoverflow.com/questions/18732193/how-i-get-the-default-ringtone-list-in-android-on-programmatically

            Android.Media.RingtoneManager RingToneMgm = new Android.Media.RingtoneManager(Application.Context);
            //RingToneMgm.SetType(RingtoneManager.);
            //RingToneMgm.SetType(Android.Media.);
            Android.Database.ICursor cursor = RingToneMgm.Cursor;
            while (cursor.MoveToNext())
            {
                //1 is the column that has the title of the ringtones. 
                String title = cursor.GetString(1);
                Console.WriteLine(title);
            }
           
            //return Results;
        }
    }
}

