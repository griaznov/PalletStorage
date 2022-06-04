
namespace PalletStorage;

internal class TestGenerator
{
    public static Storage GenerateTestStorage()
    {
        Storage storage = new("Main storage");

        StorageBox? Box1 = storage.AddBox(2, 2, 3, 5, new DateTime(1999, 1, 1), new DateTime(1999, 1, 1));
        StorageBox? Box2 = storage.AddBox(2, 2, 3, 1, new DateTime(2000, 1, 1));
        StorageBox? Box3 = storage.AddBox(2, 2, 3, 2, new DateTime(2000, 1, 2), new DateTime(2000, 1, 7));
        StorageBox? Box4 = storage.AddBox(1, 1, 1, 4, new DateTime(2000, 1, 3));
        storage.AddBox(1, 1, 1, 1, new DateTime(2000, 1, 3));
        storage.AddBox(1, 1, 1, 2, new DateTime(2000, 1, 3), new DateTime(2000, 1, 4));

        Pallet? pallet1 = storage.AddPallet(3, 3, 3);
        Pallet? pallet2 = storage.AddPallet(4, 4, 4);
        storage.AddPallet(4, 3, 3);
        storage.AddPallet(3, 3, 5);

        if (pallet1 is not null && Box1 is not null)
        {
            storage.MoveBoxToPallet(Box1, pallet1);
        }

        if (pallet1 is not null && Box2 is not null)
        {
            storage.MoveBoxToPallet(Box2, pallet1);
        }

        if (pallet2 is not null && Box3 is not null)
        {
            storage.MoveBoxToPallet(Box3, pallet2);
        }

        if (pallet2 is not null && Box4 is not null)
        {
            storage.MoveBoxToPallet(Box4, pallet2);
        }

        return storage;
    }
}



