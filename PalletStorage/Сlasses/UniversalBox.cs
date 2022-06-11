namespace PalletStorage.Сlasses;

public class UniversalBox
{
    // fields
    protected double width;
    protected double length;
    protected double weight;
    protected double volume;

    public UniversalBox(double width, double length, double height, double weight)
    {
        // Verifying parameters
        if (!IsValidBoxParams(width, length, height) || !IsValidWeight(weight))
        {
            var errorMessage = "You need to enter the following required parameters: width, length, height, weight!";
            throw new ArgumentOutOfRangeException(errorMessage);
        }

        Height = height;
        this.width = width;
        this.length = length;
        this.weight = weight;

        volume = height * width * length;
    }

    // properties
    public virtual double Height { get; }

    public virtual double Width => width;
    public virtual double Length => length;
    public virtual double Weight => weight;
    public virtual double Volume => volume;

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
        return (!(width <= 0)) && (!(length <= 0)) && (!(height <= 0));
    }

    public static bool IsValidWeight(double weight)
    {
        return !(weight <= 0);
    }
}
