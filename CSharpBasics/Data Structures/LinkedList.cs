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
            this.Prev = prev;
        }

        /// <summary>
        /// Makes a hard copy of the node passed in.
        /// </summary>
        /// <param name="copy">Copy.</param>
        public Node(Node<T> copy)
        {
            if (copy == null)
            {
                throw new ArgumentException("A non-null node must be passed in for copying.");
            }

            this.IsDoublyLinked = copy.IsDoublyLinked;
            this.Data = copy.Data;
            this.Next = copy.Next;
            this.Prev = copy.Prev;
        }
        #endregion Constructors
    }

    /// <summary>
    /// Linked list. Type T must have a public parameterless constructor.
    /// </summary>
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
        /// <summary>
        /// Initializes a new instance of the <see cref="T:CSharpBasics.LinkedList`1"/> class.
        /// </summary>
        /// <param name="head">The head node. A hard copy is made and inserted into the list.</param>
        public LinkedList(Node<T> head)
        {
            this.IsDoublyLinked = head.IsDoublyLinked;
            this.Head = new Node<T>(head);

            // Sanity check. We don't want any previous links for the head.
            this.Head.Prev = null;

            // Make hard copies of the rest of the nodes and set the tail. O(n).
            this.Tail = this.Head;
            while (this.Tail.Next != null)
            {
                this.Tail.Next = new Node<T>(this.Tail.Next);
                this.Tail.Next.Prev = this.Tail;
                this.Tail = this.Tail.Next;
                this.Count++;
            }
        }
        #endregion Constructors

        #region Methods
        /// <summary>
        /// Insert a hard copy of the specified node into the list.
        /// </summary>
        /// <returns></returns>
        /// <param name="node">Node to insert.</param>
        /// <param name="index">Position to insert the node at. Defaults to the end of the list.</param>
        public void Insert(Node<T> node, int index = -1)
        {
            if (node == null)
            {
                return;
            }

            Node<T> newNode = new Node<T>(node);

            if (!NodeIsCompatible(newNode))
            {
                throw new ArgumentException($"Cannot insert node that is {(newNode.IsDoublyLinked ? string.Empty : "not ")}doubly " +
                                            $"linked into a list that is{(this.IsDoublyLinked ? "." : " not.")}");
            }

            // Insert at the end of the list if no index specified or it's bigger than the current size.
            if (index < 0 || this.Count <= index)
            {
                // If doubly linked, link the previous to the end of our current list.
                if (this.IsDoublyLinked)
                {
                    newNode.Prev = this.Tail;
                }

                this.Tail.Next = newNode;
                this.Tail = this.Tail.Next;
                this.Count++;

                // Set the tail back to the end of the list while making hard copies as we go. O(n).
                while (this.Tail.Next != null)
                {
                    this.Tail.Next = new Node<T>(this.Tail.Next);
                    this.Tail.Next.Prev = this.Tail;
                    this.Tail = this.Tail.Next;
                    this.Count++;
                }
            }
            else
            {
                // If it's doubly linked and the insert point is closer to the tail, then start from the tail.
                bool startFromHead = !this.IsDoublyLinked || (this.Count * 1.0 / index) > 2.0;
                uint cur;
                Node<T> temp, preTemp = null;

                // Could be singly or doubly linked, and we're starting from the head.
                if (startFromHead)
                {
                    cur = 0;
                    temp = this.Head;

                    while (cur < index)
                    {
                        preTemp = temp;
                        temp = temp.Next;
                        cur++;
                    }
                }
                // It's doubly linked and should be iterated starting at the tail.
                else
                {
                    cur = this.Count - 1;
                    temp = this.Tail;

                    while (cur > index)
                    {
                        temp = temp.Prev;
                        cur--;
                    }

                    preTemp = temp.Prev;
                }

                // Connect to the leading nodes.
                newNode.Prev = temp.Prev;
                if (preTemp != null)
                {
                    // If we're not inserting at the front, then connect the beginning of the list.
                    preTemp.Next = newNode;
                }

                // Make copies of the rest of the chain inserted.
                Node<T> temp2 = newNode;
                this.Count++;
                while (temp2.Next != null)
                {
                    temp2.Next = new Node<T>(temp2.Next);
                    temp2.Next.Prev = temp2;
                    temp2 = temp2.Next;
                    this.Count++;
                }

                // Connect the inserted segment to the trailing nodes.
                temp.Prev = temp2;
                temp2.Next = temp;

                // If we inserted at the head, then set it to the new starting node.
                if (index == 0)
                {
                    this.Head = newNode;
                }
            }
        }

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
