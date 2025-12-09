using System;
using System.Collections.Generic;

namespace Opalg.Models;

class Box(int size)
{
    readonly public int size = size;
    public LinkedList<PositionedRect> rectangles = [];

    public Box(LinkedList<PositionedRect> rectangles, int size) : this(size)
    {
        this.rectangles = rectangles;
    }

    public PositionedRect Detach(int index)
    {
        PositionedRect toDetach;
        LinkedList<PositionedRect>.Enumerator rectEnumerator = this.rectangles.GetEnumerator();

        // Find index
        for (int i = 0; i <= index; i++)
        {
            bool hasNotEnded = rectEnumerator.MoveNext();
            if (!hasNotEnded) throw new ArgumentException("Linked List out of bounds");
        }
        toDetach = rectEnumerator.Current;

        // Remove and return
        this.rectangles.Remove(toDetach);
        return toDetach;

    }

    public PositionedRect Detach(PositionedRect rect)
    {
        LinkedListNode<PositionedRect> toDetach = this.rectangles.Find(rect) ?? throw new ArgumentException("Box does not contain this PositionedRect");
        this.rectangles.Remove(rect);
        return toDetach.Value;
    }

    public void Attach(PositionedRect newRect)
    {
        this.rectangles.AddFirst(newRect);
    }

    public double FillLevel()
    {
        int occupiedCellCount = 0;
        foreach (PositionedRect rect in this.rectangles)
        {
            occupiedCellCount += rect.Width * rect.Height;
        }

        return occupiedCellCount / (this.size * this.size);
    }

    public bool[,] GenDenseGrid()
    {
        bool[,] denseGrid = new bool[size, size];

        foreach (PositionedRect item in rectangles)
        {
            setCoveredCells(item);
        }

        return denseGrid;


        void setCoveredCells(PositionedRect rect)
        {
            for (int x = rect.xCoord; x < rect.xCoord + rect.rectangle.Width; x++)
            {
                for (int y = rect.yCoord; y < rect.yCoord + rect.rectangle.Height; y++)
                {
                    denseGrid[x, y] = true;
                }
            }
        }
    }

}