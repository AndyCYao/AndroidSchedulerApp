using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks;
using System.Threading;
using ScheduleApp.Messages;

namespace ScheduleApp
{
    public class PendingTaskList
    {
        public async AppTask RunPendingTaskListUpdate(CancellationToken token)
        {
            await AppTask.Run(async () =>
            {
                token.ThrowIfCancellationRequested();

                await AppTask.Delay(1000);

                var pendingTasks = new PendingTaskMessage
                {
                    tasks = ScheduleApp.Core.GetCore().GetScheduler().GetTasks(false);

                    
                };

                Device.BeginInvokeOnMainThread(() =>
                {
                    MessagingCenter.Send<List>(pendingTasks, "TickedMessage");
                });
            }, token);
        }
    }
}
