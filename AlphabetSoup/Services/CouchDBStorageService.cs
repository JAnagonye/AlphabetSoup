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

        public async Task<ICouchDBAcronymModel> Store(string acronym, string fullName, string desc)
        {
            if(acronym == null)
            {
                return null;
            }
            var acronymModel = new AcronymModel
            {
                Acronym = acronym,
                FullName = fullName,
                Description = desc
            };
            if (acronym.Length <= 10 && fullName.Length <= 100 && desc.Length <= 250 
                && Char.IsWhiteSpace(acronym, 0) == false )
            {
                return await httpClient.Insert(acronymModel);
            } else
            {
                return null; 
            }
        }
    }
}

