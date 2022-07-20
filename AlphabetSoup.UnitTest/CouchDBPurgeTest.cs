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
            PurgeModel purgeModel = new PurgeModel();
            purgeModel.Id = id;
            purgeModel.Rev = rev;
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
            PurgeModel purgeModel = new PurgeModel();
            purgeModel.Id = "an ID";
            purgeModel.Rev = "an rev";
            Mock<ICouchDBClient> mock = new Mock<ICouchDBClient>();
            mock.Setup(x => x.Purge(Mock.Of<PurgeModel>())).Returns(Task<IPurgeResponse>.FromResult(Mock.Of<IPurgeResponse>()));
            CouchDBPurgeService purgeServiceTest = new CouchDBPurgeService(mock.Object);
            Task<IPurgeResponse> result = purgeServiceTest.Delete(purgeModel);
            mock.Verify(x => x.Purge(Mock.Of<PurgeModel>()), Times.Once);
            Assert.NotNull(result.Result);
        }

    }
}
