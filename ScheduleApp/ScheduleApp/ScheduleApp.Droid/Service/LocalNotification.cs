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
using ScheduleApp;

[assembly: Xamarin.Forms.Dependency(typeof(ScheduleApp.NotificationService))]
namespace ScheduleApp.Droid
{
    public class LocalNotification : ScheduleApp.NotificationService
    {
        public void Notify(string title, string description, int id)
        {
			Notification.Builder builder = new Notification.Builder (Application.Context)
				.SetContentTitle (title)
				.SetContentText (description)
				.SetSmallIcon (Resource.Drawable.icon);

			Notification notification = builder.Build();
			NotificationManager notificationManager = Application.Context.GetSystemService (Context.NotificationService) as NotificationManager;
           
			const int notificationId = 0;
			notificationManager.Notify (notificationId, notification);
        }
    }
}