using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AtriaNotificationApp.API.Models
{
    public abstract class DateTrackDtoBase
    {
        public DateTime? DateModifiedOn { get; set; }

        public DateTime? DateCreatedOn { get; set; }
    }
}
