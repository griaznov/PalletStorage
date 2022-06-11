using PalletStorage.Сlasses.Extensions;
using static System.Console;

namespace PalletStorage.Сlasses;

public class Storage
{
    private readonly string id;
    private readonly string name;

    // Boxes.
    private readonly IDictionary<string, StorageBox> boxes;

    // Pallets. Pallets can contain boxes.
    private readonly IDictionary<string, Pallet> pallets;

    public Storage(string? name, 
        string id = "",
        IDictionary<string, StorageBox>? boxes = null,
        IDictionary<string, Pallet>? pallets = null)
    {
        this.boxes = boxes ?? new Dictionary<string, StorageBox>();
        this.pallets = pallets ?? new Dictionary<string, Pallet>();

        if (string.IsNullOrEmpty(name)) { name = "Main storage"; }
        if (string.IsNullOrEmpty(id)) { id = Guid.NewGuid().ToString(); }

        this.id = id;
        this.name = name;
    }

    public virtual string Id => id;
    public virtual string Name => name;
    public virtual IDictionary<string, StorageBox> Boxes => boxes;
    public virtual IDictionary<string, Pallet> Pallets => pallets;

    public Pallet? AddPallet(double width, double length, double height)
    {
        // Creating an element
        Pallet? pallet = Pallet.Create(width, length, height);

        // Adding element to the Storage
        if (pallet == null)
        {
            return null;
        }

        pallets.Add(pallet.Id, pallet);

        return pallet;
    }

    public StorageBox? AddBox(double width,
        double length,
        double height,
        double weight,
        DateTime prodDate = default,
        DateTime expDate = default)
    {
        // Creating an element
        StorageBox? box = StorageBox.Create(width, length, height, weight, prodDate, expDate);

        // Adding element to the Storage
        if (box == null)
        {
            return null;
        }

        boxes.Add(box.Id, box);
        
        return box;
    }

    public StorageBox? FindBox(string boxId)
    {
        if (!boxes.TryGetValue(boxId, out StorageBox? foundВox))
        {
            WriteLine($"The Storage could not find a box with ID: {boxId}");
        }
        
        return foundВox;
    }

    public Pallet? FindPallet(string palletId)
    {
        if (!pallets.TryGetValue(palletId, out Pallet? foundPallet))
        {
            WriteLine($"The Storage could not find a pallet with ID: {palletId}");
        }

        return foundPallet;
    }

    public void MoveBoxToPallet(StorageBox box, Pallet pallet)
    {
        // Moving the box on the pallet
        if (!boxes.ContainsKey(box.Id))
        {
            boxes.Add(box.Id, box); 
        }

        if (!pallet.Boxes.Contains(box))
        {
            pallet.AddBox(box);
        }
    }

    public void ReportPalletsOrderByExpirationAndWeight()
    {
        WriteLine();
        WriteLine($"Report: Pallets ordered by Expiration; with boxes, ordered by weight");
        WriteLine();

        var query = Pallets
            .GroupBy(p => p.Value.ExpirationDate)
            .Select(i => new { i.Key, sortValues = i.OrderBy(it => it.Value.Weight)})
            .OrderBy(p => p.Key);

        foreach (var item in query)
        {
            WriteLine($"Group: {item.Key}");

            foreach (var itemPallet in item.sortValues)
            {
                PalletExtensions.Print(itemPallet.Value);
            }
        }
    }

    public void ReportTopWithMaxExpirationOrderByVolume(int countRecords)
    {
        WriteLine();
        WriteLine($"Report: {countRecords} Pallet with Max expiration, ordered by volume");
        WriteLine();

        var qery = Pallets
            .OrderByDescending(p => p.Value.ExpirationDate)
            .Take(countRecords)
            .OrderBy(p => p.Value.Volume);

        foreach (var item in qery)
        {
            PalletExtensions.Print(item.Value);
        }
    }
}
