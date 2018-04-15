using System;
using System.Collections.Generic;

namespace CSharpBasics.DataStructures
{
    /* A graph can be represented in 3 ways: OOP, adjacency matrix, or an adjacency list.
     * https://www.geeksforgeeks.org/graph-and-its-representations/
     * 
     * OOP: Easy to visualize. Only need a pointer to one node in order to traverse the graph.
     *      However, it's harder to immediately look up relationships between nodes if you
     *      don't already have those nodes immediately accessible.
     *      Space: O(n)
     *      Time: Depends on algorithm. Greedy algorithms, like Dijkstra, are preferred.
     * Matrix: Useful when the data set is small, or extremely connected. The size will be O(n^2),
     *         since it's a full 2D array, corresponding each node with a possible connection to
     *         all the other nodes in the graph (the values being the weight between the nodes).
     *         If there are few connections and/or many nodes, then there will be a lot of wasted
     *         space.
     *         Time: O(1)
     *         Space: O(V^2)
     * List: Adding a vertex is easier - no need to reallocate an entire matrix each time, since we're
     *       using dynamic memory in form of a HashSet. Certain queries, though, such as whether there
     *       is an edge from vertex u to vertex v are not efficient and can be done O(V).
     *       Space: O(V + E)
     *       Time: O(1) to O(V)
     */

