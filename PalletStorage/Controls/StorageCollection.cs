
namespace PalletStorage;

public class StorageCollection
{
    public static Storage CollectionForWork()
    {
        FileStorage fileStorage = new();

        // Read from file in new contract-model for conversion to Storage
        StorageModel? storageFromFile = fileStorage.ReadFromFile<StorageModel>();
 
        if (storageFromFile == null)
        {
            Console.WriteLine("False with reading Storage from file!");

            // Generate test values in Storage
            return StorageCollectionGenerator.GenerateStorage();
        }

        // Conversion from model to Storage
        Storage? storage = (Storage?)storageFromFile;

        if (storage == null)
        {
            Console.WriteLine("False with convert Storage from file!");

            // Generate test values in Storage
            return StorageCollectionGenerator.GenerateStorage();
        }

        Console.WriteLine($"Successful reading and conversion from json files to Storage");

        return storage;
    }

    public static async Task<Storage> CollectionForWorkAsync()
    {
        FileStorage fileStorage = new();

        // Read from file in new contract-model for conversion to Storage
        StorageModel? storageFromFile = await fileStorage.ReadFromFileAsync<StorageModel>();

        if (storageFromFile == null)
        {
            Console.WriteLine("False with reading Storage from file!");

            // Generate test values in Storage
            return StorageCollectionGenerator.GenerateStorage();
        }

        // Conversion from model to Storage
        Storage? storage = (Storage?)storageFromFile;

        if (storage == null)
        {
            Console.WriteLine("False with convert Storage from file!");

            // Generate test values in Storage
            return StorageCollectionGenerator.GenerateStorage();
        }

        Console.WriteLine($"Successful reading and conversion from json files to Storage");

        return storage;
    }

    public static void SaveCollection(Storage storage)
    {
        // Save Storage in file
        FileStorage fileStorage = new();
        fileStorage.WriteToFile(storage);
    }

    public static async void SaveCollectionAsync(Storage storage)
    {
        // Save Storage in file
        FileStorage fileStorage = new();
        await fileStorage.WriteToFileAsync(storage);
    }

}
