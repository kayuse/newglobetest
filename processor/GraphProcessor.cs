using System.Xml.Schema;
using graph;
namespace processor;

public class GraphProcessor : IGraphProcessor
{
    private List<string> distanceWeights = new List<string>();
    private List<string> reached = new List<string>();
    private List<string> paths = new List<string>();
    private int currentDecisionValue = 0;
    private IGraph graph;
    public GraphProcessor(IGraph graph)
    {
        this.graph = graph;
    }
    public bool init(List<string> distanceWeights)
    {
        this.distanceWeights = distanceWeights;
        if (!this.validateAndExtract())
        {
            return false;
        }
        return true;
    }
    private bool validateAndExtract()
    {
        var totalDistance = distanceWeights.Count;
        var totalValidInput = 0;
        foreach (string distanceWeight in distanceWeights)
        {
            int weight = -1;
            var weightInString = distanceWeight[distanceWeight.Length - 1].ToString();
            bool IsSuccessful = int.TryParse(weightInString, out weight);
            var distance = distanceWeight.Substring(0, 2);
            if (distanceWeight.Length == 3 && IsSuccessful)
            {
                totalValidInput += 1;
                var fromNode = distanceWeight.Substring(0, 1);
                var toNode = distanceWeight.Substring(1, 1);
                this.graph.AddVertexEdge(fromNode, toNode, weight);
            }
        }
        return totalValidInput == totalDistance;
    }
    public int GetShortestPath(string startingNode, string endNode)
    {
        var shortest = this.shortestPathC(startingNode);
        foreach (var sh in shortest.Keys)
        {
            if (sh == endNode)
            {
                return shortest[sh];
            }
        }
        return -1;
    }
    public int computeDistance(List<string> cities)
    {
        var totalWeight = 0;
        var nodeEdge = this.graph.getGraph();
        for (var i = 0; i < cities.Count - 1; i++)
        {
            Dictionary<string, int> adjascentNode = new Dictionary<string, int>();
            int weight = -1;
            if (!nodeEdge.TryGetValue(cities[i], out adjascentNode))
            {
                return weight;
            }

            var weightGotten = adjascentNode.TryGetValue(cities[i + 1], out weight);
            if (weightGotten)
            {
                totalWeight += weight;
            }
            else
            {
                return -1;
            }

        }
        return totalWeight;
    }
    public int calcDistanceStops(string startingNode, string endNode, int decisionValue, bool AllowLesser = true)
    {
        this.paths = new List<string>();
        this.currentDecisionValue = decisionValue + 1;
        this.traverseUntil(startingNode, startingNode);
        var totalStops = 0;
        foreach (var path in this.paths)
        {
            if (path.Substring(0, 1) == startingNode && path.Substring(path.Length - 1, 1) == endNode)
            {
                if (AllowLesser)
                {
                    totalStops += 1;
                }
                else
                {
                    totalStops = path.Length == decisionValue + 1 ? totalStops + 1 : totalStops + 0;
                }
            }
        }
        return totalStops;
    }
    public int calcDistanceWeights(string startingNode, string endNode, int decisionValue)
    {
        this.paths = new List<string>();
        this.currentDecisionValue = decisionValue;
        this.traverseUntilWeight(startingNode, startingNode);
        var totalStops = 0;
        foreach (var path in this.paths)
        {
            if (path.Substring(0, 1) == startingNode && path.Substring(path.Length - 1, 1) == endNode)
            {
                totalStops += 1;

            }
        }
        return totalStops;
    }
    private void traverseUntil(String startingNode, string currentPath)
    {
        var graph = this.graph.getGraph();
        var adjNodes = new Dictionary<string, int>();
        var adjExists = graph.TryGetValue(startingNode, out adjNodes);
        if (adjExists)
        {

            foreach (var name in adjNodes.Keys)
            {
                currentPath = currentPath + name;
                if (!this.paths.Contains(currentPath) && currentPath.Length <= this.currentDecisionValue)
                {
                    this.paths.Add(currentPath);
                    this.traverseUntil(name, currentPath);
                }
                currentPath = currentPath.Substring(0, currentPath.Length - 1);
            }
        }
    }
    private void traverseUntilWeight(String startingNode, string currentPath, int weight = 0)
    {
        var graph = this.graph.getGraph();
        var adjNodes = new Dictionary<string, int>();
        var adjExists = graph.TryGetValue(startingNode, out adjNodes);
        if (adjExists)
        {

            foreach (var name in adjNodes.Keys)
            {
                currentPath = currentPath + name;
                weight += adjNodes[name];
                if (!this.paths.Contains(currentPath) && weight < this.currentDecisionValue)
                {
                    this.paths.Add(currentPath);
                    this.traverseUntilWeight(name, currentPath, weight);
                }
                currentPath = currentPath.Substring(0, currentPath.Length - 1);
                weight -= adjNodes[name];
            }
        }
    }
    public Dictionary<string, int> shortestPathC(string startingNode)
    {
        var run = true;
        var graph = this.graph.getGraph();
        Dictionary<string, int> pathDistance = new Dictionary<string, int>();
        List<string> nodes = this.traverseOnce(startingNode);
        bool JustStarting = true;
        foreach (var node in nodes)
        {
            pathDistance.Add(node, 0);
        }
        var currentNode = startingNode;
        while (run)
        {
            if (!JustStarting)
                nodes.Remove(currentNode);
            else
                JustStarting = false;

            var adjNodes = new Dictionary<string, int>();
            var adjExists = graph.TryGetValue(currentNode, out adjNodes);
            if (adjExists)
            {
                foreach (var name in adjNodes.Keys)
                {
                    if (nodes.Contains(name))
                    {
                        var calcDistance = pathDistance[currentNode] + adjNodes[name];
                        if (calcDistance <= pathDistance[name] || pathDistance[name] <= 0)
                        {
                            pathDistance[name] = calcDistance;
                        }
                    }
                }
                currentNode = GetClosestToNode(startingNode, nodes, pathDistance);
            }
            run = nodes.Count > 0;
        }
        return pathDistance;
    }
    private string GetClosestToNode(string startingNode, List<string> nodes, Dictionary<string, int> distancePath)
    {
        var lowest = 0;
        var lowestNode = startingNode;
        var sortedDict = distancePath.OrderBy(kv => kv.Value).ToDictionary(kv => kv.Key, kv => kv.Value);

        foreach (var key in sortedDict.Keys)
        {
            if (nodes.Contains(key) && sortedDict[key] > 0)
            {
                lowestNode = key;
                lowest = distancePath[key];
                break;
            }
        }
        return lowestNode;
    }
    public List<string> traverseOnce(String node)
    {
        var graph = this.graph.getGraph();
        this.reached.Add(node);
        var adjNodes = new Dictionary<string, int>();
        var adjExists = graph.TryGetValue(node, out adjNodes);
        if (adjExists)
        {
            foreach (var name in adjNodes.Keys)
            {
                if (!this.reached.Contains(name))
                {
                    this.traverseOnce(name);
                }
            }
        }
        return this.reached;
    }
    public static List<string> breakInputToCities(string inputs)
    {
        return new List<string>(inputs.Split("-"));

    }
}
