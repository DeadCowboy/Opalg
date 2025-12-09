namespace Opalg.Models;

interface IOptimizer<T>
{
    public T Optimize(Problem problem, INeighbourhood<T> neighbourhood);
}

class NaiveLocalSearch : IOptimizer<T>
{
    public T Optimize(Problem problem, INeighbourhood<T> neighbourhood)
    {
        T sNew = neighbourhood.Sample();
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