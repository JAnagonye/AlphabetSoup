using AlphabetSoup.Models;

namespace AlphabetSoup.Models
{
    internal class AcronymModel : IAcronymModel
    {
        public string Acronym { get; set; }
        public string FullName { get; set; }
        public string Description { get; set; }
    }
}
