using AtriaNotificationApp.BL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AtriaNotificationApp.BL.Interfaces
{
    public interface IEventProviderService
    {
        Task<IEnumerable<Event>> GetAllValidEvents();

        Task<string> AddEvent(Event @event);
    }
}
