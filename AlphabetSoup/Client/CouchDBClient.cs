using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using Newtonsoft.Json;
using System.Threading.Tasks;
using AlphabetSoup.Models;

namespace AlphabetSoup.Client
{
    internal class CouchDBClient : ICouchDBClient
    {
        HttpClient httpClient;
        public CouchDBClient(HttpClient client)
        {
            httpClient = client;
        }

        public void Insert(AcronymModel model)
        {
            Guid g = Guid.NewGuid();
            HttpResponseMessage nTask = httpClient.PutAsync($"http://localhost:5984/alphabetsoup/{g}", JsonContent.Create(model)).Result;
            var couchDBModel = new CouchDBAcronymModel
            {
                Id = ""
            };
        }

        public CouchDBDocsModel Get(string search)
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
            Task<HttpResponseMessage> searchTask = httpClient.PostAsync("http://localhost:5984/alphabetsoup/_find", selector);
            string searchValue = searchTask.Result.Content.ReadAsStringAsync().Result;
            //Console.WriteLine(searchValue);
            CouchDBDocsModel resultDocs = JsonConvert.DeserializeObject<CouchDBDocsModel>(searchValue);
            return resultDocs;
        }
        public void ClientDelete()
        {

        }
        public void ClientEdit()
        {

        }
    }
}
