using System;

namespace Opalg.Models;

interface IOptimizer<SolutionType>
{
    public SolutionType Optimize(SolutionType initial, INeighbourhood<SolutionType> neighbourhood);
}

class NaiveLocalSearch<SolutionType> : IOptimizer<SolutionType> where SolutionType : ISolution
{
    public int explorationLimit = 4000;
    public SolutionType Optimize(SolutionType initial, INeighbourhood<SolutionType> neighbourhood)
    {
        if (initial == null) throw new ArgumentException("Starting Solution for optimizer was null");

        SolutionType currentSolution = initial;
        SolutionType bestSolution = initial;
        double bestCost = initial.Cost();
        for (int i = 0; i < explorationLimit; i++)
        {
            SolutionType newSolution = neighbourhood.Sample(currentSolution);
            if (newSolution.Cost() < bestCost)
            {
                bestSolution = newSolution;
                bestCost = newSolution.Cost();
            }
        }
        return bestSolution;
    }
}

class SimulatedAnneling<SolutionType> : IOptimizer<SolutionType>
{
    public SolutionType Optimize(SolutionType problem, INeighbourhood<SolutionType> neighbourhood)
    {
        throw new System.NotImplementedException();
    }
}

class GreedySearch<SolutionType> : IOptimizer<SolutionType>
{
    public SolutionType Optimize(SolutionType problem, INeighbourhood<SolutionType> neighbourhood)
    {
        throw new System.NotImplementedException();
    }
}