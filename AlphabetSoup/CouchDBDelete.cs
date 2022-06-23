using System;

namespace AlphabetSoup
{
    public class CouchDBDelete
    {
        HttpClient client;
        string? deleteInput;
        public CouchDBDelete(HttpClient client)
        {
            this.client = client;
            deleteInput = "";
        }

        public void Delete()
        {
            Console.WriteLine("Search for the Deletion");
            deleteInput = Console.ReadLine();
            //Search(deleteInput);
            Console.WriteLine("Input the ID");
            deleteInput = Console.ReadLine();
            //Delete(deleteInput);
            Console.WriteLine("Delete Completed");
        }
    }
}