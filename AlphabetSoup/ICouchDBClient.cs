namespace AlphabetSoup
{
    internal interface ICouchDBClient
    {
        void ClientAdd();
        void ClientDelete();
        void ClientEdit();
        string ClientSearch(string search);
    }
}