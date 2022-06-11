using PalletStorage.Сlasses;
using PalletStorage.Сlasses.ContractModels;
using PalletStorage.Сlasses.ContractModels.Extensions;

namespace PalletStorage.Infrastructure;

public class StorageCollection
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

    public static async void SaveCollectionAsync(Storage storage)
    {
        // Save Storage in file
        FileStorage fileStorage = new();
        await fileStorage.WriteToFileAsync(storage);
    }
}
