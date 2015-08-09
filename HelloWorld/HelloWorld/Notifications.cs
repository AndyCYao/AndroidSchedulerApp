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
    class Notifications
    {
        const int notificationID = 0;
        Notification notification;
        NotificationManager notificationManager;
        Notification.Builder builder;
        Context context;

        public Notifications(Context context)
        {
            this.context = context;
        }

        public void createStandardNotification(string title, string content)
        {

            builder = new Notification.Builder(context)
                        .SetContentTitle(title)
                        .SetContentText(content)
                        .SetSmallIcon(Resource.Drawable.Icon);

            //var uiIntent = new Intent(context, typeof(MainActivity));
            //notification.Flags = NotificationFlags.AutoCancel;

            notification = builder.Build();

            notificationManager = context.GetSystemService(Context.NotificationService) as NotificationManager;

            notificationManager.Notify(notificationID, notification);
        }
        
        /* Doesn't work but code works if in same method.
        public void updateStandardNotification(string title, string context)
        {
            builder.SetContentTitle(title);
            builder.SetContentText(context);

            notification = builder.Build();
            notificationManager.Notify(notificationID, notification);
        }
        */
    }
}