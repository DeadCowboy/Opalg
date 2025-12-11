
using System;

namespace Opalg.Models;

interface INeighbourhood<T>
{
    public T Sample(T start);
}

class GeoNeighbourhood : INeighbourhood<GeometricSolution>
{
    /// <summary>
    /// Sets the number of different boxes that are tried before giving up
    /// </summary>
    public int NumBoxTrys = 5;
    private readonly Random rnd = new();
    public GeometricSolution Sample(GeometricSolution start)
    {
        // Deep Clone Solution
        GeometricSolution clonedSolution = new GeometricSolution(start);

        // Select rectangle to be moved
        Box box = clonedSolution.Boxes[rnd.Next(clonedSolution.Boxes.Count)];
        PositionedRect rect = box.Detach(rnd.Next(box.rectangles.Count));

        // Rotate rectangle randomly
        if (rnd.NextDouble() < 0.5) rect.isRotated = !rect.isRotated;

        // Select random box to place rectangle in
        Box trialBox = clonedSolution.Boxes[rnd.Next(clonedSolution.Boxes.Count)];

        // Place in Box if possible
        bool wasPlaced = false;
        bool[,] denseGrid = trialBox.GenDenseGrid();
        rect.ZeroOutPos();     // Reset Position of rect to zero zero

        for (int i = 0; i < this.NumBoxTrys; i++)
        {
            for (int x = 0; x + rect.Width <= trialBox.size; x++)
            {
                rect.MoveRight();
                for (int y = 0; y + rect.Height <= trialBox.size; y++)
                {
                    rect.MoveUp();
                    if (fits(denseGrid, rect))
                    {
                        trialBox.Attach(rect);
                        wasPlaced = true;
                        break;
                    }
                }
            }
        }

        return wasPlaced ? clonedSolution : start;

        // Rotate if not possible? Skip for now

        bool fits(bool[,] grid, PositionedRect rect)
        {
            bool overlaps = false;
            for (int x = rect.xCoord; x < rect.xCoord + rect.Width; x++)
            {
                for (int y = rect.yCoord; y < rect.yCoord + rect.Height; y++)
                {
                    overlaps |= grid[x, y];
                }
            }
            return !overlaps;
        }

    }
}

class RuleNeighbourhood : INeighbourhood<RuleSolution>
{
    public RuleSolution Sample(RuleSolution start)
    {
        throw new NotImplementedException();
    }
}

class OverlapNeighbourhood : INeighbourhood<OverlappingSolution>
{
    public OverlappingSolution Sample(OverlappingSolution start)
    {
        throw new NotImplementedException();
    }
}