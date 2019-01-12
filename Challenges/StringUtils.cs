using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace Challenges
{
    public static class StringUtils
    {
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
    }

    public class StringUtilsTests
    {
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