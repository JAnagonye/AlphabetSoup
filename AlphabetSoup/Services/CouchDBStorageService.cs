using System;
using System.Net.Http.Json;
using AlphabetSoup.Client;
using AlphabetSoup.Models;
using AlphabetSoup.Services;

namespace AlphabetSoup.Services
{
    internal sealed class CouchDBStorageService : IStorageService
    {
        ICouchDBClient httpClient;
        public CouchDBStorageService(ICouchDBClient client)
        {
            httpClient = client;
        }

        public async Task<ICouchDBAcronymModel> Store(string acronym, string fullName, string desc, ICharacterLimitService characterLimitService)
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

