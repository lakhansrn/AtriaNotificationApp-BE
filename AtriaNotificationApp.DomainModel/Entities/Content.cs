using System;
using System.Collections.Generic;
using System.Text;

namespace AtriaNotificationApp.DAL.Entities
{
    public class Content : EntityBase
    {
        public Content()
        {
            PostedBy = new Announcer();
        }
        public string Title { get; set; }

        public DateTime Posted { get; set; }

        public string Image { get; set; }

        public Announcer PostedBy { get; set; }

        public string Description { get; set; }

        public bool IsApproved { get; set; }

        public bool IsActive { get; set; }

    }
}
