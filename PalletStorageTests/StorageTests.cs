using PalletStorage;

namespace PalletStorageTests;

public class StorageTests
{
    [Fact(DisplayName = "1. Creating a simple storage")]
    public void CreationStorage()
    {
        try
        {
            Storage storage = new("Test storage");

            bool result = (null != storage) && (!string.IsNullOrEmpty(storage.Name));

            Assert.True(result, "Error with creating simple storage!");
        }
        catch (Exception ex)
        {
            Assert.Equal(typeof(ArgumentException), ex.GetType());
        }
    }

    [Fact(DisplayName = "2. Add new box to storage")]
    public void AddBoxToStorage()
    {
        Storage storage = new("Test storage");

        StorageBox? box = storage.AddBox(2, 2, 3, 1, new DateTime(2000, 1, 1));

        if (box == null)
        {
            Assert.True(false, "Error with add box to storage!");
            return;
        }

        Assert.True(storage.Boxes.ContainsKey(box.ID), "The storage does not contain a new box!");
    }

    [Fact(DisplayName = "2. Add new pallet to storage")]
    public void AddPalletToStorage()
    {
        Storage storage = new("Test storage");

        Pallet? pallet = storage.AddPallet(4, 4, 4);

        if (pallet == null)
        {
            Assert.True(false, "Error with add pallet to storage!");
            return;
        }

        Assert.True(storage.Pallets.ContainsKey(pallet.ID), "The storage does not contain a new pallet!");
    }

    [Fact(DisplayName = "2. Move a new box to pallet storage")]
    public void MoveBoxToPallet()
    {
        Storage storage = new("Test storage");

        Pallet? pallet = storage.AddPallet(4, 4, 4);
        StorageBox? box = storage.AddBox(2, 2, 3, 1, new DateTime(2000, 1, 1));

        if ((pallet == null) || (box == null))
        {
            Assert.True(false, "Error with add pallet or box to storage!");
            return;
        }

        storage.MoveBoxToPallet(box, pallet);

        Assert.True(pallet.Boxes.Contains(box), "The storage does not contain a new pallet!");
    }
}
