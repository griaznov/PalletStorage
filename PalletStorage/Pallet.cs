using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace Ex.PalletStorage;

public class Pallet : StorageBox
{
    private const double defaultPalletWeight = 30;

    private readonly List<StorageBox>? boxes = new();

    public Pallet(double width, double length, double height, double weight,
        DateTime productionDate = default,
        DateTime expirationDate = default,
        string id = "",
        double volume = 0,
        List<StorageBox>? boxes = null) 

        : base(width, length, height, weight, productionDate, expirationDate, id, volume)
    {
        // default weight value for the pallet
        this.weight = defaultPalletWeight;
        this.boxes = boxes;

        if (boxes == null)
        {
            this.boxes = new List<StorageBox>();
        }
    }

    public virtual List<StorageBox> Boxes
    {
        get { return boxes ?? new List<StorageBox>(); }
    }

    public override double Weight
    {
        get { return (weight + boxes.Sum(b => b.Weight)); }
    }

    public override double Volume
    {
        get { return (volume + boxes.Sum(b => b.Volume)); }
    }

    public DateTime MaxExpirationDate
    {
        get { 

            if (boxes.Count == 0)
            {
                return DateTime.MinValue;
            }

            return boxes.Max(b => b.ExpirationDate); 
        
        }
    }

    public override DateTime ExpirationDate
    {
        get 
        { 
            if (boxes.Count == 0)
            {
                return DateTime.MinValue;
            }

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

    public bool AddBox(StorageBox? box)
    {
        if ((this == null) || (box == null))
        {
            WriteLine($"The pallet and the box must be identified!");
            return false; 
        }

        boxes.Add(box);

        return true;
    }

    public static Pallet? Create(double width,
        double length,
        double height,
        DateTime prodDate = default)
    {
        // default weight value for the pallet
        double weight = defaultPalletWeight;

        if (prodDate == DateTime.MinValue)
        {
            prodDate = DateTime.Today;
        }

        try
        {
            return new Pallet(width, length, height, weight, prodDate);
        }
        catch
        {
            return null;
        }
    }
}

