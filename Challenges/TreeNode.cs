using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Xml.Xsl;
using NUnit.Framework;

namespace Challenges
{
    public class TreeNode
    {
        public int Value { get; }
        public TreeNode Left { get; set; }
        public TreeNode Right { get; set; }

        public TreeNode(int value)
        {
            Value = value;
        }

        public static int[] PreOrder(TreeNode node)
        {
            if (node == null)
            {
                return new int[0];
            }

            var leftValues = PreOrder(node.Left);
            var rightValues = PreOrder(node.Right);
            return new int[] {node.Value}.Concat(leftValues).Concat(rightValues).ToArray();
        }

        public static int[] InOrder(TreeNode node)
        {
            if (node == null)
            {
                return new int[0];
            }

            var leftValues = InOrder(node.Left);
            var rightValues = InOrder(node.Right);

            return leftValues.Concat(new int[] {node.Value}).Concat(rightValues).ToArray();
        }

        public static int[] PostOrder(TreeNode node)
        {
            if (node == null)
            {
                return new int[0];
            }

            var leftValues = PostOrder(node.Left);
            var rightValues = PostOrder(node.Right);

            return leftValues.Concat(rightValues).Concat(new int[] {node.Value}).ToArray();
        }

        public static int[] PreOrderWithoutRecursion(TreeNode treeNode)
        {
            var stack = new Stack<TreeNode>();
            var result = new int[0];
            stack.Push(treeNode);
            while (stack.Count > 0)
            {
                var node = stack.Pop();
                result = result.Concat(new[] {node.Value}).ToArray();

                if (node.Right != null)
                    stack.Push(node.Right);

                if (node.Left != null)
                    stack.Push(node.Left);
            }

            return result;
        }

        public static int[] InOrderWithoutRecursion(TreeNode treeNode)
        {
            var stack = new Stack<TreeNode>();
            var result = new int[0];

            var node = treeNode;
            while (stack.Count > 0 || node != null)
            {
                while (node != null)
                {
                    stack.Push(node);
                    node = node.Left;
                }

                node = stack.Pop();
                result = result.Concat(new[] {node.Value}).ToArray();
                node = node.Right;
            }

            return result;
        }

        public static int[] PostOrderWithoutRecursion(TreeNode treeNode)
        {
            var stack = new Stack<TreeNode>();
            var result = new int[0];
            stack.Push(treeNode);
            while (stack.Count > 0)
            {
                var node = stack.Pop();
                result = new int[] {node.Value}.Concat(result).ToArray();

                if (node.Left != null)
                    stack.Push(node.Left);

                if (node.Right != null)
                    stack.Push(node.Right);
            }

            return result;
        }

        public static int CountLeafNodes(TreeNode node)
        {
            if (node == null)
            {
                return 0;
            }

            if (node.Left == null && node.Right == null)
            {
                return 1;
            }

            return new[] {CountLeafNodes(node.Left), CountLeafNodes(node.Right)}.Aggregate((x, y) => x + y);
        }

        public static int CountLeafNodesWithoutRecursion(TreeNode treeNode)
        {
            var stack = new Stack<TreeNode>();
            var count = 0;
            stack.Push(treeNode);
            while (stack.Count > 0)
            {
                var node = stack.Pop();
                if (node.Left == null && node.Right == null)
                {
                    count++;
                }

                if (node.Left != null)
                    stack.Push(node.Left);

                if (node.Right != null)
                    stack.Push(node.Right);
            }

            return count;
        }
    }

    [TestFixture]
    public class TreeNodeTests
    {
        private TreeNode _treeNode;

        [SetUp]
        public void SetUp()
        {
            _treeNode = new TreeNode(25)
            {
                Left = new TreeNode(15)
                {
                    Left = new TreeNode(10)
                    {
                        Left = new TreeNode(4),
                        Right = new TreeNode(12)
                    },
                    Right = new TreeNode(22)
                    {
                        Left = new TreeNode(18),
                        Right = new TreeNode(24)
                    }
                },
                Right = new TreeNode(50)
                {
                    Left = new TreeNode(35)
                    {
                        Left = new TreeNode(31),
                        Right = new TreeNode(44)
                    },
                    Right = new TreeNode(70)
                    {
                        Left = new TreeNode(66)
                        {
                            Left = new TreeNode(60)
                        },
                        Right = new TreeNode(90)
                    }
                }
            };
        }

        [Test]
        public void PreOrder_WhenCalled_ReturnsArrayWithPreOrderValues()
        {
            var expected = new int[] {25, 15, 10, 4, 12, 22, 18, 24, 50, 35, 31, 44, 70, 66, 60, 90};

            var result = TreeNode.PreOrder(_treeNode);

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void InOrder_WhenCalled_ReturnsArrayWithInOrderValues()
        {
            var expected = new int[] {4, 10, 12, 15, 18, 22, 24, 25, 31, 35, 44, 50, 60, 66, 70, 90};

            var result = TreeNode.InOrder(_treeNode);

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void PostOrder_WhenCalled_ReturnsArrayWithPostOrderValues()
        {
            var expected = new int[] {4, 12, 10, 18, 24, 22, 15, 31, 44, 35, 60, 66, 90, 70, 50, 25};

            var result = TreeNode.PostOrder(_treeNode);

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void PreOrderWithoutRecursion_WhenCalled_ReturnsArrayWithPreOrderValues()
        {
            var expected = new int[] {25, 15, 10, 4, 12, 22, 18, 24, 50, 35, 31, 44, 70, 66, 60, 90};

            var result = TreeNode.PreOrderWithoutRecursion(_treeNode);

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void InOrderWithoutRecursion_WhenCalled_ReturnsArrayWithInOrderValues()
        {
            var expected = new int[] {4, 10, 12, 15, 18, 22, 24, 25, 31, 35, 44, 50, 60, 66, 70, 90};

            var result = TreeNode.InOrderWithoutRecursion(_treeNode);

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void PostOrderWithoutRecursion_WhenCalled_ReturnsArrayWithPostOrderValues()
        {
            var expected = new int[] {4, 12, 10, 18, 24, 22, 15, 31, 44, 35, 60, 66, 90, 70, 50, 25};

            var result = TreeNode.PostOrderWithoutRecursion(_treeNode);

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void CountLeafNodes_WhenCalled_ReturnsCountOfNodesWithoutChildren()
        {
            const int expected = 8;

            var result = TreeNode.CountLeafNodes(_treeNode);

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void CountLeafNodesWithoutRecursion_WhenCalled_ReturnsCountOfNodesWithoutChildren()
        {
            const int expected = 8;

            var result = TreeNode.CountLeafNodesWithoutRecursion(_treeNode);

            Assert.AreEqual(expected, result);
        }
    }
}