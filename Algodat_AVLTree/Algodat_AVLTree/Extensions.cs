using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algodat_AVLTree
{
    public static class Extensions
    {
        /// <summary>
        /// Extension method that takes a list of tree nodes and extracts the contents of those nodes.
        /// </summary>
        /// <param name="source">The collection of tree nodes.</param>
        /// <returns>The tree node`s contents.</returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown if node collection is null.
        /// </exception>
        public static List<int> ExtractContents(this List<TreeNode> source)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source), "Source must not be null.");

            var numbers = new List<int>();

            foreach (var item in source)
            {
                numbers.AddRange(Enumerable.Repeat(item.Content, item.ContentCount));
            }

            return numbers;
        }

        /// <summary>
        /// Extension method for the type <see cref="int"/> which checks whether a given value is within specified boundaries.
        /// </summary>
        /// <param name="toCheck">The integer to check.</param>
        /// <param name="lowerBoundary">The lower boundary.</param>
        /// <param name="upperBoundary">The upper boundary.</param>
        /// <returns>Whether the value is within the boundaries.</returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Is thrown if lower boundary is greater than upper boundary.
        /// </exception>
        public static bool IsBetween(this int toCheck, int lowerBoundary, int upperBoundary)
        {
            if (lowerBoundary > upperBoundary)
                throw new ArgumentOutOfRangeException(nameof(lowerBoundary), "Lower boundary must not be greater than upper boundary");

            return toCheck >= lowerBoundary && toCheck <= upperBoundary;
        }
    }
}
