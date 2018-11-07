using System;
using System.Collections.Generic;
using System.Text;

namespace AtriaNotificationApp.DAL.Entities
{
    public class Register: EntityBase
    {
        public string Email { get; set; }

        public string Role { get; set; }

        public Guid ReportsTo { get; set; }

    }
}
