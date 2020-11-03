using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Algodat_AVLTree
{
    public class AVLTree
    {
        /// <summary>
        /// Represent this trees head node.
        /// </summary>
        public TreeNode HeadNode
        {
            get;
            set;
        }

        /// <summary>
        /// Inserts a value into the tree.
        /// </summary>
        /// <param name="value">The value that is to be inserted.</param>
        public void Insert(int value)
        {
            var insert = new TreeNode(value);

            if (this.HeadNode == null)
            {
                this.HeadNode = insert;
            }
            else
            {
                var current = this.HeadNode;

                while (true)
                {
                    if (insert.Content < current.Content)
                    {
                        if (current.LeftSubNode == null)
                        {
                            current.LeftSubNode = insert;
                            current.LeftSubNode.ParentNode = current;
                            break;
                        }
                        else
                        {
                            current = current.LeftSubNode;
                        }
                    }
                    else if (insert.Content > current.Content)
                    {
                        if (current.RightSubNode == null)
                        {
                            current.RightSubNode = insert;
                            current.RightSubNode.ParentNode = current;
                            break;
                        }
                        else
                        {
                            current = current.RightSubNode;
                        }
                    }
                    else
                    {
                        current.ContentCount += 1;
                        break;
                    }
                }

                // Muss hier alle Balance counts prüfen.
                this.RebalanceTree();
            }
        }

        /// <summary>
        /// Removes a value from the tree.
        /// </summary>
        /// <param name="value">The value that is to be removed.</param>
        /// <exception cref="ArgumentException">
        /// Thrown if the value does not exist.
        /// </exception>
        /// <returns>The amount of times the specified value was removed from the tree.</returns>
        public int Remove(int value)
        {
            var node = this.FindNodeContainingValue(this.HeadNode, value);

            if (node == null)
                return 0;

            var amountRemoved = node.ContentCount;

            if (!node.IsLeaf)
                this.PrepareNodeDeletion(node);

            this.DeleteNode(node);

            // either node is leaf, then simply remove it by null referencing it.
            // or the node has only a left subnode, then replace the node with the let subnode
            // or the node has only a right subnode, then replace the node with the right subnode.
            // or the node has both, then search for the biggest node in the left subtree, null reference it, and switch the value.

            return amountRemoved;

            throw new NotImplementedException();
        }

        protected virtual void PrepareNodeDeletion(TreeNode toDelete)
        {
            if (toDelete.LeftSubNode == null)
            {
                toDelete.Content = toDelete.RightSubNode.Content;
                toDelete.RightSubNode.ParentNode = null;
                toDelete.LeftSubNode = toDelete.RightSubNode.LeftSubNode;
                toDelete.RightSubNode = toDelete.RightSubNode.RightSubNode;
            }
            else if (toDelete.RightSubNode == null)
            {
                toDelete.Content = toDelete.LeftSubNode.Content;
                toDelete.LeftSubNode.ParentNode = null;
                toDelete.LeftSubNode = toDelete.LeftSubNode.LeftSubNode;
                toDelete.RightSubNode = toDelete.LeftSubNode.RightSubNode;
            }
            else
            {
                var biggestSubtreeNode = this.GetSubtreeMaximum(toDelete.LeftSubNode);
                toDelete.Content = biggestSubtreeNode.Content;
                biggestSubtreeNode.ParentNode.RightSubNode = null;
                biggestSubtreeNode.ParentNode = null;
            }
        }

        protected void DeleteNode(TreeNode toDelete)
        {
            if (toDelete == null)
                throw new ArgumentNullException(nameof(toDelete), "Cant delete a null reference.");

            if (toDelete.IsLeaf)
                toDelete = null;
        }

        /// <summary>
        /// Removes all values from the tree.
        /// </summary>
        public void Clear()
        {
            this.HeadNode = null;
        }

        /// <summary>
        /// Gets the amount of elements in the tree.
        /// </summary>
        /// <returns>The amount of elements.</returns>
        public int Count()
        {
            if (this.HeadNode == null)
                return 0;

            return this.TraverseInOrder(this.HeadNode).Count();
        }

        /// <summary>
        /// Gets the amount of occurrances of a specified value in the tree.
        /// </summary>
        /// <param name="value">The specified value.</param>
        /// <returns>The amount of occurrances of the specified value.</returns>
        public int Count(int value)
        {
            var node = this.FindNodeContainingValue(this.HeadNode, value);

            if (node == null)
                return 0;
            else
                return node.ContentCount;
        }

        /// <summary>
        /// Gets a value indicating whether a specified value exists.
        /// </summary>
        /// <param name="value">The specified value to look for.</param>
        /// <returns>Whether the value exists.</returns>
        public bool Contains(int value)
        {
            var node = this.FindNodeContainingValue(this.HeadNode, value);

            return node != null;
        }

        /// <summary>
        /// Traverses the tree, returning a list of all elements in the traverse path.
        /// </summary>
        /// <param name="order">The traverse order.</param>
        /// <returns>The traversed elements.</returns>
        /// <exception cref="ArgumentException">
        /// Is thrown if an order is specified that doesnt exist.
        /// </exception>
        public List<int> Traverse(TraverseOrder order)
        {
            switch (order)
            {
                case TraverseOrder.PreOrder:
                    return this.TraversePreOrder(this.HeadNode).ExtractContents();
                case TraverseOrder.InOrder:
                    return this.TraverseInOrder(this.HeadNode).ExtractContents();
                case TraverseOrder.PostOrder:
                    return this.TraversePostOrder(this.HeadNode).ExtractContents();
                default:
                    throw new ArgumentException(nameof(order), "Specified traverse order was not recognized.");
            }
        }

        /// <summary>
        /// Gets the node with the greatest value, starting from a specified node.
        /// </summary>
        /// <param name="start">The node to start from.</param>
        /// <returns>The tree node with the greatest value.</returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown if starting node is null.
        /// </exception>
        public TreeNode GetSubtreeMaximum(TreeNode start)
        {
            if (start == null)
                throw new ArgumentNullException(nameof(start), "Starting node was null.");

            var current = start;

            while (current.RightSubNode != null)
                current = current.RightSubNode;

            return current;
        }

        /// <summary>
        /// Gets the node with the lowest value, starting from a specified node.
        /// </summary>
        /// <param name="start">The node to start from.</param>
        /// <returns>The tree node with the lowest value.</returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown if starting node is null.
        /// </exception>
        public TreeNode GetSubtreeMinimum(TreeNode start)
        {
            if (start == null)
                throw new ArgumentNullException(nameof(start), "Starting node was null.");

            var current = start;

            while (current.LeftSubNode != null)
                current = current.LeftSubNode;

            return current;

        }

        /// <summary>
        /// Helper method that looks for a node containing a specific value. If the value is found
        /// returns the node containing the value. If the value is not found, returns a null reference.
        /// </summary>
        /// <param name="currentNode">The node that is currently being navigated from.</param>
        /// <param name="value">The value that is searched for.</param>
        /// <returns>The node containing the value, if present. Otherwise a null reference.</returns>
        protected virtual TreeNode FindNodeContainingValue(TreeNode currentNode, int value)
        {
            if (currentNode == null || currentNode.Content == value)
                return currentNode;

            if (value < currentNode.Content)
                return this.FindNodeContainingValue(currentNode.LeftSubNode, value);
            else
                return this.FindNodeContainingValue(currentNode.RightSubNode, value);
        }

        protected virtual void RebalanceTree()
        {
            var nodes = this.TraversePreOrder(this.HeadNode);

            for (int i = nodes.Count - 1; i >= 0; i--)
            {
                if (nodes[i].BalanceFactor.IsBetween(-1, 1))
                    continue;

                var rotation = this.DetermineRotation(nodes[i]);
                rotation.Invoke(nodes[i]);
                break;
            }

            //foreach (var item in nodes)
            //{
            //    if (item.BalanceFactor.IsBetween(-1, 1))
            //        continue;

            //    var rotation = this.DetermineRotation(item);
            //    rotation.Invoke(item);
            //}
        }

        /// <summary>
        /// Traverses the tree using post order traversal.
        /// </summary>
        /// <param name="currentBaseNode">The node that is the current </param>
        /// <param name="numbers">The list in which to store the numbers. Needed for recursion.</param>
        /// <returns>A list containing all of the values present in the tree.</returns>
        protected List<TreeNode> TraversePostOrder(TreeNode currentBaseNode, List<TreeNode> numbers = null)
        {
            if (numbers == null)
                numbers = new List<TreeNode>();

            if (currentBaseNode != null)
            {
                this.TraversePostOrder(currentBaseNode.LeftSubNode, numbers);
                this.TraversePostOrder(currentBaseNode.RightSubNode, numbers);
                numbers.Add(currentBaseNode);
            }

            return numbers;
        }

        /// <summary>
        /// Traverses the tree using in order traversal.
        /// </summary>
        /// <param name="currentBaseNode">The node that is the current </param>
        /// <param name="numbers">The list in which to store the numbers. Needed for recursion.</param>
        /// <returns>A list containing all of the values present in the tree.</returns>
        protected List<TreeNode> TraverseInOrder(TreeNode currentBaseNode, List<TreeNode> numbers = null)
        {
            if (numbers == null)
                numbers = new List<TreeNode>();

            if (currentBaseNode != null)
            {
                this.TraverseInOrder(currentBaseNode.LeftSubNode, numbers);
                numbers.Add(currentBaseNode);
                this.TraverseInOrder(currentBaseNode.RightSubNode, numbers);
            }

            return numbers;
        }

        /// <summary>
        /// Traverses the tree using pre order traversal.
        /// </summary>
        /// <param name="currentBaseNode">The node that is the current </param>
        /// <param name="numbers">The list in which to store the numbers. Needed for recursion.</param>
        /// <returns>A list containing all of the values present in the tree.</returns>
        protected List<TreeNode> TraversePreOrder(TreeNode currentBaseNode, List<TreeNode> numbers = null)
        {
            if (numbers == null)
                numbers = new List<TreeNode>();

            if (currentBaseNode != null)
            {
                numbers.Add(currentBaseNode);
                this.TraversePreOrder(currentBaseNode.LeftSubNode, numbers);
                this.TraversePreOrder(currentBaseNode.RightSubNode, numbers);
            }

            return numbers;
        }

        /// <summary>
        /// Determines which type of rotation a tree node needs, in order to be balanced.
        /// </summary>
        /// <param name="node">The tree node that needs rebalancing.</param>
        /// <returns>A delegate pointing to the correct rotation method to rebalance the node.</returns>
        protected virtual Action<TreeNode> DetermineRotation(TreeNode node)
        {
            if (node.BalanceFactor.IsBetween(-1, 1))
                throw new ArgumentException(nameof(node), "The node does not need a rotation");

            if (node.BalanceFactor < 0)
            {
                if (node.RightSubNode.BalanceFactor > 0)
                    return this.DoubleLeftRotation;
                else
                    return this.SingleLeftRotation;
            }
            else
            {
                if (node.LeftSubNode.BalanceFactor < 0)
                    return this.DoubleRightRotation;
                else
                    return this.SingleRightRotation;
            }
        }

        protected virtual void SingleLeftRotation(TreeNode rotationAxis)
        {
            var newRoot = rotationAxis.RightSubNode;

            newRoot.ParentNode = rotationAxis.ParentNode;

            if (newRoot.ParentNode == null)
                this.HeadNode = newRoot;
            else if (newRoot.Content < newRoot.ParentNode.Content)
                newRoot.ParentNode.LeftSubNode = newRoot;
            else
                newRoot.ParentNode.RightSubNode = newRoot;

            rotationAxis.RightSubNode = newRoot.LeftSubNode;
            newRoot.LeftSubNode = rotationAxis;
            rotationAxis.ParentNode = newRoot;
        }

        protected virtual void DoubleLeftRotation(TreeNode rotationAxis)
        {
            this.SingleRightRotation(rotationAxis.RightSubNode);
            this.SingleLeftRotation(rotationAxis);
        }

        protected virtual void SingleRightRotation(TreeNode rotationAxis)
        {
            var newRoot = rotationAxis.LeftSubNode;
            newRoot.ParentNode = rotationAxis.ParentNode;

            if (newRoot.ParentNode == null)
                this.HeadNode = newRoot;
            else if (newRoot.Content > newRoot.ParentNode.Content)
                newRoot.ParentNode.RightSubNode = newRoot;
            else
                newRoot.ParentNode.LeftSubNode = newRoot;

            rotationAxis.LeftSubNode = newRoot.RightSubNode;
            rotationAxis.ParentNode = newRoot;
            newRoot.RightSubNode = rotationAxis;
        }

        protected virtual void DoubleRightRotation(TreeNode rotationAxis)
        {
            this.SingleLeftRotation(rotationAxis.LeftSubNode);
            this.SingleRightRotation(rotationAxis);
        }
    }
}
