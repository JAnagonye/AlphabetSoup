﻿using AlphabetSoup.Models;

namespace AlphabetSoup.Client
{
    internal interface ICouchDBClient
    {
        void Insert(AcronymModel model);
        void ClientDelete();
        void ClientEdit();
        ICouchDBAcronymModel Get(string search);
    }
}