using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using AlphabetSoup.Client;
using AlphabetSoup.Services;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace AlphabetSoup
{
    public static class AlphabetSoupExtensions
    {
        public static IServiceCollection AddAlphabetSoup(this IServiceCollection services)
        {
            services.AddResilientHttpClient<ICouchDBClient, CouchDBClient>();
            services.AddSingleton<IStorageService, CouchDBStorageService>();
            services.AddSingleton<ISearchService, CouchDBSearchService>();
            services.AddSingleton<IPurgeService, CouchDBPurgeService>();
            services.AddSingleton<IModifyService, CouchDBModifyService>();
            services.AddSingleton<IParseJSONService, ParseJSONService>();
            services.AddSingleton<ICharacterLimitService, CharacterLimitService>();
            return services;
        }

        private static IServiceCollection AddResilientHttpClient<TService, TImplementation>(this IServiceCollection collection) where TService : class
                                             where TImplementation : class, TService
        {
            collection.AddHttpClient<TService, TImplementation>((sp, client) => ConfigureClient(client));
            return collection;
        }
        private static void ConfigureClient(HttpClient client)
        {
            client.DefaultRequestHeaders.Add("Authorization", "Basic YWRtaW46WU9VUlBBU1NXT1JE");
        }
    }

}
