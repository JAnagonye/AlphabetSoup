using System.Net.Http.Headers;

class ABSSearch
{
    HttpClient client;
    string? searchInput;
    public ABSSearch(HttpClient client)
    {
        this.client = client;
        searchInput = "";
    }
    public void Search()//(string? search)
    {
        Console.WriteLine("Search for the Acronym. Type 'main' to go to the main screen.");
        searchInput = Console.ReadLine();
        string selectorJSON = @"{
        ""selector"": {
            ""acronym"": { 
                ""$regex"": " + $"\"{searchInput}\"" +
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
        Console.WriteLine(searchTask.Result.Content.ReadAsStringAsync().Result);
    }
}