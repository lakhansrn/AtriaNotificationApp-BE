using System;
using System.Collections.Generic;
using System.Text;

namespace AtriaNotificationApp.BL.Interfaces
{
    public interface IPasswordService
    {
        string HashPassword(string password);

        bool VerifyHashedPassword(string hashedPassword, string password);
    }
}
