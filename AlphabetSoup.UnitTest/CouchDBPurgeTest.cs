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
        public void Purge_WhenPurgeIsNull_ShouldReturnFalse()
        {
            Mock<ICouchDBClient> mock = new Mock<ICouchDBClient>();
            mock.Setup(x => x.Purge(It.IsAny<string>(), It.IsAny<string>())).Verifiable();
            CouchDBPurgeService purgeServiceTest = new CouchDBPurgeService(mock.Object);
            bool result = purgeServiceTest.Delete(null, null);
            Assert.False(result);
            mock.Verify(x => x.Purge(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
        }

        [Fact]
        public void Purge_WhenPurgeIsAValidValue_ShouldReturnTrue()
        {
            Mock<ICouchDBClient> mock = new Mock<ICouchDBClient>();
            mock.Setup(x => x.Purge(It.IsAny<string>(), It.IsAny<string>())).Verifiable();
            CouchDBPurgeService purgeServiceTest = new CouchDBPurgeService(mock.Object);
            bool result = purgeServiceTest.Delete("TestID", "TestRev");
            Assert.True(result);
            mock.Verify(x => x.Purge(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        }

    }
}
