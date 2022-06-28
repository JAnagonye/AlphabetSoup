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

        public void Store(string acronym, string fullName, string desc)
        {
            var acronymModel = new AcronymModel
            {
                Acronym = acronym,
                FullName = fullName,
                Description = desc
            };
            httpClient.Insert(acronymModel);
        }
    }
}

