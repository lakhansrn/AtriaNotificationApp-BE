using System;
using System.Collections.Generic;
using System.Text;

namespace AtriaNotificationApp.DAL.Entities
{
    public class Announcement : EntityBase
    {
        public Announcement()
        {
            Content = new List<Content>();
        }

        public List<Content> Content { get; set; }

        public string Title { get; set; }

        public string Img { get; set; }

        public string Description { get; set; }

        public DateTime PostedDate { get; set; }

        public DateTime? DateSchedule { get; set; }

    }
}
