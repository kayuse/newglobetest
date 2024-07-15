using NUnit.Framework;
namespace test;
using System.Collections.Generic;
using graph;
using processor;

[TestFixture]
public class GraphProcessorTest
{
    private IGraph? _graph;
    private IGraphProcessor _processor;

    [SetUp]
    public void Setup()
    {
        _graph = new Graph();
        _processor = new GraphProcessor(_graph);
        var inputs = new List<string>();
        inputs.AddRange(new List<string>{
            "AB5",
            "BC4",
            "CD8",
            "DC8",
            "DE6",
            "AD5",
            "CE2",
            "EB3",
            "AE7"
        });
        _ = _processor.init(inputs);
    }

    [Test]
    public void TestInputs()
    {
        // Arrange
        var inputs = new List<string>();
        inputs.AddRange(new List<string>{
            "AB5",
            "BC4",
            "CD8",
            "DC8",
            "DE6",
            "AD5",
            "CE2",
            "EB3",
            "AE7"
        });
        var isInitialized = _processor.init(inputs);


        // Assert
        Assert.That(isInitialized, Is.True, "");
        inputs.AddRange(new List<string>{
            "D8",
            "DE6",
        });
        isInitialized = _processor.init(inputs);
        Assert.That(isInitialized, Is.False, "");
    }
    [Test]
    public void TestDistance()
    {
        var distance = _processor.computeDistance(new List<string>() { "A", "B", "C" });

        // Assert
        Assert.That(distance, Is.EqualTo(9), "");
    }
     public void TestDistanceStops()
    {
        var distance = _processor.calcDistanceStops("C", "C", 3, true);

        // Assert
        Assert.That(distance, Is.EqualTo(2), "");
    }
      public void TestShortestPath()
    {
        var distance = _processor.GetShortestPath("A", "C");

        // Assert
        Assert.That(distance, Is.EqualTo(9), "");
    }
      public void TestDistanceWeight()
    {
        var distance = _processor.calcDistanceWeights("C", "C", 30);

        // Assert
        Assert.That(distance, Is.EqualTo(7), "");
    }
}