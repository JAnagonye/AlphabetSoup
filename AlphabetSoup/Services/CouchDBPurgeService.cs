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

        public async Task<IPurgeResponse> Delete(IPurgeModel purgeModel)
        {
            if(purgeModel == null || string.IsNullOrWhiteSpace(purgeModel.Id)|| string.IsNullOrWhiteSpace(purgeModel.Rev))
            {
                return null;
            }
            return await httpClient.Purge(purgeModel);
        }
    }
}