using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AtriaNotificationApp.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebPush;

namespace AtriaNotificationApp.API.Controllers
{
    [Route("api/notification")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        public static List<PushSubscription> Subscriptions { get; set; } = new List<PushSubscription>();

        [HttpPost("subscribe")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public void Subscribe([FromBody] PushSubscription sub)
        {
            Subscriptions.Add(sub);
        }

        [HttpPost("unsubscribe")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public void Unsubscribe([FromBody] PushSubscription sub)
        {
            var item = Subscriptions.FirstOrDefault(s => s.Endpoint == sub.Endpoint);
            if (item != null)
            {
                Subscriptions.Remove(item);
            }
        }

        [HttpPost("broadcast")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public void Broadcast([FromBody] NotificationModelDto message, [FromServices] VapidDetails vapidDetails)
        {
            var client = new WebPushClient();
            var serializedMessage = JsonConvert.SerializeObject(message);
            foreach (var pushSubscription in Subscriptions)
            {
                client.SendNotification(pushSubscription, serializedMessage, vapidDetails);
            }

        }
    }
}