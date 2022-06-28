using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlphabetSoup.Models;

namespace AlphabetSoup.Models
{
    internal sealed class CouchDBAcronymModel : ICouchDBAcronymModel
    {
        public IAcronymModel AcronymModel { get; set; }
        public string id { get; set; }
    }
}
