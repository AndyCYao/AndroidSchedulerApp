using System;
using System.Collections;
using PCLStorage;
using System.Xml;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Xml.Serialization;

namespace ScheduleApp
{
    public class Scheduler
    {
        private List<AppTask> tasks;
        private string tasksPath = "DefaultTasks.xml";

        public Scheduler(string filePath = "")
        {
            tasks = new List<AppTask>();

            if (filePath.Length > 0)
            {
                System.Threading.Tasks.Task.Run(() => Read(filePath)).Wait();
                tasksPath = filePath;
            }
        }

        public int TaskCount
        {
            get { return tasks.Count; }
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

        public List<AppTask> GetTasks(Boolean getInactive)
        {
            List<AppTask> filteredTasks = new List<AppTask>();
            var enumList = tasks.Where(s => s.Done == getInactive);

            foreach (var item in enumList)
            {
                filteredTasks.Add(item);
            }

            return filteredTasks;
        }

        public void AddTask(AppTask taskToAdd)
        {
           
            if (taskToAdd.TaskName == null)
            {
                throw new ArgumentNullException("taskToAdd.TaskName");
            }
            else if (taskToAdd.ReminderEnd == null)
            {
                throw new ArgumentNullException("taskToAdd.ReminderEndDate");
            }
            else
            {
                tasks.Add(taskToAdd);
            }

            System.Threading.Tasks.Task.Run(() => Write(tasksPath)).Wait();
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

                        System.Threading.Tasks.Task.Run(() => Write(tasksPath)).Wait();
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
        public AppTask TaskAt(int index)
        {
            if (index < 0 || index >= tasks.Count)
            {
                throw new ArgumentOutOfRangeException("Index out of range", "Task index cannot be less than zero or greater than or equal to the task count.");
            }

            return tasks.ElementAt(index);
        }

        public AppTask FindTaskById(int id)
        {
            Boolean found = false;

            for (int i = 0; i < tasks.Count && !found; i++)
            {
                if (tasks.ElementAt(i).TaskID == id)
                {
                    return tasks.ElementAt(i);
                }
            }

            return null;
        }

        //task ID's will increment continually until the end of ints
        public void AddTaskWithInfo(string name, string notes,
                    DateTime reminderBegin, DateTime reminderEnd,
                    string ringToneName, int frequency, string frequencyUnit)
        {
            TaskInfo tempTaskInfo = new TaskInfo();

            tempTaskInfo.TaskName = name;
            tempTaskInfo.TaskNotes = notes;
            tempTaskInfo.Done = false;
            tempTaskInfo.ReminderBegin = reminderBegin;
            tempTaskInfo.ReminderEnd = reminderEnd;
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

            AddTask(new AppTask(ref tempTaskInfo));
        }


        public void UpdateTaskWithInfo(int taskID, string name,
                        string notes, DateTime reminderBegin, DateTime reminderEnd, 
                        string ringToneName, int frequency, string frequencyUnit)
        {
            AppTask updateTask = FindTaskById(taskID);

            updateTask.TaskName = name;
            updateTask.TaskNotes = notes;
            updateTask.ReminderEnd = reminderEnd;
            updateTask.RingTone = ringToneName;
            updateTask.Frequency = frequency;
            updateTask.FrequencyUnit = frequencyUnit;

            System.Threading.Tasks.Task.Run(() => Write(tasksPath)).Wait();
        }

        public async void Write(string path)
        {
            IFile file = await FileSystem.Current.LocalStorage.CreateFileAsync(path, CreationCollisionOption.ReplaceExisting);
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.CloseOutput = true;
            XmlWriter writer = XmlWriter.Create(await file.OpenAsync(FileAccess.ReadAndWrite), settings);

            writer.WriteStartDocument();
            writer.WriteStartElement("Tasks");

            for (int i = 0; i < tasks.Count; i++)
            {
                tasks[i].WriteXML(writer);
            }

            writer.WriteEndElement();
            writer.WriteEndDocument();
            writer.Dispose();
        }

        public async void Read(string path)
        {
            try
            { 
                ExistenceCheckResult result = await FileSystem.Current.LocalStorage.CheckExistsAsync(path);
                                
                if (result == ExistenceCheckResult.FileExists)
                {
                    IFile file = await FileSystem.Current.LocalStorage.GetFileAsync(path);
                    XmlReader reader = XmlReader.Create(await file.OpenAsync(FileAccess.Read));

                    tasks.Clear();

                    reader.ReadToDescendant("Task");

                    while (reader != null && reader.Name == "Task")
                    { 
                        AppTask task = new AppTask();
                        task.ReadXML(reader);
                        tasks.Add(task);
                    }
                }
                else
                {
                    System.Threading.Tasks.Task.Run(() => Write(path)).Wait();
                }
            }
            catch(Exception e)
            {
                String error = e.Message;
            }

        }
    }
}