    public class GraphNode<T>
    {
        /// <summary>
        /// Gets or sets the data.
        /// </summary>
        /// <value>The data.</value>
        public T Data { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="T:CSharpBasics.DataStructures.GraphNode`1"/> is directed.
        /// </summary>
        /// <value><c>true</c> if is directed; otherwise, <c>false</c>.</value>
        public bool IsDirected { get; private set; }

        // C# Equivalent of a HashMap (unique keys).
        /// <summary>
        /// The neighboring nodes. The key is the Node, the value is the
        /// weight of the edge from the current node to the neighbor.
        /// </summary>
        public Dictionary<GraphNode<T>, int> Neighbors { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:CSharpBasics.DataStructures.GraphNode`1"/> class.
        /// </summary>
        /// <param name="data">Data.</param>
        /// <param name="isDirected">If set to <c>true</c> is directed.</param>
        /// <param name="neighbors">Neighbors.</param>
        public GraphNode(T data, bool isDirected = false, Dictionary<GraphNode<T>, int> neighbors = null)
        {
            this.IsDirected = isDirected;
            this.Data = data;
            this.Neighbors = neighbors ?? new Dictionary<GraphNode<T>, int>();
        }

        /// <summary>
        /// Adds the neighbor with its edge wait from the current node.
        /// </summary>
        /// <returns><c>true</c>, if neighbor was added, <c>false</c> already there.</returns>
        /// <param name="neighbor">The neighbor to add.</param>
        /// <param name="edgeWeight">Edge weight to the neighbor from the current node.</param>
        public bool AddNeighbor(GraphNode<T> neighbor, int edgeWeight)
        {
            // If the neighbor is already in the list, null, or not of the same directed
            // type, then do not add a reference and return false.
            if (neighbor == null || neighbor.IsDirected != this.IsDirected || this.Neighbors.ContainsKey(neighbor))
            {
                return false;
            }

            // Add an entry for the new neighbor to this node.
            this.Neighbors.Add(neighbor, edgeWeight);

            // Also add an entry to the new neighbor for this node if not already there
            // and it's not a directed graph.
            if (!this.IsDirected)
            {
                neighbor.AddNeighbor(this, edgeWeight);
            }

            return true;
        }
    }

    /// <summary>
    /// Graph object that wraps an adjecency list.
    /// </summary>
    public class Graph<T>
    {
        /// <summary>
        /// Gets a value indicating whether this <see cref="T:CSharpBasics.DataStructures.Graph`1"/> is directed.
        /// </summary>
        /// <value><c>true</c> if is directed; otherwise, <c>false</c>.</value>
        public bool IsDirected { get; private set; }

        private readonly HashSet<GraphNode<T>> nodes;
        /// <summary>
        /// Gets the nodes of the graph.
        /// </summary>
        /// <value>The nodes making up the graph.</value>
        public IReadOnlyCollection<GraphNode<T>> Nodes
        {
            get
            {
                return nodes;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:CSharpBasics.DataStructures.Graph`1"/> class.
        /// </summary>
        /// <param name="isDirected">If set to <c>true</c> is directed.</param>
        /// <param name="nodes">Nodes making up the graph.</param>
        public Graph(bool isDirected = false, HashSet<GraphNode<T>> nodes = null)
        {
            this.IsDirected = isDirected;
            this.nodes = nodes ?? new HashSet<GraphNode<T>>();
        }

        /// <summary>
        /// Adds the node to the graph. The node must already have its neighbors
        /// specified. We don't do that here.
        /// </summary>
        /// <returns><c>true</c>, if node was added, <c>false</c> if it was already present.</returns>
        /// <param name="node">Node to add to the graph.</param>
        public bool AddNode(GraphNode<T> node)
        {
            return this.nodes.Add(node);
        }

        /// <summary>
        /// Implements Dijkstra's algorithm to find the shortest path from
        /// the start node to the end node.
        /// </summary>
        /// <returns>The shortest path between the two nodes.</returns>
        /// <param name="startNode">The node to start from.</param>
        /// <param name="endNode">The node to navigate to.</param>
        public List<GraphNode<T>> ShortestPathToNode(GraphNode<T> startNode, GraphNode<T> endNode)
        {
            if (startNode == null || endNode == null)
            {
                throw new ArgumentException("You must specify a start and an end node.");
            }

            List<GraphNode<T>> path = new List<GraphNode<T>>();
            path.Add(startNode);

            // Step 1: Mark all nodes unvisited.
            //         We're creating a HashMap of Nodes with their corresponding 'visited' flag,
            //         and the current tentative distance to that node (default is infinity).
            //         Assign to every node a tentative distance value: set it to zero for our initial
            //         node and to infinity for all other nodes.
            Dictionary<GraphNode<T>, Tuple<bool, int>> visitedList = new Dictionary<GraphNode<T>, Tuple<bool, int>();
            foreach (GraphNode<T> node in this.Nodes)
            {
                visitedList.Add(node, new Tuple<bool, int>(false, node.Equals(startNode) ? 0 : int.MaxValue));
            }

            // Step 2:  Set the start node as current.
            GraphNode<T> current = startNode;

            while (current != null)
            {
                // Step 3: For the current node, consider all of its unvisited neighbors and calculate their
                //         tentative distances through the current node. Compare the newly calculated tentative
                //         distance to the current assigned value and assign the smaller one.

                GraphNode<T> nextBestNode = null;

                foreach (KeyValuePair<GraphNode<T>, int> neighbor in current.Neighbors)
                {
                    // If already visited, continue on.
                    if (visitedList[neighbor.Key].Item1)
                    {
                        continue;
                    }

                    // Calculate new tentative distance through current node.
                    // Add the value of the current node (visitedList[current].Item2) to the edge distance between
                    // the current node and the target node (neighbor.Value).
                    int tentativeDistance = visitedList[current].Item2 + neighbor.Value;

                    // If the target node was previously marked with a distance greater than this one,
                    // then change it, otherwise, keep in the same.
                    if (tentativeDistance < visitedList[neighbor.Key].Item2)
                    {
                        visitedList[neighbor.Key] = new Tuple<bool, int>(visitedList[neighbor.Key].Item1, tentativeDistance);
                    }

                    // If the current value calculated is the lowest we've seen so far, and that node hasn't yet been
                    // visited, then that's the next node to visit.
                    if (nextBestNode == null || (!visitedList[neighbor.Key].Item1 && visitedList[nextBestNode].Item2 > visitedList[neighbor.Key].Item2))
                    {
                        nextBestNode = neighbor.Key;
                    }
                }

                // Step 4: When we are done considering all of the neighbors of the current node, mark the current
                //         node as visited and remove it from the unvisited set. A visited node will never be checked again.
                visitedList[current] = new Tuple<bool, int>(true, visitedList[current].Item2);

                // Step 5: Move to the next unvisited node with the smallest tentative distance and repeat the above steps
                //         which check neighbors and mark visited. If there is no next node to visit, then set to null so we break.
                current = nextBestNode == current ? null : nextBestNode;

                // Step 6: If the destination node has been marked visited (when planning a route between two specific nodes) or if
                //         the smallest tentative distance among the nodes in the unvisited set is infinity (when planning a complete
                //         traversal; occurs when there is no connection between the initial node and remaining unvisited nodes),
                //         then stop. The algorithm has finished.
            }

            return path;
        }
    }
}
