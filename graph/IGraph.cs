namespace graph;
public interface IGraph
{
    public abstract Dictionary<string,Dictionary<string, int>> AddVertexEdge(string fromNode, string toNode, int weight);

    public abstract  Dictionary<string,Dictionary<string, int>> getGraph();
    

}