using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScheduleApp
{
    public class Scheduler
    {
        private List<Task> tasks = new List<Task>();


        public Scheduler()
        {

        }

        public int Count
        {
            get { return tasks.Count; }
        }

        public DateTime GetCurrentTime()
        {
            return DateTime.Now;
        }

        public List<int> GetTaskIDs()
        {
            List<int> tempIDList = new List<int>();
            for (int i = 0; i < tasks.Count; i++)
            {
                tempIDList.Add(tasks.ElementAt(i).TaskID);
            }

            return tempIDList;
        }

        public List<Task> GetActiveTasks()
        {
            List<Task> tempActiveTasks = new List<Task>();
            var enumList = tasks.Where(s => s.Done == true);

            foreach (var item in enumList)
            {
                tempActiveTasks.Add(item);
            }

            return tempActiveTasks;

        }

        public List<Task> GetInactiveTasks()
        {
            List<Task> tempActiveTasks = new List<Task>();
            var enumList = tasks.Where(s => s.Done == false);

            foreach (var item in enumList)
            {
                tempActiveTasks.Add(item);
            }

            return tempActiveTasks;
        }

        public void AddTask(Task taskToAdd)
        {
            if (taskToAdd.TaskID <= 0)
            {
                throw new ArgumentOutOfRangeException("taskToAdd.TaskID", "Task ID cannot be lower than or equal to 0.");
            }
            else if (taskToAdd.TaskName == null)
            {
                throw new ArgumentNullException("taskToAdd.TaskName");
            }
            else if (taskToAdd.ReminderEndDate == null)
            {
                throw new ArgumentNullException("taskToAdd.ReminderEndDate");
            }
            else
            {
                tasks.Add(taskToAdd);
            }
        }


        public void RemoveTask(int TaskID)
        {
            if (TaskID <= 0)
            {
                throw new ArgumentOutOfRangeException("taskToAdd.TaskID", "Task ID cannot be lower than or equal to 0.");
            }
            else
            {
                bool found = false;
                for (int i = 0; i < tasks.Count && !found; i++)
                {
                    if (tasks.ElementAt(i).TaskID == TaskID)
                    {
                        found = true;
                        tasks.RemoveAt(i);
                    }
                }

                if (!found)
                {
                    throw new Exception("No such TaskID found. Cannot remove task.");
                }
            }
        }

        // TaskAt function is sketchy. Use with care.
        // Will return null task if no such Task id is found or Task id <= 0.
        public Task TaskAt(int TaskID)
        {
            Task returnTask = null;

            if (TaskID <= 0)
            {
                throw new ArgumentOutOfRangeException("taskToAdd.TaskID", "Task ID cannot be lower than or equal to 0.");
            }
            else
            {
                bool found = false;
                for (int i = 0; i < tasks.Count && !found; i++)
                {
                    if (tasks.ElementAt(i).TaskID == TaskID)
                    {
                        found = true;
                        returnTask = tasks.ElementAt(i);
                    }
                }

                if (!found)
                {
                    throw new Exception("No such TaskID found. Cannot remove task.");
                }
            }

            return returnTask;
        }



        public void AddTaskWithInfo(string name, string notes, DateTime reminder, string ringToneName, int frequency, string frequencyUnit)
        {
            TaskInfo tempTaskInfo = new TaskInfo();
            tempTaskInfo.TaskName = name;
            tempTaskInfo.TaskNotes = notes;
            tempTaskInfo.Done = false;
            tempTaskInfo.ReminderEndDate = reminder;
            tempTaskInfo.RingToneName = ringToneName;
            tempTaskInfo.Frequency = frequency;
            tempTaskInfo.FrequencyUnit = frequencyUnit;

            if (tasks.Count > 0)
            {
                tempTaskInfo.TaskID = tasks[tasks.Count - 1].TaskID + 1;
            }
            else
            {
                tempTaskInfo.TaskID = 1;
            }

            AddTask(new Task(ref tempTaskInfo));
        }
    }
}
