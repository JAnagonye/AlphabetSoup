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

        public bool Delete(IPurgeModel purgeModel)
        {
            if (purgeModel.Id == null || purgeModel.Rev == null)
            {
                return false;
            }
            else
            {
                httpClient.Purge(purgeModel);
                return true;
            }
        }
    }
}