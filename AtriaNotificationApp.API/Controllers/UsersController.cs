using AtriaNotificationApp.API.Models;
using AtriaNotificationApp.API.Services;
using AtriaNotificationApp.BL.Interfaces;
using AtriaNotificationApp.BL.Services;
using AtriaNotificationApp.DAL.Entities;
using AtriaNotificationApp.Common.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using System.Linq;
using System.Collections.Generic;

namespace AtriaNotificationApp.API.Controllers
{
   [Authorize(Roles="announcer")]
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private IUserService _userService;
        private readonly IRegisterService registerService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
            registerService = new RegisterService();
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<ActionResult> Authenticate([FromBody]User userParam)
        {
            var user = await _userService.Authenticate(userParam.Email, userParam.Password);

            if (user == null)
                return BadRequest(new { message = "Email or password is incorrect" });

            return Ok(user);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var users =  _userService.GetAll();
            return Ok(users);
        }

        [AllowAnonymous]
        [HttpGet("{userid}")]
        public async Task<ActionResult<User>> GetUser(Guid userid)
        {
            if (Guid.Empty == userid && userid == null)
            {
                return BadRequest(new { message = "Empty guid" });
            }

            var user = await _userService.GetUserAsync(userid);
            return user;
        }

        [AllowAnonymous]
        [HttpPost("register/announcer")]
        public async Task<ActionResult<Register>> RegisterAnnouncer(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return BadRequest(new { message = "Invalid Request" });
            }
           
            await registerService.RegisterAnnouncerAsync(email, "announcer");
        
            return Ok();
        }

        [AllowAnonymous]
        [HttpPost("register/contentWriter")]
        public async Task<ActionResult<Register>> RegisterContentWriter([FromBody]ContentWriterRegistration form)
        {
            var email = form.Email;
            Guid announcerGuid = form.Id;

            if (string.IsNullOrEmpty(email))
            {
                return BadRequest(new { message = "Invalid Request" });
            }

            await registerService.RegisterContentWriterAsync(email, "contentWriter", announcerGuid);

            return Ok();
        }

        [AllowAnonymous]
        [HttpGet("register/user/{id}")]
        public async Task<ActionResult<Register>> GetRegisterUser(Guid id)
        {
            if (id.Equals(null))
                return BadRequest(new { message = "Invalid Request" });
            var result = await registerService.GetRegisterUserAsync(id);
            return Ok(result);
        }

        [AllowAnonymous]
        [HttpPost("complete_register/user")]
        public async Task<ActionResult<Register>> CompleteRegisterUser(UserDto user)
        {
            if (user == null)
                return BadRequest(new { message = "Empty field" });

            var userStatus = await _userService.checkIfUserExists(user.Email, user.Pno);

            if (userStatus != null)
            {
                if (userStatus.ToString() == "EMAIL_EXISTS")
                    return BadRequest(new { message = "Email already exists" });
                else if (userStatus.ToString() == "PNO_EXISTS")
                    return BadRequest(new { message = "Phone no. already exists" });
            }

            var userDetails = await _userService.RegisterUser(new User() { Department = user.Department, Email = user.Email, FirstName = user.FirstName, LastName = user.LastName, Password = user.Password, Pno = user.Pno, Role = user.Role });


            var user1 = await _userService.Authenticate(user.Email, user.Password);
                if (user1 == null)
                    return BadRequest(new { message = "Incorrect Email or Password" });
            return Ok(user1);
            
                
            
        }

    }
}