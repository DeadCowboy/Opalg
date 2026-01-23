using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

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

    /// <summary>
    /// Construct an inital Solution. To make it purposly bad, each rect has its own box
    /// </summary>
    /// <param name="rectCount"></param>
    /// <param name="minLength"></param>
    /// <param name="maxLength"></param>
    /// <param name="boxSize"></param>
    public GeometricSolution(int rectCount, int minLength, int maxLength, int boxSize)
    {
        if (maxLength > boxSize) throw new ArgumentException("Max rect size can not exceed box size");
        List<Box> boxes = [];
        Random rnd = new Random();
        for (int i = 0; i < rectCount; i++)
        {
            Box newBox = new Box(boxSize);
            int length = rnd.Next(minLength, maxLength + 1);
            int width = rnd.Next(minLength, maxLength + 1);
            Rectangle newRect = new Rectangle(length, width);
            PositionedRect newPosRect = new PositionedRect(newRect, 0, 0, false);
            newBox.Attach(newPosRect);
            boxes.Add(newBox);
        }
        this._boxes = boxes;
    }

    public double Cost()  // Try with Gini coefficient instead?
    {
        // Integrating the discrete difference
        // Thats why we carry +C kids
        List<Box> sortedBoxes = [.. this.Boxes];
        sortedBoxes.Sort(Box.CompareByFillgrade);
        // Idea: Square to incentivice concentration at the high end
        double inequality = sortedBoxes.Zip(sortedBoxes.Skip(1), (first, second) => second.Fillgrade() - first.Fillgrade()).Sum(); // Larger value is better
        return 1 - inequality;
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