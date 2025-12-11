namespace Opalg.Models;

readonly struct Rectangle(int Width, int Height)
{
    public readonly int Height = Height;
    public readonly int Width = Width;
}

record PositionState(int xCoord, int yCoord, bool isRotated)
{
    public int xCoord = xCoord;
    public int yCoord = yCoord;
    public bool isRotated = isRotated;
}

class PositionedRect(Rectangle rectangle, int xCoord, int yCoord, bool isRotated)
{
    public Rectangle rectangle = rectangle;

    /// Position is defined by the lower left corner
    public int xCoord = xCoord;
    /// Position is defined by the lower left corner
    public int yCoord = yCoord;

    public bool isRotated = isRotated;

    public int Height => this.isRotated ? this.rectangle.Width : this.rectangle.Height;
    public int Width => this.isRotated ? this.rectangle.Height : this.rectangle.Width;


    public PositionedRect Clone()
    {
        return new PositionedRect(this.rectangle, this.xCoord, this.yCoord, this.isRotated);
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