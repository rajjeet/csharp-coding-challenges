using System;
using System.Collections.Generic;
using System.Linq;
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
            throw new NotImplementedException();
        }
    }


    public class BinarySearchTreeTests
    {
        [Test]
        public void Remove_RemovesLeafNode_WhenExists()
        {
            var rootTreeNode = new TreeNode(50) {Left = new TreeNode(25), Right = new TreeNode(1)};
            var binarySearchTree = new BinarySearchTree(rootTreeNode);

            binarySearchTree.Remove(1);
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

            Assert.AreEqual(expected,binarySearchTree.RootTreeNode.Left.Right.Value);
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