using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlphabetSoup.Services
{
    internal class CharacterLimitService
    {
        public bool CharacterLimit(string acronym, string fullName, string desc)
        {
            if (acronym.Length > 10 || fullName.Length > 100 || desc.Length > 250)
            {
                return false;
            }
            return true;
        }
    }
}
