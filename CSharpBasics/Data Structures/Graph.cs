﻿using System;
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

    public class GraphNode
    {
        public GraphNode()
        {
        }
    }
}