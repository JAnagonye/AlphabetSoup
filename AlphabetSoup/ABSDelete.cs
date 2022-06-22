using System;

public class ABSDelete
{
    HttpClient client;
    string? deleteInput;
	public ABSDelete(HttpClient client)
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
