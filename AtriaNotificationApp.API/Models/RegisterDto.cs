using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AtriaNotificationApp.API.Models
{
    public class RegisterDto
    {
        public Guid Id { get; set; }

        public String Role { get; set; }

        public String Email { get; set; }
    }
}
