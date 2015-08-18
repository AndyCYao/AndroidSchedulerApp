using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScheduleApp
{
    class Scheduler
    {
        private List<Task> tasks = new List<Task>();


        public Scheduler()
        {

        }

        public DateTime GetCurrentTime()
        {
            return DateTime.Now;
        }

        public void AddTask(Task taskToAdd)
        {
            if (taskToAdd.TaskID <= 0)
            {
                throw new ArgumentOutOfRangeException("taskToAdd.TaskID", "Task ID cannot be lower than or equal to 0");
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
                throw new ArgumentOutOfRangeException("taskToAdd.TaskID", "Task ID cannot be lower than or equal to 0");
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

        // TaskAt class is sketchy. Use with care.
        // Will return null task if no such Task id is found or Task id <= 0.
        public Task TaskAt(int TaskID)
        {
            Task returnTask = null;

            if (TaskID <= 0)
            {
                throw new ArgumentOutOfRangeException("taskToAdd.TaskID", "Task ID cannot be lower than or equal to 0");
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


        

    }
}
