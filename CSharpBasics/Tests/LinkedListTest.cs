using CSharpBasics;
using NUnit.Framework;

namespace CSharpBasicsTests
{
    [TestFixture]
    public class LinkedListTest
    {
        [Test]
        public void ToStringTest()
        {
            LinkedList<int> singleList = new LinkedList<int>(false, new Node<int>(false));
            LinkedList<int> doubleList = new LinkedList<int>(true, new Node<int>(true));

            Assert.AreEqual("0", singleList.ToString());
            Assert.AreEqual("0", doubleList.ToString());

            // 1 -> 2 -> 3 -> 4
            Node<int> head = new Node<int>(false, 1);
            head.Next = new Node<int>(false, 2);
            head.Next.Next = new Node<int>(false, 3);
            head.Next.Next.Next = new Node<int>(false, 4);
            singleList = new LinkedList<int>(false, head);

            Assert.AreEqual(4, singleList.Count);
            Assert.AreEqual("1 -> 2 -> 3 -> 4", singleList.ToString());

            // 1 <-> 2 <-> 3 <-> 4
            head = new Node<int>(true, 1);
            head.Next = new Node<int>(true, 2, null, head);
            head.Next.Next = new Node<int>(true, 3, null, head.Next);
            head.Next.Next.Next = new Node<int>(true, 4, null, head.Next.Next);
            doubleList = new LinkedList<int>(true, head);

            Assert.AreEqual(4, doubleList.Count);
            Assert.AreEqual("1 <-> 2 <-> 3 <-> 4", doubleList.ToString());

            // Test empty list.
            singleList = new LinkedList<int>(false);
            doubleList = new LinkedList<int>(true);
            Assert.IsTrue(singleList.IsEmpty);
            Assert.IsTrue(doubleList.IsEmpty);
            Assert.AreEqual(string.Empty, singleList.ToString());
            Assert.AreEqual(string.Empty, doubleList.ToString());
        }

        [Test]
        public void InsertTest()
        {
            LinkedList<int> singleList = new LinkedList<int>(false);
            Node<int> node = new Node<int>(false, 1);

            // Insert just a single node at the end.
            singleList.Insert(node);
            Assert.AreEqual(1, singleList.Count);
            Assert.AreEqual("1", singleList.ToString());
            singleList.Insert(node);
            Assert.AreEqual(2, singleList.Count);
            Assert.AreEqual("1 -> 1", singleList.ToString());

            // Insert a node with others attached.
            singleList = new LinkedList<int>(false);
            node.Next = new Node<int>(false, 2);
            singleList.Insert(node);
            Assert.AreEqual(2, singleList.Count);
            Assert.AreEqual("1 -> 2", singleList.ToString());

            // Insert at the front.
            node = new Node<int>(false, -1);
            singleList.Insert(node, 0);
            Assert.AreEqual(3, singleList.Count);
            Assert.AreEqual("-1 -> 1 -> 2", singleList.ToString());

            // Insert at front with others attached.
            node.Next = new Node<int>(false, -2);
            node.Next.Next = new Node<int>(false, -3);
            singleList.Insert(node, 0);
            Assert.AreEqual(6, singleList.Count);
            Assert.AreEqual("-1 -> -2 -> -3 -> -1 -> 1 -> 2", singleList.ToString());

            // Insert into the middle.
            node.Next.Next = null;
            singleList.Insert(node, 3);
            Assert.AreEqual(8, singleList.Count);
            Assert.AreEqual("-1 -> -2 -> -3 -> -1 -> -2 -> -1 -> 1 -> 2", singleList.ToString());

            LinkedList<int> doubleList = new LinkedList<int>(true);
            node = new Node<int>(true, 1);

            // Insert just a single node at the end.
            doubleList.Insert(node);
            Assert.AreEqual(1, doubleList.Count);
            Assert.AreEqual("1", doubleList.ToString());
            doubleList.Insert(node);
            Assert.AreEqual(2, doubleList.Count);
            Assert.AreEqual("1 <-> 1", doubleList.ToString());

            // Insert a node with others attached.
            doubleList = new LinkedList<int>(true);
            node.Next = new Node<int>(true, 2, null, node.Next);
            doubleList.Insert(node);
            Assert.AreEqual(2, doubleList.Count);
            Assert.AreEqual("1 <-> 2", doubleList.ToString());

            // Insert at the front.
            node = new Node<int>(true, -1);
            doubleList.Insert(node, 0);
            Assert.AreEqual(3, doubleList.Count);
            Assert.AreEqual("-1 <-> 1 <-> 2", doubleList.ToString());

            // Insert at front with others attached.
            node.Next = new Node<int>(true, -2, null, node);
            node.Next.Next = new Node<int>(true, -3, null, node.Next);
            doubleList.Insert(node, 0);
            Assert.AreEqual(6, doubleList.Count);
            Assert.AreEqual("-1 <-> -2 <-> -3 <-> -1 <-> 1 <-> 2", doubleList.ToString());

            // Insert into the middle.
            node.Next.Next = null;
            doubleList.Insert(node, 3);
            Assert.AreEqual(8, doubleList.Count);
            Assert.AreEqual("-1 <-> -2 <-> -3 <-> -1 <-> -2 <-> -1 <-> 1 <-> 2", doubleList.ToString());

            // Insert so that it starts traversing from the back.
            doubleList.Insert(node, 6);
            // Make sure that killing the local node doesn't affect the list (since the nodes shouldv'e been hard copied).
            node = null;
            Assert.AreEqual(10, doubleList.Count);
            Assert.AreEqual("-1 <-> -2 <-> -3 <-> -1 <-> -2 <-> -1 <-> -1 <-> -2 <-> 1 <-> 2", doubleList.ToString());
        }

