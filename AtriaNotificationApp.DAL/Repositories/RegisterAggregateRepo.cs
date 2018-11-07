using AtriaNotificationApp.DAL.Interfaces;
using AtriaNotificationApp.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;

namespace AtriaNotificationApp.DAL.Repositories
{
    public class RegisterAggregateRepo: IRegisterAggregateRepo
    {
        public async Task<Register> RegisterUser(Register registerDetails)
        {
            DocumentDBRepository<Register> registerRepo = new DocumentDBRepository<Register>();
            registerDetails.InitId();
            var result = await registerRepo.CreateItemAsync(registerDetails);
            return result;
        }

        public async Task<Register> GetRegisterAnnouncer(Guid guid)
        {
            DocumentDBRepository<Register> registerRepo = new DocumentDBRepository<Register>();
            var result = await registerRepo.GetItemAsync(guid);
            return result;
        }

        //public Task<Register> CompleteRegisterAnnouncer(User user)
        //{
        //    DocumentDBRepository<User> userRepo = new DocumentDBRepository<User>();
        //}
    }
}
