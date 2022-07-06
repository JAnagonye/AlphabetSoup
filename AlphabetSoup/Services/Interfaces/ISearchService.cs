using AlphabetSoup.Models;

namespace AlphabetSoup.Services
{
    public interface ISearchService
    {
        ICouchDBDocsModel Search(string search);
    }
}