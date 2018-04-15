using System;
using CSharpBasics.DataStructures;
using NUnit.Framework;

namespace CSharpBasics.Tests
{
    [TestFixture]
    public class GraphTest
    {
        private GraphNode<int> node1, node2, node3, node4, node5, node6;

        [SetUp]
        public void TestSetup()
        {
            // We're making this graph: https://upload.wikimedia.org/wikipedia/commons/5/57/Dijkstra_Animation.gif
            node1 = new GraphNode<int>(1);
            node2 = new GraphNode<int>(2);
            node3 = new GraphNode<int>(3);
            node4 = new GraphNode<int>(4);
            node5 = new GraphNode<int>(5);
            node6 = new GraphNode<int>(6);

            node1.AddNeighbor(node2, 7);
            node1.AddNeighbor(node3, 9);
            node1.AddNeighbor(node6, 14);

            node2.AddNeighbor(node3, 10);
            node2.AddNeighbor(node4, 15);

            node3.AddNeighbor(node4, 11);
            node3.AddNeighbor(node6, 2);

            node4.AddNeighbor(node5, 6);

            node5.AddNeighbor(node6, 9);
        }

        [Test]
        public void GraphNodeTest()
        {



        }

        [Test]
        public void DijkstraTest()
        {
            GraphNode<int> node1 = new GraphNode<int>(1);

        }
    }
}
