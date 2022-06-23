using AlphabetSoup;
using Autofac.Extras.Moq;

namespace AlphabetSoup.UnitTest
{
    public class CouchDBSearchTest
    {
        [Fact]
        public void Test1()
        {
            using (var mock = AutoMock.GetLoose())
            {
                HttpClient client = new HttpClient();
                CouchDBSearch absSearchTest = new CouchDBSearch(client);
                string result = absSearchTest.Search(null);
                Assert.Null(result);
            }
        }
        [Fact]
        public void Search_WhenSearchIsHasValue_ShouldReturnValue()
        {
            using (var mock = AutoMock.GetLoose())
            {
                HttpClient client = new HttpClient();
                CouchDBSearch absSearchTest = new CouchDBSearch(client);
                mock.Mock<ISearchService>()
                    .Setup(x => x.Search("TestAcronym"))
                    .Returns(GetAcronym);
                var ctlr = mock.Create<ISearchService>;
                var expected = GetAcronym();

                var actual = ctlr.Search();
            }
        }

        private string GetAcronym()
        {
            Guid g = new Guid();
            string testAcronym = @" ""_id"":" + $"\"{g}\"" + 
            @"""acronym"": ""TestAcronym"",
            ""fullName"": ""test"",
            ""description"": ""test""";
            return testAcronym;      
        }
    }
}