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
            head.Next = new Node<int>(true, 2);
            head.Next.Prev = head;
            head.Next.Next = new Node<int>(true, 3);
            head.Next.Next.Prev = head.Next;
            head.Next.Next.Next = new Node<int>(true, 4);
            head.Next.Next.Next.Prev = head.Next.Next;
            doubleList = new LinkedList<int>(head);

            Assert.AreEqual(4, doubleList.Count);
            Assert.AreEqual("1 <-> 2 <-> 3 <-> 4", doubleList.ToString());
        }
    }
}
