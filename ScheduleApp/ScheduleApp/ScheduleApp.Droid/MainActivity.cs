using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Util;
using Android.Media;
using Android.Content;
using ScheduleApp;

namespace ScheduleApp.Droid
{
    [Activity(Label = "ScheduleApp", Icon = "@drawable/icon", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsApplicationActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);
            LoadApplication(new App());
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent intent)
        {
            base.OnActivityResult(requestCode, resultCode, intent);
            if (resultCode == Result.Ok)
            {
                    switch (requestCode)
                {
                    case 0:
                        //do stuff in the app config to set the default code.
                        Android.Net.Uri ring = (Android.Net.Uri)intent.GetParcelableExtra(RingtoneManager.ExtraRingtonePickedUri);
                    
                        //validiating the ring.path through checking the cursor.
                        
                        Android.Media.RingtoneManager RingToneMgm = new Android.Media.RingtoneManager(Application.Context);
                        Android.Database.ICursor cursor = RingToneMgm.Cursor;
                        while (cursor.MoveToNext())
                        {
                            //here i have to check the ring.path;
                            //http://stackoverflow.com/questions/7645951/how-to-check-if-resource-pointed-by-uri-is-available
                            var Result = cursor.GetString(RingToneMgm.ID_COLUMN_INDEX);
                        }
                        //Android.Net.Uri TestRing = Android.Net.Uri.Parse(Result);

                        Core core = Core.GetCore();
                        AppConfig config = core.GetConfig();
                        ThemeStruct ConfigStruct = config.Theme;
                        //ConfigStruct.defaultNotificationSound = Result;

                        config.Theme = ConfigStruct;


                        break;


                    default:
                        break;
                }
            }

        }





    }
}

