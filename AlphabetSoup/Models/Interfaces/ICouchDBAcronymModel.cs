namespace AlphabetSoup.Models
{
    internal interface ICouchDBAcronymModel
    {
        string id { get; set; }
        IAcronymModel AcronymModel { get; set; }

    }
}