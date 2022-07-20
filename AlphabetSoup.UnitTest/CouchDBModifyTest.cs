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
            CouchDBAcronymModel inputModel = new CouchDBAcronymModel();
            inputModel.Acronym = acronym;
            inputModel.FullName = fullName;
            inputModel.Description = desc;
            inputModel.Id = It.IsAny<string>();
            inputModel.Rev = It.IsAny<string>();
            Mock<ICouchDBClient> mock = new Mock<ICouchDBClient>();
            mock.Setup(x => x.Modify(It.IsAny<CouchDBAcronymModel>())).Verifiable();
            CouchDBModifyService modifyServiceTest = new CouchDBModifyService(mock.Object);
            Task<ICouchDBAcronymModel> result = modifyServiceTest.Edit(inputModel);
            Assert.Null(result);
            mock.Verify(x => x.Modify(It.IsAny<CouchDBAcronymModel>()), Times.Never);  
        }
    }
}
