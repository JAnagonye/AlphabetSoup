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
    public class CouchDBStorageTest
    {
        [Fact]
        public void Insert_WhenInsertIsNull_ShouldReturnFalse()
        {
            Mock<ICouchDBClient> mock = new Mock<ICouchDBClient>();
            mock.Setup(x => x.Insert(It.IsAny<AcronymModel>())).Verifiable();
            CouchDBStorageService storageServiceTest = new CouchDBStorageService(mock.Object);
            bool result = storageServiceTest.Store(null, null, null);
            Assert.False(result);
            mock.Verify(x => x.Insert(It.IsAny<AcronymModel>()), Times.Never);
        }

        [Fact]
        public void Insert_WhenInsertIsAValidValue_ShouldReturnTrue()
        {
            Mock<ICouchDBClient> mock = new Mock<ICouchDBClient>();
            mock.Setup(x => x.Insert(It.IsAny<AcronymModel>())).Verifiable();
            CouchDBStorageService storageServiceTest = new CouchDBStorageService(mock.Object);
            bool result = storageServiceTest.Store("Test", "Test","Test");
            Assert.True(result);
            mock.Verify(x => x.Insert(It.IsAny<AcronymModel>()), Times.Once);
        }

        [Fact]
        public void Insert_WhenInsertHasAValueOverMaxCharacters_ShouldReturnFalse()
        {
            Mock<ICouchDBClient> mock = new Mock<ICouchDBClient>();
            mock.Setup(x => x.Insert(It.IsAny<AcronymModel>())).Verifiable();
            CouchDBStorageService storageServiceTest = new CouchDBStorageService(mock.Object);
            bool result = storageServiceTest.Store("Very Long Test", "test", "test");
            Assert.False(result);
            mock.Verify(x => x.Insert(It.IsAny<AcronymModel>()), Times.Never);
        }

        [Fact]
        public void Insert_WhenInsertHasAcronymValueWithWhitespace_ShouldReturnFalse()
        {
            Mock<ICouchDBClient> mock = new Mock<ICouchDBClient>();
            mock.Setup(x => x.Insert(It.IsAny<AcronymModel>())).Verifiable();
            CouchDBStorageService storageServiceTest = new CouchDBStorageService(mock.Object);
            bool result = storageServiceTest.Store(" ", "AA", "AA");
            Assert.False(result);
            mock.Verify(x => x.Insert(It.IsAny<AcronymModel>()), Times.Never);
        }
    }
}
