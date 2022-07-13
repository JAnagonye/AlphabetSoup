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
            ICouchDBDocsModel result = searchServiceTest.Search(null);
            Assert.Null(result);
            mock.Verify(x => x.Get(It.IsAny<string>()), Times.Never);
        }
        [Fact]
        public void Search_WhenSearchIsHasValue_ShouldReturnValue()
        {
            Mock<ICouchDBClient> mock = new Mock<ICouchDBClient>();
            CouchDBDocsModel couchDBAcronymModel = Mock.Of<CouchDBDocsModel>();
            mock.Setup(x => x.Get("TestAcro"))
                             .Returns(couchDBAcronymModel);
            CouchDBSearchService searchServiceTest = new CouchDBSearchService(mock.Object);
            var actual = searchServiceTest.Search("TestAcro");
            Assert.NotNull(actual);
            mock.Verify(x => x.Get("TestAcro"), Times.Once);
        }
        
    }
}