namespace Opalg.Models;

class Solver(Problem problem, INeighbourhood neighbourhood, IOptimizer optimizer)
{
    public Solution solve()
    {
        Solution optimizedSolution = optimizer.optimise(problem, neighbourhood);
        return optimizedSolution;
    }
}