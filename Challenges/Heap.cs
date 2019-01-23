using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;

namespace Challenges
{
    public class MinHeap
    {
        private readonly Node _node;

        private class Node
        {
            private Node _left;
            private Node _right;
            public int Value { get; set; }

            public Node Left
            {
                get => _left;
                set
                {
                    if (value != null)
                    {
                        _left = value;
                        _left.Parent = this;
                    }
                    else
                    {
                        _left = null;
                    }
                }
            }

            public Node Right
            {
                get => _right;
                set
                {
                    if (value != null)
                    {
                        _right = value;
                        _right.Parent = this;
                    }
                    else
                    {
                        _right = null;
                    }
                }
            }

            public Node Parent { get; set; }

            public Node(int value)
            {
                Value = value;
            }
        }

        public MinHeap(int value)
        {
            _node = new Node(value);
        }

        public void Add(int value)
        {
            var addedNode = AddToBottom(value);
            ReorganizeTree(addedNode);
        }

        private static void ReorganizeTree(Node addedNode)
        {
            var child = addedNode;
            while (true)
            {
                if (child.Parent?.Value > child.Value)
                {
                    var temp = child.Value;
                    child.Value = child.Parent.Value;
                    child.Parent.Value = temp;
                    child = child.Parent;
                    continue;
                }

                break;
            }
        }


        private Node AddToBottom(int value)
        {
            var queue = new Queue<Node>();
            queue.Enqueue(_node);

            while (queue.Count > 0)
            {
                var node = queue.Dequeue();

                if (node.Left != null)
                {
                    queue.Enqueue(node.Left);
                }
                else
                {
                    var addedNode = node.Left = new Node(value);
                    return addedNode;
                }

                if (node.Right != null)
                {
                    queue.Enqueue(node.Right);
                }
                else
                {
                    var addedNode = node.Right = new Node(value);
                    return addedNode;
                }
            }

            return null;
        }

        public int Remove()
        {
            var result = GetMin();
            MoveLastElementToTop();
            ReorganizeTreeFromTop();
            return result;
        }

        private void ReorganizeTreeFromTop()
        {
            var node = _node;
            while (true)
            {
                if (node.Left?.Value < node.Value && node.Left?.Value < node.Right?.Value)
                {
                    var temp = node.Value;
                    node.Value = node.Left.Value;
                    node.Left.Value = temp;
                    node = node.Left;
                    continue;
                }

                if (node.Right?.Value < node.Value && node.Right?.Value < node.Left?.Value)
                {
                    var temp = node.Value;
                    node.Value = node.Right.Value;
                    node.Right.Value = temp;
                    node = node.Right;
                    continue;
                }

                break;
            }
        }

        private void MoveLastElementToTop()
        {
            var queue = new Queue<Node>();
            queue.Enqueue(_node);

            while (true)
            {
                var node = queue.Dequeue();

                if (node.Left != null)
                    queue.Enqueue(node.Left);

                if (node.Right != null)
                    queue.Enqueue(node.Right);

                if (queue.Count == 0)
                {
                    _node.Value = node.Value;
                    var parent = node.Parent;
                    if (parent.Left == node)
                        parent.Left = null;
                    else if (parent.Right == node)
                        parent.Right = null;

                    break;
                }
            }
        }

        public int GetMin()
        {
            return _node.Value;
        }
    }

    public class MinHeapArray<T> where T : IComparable
    {
        private readonly List<T> _list;

        public MinHeapArray(T value)
        {
            _list = new List<T> {value};
        }

        public void Add(T value)
        {
            _list.Add(value);

            var index = _list.Count - 1;
            while (true)
            {
                var child = _list[index];
                var parentIndex = Math.Abs(index - 1) / 2;
                var parent = _list[parentIndex];
                if (child.CompareTo(parent) < 0)
                {
                    var temp = parent;
                    _list[parentIndex] = child;
                    _list[index] = temp;
                    index = parentIndex;
                    continue;
                }

                break;
            }
        }

        public T GetMin()
        {
            return _list[0];
        }

