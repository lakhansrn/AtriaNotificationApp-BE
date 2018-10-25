using AtriaNotificationApp.DAL.Entities;
using AtriaNotificationApp.DAL.Interfaces;
using MongoDB.Driver;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtriaNotificationApp.DAL.Repositories
{
    public class EventAggregateRepository : IEventAggregateRepository
    {
        public async Task<Event> AddEvent(Event @event)
        {
            @event.InitId();
            @event.Announcements.ForEach(x=>{x.InitId();
            x.Content.ForEach(y => y.InitId());
            });

            DocumentDBRepository<Event> eventRepo = new DocumentDBRepository<Event>();
            var result =  await eventRepo.CreateItemAsync(@event);
            return result;
        }

        public async Task<IEnumerable<Event>> AddEvents(IEnumerable<Event> events)
        {
            foreach (var @event in events)
            {
                @event.InitId();
                @event.Announcements
                .ForEach(x=>{x.InitId();
                             x.Content.ForEach(y => y.InitId());
                            });
            }

            DocumentDBRepository<Event> eventRepo = new DocumentDBRepository<Event>();
            var result =  await eventRepo.CreateItemsAsync(events);
            return result;
        }

        public async Task<IEnumerable<EventAggregateRoot>> GetAllEventRoots()
        {
            DocumentDBRepository<Event> eventRepo = new DocumentDBRepository<Event>();
            ICollection<EventAggregateRoot> roots = new List<EventAggregateRoot>();
            try
            {
                var events = await eventRepo.GetItemsAsync();
                foreach (var item in events)
                {
                    roots.Add(new EventAggregateRoot(item));
                }
                return roots;
            }
            catch (Exception m)
            {
                List<EventAggregateRoot> noRootFound = new List<EventAggregateRoot>();
                Console.WriteLine(m.Message);
                return noRootFound;
            }
        }

        public async Task<EventAggregateRoot> GetEventsByAnnouncmentID(Guid guid)
        {
            DocumentDBRepository<Event> eventRepo = new DocumentDBRepository<Event>();
            ICollection<EventAggregateRoot> roots = new List<EventAggregateRoot>();
            try
            {
                var events = await eventRepo.GetItemsAsync(x => x.Announcements.Any(y => y.Id == guid));
                return new EventAggregateRoot(events.FirstOrDefault());
            }
            catch (Exception m)
            {
                Console.WriteLine(m.Message);
                return null;
            }            
        }

        public async Task<Event> AddContent(Guid event_guid, Guid announcement_guid, Content content)
        {
            DocumentDBRepository<Event> eventRepo = new DocumentDBRepository<Event>();
            ICollection<EventAggregateRoot> roots = new List<EventAggregateRoot>();
            try
            {
                var event1 = await eventRepo.GetItemAsync(event_guid); 
                
                List<Announcement> updated_announcement = new List<Announcement>();

                foreach (var announcement in event1.Announcements)
                {
                    if (announcement.Id == announcement_guid)
                    {                       
                        List<Content> updated_content = announcement.Content.Append(new Content() { Description = content.Description, Id = Guid.NewGuid(), Image = content.Image, IsActive = content.IsActive, IsApproved = content.IsApproved, Posted = DateTime.Now, PostedBy = content.PostedBy, Title = content.Title }).ToList();    
                        updated_announcement.Add(new Announcement() { Content = updated_content, Description = announcement.Description, Id = announcement.Id, Img = announcement.Img, PostedDate = announcement.PostedDate, Title = announcement.Title });
                    }
                    else
                    {
                        updated_announcement.Add(new Announcement() { Content = announcement.Content, Description = announcement.Description, Id = announcement.Id, Img = announcement.Img, PostedDate = announcement.PostedDate, Title = announcement.Title });
                    }
                }

                event1.Announcements = updated_announcement;

                var res = eventRepo.UpdateItemAsync(event_guid, event1);

                return await res;
            }
            catch (Exception m)
            {
                Console.WriteLine(m.Message);
                return null;
            }
        }
    }
}