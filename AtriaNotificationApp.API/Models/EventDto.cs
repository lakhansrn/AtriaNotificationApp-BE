using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AtriaNotificationApp.API.Models
{
    public class EventDto : DateTrackDtoBase
    {
        public EventDto()
        {
            Announcements = new List<AnnouncementDto>();
        }

        public Guid Id { get ; set ; }

        public List<AnnouncementDto> Announcements { get; set; }

        [JsonProperty("event_name")]
        public string EventName { get; set; }

        [JsonProperty("event_banner")]
        public string EventBanner { get; set; }

        public string Description { get; set; }

        public bool ShowAsBanner { get; set; }

        public DateTime? DateSchedule { get; set; }

    }
}
