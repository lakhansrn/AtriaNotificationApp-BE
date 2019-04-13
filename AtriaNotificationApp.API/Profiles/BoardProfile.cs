using AtriaNotificationApp.API.Models;
using AtriaNotificationApp.DAL.Entities;
using AutoMapper;

namespace AtriaNotificationApp.API.Profiles
{
    public class BoardProfile : Profile
    {
        public BoardProfile()
        {
             CreateMap<Board,BoardDto>();
             CreateMap<BoardDto,Board>();
             CreateMap<Announcement,AnnouncementDto>();
             CreateMap<AnnouncementDto,Announcement>();
             CreateMap<Content,ContentDto>();
             CreateMap<ContentDto,Content>();
        }
    }
}