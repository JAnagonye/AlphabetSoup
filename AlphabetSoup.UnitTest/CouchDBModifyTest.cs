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
    public class CouchDBModifyTest
    {
        [Theory]
        [InlineData(null, null, null)]
        [InlineData("", "", "")]
        [InlineData(" ", " ", " ")]
        public void Modify_WhenModifyIsEmpty_ShouldReturnNull(string acronym, string fullName, string desc)
        {
            CouchDBAcronymModel inputModel = Mock.Of<CouchDBAcronymModel>(m => m.Acronym == acronym &&
            m.FullName == fullName &&
            m.Description == desc &&
            m.Id == It.IsAny<string>() &&
            m.Rev == It.IsAny<string>());
            Mock<ICouchDBClient> mock = new Mock<ICouchDBClient>();
            Mock<ICharacterLimitService> mockLimitService = new Mock<ICharacterLimitService>();
            mock.Setup(x => x.Modify(It.IsAny<CouchDBAcronymModel>())).Verifiable();
            CouchDBModifyService modifyServiceTest = new CouchDBModifyService(mock.Object, mockLimitService.Object);
            Task<ICouchDBAcronymModel> result = modifyServiceTest.Edit(inputModel);
            Assert.Null(result.Result);
            mock.Verify(x => x.Modify(It.IsAny<CouchDBAcronymModel>()), Times.Never);  
        }

        [Fact]
        public void Modify_WhenModelIsNull_ShouldReturnNull()
        {
            Mock<ICouchDBClient> mock = new Mock<ICouchDBClient>();
            Mock<ICharacterLimitService> mockLimitService = new Mock<ICharacterLimitService>();
            mock.Setup(x => x.Modify(It.IsAny<CouchDBAcronymModel>())).Verifiable();
            CouchDBModifyService modifyServiceTest = new CouchDBModifyService(mock.Object, mockLimitService.Object);
            Task<ICouchDBAcronymModel> result = modifyServiceTest.Edit(null);
            Assert.Null(result.Result);
            mock.Verify(x => x.Modify(It.IsAny<CouchDBAcronymModel>()), Times.Never);
        }
        [Fact]
        public void Modify_WhenAcronymIsValid_ShouldReturnNull()
        {
            CouchDBAcronymModel model = Mock.Of<CouchDBAcronymModel>(m => m.Acronym == "VeryLongAcronym");
            Mock<ICouchDBClient> mock = new Mock<ICouchDBClient>();
            Mock<ICharacterLimitService> mockLimitService = new Mock<ICharacterLimitService>();
            mock.Setup(x => x.Modify(It.IsAny<CouchDBAcronymModel>())).Verifiable();
            CouchDBModifyService modifyServiceTest = new CouchDBModifyService(mock.Object, mockLimitService.Object);
            Task<ICouchDBAcronymModel> result = modifyServiceTest.Edit(model);
            Assert.Null(result.Result);
            mock.Verify(x => x.Modify(It.IsAny<CouchDBAcronymModel>()), Times.Never);
        }

        [Fact]
        public void Modify_WhenModelIsValid_ShouldReturnNotNull()
        {
            CouchDBAcronymModel model = Mock.Of<CouchDBAcronymModel>(m => m.Acronym == "acro" &&
            m.FullName ==  "fullname"&&
            m.Description == "desc" &&
            m.Id == It.IsAny<string>() &&
            m.Rev == It.IsAny<string>());
            Mock<ICouchDBClient> mock = new Mock<ICouchDBClient>();
            Mock<ICharacterLimitService> mockLimitService = new Mock<ICharacterLimitService>();
            mock.Setup(x => x.Modify(model)).Returns(Task<ICouchDBAcronymModel>.FromResult(Mock.Of<ICouchDBAcronymModel>()));
            mockLimitService.Setup(x => x.IsCharacterLimit(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Returns(true);
            CouchDBModifyService modifyServiceTest = new CouchDBModifyService(mock.Object, mockLimitService.Object);
            Task<ICouchDBAcronymModel> result = modifyServiceTest.Edit(model);
            mock.Verify(x => x.Modify(model), Times.Once);
            Assert.NotNull(result.Result);
        }
    }
}
