using static System.Console;

namespace Ex.PalletStorage;

public class UniversalBox : IUniversalBox
{
    // fields
    protected double height; // высота
    protected double width; // ширина
    protected double length;
    protected double weight; // вес

    protected double volume;

    public UniversalBox(double width, double length, double height, double weight)
    {
        // Verifying parameters
        if (!IsValidBoxParams(width, length, height) || !IsValidWeight(weight))
        {
            throw new ArgumentOutOfRangeException(
                "You need to enter the following required parameters: width, length, height, weight!");
        }

        this.height = height;
        this.width = width;
        this.length = length;
        this.weight = weight;

        volume = height * width * length;
    }

    // properties
    public virtual double Height
    {
        get { return height; }
    }

    public virtual double Width
    {
        get { return width; }
    }

    public virtual double Length
    {
        get { return length; }
    }

    public virtual double Weight
    {
        get { return weight; }
    }

    public virtual double Volume
    {
        get { return volume; }
    }

    public virtual void Print()
    {
        WriteLine($"Box: {width}/{length}/{height} (w/l/h), weight: {weight}, volume: {volume}");
    }

    public static UniversalBox? Create(double width, double length, double height, double weight)
    {
        try
        {
            return new UniversalBox(width, length, height, weight);
        }
        catch
        {
            return null;
        } 
    }

    public static bool IsValidBoxParams(double width, double length, double height)
    {
        if ((width <= 0) || (length <= 0) || (height <= 0))
        {
            return false;
        }

        return true;
    }

    public static bool IsValidWeight(double weight)
    {
        if (weight <= 0)
        {
            return false;
        }

        return true;
    }
}