using System;
using AlphabetSoup.Client;
using AlphabetSoup.Models;

namespace AlphabetSoup.Services
{
    internal sealed class CouchDBModifyService : IModifyService
    {
        private readonly ICouchDBClient httpClient;
        private readonly ICharacterLimitService _characterLimitService;
        public CouchDBModifyService(ICouchDBClient client, ICharacterLimitService characterLimitService)
        {
            this.httpClient = client;
            _characterLimitService = characterLimitService;
        }

        public async Task<ICouchDBAcronymModel> Edit(CouchDBAcronymModel model)
        {
            if(model == null)
            {
                return null;
            }
            if (string.IsNullOrWhiteSpace(model.Acronym))
            {
                return null;
            }
            if (!_characterLimitService.IsCharacterLimit(model.Acronym, model.FullName, model.Description))
            {
                return null;
            }
            return await httpClient.Modify(model); 
        }
    }
}
