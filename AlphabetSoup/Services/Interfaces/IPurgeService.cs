using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlphabetSoup.Services
{
    internal interface IPurgeService
    {
        void Delete(string id, string rev);
    }
}
