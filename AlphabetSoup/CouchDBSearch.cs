using System.Net.Http.Headers;

namespace AlphabetSoup
{
    internal sealed class CouchDBSearch : ISearchService
    {
        ICouchDBClient client;
        public CouchDBSearch(ICouchDBClient client)
        {
            this.client = client;
        }

        public string Search(string search)
        {
            if(string.IsNullOrEmpty(search))
            {
                return null;
            }
            return client.ClientSearch(search);
        }
    }
}