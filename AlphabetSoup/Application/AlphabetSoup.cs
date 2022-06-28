using System.Xml.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Net.Http.Headers;
using System.Net.Http.Json;
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
                string inputStr = "";
                bool mainScreen = true;
                bool running = true;
                string mainInput;
                string searchInput = string.Empty;
                string acronym = string.Empty;
                string fullName = string.Empty;
                string desc = string.Empty;
                CouchDBStorageService storeService = new CouchDBStorageService(client);
                CouchDBSearchService searchService = new CouchDBSearchService(client);
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
                            storeService.Store(acronym, fullName, desc);
                            Console.WriteLine("It has been saved! Here's what you can do: ");
                            Console.WriteLine($"Here's what you've inputed for Acronym: {acronym}");
                            Console.WriteLine($"Here's what you've inputed for the Full Name: {fullName}");
                            Console.WriteLine($"Here's what you've inputed for the Description: {desc}");
                            Console.WriteLine("It has been saved! Here's what you can do: ");
                            mainScreen = true;
                            break;
                        case "2":
                            //CouchDBDelete absDelete = new CouchDBDelete(client);
                            //absDelete.Delete();
                            mainScreen = true;
                            break;
                        case "3":
                            //couchdbedit absedit = new couchdbedit(client);
                            //absedit.edit();
                            mainScreen = true;
                            break;
                        case "4":
                            Console.WriteLine("Search for the Acronym. Type 'main' to go to the main screen.");
                            searchInput = Console.ReadLine();
                            Console.WriteLine(searchService.Search(searchInput));
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
        }
    }
}
