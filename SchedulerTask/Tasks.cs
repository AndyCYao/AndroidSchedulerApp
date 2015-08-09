using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulerTask
{
    public class Task
    {
        static int numOfTasks = 0;            // should be automatically generated?
                                              // should also figure out how many existing tasks are there. 
        private int m_id;
        private string m_task_name;
        private string m_task_notes;

<<<<<<< HEAD
        public Task(string fTaskName, string fTaskNotes)
        {
			/* Maybe there isn't a need to check string length. 
			if (fTaskName.Length () > 40) 
			{
				m_task_name = fTaskName.Substring (0, 40);
			} 
			else 
			{
				m_task_name = fTaskName;
			}
			*/

			m_task_name = fTaskName.Substring (0, 40);

			m_task_notes = fTaskNotes;
=======
        public Task(string argTaskName, string argTaskNotes)
        {
            if (argTaskName.Length > 40)
            {
                m_task_name = argTaskName.Substring(0, 40);
            }
            else
            {
                m_task_name = argTaskName;
            }
            m_task_notes = argTaskNotes;
>>>>>>> 11932698512b568279aa607656c1f2628d709a41
            m_id = numOfTasks;
            numOfTasks++;
            //m_id would need to be incremented from existing log of tasks. 
        }

        public string TaskName
        {
            get { return m_task_name; }
        }
        public string TaskNotes
        {
            get { return m_task_notes; }
        }
        public int TaskID
        {
            get { return m_id; }
        }
    }
}
