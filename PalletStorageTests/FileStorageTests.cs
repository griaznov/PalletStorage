using static System.IO.Path;
using Newtonsoft.Json;
using PalletStorage;

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
        string tempFilePath = TempFilePath();

        StorageBox box = new(2, 3, 4, 1, DateTime.Today, DateTime.Today);

        FileStorage fileStorage = new(tempFilePath);
        //fileStorage.WriteToFile(box);
        await fileStorage.WriteToFileAsync(box);

        Assert.True(File.Exists(tempFilePath));

        try
        {
            string text = File.ReadAllText(tempFilePath);
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
        string tempFilePath = TempFilePath();

        Pallet? pallet = Pallet.Create(2, 3, 4);
        if (pallet == null)
        {
            Assert.True(false, "False with Create() Pallet!");
            return;
        }

        FileStorage fileStorage = new(tempFilePath);
        //fileStorage.WriteToFile(pallet);
        await fileStorage.WriteToFileAsync(pallet);

        try
        {
            Assert.True(File.Exists(tempFilePath));
            string text = File.ReadAllText(tempFilePath);
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
        string tempFilePath = TempFilePath();

        StorageBox testObject = new(2, 3, 4, 1, DateTime.Today, DateTime.Today);
        
        FileStorage fileStorage = new(tempFilePath);
        //fileStorage.WriteToFile(testObject);
        await fileStorage.WriteToFileAsync(testObject);

        try
        {
            //BoxModel? objectFromFile = fileStorage.ReadFromFile<BoxModel>();
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
            
            Assert.True(ObjectComparison.EqualByJson(testObject, convertedObject), "Box readed from the file is not the same!");
        }
        finally
        {
            DeleteFile(tempFilePath);
        }
    }

    [Fact(DisplayName = "4. Pallet: Serialization and deserialization from the file is successful")]
    public async void SerializeDeserializePalletInFile()
    {
        string tempFilePath = TempFilePath();

        Pallet? testObject = Pallet.Create(2, 3, 4);

        if (testObject == null)
        {
            Assert.True(false, "False with Create() Pallet!");
            return;
        }

        FileStorage fileStorage = new(tempFilePath);
        //fileStorage.WriteToFile(testObject);
        await fileStorage.WriteToFileAsync(testObject);

        try
        {
            //PalletModel? objectFromFile = fileStorage.ReadFromFile<PalletModel>();
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
        string tempFilePath = TempFilePath();

        Storage testObject = StorageCollectionGenerator.GenerateStorage();

        FileStorage fileStorage = new(tempFilePath);
        //fileStorage.WriteToFile(testObject);
        await fileStorage.WriteToFileAsync(testObject);

        try
        {
            //StorageModel? objectFromFile = fileStorage.ReadFromFile<StorageModel>();
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

            Assert.True(ObjectComparison.EqualByJson(testObject, convertedObject), "Storage readed from the file is not the same!");
        }
        finally
        {
            DeleteFile(tempFilePath);
        }
    }
}
