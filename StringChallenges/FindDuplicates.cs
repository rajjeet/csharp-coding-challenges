using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace StringChallenges
{
    public class FindDuplicates
    {
        [Test]
        public void FindDuplicates_ReturnsCorrectMapWithDuplicates_WhenProvidedString()
        {
            const string input = "Swiss Cheese";
            var expected = new Dictionary<char, int>
            {
                {'s', 3}, {'e', 3}                
            };
            
            var result = FindDuplicatesMethod(input);
            
            Assert.AreEqual(expected, result);
        }

        private static Dictionary<char, int> FindDuplicatesMethod(string input)
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
}