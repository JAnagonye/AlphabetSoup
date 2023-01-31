using AlphabetSoup.Models;

namespace AlphabetSoup.Services
{
    internal interface IParseJSONService
    {
        ICouchDBAcronymModel ParseAcronymModelResponse(string result, IAcronymModel model);
        IPurgeResponse ParsePurgeResponse(string result, IPurgeModel purgeModel);
    }
}