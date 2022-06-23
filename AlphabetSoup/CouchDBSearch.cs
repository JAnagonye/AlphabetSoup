using System.Net.Http.Headers;

namespace AlphabetSoup
{
    internal sealed class CouchDBSearch : ISearchService
    {
        HttpClient client;
        public CouchDBSearch(HttpClient client)
        {
            this.client = client;
        }

        public string Search(string search)
        {
            if(string.IsNullOrEmpty(search))
            {
                return null;
            }
            CouchDBClient couchDBClient = new CouchDBClient();
            return couchDBClient.ClientSearch(search);
        }
    }
}