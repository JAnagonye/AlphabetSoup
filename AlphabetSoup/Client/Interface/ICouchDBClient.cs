using AlphabetSoup.Models;

namespace AlphabetSoup.Client
{
    public interface ICouchDBClient
    {
        Task<ICouchDBAcronymModel> Insert(IAcronymModel model);
        Task<HttpResponseMessage> Purge(IPurgeModel purgeModel);
        Task<ICouchDBAcronymModel> Modify(CouchDBAcronymModel model);
        Task<ICouchDBDocsModel> Get(string search);
    }
}