using System;
using CSharpBasics.Algorithms;
using NUnit.Framework;

namespace CSharpBasics.Tests
{
    [TestFixture]
    public class MergeSortTest
    {
        [Test]
        public void IntMergeSortTest()
        {
            int[] test = new int[100];
            Random random = new Random();

            Console.Write("Unsorted : [ ");
            for (int i = 0; i < 100; i++)
            {
                test[i] = random.Next();
                Console.Write(test[i] + " ");
            }
            Console.WriteLine("]");

            MergeSort.IntSort(test);

            int prev = int.MinValue;
            Console.Write("Sorted : [ ");
            for (int i = 0; i < 100; i++)
            {
                Assert.GreaterOrEqual(test[i], prev);
                Console.Write(test[i] + " ");
                prev = test[i];
            }
            Console.Write("]");
        }
    }
}
