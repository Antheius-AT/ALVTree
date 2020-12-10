//-----------------------------------------------------------------------
// <copyright file="TraverseOrder.cs" company="FHWN">
//     Copyright (c) FHWN. All rights reserved.
// </copyright>
// <author>Gregor Faiman</author>
//-----------------------------------------------------------------------
namespace Algodat_AVLTree
{
    /// <summary>
    /// Enumeration specifying the tree traverse order.
    /// </summary>
    public enum TraverseOrder
    {
        /// <summary>
        /// Pre order traversal. Node -> left -> right
        /// </summary>
        PreOrder,

        /// <summary>
        /// In order traversal. Left -> Node -> Right
        /// </summary>
        InOrder,

        /// <summary>
        /// Post order traversal. Left -> Right -> Node
        /// </summary>
        PostOrder
    }
}
