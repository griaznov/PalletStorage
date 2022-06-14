using PalletStorage.Сlasses;
using PalletStorage.Сlasses.ContractModels;
using PalletStorage.Сlasses.ContractModels.Extensions;
using PalletStorage.Сlasses.Extensions;

namespace PalletStorage.Infrastructure;
using static Console;

public static class StorageCollection
{
    public static async Task<Storage> CollectionForWorkAsync()
    {
        FileStorage fileStorage = new();
        Storage storage;

        try
        {
            // Read from file in new contract-model for conversion to Storage
            var storageFromFile = await fileStorage.ReadFromFileAsync<StorageModel>();

            // Conversion from model to Storage
            storage = storageFromFile.FromModel();
        }
        catch
        {
            Console.WriteLine("False with reading Storage from file!");

            // Generate test values in Storage
            return StorageCollectionGenerator.GenerateStorage();
        }

        Console.WriteLine($"Successful reading and conversion from json files to Storage");

        return storage;
    }

    public static async Task SaveCollectionInModelAsync(Storage storage)
    {
        // Save Storage in file
        FileStorage fileStorage = new();
        await fileStorage.WriteToFileAsync(storage.ToModel());
    }

    public static void PrintPalletsOrderByExpirationAndWeight(this Storage storage)
    {
        WriteLine();
        WriteLine("Report: Pallets ordered by Expiration; with boxes, ordered by weight");
        WriteLine();

        var query = storage.Pallets
            .GroupBy(p => p.Value.ExpirationDate)
            .Select(i => new { i.Key, sortValues = i.OrderBy(it => it.Value.Weight) })
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

    public static void PrintTopWithMaxExpirationOrderByVolume(this Storage storage, int countRecords)
    {
        WriteLine();
        WriteLine($"Report: {countRecords} Pallet with Max expiration, ordered by volume");
        WriteLine();

        var query = storage.Pallets
            .OrderByDescending(p => p.Value.ExpirationDate)
            .Take(countRecords)
            .OrderBy(p => p.Value.Volume);

        foreach (var item in query)
        {
            item.Value.Print();
        }
    }
}
