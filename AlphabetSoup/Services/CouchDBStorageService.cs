using System;
using System.Net.Http.Json;
using AlphabetSoup.Client;
using AlphabetSoup.Models;

namespace AlphabetSoup.Services
{
    internal sealed class CouchDBStorageService : IStorageService
    {
        private readonly ICouchDBClient httpClient;
        private readonly ICharacterLimitService characterLimitService;

        public CouchDBStorageService(ICouchDBClient httpClient, ICharacterLimitService characterLimitService)
        {
            this.httpClient = httpClient;
            this.characterLimitService = characterLimitService;
        }

        public async Task<ICouchDBAcronymModel> Store(string acronym, string fullName, string desc)
        {
            if (string.IsNullOrWhiteSpace(acronym))
            {
                return null;
            }
            if (!characterLimitService.IsCharacterLimit(acronym, fullName, desc))
            {
                return null;
            }
            var acronymModel = new AcronymModel
            {
                Acronym = acronym,
                FullName = fullName,
                Description = desc
            };
            return await httpClient.Insert(acronymModel);
        }
    }
}

