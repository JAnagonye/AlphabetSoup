using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace AlphabetSoup.Models
{
    public interface ICouchDBDocsModel
    {
        public IEnumerable<ICouchDBAcronymModel> Docs { get; }
    }
}
