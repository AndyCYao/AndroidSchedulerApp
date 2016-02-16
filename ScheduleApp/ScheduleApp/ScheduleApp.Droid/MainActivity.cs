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
using Xamarin.Forms;
using ScheduleApp.Messages;
using System.Collections.Generic;


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

            WireUpTaskNotification();

            HandleReceivedMessages();
        }

        void WireUpTaskNotification()
        {
            MessagingCenter.Subscribe<StartTaskNotification>(this, "StartTaskNotification", message =>
            {
                var intent = new Intent(this, typeof(NotificationService));
                StartService(intent);
            });

            MessagingCenter.Subscribe<StopTaskNotification>(this, "StopTaskNotification", message =>
            {
                var intent = new Intent(this, typeof(NotificationService));
                StopService(intent);
            });
        }

        protected override void OnDestroy()
        {
            //should check to see if service is running
            //var intent = new Intent(this, typeof(NotificationService));
            //StopService(intent);

            base.OnDestroy();
        }

        void HandleReceivedMessages()
        {
            MessagingCenter.Subscribe<PendingTaskMessage>(this, "PendingTaskMessage", message =>
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    var list = message.tasks as List<AppTask>;

                    if (list.Count == 0)
                    {
                        var notification = new LocalNotification();
                        notification.Notify("ScheduleApp", "No upcoming tasks...", RingtoneManager.ExtraRingtoneDefaultUri, 0);
                    }
                    //perhaps summarize all tasks into a single notification
                    for (int i = 0; i < list.Count; i++)
                    {
                        var task = list[i];
                        var notification = new LocalNotification();
                        notification.Notify(task.TaskName, "Get it done!", Core.GetCore().GetConfig().Theme.defaultNotificationSound, i);
                    }
                });
            });

            MessagingCenter.Subscribe<CancelledMessage>(this, "CancelledMessage", message =>
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    var notification = new LocalNotification();
                    notification.Notify("ScheduleApp", "Task reminders disabled.", RingtoneManager.ExtraRingtoneDefaultUri, 0);
                });
            });
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

                        Core core = Core.GetCore();
                        AppConfig config = core.GetConfig();
                        ThemeStruct theme = config.Theme;
                        theme.defaultNotificationSound = ring.ToString();
                        config.Theme = theme;
                        break;
                        

                    default:
                        break;
                }
            }

        }





    }
}

