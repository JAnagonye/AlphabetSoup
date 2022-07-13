using AlphabetSoup.Models;

namespace AlphabetSoup.Client
{
    public interface ICouchDBClient
    {
        ICouchDBAcronymModel Insert(IAcronymModel model);
        void Purge(string id, string rev);
        ICouchDBAcronymModel Modify(CouchDBAcronymModel model);
        ICouchDBDocsModel Get(string search);
    }
}