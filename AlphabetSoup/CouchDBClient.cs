using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace AlphabetSoup
{
    internal class CouchDBClient
    {
        HttpClient client;

        public CouchDBClient()
        {
            this.client = new HttpClient();
        }

        public void ClientAdd()
        {

        }

        public string ClientSearch(string search)
        {
            string selectorJSON = @"{
            ""selector"": {
            ""acronym"": { 
                ""$regex"": " + $"\"{search}\"" +
                @"}
            },
            ""fields"": [
            ""_id"",
            ""acronym"", 
            ""fullName"", 
            ""description""
            ]
            }";
            StringContent selector = new StringContent(selectorJSON);
            selector.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            client.DefaultRequestHeaders.Add("Authorization", "Basic YWRtaW46WU9VUlBBU1NXT1JE");
            Task<HttpResponseMessage> searchTask = client.PostAsync("http://localhost:5984/alphabetsoup/_find", selector);
            return searchTask.Result.Content.ReadAsStringAsync().Result;
        }
        public void ClientDelete()
        {

        }
        public void ClientEdit()
        {

        }
    }
}
