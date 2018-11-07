using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AtriaNotificationApp.API.Models;
using AtriaNotificationApp.DAL.Entities;

namespace AtriaNotificationApp.API.Services
{
    public interface IUserService
    {
        Task<User> Authenticate(string email, string password);
        IEnumerable<UserDto> GetAll();
        Task<string> checkIfUserExists(string email, int pno);
        Task<User> RegisterUser(User user);
        Task<User> GetUserAsync(Guid userid);
    }
}