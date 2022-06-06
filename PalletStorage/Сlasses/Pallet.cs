using System.Text;
using System.Text.Json.Serialization;
using static System.Console;

namespace PalletStorage;

public class Pallet : UniversalBox
{
    private const double defaultPalletWeight = 30;
    
    protected string id;
    private readonly List<StorageBox> boxes = new();

    public Pallet(double width, double length, double height,
        double weight = 0,
        DateTime expirationDate = default,
        string id = "",
        double volume = 0,
        List<StorageBox>? boxes = null) 

        : base(width, length, height, weight)
    {
        // default weight value for the pallet
        this.weight = defaultPalletWeight;
        this.boxes = boxes ?? new List<StorageBox>();
        this.id = id;

        if (string.IsNullOrEmpty(this.id))
        {
            this.id = Guid.NewGuid().ToString();
        }
    }

    // Pallet's own weight
    [JsonIgnore]
    public virtual double PalletWeight { get { return weight; } }
    public virtual string ID { get { return id; } }
    public virtual List<StorageBox> Boxes { get { return boxes ?? new List<StorageBox>(); } }
    public override double Weight { get { return (weight + boxes.Sum(b => b.Weight)); } }
    public override double Volume { get { return (volume + boxes.Sum(b => b.Volume)); } }

    public DateTime ExpirationDate
    {
        get 
        { 
            if (boxes.Count == 0) { return DateTime.MinValue; }
            return boxes.Min(box => box.ExpirationDate); 
        }
    }

    public override void Print()
    {
        StringBuilder stringBuilder = new();

        stringBuilder.AppendFormat($"Pallet: id: {id}, (w/l/h): {width}/{length}/{height}, weight: {Weight}, volume: {Volume},");
        stringBuilder.AppendFormat($"exp.date: {ExpirationDate}, Count boxes: {boxes.Count}");

        WriteLine(stringBuilder.ToString());
    }

    public void AddBox(StorageBox box)
    {
        boxes.Add(box);
    }

    public static Pallet? Create(double width, double length, double height)
    {
        // default weight value for the pallet
        double weight = defaultPalletWeight;

        try
        {
            return new Pallet(width, length, height, weight);
        }
        catch
        {
            return null;
        }
    }
}

