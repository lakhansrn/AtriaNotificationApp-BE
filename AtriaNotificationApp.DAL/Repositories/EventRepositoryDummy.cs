using AtriaNotificationApp.DAL.Entities;
using AtriaNotificationApp.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace AtriaNotificationApp.DAL.Repositories
{
    public class EventRepositoryDummy : IEventAggregateRepository
    {
        public IEnumerable<EventAggregateRoot> GetAllEventRoots()
        {
            var root = new List<EventAggregateRoot>();

            root = Init(root);

            return root;
        }

        private List<EventAggregateRoot> Init(List<EventAggregateRoot> root)
        {

            var event1 = new Event()
            {
                EventName = "Dextrix",
                Description = "lorem impsi",
                ShowAsBanner = true,
                EventBanner = "https://images.pexels.com/photos/261909/pexels-photo-261909.jpeg?auto=compress&cs=tinysrgb&h=650&w=940",
                Announcements = new List<Announcement>()
               {
                   new Announcement()
                   {
                       Title ="code sprint",
                       Img="https://images.pexels.com/photos/533671/pexels-photo-533671.jpeg?auto=compress&cs=tinysrgb&h=350",
                       Description="Lorem ipsum dolor sit amet consectetur adipisicing elit. Nisi, cupiditate.",
                       PostedDate=DateTime.Parse("2017-01-16")
                   },
                   new Announcement()
                   {
                       Title ="maze builder",
                       Img="https://images.pexels.com/photos/908284/pexels-photo-908284.jpeg?auto=compress&cs=tinysrgb&h=350",
                       Description ="Lorem ipsum dolor sit amet consectetur adipisicing elit. Nisi, cupiditate.",
                       PostedDate =DateTime.Parse("2017-01-16")
                   },
                   new Announcement()
                   {
                       Title ="android workshop",
                       Img="https://images.pexels.com/photos/595804/pexels-photo-595804.jpeg?auto=compress&cs=tinysrgb&h=350",
                       Description="Lorem ipsum dolor sit amet consectetur adipisicing elit. Nisi, cupiditate.",
                       PostedDate=DateTime.Parse("2017-01-16")
                   }
               }
            };

            var root1 = new EventAggregateRoot(event1);

            var event2 = new Event()
            {
                EventName = "TechVistara",
                Description = "lorem impsi",
                ShowAsBanner = true,
                EventBanner = "https://images.pexels.com/photos/289737/pexels-photo-289737.jpeg?auto=compress&cs=tinysrgb&h=650&w=940",
                Announcements = new List<Announcement>()
               {
                   new Announcement()
                   {
                       Title ="code sprint",
                       Img="https://images.pexels.com/photos/533671/pexels-photo-533671.jpeg?auto=compress&cs=tinysrgb&h=350",
                       Description="Lorem ipsum dolor sit amet consectetur adipisicing elit. Nisi, cupiditate.",
                       PostedDate=DateTime.Parse("2017-01-16")
                   },
                   new Announcement()
                   {
                       Title ="maze builder",
                       Img="https://images.pexels.com/photos/908284/pexels-photo-908284.jpeg?auto=compress&cs=tinysrgb&h=350",
                       Description ="Lorem ipsum dolor sit amet consectetur adipisicing elit. Nisi, cupiditate.",
                       PostedDate =DateTime.Parse("2017-01-16")
                   },
                   new Announcement()
                   {
                       Title ="android workshop",
                       Img="https://images.pexels.com/photos/595804/pexels-photo-595804.jpeg?auto=compress&cs=tinysrgb&h=350",
                       Description="Lorem ipsum dolor sit amet consectetur adipisicing elit. Nisi, cupiditate.",
                       PostedDate=DateTime.Parse("2017-01-16")
                   }
               }
            };

            var root2 = new EventAggregateRoot(event2);

            var event3 = new Event()
            {
                EventName = "Go Green",
                Description = "lorem impsi",
                ShowAsBanner = false,
                EventBanner = "https://images.pexels.com/photos/239886/pexels-photo-239886.jpeg?auto=compress&cs=tinysrgb&h=650&w=940",
                Announcements = new List<Announcement>()
               {
                   new Announcement()
                   {
                       Title ="code sprint",
                       Img="https://images.pexels.com/photos/533671/pexels-photo-533671.jpeg?auto=compress&cs=tinysrgb&h=350",
                       Description="Lorem ipsum dolor sit amet consectetur adipisicing elit. Nisi, cupiditate.",
                       PostedDate=DateTime.Parse("2017-01-16")
                   },
                   new Announcement()
                   {
                       Title ="maze builder",
                       Img="https://images.pexels.com/photos/908284/pexels-photo-908284.jpeg?auto=compress&cs=tinysrgb&h=350",
                       Description ="Lorem ipsum dolor sit amet consectetur adipisicing elit. Nisi, cupiditate.",
                       PostedDate =DateTime.Parse("2017-01-16")
                   },
                   new Announcement()
                   {
                       Title ="android workshop",
                       Img="https://images.pexels.com/photos/595804/pexels-photo-595804.jpeg?auto=compress&cs=tinysrgb&h=350",
                       Description="Lorem ipsum dolor sit amet consectetur adipisicing elit. Nisi, cupiditate.",
                       PostedDate=DateTime.Parse("2017-01-16")
                   }
               }
            };

            var root3 = new EventAggregateRoot(event3);

            var roots = new List<EventAggregateRoot>() { root1, root2, root3 };

            return roots;
        }
    }
}
