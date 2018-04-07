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
            LinkedList<int> singleList = new LinkedList<int>(new Node<int>(false));
            LinkedList<int> doubleList = new LinkedList<int>(new Node<int>(true));

            Assert.AreEqual("0", singleList.ToString());
            Assert.AreEqual("0", doubleList.ToString());

            // 1 -> 2 -> 3 -> 4
            Node<int> head = new Node<int>(false, 1);
            head.Next = new Node<int>(false, 2);
            head.Next.Next = new Node<int>(false, 3);
            head.Next.Next.Next = new Node<int>(false, 4);
            singleList = new LinkedList<int>(head);

            Assert.AreEqual(4, singleList.Count);
            Assert.AreEqual("1 -> 2 -> 3 -> 4", singleList.ToString());

            // 1 <-> 2 <-> 3 <-> 4
            head = new Node<int>(true, 1);
            head.Next = new Node<int>(true, 2, null, head);
            head.Next.Next = new Node<int>(true, 3, null, head.Next);
            head.Next.Next.Next = new Node<int>(true, 4, null, head.Next.Next);
            doubleList = new LinkedList<int>(head);

            Assert.AreEqual(4, doubleList.Count);
            Assert.AreEqual("1 <-> 2 <-> 3 <-> 4", doubleList.ToString());
        }

        [Test]
        public void InsertTest()
        {
            LinkedList<int> singleList = new LinkedList<int>(new Node<int>(false));
            Node<int> node = new Node<int>(false, 1);

            // Insert just a single node at the end.
            singleList.Insert(node);
            Assert.AreEqual(2, singleList.Count);
            Assert.AreEqual("0 -> 1", singleList.ToString());

            // Insert a node with others attached.
            node.Next = new Node<int>(false, 2);
            singleList.Insert(node);
            Assert.AreEqual(4, singleList.Count);
            Assert.AreEqual("0 -> 1 -> 1 -> 2", singleList.ToString());

            // Insert at the front.
            node = new Node<int>(false, -1);
            singleList.Insert(node, 0);
            Assert.AreEqual(5, singleList.Count);
            Assert.AreEqual("-1 -> 0 -> 1 -> 1 -> 2", singleList.ToString());

            // Insert at front with others attached.
            node.Next = new Node<int>(false, -2);
            node.Next.Next = new Node<int>(false, -3);
            singleList.Insert(node, 0);
            Assert.AreEqual(8, singleList.Count);
            Assert.AreEqual("-1 -> -2 -> -3 -> -1 -> 0 -> 1 -> 1 -> 2", singleList.ToString());

            // Insert into the middle.
            node.Next.Next = null;
            singleList.Insert(node, 5);
            Assert.AreEqual(10, singleList.Count);
            Assert.AreEqual("-1 -> -2 -> -3 -> -1 -> 0 -> -1 -> -2 -> 1 -> 1 -> 2", singleList.ToString());

            LinkedList<int> doubleList = new LinkedList<int>(new Node<int>(true));
            node = new Node<int>(true, 1);

            // Insert just a single node at the end.
            doubleList.Insert(node);
            Assert.AreEqual(2, doubleList.Count);
            Assert.AreEqual("0 <-> 1", doubleList.ToString());

            // Insert a node with others attached.
            node.Next = new Node<int>(true, 2, null, node.Next);
            doubleList.Insert(node);
            Assert.AreEqual(4, doubleList.Count);
            Assert.AreEqual("0 <-> 1 <-> 1 <-> 2", doubleList.ToString());

            // Insert at the front.
            node = new Node<int>(true, -1);
            doubleList.Insert(node, 0);
            Assert.AreEqual(5, doubleList.Count);
            Assert.AreEqual("-1 <-> 0 <-> 1 <-> 1 <-> 2", doubleList.ToString());

            // Insert at front with others attached.
            node.Next = new Node<int>(true, -2, null, node);
            node.Next.Next = new Node<int>(true, -3, null, node.Next);
            doubleList.Insert(node, 0);
            Assert.AreEqual(8, doubleList.Count);
            Assert.AreEqual("-1 <-> -2 <-> -3 <-> -1 <-> 0 <-> 1 <-> 1 <-> 2", doubleList.ToString());

            // Insert into the middle.
            node.Next.Next = null;
            doubleList.Insert(node, 5);
            Assert.AreEqual(10, doubleList.Count);
            Assert.AreEqual("-1 <-> -2 <-> -3 <-> -1 <-> 0 <-> -1 <-> -2 <-> 1 <-> 1 <-> 2", doubleList.ToString());

            // Insert so that it starts traversing from the back.
            doubleList.Insert(node, 8);
            // Make sure that killing the local node doesn't affect the list (since the nodes shouldv'e been hard copied).
            node = null;
            Assert.AreEqual(12, doubleList.Count);
            Assert.AreEqual("-1 <-> -2 <-> -3 <-> -1 <-> 0 <-> -1 <-> -2 <-> 1 <-> -1 <-> -2 <-> 1 <-> 2", doubleList.ToString());
        }
    }
}
