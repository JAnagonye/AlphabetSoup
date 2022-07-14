using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlphabetSoup.Models;
using Newtonsoft.Json.Linq;

namespace AlphabetSoup.Services
{
    internal class ModelJSONParseService
    {

        public async Task<ICouchDBAcronymModel> JSONParse(string result, IAcronymModel model)
        {
            CouchDBAcronymModel response = new CouchDBAcronymModel();
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
