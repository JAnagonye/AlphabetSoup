using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlphabetSoup.Models
{
    public interface IPurgeResponse
    {
        bool IsSuccess { get;  }
        string ErrorMessage { get; }
        IPurgeModel PurgeModel { get; }
    }
}
