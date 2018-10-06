
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents.Linq;
using AtriaNotificationApp.DAL.Interfaces;
using AtriaNotificationApp.DAL.Entities;
using MongoDB.Driver;
using MongoDB.Bson;

namespace AtriaNotificationApp.DAL.Repositories
{
    public class DocumentDBRepository<T> : IDocumentDBRepository<T> where T : EntityBase
    {

        // private readonly string Endpoint = "https://lakhan.documents.azure.com:443/";
        // private readonly string Key = "8AphYJMXMGQmdkT6FZF7kZYVXqkxvIMM6xzgL9nr66P1zvOa1hFKOzw5UZp3sS98S0iZzF9S4DG9QTNa976IOQ==";
        private readonly string DatabaseId = "AtriaNotificationDb";
        private readonly string CollectionId = typeof(T).Name;
        // private DocumentClient client;
        private MongoClient _client;

        private IMongoCollection<T> collection;

        public DocumentDBRepository()
        {
            // this.client = new DocumentClient(new Uri(Endpoint), Key);
            // try{
            _client = new MongoClient("mongodb+srv://admin:password12345@cluster0-lj6qc.mongodb.net/test?retryWrites=true");
            var database = _client.GetDatabase(DatabaseId);
            collection = database.GetCollection<T>(CollectionId);

            // CreateDatabaseIfNotExistsAsync().Wait();
            // CreateCollectionIfNotExistsAsync().Wait();
        // }
        }

        public async Task<T> GetItemAsync(Guid id)
        {
            try
            {
                var filter = Builders<T>.Filter.Where(x=>x.Id == id);

                var result = await collection.FindAsync(filter);

                return result.FirstOrDefault();

                // Document document = await client.ReadDocumentAsync(UriFactory.CreateDocumentUri(DatabaseId, CollectionId, id));
                // return (T)(dynamic)document;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<IEnumerable<T>> GetItemsAsync(Expression<Func<T, bool>> predicate)
        {
            // IDocumentQuery<T> query = client.CreateDocumentQuery<T>(
            //     UriFactory.CreateDocumentCollectionUri(DatabaseId, CollectionId),
            //     new FeedOptions { MaxItemCount = -1 })
            //     .Where(predicate)
            //     .AsDocumentQuery();

            // List<T> results = new List<T>();
            // while (query.HasMoreResults)
            // {
            //     results.AddRange(await query.ExecuteNextAsync<T>());
            // }

            var filter = Builders<T>.Filter.Where(predicate);
            var results = await collection.FindAsync(filter);

            return results.ToList();
        }

        public async Task<T> CreateItemAsync(T item)
        {
            await collection.InsertOneAsync(item);
            return item;
            //return await client.CreateDocumentAsync(UriFactory.CreateDocumentCollectionUri(DatabaseId, CollectionId), item);
        }

         public async Task<IEnumerable<T>> CreateItemsAsync(IEnumerable<T> items)
        {
            await collection.InsertManyAsync(items);

            return items;
        }

        public async Task<T> UpdateItemAsync(Guid id, T item)
        {
            var filter = Builders<T>.Filter.Where(x=>x.Id == id);
            return await collection.FindOneAndReplaceAsync<T>(filter,item);
            //return await client.ReplaceDocumentAsync(UriFactory.CreateDocumentUri(DatabaseId, CollectionId, id.ToString()), item);
        }

        public async Task DeleteItemAsync(Guid id)
        {
            var filter = Builders<T>.Filter.Where(x=>x.Id == id);
            await collection.DeleteOneAsync(filter);
            //await client.DeleteDocumentAsync(UriFactory.CreateDocumentUri(DatabaseId, CollectionId, id.ToString()));
        }

        public async Task<IEnumerable<T>> GetItemsAsync()
        {
            var filter = Builders<T>.Filter.Empty;
            var results = await collection.FindAsync(filter);

            return results.ToList();
        }

       

        // private async Task CreateDatabaseIfNotExistsAsync()
        // {
        //     try
        //     {
        //         await client.ReadDatabaseAsync(UriFactory.CreateDatabaseUri(DatabaseId));
        //     }
        //     catch (DocumentClientException e)
        //     {
        //         if (e.StatusCode == System.Net.HttpStatusCode.NotFound)
        //         {
        //             await client.CreateDatabaseAsync(new Database { Id = DatabaseId });
        //         }
        //         else
        //         {
        //             throw;
        //         }
        //     }
        // }

        // private async Task CreateCollectionIfNotExistsAsync()
        // {
        //     try
        //     {
        //         await client.ReadDocumentCollectionAsync(UriFactory.CreateDocumentCollectionUri(DatabaseId, CollectionId));
        //     }
        //     catch (DocumentClientException e)
        //     {
        //         if (e.StatusCode == System.Net.HttpStatusCode.NotFound)
        //         {
        //             await client.CreateDocumentCollectionAsync(
        //                 UriFactory.CreateDatabaseUri(DatabaseId),
        //                 new DocumentCollection { Id = CollectionId },
        //                 new RequestOptions { OfferThroughput = 1000 });
        //         }
        //         else
        //         {
        //             throw;
        //         }
        //     }
        // }
    }
}