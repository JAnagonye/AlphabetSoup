using AlphabetSoup.Models;

namespace AlphabetSoup.Services
{
    internal interface ISearchService
    {
        CouchDBDocsModel Search(string search);
    }
}