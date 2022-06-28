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

        public ICouchDBAcronymModel Search(string search)
        {
            if (string.IsNullOrEmpty(search))
            {
                return null;
            }
            return httpClient.Get(search);
        }
    }
}