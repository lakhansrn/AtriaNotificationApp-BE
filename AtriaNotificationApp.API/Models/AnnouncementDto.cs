using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AtriaNotificationApp.API.Models
{
    public class AnnouncementDto
    {
        public string Title { get; set; }

        public string Img { get; set; }

        public string Description { get; set; }

        [JsonProperty("posted")]
        public DateTime PostedDate { get; set; }

    }
}
