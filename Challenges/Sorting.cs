using System.Collections.Generic;
using NUnit.Framework;

namespace Challenges
{
    public static class Sorting
    {
        public static int[] BubbleSort(int[] array)
        {
            while (true)
            {
                var didSwap = false;
                for (var index = 0; index < array.Length - 1; index++)
                {
                    if (array[index] <= array[index + 1]) continue;
                    var temp = array[index];
                    array[index] = array[index + 1];
                    array[index + 1] = temp;
                    didSwap = true;
                }

                if (!didSwap) return array;
            }
        }
    }

    public class SortingTest
    {
        [Test]
        public void BubbleSort_SortsList_WhenArrayWithMultipleElementsIsProvided()
        {
            var array = new int[] {6, 7, 9, 3, 8, 4, 2};
            var expected = new int[] {2, 3, 4, 6, 7, 8, 9};

            var result = Sorting.BubbleSort(array);

            Assert.AreEqual(expected, result);
        }
        
        [Test]
        public void BubbleSort_SortsList_WhenArrayWithSingleElementIsProvided()
        {
            var array = new int[] {2};
            var expected = new int[] {2};

            var result = Sorting.BubbleSort(array);

            Assert.AreEqual(expected, result);
        }
        
        [Test]
        public void BubbleSort_SortsList_WhenArrayWithTwoElementIsProvided()
        {
            var array = new int[] {2, 1};
            var expected = new int[] {1, 2};

            var result = Sorting.BubbleSort(array);

            Assert.AreEqual(expected, result);
        }
        
        [Test]
        public void BubbleSort_SortsList_WhenArrayWithNoElementIsProvided()
        {
            var array = new int[] {};
            var expected = new int[] {};

            var result = Sorting.BubbleSort(array);

            Assert.AreEqual(expected, result);
        }
        
    }
}