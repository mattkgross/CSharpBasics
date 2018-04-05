using System;

namespace CSharpBasics
{
    /// <summary>
    /// Linked list node. Type T must have a public parameterless constructor.
    /// Generic Constraints: http://www.tutorialsteacher.com/csharp/constraints-in-generic-csharp
    /// </summary>
    public class Node<T> where T : new()
    {
        // The node's data.
        private T data;
        /// <summary>
        /// Gets or sets the data.
        /// </summary>
        /// <value>The data.</value>
        public T Data { get; set; }

        // The previous node in the list.
        private Node<T> prev;
        /// <summary>
        /// Gets or sets the previous node.
        /// If the list is not doubly linked, null is **always** returned.
        /// </summary>
        /// <value>The previous node.</value>
        public Node<T> Prev
        {
            get
            {
                return this.IsDoublyLinked ? this.prev : null;
            }

            set
            {
                this.prev = this.IsDoublyLinked ? value : null;
            }
        }

        // The next node in the list.
        private Node<T> next;
        /// <summary>
        /// Gets or sets the next node.
        /// </summary>
        /// <value>The next node.</value>
        public Node<T> Next { get; set; }

        /// <summary>
        /// Gets a value indicating whether this <see cref="T:CSharpBasics.Node`1"/> is doubly linked.
        /// </summary>
        /// <value><c>true</c> if is doubly linked; otherwise, <c>false</c>.</value>
        public bool IsDoublyLinked { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:CSharpBasics.Node`1"/> class.
        /// </summary>
        /// <param name="isDoublyLinked">If set to <c>true</c> is doubly linked.</param>
        /// <param name="data">The node's data.</param>
        /// <param name="next">The next node.</param>
        /// <param name="prev">The previous node.</param>
        public Node(bool isDoublyLinked = true, T data = default(T), Node<T> next = null, Node<T> prev = null)
        {
            this.IsDoublyLinked = isDoublyLinked;
            this.data = data;
            this.next = next;
            this.prev = this.IsDoublyLinked ? prev : null;
        }

        /// <summary>
        /// Insert the specified node.
        /// </summary>
        /// <returns></returns>
        /// <param name="node">The node to insert after the current one.</param>
        public void Insert(Node<T> node)
        {

        }
    }

    class LinkedList
    {
        #region Tests
        public static void RunTests()
        {
            InsertTest();
        }

        private static void InsertTest()
        {

        }
        #endregion Tests
    }
}
