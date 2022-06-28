using AlphabetSoup.Models;

namespace AlphabetSoup.Services
{
    internal interface ISearchService
    {
        ICouchDBAcronymModel Search(string search);
    }
}