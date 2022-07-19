using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlphabetSoup.Models
{
    internal class PurgeResponse : IPurgeResponse
    {
        public bool IsSuccess { get; set; }
        public string ErrorMessage { get; set; }

        public IPurgeModel PurgeModel { get; set; }
    }
}
