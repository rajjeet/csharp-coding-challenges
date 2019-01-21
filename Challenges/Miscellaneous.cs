using System.Collections.Generic;
using System.Security.Cryptography;
using NUnit.Framework;

namespace Challenges
{
    public static class Miscellaneous
    {
        public static bool FindUsingBinary(int[] array, int valueToFind, out int index)
        {
            var min = 0;
            var max = array.Length - 1;

            while (true)
            {
                var midPoint = (max + min) / 2;

                if (array[midPoint] == valueToFind)
                {
                    index = midPoint;
                    return true;
                }

                if (min == max)
                    break;

                if (array[midPoint] > valueToFind)
                {
                    max = midPoint;
                    continue;
                }

                if (array[midPoint] < valueToFind)
                {
                    min = midPoint;
                    continue;
                }


                break;
            }

            index = -1;
            return false;
        }

        public static int CalculateFibonacciRecursively(int input)
        {
            if (input < 3) return 1;
            return CalculateFibonacciRecursively(input - 1) + CalculateFibonacciRecursively(input - 2);
        }

        public static int CalculateFibonacciDynamically(int input)
        {
            var store = new Dictionary<int, int> {{1, 1}, {2, 1}};
            var index = 3;
            while (index <= input)
            {
                var numMinusOne = store[index - 1];
                var numMinusTwo = store[index - 2];
                store.Add(index, numMinusOne + numMinusTwo);    
                index++;
            }

            return store[input];
        }

        public static int CalculateFibonacciIteratively(int input)
        {            
            var result = 1;
            var numMinusTwo = 0;
            for (var index = 1; index < input; index++)
            {
                var numMinusOne = numMinusTwo;
                numMinusTwo = result;                
                result = numMinusOne + numMinusTwo;
            }

            return result;
        }
    }

    [TestFixture]
    public class MiscellaneousTests
    {
        [Test]
        public void CalculateFibonacciRecursively_WhenPassed10_Returns55()
        {
            const int expected = 55;
            var result = Miscellaneous.CalculateFibonacciRecursively(10);


            Assert.AreEqual(expected, result);
        }

        [Test]
        public void CalculateFibonacciDynamically_WhenPassed40_ReturnsCorrectResult()
        {
            const int expected = 102334155;
            
            var result = Miscellaneous.CalculateFibonacciDynamically(40);

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void CalculateFibonacciIteratively_WhenPassed40_ReturnsCorrectResult()
        {
            const int expected = 55;
            
            var result = Miscellaneous.CalculateFibonacciIteratively(10);

            Assert.AreEqual(expected, result);
        }
        
        [Test]
        public void CalculateFibonacciIteratively_WhenPassed1_ReturnsCorrectResult()
        {
            const int expected = 1;
            
            var result = Miscellaneous.CalculateFibonacciIteratively(1);

            Assert.AreEqual(expected, result);
        }
        
        [Test]
        public void CalculateFibonacciIteratively_WhenPassed2_ReturnsCorrectResult()
        {
            const int expected = 1;
            
            var result = Miscellaneous.CalculateFibonacciIteratively(2);

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void FindUsingBinarySearch_ReturnsCorrectIndex_GivenArrayWithEvenElements()
        {
            var ints = new int[] {1, 7, 6, 8, 5, 2, 4, 10, 3, 9};
            const int expected = 7;

            var result = Miscellaneous.FindUsingBinary(ints, 10, out var index);

            Assert.IsTrue(result);
            Assert.AreEqual(expected, index);
        }

        [Test]
        public void FindUsingBinarySearch_ReturnsTrueAndIndex_GivenArrayWithOddElements()
        {
            var ints = new int[] {1, 7, 6, 8, 5, 2, 4, 10, 3, 9, 11};
            const int expected = 7;

            var result = Miscellaneous.FindUsingBinary(ints, 10, out var index);

            Assert.IsTrue(result);
            Assert.AreEqual(expected, index);
        }

        [Test]
        public void FindUsingBinarySearch_ReturnsTrueAndIndex_GivenArrayWithSingleElements()
        {
            var ints = new int[] {1};
            const int expected = 0;

            var result = Miscellaneous.FindUsingBinary(ints, 1, out var index);

            Assert.IsTrue(result);
            Assert.AreEqual(expected, index);
        }

        [Test]
        public void FindUsingBinarySearch_ReturnsFalse_GivenArrayWithSingleElements()
        {
            var ints = new int[] {1};
            const int expected = 0;

            var result = Miscellaneous.FindUsingBinary(ints, 12, out var index);

            Assert.IsFalse(result);
        }
    }
}