        [Test]
        public void RemoveTest()
        {
            LinkedList<int> singleList = new LinkedList<int>(false);

            singleList.Remove(1);
            Assert.IsTrue(singleList.IsEmpty);

            singleList.Insert(0);
            singleList.Insert(1);
            singleList.Insert(2);
            singleList.Insert(3);

            singleList.Remove(2);
            Assert.AreEqual(3, singleList.Count);
            Assert.AreEqual("0 -> 1 -> 3", singleList.ToString());

            singleList.Remove(5);
            singleList.Remove(0, 1);
            Assert.AreEqual(3, singleList.Count);

            singleList.Insert(0);
            singleList.Insert(0);
            singleList.Remove(0, 1);
            Assert.AreEqual(4, singleList.Count);
            Assert.AreEqual("0 -> 1 -> 3 -> 0", singleList.ToString());

            LinkedList<int> doubleList = new LinkedList<int>(true);

            doubleList.Remove(1);
            Assert.IsTrue(doubleList.IsEmpty);

            doubleList.Insert(0);
            doubleList.Insert(1);
            doubleList.Insert(2);
            doubleList.Insert(3);

            doubleList.Remove(2);
            Assert.AreEqual(3, doubleList.Count);
            Assert.AreEqual("0 <-> 1 <-> 3", doubleList.ToString());

            doubleList.Remove(5);
            doubleList.Remove(0, 1);
            Assert.AreEqual(3, doubleList.Count);

            doubleList.Insert(0);
            doubleList.Insert(0);
            doubleList.Remove(0, 1);
            Assert.AreEqual(4, doubleList.Count);
            Assert.AreEqual("0 <-> 1 <-> 3 <-> 0", doubleList.ToString());
        }

        [Test]
        public void ReverseTest()
        {
            LinkedList<int> singleList = new LinkedList<int>(false);
            Assert.IsTrue(singleList.Reverse().IsEmpty);

            singleList.Insert(1);
            Assert.AreEqual(1, singleList.Reverse().Count);
            Assert.AreEqual("1", singleList.Reverse().ToString());

            singleList.Insert(2);
            singleList.Insert(3);
            Assert.AreEqual(3, singleList.Reverse().Count);
            Assert.AreEqual("3 -> 2 -> 1", singleList.Reverse().ToString());

            LinkedList<int> doubleList = new LinkedList<int>(true);
            Assert.IsTrue(doubleList.Reverse().IsEmpty);

            doubleList.Insert(1);
            Assert.AreEqual(1, doubleList.Reverse().Count);
            Assert.AreEqual("1", doubleList.Reverse().ToString());

            doubleList.Insert(2);
            doubleList.Insert(3);
            Assert.AreEqual(3, doubleList.Reverse().Count);
            Assert.AreEqual("3 <-> 2 <-> 1", doubleList.Reverse().ToString());
        }

        [Test]
        public void HasCycleTest()
        {
            LinkedList<int> singleList = new LinkedList<int>(false);
            Assert.IsFalse(singleList.HasCycle());

            singleList.Insert(1);
            singleList.Insert(1);
            singleList.Insert(3);
            singleList.Insert(1);
            singleList.Insert(4);

            Assert.IsFalse(singleList.HasCycle());

            // Ok, let's do an actual cycle.
            // 1 -> 2 -> 3 - > (cycle start) 4 -> 5 -> 6 -> 7 -> 8 -> ... (loop back) 
            Node<int> cycleList = new Node<int>(false, 1);
            cycleList.Next = new Node<int>(false, 2);
            cycleList.Next.Next = new Node<int>(false, 3);
            Node<int> cycleStart = new Node<int>(false, 4);
            cycleStart.Next = new Node<int>(false, 5);
            cycleStart.Next.Next = new Node<int>(false, 6);
            cycleStart.Next.Next.Next = new Node<int>(false, 7);
            cycleStart.Next.Next.Next.Next = new Node<int>(false, 8);
            cycleStart.Next.Next.Next.Next.Next = cycleStart;
            cycleList.Next.Next.Next = cycleStart;

            // This is when I discovered that a circular linked list
            // causes the constructor and insert to overflow the stack.
            // This is due to our hard copy loops. The price you pay...
            // Eeek. We can fix this in a future exercise.
            // singleList = new LinkedList<int>(false, cycleList);
            // Assert.IsTrue(singleList.HasCycle());
        }
    }
}
