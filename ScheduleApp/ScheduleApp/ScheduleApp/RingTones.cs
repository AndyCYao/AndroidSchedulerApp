using System;
using System.Collections.Generic;
//Get local ringtones per operating system. 
namespace ScheduleApp
{
    public interface RingTones
    {
        List<String> GetRingTones();
    }
}
