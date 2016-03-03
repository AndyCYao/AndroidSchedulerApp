using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using ScheduleApp;

//Get local ringtones per operating system. 
namespace ScheduleApp
{
    public interface iRingTones
    {
        void GetRingTonePicker(AppTask task = null);
    }

}
