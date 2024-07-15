public interface IGraphProcessor
{
    public List<string> traverseOnce(String node);
    public Dictionary<string, int> shortestPathC(string startingNode);
    public bool init(List<string> distanceWeights);
    public int GetShortestPath(string startingNode, string endNode);
    public int calcDistanceStops(string startingNode, string endNode, int decisionValue, bool AllowLesser);
    public int calcDistanceWeights(string startingNode, string endNode, int decisionValue);
    public int computeDistance(List<string> cities);

}