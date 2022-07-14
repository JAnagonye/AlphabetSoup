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
using AlphabetSoup.Services;

namespace AlphabetSoup.Client
{
    internal class CouchDBClient : ICouchDBClient
    {
        HttpClient httpClient;
        public CouchDBClient(HttpClient client)
        {
            httpClient = client;
        }

        public async Task<ICouchDBAcronymModel> Insert(IAcronymModel model)
        {
            
            CouchDBAcronymModel response = new CouchDBAcronymModel();
            Guid g = Guid.NewGuid();
            HttpResponseMessage insertTask = await httpClient.PutAsync($"http://localhost:5984/alphabetsoup/{g}", JsonContent.Create(model));
            if(!insertTask.IsSuccessStatusCode)
            {
                return null;
            }
            string result = insertTask.Content.ReadAsStringAsync().Result;
            ModelJSONParseService parseService = new ModelJSONParseService();
            return await parseService.JSONParse(result, model);
        }

        public async Task<ICouchDBDocsModel> Get(string search)
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
            HttpResponseMessage searchTask = await httpClient.PostAsync("http://localhost:5984/alphabetsoup/_find", selector);
            string searchValue = searchTask.Content.ReadAsStringAsync().Result;
            CouchDBDocsModel resultDocs = JsonConvert.DeserializeObject<CouchDBDocsModel>(searchValue);
            return resultDocs;
        }
        public async void Purge(IPurgeModel purgeModel)
        {
            string purgeJSON = @"{ "
            + $"\"{purgeModel.Id}\"" + @": [ "
            + $"\"{purgeModel.Rev}\"" +
                @"]
            }";
            StringContent purge = new StringContent(purgeJSON);
            purge.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            HttpResponseMessage purgeTask = await httpClient.PostAsync("http://localhost:5984/alphabetsoup/_purge", purge);
        }
        public async Task<ICouchDBAcronymModel> Modify(CouchDBAcronymModel model)
        {
            CouchDBAcronymModel response = new CouchDBAcronymModel();
            string updateJSON = @"{
            ""acronym"": " + $"\"{model.Acronym}\"" +
            @", ""fullName"": " + $"\"{model.FullName}\"" +
            @", ""description"": " + $"\"{model.Description}\"" +
            @", ""_rev"": " + $"\"{model.Rev}\"" +
            @"}";
            StringContent update = new StringContent(updateJSON);
            HttpResponseMessage modifyTask = await httpClient.PutAsync("http://localhost:5984/alphabetsoup/" + $"{model.Id}", update);
            if (!modifyTask.IsSuccessStatusCode)
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
