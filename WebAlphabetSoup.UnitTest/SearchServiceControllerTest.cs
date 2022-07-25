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
    public class SearchServiceControllerTest
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public async Task SearchServiceController_WhenSearchIsInvalid_ShouldReturnBadRequest(string search)
        {
            Mock<ISearchService> mock = new Mock<ISearchService>();
            mock.Setup(x => x.Search(It.IsAny<string>())).Verifiable();
            SearchServiceController searchControllerTest = new SearchServiceController(mock.Object);
            IActionResult result = await searchControllerTest.GetAsync(search);
            mock.Verify(x => x.Search(It.IsAny<string>()), Times.Never);
            Assert.NotNull(result);
            Assert.IsType<BadRequestResult>(result);
        }
        [Fact]
        public async Task SearchServiceController_WhenSearchIsValid_ShouldReturnOk()
        {
            string search = "Valid";
            Mock<ISearchService> mock = new Mock<ISearchService>();
            mock.Setup(x => x.Search(It.IsAny<string>())).Verifiable();
            SearchServiceController searchControllerTest = new SearchServiceController(mock.Object);
            IActionResult result = await searchControllerTest.GetAsync(search);
            mock.Verify(x => x.Search(It.IsAny<string>()), Times.Once);
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
        }
    }
}
