using System;
using System.Collections.Generic;
using System.Linq;
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

        public static int[] QuickSort(int[] array)
        {
            if (array.Length < 2)
                return array;

            var pivot = array.Length / 2;
            var lowerBound = 0;
            var upperBound = array.Length - 1;

            while (lowerBound != upperBound)
            {
                while (array[pivot] > array[lowerBound])
                {
                    lowerBound++;
                }

                while (array[pivot] < array[upperBound])
                {
                    upperBound--;
                }

                if (pivot == lowerBound)
                    pivot = upperBound;
                else if (pivot == upperBound)
                    pivot = lowerBound;

                var temp = array[lowerBound];
                array[lowerBound] = array[upperBound];
                array[upperBound] = temp;
            }

            var leftSide = array.Take(pivot).ToArray();
            var middle = new[] {array[pivot]};
            var rightSide = array.Skip(pivot + 1).ToArray();

            return QuickSort(leftSide).Concat(middle).Concat(QuickSort(rightSide)).ToArray();
        }
    }

    public class SortingTest
    {
        [Test]
        public void Test()
        {
            var array = new int[] {6, 7, 9, 3, 8, 4, 2};
            foreach (var i in array.Skip(4))
            {
                Console.WriteLine(i);
            }
        }

        [Test]
        public void QuickSort_SortsList_WhenArrayWithOldElementsIsProvided()
        {
            var array = new int[] {6, 7, 9, 3, 8, 4, 2};
            var expected = new int[] {2, 3, 4, 6, 7, 8, 9};

            var result = Sorting.QuickSort(array);

            Assert.AreEqual(expected, result);
        }
        
        [Test]
        public void QuickSort_SortsList_WhenArrayWithEvenElementsIsProvided()
        {
            var array = new int[] {6, 7, 9, 3, 8, 4, 2, 1};
            var expected = new int[] {1, 2, 3, 4, 6, 7, 8, 9};

            var result = Sorting.QuickSort(array);

            Assert.AreEqual(expected, result);
        }
        
        [Test]
        public void QuickSort_SortsList_WhenArrayWithSingleElementsIsProvided()
        {
            var array = new int[] {1};
            var expected = new int[] {1};

            var result = Sorting.QuickSort(array);

            Assert.AreEqual(expected, result);
        }        
        
        [Test]
        public void QuickSort_SortsList_WhenArrayWithNoElementsIsProvided()
        {
            var array = new int[] {};
            var expected = new int[] {};

            var result = Sorting.QuickSort(array);

            Assert.AreEqual(expected, result);
        }        
        
        [Test]
        public void QuickSort_SortsList_WhenArrayWithTwoElementsIsProvided()
        {
            var array = new int[] {2, 1};
            var expected = new int[] {1, 2};

            var result = Sorting.QuickSort(array);

            Assert.AreEqual(expected, result);
        }

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
            var array = new int[] { };
            var expected = new int[] { };

            var result = Sorting.BubbleSort(array);

            Assert.AreEqual(expected, result);
        }
    }
}