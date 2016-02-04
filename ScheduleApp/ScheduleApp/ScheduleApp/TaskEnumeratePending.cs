using ScheduleApp.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ScheduleApp
{
    public class TaskEnumeratePending
    {
        public async Task RunEnumeration(CancellationToken token)
        {
            await Task.Run(() =>
            {
                TimeSpan minute = new TimeSpan(0, 1, 0);

                while (true)
                {
                    Int64 currentTimeTicks = DateTime.Now.Ticks;
                    token.ThrowIfCancellationRequested();

                    var taskList = getTasksToNotifyAndUpdate();

                    var message = new PendingTaskMessage
                    {
                        tasks = taskList
                    };

                    Device.BeginInvokeOnMainThread(() =>
                    {
                        MessagingCenter.Send<PendingTaskMessage>(message, "PendingTaskMessage");
                    });

                    while ((DateTime.Now.Ticks - currentTimeTicks) < minute.Ticks)
                    {
                        Task.Delay(1000);
                    }                    
                }                 
            }, token);
        }

        public List<AppTask> getTasksToNotifyAndUpdate()
        {
            List<AppTask> activeTasks = ScheduleApp.Core.GetCore().GetScheduler().GetTasks(false);
            List<AppTask> reminderList = new List<AppTask>();

            
            for (int i = 0; i < activeTasks.Count; i++)
            {
                if (DateTime.Now <= activeTasks[i].ReminderEnd && DateTime.Now <= activeTasks[i].NextReminder.AddMinutes(1))
                {
                    TimeSpan multiple;

                    reminderList.Add(activeTasks[i]);

                    switch (activeTasks[i].FrequencyUnit)
                    {
                        case "Minutes": multiple = new TimeSpan(0, activeTasks[i].Frequency, 0); break;
                        case "Hours": multiple = new TimeSpan(activeTasks[i].Frequency, 0, 0); break;
                        case "Days": multiple = new TimeSpan(activeTasks[i].Frequency, 0, 0, 0); break;
                        case "Weeks": multiple = new TimeSpan(activeTasks[i].Frequency * 7, 0, 0, 0); break;
                        case "Months": break;
                        case "Years": multiple = new TimeSpan(activeTasks[i].Frequency * 365, 0, 0); break;
                        default: /* DO NOTHING */ break;
                    }
                    
                    if (activeTasks[i].NextReminder.Add(multiple) > activeTasks[i].ReminderEnd)
                    {
                        activeTasks[i].Done = true;
                    }
                    else
                    {
                        activeTasks[i].NextReminder = activeTasks[i].NextReminder.Add(multiple);
                    }
                }
            }

            return activeTasks;
        }
    }
}
