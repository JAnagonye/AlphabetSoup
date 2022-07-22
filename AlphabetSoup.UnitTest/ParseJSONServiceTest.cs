using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlphabetSoup.Models;
using AlphabetSoup.Services;
using Moq;

namespace AlphabetSoup.UnitTest
{
    public class ParseJSONServiceTest
    {
        [Fact]
        public void ParseAcronymModelResponse_WhenParamIsNull_ShouldReturnNull()
        {
            ParseJSONService parseJSONServiceTest = new ParseJSONService();
            ICouchDBAcronymModel result = parseJSONServiceTest.ParseAcronymModelResponse(It.IsAny<string>(), It.IsAny<IAcronymModel>());
            Assert.Null(result);
        }
        [Fact]
        public void ParseAcronymModelResponse_WhenParamIsValid_ShouldReturnNotNull()
        {
            string requestResult = @"{""ok"": ""true"", ""id"": ""id"", ""rev"": ""rev"" }";
            AcronymModel model = Mock.Of<AcronymModel>(m => m.Acronym == "valid" &&
            m.FullName == "valid" &&
            m.Description == "valid");
            ParseJSONService parseJSONServiceTest = new ParseJSONService();
            ICouchDBAcronymModel result = parseJSONServiceTest.ParseAcronymModelResponse(requestResult, model);
            Assert.NotNull(result);
        }
        [Fact]
        public void ParseAcronymModelResponse_WhenOkIsFalse_ShouldReturnNull()
        {
            string requestResult = @"{""ok"": ""false"", ""id"": ""id"", ""rev"": ""rev"" }";
            AcronymModel model = Mock.Of<AcronymModel>(m => m.Acronym == "valid" &&
            m.FullName == "valid" &&
            m.Description == "valid");
            ParseJSONService parseJSONServiceTest = new ParseJSONService();
            ICouchDBAcronymModel result = parseJSONServiceTest.ParseAcronymModelResponse(requestResult, model);
            Assert.Null(result);
        }

        [Fact]
        public void ParsePurgeResponse_WhenParamIsNull_ShouldReturnNull()
        {
            ParseJSONService parseJSONServiceTest = new ParseJSONService();
            IPurgeResponse result = parseJSONServiceTest.ParsePurgeResponse(It.IsAny<string>(), It.IsAny<IPurgeModel>());
            Assert.Null(result);
        }

        [Fact]
        public void ParsePurgeResponse_WhenIDIsNotEqual_ShouldReturnErrorMessage()
        {
            string requestResult = @"{""purge_seq"": ""null"", ""purged"": { ""id"" :[ ""rev""] }  }";
            PurgeModel model = Mock.Of<PurgeModel>(m => m.Id == "valid" &&
            m.Rev == "valid");
            ParseJSONService parseJSONServiceTest = new ParseJSONService();
            IPurgeResponse result = parseJSONServiceTest.ParsePurgeResponse(requestResult, model);
            Assert.NotNull(result);
            Assert.NotNull(result);
            string expected = result.ErrorMessage;
            string actual = "Id was invalid.";
            Assert.Equal(expected, actual);
            bool expectedSuccess = result.IsSuccess;
            bool actualSuccess = false;
            Assert.Equal(expectedSuccess, actualSuccess);
        }

        [Fact]
        public void ParsePurgeResponse_WhenPurgedIsNull_ShouldReturnErrorMessage()
        {
            string requestResult = @"{""error"": ""bad_request"", ""reason"": ""Invalid format"" }";
            PurgeModel model = Mock.Of<PurgeModel>(m => m.Id == "invalid" &&
            m.Rev == "valid");
            ParseJSONService parseJSONServiceTest = new ParseJSONService();
            IPurgeResponse result = parseJSONServiceTest.ParsePurgeResponse(requestResult, model);
            Assert.NotNull(result);
            string expectedError = result.ErrorMessage;
            string actualError = "There was an error purging.";
            Assert.Equal(expectedError ,actualError);
            bool expectedSuccess = result.IsSuccess;
            bool actualSuccess = false;
            Assert.Equal(expectedSuccess, actualSuccess);
        }

        [Fact]
        public void ParsePurgeResponse_WhenRevIsNotValid_ShouldReturnErrorMessage()
        {
            string requestResult = @"{""purge_seq"": ""null"", ""purged"": { ""id"" :[ ""rev""] }  }";
            PurgeModel model = Mock.Of<PurgeModel>(m => m.Id == "id" &&
            m.Rev == "invalid");
            ParseJSONService parseJSONServiceTest = new ParseJSONService();
            IPurgeResponse result = parseJSONServiceTest.ParsePurgeResponse(requestResult, model);
            Assert.NotNull(result);
            string expectedError = result.ErrorMessage;
            string actualError = "Rev was invalid.";
            Assert.Equal(expectedError, actualError);
            bool expectedSuccess = result.IsSuccess;
            bool actualSuccess = false;
            Assert.Equal(expectedSuccess, actualSuccess);
        }

        [Fact]
        public void ParsePurgeResponse_WhenInputIsValid_ShouldReturnIsSucccessTrue()
        {
            string requestResult = @"{""purge_seq"": ""null"", ""purged"": { ""id"" :[ ""rev""] }  }";
            PurgeModel model = Mock.Of<PurgeModel>(m => m.Id == "id" &&
            m.Rev == "rev");
            ParseJSONService parseJSONServiceTest = new ParseJSONService();
            IPurgeResponse result = parseJSONServiceTest.ParsePurgeResponse(requestResult, model);
            Assert.NotNull(result);
            string expectedError = result.ErrorMessage;
            string? actualError = null;
            Assert.Equal(expectedError, actualError);
            bool expectedSuccess = result.IsSuccess;
            bool actualSuccess = true;
            Assert.Equal(expectedSuccess, actualSuccess);
        }
    }
}
