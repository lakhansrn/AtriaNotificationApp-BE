using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.Azure.Documents;
using MongoDB.Driver;

namespace AtriaNotificationApp.DAL.Interfaces
{
    public interface IDocumentDBRepository<T> where T : class
    {

        Task<T> CreateItemAsync(T item);

        Task<IEnumerable<T>> CreateItemsAsync(IEnumerable<T> items);

        Task DeleteItemAsync(Guid id);
        Task<T> GetItemAsync(Guid id);
        Task<IEnumerable<T>> GetItemsAsync(Expression<Func<T, bool>> predicate);
        Task<T> UpdateItemAsync(Guid id, T item);

    }
}