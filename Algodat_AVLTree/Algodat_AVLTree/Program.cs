using System;
using System.Runtime.CompilerServices;

namespace Algodat_AVLTree
{
    public class Program
    {
        static void Main(string[] args)
        {
            var tree = new AVLTree();
            tree.Insert(5);
            tree.Insert(7);
            tree.Insert(20);
            tree.Insert(15);
            tree.Insert(14);
            tree.Insert(16);
            tree.Insert(4);
            tree.Insert(2);
            tree.Insert(-10);
            tree.Insert(-5);

            Console.WriteLine(tree.HeadNode.BalanceFactor);

            tree.Insert(8);
            tree.Insert(9);
            tree.Insert(10);
            tree.Insert(15);

            Console.WriteLine(tree.HeadNode.BalanceFactor);

            tree.Insert(4);
            Console.WriteLine(tree.HeadNode.BalanceFactor);

            var removed = tree.Remove(4);

            Console.ReadLine();
        }
    }
}
