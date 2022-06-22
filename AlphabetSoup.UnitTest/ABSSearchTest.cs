using AlphabetSoup;

namespace AlphabetSoup.UnitTest
{
    public class ABSSearchTest
    {
        [Fact]
        public void Test1()
        {
            HttpClient client = new HttpClient();
            ABSSearch absSearchTest = new ABSSearch(client);
            string result = absSearchTest.Search(null);
            Assert.Null(result);
        }
    }
}