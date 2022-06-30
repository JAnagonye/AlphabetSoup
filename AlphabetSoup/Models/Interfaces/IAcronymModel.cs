namespace AlphabetSoup.Models
{
    public interface IAcronymModel
    {
        string Acronym { get; set; }
        string Description { get; set; }
        string FullName { get; set; }
    }
}