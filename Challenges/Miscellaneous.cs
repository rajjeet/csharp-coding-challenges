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
    }

    [TestFixture]
    public class MiscellaneousTests
    {
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