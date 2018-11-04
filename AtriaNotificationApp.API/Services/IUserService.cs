using System.Collections.Generic;
using AtriaNotificationApp.API.Models;

namespace AtriaNotificationApp.API.Services
{
    public interface IUserService
    {
        User Authenticate(string email, string password);
        IEnumerable<User> GetAll();
    }
}