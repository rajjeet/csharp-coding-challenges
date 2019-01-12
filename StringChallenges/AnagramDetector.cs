using System.Linq;
using NUnit.Framework;

namespace StringChallenges
{
    public static class AnagramDetector
    {
        public static bool DetectAnagram(string word1, string word2)
        {
            var word1CharArray = word1.ToLower().Trim().ToCharArray();
            var word2CharArray = word2.ToLower().Trim().ToCharArray();
            return word1CharArray.Length == word2CharArray.Length &&
                   word1CharArray.All(word1Char => word2CharArray.Contains(word1Char));
        }
    }

    [TestFixture]
    public class AnagramDetectorTest
    {
        [Test]
        public void DetectAnagram_WhenValidAnagramWordsPassed_ShouldReturnTrue()
        {
            const string word1 = "elbow";
            const string word2 = "below";
            const bool expected = true;

            var result = AnagramDetector.DetectAnagram(word1, word2);

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void DetectAnagram_WhenInvalidAnagramWordsPassed_ShouldReturnFalse()
        {
            const string word1 = "horse";
            const string word2 = "below";
            const bool expected = false;

            var result = AnagramDetector.DetectAnagram(word1, word2);

            Assert.AreEqual(expected, result);
        }
        
        [Test]
        public void DetectAnagram_WhenInvalidAnagramWordsPassedWithDifferentLengths_ShouldReturnFalse()
        {
            const string word1 = "horses";
            const string word2 = "below";
            const bool expected = false;

            var result = AnagramDetector.DetectAnagram(word1, word2);

            Assert.AreEqual(expected, result);
        }
    }
}