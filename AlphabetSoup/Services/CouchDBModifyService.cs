using System;
using AlphabetSoup.Client;
using AlphabetSoup.Models;

namespace AlphabetSoup.Services
{
    internal sealed class CouchDBModifyService : IModifyService
    {
        ICouchDBClient httpClient;
        public CouchDBModifyService(ICouchDBClient client)
        {
            this.httpClient = client;
        }

        public async Task<ICouchDBAcronymModel> Edit(CouchDBAcronymModel model)
        {
            if (model == null)
            {
                return null;
            }
            if(string.IsNullOrWhiteSpace(model.Acronym)|| string.IsNullOrWhiteSpace(model.FullName) || string.IsNullOrWhiteSpace(model.Description))
            {
                return null;
            }
            if (model.Acronym.Length <= 10 && model.FullName.Length <= 100 && model.Description.Length <= 250)
            {
                return await httpClient.Modify(model); 
            }
            return null;
        }
    }
}
