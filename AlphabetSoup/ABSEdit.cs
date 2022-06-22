using System;

public class ABSEdit
{
	HttpClient client;
	public ABSEdit(HttpClient client)
    {
        this.client = client;
    }

    public void Edit()
    {
        Console.WriteLine("Search for the acryonym to be Edited");
        Console.WriteLine("1- Edit the Acronym ");
        Console.WriteLine("2- Edit the Full Name");
        Console.WriteLine("3- Edit the Descripton");
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
    }
}
