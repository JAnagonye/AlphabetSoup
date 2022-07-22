using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlphabetSoup.Client;
using AlphabetSoup.Models;
using AlphabetSoup.Services;
using Moq;

namespace AlphabetSoup.UnitTest
{
    public class CouchDBPurgeTest
    {
        [Fact]
        public void Purge_WhenPurgeModelIsNull_ShouldReturnNull()
        {
            Mock<ICouchDBClient> mock = new Mock<ICouchDBClient>();
            mock.Setup(x => x.Purge(It.IsAny<IPurgeModel>())).Verifiable();
            CouchDBPurgeService purgeServiceTest = new CouchDBPurgeService(mock.Object);
            Task<IPurgeResponse> result = purgeServiceTest.Delete(null);
            mock.Verify(x => x.Purge(It.IsAny<IPurgeModel>()), Times.Never);
            Assert.Null(result.Result);
        }
        [Theory]
        [InlineData(null, null)]
        [InlineData("", "")]
        [InlineData(" ", " ")]
        public void Purge_WhenPurgeIdAndRevIsNull_ShouldReturnNull(string id, string rev)
        {
            IPurgeModel purgeModel = Mock.Of<IPurgeModel>(p => p.Id == id && p.Rev == rev);
            Mock<ICouchDBClient> mock = new Mock<ICouchDBClient>();
            mock.Setup(x => x.Purge(It.IsAny<IPurgeModel>())).Verifiable();
            CouchDBPurgeService purgeServiceTest = new CouchDBPurgeService(mock.Object);
            Task<IPurgeResponse> result = purgeServiceTest.Delete(purgeModel);
            mock.Verify(x => x.Purge(It.IsAny<IPurgeModel>()), Times.Never);
            Assert.Null(result.Result);
        }
        [Fact]
        public void Purge_WhenPurgeIsAValidValue_ShouldReturnValue()
        {
            IPurgeModel purgeModel = Mock.Of<IPurgeModel>(p => p.Id == "an ID" && p.Rev == " an Rev");
            Mock<ICouchDBClient> mock = new Mock<ICouchDBClient>();
            mock.Setup(x => x.Purge(purgeModel)).Returns(Task<IPurgeResponse>.FromResult(Mock.Of<IPurgeResponse>()));
            CouchDBPurgeService purgeServiceTest = new CouchDBPurgeService(mock.Object);
            Task<IPurgeResponse> result = purgeServiceTest.Delete(purgeModel);
            mock.Verify(x => x.Purge(purgeModel), Times.Once);
            Assert.NotNull(result.Result);
        }

    }
}
