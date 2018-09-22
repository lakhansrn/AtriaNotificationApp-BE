using AtriaNotificationApp.Common.Interfaces;
using AtriaNotificationApp.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace AtriaNotificationApp.DAL.Interfaces
{
    public interface IEventAggregateRoot: IAggregateRoot
    {
         Event Event { get; }
    }
}
