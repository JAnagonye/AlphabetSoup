using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlphabetSoup.Models;

namespace AlphabetSoup.Models
{
    internal sealed class CouchDBAcronymModel : AcronymModel, ICouchDBAcronymModel
    {
        public string id { get; set; }
        public IAcronymModel AcronymModel { get; set; }
    }
}
