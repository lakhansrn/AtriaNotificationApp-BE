using AtriaNotificationApp.DAL.Entities;
using AtriaNotificationApp.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace AtriaNotificationApp.DAL.Repositories
{
    public class AnnouncementAggregateRepository : IAnnouncementAggregateRepository
    {
        public async Task<IEnumerable<ContentAggregateRoot>> GetContents(Guid guid)
        {
            DocumentDBRepository<Event> eventRepo = new DocumentDBRepository<Event>();
            ICollection<ContentAggregateRoot> roots = new List<ContentAggregateRoot>();
            try
            {
                var events = await eventRepo.GetItemsAsync();
                foreach (var e in events)
                {                    
                    foreach(var announcements in e.Announcements.Where(a => a.Id == guid))
                    {
                        foreach(var content in announcements.Content)
                        {
                            roots.Add(new ContentAggregateRoot(content));
                        }                        
                    }
                }
                return roots;
            }
            catch (Exception m)
            {
                List<ContentAggregateRoot> noRootFound = new List<ContentAggregateRoot>();
                Console.WriteLine(m.Message);
                return noRootFound;
            }
        }
    }
}
