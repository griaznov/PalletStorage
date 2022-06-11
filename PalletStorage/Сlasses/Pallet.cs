using System.Text.Json.Serialization;

namespace PalletStorage.Сlasses;

public class Pallet : UniversalBox
{
    private const double DefaultPalletWeight = 30;

    private string id;
    private readonly List<StorageBox> boxes = new();

    public Pallet(double width,
        double length,
        double height,
        double weight = 0,
        DateTime expirationDate = default,
        string id = "",
        double volume = 0,
        List<StorageBox>? boxes = null) 

        : base(width, length, height, weight)
    {
        // default weight value for the pallet
        this.weight = DefaultPalletWeight;
        this.boxes = boxes ?? new List<StorageBox>();
        this.id = id;

        if (string.IsNullOrEmpty(this.id))
        {
            this.id = Guid.NewGuid().ToString();
        }
    }

    // Pallet's own weight
    [JsonIgnore]
    public virtual double PalletWeight => weight;
    public virtual string Id => id;
    public virtual List<StorageBox> Boxes => boxes ?? new List<StorageBox>();
    public override double Weight => (weight + boxes.Sum(b => b.Weight));
    public override double Volume => (volume + boxes.Sum(b => b.Volume));

    public DateTime ExpirationDate
    {
        get 
        { 
            if (boxes.Count == 0) { return DateTime.MinValue; }
            return boxes.Min(box => box.ExpirationDate); 
        }
    }

    public void AddBox(StorageBox box)
    {
        boxes.Add(box);
    }

    public static Pallet? Create(double width, double length, double height)
    {
        // default weight value for the pallet
        double weight = DefaultPalletWeight;

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

