using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace AlphabetSoup.Models
{
    internal interface IStatusResponse
    {
        [JsonProperty("status")] public int Status { get; set; }
    }
}
