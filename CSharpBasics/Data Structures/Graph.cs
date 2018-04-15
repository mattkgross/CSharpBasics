﻿using System;
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



            return path;
        }
    }
}
