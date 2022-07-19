using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlphabetSoup.Models;
using AlphabetSoup.Services;
using Newtonsoft.Json.Linq;

namespace AlphabetSoup.Services
{
    internal class ParseJSONService : IParseJSONService
    {

        public ICouchDBAcronymModel ParseAcronymModelResponse(string result, IAcronymModel model)
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

        public IPurgeResponse ParsePurgeResponse(string result, IPurgeModel purgeModel)
        {
            PurgeResponse response = new PurgeResponse();
            response.PurgeModel = purgeModel;
            response.IsSuccess = false;
            JObject jObject = JObject.Parse(result);
            JToken purged = jObject.GetValue("purged");
            if (purged == null)
            {
                response.ErrorMessage = "There was an error purging.";
                return response;
            }
            JArray rev = purged[purgeModel.Id] as JArray;
            if (rev == null)
            {
                response.ErrorMessage = "Id was invalid.";
                return response;
            }
            if(purgeModel.Rev != rev.First.Value<string>())
            {
                response.ErrorMessage = "Rev was invalid.";
                return response;
            }
            response.IsSuccess = true;
            return response;
        }
    }
}
