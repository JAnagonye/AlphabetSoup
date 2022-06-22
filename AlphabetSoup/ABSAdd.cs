using System;
using System.Net.Http.Json;

public class ABSAdd
{
    HttpClient client;
    string? acronym;
    string? fullName;
    string? desc;

    public ABSAdd(HttpClient client)
    {
        this.client = client;
    }

    public void Add()
    {
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
        Guid g = Guid.NewGuid();
        client.DefaultRequestHeaders.Add("Authorization", "Basic YWRtaW46WU9VUlBBU1NXT1JE");
        HttpResponseMessage nTask = client.PutAsync($"http://localhost:5984/alphabetsoup/{g}", JsonContent.Create<Soup>(soup)).Result;
        Console.WriteLine("It has been saved! Here's what you can do: ");
        Console.WriteLine($"Here's what you've inputed for Acronym: {acronym}");
        Console.WriteLine($"Here's what you've inputed for the Full Name: {fullName}");
        Console.WriteLine($"Here's what you've inputed for the Description: {desc}");
        Console.WriteLine("It has been saved! Here's what you can do: ");
    }
}
