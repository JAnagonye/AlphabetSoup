namespace AlphabetSoup.Services
{
    public interface IStorageService
    {
        bool Store(string acronym, string fullName, string desc);
    }
}