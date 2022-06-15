using PalletStorage.Сlasses.Extensions;
using static System.Console;

namespace PalletStorage.Сlasses;

public class Storage
{
    public virtual Guid Id { get; }
    public virtual string Name { get; }
    public virtual IDictionary<Guid, StorageBox> Boxes { get; }
    public virtual IDictionary<Guid, Pallet> Pallets { get; }

    public Storage(string? name,
        Guid id = default,
        IDictionary<Guid, StorageBox>? boxes = default,
        IDictionary<Guid, Pallet>? pallets = default)
    {
        Boxes = boxes ?? new Dictionary<Guid, StorageBox>();
        Pallets = pallets ?? new Dictionary<Guid, Pallet>();

        if (string.IsNullOrEmpty(name)) { name = "Main storage"; }
        if (id == default) { id = Guid.NewGuid(); }

        Id = id;
        Name = name;
    }

    public Pallet AddPallet(double width, double length, double height)
    {
        // Creating an element
        var pallet = Pallet.Create(width, length, height);

        // Adding element to the Storage
        Pallets.Add(pallet.Id, pallet);

        return pallet;
    }

    public StorageBox AddBox(double width,
        double length,
        double height,
        double weight,
        DateTime prodDate = default,
        DateTime expDate = default)
    {
        // Creating an element
        var box = StorageBox.Create(width, length, height, weight, prodDate, expDate);

        Boxes.Add(box.Id, box);
        
        return box;
    }

    public void MoveBoxToPallet(StorageBox box, Pallet pallet)
    {
        // Moving the box on the pallet
        if (!Boxes.ContainsKey(box.Id))
        {
            Boxes.Add(box.Id, box); 
        }

        if (!pallet.Boxes.Contains(box))
        {
            pallet.AddBox(box);
        }
    }
}
