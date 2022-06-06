using System.Text.Json;
using static System.IO.Path;
using static System.Console;

namespace PalletStorage;

public class StorageJsonSerializer
{
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

    public static void WriteToJsonFile<T>(T input, string fileName)
    {
        string filePath = Combine(Directory.GetCurrentDirectory(), fileName);

        using Stream fileStream = File.OpenWrite(filePath);
        JsonSerializer.Serialize<T>(fileStream, input, MainJsonOptions());
    }

    public static async Task<T> WriteToJsonFileAsync<T>(T input, string fileName)
    {
        string filePath = Combine(Directory.GetCurrentDirectory(), fileName);

        using Stream fileStream = File.OpenWrite(filePath);
        await JsonSerializer.SerializeAsync<T>(fileStream, input, MainJsonOptions());
        
        return input;
    }

    public static T? ReadFromJsonFile<T>(string fileName)
    {
        T? output;

        if (!File.Exists(fileName))
        {
            WriteLine($"File {fileName} not exist!");
            return default;
        }
        
        using (Stream fileStream = File.OpenRead(fileName))
        {
            output = JsonSerializer.Deserialize<T>(fileStream, MainJsonOptions());
        }

        if (output == null)
        {
            WriteLine($"Error Deserialize from file {fileName} to {typeof(T)}!");
        }

        return output;
    }

    public static async Task<T?> ReadFromJsonFileAsync<T>(string fileName)
    {
        T? output;

        if (!File.Exists(fileName))
        {
            WriteLine($"File {fileName} not exist!");
            return default;
        }

        using (Stream fileStream = File.OpenRead(fileName))
        {
           output = await JsonSerializer.DeserializeAsync<T>(fileStream, MainJsonOptions());
        }

        if (output == null)
        {
            WriteLine($"Error Deserialize from file {fileName} to {typeof(T)}!");
        }

        return output;
    }

}
