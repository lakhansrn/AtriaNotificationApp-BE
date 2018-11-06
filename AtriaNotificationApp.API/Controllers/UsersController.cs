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
        [HttpPost("register/announcer")]
        public async Task<ActionResult<Register>> RegisterAnnouncer(string email)
        {
            if (email == null || email.Length == 0)
                return BadRequest(new { message = "Invalid Request" });
            var result = await registerService.RegisterAnnouncerAsync(email);
            var toEmail = result.Email;
            var id = result.Id;
            var mail = new ValuesController();
            mail.SendMail(new MailTestModel() { To = toEmail, Subject = "Announcer Registration", Body = $"Register using the following link http://localhost:4200/announcerRegistration?id={id}" });
            return Ok(result);
        }

        [AllowAnonymous]
        [HttpGet("register/announcer/{id}")]
        public async Task<ActionResult<Register>> GetRegisterAnnouncer(Guid id)
        {
            if (id.Equals(null))
                return BadRequest(new { message = "Invalid Request" });
            var result = await registerService.GetRegisterAnnouncerAsync(id);
            return Ok(result);
        }

        [AllowAnonymous]
        [HttpPost("complete_register/announcer")]
        public async Task<ActionResult<Register>> CompleteRegisterAnnouncer(UserDto user)
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