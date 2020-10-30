using System;
using System.Collections.Generic;
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
        /// Gets the node containing the smallest value.
        /// </summary>
        public TreeNode Minimum
        {
            get
            {
                if (this.HeadNode == null)
                    return null;

                var current = this.HeadNode;

                while (current.LeftSubNode != null)
                    current = current.LeftSubNode;

                return current;
            }
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

            // either node is leaf, then simply remove it by null referencing it.
            // or the node has only a left subnode, then replace the node with the let subnode
            // or the node has only a right subnode, then replace the node with the right subnode.
            // or the node has both, then search for the biggest node in the left subtree, null reference it, and switch the value.

            return amountRemoved;

            throw new NotImplementedException();
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
            throw new NotImplementedException();
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
                    return this.TraversePreOrder();
                    break;
                case TraverseOrder.InOrder:
                    return this.TraverseInOrder(this.HeadNode);
                    break;
                case TraverseOrder.PostOrder:
                    return this.TraversePostOrder();
                    break;
                default:
                    break;
            }

            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        protected List<int> TraversePostOrder()
        {
            throw new NotImplementedException();
        }

        protected List<int> TraverseInOrder(TreeNode current)
        {
            if (current != null)
            {
                Console.WriteLine(current.Content);
                this.TraverseInOrder(current.LeftSubNode);
                this.TraverseInOrder(current.RightSubNode);
            }

            return null;
        }

        protected List<int> TraversePreOrder()
        {
            throw new NotImplementedException();
        }
    }
}
