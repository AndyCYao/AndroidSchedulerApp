using System;
using System.IO;
using System.Xml.Serialization;
using PCLStorage;

namespace ScheduleApp
{
    public class Task
    {
        // static int numOfTasks = 0; the scheduler will handle assigning primary keys, not the Tasks' role now. 
        private int m_id;
        private string m_task_name;
        private string m_task_notes;
		private bool m_done;

		private DateTime m_reminder_end_date;
		private string m_ringtone_name;

		//Aug 8th - ID will now be assigned by the Scheduler.
		public Task(string fTaskName, string fTaskNotes, int fID)
		{
			m_task_name = fTaskName.Substring (0, 40);
			m_task_notes = fTaskNotes;
			m_id = fID;
		}

		//Declare a Done boolean property for each task
		public bool Done
		{
			get{  return m_done; }
			set{ 
				m_done = value; 
				}
		}

		//Both Task Name and Task Notes needs to be able to get modified
		//directly, not just from at the time of creating the object. 
		//therefore, i need to create a set for them two as well. 
        public string TaskName
        {
            get { return m_task_name; }
			set{ 
				m_task_name = value; 
			}
        }
        public string TaskNotes
        {
            get { return m_task_notes; }
			set{ 
				m_task_notes= value; 
			}
        }
		//Notification Details below..
		public DateTime ReminderEndDate
		{
			get { return m_reminder_end_date; }
			set {
				m_reminder_end_date = value;
			}
		}
		public string RingTone
		{
			get { return m_ringtone_name; }
			set {
				m_ringtone_name = value;
			}
		}

        public async void CreateSerializer(XmlSerializer ser)
        {
            IFile stream = await FileSystem.Current.LocalStorage.CreateFileAsync(TaskName, CreationCollisionOption.OpenIfExists);
            ser = new XmlSerializer(typeof(Task));
            ser.Serialize(await stream.OpenAsync(FileAccess.ReadAndWrite), this);               
        }

        public int TaskID
        {
            get { return m_id; }
        }
    }
}

