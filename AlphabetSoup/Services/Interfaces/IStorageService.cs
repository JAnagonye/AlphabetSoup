using AlphabetSoup.Models;

namespace AlphabetSoup.Services
{
    public interface IStorageService
    {
        Task<ICouchDBAcronymModel> Store(string acronym, string fullName, string desc);
    }
}