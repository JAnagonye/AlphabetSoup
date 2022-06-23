using System;
using System.Net.Http.Json;

namespace AlphabetSoup
{
    internal sealed class CouchDBAdd : IStorageService
    {
        HttpClient client;
        string acronym;
        string fullName;
        string desc;
        public CouchDBAdd(HttpClient client)
        {
            this.client = client;
        }

        public void Add(string acronym, string fullName, string desc)
        {
            var soup = new AcronymModel
            {
                Acronym = acronym,
                FullName = fullName,
                Description = desc
            };
            Guid g = Guid.NewGuid();
            client.DefaultRequestHeaders.Add("Authorization", "Basic YWRtaW46WU9VUlBBU1NXT1JE");
            HttpResponseMessage nTask = client.PutAsync($"http://localhost:5984/alphabetsoup/{g}", JsonContent.Create<AcronymModel>(soup)).Result;           
        }
    }
}

