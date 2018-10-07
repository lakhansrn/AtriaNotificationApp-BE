using AtriaNotificationApp.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AtriaNotificationApp.DAL.Interfaces
{
    public interface IContentAggregateRepository
    {
        Task<IEnumerable<ContentAggregateRoot>> GetContents(Guid id);
    }
}
