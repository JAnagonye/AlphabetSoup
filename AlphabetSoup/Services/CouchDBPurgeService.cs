using System;
using AlphabetSoup.Client;
using AlphabetSoup.Models;

namespace AlphabetSoup.Services
{
    internal sealed class CouchDBPurgeService : IPurgeService
    {
        ICouchDBClient httpClient;
        public CouchDBPurgeService(ICouchDBClient client)
        {
            this.httpClient = client;
        }

        public async Task<HttpResponseMessage> Delete(IPurgeModel purgeModel)
        {
                return await httpClient.Purge(purgeModel);
        }
    }
}