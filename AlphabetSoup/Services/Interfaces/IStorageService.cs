

using AlphabetSoup.Models;

namespace AlphabetSoup.Services
{
    public interface IStorageService
    {
        ICouchDBAcronymModel Store(string acronym, string fullName, string desc);
    }
}