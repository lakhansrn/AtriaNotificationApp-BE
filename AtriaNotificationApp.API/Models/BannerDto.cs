using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AtriaNotificationApp.API.Models
{
    public class BannerDto
    {
        [JsonProperty("img")]
        public string ImageUrl { get; set; }    

        [JsonProperty("title")]
        public string Title { get; set; }      

        [JsonProperty("id")]
        public int ID { get; set; }      
    }
}
