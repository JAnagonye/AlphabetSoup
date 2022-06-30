using AlphabetSoup.Models;

namespace AlphabetSoup.Client
{
    internal interface ICouchDBClient
    {
        void Insert(AcronymModel model);
        void Purge(string id, string rev);
        void Modify();
        CouchDBDocsModel Get(string search);
    }
}