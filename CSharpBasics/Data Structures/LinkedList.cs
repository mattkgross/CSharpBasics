using System;

namespace CSharpBasics
{
    /// <summary>
    /// Linked list node. Type T must have a public parameterless constructor.
    /// Generic Constraints: http://www.tutorialsteacher.com/csharp/constraints-in-generic-csharp
    /// </summary>
    public class Node<T> where T : new()
    {
        #region Properties
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
        #endregion Properties

        #region Constructors
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
            this.Data = data;
            this.Next = next;
            this.prev = this.IsDoublyLinked ? prev : null;
        }
        #endregion Constructors
    }

    class LinkedList<T> where T : new()
    {
        #region Variables
        /// <summary>
        /// Gets the number of elements in the list.
        /// </summary>
        /// <value>The count.</value>
        public uint Count { get; private set; } = 1;
        #endregion Variables

        #region Properties
        /// <summary>
        /// Gets the head <see cref="T:CSharpBasics.Node`1"/> in the list.
        /// </summary>
        /// <value>The head node.</value>
        public Node<T> Head { get; private set; }

        /// <summary>
        /// Gets the tail <see cref="T:CSharpBasics.Node`1"/> in the list.
        /// </summary>
        /// <value>The tail node.</value>
        public Node<T> Tail { get; private set; }

        /// <summary>
        /// Gets a value indicating whether this <see cref="T:CSharpBasics.LinkedList`1"/> is doubly linked.
        /// </summary>
        /// <value><c>true</c> if is doubly linked; otherwise, <c>false</c>.</value>
        public bool IsDoublyLinked { get; private set; }
        #endregion Properties

        #region Constructors
        public LinkedList(Node<T> head)
        {
            this.Head = head ?? throw new ArgumentException("A linked list requires a head node.");
            this.IsDoublyLinked = head.IsDoublyLinked;

            // Set the tail. O(n).
            this.Tail = head;
            while (this.Tail.Next != null)
            {
                this.Tail = this.Tail.Next;
                this.Count++;
            }
        }
        #endregion Constructors

        #region Methods
        /// <summary>
        /// Insert the specified node into the list.
        /// </summary>
        /// <returns></returns>
        /// <param name="node">Node to insert.</param>
        /// <param name="index">Position to insert the node at. Defaults to the end of the list.</param>
        /*public void Insert(Node<T> node, int index = -1)
        {
            if (node == null)
            {
                return;
            }

            if (!NodeIsCompatible(node))
            {
                throw new ArgumentException($"Cannot insert node that is {(node.IsDoublyLinked ? string.Empty : "not ")}doubly " +
                                            $"linked into a list that is{(this.IsDoublyLinked ? "." : " not.")}");
            }

            // Insert at the end of the list if no index specified.
            if (index < 0)
            {
                // If doubly linked, link the previous to the end of our current list.
                if (this.IsDoublyLinked)
                {
                    node.Prev = this.Tail;
                }

                this.Tail.Next = node;

                // Set the tail back to the end of the list.
                while (this.Tail.Next != null)
                {
                    this.Tail = this.Tail.Next;
                }
            }
            else
            {
                // If it's doubly linked and the insert point is closer to the tail, then start from the tail.
                bool startFromHead = !this.IsDoublyLinked || (this.Count / index) > 2;

                if (startFromHead)
                {

                }
                // It's doubly linked and should be iterated starting at the tail.
                else
                {
                    uint cur = this.Count - 1;
                }
            }
        }*/

        /// <summary>
        /// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:CSharpBasics.LinkedList`1"/>.
        /// </summary>
        /// <returns>A <see cref="T:System.String"/> that represents the current <see cref="T:CSharpBasics.LinkedList`1"/>.</returns>
        public override string ToString()
        {
            string separator = this.IsDoublyLinked ? "<->" : "->";
            string output = Head.Data.ToString();
            Node<T> temp = this.Head.Next;

            while (temp != null)
            {
                output += $" {separator} {temp.Data.ToString()}";
                temp = temp.Next;
            }

            return output;
        }
        #endregion Methods

        #region Helpers
        /// <summary>
        /// Determines if a given node is compatible with the current list.
        /// </summary>
        /// <returns><c>true</c>, if is a compatible node, <c>false</c> otherwise.</returns>
        /// <param name="node">Node to check.</param>
        private bool NodeIsCompatible(Node<T> node)
        {
            return this.IsDoublyLinked == node.IsDoublyLinked;
        }
        #endregion Helpers
    }
}
