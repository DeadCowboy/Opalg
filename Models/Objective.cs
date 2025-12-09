namespace Opalg.Models;

interface IObjective
{
    public double evaluate(Solution solution);
}

class Objective : IObjective
{
    public double evaluate(Solution solution)
    {
        return 0;
    }
}