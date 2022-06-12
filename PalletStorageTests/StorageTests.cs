using Xunit;
using PalletStorage.Сlasses;

namespace PalletStorageTests;

public class StorageTests
{
    [Fact(DisplayName = "1. Creating a simple storage")]
    public void CreationStorage()
    {
        // Arrange 
        Storage storage = new("Test storage");
        var result = !string.IsNullOrEmpty(storage.Name);

        // Assert
        Assert.True(result, "Error with creating simple storage!");
    }

    [Fact(DisplayName = "2. Add new box to storage")]
    public void AddBoxToStorage()
    {
        // Arrange 
        Storage storage = new("Test storage");

        // Act
        StorageBox box = storage.AddBox(2, 2, 3, 1, new DateTime(2000, 1, 1));

        // Assert
        Assert.True(storage.Boxes.ContainsKey(box.Id), "The storage does not contain a new box!");
    }

    [Fact(DisplayName = "3. Add new pallet to storage")]
    public void AddPalletToStorage()
    {
        // Arrange
        Storage storage = new("Test storage");

        // Act
        Pallet pallet = storage.AddPallet(4, 4, 4);

        // Assert
        Assert.True(storage.Pallets.ContainsKey(pallet.Id), "The storage does not contain a new pallet!");
    }

    [Fact(DisplayName = "4. Move a new box to pallet storage")]
    public void MoveBoxToPallet()
    {
        // Arrange
        Storage storage = new("Test storage");
        Pallet pallet = storage.AddPallet(4, 4, 4);
        StorageBox box = storage.AddBox(2, 2, 3, 1, new DateTime(2000, 1, 1));

        // Act
        storage.MoveBoxToPallet(box, pallet);

        // Assert
        Assert.True(pallet.Boxes.Contains(box), "The pallet in storage does not contain a new box!");
    }

    [Fact(DisplayName = "5. Move 2 equal boxes to pallet storage")]
    public void MoveTwoBoxesToPallet()
    {
        // Arrange
        Storage storage = new("Test storage");
        Pallet pallet = storage.AddPallet(4, 4, 4);
        StorageBox box = storage.AddBox(2, 2, 3, 1, new DateTime(2000, 1, 1));

        // Act
        storage.MoveBoxToPallet(box, pallet);
        storage.MoveBoxToPallet(box, pallet);

        // Assert
        Assert.True(pallet.Boxes.Count() == 1, "The pallet in storage does not contain one box!");
    }
}
