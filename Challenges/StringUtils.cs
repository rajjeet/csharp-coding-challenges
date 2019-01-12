using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using NUnit.Framework;

namespace Challenges
{
    public static class StringUtils
    {
        public static string ReverseStringIteratively(string input)
        {
            var charArray = input.ToCharArray();
            var result = new char[charArray.Length];
            for (var i = charArray.Length; i > 0; i--)
            {
                result[charArray.Length - i] = charArray[i - 1];
            }

            return new string(result);
        }

        public static char FindFirstNonRepeatingCharacter(string input)
        {
            var charArray = input.ToCharArray();
            return charArray.GroupBy(i => i).First(g => g.Count() == 1).Key;
        }

        public static bool DetectAnagram(string word1, string word2)
        {
            var word1CharArray = word1.ToLower().Trim().ToCharArray();
            var word2CharArray = word2.ToLower().Trim().ToCharArray();
            return word1CharArray.Length == word2CharArray.Length &&
                   word1CharArray.All(word1Char => word2CharArray.Contains(word1Char));
        }

        public static Dictionary<char, int> FindDuplicates(string input)
        {
            var charArray = input.ToCharArray();
            var result = new Dictionary<char, int>();
            foreach (var letter in charArray)
            {
                if (result.ContainsKey(letter))
                {
                    result[letter] = result[letter] + 1;
                }
                else
                {
                    result.Add(letter, 1);
                }
            }

            return result.Where(e => e.Value > 1).ToDictionary(i => i.Key, i => i.Value);
        }

        public static string ReverseStringRecursively(string input)
        {
            if (input.Length < 2) return input;
                                    
            return string.Concat(ReverseStringRecursively(input.Substring(1)), input[0]);
        }
    }

    public class StringUtilsTests
    {

        [Test]
        public void ReverseStringRecursively_ReversesStringUsingRecursion_WhenStringWithEvenLengthIsProvided()
        {
            const string sampleString = "cantaloupe";
            const string expected = "epuolatnac";

            var result = StringUtils.ReverseStringRecursively(sampleString);

            Assert.AreEqual(expected, result);
        }
        
        [Test]
        public void ReverseStringRecursively_ReversesStringUsingRecursion_WhenStringWithOddLengthIsProvided()
        {
            const string sampleString = "canoe";
            const string expected = "eonac";

            var result = StringUtils.ReverseStringRecursively(sampleString);

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void ReverseStringIteratively_ReversesStringUsingIteration_WhenStringIsProvided()
        {
            const string sampleString = "cantaloupe";
            const string expected = "epuolatnac";

            var result = StringUtils.ReverseStringIteratively(sampleString);

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void
            FindFirstNonRepeatingCharacter_WhenPassedStringWithNonRepeatingCharacterAsFirstCharacter_ReturnsFirstCharacter()
        {
            const string testSample = "cotton";
            const char expected = 'c';

            var result = StringUtils.FindFirstNonRepeatingCharacter(testSample);

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void
            FindFirstNonRepeatingCharacter_WhenPassedStringWithNonRepeatingCharacterAsNonFirstCharacter_ReturnsFirstNonRepeatingCharacter()
        {
            const string testSample = "assimilate";
            const char expected = 'm';

            var result = StringUtils.FindFirstNonRepeatingCharacter(testSample);

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void DetectAnagram_WhenValidAnagramWordsPassed_ShouldReturnTrue()
        {
            const string word1 = "elbow";
            const string word2 = "below";
            const bool expected = true;

            var result = StringUtils.DetectAnagram(word1, word2);

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void DetectAnagram_WhenInvalidAnagramWordsPassed_ShouldReturnFalse()
        {
            const string word1 = "horse";
            const string word2 = "below";
            const bool expected = false;

            var result = StringUtils.DetectAnagram(word1, word2);

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void DetectAnagram_WhenInvalidAnagramWordsPassedWithDifferentLengths_ShouldReturnFalse()
        {
            const string word1 = "horses";
            const string word2 = "below";
            const bool expected = false;

            var result = StringUtils.DetectAnagram(word1, word2);

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void FindDuplicates_ReturnsCorrectMapWithDuplicates_WhenProvidedString()
        {
            const string input = "Swiss Cheese";
            var expected = new Dictionary<char, int>
            {
                {'s', 3}, {'e', 3}
            };

            var result = StringUtils.FindDuplicates(input);

            Assert.AreEqual(expected, result);
        }
    }
}