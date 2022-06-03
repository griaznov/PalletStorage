using System.Text.Json;
using System.Text.Json.Serialization;
using static System.IO.Path;
using static System.Console;

namespace PalletStorage;

internal class FileStorage : IFileStorage
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

    private static JsonSerializerOptions MainJsonOptions()
    {
        return new JsonSerializerOptions()
        {
            IncludeFields = true, // includes all fields
            PropertyNameCaseInsensitive = true,
            WriteIndented = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        };
    }

    public void WriteStorageToFile(Storage storage)
    {
        WriteToJsonFile(storage, filePath);
    }

    public Storage? ReadStorageFromFile()
    {
        return ReadFromJsonFile<Storage>(filePath);
    }

    public ContractStorage? ReadContractStorageFromFile()
    {
        return ReadFromJsonFile<ContractStorage>(filePath);
    }

    public void WriteToJsonFile<T>(T input, string fileName)
    {
        string filePath = Combine(Directory.GetCurrentDirectory(), fileName);

        using Stream fileStream = File.OpenWrite(filePath);
        JsonSerializer.Serialize(fileStream, input, MainJsonOptions());
    }

    public T? ReadFromJsonFile<T>(string fileName)
    {
        T? output;

        using (Stream fileStream = File.OpenRead(fileName))
        {
            output = JsonSerializer.Deserialize<T>(fileStream, MainJsonOptions());
        }

        if (output == null)
        {
            WriteLine($"Error Deserialize from file {filePath} to {typeof(T)}!");
        }

        return output;
    }

}
