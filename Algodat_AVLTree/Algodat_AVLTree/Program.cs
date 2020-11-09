using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Algodat_AVLTree
{
    public class Program
    {
        static void Main(string[] args)
        {
            var tree = new AVLTree();
            tree.Insert(10);
            tree.Insert(15);
            tree.Insert(20);
            tree.Insert(13);
            tree.Insert(25);
            tree.Insert(5);
            tree.Insert(7);
            tree.Insert(5);
            tree.Insert(5);

            tree.Count();

            tree.Traverse((TraverseOrder)10);

            var list = tree.Traverse(TraverseOrder.PostOrder);
            tree.Remove(10);
        }
    }
}
