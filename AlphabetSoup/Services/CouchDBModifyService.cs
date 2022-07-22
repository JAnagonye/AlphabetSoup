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
            CharacterLimitService characterLimitService = new CharacterLimitService();
            if(model == null)
            {
                return null;
            }
            if (string.IsNullOrWhiteSpace(model.Acronym))
            {
                return null;
            }
            if (!characterLimitService.CharacterLimit(model.Acronym, model.FullName, model.Description))
            {
                return null;
            }
            return await httpClient.Modify(model); 
        }
    }
}
