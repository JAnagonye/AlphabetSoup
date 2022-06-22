using System.Xml.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Net.Http.Headers;
using System.Net.Http.Json;

class AlphabetSoup
{
    static HttpClient client = new HttpClient();

    static void Main(string[] args)
    {
        Console.WriteLine("Welcome to the Alphabet Soup Application");
        string? inputStr = "";
        bool mainScreen = true;
        bool running = true;
        string? mainInput;

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
                    ABSAdd absAdd = new ABSAdd(client);
                    absAdd.Add();
                    mainScreen = true;
                    break;
                case "2":
                    ABSDelete absDelete = new ABSDelete(client);
                    absDelete.Delete();
                    mainScreen = true;
                    break;
                case "3":
                    ABSEdit absEdit = new ABSEdit(client);
                    absEdit.Edit();
                    mainScreen = true;
                    break;
                case "4":
                    ABSSearch absSearch = new ABSSearch(client);
                    absSearch.Search();
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
