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
        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        public void Insert_WhenInsertIsNull_ShouldReturnFalse(string inputValue)
        {
            Mock<ICouchDBClient> mock = new Mock<ICouchDBClient>();
            mock.Setup(x => x.Insert(It.IsAny<AcronymModel>())).Verifiable();
            CouchDBStorageService storageServiceTest = new CouchDBStorageService(mock.Object);
            Task<ICouchDBAcronymModel> result = storageServiceTest.Store(inputValue, inputValue, inputValue);
            mock.Verify(x => x.Insert(It.IsAny<AcronymModel>()), Times.Never);
        }

        [Fact]
        public void Insert_WhenInsertIsAValidValue_ShouldReturnTrue()
        {
            Mock<ICouchDBClient> mock = new Mock<ICouchDBClient>();
            mock.Setup(x => x.Insert(It.IsAny<AcronymModel>())).Verifiable();
            CouchDBStorageService storageServiceTest = new CouchDBStorageService(mock.Object);
            Task<ICouchDBAcronymModel> result = storageServiceTest.Store("Test", "Test","Test");
            mock.Verify(x => x.Insert(It.IsAny<AcronymModel>()), Times.Once);
        }

        [Fact]
        public void Insert_WhenInsertHasAValueOverMaxCharacters_ShouldReturnFalse()
        {
            Mock<ICouchDBClient> mock = new Mock<ICouchDBClient>();
            mock.Setup(x => x.Insert(It.IsAny<AcronymModel>())).Verifiable();
            CouchDBStorageService storageServiceTest = new CouchDBStorageService(mock.Object);
            Task<ICouchDBAcronymModel> result = storageServiceTest.Store("Very Long Test", "test", "test");
            mock.Verify(x => x.Insert(It.IsAny<AcronymModel>()), Times.Never);
        }
    }
}
