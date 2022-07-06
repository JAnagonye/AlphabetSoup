using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlphabetSoup.Services
{
    public interface IPurgeService
    {
        bool Delete(string id, string rev);
    }
}
