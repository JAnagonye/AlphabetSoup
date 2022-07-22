using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlphabetSoup.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WebAlphabetSoup.Controllers;

namespace WebAlphabetSoup.UnitTest
{
    public class StoragServiceController
    {
        [Theory]
        [InlineData(null, null, null)]
        [InlineData("", "", "")]
        [InlineData(" ", " ", " ")]
        public async Task StorageServiceController_WhenPropertiesIsInvalid_ShouldReturnBadRequest(string acronym, string fullName, string desc)
        {
            Mock<IStorageService> mock = new Mock<IStorageService>();
            mock.Setup(x => x.Store(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Verifiable();
            StorageServiceController storeControllerTest = new StorageServiceController(mock.Object);
            IActionResult result = await storeControllerTest.PostAsync(acronym, fullName, desc);
            mock.Verify(x => x.Store(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Never);
            Assert.NotNull(result);
            Assert.IsType<BadRequestResult>(result);
        }
        [Fact]
        public async Task StorageServiceController_WhenInputIsValid_ShouldReturnOk()
        {
            string acronym = "valid";
            string fullName = "validName";
            string desc = "validDesc";
            Mock<IStorageService> mock = new Mock<IStorageService>();
            mock.Setup(x => x.Store(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Verifiable();
            StorageServiceController storeControllerTest = new StorageServiceController(mock.Object);
            IActionResult result = await storeControllerTest.PostAsync(acronym, fullName, desc);
            mock.Verify(x => x.Store(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Once);
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
        }
    }
}
