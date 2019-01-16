using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Challenges;
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
    }

    public class BinarySearchTree
    {
        public TreeNode RootTreeNode { get; set; }

        public BinarySearchTree(TreeNode rootTreeNode)
        {
            if (Validate(rootTreeNode))
                RootTreeNode = rootTreeNode;
            else
                throw new ArgumentException("Invalid binary search tree provided to BinarySearchTree class");
        }

        public void Add(int value)
        {
            var node = RootTreeNode;
            while (true)
            {
                if (value > node.Value)
                {
                    if (node.Right == null)
                    {
                        node.Right = new TreeNode(value);
                    }
                    else
                    {
                        node = node.Right;
                        continue;
                    }
                }
                else if (value < node.Value)
                {
                    if (node.Left == null)
                    {
                        node.Left = new TreeNode(value);
                    }
                    else
                    {
                        node = node.Left;
                        continue;
                    }
                }

                break;
            }
        }

        public static bool Validate(TreeNode node, int min = int.MinValue, int max = int.MaxValue)
        {
            if (node == null)
            {
                return true;
            }

            if (node.Value < min || node.Value > max)
                return false;

            return Validate(node.Left, min, node.Value - 1) && Validate(node.Right, node.Value + 1, max);
        }

        public void Remove(int value)
        {
            var node = RootTreeNode;
            while (true)
            {
                if (value < node.Left?.Value)
                {
                    node = node.Left;
                    continue;
                }

                if (value > node.Right?.Value)
                {
                    node = node.Right;
                    continue;
                }

                if (value == node.Left?.Value)
                {
                    if (node.Left.Right != null)
                    {
                        node.Left = node.Left.Right;
                    }
                    else if (node.Left != null)
                    {
                        node.Left = node.Left.Left;
                    }
                    else
                    {
                        node.Left = null;
                    }
                }
                else if (value == node.Right?.Value)
                {
                    if (node.Right.Right != null)
                    {
                        var oldLeftNode = node.Right.Left;
                        node.Right = node.Right.Right;

                        var newParentNode = node.Right;
                        while (true)
                        {
                            if (newParentNode.Left == null)
                            {
                                newParentNode.Left = oldLeftNode;
                                break;
                            }

                            newParentNode = newParentNode.Left;
                        }
                    }
                    else if (node.Right.Left != null)
                    {
                        node.Right = node.Right.Left;
                    }
                    else
                    {
                        node.Right = null;
                    }
                }
                else if (value == node.Value)
                {
                    if (node.Right != null)
                    {
                        RootTreeNode = node.Right;
                        if (node.Left != null)
                        {
                            node.Right.Left = node.Left;
                        }
                    }
                    else if (node.Left != null)
                    {
                        RootTreeNode = node.Left;
                    }
                }

                break;
            }
        }
    }


    public class BinarySearchTreeTests
    {
        [Test]
        public void Remove_RemovesRightLeafNodeWithNoChildren_Removes()
        {
            var leftNode = new TreeNode(25);
            var rightNode = new TreeNode(75);
            const int rootTreeNodeValue = 50;
            var rootTreeNode = new TreeNode(rootTreeNodeValue) {Left = leftNode, Right = rightNode};
            var binarySearchTree = new BinarySearchTree(rootTreeNode);

            binarySearchTree.Remove(rightNode.Value);

            Assert.IsNull(binarySearchTree.RootTreeNode.Right);
            Assert.AreEqual(leftNode, binarySearchTree.RootTreeNode.Left);
            Assert.AreEqual(rootTreeNodeValue, binarySearchTree.RootTreeNode.Value);
        }

        [Test]
        public void Remove_RemovesRightNodeWithChildren_RemovesNodeAndRearrangesBST()
        {
            var leftNode = new TreeNode(25);
            var oldRightNode = new TreeNode(75)
            {
                Left = new TreeNode(60),
                Right = new TreeNode(90)
                {
                    Left = new TreeNode(80),
                    Right = new TreeNode(100)
                }
            };
            const int rootTreeNodeValue = 50;
            var rootTreeNode = new TreeNode(rootTreeNodeValue) {Left = leftNode, Right = oldRightNode};
            var binarySearchTree = new BinarySearchTree(rootTreeNode);

            binarySearchTree.Remove(oldRightNode.Value);

            Assert.AreEqual(90, binarySearchTree.RootTreeNode.Right.Value);
            Assert.AreEqual(80, binarySearchTree.RootTreeNode.Right.Left.Value);
            Assert.AreEqual(100, binarySearchTree.RootTreeNode.Right.Right.Value);
            Assert.AreEqual(60, binarySearchTree.RootTreeNode.Right.Left.Left.Value);
        }

        [Test]
        public void Remove_RemovesLeftLeafNodeWithNoChildren_Removes()
        {
            var leftNode = new TreeNode(25);
            var rightNode = new TreeNode(75);
            const int rootTreeNodeValue = 50;
            var rootTreeNode = new TreeNode(rootTreeNodeValue) {Left = leftNode, Right = rightNode};
            var binarySearchTree = new BinarySearchTree(rootTreeNode);

            binarySearchTree.Remove(leftNode.Value);

            Assert.IsNull(binarySearchTree.RootTreeNode.Left);
            Assert.AreEqual(rightNode, binarySearchTree.RootTreeNode.Right);
            Assert.AreEqual(rootTreeNodeValue, binarySearchTree.RootTreeNode.Value);
        }

        [Test]
        public void Remove_RemovesRootNodeWithRightAndLeftLeaf_PromotesRightNode()
        {
            var leftNode = new TreeNode(25);
            var rightNode = new TreeNode(75);
            var rootTreeNode = new TreeNode(50) {Left = leftNode, Right = rightNode};
            var binarySearchTree = new BinarySearchTree(rootTreeNode);

            binarySearchTree.Remove(rootTreeNode.Value);

            Assert.IsNull(binarySearchTree.RootTreeNode.Right);
            Assert.AreEqual(leftNode, binarySearchTree.RootTreeNode.Left);
            Assert.AreEqual(rightNode, binarySearchTree.RootTreeNode);
        }

        [Test]
        public void Remove_RemovesRootNodeWithLeftLeaf_PromotesRightNode()
        {
            var leftNode = new TreeNode(25);
            var rootTreeNode = new TreeNode(50) {Left = leftNode};
            var binarySearchTree = new BinarySearchTree(rootTreeNode);

            binarySearchTree.Remove(rootTreeNode.Value);

            Assert.AreEqual(leftNode, binarySearchTree.RootTreeNode);
        }

        [Test]
        public void Validate_ReturnsFalse_WhenInValidBinarySearchTreeWithDepthOf1IsProvided()
        {
            var rootTreeNode = new TreeNode(50) {Left = new TreeNode(25), Right = new TreeNode(1)};

            var result = BinarySearchTree.Validate(rootTreeNode);

            Assert.IsFalse(result);
        }

        [Test]
        public void Validate_ReturnsFalse_WhenBinarySearchTreeWithInvalidNodeAtDepthOf2IsProvided()
        {
            var rootTreeNode = new TreeNode(50)
            {
                Left = new TreeNode(25),
                Right = new TreeNode(75)
                {
                    Left = new TreeNode(15),
                    Right = new TreeNode(100)
                }
            };

            var result = BinarySearchTree.Validate(rootTreeNode);

            Assert.IsFalse(result);
        }

        [Test]
        public void Validate_ReturnsTrue_WhenBinarySearchTreeWithValidNodeAtDepthOf2IsProvided()
        {
            var rootTreeNode = new TreeNode(50)
            {
                Left = new TreeNode(25)
                {
                    Left = new TreeNode(10),
                    Right = new TreeNode(45)
                },
                Right = new TreeNode(75)
                {
                    Left = new TreeNode(55),
                    Right = new TreeNode(100)
                }
            };

            var result = BinarySearchTree.Validate(rootTreeNode);

            Assert.IsTrue(result);
        }

        [Test]
        public void Validate_ReturnsFalse_WhenLeftNodeWithLeftParentNodeOnLeftSideOfTreeIsInvalid()
        {
            var rootTreeNode = new TreeNode(50)
            {
                Left = new TreeNode(25)
                {
                    Left = new TreeNode(10)
                    {
                        Left = new TreeNode(11), // Invalid
                        Right = new TreeNode(20)
                    },
                    Right = new TreeNode(45)
                    {
                        Left = new TreeNode(30),
                        Right = new TreeNode(47)
                    }
                },
                Right = new TreeNode(75)
                {
                    Left = new TreeNode(55)
                    {
                        Left = new TreeNode(52),
                        Right = new TreeNode(60)
                    },
                    Right = new TreeNode(100)
                    {
                        Left = new TreeNode(80),
                        Right = new TreeNode(110)
                    }
                }
            };

            var result = BinarySearchTree.Validate(rootTreeNode);

            Assert.False(result);
        }

        [Test]
        public void Validate_ReturnsFalse_WhenLeftNodeWithRightParentNodeOnLeftSideOfTreeIsInvalid()
        {
            var rootTreeNode = new TreeNode(50)
            {
                Left = new TreeNode(25)
                {
                    Left = new TreeNode(10)
                    {
                        Left = new TreeNode(5),
                        Right = new TreeNode(20)
                    },
                    Right = new TreeNode(45)
                    {
                        Left = new TreeNode(46), //Invalid
                        Right = new TreeNode(47)
                    }
                },
                Right = new TreeNode(75)
                {
                    Left = new TreeNode(55)
                    {
                        Left = new TreeNode(52),
                        Right = new TreeNode(60)
                    },
                    Right = new TreeNode(100)
                    {
                        Left = new TreeNode(80),
                        Right = new TreeNode(110)
                    }
                }
            };

            var result = BinarySearchTree.Validate(rootTreeNode);

            Assert.False(result);
        }

        [Test]
        public void Validate_ReturnsFalse_WhenRightNodeWithLeftParentNodeOnLeftSideOfTreeIsInvalid()
        {
            var rootTreeNode = new TreeNode(50)
            {
                Left = new TreeNode(25)
                {
                    Left = new TreeNode(10)
                    {
                        Left = new TreeNode(5),
                        Right = new TreeNode(26) //Invalid
                    },
                    Right = new TreeNode(45)
                    {
                        Left = new TreeNode(30),
                        Right = new TreeNode(47)
                    }
                },
                Right = new TreeNode(75)
                {
                    Left = new TreeNode(55)
                    {
                        Left = new TreeNode(52),
                        Right = new TreeNode(60)
                    },
                    Right = new TreeNode(100)
                    {
                        Left = new TreeNode(80),
                        Right = new TreeNode(110)
                    }
                }
            };

            var result = BinarySearchTree.Validate(rootTreeNode);

            Assert.False(result);
        }

        [Test]
        public void Validate_ReturnsFalse_WhenRightNodeWithRightParentNodeOnLeftSideOfTreeIsInvalid()
        {
            var rootTreeNode = new TreeNode(50)
            {
                Left = new TreeNode(25)
                {
                    Left = new TreeNode(10)
                    {
                        Left = new TreeNode(5),
                        Right = new TreeNode(20)
                    },
                    Right = new TreeNode(45)
                    {
                        Left = new TreeNode(30),
                        Right = new TreeNode(51) //Invalid
                    }
                },
                Right = new TreeNode(75)
                {
                    Left = new TreeNode(55)
                    {
                        Left = new TreeNode(52),
                        Right = new TreeNode(60)
                    },
                    Right = new TreeNode(100)
                    {
                        Left = new TreeNode(80),
                        Right = new TreeNode(110)
                    }
                }
            };

            var result = BinarySearchTree.Validate(rootTreeNode);

            Assert.False(result);
        }

        [Test]
        public void Validate_ReturnsFalse_WhenLeftNodeWithLeftParentNodeOnRightSideOfTreeIsInvalid()
        {
            var rootTreeNode = new TreeNode(50)
            {
                Left = new TreeNode(25)
                {
                    Left = new TreeNode(10)
                    {
                        Left = new TreeNode(5),
                        Right = new TreeNode(20)
                    },
                    Right = new TreeNode(45)
                    {
                        Left = new TreeNode(30),
                        Right = new TreeNode(47)
                    }
                },
                Right = new TreeNode(75)
                {
                    Left = new TreeNode(55)
                    {
                        Left = new TreeNode(49), //Invalid
                        Right = new TreeNode(60)
                    },
                    Right = new TreeNode(100)
                    {
                        Left = new TreeNode(80),
                        Right = new TreeNode(110)
                    }
                }
            };

            var result = BinarySearchTree.Validate(rootTreeNode);

            Assert.False(result);
        }

        [Test]
        public void Validate_ReturnsFalse_WhenLeftNodeWithRightParentNodeOnRightSideOfTreeIsInvalid()
        {
            var rootTreeNode = new TreeNode(50)
            {
                Left = new TreeNode(25)
                {
                    Left = new TreeNode(10)
                    {
                        Left = new TreeNode(5),
                        Right = new TreeNode(20)
                    },
                    Right = new TreeNode(45)
                    {
                        Left = new TreeNode(30),
                        Right = new TreeNode(47)
                    }
                },
                Right = new TreeNode(75)
                {
                    Left = new TreeNode(55)
                    {
                        Left = new TreeNode(52),
                        Right = new TreeNode(60)
                    },
                    Right = new TreeNode(100)
                    {
                        Left = new TreeNode(74), //Invalid
                        Right = new TreeNode(110)
                    }
                }
            };

            var result = BinarySearchTree.Validate(rootTreeNode);

            Assert.False(result);
        }

        [Test]
        public void Validate_ReturnsFalse_WhenRightNodeWithLeftParentNodeOnRightSideOfTreeIsInvalid()
        {
            var rootTreeNode = new TreeNode(50)
            {
                Left = new TreeNode(25)
                {
                    Left = new TreeNode(10)
                    {
                        Left = new TreeNode(5),
                        Right = new TreeNode(20)
                    },
                    Right = new TreeNode(45)
                    {
                        Left = new TreeNode(30),
                        Right = new TreeNode(47)
                    }
                },
                Right = new TreeNode(75)
                {
                    Left = new TreeNode(55)
                    {
                        Left = new TreeNode(52),
                        Right = new TreeNode(76) //Invalid
                    },
                    Right = new TreeNode(100)
                    {
                        Left = new TreeNode(80),
                        Right = new TreeNode(110)
                    }
                }
            };

            var result = BinarySearchTree.Validate(rootTreeNode);

            Assert.False(result);
        }

        [Test]
        public void Validate_ReturnsFalse_WhenRightNodeWithRightParentNodeOnRightSideOfTreeIsInvalid()
        {
            var rootTreeNode = new TreeNode(50)
            {
                Left = new TreeNode(25)
                {
                    Left = new TreeNode(10)
                    {
                        Left = new TreeNode(5),
                        Right = new TreeNode(20)
                    },
                    Right = new TreeNode(45)
                    {
                        Left = new TreeNode(30),
                        Right = new TreeNode(47)
                    }
                },
                Right = new TreeNode(75)
                {
                    Left = new TreeNode(55)
                    {
                        Left = new TreeNode(52),
                        Right = new TreeNode(60)
                    },
                    Right = new TreeNode(100)
                    {
                        Left = new TreeNode(80),
                        Right = new TreeNode(99) //Invalid
                    }
                }
            };

            var result = BinarySearchTree.Validate(rootTreeNode);

            Assert.False(result);
        }

        [Test]
        public void Validate_ReturnsTrue_WhenValidBinarySearchTreeWithDepthOf1IsProvided()
        {
            var rootTreeNode = new TreeNode(50) {Left = new TreeNode(25), Right = new TreeNode(75)};
            var binarySearchTree = new BinarySearchTree(rootTreeNode);

            var result = BinarySearchTree.Validate(rootTreeNode);

            Assert.IsTrue(result);
        }

        [Test]
        public void Add_InsertsNodeToRightSideOfRoot_WhenTreeIncludesOnlyRootNodeAndNodeValueIsGreaterThanRoot()
        {
            var rootTreeNode = new TreeNode(10);
            var binarySearchTree = new BinarySearchTree(rootTreeNode);
            var expected = 20;

            binarySearchTree.Add(expected);

            Assert.AreEqual(expected, binarySearchTree.RootTreeNode.Right.Value);
        }

        [Test]
        public void Add_InsertsNodeToLeftSideOfRoot_WhenTreeIncludesOnlyRootNodeAndNodeValueIsLessThanRoot()
        {
            var rootTreeNode = new TreeNode(10);
            var binarySearchTree = new BinarySearchTree(rootTreeNode);
            var expected = 5;

            binarySearchTree.Add(expected);

            Assert.AreEqual(expected, binarySearchTree.RootTreeNode.Left.Value);
        }

        [Test]
        public void Add_InsertsLeftNodeTo3rdLayer_WhenTreeRootNodeHasChildren()
        {
            var rootTreeNode = new TreeNode(10);
            var node1 = new TreeNode(5);
            rootTreeNode.Left = node1;
            var binarySearchTree = new BinarySearchTree(rootTreeNode);
            var expected = 3;

            binarySearchTree.Add(expected);

            Assert.AreEqual(expected, binarySearchTree.RootTreeNode.Left.Left.Value);
        }

        [Test]
        public void Add_InsertsRightNodeTo3rdLayer_WhenTreeRootNodeHasChildren()
        {
            var rootTreeNode = new TreeNode(10);
            var node1 = new TreeNode(5);
            rootTreeNode.Left = node1;
            var binarySearchTree = new BinarySearchTree(rootTreeNode);
            var expected = 7;

            binarySearchTree.Add(expected);

            Assert.AreEqual(expected, binarySearchTree.RootTreeNode.Left.Right.Value);
        }

        [Test]
        public void Add_LinksNodeToCorrectParent_WhenNodeWithDepthOf2HasBeenAddedOnRight()
        {
            var rootTreeNode = new TreeNode(10);
            var node1 = new TreeNode(5);
            rootTreeNode.Left = node1;
            var binarySearchTree = new BinarySearchTree(rootTreeNode);
            var expected = 1;

            binarySearchTree.Add(expected);

            Assert.AreEqual(expected, binarySearchTree.RootTreeNode.Left.Left.Value);
        }

        [Test]
        public void Add_LinksNodeToCorrectParent_WhenNodeWithDepthOf2HasBeenAddedOnLeft()
        {
            var rootTreeNode = new TreeNode(10);
            var node1 = new TreeNode(5);
            rootTreeNode.Left = node1;
            var binarySearchTree = new BinarySearchTree(rootTreeNode);
            var expected = 7;

            binarySearchTree.Add(expected);

            Assert.AreEqual(expected, binarySearchTree.RootTreeNode.Left.Right.Value);
        }

        [Test]
        public void Add_RunsWithoutError_WhenNodeWithExistingValueIsPassed()
        {
            var rootTreeNode = new TreeNode(10);
            var node1 = new TreeNode(5);
            rootTreeNode.Left = node1;
            var binarySearchTree = new BinarySearchTree(rootTreeNode);
            var expected = 5;

            binarySearchTree.Add(expected);

            Assert.AreEqual(expected, binarySearchTree.RootTreeNode.Left.Value);
            Assert.IsNull(binarySearchTree.RootTreeNode.Left.Left);
            Assert.IsNull(binarySearchTree.RootTreeNode.Left.Right);
        }
    }
}