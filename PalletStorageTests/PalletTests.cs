using PalletStorage;
using PalletStorage.Сlasses;

namespace PalletStorageTests;

public class PalletTests
{
    [Fact(DisplayName = "1. Creating a normal pallet and checking the volume count")]
    public void CreationPallet()
    {
        Pallet? pallet = Pallet.Create(2, 3, 4);

        if (pallet == null)
        {
            Assert.True(false, "False with Create() Pallet!");
            return;
        }

        Assert.Equal(24, pallet.Volume);
    }

    [Fact(DisplayName = "2. Add box to pallet")]
    public void AddBoxToPallet()
    {
        Pallet? pallet = Pallet.Create(2, 3, 4);

        if (pallet == null)
        {
            Assert.True(false, "False with Create() Pallet!");
            return;
        }

        StorageBox? box = StorageBox.Create(2, 2, 2, 2, DateTime.Today);

        if ((box == null))
        {
            Assert.True(false, "False with Create() StorageBox!");
            return;
        }

        pallet.AddBox(box);

        Assert.Contains(box, pallet.Boxes);
    }

    [Fact(DisplayName = "3. Checking volume and weight calculation for pallet")]
    public void CheckingVolumeСalculation()
    {
        Pallet? pallet = Pallet.Create(2, 3, 4);

        if (pallet == null)
        {
            Assert.True(false, "False with Create() Pallet!");
            return;
        }

        StorageBox? box1 = StorageBox.Create(2, 2, 2, 2, DateTime.Today);
        StorageBox? box2 = StorageBox.Create(2, 3, 2, 3, DateTime.Today);

        if ((box1 == null) || (box2 == null))
        {
            Assert.True(false, "False with Create() StorageBox!");
            return;
        }

        pallet.AddBox(box1);
        pallet.AddBox(box2);

        double volume = 24 + 8 + 12;
        double weight = 30 + 2 + 3;

        Assert.Equal(volume, pallet.Volume);
        Assert.Equal(weight, pallet.Weight);
    }

    [Fact(DisplayName = "4. Checking expiration date calculation for pallet")]
    public void CheckingExpirationDate()
    {
        Pallet? pallet = Pallet.Create(2, 3, 4);

        if (pallet == null)
        {
            Assert.True(false, "False with Create() Pallet!");
            return;
        }

        // For an empty pallet, the date must be empty
        Assert.Equal(DateTime.MinValue, pallet.ExpirationDate);

        DateTime lessDate = DateTime.Today;
        DateTime biggerDate = lessDate.AddDays(1);
        DateTime biggerDate2 = lessDate.AddDays(2);

        StorageBox? box1 = StorageBox.Create(2, 2, 2, 2, DateTime.Today, lessDate);
        StorageBox? box2 = StorageBox.Create(2, 3, 2, 3, DateTime.Today, biggerDate);
        StorageBox? box3= StorageBox.Create(2, 3, 2, 3, DateTime.Today, biggerDate2);

        if ((box1 == null) || (box2 == null) || (box3 == null))
        {
            Assert.True(false, "False with Create() StorageBox!");
            return;
        }

        pallet.AddBox(box1);
        pallet.AddBox(box2);
        pallet.AddBox(box3);

        // Minimum value from boxes
        Assert.Equal(lessDate, pallet.ExpirationDate);
    }
}
