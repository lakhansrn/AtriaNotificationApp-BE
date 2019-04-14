using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AtriaNotificationApp.API.Models
{
    public class BoardDto : DateTrackDtoBase
    {
        public BoardDto()
        {
            Announcements = new List<AnnouncementDto>();
        }

        public Guid Id { get ; set ; }

        public List<AnnouncementDto> Announcements { get; set; }

        [JsonProperty("board_name")]
        public string BoardName { get; set; }

        [JsonProperty("board_banner")]
        public string BoardBanner { get; set; }

        public string Description { get; set; }

        public bool ShowAsBanner { get; set; }

        public DateTime? DateSchedule { get; set; }

        public bool IsFixed { get; set; }

        public int Order { get; set; }

    }
}
