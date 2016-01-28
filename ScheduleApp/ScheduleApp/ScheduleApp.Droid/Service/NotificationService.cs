using System;
using ScheduleApp;

using Android.App;
using Android.Content;
using System.Threading.Tasks;
using System.Threading;
using Android.OS;
using Xamarin.Forms;
using ScheduleApp.Messages;

namespace ScheduleApp.Droid
{
    [Service]
    public class NotificationService : Service
    {
        CancellationTokenSource _cts;

        public override IBinder OnBind(Intent intent)
        {
            return null;
        }

        public override StartCommandResult OnStartCommand(Intent intent, StartCommandFlags flags, int startId)
        {
            _cts = new CancellationTokenSource();

            Task.Run(() =>
            {
                try
                {
                    var pendingTaskList = new TaskEnumeratePending();
                    pendingTaskList.RunEnumeration(_cts.Token).Wait();
                }
                catch (Android.OS.OperationCanceledException)
                {
                }
                finally
                {
                    if (_cts.IsCancellationRequested)
                    {
                        var message = new CancelledMessage();
                        Device.BeginInvokeOnMainThread(
                            () => MessagingCenter.Send(message, "CancelledMessage")
                        );
                    }
                }

            }, _cts.Token);

            return StartCommandResult.Sticky;
        }

        public override void OnDestroy()
        {
            if (_cts != null)
            {
                _cts.Token.ThrowIfCancellationRequested();

                _cts.Cancel();
            }
            base.OnDestroy();
        }
    }
}
