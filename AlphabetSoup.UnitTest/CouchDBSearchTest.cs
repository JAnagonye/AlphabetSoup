using AlphabetSoup.Client;
using AlphabetSoup.Models;
using AlphabetSoup.Services;
using Autofac.Extras.Moq;
using Moq;

namespace AlphabetSoup.UnitTest
{
    public class CouchDBSearchTest
    {
        [Fact]
        public void Search_WhenSearchIsNull_ShouldReturnNull()
        {
            Mock<ICouchDBClient> mock = new Mock<ICouchDBClient>();
            mock.Setup(x => x.Get(It.IsAny<string>())).Verifiable();
            CouchDBSearchService searchServiceTest = new CouchDBSearchService(mock.Object);
            Task<ICouchDBDocsModel> result = searchServiceTest.Search(null);
            Assert.Null(result.Result);
            mock.Verify(x => x.Get(It.IsAny<string>()), Times.Never);
        }
        [Fact]
        public void Search_WhenSearchIsHasValue_ShouldReturnValue()
        {
            Mock<ICouchDBClient> mock = new Mock<ICouchDBClient>();
            ICouchDBDocsModel couchDBAcronymModel = Mock.Of<ICouchDBDocsModel>();
            mock.Setup(x => x.Get("Test"));
            CouchDBSearchService searchServiceTest = new CouchDBSearchService(mock.Object);
            Task<ICouchDBDocsModel> actual = searchServiceTest.Search("Test");
            Assert.NotNull(actual);
            mock.Verify(x => x.Get("Test"), Times.Once);
        }
        
    }
}