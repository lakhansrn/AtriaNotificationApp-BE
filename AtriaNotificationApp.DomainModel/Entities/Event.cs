using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace AtriaNotificationApp.DAL.Entities
{
    public class Event : EntityBase
    {
        public Event()
        {
            Announcements = new List<Announcement>();
        }


        public List<Announcement> Announcements { get; set; }

        public string EventName { get; set; }

        public string EventBanner { get; set; }

        public string Description { get; set; }

        public bool ShowAsBanner { get; set; }

        public DateTime? DateSchedule { get; set; }

        // private IEnumerator GetEnumerator()
        // {
        //     List<Event> events = new List<Event>();
        //     foreach (var item in events)
        //     {
        //         yield return item;
        //     }
        // }
    }
}
