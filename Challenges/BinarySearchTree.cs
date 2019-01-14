using Newtonsoft.Json.Bson;
using NUnit.Framework;

namespace Challenges
{
    public class BinarySearchTree
    {
        public TreeNode RootTreeNode { get; set; }

        public BinarySearchTree(TreeNode rootTreeNode)
        {
            RootTreeNode = rootTreeNode;
        }


        public void Add(TreeNode nodeToAdd)
        {
            var parentNode = RootTreeNode;
            while (true)
            {
                if (nodeToAdd.Value > parentNode.Value)
                {
                    if (parentNode.Right == null)
                        parentNode.Right = nodeToAdd;
                    else
                    {
                        parentNode = parentNode.Right;
                        continue;
                    }
                }
                else if (nodeToAdd.Value < parentNode.Value)
                {
                    if (parentNode.Left == null)
                        parentNode.Left = nodeToAdd;
                    else
                    {
                        parentNode = parentNode.Left;
                        continue;
                    }
                }

                break;
            }
        }
    }

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


    public class BinarySearchTreeTests
    {
        [Test]
        public void Add_InsertsNodeToRightSideOfRoot_WhenTreeIncludesOnlyRootNodeAndNodeValueIsGreaterThanRoot()
        {
            var rootTreeNode = new TreeNode(10);
            var binarySearchTree = new BinarySearchTree(rootTreeNode);
            var nodeToAdd = new TreeNode(20);

            binarySearchTree.Add(nodeToAdd);

            Assert.AreEqual(nodeToAdd, binarySearchTree.RootTreeNode.Right);
        }

        [Test]
        public void Add_InsertsNodeToLeftSideOfRoot_WhenTreeIncludesOnlyRootNodeAndNodeValueIsLessThanRoot()
        {
            var rootTreeNode = new TreeNode(10);
            var binarySearchTree = new BinarySearchTree(rootTreeNode);
            var nodeToAdd = new TreeNode(5);

            binarySearchTree.Add(nodeToAdd);

            Assert.AreEqual(nodeToAdd, binarySearchTree.RootTreeNode.Left);
        }

        [Test]
        public void Add_InsertsLeftNodeTo3rdLayer_WhenTreeRootNodeHasChildren()
        {
            var rootTreeNode = new TreeNode(10);
            var node1 = new TreeNode(5);
            rootTreeNode.Left = node1;
            var binarySearchTree = new BinarySearchTree(rootTreeNode);

            var nodeToAdd = new TreeNode(3);
            binarySearchTree.Add(nodeToAdd);

            Assert.AreEqual(nodeToAdd, binarySearchTree.RootTreeNode.Left.Left);
        }
        
        [Test]
        public void Add_InsertsRightNodeTo3rdLayer_WhenTreeRootNodeHasChildren()
        {
            var rootTreeNode = new TreeNode(10);
            var node1 = new TreeNode(5);
            rootTreeNode.Left = node1;
            var binarySearchTree = new BinarySearchTree(rootTreeNode);

            var nodeToAdd = new TreeNode(7);
            binarySearchTree.Add(nodeToAdd);

            Assert.AreEqual(nodeToAdd, binarySearchTree.RootTreeNode.Left.Right);
        }
        
        [Test]
        public void Add_RunsWithoutError_WhenNodeWithExistingValueIsPassed()
        {
            var rootTreeNode = new TreeNode(10);
            var node1 = new TreeNode(5);
            rootTreeNode.Left = node1;
            var binarySearchTree = new BinarySearchTree(rootTreeNode);

            var nodeToAdd = new TreeNode(5);
            binarySearchTree.Add(nodeToAdd);

            Assert.AreEqual(node1, binarySearchTree.RootTreeNode.Left);
            Assert.IsNull(binarySearchTree.RootTreeNode.Left.Left);
            Assert.IsNull(binarySearchTree.RootTreeNode.Left.Right);
        }
    }
}