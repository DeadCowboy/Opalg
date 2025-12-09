namespace Opalg.Models;

struct Rectangle
{
    public bool isRotated;
    public int Height
    {
        get { return isRotated ? Width : Height; }
        set
        {
            if (isRotated) { Width = value; }
            else { Height = value; }
        }
    }
    public int Width
    {
        get { return isRotated ? Height : Width; }
        set
        {
            if (isRotated) { Height = value; }
            else { Width = value; }
        }
    }
}

record PositionState(int xCoord, int yCoord, bool isRotated)
{
    public int xCoord = xCoord;
    public int yCoord = yCoord;
    public bool isRotated = isRotated;
}

class PositionedRect(Rectangle rectangle, int xCoord, int yCoord)
{
    public Rectangle rectangle = rectangle;

    /// Position is defined by the lower left corner
    public int xCoord = xCoord;
    /// Position is defined by the lower left corner
    public int yCoord = yCoord;

    public int Height
    {
        get { return this.rectangle.Height; }
        set { this.rectangle.Height = value; }
    }

    public int Width
    {
        get { return this.rectangle.Width; }
        set { this.rectangle.Width = value; }
    }

    public bool isRotated
    {
        get { return this.rectangle.isRotated; }
        set { this.rectangle.isRotated = value; }
    }

    public PositionState GetState()
    {
        return new PositionState(this.xCoord, this.yCoord, this.isRotated);
    }

    public void SetState(PositionState state)
    {
        this.xCoord = state.xCoord;
        this.yCoord = state.yCoord;
        this.isRotated = state.isRotated;
    }

    public void ZeroOutPos()
    {
        this.xCoord = 0;
        this.yCoord = 0;
    }

    public void MoveRight()
    {
        this.xCoord++;
    }

    public void MoveUp()
    {
        this.yCoord++;
    }
}