        public T Remove()
        {
            var result = GetMin();
            var lastElement = _list.Count - 1;
            _list[0] = _list[lastElement];
            _list.RemoveAt(lastElement);

            var parentIndex = 0;
            while (true)
            {
                var leftChildIndex = parentIndex * 2 + 1;
                var rightChildIndex = parentIndex * 2 + 2;

                if (leftChildIndex < _list.Count)
                {
                    if (IsParentGreaterThanChild(parentIndex, leftChildIndex) &&
                        IsChildLowerThanSibling(leftChildIndex, rightChildIndex))
                    {
                        Swap(leftChildIndex, parentIndex);
                        parentIndex = leftChildIndex;
                        continue;
                    }
                }

                if (rightChildIndex < _list.Count)
                {
                    
                    if (IsParentGreaterThanChild(parentIndex, rightChildIndex) &&
                        IsChildLowerThanSibling(rightChildIndex, leftChildIndex))
                    {
                        Swap(rightChildIndex, parentIndex);
                        parentIndex = rightChildIndex;
                        continue;
                    }
                }

                break;
            }

            return result;
        }

        private void Swap(int index1, int index2)
        {
            var temp = _list[index1];
            _list[index1] = _list[index2];
            _list[index2] = temp;
        }

        private bool IsChildLowerThanSibling(int childIndex, int siblingIndex)
        {
            return siblingIndex >= _list.Count || _list[childIndex].CompareTo(_list[siblingIndex]) < 0;
        }

        private bool IsParentGreaterThanChild(int parentIndex, int childIndex)
        {
            return _list[parentIndex].CompareTo(_list[childIndex]) > 0;
        }
    }

    [TestFixture]
    public class MinHeapTests
    {
        [Test]
        public void Add_AddsNode_WhenTreeConsistsOfSingleNode()
        {
            var minHeap = new MinHeap(3);
            minHeap.Add(2);

            var result = minHeap.GetMin();

            Assert.AreEqual(2, result);
        }

        [Test]
        public void Add_AddsNode_WhenTreeConsistsOfMultipleNodes()
        {
            var minHeap = new MinHeap(11);
            minHeap.Add(10);
            minHeap.Add(7);
            minHeap.Add(8);
            minHeap.Add(4);
            minHeap.Add(9);
            minHeap.Add(3);
            minHeap.Add(5);

            var result = minHeap.GetMin();

            Assert.AreEqual(3, result);
        }

        [Test]
        public void Remove_RemovesNodeWithLowestValue_WhenCalled()

        {
            var minHeap = new MinHeap(11);
            minHeap.Add(10);
            minHeap.Add(7);
            minHeap.Add(8);
            minHeap.Add(4);
            minHeap.Add(9);
            minHeap.Add(3);
            minHeap.Add(5);

            var result1 = minHeap.Remove();
            var result2 = minHeap.Remove();
            minHeap.Add(1);
            var result3 = minHeap.Remove();

            Assert.AreEqual(3, result1);
            Assert.AreEqual(4, result2);
            Assert.AreEqual(1, result3);
        }
    }

    public class MinHeapArrayTests
    {
        [Test]
        public void Add_AddsNode_WhenTreeConsistsOfSingleNode()
        {
            var minHeap = new MinHeapArray<int>(3);
            minHeap.Add(2);

            var result = minHeap.GetMin();

            Assert.AreEqual(2, result);
        }

        [Test]
        public void Add_AddsNode_WhenTreeConsistsOfMultipleNodes()
        {
            var minHeap = new MinHeapArray<int>(11);
            minHeap.Add(10);
            minHeap.Add(7);
            minHeap.Add(8);
            minHeap.Add(4);
            minHeap.Add(9);
            minHeap.Add(3);
            minHeap.Add(5);

            var result = minHeap.GetMin();

            Assert.AreEqual(3, result);
        }

        [Test]
        public void Remove_RemovesNodeWithLowestValue_WhenCalled()

        {
            var minHeap = new MinHeapArray<int>(11);
            minHeap.Add(10);
            minHeap.Add(7);
            minHeap.Add(8);
            minHeap.Add(4);
            minHeap.Add(9);
            minHeap.Add(3);
            minHeap.Add(5);

            var result1 = minHeap.Remove();
            var result2 = minHeap.Remove();
            minHeap.Add(1);
            var result3 = minHeap.Remove();

            Assert.AreEqual(3, result1);
            Assert.AreEqual(4, result2);
            Assert.AreEqual(1, result3);
        }
    }
}