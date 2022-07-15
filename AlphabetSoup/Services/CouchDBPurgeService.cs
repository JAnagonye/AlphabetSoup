using System;
using AlphabetSoup.Client;
using AlphabetSoup.Models.Interfaces;

namespace AlphabetSoup.Services
{
    internal sealed class CouchDBPurgeService : IPurgeService
    {
        ICouchDBClient httpClient;
        public CouchDBPurgeService(ICouchDBClient client)
        {
            this.httpClient = client;
        }

        public async Task Delete(IPurgeModel purgeModel)
        {
                await httpClient.Purge(purgeModel);
        }
    }
}