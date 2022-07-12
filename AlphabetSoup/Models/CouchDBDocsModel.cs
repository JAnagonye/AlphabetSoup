using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace AlphabetSoup.Models
{
    internal class CouchDBDocsModel : ICouchDBDocsModel
    {
        public List<CouchDBAcronymModel> docs { get; set; }
        IEnumerable<ICouchDBAcronymModel> ICouchDBDocsModel.Docs => docs;

        public override string ToString()
        {
            return JsonConvert.SerializeObject(docs);
        }
    }
}
