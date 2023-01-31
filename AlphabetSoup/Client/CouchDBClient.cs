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
using System.Diagnostics.CodeAnalysis;

namespace AlphabetSoup.Client
{
    [ExcludeFromCodeCoverage]
    internal class CouchDBClient : ICouchDBClient
    {
        private readonly HttpClient httpClient;
        private readonly IParseJSONService _parseJSONService;
        public CouchDBClient(HttpClient client, IParseJSONService parseJSONService)
        {
            httpClient = client;
            _parseJSONService = parseJSONService;
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
            return _parseJSONService.ParseAcronymModelResponse(result, model);
        }

        public async Task<ICouchDBDocsModel> Get(string search)
        {
            JObject selectorJObject = new JObject(
                new JProperty("selector",
                    new JObject(
                       new JProperty("acronym",
                            new JObject(
                                new JProperty("$eq", $"{search}")
                                )
                            )
                       )
                    ),
                new JProperty("fields",
                    new JArray("_id", "_rev", "acronym", "fullName", "description"
                        )
                    )
                );
            string selectorJObjectString = selectorJObject.ToString();
            StringContent selector = new StringContent(selectorJObjectString);
            selector.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            HttpResponseMessage searchTask = await httpClient.PostAsync("http://localhost:5984/alphabetsoup/_find", selector);
            string searchValue = searchTask.Content.ReadAsStringAsync().Result;
            CouchDBDocsModel resultDocs = JsonConvert.DeserializeObject<CouchDBDocsModel>(searchValue);
            return resultDocs;
        }
        public async Task<IPurgeResponse> Purge(IPurgeModel purgeModel)
        {
            JObject purgeJObject = new JObject(
                new JProperty($"{purgeModel.Id}",
                new JArray($"{purgeModel.Rev}")
                    )
                )
                ;
            string purgeJObjectString = purgeJObject.ToString();
            StringContent purge = new StringContent(purgeJObjectString);
            purge.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            HttpResponseMessage purgeTask = await httpClient.PostAsync("http://localhost:5984/alphabetsoup/_purge", purge);
            string result = purgeTask.Content.ReadAsStringAsync().Result;
            return _parseJSONService.ParsePurgeResponse(result, purgeModel);
        }
        public async Task<ICouchDBAcronymModel> Modify(CouchDBAcronymModel model)
        {
            CouchDBAcronymModel response = new CouchDBAcronymModel();
            JObject updateJObject = new JObject(
                new JProperty("acronym", $"{model.Acronym}"),
                new JProperty("fullName", $"{model.FullName}"),
                new JProperty("description", $"{model.Description}"),
                new JProperty("_rev", $"{model.Rev}")
                );
            string updateJObjectString = updateJObject.ToString();
            StringContent update = new StringContent(updateJObjectString);
            update.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            HttpResponseMessage modifyTask = await httpClient.PutAsync("http://localhost:5984/alphabetsoup/" + $"{model.Id}", update);
            if (!modifyTask.IsSuccessStatusCode)
            {
                return null;
            }
            string result = modifyTask.Content.ReadAsStringAsync().Result;
            ParseJSONService parseService = new ParseJSONService();
            return parseService.ParseAcronymModelResponse(result, model);
        }
    }
}
