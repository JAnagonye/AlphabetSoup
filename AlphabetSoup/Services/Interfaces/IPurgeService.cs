using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlphabetSoup.Models.Interfaces;

namespace AlphabetSoup.Services
{
    public interface IPurgeService
    {
        Task Delete(IPurgeModel purgeModel);
    }
}
