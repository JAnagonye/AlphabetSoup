using System.Net.Http.Headers;
using AlphabetSoup.Client;
using AlphabetSoup.Models;
using AlphabetSoup.Services;

namespace AlphabetSoup.Services
{
    internal sealed class CouchDBSearchService : ISearchService
    {
        ICouchDBClient httpClient;
        public CouchDBSearchService(ICouchDBClient client)
        {
            httpClient = client;
        }

        public async Task<ICouchDBDocsModel> Search(string search)
        {
            if (string.IsNullOrWhiteSpace(search))
            {
                return null;
            }
            return await httpClient.Get(search);
        }
    }
}