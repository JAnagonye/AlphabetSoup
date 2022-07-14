using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using Newtonsoft.Json;
using System.Threading.Tasks;
using AlphabetSoup.Models;
using System.Text.Json.Nodes;
using Newtonsoft.Json.Linq;

namespace AlphabetSoup.Client
{
    internal class CouchDBClient : ICouchDBClient
    {
        HttpClient httpClient;
        public CouchDBClient(HttpClient client)
        {
            httpClient = client;
        }

        public ICouchDBAcronymModel Insert(IAcronymModel model)
        {
            CouchDBAcronymModel response = new CouchDBAcronymModel();
            Guid g = Guid.NewGuid();
            HttpResponseMessage insertTask = httpClient.PutAsync($"http://localhost:5984/alphabetsoup/{g}", JsonContent.Create(model)).Result;
            if(insertTask.IsSuccessStatusCode == false)
            {
                return null;
            }
            string result = insertTask.Content.ReadAsStringAsync().Result;
            JObject jObject = JObject.Parse(result);
            JToken jToken = jObject.GetValue("ok");
            if (!jToken.Value<bool>())
            {
                return null;
            }
            response.Acronym = model.Acronym;
            response.FullName = model.FullName;
            response.Description = model.Description;
            response.Id = jObject.GetValue("id").Value<string>();
            response.Rev = jObject.GetValue("rev").Value<string>();
            return response;
        }

        public ICouchDBDocsModel Get(string search)
        {
            string selectorJSON = @"{
            ""selector"": {
            ""acronym"": { 
                ""$regex"": " + $"\"{search}\"" +
                    @"}
                },
            ""fields"": [
            ""_id"",
            ""_rev"",
            ""acronym"", 
            ""fullName"", 
            ""description""
                ]
            }";
            StringContent selector = new StringContent(selectorJSON);
            selector.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            Task<HttpResponseMessage> searchTask = httpClient.PostAsync("http://localhost:5984/alphabetsoup/_find", selector);
            string searchValue = searchTask.Result.Content.ReadAsStringAsync().Result;
            CouchDBDocsModel resultDocs = JsonConvert.DeserializeObject<CouchDBDocsModel>(searchValue);
            return resultDocs;
        }
        public void Purge(string id, string rev)
        {
            string purgeJSON = @"{ "
            + $"\"{id}\"" + @": [ "
            + $"\"{rev}\"" +
                @"]
            }";
            StringContent purge = new StringContent(purgeJSON);
            purge.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            Task<HttpResponseMessage> purgeTask = httpClient.PostAsync("http://localhost:5984/alphabetsoup/_purge", purge);
        }
        public ICouchDBAcronymModel Modify(CouchDBAcronymModel model)
        {
            CouchDBAcronymModel response = new CouchDBAcronymModel();
            string updateJSON = @"{
            ""acronym"": " + $"\"{model.Acronym}\"" +
            @", ""fullName"": " + $"\"{model.FullName}\"" +
            @", ""description"": " + $"\"{model.Description}\"" +
            @", ""_rev"": " + $"\"{model.Rev}\"" +
            @"}";
            StringContent update = new StringContent(updateJSON);
            HttpResponseMessage modifyTask = httpClient.PutAsync("http://localhost:5984/alphabetsoup/" + $"{model.Id}", update).Result;
            if (modifyTask.IsSuccessStatusCode == false)
            {
                return null;
            }
            string result = modifyTask.Content.ReadAsStringAsync().Result;
            JObject jObject = JObject.Parse(result);
            JToken jToken = jObject.GetValue("ok");
            if (!jToken.Value<bool>())
            {
                return null;
            }
            response.Acronym = model.Acronym;
            response.FullName = model.FullName;
            response.Description = model.Description;
            response.Id = jObject.GetValue("id").Value<string>();
            response.Rev = jObject.GetValue("rev").Value<string>();
            return response;
        }
    }
}
