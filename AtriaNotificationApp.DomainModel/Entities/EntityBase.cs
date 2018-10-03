using System;
using AtriaNotificationApp.DAL.Interfaces;

namespace AtriaNotificationApp.DAL.Entities
{
    public class EntityBase : IEntityBase
    {
        public Guid Id { get ; set ; }

        public void InitId()
        {
            if(Id == Guid.Empty)
            {
                Id= Guid.NewGuid();
            }
        }
    }
}