using System;

namespace ScheduleApp
{
    public interface NotificationService
    {
        void Notify(string title, string description, string soundLocation, int ID);
    }
}
