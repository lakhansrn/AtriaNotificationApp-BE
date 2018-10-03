using AtriaNotificationApp.BL.Interfaces;
using AtriaNotificationApp.DAL.Entities;
using AtriaNotificationApp.DAL.Interfaces;
using AtriaNotificationApp.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtriaNotificationApp.BL.Services
{
    public class EventProviderService : IEventProviderService
    {
        private readonly IEventAggregateRepository eventRepository;

        public EventProviderService()
        {
            eventRepository = new EventAggregateRepository();
        }

        public async Task<IEnumerable<Event>> GetAllValidEvents()
        {
            var eventRoots = await eventRepository.GetAllEventRoots();
            
            return eventRoots.ToList().Select(x=>x.Event);
        }

        public async Task<string> AddEvent(Event item)
        {

            return await eventRepository.AddEvent(item);
        }
    }
}
