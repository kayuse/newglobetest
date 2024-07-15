using NUnit.Framework;
namespace test;
using System.Collections.Generic;
using graph;

[TestFixture]
public class GraphTests
{
    private IGraph? _graph;

    [SetUp]
    public void Setup()
    {
        _graph = new Graph();

    }

    [Test]
    public void TestGraph()
    {
        // Arrange
        _ = _graph?.AddVertexEdge("A", "B", 5);

        var graph = _graph?.getGraph();

        // Assert
        Assert.That(graph?.Keys.Count, Is.EqualTo(1), "The Keys for an adjascent node should be the number of nodes entered");
        _graph?.AddVertexEdge("A", "C", 4);

        Assert.That(graph?.Keys.Count, Is.EqualTo(1), "");
        var graphA = graph["A"];
        Assert.That(graphA.Keys.Count, Is.EqualTo(2), "");

    }
}