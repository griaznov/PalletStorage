using PalletStorage.Сlasses;

namespace PalletStorage.Infrastructure;

public class StorageCollectionGenerator
{
    // Generate test collection
    public static Storage GenerateStorage()
    {
        Storage storage = new("Main storage");

        StorageBox box1 = storage.AddBox(2, 2, 3, 5, new DateTime(1999, 1, 1), new DateTime(1999, 1, 1));
        StorageBox box2 = storage.AddBox(2, 2, 3, 1, new DateTime(2000, 1, 1));
        StorageBox box3 = storage.AddBox(2, 2, 3, 2, new DateTime(2000, 1, 2), new DateTime(2000, 1, 7));
        StorageBox box4 = storage.AddBox(1, 1, 1, 4, new DateTime(2000, 1, 3));
        
        storage.AddBox(1, 1, 1, 1, new DateTime(2000, 1, 3));
        storage.AddBox(1, 1, 1, 2, new DateTime(2000, 1, 3), new DateTime(2000, 1, 4));

        Pallet pallet1 = storage.AddPallet(3, 3, 3);
        Pallet pallet2 = storage.AddPallet(4, 4, 4);
       
        storage.AddPallet(4, 3, 3);
        storage.AddPallet(3, 3, 5);

        storage.MoveBoxToPallet(box1, pallet1);
        storage.MoveBoxToPallet(box2, pallet1);
        storage.MoveBoxToPallet(box3, pallet2);
        storage.MoveBoxToPallet(box4, pallet2);

        Console.WriteLine($"A new Storage test collection was generated.");

        return storage;
    }
}
