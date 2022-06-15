using Xunit;
using static System.IO.Path;
using PalletStorage.Infrastructure;
using PalletStorage.Сlasses;
using PalletStorage.Сlasses.ContractModels;
using PalletStorage.Сlasses.ContractModels.Extensions;

namespace PalletStorageTests;

public class FileStorageTests
{
    private static string TempFilePath()
    {
        return Combine(GetTempPath(), GetRandomFileName());
    }

    private static void DeleteFile(string filePath)
    {
        FileInfo file = new(filePath);
        file.Delete();
    }

    [Fact(DisplayName = "1. Writing to a file Box objects with true result")]
    public async void CanWriteBoxInFile()
    {
        // Arrange
        var tempFilePath = TempFilePath();
        var fileStorage = new FileStorage(tempFilePath);
        var box = new StorageBox(2, 3, 4, 1, DateTime.Today, DateTime.Today);

        try
        {
            // Act
            await fileStorage.WriteToFileAsync(box);
            var text = await File.ReadAllTextAsync(tempFilePath);

            // Assert
            Assert.True(File.Exists(tempFilePath));
            Assert.NotEmpty(text);
        }
        finally
        {
            DeleteFile(tempFilePath);
        }
    }

    [Fact(DisplayName = "2. Writing to a file Pallet objects with true result")]
    public async void CanWritePalletInFile()
    {
        // Arrange
        var tempFilePath = TempFilePath();
        var pallet = Pallet.Create(2, 3, 4);
        var fileStorage = new FileStorage(tempFilePath);
        
        try
        {
            // Act
            await fileStorage.WriteToFileAsync(pallet);
            var text = await File.ReadAllTextAsync(tempFilePath);

            // Assert
            Assert.True(File.Exists(tempFilePath));
            Assert.NotEmpty(text);
        }
        finally
        {
            DeleteFile(tempFilePath);
        }
    }

    [Fact(DisplayName = "3. Box: Serialization and deserialization from the file is successful")]
    public async Task SerializeDeserializeBoxInFileAsync()
    {
        // Arrange
        var tempFilePath = TempFilePath();
        var fileStorage = new FileStorage(tempFilePath);
        var testObject = new StorageBox(2, 3, 4, 1, DateTime.Today, DateTime.Today);

        await fileStorage.WriteToFileAsync(testObject.ToModel());

        try
        {
            // Act
            var objectFromFile = await fileStorage.ReadFromFileAsync<BoxModel>();
            var convertedObject = objectFromFile.FromModel();

            // Assert
            Assert.True(ObjectComparison.EqualByJson(testObject, convertedObject), "Box from the file is not the same!");
        }
        finally
        {
            DeleteFile(tempFilePath);
        }
    }

    [Fact(DisplayName = "4. Pallet: Serialization and deserialization from the file is successful")]
    public async void SerializeDeserializePalletInFile()
    {
        // Arrange
        var tempFilePath = TempFilePath();
        FileStorage fileStorage = new(tempFilePath);
        var testObject = Pallet.Create(2, 3, 4);

        await fileStorage.WriteToFileAsync(testObject.ToModel());

        try
        {
            // Act
            var objectFromFile = await fileStorage.ReadFromFileAsync<PalletModel>();
            var convertedObject = objectFromFile.FromModel();

            // Assert
            Assert.True(ObjectComparison.EqualByJson(testObject, convertedObject), "Pallet from the file is not the same!");
        }
        finally
        {
            DeleteFile(tempFilePath);
        }
    }

    [Fact(DisplayName = "5. Storage: Serialization and deserialization from the file is successful")]
    public async void SerializeDeserializeStorageInFile()
    {
        // Arrange
        var tempFilePath = TempFilePath();
        FileStorage fileStorage = new(tempFilePath);
        var testObject = StorageCollectionGenerator.GenerateStorage();

        await fileStorage.WriteToFileAsync(testObject.ToModel());

        try
        {
            // Act
            var objectFromFile = await fileStorage.ReadFromFileAsync<StorageModel>();
            var convertedObject = objectFromFile.FromModel();

            // Assert
            Assert.True(ObjectComparison.EqualByJson(testObject, convertedObject), "Storage from the file is not the same!");
        }
        finally
        {
            DeleteFile(tempFilePath);
        }
    }
}
