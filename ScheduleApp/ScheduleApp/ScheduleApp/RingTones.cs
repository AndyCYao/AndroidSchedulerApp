using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using ScheduleApp;

//Get local ringtones per operating system. 
namespace ScheduleApp
{
    public interface RingTones
    {
        List<Tuple<String, String>> GetRingTones();
    }

    public interface playRingTones {
        void playRingTones();
    }
}
