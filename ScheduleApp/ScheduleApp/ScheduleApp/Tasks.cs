using System;
using System.IO;
using System.Xml;
// using System.Xml.Serialization;
using PCLStorage;

namespace ScheduleApp
{
    //needs to be declared in the namespace, but not in the Task class itself
    //This will be used to simplify the data needed in a task upon creating it. 
    //aug 24th 2015 updated frequency and unit
    public struct TaskInfo
	{
		public int TaskID;
		public string TaskName;
		public string TaskNotes;
		public bool Done;
        public DateTime ReminderBegin;
        public DateTime ReminderEnd;
		public string RingToneName;
        public int Frequency;
        public string FrequencyUnit;
	}

	public class Task
    {
        // static int numOfTasks = 0; the scheduler will handle assigning primary keys, not the Tasks' role now. 
        private int m_id;
        private string m_task_name;
        private string m_task_notes;
        private bool m_done;

        private DateTime m_reminder_begin;
        private DateTime m_reminder_end;
        private DateTime m_reminder_next;
        private string m_ringtone_name;

        //As in, once every m_freq and unit,  once every 5 hours, once every 10 minutes, etc. 
        private int m_frequency;
        private string m_frequency_unit;

        //Aug 8th - ID will now be assigned by the Scheduler. Overload 1
        public Task(string fTaskName, string fTaskNotes, int fID)
        {
            if (fTaskName.Length > 0)
            {
                m_task_name = fTaskName.Substring(0, 40);
            }
            else
            {
                m_task_name = "";
            }

            m_task_notes = fTaskNotes;
            m_id = fID;
            m_frequency_unit = "Weeks"; //should initialize all instance members
        }

        //Aug 18th Working on a constructor that takes a struct instead of individual 
        //parameters, and from this struct we update the info.
        // ********************
        // Overload 2
        public Task(ref TaskInfo f){
            m_id = f.TaskID;
            m_task_name = f.TaskName;
            m_task_notes = f.TaskNotes;
            m_done = f.Done;
            m_reminder_begin = f.ReminderBegin;
            m_reminder_end = f.ReminderEnd;
            m_ringtone_name = f.RingToneName;
            m_frequency = f.Frequency;
            m_frequency_unit = f.FrequencyUnit;

            CalculateNextReminder();
        }

        private void CalculateNextReminder()
        {
            DateTime current = DateTime.Now.ToLocalTime();

            //factor in frequency and unit

        }

        //********************
        //Overload 3, this is for the XML serializer. it needs a private parameterless constructor
        //not needed any more. 
        //private Task() { }

        public int TaskID
        {
            get { return m_id; }
        }

        public bool Done
        {
            get{  return m_done; }
            set{ 
                m_done = value; 
                }
        }

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
        
        public DateTime ReminderBegin
        {
            get { return m_reminder_begin; }
            set
            {
                m_reminder_begin = value;
            }
        }

        public DateTime ReminderEnd
        {
            get { return m_reminder_end; }
            set {
                m_reminder_end = value;
            }
        }

        public string RingTone
        {
            get { return m_ringtone_name; }
            set {
                m_ringtone_name = value;
            }
        }

        public string FrequencyUnit
        {
            get { return m_frequency_unit; }
            set {
                m_frequency_unit = value;
            }
        }

        public int Frequency
        {
            get { return m_frequency; }
            set
            {
                m_frequency = value;
            }
        }

        //Actually needs to return an XML file. -> change this method to WriteXML
        /*
        public async void CreateSerializer(XmlSerializer ser)
        	//currently takes the serializer, attempt to open 
		{
            IFile stream = await FileSystem.Current.LocalStorage.CreateFileAsync(TaskName, CreationCollisionOption.OpenIfExists);
            ser = new XmlSerializer(typeof(Task));
            ser.Serialize(await stream.OpenAsync(FileAccess.ReadAndWrite), this);               
        }
        */


        //Aug 19th 2015 - the WriteXML function will now replace the the above CreateSerializer
        //this function will output a XML file for the scheduler to use. 
        // referenced http://www.dotnetperls.com/xmlwriter

        public void WriteXML(XmlWriter writer)
        {
            writer.WriteStartElement("Task");
            writer.WriteElementString("TaskID", TaskID.ToString());
            writer.WriteElementString("TaskName", TaskName);
            writer.WriteElementString("TaskNotes", TaskNotes);
            writer.WriteElementString("ReminderBegin", ReminderBegin.ToString());
            writer.WriteElementString("ReminderEnd", ReminderEnd.ToString());
            writer.WriteElementString("RingTone", RingTone);
            writer.WriteElementString("Done", Done.ToString());
            writer.WriteElementString("Frequency", Frequency.ToString());
            writer.WriteElementString("FrequencyUnit", FrequencyUnit);
            writer.WriteEndElement();
        }        

        public void ReadXML(XmlReader reader)
        {
            reader.ReadStartElement("Task");

            m_id = reader.ReadElementContentAsInt();
            TaskName = reader.ReadElementContentAsString();
            TaskNotes = reader.ReadElementContentAsString();
            ReminderBegin = DateTime.Parse(reader.ReadElementContentAsString());
            ReminderEnd = DateTime.Parse(reader.ReadElementContentAsString());
            RingTone = reader.ReadElementContentAsString();
            Done = Boolean.Parse(reader.ReadElementContentAsString());
            Frequency = reader.ReadElementContentAsInt();
            FrequencyUnit = reader.ReadElementContentAsString();

            reader.ReadEndElement();
        }
        
        /*
        public MemoryStream WriteXML2()
        {
            System.Xml.Serialization.XmlSerializer writer =
                new System.Xml.Serialization.XmlSerializer(typeof(Task));

            //using (MemoryStream memStream = new MemoryStream(100))
            //{
            //    TextWriter tw = new StreamWriter(memStream);
            //    writer.Serialize(tw, this);
            //    return memStream;
            //}
            MemoryStream memStream = new MemoryStream(100);
            TextWriter tw = new StreamWriter(memStream);

            writer.Serialize(tw, this);
            return memStream;
        }
        */
    }
}

