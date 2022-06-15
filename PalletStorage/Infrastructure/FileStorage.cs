using PalletStorage.Interfaces;
using static System.IO.Path;

namespace PalletStorage.Infrastructure;

public class FileStorage : IFileStorage
{
    private readonly string filePath;

    public FileStorage(string filePath = "")
    {
        if (string.IsNullOrEmpty(filePath))
        {
            filePath = Combine(Directory.GetCurrentDirectory(), "Storage.json");
        }

        this.filePath = filePath;
    }

    public async Task<T> WriteToFileAsync<T>(T inputObject)
    {
        return await StorageJsonSerializer.WriteToJsonFileAsync(inputObject, filePath);
    }

    public async Task<T> ReadFromFileAsync<T>()
    {
        return await StorageJsonSerializer.ReadFromJsonFileAsync<T>(filePath);
    }
}
