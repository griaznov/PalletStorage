using static System.IO.Path;
using PalletStorage.Infrastructure;
using PalletStorage.Сlasses;
using PalletStorage.Сlasses.ContractModels;

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

    [Fact(DisplayName = "1. Writing to a file Box and Pallet objects with true result")]
    public async void CanWriteBoxInFile()
    {
        var tempFilePath = TempFilePath();

        StorageBox box = new(2, 3, 4, 1, DateTime.Today, DateTime.Today);

        FileStorage fileStorage = new(tempFilePath);
        
        await fileStorage.WriteToFileAsync(box);

        Assert.True(File.Exists(tempFilePath));

        try
        {
            var text = await File.ReadAllTextAsync(tempFilePath);
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
        var tempFilePath = TempFilePath();

        Pallet? pallet = Pallet.Create(2, 3, 4);
        if (pallet == null)
        {
            Assert.True(false, "False with Create() Pallet!");
            return;
        }

        FileStorage fileStorage = new(tempFilePath);
        
        await fileStorage.WriteToFileAsync(pallet);

        try
        {
            Assert.True(File.Exists(tempFilePath));
            var text = await File.ReadAllTextAsync(tempFilePath);
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
        var tempFilePath = TempFilePath();

        StorageBox testObject = new(2, 3, 4, 1, DateTime.Today, DateTime.Today);
        
        FileStorage fileStorage = new(tempFilePath);
        
        await fileStorage.WriteToFileAsync(testObject);

        try
        {
            BoxModel? objectFromFile = await fileStorage.ReadFromFileAsync<BoxModel>();

            if (objectFromFile == null)
            {
                Assert.True(false, "False with reading box from file!");
                return;
            }

            StorageBox? convertedObject = (StorageBox?)objectFromFile;

            if (convertedObject == null)
            {
                Assert.NotNull(convertedObject);
                return;
            }
            
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
        var tempFilePath = TempFilePath();

        Pallet? testObject = Pallet.Create(2, 3, 4);

        if (testObject == null)
        {
            Assert.True(false, "False with Create() Pallet!");
            return;
        }

        FileStorage fileStorage = new(tempFilePath);
        
        await fileStorage.WriteToFileAsync(testObject);

        try
        {
            PalletModel? objectFromFile = await fileStorage.ReadFromFileAsync<PalletModel>();

            if (objectFromFile == null)
            {
                Assert.True(false, "False with reading Pallet from file!");
                return;
            }

            Pallet? convertedObject = (Pallet?)objectFromFile;

            if (convertedObject == null)
            {
                Assert.NotNull(convertedObject);
                return;
            }

            Assert.True(ObjectComparison.EqualByJson(testObject, convertedObject), "Pallet readed from the file is not the same!");
        }
        finally
        {
            DeleteFile(tempFilePath);
        }
    }

    [Fact(DisplayName = "5. Storage: Serialization and deserialization from the file is successful")]
    public async void SerializeDeserializeStorageInFile()
    {
        var tempFilePath = TempFilePath();

        Storage testObject = StorageCollectionGenerator.GenerateStorage();

        FileStorage fileStorage = new(tempFilePath);
        
        await fileStorage.WriteToFileAsync(testObject);

        try
        {
            StorageModel? objectFromFile = await fileStorage.ReadFromFileAsync<StorageModel>();

            if (objectFromFile == null)
            {
                Assert.True(false, "False with reading Storage from file!");
                return;
            }

            Storage? convertedObject = (Storage?)objectFromFile;

            if (convertedObject == null)
            {
                Assert.NotNull(convertedObject);
                return;
            }

            Assert.True(ObjectComparison.EqualByJson(testObject, convertedObject), "Storage from the file is not the same!");
        }
        finally
        {
            DeleteFile(tempFilePath);
        }
    }
}
