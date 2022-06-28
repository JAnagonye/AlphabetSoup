using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlphabetSoup.Models
{
    internal class CouchDBDocsModel
    {
        public List<CouchDBAcronymModel> couchDBDocs { get; set; }
        public ICouchDBAcronymModel CouchDBAcronym { get; set; }
    }
}
