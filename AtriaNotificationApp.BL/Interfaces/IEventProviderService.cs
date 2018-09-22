using AtriaNotificationApp.BL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AtriaNotificationApp.BL.Interfaces
{
    public interface IEventProviderService
    {
        IEnumerable<Event> GetAllValidEvents();
    }
}
