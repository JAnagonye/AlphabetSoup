namespace AlphabetSoup.Services
{
    public interface ICharacterLimitService
    {
        bool IsCharacterLimit(string acronym, string fullName, string desc);
    }
}