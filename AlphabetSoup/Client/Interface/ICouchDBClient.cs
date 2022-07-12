using AlphabetSoup.Models;

namespace AlphabetSoup.Client
{
    public interface ICouchDBClient
    {
        void Insert(IAcronymModel model);
        void Purge(string id, string rev);
        void Modify(string id, string rev, string acronym);
        ICouchDBDocsModel Get(string search);
    }
}