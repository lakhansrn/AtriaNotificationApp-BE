using System;

namespace AtriaNotificationApp.DAL.Interfaces
{
    public interface IEntityBase
    {
        Guid Id { get; set; }

        void InitId();
         
    }
}