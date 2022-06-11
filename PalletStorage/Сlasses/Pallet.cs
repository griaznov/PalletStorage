using System.Text.Json.Serialization;

namespace PalletStorage.Сlasses;

public class Pallet : UniversalBox
{
    private const double DefaultPalletWeight = 30;

    public double PalletWeight { get; }
    public Guid Id { get; }
    public List<StorageBox> Boxes { get; }
    public override double Weight => (PalletWeight + Boxes.Sum(b => b.Weight));
    public override double Volume => (base.Volume + Boxes.Sum(b => b.Volume));
    public DateTime ExpirationDate => Boxes.Count == 0 ? default : Boxes.Min(box => box.ExpirationDate);

    public Pallet(double width,
        double length,
        double height,
        double weight = 0,
        Guid id = default,
        List<StorageBox>? boxes = null)
        : base(width, length, height, weight)
    {
        // default weight value for the pallet
        //this.weight = DefaultPalletWeight;
        PalletWeight = DefaultPalletWeight;
        Boxes = boxes ?? new List<StorageBox>();
        Id = id;

        if (Id == default) { Id = Guid.NewGuid(); }
    }

    public void AddBox(StorageBox box)
    {
        Boxes.Add(box);
    }

    public static Pallet Create(double width, double length, double height)
    {
        return new Pallet(width, length, height, DefaultPalletWeight);
    }
}

