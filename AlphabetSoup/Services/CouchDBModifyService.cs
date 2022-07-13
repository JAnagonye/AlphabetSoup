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

        public ICouchDBAcronymModel Edit(CouchDBAcronymModel model)
        {
            if (model.Acronym.Length <= 10 && model.FullName.Length <= 100 && model.Description.Length <= 250
                && Char.IsWhiteSpace(model.Acronym, 0) == false)
            {
                return httpClient.Modify(model);
            }
            else
            {
                return null;
            }
        }
    }
}
