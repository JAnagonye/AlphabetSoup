using AlphabetSoup.Models;

namespace AlphabetSoup.Services
{
    public interface ISearchService
    {
        Task<ICouchDBDocsModel> Search(string search);
    }
}