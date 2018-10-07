using AtriaNotificationApp.API.Models;
using AtriaNotificationApp.DAL.Entities;
using AutoMapper;

namespace AtriaNotificationApp.API.Profiles
{
    public class EventProfile : Profile
    {
        public EventProfile()
        {
             CreateMap<Event,EventDto>();
             CreateMap<EventDto,Event>();
             CreateMap<Announcement,AnnouncementDto>();
             CreateMap<AnnouncementDto,Announcement>();
             CreateMap<Announcement,ContentDto>();
             CreateMap<ContentDto,Announcement>();
        }
    }
}