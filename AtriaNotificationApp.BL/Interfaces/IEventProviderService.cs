using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AtriaNotificationApp.DAL.Entities;

namespace AtriaNotificationApp.BL.Interfaces
{
    public interface IEventProviderService
    {
        Task<IEnumerable<Event>> GetAllValidEvents();

        Task<Event> AddEvent(Event @event);
        Task<IEnumerable<Event>> AddEvents(IEnumerable<Event> events);
    }
}
