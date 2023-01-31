using AlphabetSoup.Models;

namespace AlphabetSoup.Client
{
    public interface ICouchDBClient
    {
        Task<ICouchDBAcronymModel> Insert(IAcronymModel model);
        Task<IPurgeResponse> Purge(IPurgeModel purgeModel);
        Task<ICouchDBAcronymModel> Modify(CouchDBAcronymModel model);
        Task<ICouchDBDocsModel> Get(string search);
    }
}