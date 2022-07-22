using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlphabetSoup.Models;
using AlphabetSoup.Services;
using Moq;
using Newtonsoft.Json.Linq;

namespace AlphabetSoup.UnitTest
{
    public class ParseJSONServiceTest
    {
        [Theory]
        [InlineData(null, null)]
        [InlineData("", null)]
        [InlineData(" ", null)]
        public void ParseAcronymModelResponse_WhenParamIsNull_ShouldReturnNull(string resultString, IAcronymModel acronymModel)
        {
            ParseJSONService parseJSONServiceTest = new ParseJSONService();
            ICouchDBAcronymModel result = parseJSONServiceTest.ParseAcronymModelResponse(resultString, acronymModel);
            Assert.Null(result);
        }
        [Fact]
        public void ParseAcronymModelResponse_WhenParamIsValid_ShouldReturnNotNull()
        {
            JObject request = new JObject(
                new JProperty("ok", "true"),
                new JProperty("id", "id"),
                new JProperty("rev", "rev"));
            string requestString = request.ToString();
            AcronymModel model = Mock.Of<AcronymModel>(m => m.Acronym == "valid" &&
            m.FullName == "valid" &&
            m.Description == "valid");
            ParseJSONService parseJSONServiceTest = new ParseJSONService();
            ICouchDBAcronymModel result = parseJSONServiceTest.ParseAcronymModelResponse(requestString, model);
            Assert.NotNull(result);
        }
        [Fact]
        public void ParseAcronymModelResponse_WhenOkIsFalse_ShouldReturnNull()
        {
            JObject request = new JObject(
                new JProperty("ok", "false"),
                new JProperty("id", "id"),
                new JProperty("rev", "rev"));
            string requestString = request.ToString();
            AcronymModel model = Mock.Of<AcronymModel>(m => m.Acronym == "valid" &&
            m.FullName == "valid" &&
            m.Description == "valid");
            ParseJSONService parseJSONServiceTest = new ParseJSONService();
            ICouchDBAcronymModel result = parseJSONServiceTest.ParseAcronymModelResponse(requestString, model);
            Assert.Null(result);
        }

        [Theory]
        [InlineData(null, null)]
        [InlineData("", null)]
        [InlineData(" ", null)]
        public void ParsePurgeResponse_WhenParamIsNull_ShouldReturnNull(string resultString, IPurgeModel purgeModel)
        {
            ParseJSONService parseJSONServiceTest = new ParseJSONService();
            IPurgeResponse result = parseJSONServiceTest.ParsePurgeResponse(resultString, purgeModel);
            Assert.Null(result);
        }

        [Fact]
        public void ParsePurgeResponse_WhenIDIsNotEqual_ShouldReturnErrorMessage()
        {
            JObject request = new JObject(
                new JProperty("purge_seq", "null"),
                new JProperty("purged",
                    new JObject(
                        new JProperty("id",
                            new JArray(
                                new JObject(
                                    new JProperty("rev")))
                    ))));
            string requestString = request.ToString();
            PurgeModel model = Mock.Of<PurgeModel>(m => m.Id == "valid" &&
            m.Rev == "valid");
            ParseJSONService parseJSONServiceTest = new ParseJSONService();
            IPurgeResponse result = parseJSONServiceTest.ParsePurgeResponse(requestString, model);
            Assert.NotNull(result);
            string actual = result.ErrorMessage;
            string expected = "Id was invalid.";
            Assert.Equal(expected, actual);
            bool actualSuccess = result.IsSuccess;
            bool expectedSuccess = false;
            Assert.Equal(expectedSuccess, actualSuccess);
        }

        [Fact]
        public void ParsePurgeResponse_WhenPurgedIsNull_ShouldReturnErrorMessage()
        {
            JObject request = new JObject(
                new JProperty("error", "bad_request"),
                new JProperty("reason", "InvalidFormat"));
            string requestString = request.ToString();
            PurgeModel model = Mock.Of<PurgeModel>(m => m.Id == "invalid" &&
            m.Rev == "valid");
            ParseJSONService parseJSONServiceTest = new ParseJSONService();
            IPurgeResponse result = parseJSONServiceTest.ParsePurgeResponse(requestString, model);
            Assert.NotNull(result);
            string actualError = result.ErrorMessage;
            string expectedError = "There was an error purging.";
            Assert.Equal(expectedError ,actualError);
            bool actualSuccess = result.IsSuccess;
            bool expectedSuccess = false;
            Assert.Equal(expectedSuccess, actualSuccess);
        }

        [Fact]
        public void ParsePurgeResponse_WhenRevIsNotValid_ShouldReturnErrorMessage()
        {

            JObject request = new JObject(
                new JProperty("purge_seq", "null"),
                new JProperty("purged",
                    new JObject(
                        new JProperty("id",
                            new JArray(
                                new JObject(
                                    new JProperty("rev")))
                    ))));
            string requestString = request.ToString();
            string requestResult = @"{""purge_seq"": ""null"", ""purged"": { ""id"" :[ ""rev""] }  }";
            PurgeModel model = Mock.Of<PurgeModel>(m => m.Id == "id" &&
            m.Rev == "invalid");
            ParseJSONService parseJSONServiceTest = new ParseJSONService();
            IPurgeResponse result = parseJSONServiceTest.ParsePurgeResponse(requestString, model);
            Assert.NotNull(result);
            string actualError = result.ErrorMessage;
            string expectedError = "Rev was invalid.";
            Assert.Equal(expectedError, actualError);
            bool actualSuccess = result.IsSuccess;
            bool expectedSuccess = false;
            Assert.Equal(expectedSuccess, actualSuccess);
        }

        [Fact]
        public void ParsePurgeResponse_WhenInputIsValid_ShouldReturnIsSucccessTrue()
        {
            JObject request = new JObject(
                new JProperty("purge_seq", "null"),
                new JProperty("purged",
                    new JObject(
                        new JProperty("id",
                            new JArray(
                                new JObject(
                                    new JProperty("rev")))
                    ))));
            string requestString = request.ToString();
            string requestResult = @"{""purge_seq"": ""null"", ""purged"": { ""id"" :[ ""rev""] }  }";
            PurgeModel model = Mock.Of<PurgeModel>(m => m.Id == "id" &&
            m.Rev == "rev");
            ParseJSONService parseJSONServiceTest = new ParseJSONService();
            IPurgeResponse result = parseJSONServiceTest.ParsePurgeResponse(requestString, model);
            Assert.NotNull(result);
            string actualError = result.ErrorMessage;
            string? expectedError = null;
            Assert.Equal(expectedError, actualError);
            bool actualSuccess = result.IsSuccess;
            bool expectedSuccess = true;
            Assert.Equal(expectedSuccess, actualSuccess);
        }
    }
}
