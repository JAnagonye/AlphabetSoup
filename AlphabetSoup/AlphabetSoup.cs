using System.Xml.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Net.Http.Headers;
using System.Net.Http.Json;

class App
{
    static HttpClient client = new HttpClient();

    private class Soup
    {
        public string? Acronym { get; set; }
        public string? FullName { get; set; }
        public string? Description { get; set; }
    }      

    static void Main(string[] args)
    {
        Console.WriteLine("Welcome to the Alphabet Soup Application");
        string? inputStr = "";
        bool mainScreen = true;
        bool running = true;
        string? acronym;
        string? fullName;
        string? desc;
        string? searchInput;
        string? mainInput;
        string? deleteInput;
        string? editInput;

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
                    acronym = Console.ReadLine();
                    Console.WriteLine("Input the Full Name");
                    fullName = Console.ReadLine();
                    Console.WriteLine("Input the Description");
                    desc = Console.ReadLine();
                    var soup = new Soup
                    {
                        Acronym = acronym,
                        FullName = fullName,
                        Description = desc
                    };
                    PostToDB(JsonContent.Create<Soup>(soup));
                    Console.WriteLine("It has been saved! Here's what you can do: ");
                    Console.WriteLine($"Here's what you've inputed for Acronym: {acronym}");
                    Console.WriteLine($"Here's what you've inputed for the Full Name: {fullName}");
                    Console.WriteLine($"Here's what you've inputed for the Description: {desc}");
                    Console.WriteLine("It has been saved! Here's what you can do: ");
                    mainScreen = true;
                    break;
                case "2":
                    Console.WriteLine("Search for the Deletion");
                    deleteInput = Console.ReadLine();
                    Search(deleteInput);
                    Console.WriteLine("Input the ID");
                    deleteInput = Console.ReadLine();
                    Delete(deleteInput);
                    Console.WriteLine("Delete Completed");
                    mainScreen = true;
                    break;
                case "3":
                    Console.WriteLine("Search for the acryonym to be Edited");
                    Console.WriteLine("1- Edit the Acronym ");
                    Console.WriteLine("2- Delete");
                    Console.WriteLine("3- Edit");
                    editInput = Console.ReadLine();
                    switch (editInput)
                    {
                        case "1":
                            Edit(editInput);
                            break;
                        case "2":
                            break;
                        case "3":
                            break;
                    }
                    Console.WriteLine("Editing Complete!");
                    mainScreen = true;
                    break;
                case "4":
                    Console.WriteLine("Search for the Acronym. Type 'main' to go to the main screen.");
                    searchInput = Console.ReadLine();
                    Search(searchInput);
                    mainInput = Console.ReadLine();
                    if (mainInput == "main")
                    {
                        mainScreen = true;
                    }
                    break;
            }
            if (inputStr == "exit")
            {
                running = false;
            }
        }
    }

    static void Search(string? search)
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
        client.DefaultRequestHeaders.Add("Authorization", "Basic YWRtaW46WU9VUlBBU1NXT1JE");
        Task<HttpResponseMessage> searchTask = client.PostAsync("http://localhost:5984/alphabetsoup/_find", selector);
        Console.WriteLine(searchTask.Result.Content.ReadAsStringAsync().Result) ;
    }

    static void PostToDB(HttpContent content)
    {
        Guid g = Guid.NewGuid();
        client.DefaultRequestHeaders.Add("Authorization", "Basic YWRtaW46WU9VUlBBU1NXT1JE");
        HttpResponseMessage nTask = client.PutAsync($"http://localhost:5984/alphabetsoup/{g}", content).Result;
    }

    static void Delete(string? delete)
    {

    }

    static void Edit(string? edit)
    {

    }
}
