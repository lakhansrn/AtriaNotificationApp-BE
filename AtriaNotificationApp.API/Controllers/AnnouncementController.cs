using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AtriaNotificationApp.API.Models;
using AtriaNotificationApp.BL.Interfaces;
using AtriaNotificationApp.BL.Services;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AtriaNotificationApp.API.Controllers
{
    [Route("api/announcement")]
    [ApiController]
    public class AnnouncementController : ControllerBase
    {
        private readonly IAnnouncementProviderService announcementProviderService;
        private readonly IMapper _mapper;

        public AnnouncementController(IMapper mapper)
        {
            announcementProviderService = new AnnouncementProviderService();
            _mapper = mapper;
        }

        [HttpGet("{guid}")]
        public async Task<ActionResult<List<ContentDto>>> GetContentsAsync(Guid guid)
        {
            var contents = await announcementProviderService.GetContentsAsync(guid);
            List<ContentDto> contentDtos = _mapper.Map<List<ContentDto>>(contents);
            return contentDtos;
        }
    }
}