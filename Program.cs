using graph;
using processor;

namespace dotnet;

class Program
{
    static void Main(string[] args)
    {
        /*
            The Algorithm        
        */
        IGraph graph = new Graph();
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
        IGraphProcessor processor = new GraphProcessor(graph);
        var initialized = processor.init(inputs);
        if (!initialized)
        {
            Console.WriteLine("Invalid inputs");
            return;
        }
        var distance = processor.computeDistance(new List<string>() { "A", "B", "C" });
        Console.WriteLine(distance <= 0 ? "No Such Route" : distance);
        var aebcd = processor.computeDistance(new List<string>() { "A", "E", "B", "C", "D" });
        Console.WriteLine(aebcd <= 0 ? "No Such Route" : aebcd);
        var aed = processor.computeDistance(new List<string>() { "A", "E", "D" });
        Console.WriteLine(aed <= 0 ? "No Such Route" : aed);
        var stops = processor.calcDistanceStops("C", "C", 3, true);
        Console.WriteLine(stops);
        var atoC = processor.calcDistanceStops("A", "C", 4, false);
        Console.WriteLine(atoC);
        var pathValAC = processor.GetShortestPath("A", "C");
        Console.WriteLine(pathValAC);
        var pathVal = processor.GetShortestPath("B", "B");
        Console.WriteLine(pathVal);
        var pathsWeight = processor.calcDistanceWeights("C", "C", 30);
        Console.WriteLine(pathsWeight);
        // A User Input Simulation
        Run();
    }
    static void Run()
    {
        IGraph graph = new Graph();
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
        IGraphProcessor processor = new GraphProcessor(graph);
        var initialized = processor.init(inputs);
        var run = true;
        while (run)
        {
            string prompt = "Hi There, What do you want to do? \n 1." +
            "Calculate Distance Between Cities \n 2. Calculate Shortest Distance \n 3. Calculate Total Distance with of Max Stops " +
            "\n 4. Calculate Total Distance with Weigths \n 5. Close";
            Console.WriteLine(prompt);
            string optionString = Console.ReadLine();
            int option;
            bool IsSuccessful = int.TryParse(optionString, out option);
            string citiesPrompt;
            int value;
            if (IsSuccessful)
            {
                switch (option)
                {
                    case 1:
                        prompt = "Please put city in this format A,B";
                        Console.WriteLine(prompt);
                        citiesPrompt = Console.ReadLine();
                        string[] citiesArr = citiesPrompt.Split(",");
                        List<string> cities = new List<string>(citiesArr);
                        int distance = processor.computeDistance(cities);
                        Console.WriteLine("The distance is " + distance);
                        break;
                    case 2:
                        prompt = "Please put city in this format A,B";
                        Console.WriteLine(prompt);
                        citiesPrompt = Console.ReadLine();
                        string[] shortestCities = citiesPrompt.Split(",");
                        int val = processor.GetShortestPath(shortestCities[0], shortestCities[1]);
                        Console.WriteLine("The distance is " + val);
                        break;
                    case 3:
                        prompt = "Please put city in this format A,B,3";
                        Console.WriteLine(prompt);
                        citiesPrompt = Console.ReadLine();
                        string[] shortestStops = citiesPrompt.Split(",");
                        var shortStopDecisionVal = 0;
                        int.TryParse(shortestStops[2], out shortStopDecisionVal);
                        int stopVal = processor.calcDistanceStops(shortestStops[0], shortestStops[1], shortStopDecisionVal, true);
                        Console.WriteLine("The distance is " + stopVal);
                        break;
                    case 4:
                        prompt = "Please put city in this format A,B,3";
                        Console.WriteLine(prompt);
                        citiesPrompt = Console.ReadLine();
                        string[] weightStops = citiesPrompt.Split(",");
                        var weightStopsDecisitionVal = 0;
                        int.TryParse(weightStops[2], out weightStopsDecisitionVal);
                        int weightVal = processor.calcDistanceWeights(weightStops[0], weightStops[1], weightStopsDecisitionVal);
                        Console.WriteLine("The distance is " + weightVal);
                        break;
                    case 5:
                        run = false;
                        Console.WriteLine("Thank you for trying this program, I hope we can proceed with the hiring strategy");
                        break;
                    default:
                        run = false;
                        break;
                }
            }
        }
    }
}
