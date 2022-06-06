using static System.IO.Path;

namespace PalletStorage;

public class FileStorage : IFileStorage
{
    private readonly string filePath = string.Empty;

    public FileStorage(string filePath = "")
    {
        if (string.IsNullOrEmpty(filePath))
        {
            filePath = Combine(Directory.GetCurrentDirectory(), "Storage.json");
        }

        this.filePath = filePath;
    }

    public void WriteToFile<T>(T inputObject)
    {
        StorageJsonSerializer.WriteToJsonFile(inputObject, filePath);
    }

    public async Task<T> WriteToFileAsync<T>(T inputObject)
    {
        return await StorageJsonSerializer.WriteToJsonFileAsync(inputObject, filePath);
    }

    public T? ReadFromFile<T>()
    {
        return StorageJsonSerializer.ReadFromJsonFile<T>(filePath);
    }

    public async Task<T?> ReadFromFileAsync<T>()
    {
        return await StorageJsonSerializer.ReadFromJsonFileAsync<T>(filePath);
    }

}
