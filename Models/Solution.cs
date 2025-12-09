using System.Collections.Generic;

namespace Opalg.Models;

interface ISolution
{
    public double Cost();
}

class GeometricSolution : ISolution
{
    public List<Box> Boxes = [];
    double ISolution.Cost()
    {
        throw new System.NotImplementedException();
    }

}

class RuleSolution : ISolution
{
    public double Cost()
    {
        throw new System.NotImplementedException();
    }
}

class OverlappingSolution : ISolution
{
    public double Cost()
    {
        throw new System.NotImplementedException();
    }
}