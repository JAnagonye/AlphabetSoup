using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlphabetSoup.Models;
using AlphabetSoup.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WebAlphabetSoup.Controllers;

namespace WebAlphabetSoup.UnitTest
{
    public class PurgeServiceControllerTestcs
    {
        [Fact]
        public async Task PurgeServiceController_WhenModelIsNull_ShouldReturnBadRequest()
        {
            Mock<IPurgeService> mock = new Mock<IPurgeService>();
            mock.Setup(x => x.Delete(It.IsAny<IPurgeModel>())).Verifiable();
            PurgeServiceController purgeControllerTest = new PurgeServiceController(mock.Object);
            IActionResult result = await purgeControllerTest.DeleteAsync(Mock.Of<PurgeModel>());
            mock.Verify(x => x.Delete(It.IsAny<IPurgeModel>()), Times.Never);
            Assert.NotNull(result);
            Assert.IsType<BadRequestResult>(result);
        }
        [Theory]
        [InlineData(null, null)]
        [InlineData("", "")]
        [InlineData(" ", " ")]
        public async Task PurgeServiceController_WhenPropertiesIsInvalid_ShouldReturnBadRequest(string id, string rev)
        {
            PurgeModel purgeModel = Mock.Of<PurgeModel>(m => m.Id == id &&
            m.Rev == rev);
            Mock<IPurgeService> mock = new Mock<IPurgeService>();
            mock.Setup(x => x.Delete(It.IsAny<IPurgeModel>())).Verifiable();
            PurgeServiceController purgeControllerTest = new PurgeServiceController(mock.Object);
            IActionResult result = await purgeControllerTest.DeleteAsync(purgeModel);
            mock.Verify(x => x.Delete(It.IsAny<IPurgeModel>()), Times.Never);
            Assert.NotNull(result);
            Assert.IsType<BadRequestResult>(result);
        }
        [Fact]
        public async Task PurgeServiceController_WhenModelIsValid_ShouldReturnBadRequest()
        {
            PurgeModel purgeModel = Mock.Of<PurgeModel>(m => m.Id == "valid" &&
            m.Rev == "valid");
            Mock<IPurgeService> mock = new Mock<IPurgeService>();
            mock.Setup(x => x.Delete(It.IsAny<IPurgeModel>())).Verifiable();
            PurgeServiceController purgeControllerTest = new PurgeServiceController(mock.Object);
            IActionResult result = await purgeControllerTest.DeleteAsync(purgeModel);
            mock.Verify(x => x.Delete(It.IsAny<IPurgeModel>()), Times.Once);
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
        }
    }
}
