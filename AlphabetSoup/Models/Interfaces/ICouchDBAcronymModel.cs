namespace AlphabetSoup.Models
{
    internal interface ICouchDBAcronymModel
    {
        IAcronymModel AcronymModel { get; set; }
        string id { get; set; }
    }
}