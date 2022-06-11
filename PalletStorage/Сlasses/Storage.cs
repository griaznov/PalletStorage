using PalletStorage.Сlasses.Extensions;
using static System.Console;

namespace PalletStorage.Сlasses;

public class Storage
{
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

        Id = id;
        Name = name;
    }

    public virtual string Id { get; }

    public virtual string Name { get; }

    public virtual IDictionary<string, StorageBox> Boxes => boxes;
    public virtual IDictionary<string, Pallet> Pallets => pallets;

    public Pallet AddPallet(double width, double length, double height)
    {
        // Creating an element
        var pallet = Pallet.Create(width, length, height);

        // Adding element to the Storage
        pallets.Add(pallet.Id, pallet);

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

        boxes.Add(box.Id, box);
        
        return box;
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
        WriteLine("Report: Pallets ordered by Expiration; with boxes, ordered by weight");
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
                itemPallet.Value.Print();
            }
        }
    }

    public void ReportTopWithMaxExpirationOrderByVolume(int countRecords)
    {
        WriteLine();
        WriteLine($"Report: {countRecords} Pallet with Max expiration, ordered by volume");
        WriteLine();

        var query = Pallets
            .OrderByDescending(p => p.Value.ExpirationDate)
            .Take(countRecords)
            .OrderBy(p => p.Value.Volume);

        foreach (var item in query)
        {
            item.Value.Print();
        }
    }
}
