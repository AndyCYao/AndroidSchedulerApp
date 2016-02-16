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

using Android.Net;
using ScheduleApp.Droid;
using Android.Media;
using Xamarin.Forms;
using Android.Content.PM;
using Android.Util;
using System.Diagnostics;


[assembly: Xamarin.Forms.Dependency(typeof(LocalRingTones))]

//This is the Android part of the GetRingtones
namespace ScheduleApp.Droid
{
    public class LocalRingTones : iRingTones
    {
        //need an empty parameterless constructor so dependency service can create new instances. 
        public LocalRingTones() { }

        public void GetRingTones1()
        {
            Core core = Core.GetCore();
            AppConfig config = core.GetConfig();
            Android.Net.Uri rURI = Android.Net.Uri.Parse(config.Theme.defaultNotificationSound);

            Intent intent = new Intent(RingtoneManager.ActionRingtonePicker);
            intent.PutExtra(RingtoneManager.ExtraRingtoneTitle, "Notification Ringtone");
            intent.PutExtra(RingtoneManager.ExtraRingtoneShowSilent, true);
            //intent.PutExtra(RingtoneManager.ExtraRingtoneShowDefault, true);
            intent.PutExtra(RingtoneManager.ExtraRingtoneType, "TYPE_ALL");
            //Need condition to see if default Noti. actually exists, else dont send anything
            //look below. for possible solution. 
            //http://stackoverflow.com/questions/7645951/how-to-check-if-resource-pointed-by-uri-is-available

            //check existence, then set to EXTRA_RINGTONE_DEFAULT_URI if not found
            //try
            //{
            //    openInputStream
            //}
            //catch (System.Exception ex)
            //{
            //    rURI = Android.Net.Uri.Parse(RingtoneManager.ExtraRingtoneDefaultUri);
            //}

            intent.PutExtra(RingtoneManager.ExtraRingtoneExistingUri, rURI);

            ((Activity)Forms.Context).StartActivityForResult(intent, 0);
            
        }

       
    }


}

