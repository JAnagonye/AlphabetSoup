using System.Xml.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using AlphabetSoup.Services;
using AlphabetSoup.Client;

namespace AlphabetSoup.Application
{
    internal sealed class AlphabetSoupApp
    {
        static void Main(string[] args)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                CouchDBClient client = new CouchDBClient(httpClient);
                httpClient.DefaultRequestHeaders.Add("Authorization", "Basic YWRtaW46WU9VUlBBU1NXT1JE");
                Console.WriteLine("Welcome to the Alphabet Soup Application");
                string inputStr = string.Empty;
                bool mainScreen = true;
                bool running = true;
                string searchInput = string.Empty;
                string acronym = string.Empty;
                string fullName = string.Empty;
                string desc = string.Empty;
                string deleteInput = string.Empty;
                string deleteIdInput = string.Empty;
                string deleteRevInput = string.Empty;
                string editInput = string.Empty;
                CouchDBStorageService storeService = new CouchDBStorageService(client);
                CouchDBSearchService searchService = new CouchDBSearchService(client);
                CouchDBPurgeService purgeService = new CouchDBPurgeService(client); 
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
                            Console.WriteLine("Input the Acryonym(Maximum Characters: 10)");
                            acronym = Console.ReadLine();
                            Console.WriteLine("Input the Full Name(Maximum Characters: 100");
                            fullName = Console.ReadLine();
                            Console.WriteLine("Input the Description(Maximum Characters: 250");
                            desc = Console.ReadLine();
                            storeService.Store(acronym, fullName, desc);
                            Console.WriteLine("It has been saved! Here's what you can do: ");
                            Console.WriteLine($"Here's what you've inputed for Acronym: {acronym}");
                            Console.WriteLine($"Here's what you've inputed for the Full Name: {fullName}");
                            Console.WriteLine($"Here's what you've inputed for the Description: {desc}");
                            Console.WriteLine("It has been saved! Here's what you can do: ");
                            mainScreen = true;
                            break;
                        case "2":
                            Console.WriteLine("Search for the Deletion (Warning: This will delete the whole file, the acronym and its data)");
                            deleteInput = Console.ReadLine();
                            Console.WriteLine(JsonConvert.SerializeObject(searchService.Search(deleteInput)));
                            Console.WriteLine("Input the ID(_id field) exactly from the search.");
                            deleteIdInput = Console.ReadLine();
                            Console.WriteLine("Input the Rev(_rev field) exactly from the Search");
                            deleteRevInput = Console.ReadLine();
                            if (!purgeService.Delete(deleteIdInput, deleteRevInput))
                            {
                                Console.WriteLine("Invalid Input. The id or rev is null.");
                            }
                            Console.WriteLine("Delete Completed");
                            mainScreen = true;
                            break;
                        case "3":
                            Console.WriteLine("Search for the acryonym to be Edited");
                            Console.WriteLine("1- Edit the Acronym ");
                            Console.WriteLine("2- Edit the Full Name");
                            Console.WriteLine("3- Edit the Descripton");
                            editInput = Console.ReadLine();
                            switch (editInput)
                            {
                                case "1":
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
                            Console.WriteLine(JsonConvert.SerializeObject(searchService.Search(searchInput)));
                            if (searchInput == "main")
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
        }
    }
}
