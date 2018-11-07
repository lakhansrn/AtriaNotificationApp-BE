using AtriaNotificationApp.DAL.Entities;
using AtriaNotificationApp.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace AtriaNotificationApp.DAL.Repositories
{
    public class UserAggregateRepository: IUserAggregateRepository
    {
        public async Task<string> CheckIfUserExists(string email, int pno)
        {
            DocumentDBRepository<User> userRepo = new DocumentDBRepository<User>();
            var users = await userRepo.GetItemsAsync(x => x.Email == email || x.Pno == pno);
            foreach (var item in users)
            {
                if (item.Email == email)
                    return "EMAIL_EXISTS";
                else if (item.Pno == pno)
                    return "PNO_EXISTS";
            }
            return null;
        }

        public async Task<User> RegisterUser(User user)
        {
            DocumentDBRepository<User> userRepo = new DocumentDBRepository<User>();
            var userDetails = await userRepo.CreateItemAsync(user);
            return userDetails;
        }

        public async Task<User> GetUserAsync(Guid userid)
        {
            DocumentDBRepository<User> userRepo = new DocumentDBRepository<User>();
            var userDetails = await userRepo.GetItemAsync(userid);
            if (userDetails == null)
                return null;
            return userDetails;
        }

        public async Task<User> Authenticate(string email, string password)
        {
            DocumentDBRepository<User> userRepo = new DocumentDBRepository<User>();
            var userDetails = await userRepo.GetItemsAsync(x => x.Email == email);
            if (userDetails == null)
                return null;
            return userDetails.FirstOrDefault(x => x.Email == email);
        }
    }
}
