using System.Net.Http.Headers;

class ABSSearch
{
    public ABSSearch()
    {

    }

    public void Search(string? search, HttpClient httpClient)
    {
        string selectorJSON = @"{
        ""selector"": {
            ""acronym"": { 
                ""$regex"": " + $"\"{search}\"" +
            @"}
        },
        ""fields"": [
            ""_id""
            ""acronym"", 
            ""fullName"", 
            ""description""
            ]
        }";
        StringContent selector = new StringContent(selectorJSON);
        selector.Headers.ContentType = new MediaTypeHeaderValue("application/json");
        httpClient.DefaultRequestHeaders.Add("Authorization", "Basic YWRtaW46WU9VUlBBU1NXT1JE");
        Task<HttpResponseMessage> searchTask = httpClient.PostAsync("http://localhost:5984/alphabetsoup/_find", selector);
        Console.WriteLine(searchTask.Result.Content.ReadAsStringAsync().Result);
    }
}