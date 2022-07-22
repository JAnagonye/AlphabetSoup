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
        [InlineData(null, null, null)]
        [InlineData("", "", "")]
        [InlineData(" ", " ", " ")]
        public void Insert_WhenInsertIsNull_ShouldReturnNull(string acronym, string fullName, string desc)
        {
            Mock<ICouchDBClient> mock = new Mock<ICouchDBClient>();
            mock.Setup(x => x.Insert(It.IsAny<IAcronymModel>())).Verifiable();
            CouchDBStorageService storageServiceTest = new CouchDBStorageService(mock.Object);
            Task<ICouchDBAcronymModel> result = storageServiceTest.Store(acronym, fullName, desc);
            mock.Verify(x => x.Insert(It.IsAny<IAcronymModel>()), Times.Never);
            Assert. Null(result.Result);
        }
        [Theory]
        [InlineData("LONGACRONYM", " full", "desc ")]
        [InlineData("Acro", "Over 100 characters long                                                                                    ", "DESC ")]
        [InlineData("A", "F ", "This is an attempt to make this message over 250 characters long                               " +
            "                                             " +
            "                                                   " +
            "                                                                                                                                        ")]
        public void Insert_WhenInsertHasAValueOverMaxCharacters_ShouldReturnNull(string acronym, string fullName, string desc)
        {
            Mock<ICouchDBClient> mock = new Mock<ICouchDBClient>();
            mock.Setup(x => x.Insert(It.IsAny<AcronymModel>())).Verifiable();
            CouchDBStorageService storageServiceTest = new CouchDBStorageService(mock.Object);
            Task<ICouchDBAcronymModel> result = storageServiceTest.Store(acronym, fullName, desc);
            mock.Verify(x => x.Insert(It.IsAny<AcronymModel>()), Times.Never);
            Assert.Null(result.Result);
        }
        [Fact]
        public void Insert_WhenInsertIsAValidValue_ShouldReturnValue()
        {
            Mock<ICouchDBClient> mock = new Mock<ICouchDBClient>();
            mock.Setup(x => x.Insert(It.IsAny<IAcronymModel>())).Returns(Task<ICouchDBAcronymModel>.FromResult(Mock.Of<ICouchDBAcronymModel>()));
            CouchDBStorageService storageServiceTest = new CouchDBStorageService(mock.Object);
            Task<ICouchDBAcronymModel> result = storageServiceTest.Store("Test", "Test", "Test");
            mock.Verify(x => x.Insert(It.IsAny<IAcronymModel>()), Times.Once);
            Assert.NotNull(result.Result);
        }
    }
}
