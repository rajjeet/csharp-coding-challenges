using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
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

        public static bool ContainsOnlyDigits(string input)
        {
            var charArray = input.ToCharArray();
            return charArray.All(c => c >= '0' && c <= '9');
        }

        public static bool ContainsOnlyDigitsUsingRegex(string input)
        {
            const string expr = @"^\d+$";
            return Regex.IsMatch(input, expr);
        }

        public static int CountVowels(string input)
        {
            var charArray = input.ToLower().ToCharArray();
            var vowels = new char[] {'a', 'e', 'i', 'o', 'u'};
            return charArray.Count(c => vowels.Contains(c));
        }

        public static int CountConstants(string input)
        {
            var charArray = input.ToLower().ToCharArray();
            var vowels = new char[] {'a', 'e', 'i', 'o', 'u'};
            return charArray.Count(c => !vowels.Contains(c));
        }

        public static IEnumerable<string> FindPermutations(string input)
        {
            var result = new List<string>();

            FindPermutationsRaw("", input, result);

            result.Sort();
            return result;
        }

        private static void FindPermutationsRaw(string permutation, string input, ICollection<string> result)
        {
            if (input == "")
            {
                result.Add(permutation + input);
            }
            else
            {
                for (var i = 0; i < input.Length; i++)
                {
                    var selectedCharacter = input[i];
                    var lengthAfterSelectedCharacter = input.Length - i - 1;
                    var remainingCharacters =
                        input.Substring(0, i) + input.Substring(i + 1, lengthAfterSelectedCharacter);
                    var permutationAppend = permutation + selectedCharacter;
                    FindPermutationsRaw(permutationAppend, remainingCharacters, result);
                }
            }
        }

        public static string ReverseWords(string input)
        {
            var words = input.Split(' ');
            var result = "";
            for (var i = words.Length - 1; i >= 0; i--)
            {
                if (!string.IsNullOrEmpty(words[i]))
                {
                    result += $" {words[i].Trim()}";
                }
            }

            return result.Trim();
        }

        public static bool CheckRotation(string string1, string string2)
        {
            if (string1.Length != string2.Length)
            {
                return false;
            }
            
            var rotatedString = string1;
            foreach (var iteration in string1)
            {
                rotatedString = RotateString(rotatedString);
                if (rotatedString == string2)
                {
                    return true;
                }
            }

            return false;
        }

        public static string RotateString(string input)
        {
            return input.Substring(input.Length - 1, 1) + input.Substring(0, input.Length - 1);
        }

        public static bool IsPalindrome(string input)
        {
            var reverseString = ReverseStringRecursively(input);
            return reverseString == input;
        }
    }

    public class StringUtilsTests
    {
        
        
        [Test]
        public void IsPalindrome_ReturnsFalse_WhenAPalindromeStringIsNotPassed()
        {
            const string input = "mommy";

            var result = StringUtils.IsPalindrome(input);
            
            Assert.IsFalse(result);
            
        }
        
        [Test]
        public void IsPalindrome_ReturnsTrue_WhenAPalindromeStringIsPassed()
        {
            const string input = "mom";

            var result = StringUtils.IsPalindrome(input);
            
            Assert.IsTrue(result);
            
        }
        
        [Test]
        public void RotateString_ReturnsStringRotatedToLeftBy1_WhenStringPassed()
        {
            const string input = "canoe";
            const string expected = "ecano";

            var result = StringUtils.RotateString(input);
            
            Assert.AreEqual(expected, result);
        }
        
        [Test]
        public void CheckRotation_ReturnsTrue_WhenTwoStringsArePassedThatAreRotationsOfOneAnother()
        {
            const string string1 = "IndiaUSAEngland";
            const string string2 = "USAEnglandIndia";

            var result = StringUtils.CheckRotation(string1, string2);
            
            Assert.IsTrue(result);
        }        
        
        [Test]
        public void CheckRotation_ReturnsTrue_WhenTwoStringsArePassedThatAreRotationsOfOneAnotherWithOnlyACharacterDifferenceAtTheEndOfRotation()
        {
            const string string1 = "SAEnglandIndiaU";
            const string string2 = "USAEnglandIndia";

            var result = StringUtils.CheckRotation(string1, string2);
            
            Assert.IsTrue(result);
        }
        
        [Test]
        public void CheckRotation_ReturnsFalse_WhenTwoStringsArePassedThatAreNotRotationsOfOneAnother()
        {
            const string string1 = "IndiaUSAEngland";
            const string string2 = "USAEnglandCanada";

            var result = StringUtils.CheckRotation(string1, string2);
            
            Assert.IsFalse(result);
        }
        
        [Test]
        public void ReverseWords_ReturnsStringWithReversedWords_WhenStringWithMultipleWordsIsPassed()
        {
            const string input = "This is a sample string";
            const string expected = "string sample a is This";

            var result = StringUtils.ReverseWords(input);

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void
            ReverseWords_ReturnsStringWithReversedWordsWithoutExtraWhitespace_WhenStringWithMultipleWordsWithExtraWhitespaceInBetweenIsPassed()
        {
            const string input = "This is a     sample    string";
            const string expected = "string sample a is This";

            var result = StringUtils.ReverseWords(input);

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void ReverseWords_ReturnsSameInputWithTrimmedWhitespace_WhenSingleWordIsPassed()
        {
            const string input = "  Sample ";
            const string expected = "Sample";

            var result = StringUtils.ReverseWords(input);

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void FindPermutations_ReturnsListOfPermutations_GivenValidStringWith3Letters()
        {
            const string input = "car";
            var expected = new List<string> {"car", "acr", "rca", "cra", "rac", "arc"};
            expected.Sort();

            var result = StringUtils.FindPermutations(input);

            CollectionAssert.AreEqual(expected, result);
        }

        [Test]
        public void FindPermutations_ReturnsListOfPermutations_GivenValidStringWith4Letters()
        {
            const string input = "care";
            var expected = new List<string>
            {
                "acer", "acre", "aecr", "aerc", "arce", "arec", "caer", "care", "cear", "cera", "crae", "crea", "eacr",
                "earc", "ecar", "ecra", "erac", "erca", "race", "raec", "rcae", "rcea", "reac", "reca"
            };
            expected.Sort();

            var result = StringUtils.FindPermutations(input);

            foreach (var t in result)
            {
                Console.WriteLine(t);
            }

            CollectionAssert.AreEqual(expected, result);
        }

        [Test]
        public void FindPermutations_ReturnsListOfTwoPermutations_GivenAStringWithTwoCharacter()
        {
            const string input = "at";
            var expected = new List<string> {"at", "ta"};

            var result = StringUtils.FindPermutations(input);

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void FindPermutations_ReturnsListOfSinglePermutation_GivenAStringWithSingleCharacter()
        {
            const string input = "a";
            var expected = new List<string> {"a"};

            var result = StringUtils.FindPermutations(input);

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void CountConstants_ReturnsConstantCount_WhenValidStringIsProvided()
        {
            const string input = "Awesomeness";
            const int expected = 6;

            var result = StringUtils.CountConstants(input);

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void CountVowels_ReturnsVowelCount_WhenValidStringIsProvided()
        {
            const string input = "Awesomeness";
            const int expected = 5;

            var result = StringUtils.CountVowels(input);

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void ContainsOnlyDigitsUsingRegex_ReturnsTrue_WhenStringWithOnlyDigitsIsProvided()
        {
            const string input = "123456";
            const bool expected = true;

            var result = StringUtils.ContainsOnlyDigitsUsingRegex(input);

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void ContainsOnlyDigitsUsingRegex_ReturnsFalse_WhenStringWitDigitsAndAlphabetsIsProvided()
        {
            const string input = "123456a";
            const bool expected = false;

            var result = StringUtils.ContainsOnlyDigitsUsingRegex(input);

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void ContainsOnlyDigits_ReturnsTrue_WhenStringWithOnlyDigitsIsProvided()
        {
            const string input = "123456";
            const bool expected = true;

            var result = StringUtils.ContainsOnlyDigits(input);

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void ContainsOnlyDigits_ReturnsFalse_WhenStringWitDigitsAndAlphabetsIsProvided()
        {
            const string input = "123456a";
            const bool expected = false;

            var result = StringUtils.ContainsOnlyDigits(input);

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void ReverseStringRecursively_ReversesStringUsingRecursion_WhenStringWithEvenLengthIsProvided()
        {
            const string input = "cantaloupe";
            const string expected = "epuolatnac";

            var result = StringUtils.ReverseStringRecursively(input);

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void ReverseStringRecursively_ReversesStringUsingRecursion_WhenStringWithOddLengthIsProvided()
        {
            const string input = "canoe";
            const string expected = "eonac";

            var result = StringUtils.ReverseStringRecursively(input);

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void ReverseStringIteratively_ReversesStringUsingIteration_WhenStringIsProvided()
        {
            const string input = "cantaloupe";
            const string expected = "epuolatnac";

            var result = StringUtils.ReverseStringIteratively(input);

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