using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlphabetSoup.Client;
using AlphabetSoup.Models;
using AlphabetSoup.Models.Interfaces;
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
            mock.Setup(x => x.Purge(It.IsAny<IPurgeModel>())).Verifiable();
            CouchDBPurgeService purgeServiceTest = new CouchDBPurgeService(mock.Object);
            Task result = purgeServiceTest.Delete(null);
            mock.Verify(x => x.Purge(It.IsAny<IPurgeModel>()), Times.Never);
        }

        [Fact]
        public void Purge_WhenPurgeIsAValidValue_ShouldReturnTrue()
        {
            Mock<ICouchDBClient> mock = new Mock<ICouchDBClient>();
            mock.Setup(x => x.Purge(It.IsAny<IPurgeModel>())).Verifiable();
            CouchDBPurgeService purgeServiceTest = new CouchDBPurgeService(mock.Object);
            Task result = purgeServiceTest.Delete(new PurgeModel());
            mock.Verify(x => x.Purge(It.IsAny<IPurgeModel>()), Times.Once);
        }

    }
}
