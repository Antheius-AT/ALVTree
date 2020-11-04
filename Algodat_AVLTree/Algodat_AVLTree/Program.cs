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

            tree.Remove(7);
            tree.Remove(16);
            tree.Remove(14);
            tree.Remove(4);

            tree.Insert(-10);
            tree.Insert(-5);
            tree.Insert(4);
            tree.Insert(4);

            Console.WriteLine(tree.HeadNode.BalanceFactor);

            tree.Insert(8);
            tree.Insert(9);
            tree.Insert(10);
            tree.Insert(15);

            Console.WriteLine(tree.HeadNode.BalanceFactor);

            tree.Insert(4);
            Console.WriteLine(tree.HeadNode.BalanceFactor);


            Console.WriteLine($"Minimum: {tree.GetSubtreeMinimum(tree.HeadNode).Content}");

            tree.Clear();
            var amount = tree.Remove(4);
            var anotherAmount = tree.Remove(1000);
            Console.ReadLine();
        }

        private static void Print(AVLTree tree)
        {
            var numbersInOrder = tree.Traverse(TraverseOrder.InOrder);
            Console.WriteLine("In Order:");
            foreach (var item in numbersInOrder)
            {
                Console.WriteLine(item);
            }

            Console.WriteLine("Post Order");
            var numbersPostOrder = tree.Traverse(TraverseOrder.PostOrder);
            foreach (var item in numbersPostOrder)
            {
                Console.WriteLine(item);
            }

            Console.WriteLine("Pree Order");
            var numbersPreeOrder = tree.Traverse(TraverseOrder.PreOrder);
            foreach (var item in numbersPreeOrder)
            {
                Console.WriteLine(item);
            }
        }
    }
}
