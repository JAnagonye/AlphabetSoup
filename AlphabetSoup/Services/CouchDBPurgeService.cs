using System;
using AlphabetSoup.Client;

namespace AlphabetSoup.Services
{
    internal sealed class CouchDBPurgeService : IPurgeService
    {
        ICouchDBClient httpClient;
        public CouchDBPurgeService(ICouchDBClient client)
        {
            this.httpClient = client;
        }

        public void Delete(string id, string rev)
        {
            httpClient.Purge(id, rev);
        }
    }
}