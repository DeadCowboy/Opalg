namespace Opalg.Models;

interface IOptimizer
{
    public Solution Optimize(Problem problem, INeighbourhood neighbourhood);
}

class NaiveLocalSearch : IOptimizer
{
    public Solution Optimize(Problem problem, INeighbourhood neighbourhood)
    {
        Solution sNew = neighbourhood.sample();
    }
}

class SimulatedAnneling : IOptimizer
{
    public Solution Optimize(Problem problem, INeighbourhood neighbourhood)
    {
        Solution sNew = neighbourhood.sample();
    }
}

class GreedySearch : IOptimizer
{

}