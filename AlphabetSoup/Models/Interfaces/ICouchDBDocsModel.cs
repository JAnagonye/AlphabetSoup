using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlphabetSoup.Models
{
    internal interface ICouchDBDocsModel
    {
        public List<CouchDBAcronymModel> Docs { get; set; }
        public ICouchDBAcronymModel CouchDBAcronym { get; set; }
    }
}
