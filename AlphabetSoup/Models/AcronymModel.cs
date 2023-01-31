using AlphabetSoup.Models;
using Newtonsoft.Json;

namespace AlphabetSoup.Models
{
    public class AcronymModel : IAcronymModel
    {
        [JsonProperty("acronym")] public string Acronym { get; set; }
        [JsonProperty("fullName")] public string FullName { get; set; }
        [JsonProperty("description")] public string Description { get; set; }
    }
}
