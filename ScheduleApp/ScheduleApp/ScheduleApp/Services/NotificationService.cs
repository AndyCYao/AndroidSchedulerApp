using System;

namespace ScheduleApp
{
    public interface NotificationService
    {
        void Notify(string title, string description, int ID);
    }
}
