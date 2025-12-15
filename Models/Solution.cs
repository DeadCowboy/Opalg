using System.Collections.Generic;

namespace Opalg.Models;

public interface ISolution
{
    public List<Box> Boxes { get; set; }
    public double Cost();
}

public class GeometricSolution : ISolution
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
        this._boxes = newBoxList;
    }

    public GeometricSolution(List<Box> Boxes)
    {
        this._boxes = Boxes;
    }

    private List<Box> _boxes;
    public List<Box> Boxes { get => _boxes; set => _boxes = value; }
    double ISolution.Cost()
    {
        throw new System.NotImplementedException();
    }

}

public class RuleSolution : ISolution
{
    public RuleSolution(List<Box> Boxes)
    {
        this._boxes = Boxes;
    }
    private List<Box> _boxes;
    public List<Box> Boxes { get => _boxes; set => _boxes = value; }
    public double Cost()
    {
        throw new System.NotImplementedException();
    }
}

public class OverlappingSolution : ISolution
{
    public OverlappingSolution(List<Box> Boxes)
    {
        this._boxes = Boxes;
    }
    private List<Box> _boxes;
    public List<Box> Boxes { get => _boxes; set => _boxes = value; }
    public double Cost()
    {
        throw new System.NotImplementedException();
    }
}