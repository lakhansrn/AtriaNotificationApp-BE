using AtriaNotificationApp.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace AtriaNotificationApp.DAL.Entities
{
    public class AnnouncementAggregateRoot
    {
        public AnnouncementAggregateRoot(Announcement announcement)
        {
            this.Announcement = announcement;
        }

        public Announcement Announcement
        {
            get;
            private set;
        }
    }
}
