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

        public bool Delete(string id, string rev)
        {
            if (id == null || rev == null)
            {
                return false;
            }
            else
            {
                httpClient.Purge(id, rev);
                return true;
            }
        }
    }
}