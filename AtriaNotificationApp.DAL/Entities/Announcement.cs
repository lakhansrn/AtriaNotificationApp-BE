using System;
using System.Collections.Generic;
using System.Text;

namespace AtriaNotificationApp.DAL.Entities
{
    public class Announcement
    {
        public string Title { get; set; }

        public string Img { get; set; }

        public string Description { get; set; }

        public DateTime PostedDate { get; set; }
    }
}
