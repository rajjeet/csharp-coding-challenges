using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace Challenges
{
    /*
        The Knapsack Problem
        ----------------------
        The knapsack has a capacity of 4
        There are 3 possible items to put inside
        1. Book (value: 1, capacity: 2)
        2. Pencil (value: 3, capacity: 1)
        3. Flashlight (value: 3, capacity: 2)
        
        Objective: Find the combination of items that yields the maximum total value
        Constraints: An item may only be used once           
    */

    public struct Item
    {
        public int Value { get; }
        public int Capacity { get; }

        public Item(int value, int capacity)
        {
            Value = value;
            Capacity = capacity;
        }
    }

    public static class KnapsackProblem
    {
        public static IEnumerable<Item> FindKnapsackItems(List<Item> items, int knapsackCapacity)
        {
            var result = new List<Item>();
            
            // Create a two 2-dimensional array, 1 to hold item inclusion flag and other to hold item values
            // NOTE: dimensions are items + 1 x knapsackCapacity + 1 to account for no item and no capacity
            var totalValues = new int[items.Count + 1, knapsackCapacity + 1];
            var itemInclusion = new bool[items.Count + 1, knapsackCapacity + 1];

            // Iterate through both dimensions of the array, and calculate value of including vs excluding the item
            // Including the item subtracts the remaining capacity, and the item value and sets the inclusion flag to 1
            // Excluding item uses the previous item's value and sets the inclusion flag to 0
            for (var item = 1; item <= items.Count; item++)
            {
                for (var capacity = 1; capacity <= knapsackCapacity; capacity++)
                {
                    // Value without including item
                    var previousValue = totalValues[item - 1, capacity];

                    // Value with including item
                    var includedValue = 0;
                    var remainingCapacity = capacity;
                    var currentItem = item;
                    while (remainingCapacity > 0)
                    {
                        if (items[currentItem - 1].Capacity <= remainingCapacity &&
                            (itemInclusion[currentItem, remainingCapacity] || currentItem == item))
                        {
                            includedValue += items[currentItem - 1].Value;
                            remainingCapacity -= items[currentItem - 1].Capacity;
                        }

                        currentItem--;
                        if (currentItem == 0) break;
                    }

                    if (includedValue >= previousValue && includedValue > 0)
                    {
                        totalValues[item, capacity] = includedValue;
                        itemInclusion[item, capacity] = true;
                    }
                    else
                    {
                        totalValues[item, capacity] = previousValue;
                        itemInclusion[item, capacity] = false;
                    }
                }
            }

            // Once the arrays are filled out, we start from the bottom right of the array with the highest value
            // If the item is included, we subtract it from the capacity, and move up to next item and down the capacity the item took
            // if the item is not included, we don't subtract any capacity, and move up to next item
            // As we traverse the matrix, we add the included items into the hashtable
            for (var capacity = knapsackCapacity; capacity > 0; capacity--)
            {
                for (var item = items.Count; item > 0; item--)
                {
                    if (itemInclusion[item, capacity] && capacity > items[item - 1].Capacity)
                    {
                        result.Add(items[item - 1]);
                        capacity -= items[item - 1].Capacity;
                    }
                }
            }

            return result;
        }
    }

    [TestFixture]
    public class KnapsackProblemTests
    {
        [Test]
        public void FindKnapsackItems_ReturnsValueMaximizingSetOfItems_WhenGiven3Items()
        {
            var flashLight = new Item(3, 2);
            var book = new Item(1, 2);
            var pencil = new Item(3, 1);            
            var items = new List<Item> {flashLight, book, pencil};            
            var knapsackCapacity = 4;
            var expected = new List<Item> {flashLight, pencil};

            var result = KnapsackProblem.FindKnapsackItems(items, knapsackCapacity);

            CollectionAssert.AreEquivalent(expected, result);
        }
    }
}