using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScheduleApp.Messages
{
    public class PendingTaskMessage
    {
        public List<AppTask> tasks { get; set; }
    }
}
