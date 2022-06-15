using static System.Console;

namespace PalletStorage.Сlasses.Extensions;

public static class StorageExtensions
{
    public static void Print(this Storage storage)
    {
        WriteLine($"Storage: {storage.Name}, with boxes: {storage.Boxes.Count}, pallets: {storage.Pallets.Count}");
        WriteLine("");

        WriteLine("Boxes:");
        foreach (var keyValue in storage.Boxes)
        {
            Write("\t");
            keyValue.Value.Print();
        }

        WriteLine("Pallets:");
        foreach (var keyValue in storage.Pallets)
        {
            var pallet = keyValue.Value;

            Write("\t");
            pallet.Print();
        }
    }
}

