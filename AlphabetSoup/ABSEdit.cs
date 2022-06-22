using System;

namespace AlphabetSoup
{
    public class ABSEdit
    {
        HttpClient client;
        string? editInput;
        public ABSEdit(HttpClient client)
        {
            this.client = client;
            editInput = "";
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
                    break;
                case "2":
                    break;
                case "3":
                    break;
            }
            Console.WriteLine("Editing Complete!");
        }
    }
}
