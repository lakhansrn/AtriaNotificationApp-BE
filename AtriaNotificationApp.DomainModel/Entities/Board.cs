using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace AtriaNotificationApp.DAL.Entities
{
    public class Board : EntityBase
    {
        public Board()
        {
            Announcements = new List<Announcement>();
        }


        public List<Announcement> Announcements { get; set; }

        public string BoardName { get; set; }

        public string BoardBanner { get; set; }

        public string Description { get; set; }

        public bool ShowAsBanner { get; set; }

        public DateTime? DateSchedule { get; set; }

        // private IEnumerator GetEnumerator()
        // {
        //     List<Board> events = new List<Board>();
        //     foreach (var item in events)
        //     {
        //         yield return item;
        //     }
        // }

        public bool IsFixed { get; set; }

        public int Order { get; set; }
    }
}
