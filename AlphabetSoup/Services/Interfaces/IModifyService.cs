using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlphabetSoup.Models;

namespace AlphabetSoup.Services
{
    public interface IModifyService
    {
        Task<ICouchDBAcronymModel> Edit(CouchDBAcronymModel model);
    }
}
