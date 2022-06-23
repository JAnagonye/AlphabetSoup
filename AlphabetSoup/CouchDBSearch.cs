using System.Net.Http.Headers;

namespace AlphabetSoup
{
    internal sealed class CouchDBSearch : ISearchService
    {
        HttpClient client;
        string searchInput;
        public CouchDBSearch(HttpClient client)
        {
            this.client = client;
            searchInput = "";
        }
        public string Search(string search)
        {
            if(string.IsNullOrEmpty(search))
            {
                return null;
            }
            string selectorJSON = @"{
            ""selector"": {
            ""acronym"": { 
                ""$regex"": " + $"\"{search}\"" +
                @"}
            },
            ""fields"": [
            ""_id"",
            ""acronym"", 
            ""fullName"", 
            ""description""
            ]
            }";
            StringContent selector = new StringContent(selectorJSON);
            selector.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            client.DefaultRequestHeaders.Add("Authorization", "Basic YWRtaW46WU9VUlBBU1NXT1JE");
            Task<HttpResponseMessage> searchTask = client.PostAsync("http://localhost:5984/alphabetsoup/_find", selector);
            return searchTask.Result.Content.ReadAsStringAsync().Result;
        }
    }
}