using System;
using System.Collections.Generic;
using System.Text;

namespace Algodat_AVLTree
{
    /// <summary>
    /// Represent a tree node.
    /// </summary>
    public class TreeNode
    {
        private int contentCount;

        public TreeNode(int value)
        {
            this.Content = value;
            this.ContentCount = 1;
        }

        /// <summary>
        /// Represents a left node, guaranteed to contain a smaller value in its <see cref="Content"/> Property.
        /// </summary>
        public TreeNode LeftSubNode
        {
            get;
            set;
        }

        /// <summary>
        /// Represents a right node, guaranteed to contain a greater value in its <see cref="Content"/> Property.
        /// </summary>
        public TreeNode RightSubNode
        {
            get;
            set;
        }

        /// <summary>
        /// Represents the parent node. If this property is null, the current node is the root of the tree.
        /// </summary>
        public TreeNode ParentNode
        {
            get;
            set;
        }

        /// <summary>
        /// Gets this nodes balance factor.
        /// </summary>
        public int BalanceFactor
        {
            get
            {
                return this.CalculateBalanceFactor(this);
            }
        }

        /// <summary>
        /// Gets or sets this nodes content.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if value is less than 1.
        /// </exception>
        public int Content
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the amount of times this nodes content is contained in the tree.
        /// </summary>
        public int ContentCount
        {
            get
            {
                return this.contentCount;
            }

            set
            {
                if (value < 1)
                    throw new ArgumentOutOfRangeException(nameof(value), "Count must not be less than 1");

                this.contentCount = value;
            }
        }

        /// <summary>
        /// Helper method that calculates the balance factor for this particular node using recursion.
        /// </summary>
        /// <param name="node">This particular node itself.</param>
        /// <returns>The calculated balance factor.</returns>
        protected virtual int CalculateBalanceFactor(TreeNode node)
        {
            var left = IterateRecursively(node.LeftSubNode, 0);
            var right = IterateRecursively(node.RightSubNode, 0);

            return left - right;
        }

        /// <summary>
        /// Recursive helper method to move through all available subnodes to gain the depth of a tree branch.
        /// </summary>
        /// <param name="node">The current tree node.</param>
        /// <param name="iterationCount">The current iteration count.</param>
        /// <returns>The total iteration count for the specified branch.</returns>
        protected virtual int IterateRecursively(TreeNode node, int iterationCount)
        {
            if (node == null)
                return iterationCount;

            iterationCount += 1;

            iterationCount = this.IterateRecursively(node.LeftSubNode, iterationCount);
            iterationCount = this.IterateRecursively(node.RightSubNode, iterationCount);

            return iterationCount;
        }
    }
}
