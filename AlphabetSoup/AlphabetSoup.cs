using System.Xml.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace AlphabetSoup
{
    internal sealed class AlphabetSoupApp
    {
        static HttpClient client = new HttpClient();

        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the Alphabet Soup Application");
            string inputStr = "";
            bool mainScreen = true;
            bool running = true;
            string mainInput;
            string searchInput = string.Empty;
            string acronym = string.Empty;
            string fullName = string.Empty;
            string desc = string.Empty;
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
                        CouchDBAdd absAdd = new CouchDBAdd(client);
                        absAdd.Add(acronym, fullName, desc);
                        Console.WriteLine("It has been saved! Here's what you can do: ");
                        Console.WriteLine($"Here's what you've inputed for Acronym: {acronym}");
                        Console.WriteLine($"Here's what you've inputed for the Full Name: {fullName}");
                        Console.WriteLine($"Here's what you've inputed for the Description: {desc}");
                        Console.WriteLine("It has been saved! Here's what you can do: ");
                        mainScreen = true;
                        break;
                    case "2":
                        CouchDBDelete absDelete = new CouchDBDelete(client);
                        absDelete.Delete();
                        mainScreen = true;
                        break;
                    case "3":
                        CouchDBEdit absEdit = new CouchDBEdit(client);
                        absEdit.Edit();
                        mainScreen = true;
                        break;
                    case "4":
                        Console.WriteLine("Search for the Acronym. Type 'main' to go to the main screen.");
                        searchInput = Console.ReadLine();
                        CouchDBSearch absSearch = new CouchDBSearch(client);
                        absSearch.Search(searchInput);
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
