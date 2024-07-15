namespace graph;

public class Graph : IGraph
{
    private Dictionary<string, Dictionary<string, int>> graph = new Dictionary<string, Dictionary<string, int>>();
    public Dictionary<string, Dictionary<string, int>> AddVertexEdge(string fromNode, string toNode, int weight)
    {
        var adjNode = new Dictionary<string, int>
        {
            { toNode, weight }
        };
        var adjNodes = new Dictionary<string, int>();
        if (!graph.TryGetValue(fromNode, out adjNodes))
        {
            graph.Add(fromNode, adjNode);
        }
        else
        {
            adjNodes[toNode] = weight;
        }
        return graph;
    }
    public Dictionary<string, Dictionary<string, int>> getGraph()
    {
        return graph;
    }
}


enum GraphTraverseType
{
    Count,
    Weight
}