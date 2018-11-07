using AtriaNotificationApp.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AtriaNotificationApp.DAL.Interfaces
{
    public interface IUserAggregateRepository
    {
        Task<string> CheckIfUserExists(string email, int pno);

        Task<User> RegisterUser(User user);

        Task<User> GetUserAsync(Guid userid);

        Task<User> Authenticate(string email, string password);
    }
}
