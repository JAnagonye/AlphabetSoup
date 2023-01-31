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
    public class CharacterLimitServiceTest
    {
        [Theory]
        [InlineData("LONGACRONYM", " full", "desc ")]
        [InlineData("Acro", "Over 100 characters long                                                                                    ", "DESC ")]
        [InlineData("A", "F ", "This is an attempt to make this message over 250 characters long                               " +
            "                                             " +
            "                                                   " +
            "                                                                                                                                        ")]
        public void IsCharacterLimit_WhenValueOverMaxCharacters_ShouldReturnFalse(string acronym, string fullName, string desc)
        {
            CharacterLimitService characterLimitService = new CharacterLimitService();
            bool result = characterLimitService.IsCharacterLimit(acronym, fullName, desc);
            Assert.False(result);
        }

        [Fact]
        public void IsCharacterLimit_WhenValueIsValid_ShouldReturnTrue()
        {
            CharacterLimitService characterLimitService = new CharacterLimitService();
            bool result = characterLimitService.IsCharacterLimit("Test", "Test", "Test");
            Assert.True(result);
        }
    }
}
