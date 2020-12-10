//-----------------------------------------------------------------------
// <copyright file="TreeNode.cs" company="FHWN">
//     Copyright (c) FHWN. All rights reserved.
// </copyright>
// <author>Gregor Faiman</author>
//-----------------------------------------------------------------------
namespace Algodat_AVLTree
{
    using System;

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
        /// If the parent node is set
        /// </summary>
        public TreeNode ParentNode
        {
            get;
            set;
        }

        /// <summary>
        /// Gets this nodes balance factor. 
        /// A negative balance factor means, that the node`s subtree is right heavy.
        /// A positive balance factor means, that the node`s subtree is left heavy.
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
        /// Gets a value indicating whether the node is a leaf.
        /// </summary>
        public bool IsLeaf
        {
            get
            {
                return this.LeftSubNode == null && this.RightSubNode == null;
            }
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
            var left = this.IterateRecursively(node.LeftSubNode, iterationCount);
            var right = this.IterateRecursively(node.RightSubNode, iterationCount);

            // Math.Max because I dont want the subtree depths to be added, but rather 
            // I want to check which subtree is the greatest.
            return Math.Max(left, right);
        }
    }
}
