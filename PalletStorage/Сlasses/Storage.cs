using static System.Console;

namespace PalletStorage;

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

    public virtual string ID { get { return id; } }
    public virtual string Name { get { return name; } }
    public virtual IDictionary<string, StorageBox> Boxes { get { return boxes; } }
    public virtual IDictionary<string, Pallet> Pallets { get { return pallets; } }

    public Pallet? AddPallet(double width, double length, double height)
    {
        // Creating an element
        Pallet? pallet = Pallet.Create(width, length, height);

        // Adding element to the Storage
        if (pallet == null)
        {
            return null;
        }

        pallets.Add(pallet.ID, pallet);

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

        boxes.Add(box.ID, box);
        
        return box;
    }

    public void Print()
    {
        WriteLine($"Storage: {name}, with boxes: {boxes.Count}, pallets: {pallets.Count}");
        WriteLine("");

        WriteLine("Boxes:");
        foreach (var keyValue in boxes)
        {
            Write(" ");
            keyValue.Value.Print();
        }

        WriteLine("Pallets:");
        foreach (var keyValue in pallets)
        {
            var palett = keyValue.Value;

            Write(" ");
            palett.Print();
        }
    }

    public StorageBox? FindBox(string boxID)
    {
        if (!boxes.TryGetValue(boxID, out StorageBox? foundВox))
        {
            WriteLine($"The Storage could not find a box with ID: {boxID}");
        }
        
        return foundВox;
    }

    public Pallet? FindPallet(string palletID)
    {
        if (!pallets.TryGetValue(palletID, out Pallet? foundPallet))
        {
            WriteLine($"The Storage could not find a pallet with ID: {palletID}");
        }

        return foundPallet;
    }

    public void MoveBoxToPallet(StorageBox box, Pallet pallet)
    {
        // Moving the box on the pallet
        if (!boxes.ContainsKey(box.ID))
        {
            boxes.Add(box.ID, box); 
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

        var qery = Pallets
            .GroupBy(p => p.Value.ExpirationDate)
            .Select(i => new { i.Key, sortValues = i.OrderBy(it => it.Value.Weight)})
            .OrderBy(p => p.Key);

        foreach (var item in qery)
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

        var qery = Pallets
            .OrderByDescending(p => p.Value.ExpirationDate)
            .Take(countRecords)
            .OrderBy(p => p.Value.Volume);

        foreach (var item in qery)
        {
            item.Value.Print();
        }
    }
}
