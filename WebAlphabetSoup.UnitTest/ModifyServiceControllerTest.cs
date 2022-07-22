using AlphabetSoup.Models;
using AlphabetSoup.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WebAlphabetSoup.Controllers;

namespace WebAlphabetSoup.UnitTest
{
    public class ModifyServiceControllerTest
    {
        [Fact]
        public async Task ModifyServiceController_WhenModelIsNull_ShouldReturnBadRequest()
        {
            Mock<IModifyService> mock = new Mock<IModifyService>();
            mock.Setup(x => x.Edit(It.IsAny<CouchDBAcronymModel>())).Verifiable();
            ModifyServiceController modifyControllerTest = new ModifyServiceController(mock.Object);
            IActionResult result = await modifyControllerTest.PutAsync(Mock.Of<CouchDBAcronymModel>());
            mock.Verify(x => x.Edit(It.IsAny<CouchDBAcronymModel>()), Times.Never);
            Assert.NotNull(result);
            Assert.IsType<BadRequestResult>(result);
        }
        [Theory]
        [InlineData(null, null, null)]
        [InlineData("", "", "")]
        [InlineData(" ", " ", " ")]
        public async Task ModifyServiceController_WhenPropertiesIsInvalid_ShouldReturnBadRequest(string acronym, string id, string rev)
        {
            CouchDBAcronymModel nullModel = Mock.Of<CouchDBAcronymModel>(m => m.Acronym == acronym &&
            m.Id == id &&
            m.Rev == rev);
            Mock<IModifyService> mock = new Mock<IModifyService>();
            mock.Setup(x => x.Edit(It.IsAny<CouchDBAcronymModel>())).Verifiable();
            ModifyServiceController modifyControllerTest = new ModifyServiceController(mock.Object);
            IActionResult result = await modifyControllerTest.PutAsync(nullModel);
            mock.Verify(x => x.Edit(It.IsAny<CouchDBAcronymModel>()), Times.Never);
            Assert.NotNull(result);
            Assert.IsType<BadRequestResult>(result);
        }
        [Fact]
        public async Task ModifyServiceController_WhenModelIsValid_ShouldReturnOk()
        {
            CouchDBAcronymModel validModel = Mock.Of<CouchDBAcronymModel>(m => m.Acronym == "valid" &&
            m.Id == "valid" &&
            m.Rev == "valid");
            Mock<IModifyService> mock = new Mock<IModifyService>();
            mock.Setup(x => x.Edit(It.IsAny<CouchDBAcronymModel>())).Verifiable();
            ModifyServiceController modifyControllerTest = new ModifyServiceController(mock.Object);
            IActionResult result = await modifyControllerTest.PutAsync(validModel);
            mock.Verify(x => x.Edit(It.IsAny<CouchDBAcronymModel>()), Times.Once);
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
        }
    }
}