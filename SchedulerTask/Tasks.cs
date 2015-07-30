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

        public Task(string taskName, string taskNotes)
        {
            m_task_name = taskName;
            m_task_notes = taskNotes;
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
