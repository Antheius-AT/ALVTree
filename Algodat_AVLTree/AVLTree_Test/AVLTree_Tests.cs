using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Algodat_AVLTree;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using NUnit.Framework;

namespace AVLTree_Test
{
    [TestFixture]
    public class AVLTree_Tests
    {
        private AVLTree tree;

        [SetUp]
        public void SetUp()
        {
            this.tree = new AVLTree();
        }

        [TestCase(5)]
        [TestCase(-10)]
        [TestCase(1000000)]
        [TestCase(-550000)]
        [Test]
        public void IsCorrectlyAdded_SingleValues(int value)
        {
            this.tree.Insert(value);

            Assert.IsTrue(this.tree.HeadNode.Content == value);
        }

        
        [Test]
        public void ValueFound_If_Previously_Added()
        {
            this.tree.Insert(10);
            this.tree.Insert(5);
            this.tree.Insert(100);

            var containsTen = this.tree.Contains(10);
            var containsFive = this.tree.Contains(5);
            var containsNegativeFifty = this.tree.Contains(-50);

            Assert.IsTrue(containsTen);
            Assert.IsTrue(containsFive);
            Assert.IsFalse(containsNegativeFifty);
        }

        [TestCase(5, 10, 20, 50, 100, 200, 500, 1000)]
        [Test]
        public void Does_Rotating_Left_Work(params int[] values)
        {
            foreach (var item in values)
            {
                this.tree.Insert(item);
            }

            Assert.IsTrue(this.tree.HeadNode.Content == 50);
            Assert.IsTrue(this.tree.HeadNode.LeftSubNode.Content == 10);
            Assert.IsTrue(this.tree.HeadNode.LeftSubNode.LeftSubNode.Content == 5);
            Assert.IsTrue(this.tree.HeadNode.LeftSubNode.RightSubNode.Content == 20);
            Assert.IsTrue(this.tree.HeadNode.RightSubNode.Content == 200);
            Assert.IsTrue(this.tree.HeadNode.RightSubNode.LeftSubNode.Content == 100);
            Assert.IsTrue(this.tree.HeadNode.RightSubNode.RightSubNode.Content == 500);
            Assert.IsTrue(this.tree.HeadNode.RightSubNode.RightSubNode.RightSubNode.Content == 1000);
        }

        [TestCase(1000, 800, 500, 200, 50, 40, 20, 10)]
        [Test]
        public void Does_Rotating_Right_Work(params int[] values)
        {
            foreach (var item in values)
            {
                this.tree.Insert(item);
            }

            Assert.IsTrue(this.tree.HeadNode.Content == 200);
            Assert.IsTrue(this.tree.HeadNode.LeftSubNode.Content == 40);
            Assert.IsTrue(this.tree.HeadNode.LeftSubNode.LeftSubNode.Content == 20);
            Assert.IsTrue(this.tree.HeadNode.LeftSubNode.LeftSubNode.LeftSubNode.Content == 10);
            Assert.IsTrue(this.tree.HeadNode.LeftSubNode.RightSubNode.Content == 50);
            Assert.IsTrue(this.tree.HeadNode.RightSubNode.LeftSubNode.Content == 500);
            Assert.IsTrue(this.tree.HeadNode.RightSubNode.RightSubNode.Content == 1000);
            Assert.IsTrue(this.tree.HeadNode.RightSubNode.Content == 800);
        }

        [TestCase(50, 20, -50, 100)]
        [TestCase(20)]
        [TestCase(-100, 50, 1, 2)]
        [TestCase(5, 5, 5, 5, 10)]
        [TestCase()]
        [Test]
        public void Does_Count_Work_After_Inserting_Elements(params int[] values)
        {
            foreach (var item in values)
            {
                this.tree.Insert(item);
            }

            var count = this.tree.Count();

            Assert.AreEqual(values.Length, count);
        }
        
        [TestCase(5, 100, 2000, -50, 5, 100, 20, 2, 0)]
        [TestCase(10)]
        [TestCase(-5)]
        [TestCase()]
        [Test]
        public void Does_Clear_Actually_Remove_All_Values(params int[] values)
        {
            foreach (var item in values)
            {
                this.tree.Insert(item);
            }

            this.tree.Clear();

            var count = this.tree.Count();

            Assert.IsTrue(this.tree.HeadNode == null);
            Assert.AreEqual(0, count);
        }

        [TestCase(5, 10, 20, 50, 100, -50, 5, 5)]
        [TestCase(-20, -21, -19, -19, -19, 5, 5, 100)]
        [TestCase(50, 50, 50, 50, 50)]
        [TestCase(500000)]
        [Test]
        public void Does_Count_Work_For_Specific_Value(int valueToCheck, params int[] valuesInTree)
        {
            foreach (var item in valuesInTree)
            {
                this.tree.Insert(item);
            }

            var count = this.tree.Count(valueToCheck);

            Assert.AreEqual(valuesInTree.Where(p => p == valueToCheck).Count(), count);
        }

        [TestCase(0, 100, 200, -50, 22, 5)]
        [TestCase(0, -5, -20, -30)]
        [TestCase(20, 22, 19, 21, 22)]
        [TestCase(50, 50)]
        [TestCase(33, 27, 15, 33, 33)]
        [Test]
        public void Does_Contains_Work_After_Inserting_Elements(int toCheck, params int[] values)
        {
            foreach (var item in values)
            {
                this.tree.Insert(item);
            }

            var contains = tree.Contains(toCheck);

            Assert.AreEqual(values.Contains(toCheck), contains);
        }

        [TestCase(5, 20, -22, 5, -10, 5, 1000)]
        [TestCase(-1000, -10000, -100000, -1000000, 20)]
        [TestCase(50, -20, 3, 100, 50, 3, 22, 17)]
        [TestCase(5)]
        [TestCase()]
        [Test]
        public void DoesTraverseWork_InOrder(params int[] values)
        {
            foreach (var item in values)
            {
                this.tree.Insert(item);
            }

            var traversed = this.tree.Traverse(TraverseOrder.InOrder);

            Assert.AreEqual(traversed, values.OrderBy(p => p));
        }

        [TestCase(5, 5, 5, 5, 5, 5, 5, 10)]
        [TestCase(10, 22, -5, -100, 0, 0, -5, 50)]
        [TestCase(5, 22, 10, 3, 100, 50, -100, 3, 3, 5, 10, 5)]
        [TestCase(100, 100, 99, 0, 50)]
        [TestCase(10)]
        [TestCase(10, 10, 20, 50, 55, 5, 15)]
        [TestCase(10, 10, 20, 50, 55, 5, 15, 7)]
        [Test]
        public void Does_Deleting_Node_Remove_Node_From_Tree(int toDelete, params int[] values)
        {
            foreach (var item in values)
            {
                this.tree.Insert(item);
            }

            tree.Remove(toDelete);

            var treeValues = this.tree.Traverse(TraverseOrder.InOrder);
            var control = values.Where(p => p != toDelete).OrderBy(p => p).ToList();

            Assert.AreEqual(control, treeValues);
        }

        [Test]
        public void Unsupported_TraversalMethod_Throws_Exception()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                this.tree.Traverse((TraverseOrder)10);
            });
        }
    }
}
