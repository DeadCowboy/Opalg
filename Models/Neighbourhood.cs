
using System;

namespace Opalg.Models;

interface INeighbourhood<T>
{
    public bool Sample(T start);
}

class GeoNeighbourhood : INeighbourhood<GeometricSolution>
{
    /// <summary>
    /// Sets the number of different boxes that are tried before giving up
    /// </summary>
    public int NumBoxTrys = 5;
    private readonly Random rnd = new();
    public bool Sample(GeometricSolution start)
    {
        // Select rectangle
        Box randomStartBox = start.Boxes[rnd.Next(start.Boxes.Count)];
        PositionedRect randomRect = randomStartBox.Detach(rnd.Next(randomStartBox.rectangles.Count));

        // Safe rect Position
        PositionState startState = randomRect.GetState();


        // Rotate?
        if (rnd.NextDouble() < 0.5) randomRect.isRotated = !randomRect.isRotated;

        // Select random box
        Box trialBox = start.Boxes[rnd.Next(start.Boxes.Count)];

        // Reset Position of rect to zeros
        randomRect.ZeroOutPos();

        // Place in Box if possible
        bool wasPlaced = false;
        bool[,] denseGrid = trialBox.GenDenseGrid();

        for (int i = 0; i < this.NumBoxTrys; i++)
        {
            for (int x = 0; x + randomRect.Width <= trialBox.size; x++)
            {
                randomRect.MoveRight();
                for (int y = 0; y + randomRect.Height <= trialBox.size; y++)
                {
                    randomRect.MoveUp();
                    if (fits(denseGrid, randomRect))
                    {
                        trialBox.Attach(randomRect);
                        wasPlaced = true;
                        break;
                    }
                }
            }
        }

        if (!wasPlaced)
        {
            randomRect.SetState(startState);
            randomStartBox.Attach(randomRect);
        }

        return wasPlaced;

        // Rotate if not possible?

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
    public bool Sample(RuleSolution start)
    {
        throw new NotImplementedException();
    }
}

class OverlapNeighbourhood : INeighbourhood<OverlappingSolution>
{
    public bool Sample(OverlappingSolution start)
    {
        throw new NotImplementedException();
    }
}