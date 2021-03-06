﻿using AtriaNotificationApp.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AtriaNotificationApp.DAL.Interfaces
{
    public interface IRegisterAggregateRepo
    {
        Task<Register> RegisterUser(Register registerDetails);

        Task<Register> GetRegisterAnnouncer(Guid guid);

        //Task<Register> CompleteRegisterAnnouncer(User user);
    }
}
