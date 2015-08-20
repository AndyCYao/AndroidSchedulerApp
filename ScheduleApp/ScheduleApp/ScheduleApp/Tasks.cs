using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using PCLStorage;

namespace ScheduleApp
{
    //needs to be declared in the namespace, but not in the Task class itself
    //This will be used to simplify the data needed in a task upon creating it. 
    public struct TaskInfo
	{
		public int TaskID;
		public string TaskName;
		public string TaskNotes;
		public bool Done;
		public DateTime ReminderEndDate;
		public string RingToneName;                

	}

	public class Task
    {
        // static int numOfTasks = 0; the scheduler will handle assigning primary keys, not the Tasks' role now. 
        private int m_id;
        private string m_task_name;
        private string m_task_notes;
        private bool m_done;

        private DateTime m_reminder_end_date;
        private string m_ringtone_name;

        //Aug 8th - ID will now be assigned by the Scheduler. Overload 1
        public Task(string fTaskName, string fTaskNotes, int fID)
        {
            m_task_name = fTaskName.Substring (0, 40);
            m_task_notes = fTaskNotes;
            m_id = fID;
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
            m_reminder_end_date = f.ReminderEndDate;
            m_ringtone_name = f.RingToneName;
        }

        //********************
        //Overload 3, this is for the XML serializer. it needs a private parameterless constructor
        private Task() { }

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
        /*
        public XmlWriter WriteXML()
        {
            StringWriter stringWriter = new StringWriter();

            using (XmlWriter result = XmlWriter.Create(stringWriter))
            {
                result.WriteStartDocument();
                result.WriteStartElement("TestingXMLoutput");
                result.WriteElementString("TaskID", this.TaskID.ToString());
                result.WriteElementString("TaskName", this.TaskName);
                result.WriteElementString("TaskNotes", this.TaskNotes);
                result.WriteElementString("ReminderEndDate", this.ReminderEndDate.ToString());
                result.WriteElementString("RingTone", this.RingTone);
                result.WriteElementString("Done", this.Done.ToString());
                result.WriteEndElement();
                result.WriteEndDocument();
                return result.ToString();
            }
            //return result;
        }
        */
        
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



        public int TaskID
        {
            get { return m_id; }
        }
    }
}

