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
    //TODO- From Lakhan : Improve this class, lot of duplicate and unnecessary code
    public class EventAggregateRepository : IEventAggregateRepository
    {
        public async Task<Event> AddEvent(Event @event)
        {
            @event.InitId();
            @event.DateCreatedOn = DateTime.UtcNow;
            @event.DateModifiedOn = DateTime.UtcNow;

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
                @event.DateCreatedOn = DateTime.UtcNow;
                @event.DateModifiedOn = DateTime.UtcNow;

                @event.Announcements
                .ForEach(x=>{x.InitId();
                             x.Content.ForEach(y => y.InitId());
                            });
            }

            DocumentDBRepository<Event> eventRepo = new DocumentDBRepository<Event>();
            var result =  await eventRepo.CreateItemsAsync(events);
            return result;
        }

        public async Task<Event> AddAnnouncement(Guid eventid, Announcement announcement)
        {
            DocumentDBRepository<Event> eventRepo = new DocumentDBRepository<Event>();

            Event event1 = await eventRepo.GetItemAsync(eventid);
            announcement.InitId();
            announcement.DateCreatedOn = DateTime.UtcNow;
            announcement.DateModifiedOn = DateTime.UtcNow;

            event1.Announcements.Add(announcement);

            var result = await eventRepo.UpdateItemAsync(eventid, event1);
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

        public async Task<Event> UpdateEvent(Event @event)
        {
            DocumentDBRepository<Event> eventRepo = new DocumentDBRepository<Event>();

                Event dbEvent = await eventRepo.GetItemAsync(@event.Id);


                dbEvent.EventName = @event.EventName;
                dbEvent.EventBanner = @event.EventBanner;
                dbEvent.Description = @event.Description;
                dbEvent.ShowAsBanner = @event.ShowAsBanner;
                dbEvent.DateModifiedOn = DateTime.UtcNow;

                Event updatedEvent = await eventRepo.UpdateItemAsync(dbEvent.Id, dbEvent);

                return updatedEvent;
        }

        public async Task<Event> UpdateAnnouncement(Guid eventid, Announcement announcement)
        {
            DocumentDBRepository<Event> eventRepo = new DocumentDBRepository<Event>();
            IEnumerable<Event> events = new List<Event>();

            events = await eventRepo.GetItemsAsync(x => x.Id == eventid);

            Event toBeUpdatedEvent = events.FirstOrDefault(x => x.Id == eventid);

            toBeUpdatedEvent.DateModifiedOn = DateTime.UtcNow;

            Announcement tobeUpdatedAnnouncement = toBeUpdatedEvent.Announcements.FirstOrDefault(x => x.Id == announcement.Id);
            tobeUpdatedAnnouncement.Title = announcement.Title;
            tobeUpdatedAnnouncement.PostedDate = DateTime.Now;
            tobeUpdatedAnnouncement.Img = announcement.Img;
            tobeUpdatedAnnouncement.Description = announcement.Description;
            tobeUpdatedAnnouncement.DateModifiedOn = DateTime.UtcNow;

            toBeUpdatedEvent.Announcements.FirstOrDefault(x => x.Id == announcement.Id).Equals(tobeUpdatedAnnouncement);
            Event updatedEvent = await eventRepo.UpdateItemAsync(toBeUpdatedEvent.Id, toBeUpdatedEvent);

            return updatedEvent;
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
                throw;
            }            
        }

        //TODO: From -> Lakhan : Check if the below implementation is right or not . Is there any unused code??
        public async Task<Event> AddContent(Guid event_guid, Guid announcement_guid, Content content)
        {
            DocumentDBRepository<Event> eventRepo = new DocumentDBRepository<Event>();
            try
            {
                var dbEvents = await eventRepo.GetItemAsync(event_guid); 
                
                List<Announcement> updated_announcement = new List<Announcement>();

                foreach (var announcement in dbEvents.Announcements)
                {
                    if (announcement.Id == announcement_guid)
                    {                       
                        List<Content> updated_content = announcement.Content.Append(new Content()
                                                        { Description = content.Description, Id = Guid.NewGuid(), Image = content.Image,
                                                            IsActive = content.IsActive, IsApproved = content.IsApproved, Posted = DateTime.Now,
                                                            PostedBy = content.PostedBy, Title = content.Title,
                                                            DateCreatedOn=DateTime.UtcNow,
                                                            DateModifiedOn=DateTime.UtcNow}).ToList();    
                        updated_announcement.Add(new Announcement() { Content = updated_content,
                                                                      Description = announcement.Description, Id = announcement.Id, Img = announcement.Img,
                                                                      PostedDate = announcement.PostedDate, Title = announcement.Title,DateCreatedOn=announcement.DateCreatedOn,DateModifiedOn=DateTime.UtcNow });
                    }
                    else
                    {
                        updated_announcement.Add(new Announcement() { Content = announcement.Content, Description = announcement.Description, Id = announcement.Id,
                            Img = announcement.Img, PostedDate = announcement.PostedDate, Title = announcement.Title,
                            DateCreatedOn = announcement.DateCreatedOn,
                            DateModifiedOn = DateTime.UtcNow
                        });
                    }
                }

                dbEvents.Announcements = updated_announcement;

                var res = eventRepo.UpdateItemAsync(event_guid, dbEvents);

                return await res;
            }
            catch (Exception m)
            {
                Console.WriteLine(m.Message);
                throw;
            }
        }

        public async Task<Event> UpdateContent(Guid event_guid, Guid announcement_guid, Guid content_id, Content content)
        {
            DocumentDBRepository<Event> eventRepo = new DocumentDBRepository<Event>();
            content.DateModifiedOn = DateTime.UtcNow;

            try
            {
                var dbEvent = await eventRepo.GetItemAsync(event_guid);

                List<Announcement> updated_announcement = new List<Announcement>();

                foreach (var announcement in dbEvent.Announcements)
                {
                    if (announcement.Id == announcement_guid)
                    {
                        List<Content> updated_content = new List<Content>();
                        foreach (Content dbcontent in announcement.Content)
                        {
                            if (dbcontent.Id == content_id)
                            {
                                updated_content.Add(content);
                            }
                            else
                            {
                                updated_content.Add(dbcontent);
                            }
                        }
                        updated_announcement.Add(new Announcement() { Content = updated_content, Description = announcement.Description, Id = announcement.Id,
                            Img = announcement.Img, PostedDate = announcement.PostedDate, Title = announcement.Title,DateCreatedOn=announcement.DateCreatedOn,DateModifiedOn=DateTime.UtcNow });
                    }
                    else
                    {
                        updated_announcement.Add(new Announcement() { Content = announcement.Content,
                            Description = announcement.Description, Id = announcement.Id, Img = announcement.Img,
                            PostedDate = announcement.PostedDate, Title = announcement.Title,
                            DateCreatedOn = announcement.DateCreatedOn,
                            DateModifiedOn = DateTime.UtcNow
                        });
                    }
                }

                dbEvent.Announcements = updated_announcement;

                var res = eventRepo.UpdateItemAsync(event_guid, dbEvent);

                return await res;
            }
            catch (Exception m)
            {
                Console.WriteLine(m.Message);
                throw;
            }
        }

        public async Task DeleteEvent(Guid eventGuid)
        {
            DocumentDBRepository<Event> eventRepo = new DocumentDBRepository<Event>();

            await eventRepo.DeleteItemAsync(eventGuid);
        }
    }
}