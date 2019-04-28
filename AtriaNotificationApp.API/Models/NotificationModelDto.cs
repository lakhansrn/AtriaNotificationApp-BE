using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AtriaNotificationApp.API.Models
{
    public class NotificationModelDto
    {
        public string Title { get; set; }
        public string Message { get; set; }
        public string Url { get; set; }
    }
}
