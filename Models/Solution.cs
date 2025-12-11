using System.Collections.Generic;

namespace Opalg.Models;

interface ISolution
{
    public double Cost();
}

class GeometricSolution : ISolution
{
    /// <summary>
    /// Clone Constructor
    /// Creates a deep cloned instance of a GeometricSolution
    /// </summary>
    /// <param name="initSolution"></param>
    public GeometricSolution(GeometricSolution initSolution)
    {
        List<Box> newBoxList = [];
        foreach (Box box in initSolution.Boxes)
        {
            newBoxList.Add(box.Clone());
        }
        this.Boxes = newBoxList;
    }

    public GeometricSolution(List<Box> Boxes)
    {
        this.Boxes = Boxes;
    }
    public List<Box> Boxes;
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