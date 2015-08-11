using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;	// added August 10th

namespace SchedulerTask
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
		private DateTime m_start_date;

		private int m_freq_numerator;
		private int m_freq_denominator;
		//unclear what do we set 



		//Aug 8th - ID will now be assigned by the Scheduler.
		public Task(string fTaskName, string fTaskNotes, int fID)
		{
			m_task_name = fTaskName.Substring (0, 1000);
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
		//this start date has to toggle between 
		//next week, next day, etc. 
		public DateTime StartDate
		{
			get { return m_start_date; }
			set {
				m_start_date = value;
			}
		}

		//Frequencies
		public int NumOfTimes
		{
			get { return m_freq_numerator; }
			set {
				m_freq_numerator = value;
			}
		}
		public int PerDenominator
		{
			get { return m_freq_denominator; }
			set {
				m_freq_denominator = value;
			}
		}

		//Output an XML file
		//with reference in this https://support.microsoft.com/en-us/kb/815813
		//as well as https://msdn.microsoft.com/en-us/library/58a18dwa(v=vs.110).aspx
		public void OutputXML
		{
			get{
				XmlSerializer serializer = new XmlSerializer(typeof(Task));
				serializer.Serialize(Console.Out,self);
			}	
		}


        public int TaskID
        {
            get { return m_id; }
        }


    }
}
