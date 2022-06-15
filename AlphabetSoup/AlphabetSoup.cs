using System.Xml.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Net.Http.Headers;

//For accessing the Database, have to log into Docker(DOWNLOADED it through Docker and docker compose command https://medevel.com/tutorial-install-couchdb-with-docker/) and access the admin
class App
{
    static HttpClient client = new HttpClient();

    async void setUp() //(HttpContent? content)
    {
        //object res = await client.PostAsync("127.0.0.1:5984/alphabetsoup/_design/getalldata/_view/get-all-data", content);
        var request = new HttpRequestMessage(HttpMethod.Get, "http://localhost:5984/alphabetsoup/_design/getAllData/_view/get-all-data");
        request.Headers.Authorization = new AuthenticationHeaderValue("Basic", "YWRtaW46WU9VUlBBU1NXT1JE");
        try
        {
            HttpResponseMessage res = await client.SendAsync(request);
            //HttpResponseMessage response = await client.GetAsync("http://localhost:5984/alphabetsoup/_design/getAllData/_view/get-all-data");
            res.EnsureSuccessStatusCode();
            string responseBody = await res.Content.ReadAsStringAsync();
            Console.WriteLine(responseBody);
        }
        catch (HttpRequestException e)
        {
            Console.WriteLine("\nException Caught!");
            Console.WriteLine("Message :{0} ", e.Message);
        }
    }
    private class Soup
    {
        public string? Acryonym { get; set; }
        public string? FullName { get; set; }
        public string? Description { get; set; }
        

    }
    async void postToDB(HttpContent? content)
    {
        await client.PostAsync("http://localhost:5984/alphabetsoup/_design/getAllData/_view/get-all-data", content);
    }
    static Task Main(string[] args)
    {
        App nApp = new App();
        Console.WriteLine("Welcome to the Alphabet Soup Application");
        string? inputStr = "";
        bool mainScreen = true;
        bool running = true;
        string? acryonym;
        string? fullName;
        string? desc;
        //string? searchInput;
        
        while (running)
        {
            if (mainScreen)
            {
                Console.WriteLine("Type 'exit' in order to leave the App.");
                Console.WriteLine("1- Add");
                Console.WriteLine("2- Delete");
                Console.WriteLine("3- Edit");
                Console.WriteLine("4- Search");
                inputStr = Console.ReadLine();

                mainScreen = false;
            }
            switch (inputStr)
            {
                case "1":
                    Console.WriteLine("Input the Acryonym");
                    acryonym = Console.ReadLine();
                    Console.WriteLine("Input the Full Name");
                    fullName = Console.ReadLine();
                    Console.WriteLine("Input the Description");
                    desc = Console.ReadLine();
                    
                    var soup = new Soup
                    {
                        Acryonym = acryonym,
                        FullName = fullName,
                        Description = desc
                    };
                    string jsonSoup = JsonSerializer.Serialize<Soup>(soup);
                    //postToDB(JsonSerializer.Serialize<Soup>(soup));
                    Console.WriteLine(jsonSoup);
                    Console.WriteLine("It has been saved! Here's what you can do: ");
                    Console.WriteLine($"Here's what you've inputed for Acryonym: {acryonym}");
                    Console.WriteLine($"Here's what you've inputed for the Full Name: {fullName}");
                    Console.WriteLine($"Here's what you've inputed for the Description: {desc}");
                    Console.WriteLine("It has been saved! Here's what you can do: ");
                    mainScreen = true;
                    break;
                case "2":
                    Console.WriteLine("Search for the Deletion");
                    //TODO: Search Function
                    //TODO: Delete Function
                    Console.WriteLine("Delete Completed");
                    mainScreen = true;
                    break;
                case "3":
                    Console.WriteLine("Search for the acryonym to be Edited");
                    //TODO: Search Function
                    //TODO: Make a decision on rewrite, or to pick a specific part to edit
                    Console.WriteLine("Editing Complete!");
                    mainScreen = true;
                    break;
                case "4":
                    Console.WriteLine("Search");
                    //searchInput = Console.ReadLine();
                    //SearchFunction
                    //searchInput = Console.ReadLine();
                    mainScreen = true;
                    break;
            }
            if (inputStr == "exit")
            {
                running = false;
                nApp.setUp();
            }
        }

        return Task.CompletedTask;
    }

    static void Search(string? search)
    {

    }
}
