using System;
using AlphabetSoup.Client;

namespace AlphabetSoup.Services
{
    internal sealed class CouchDBModifyService : IModifyService
    {
        ICouchDBClient httpClient;
        public CouchDBModifyService(ICouchDBClient client)
        {
            this.httpClient = client;
        }

        public void Edit()
        {
            httpClient.Modify();
        }
    }
}
