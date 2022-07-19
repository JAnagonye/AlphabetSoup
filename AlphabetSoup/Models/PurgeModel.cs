using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlphabetSoup.Models;
using Newtonsoft.Json;

namespace AlphabetSoup.Models
{
    public class PurgeModel : IPurgeModel
    {
        [JsonProperty("_id")] public string Id { get; set; }
        [JsonProperty("_rev")] public string Rev { get; set; }
    }
}
