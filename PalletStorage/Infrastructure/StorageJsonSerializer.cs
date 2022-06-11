using System.Text.Json;
using static System.IO.Path;
using static System.Console;

namespace PalletStorage.Infrastructure;

public class StorageJsonSerializer
{
    private static JsonSerializerOptions MainJsonOptions()
    {
        return new JsonSerializerOptions()
        {
            // includes all fields
            IncludeFields = true, 
            PropertyNameCaseInsensitive = true,
            WriteIndented = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        };
    }

    public static async Task<T> WriteToJsonFileAsync<T>(T input, string fileName)
    {
        var filePath = Combine(Directory.GetCurrentDirectory(), fileName);

        await using Stream fileStream = File.OpenWrite(filePath);
        await JsonSerializer.SerializeAsync<T>(fileStream, input, MainJsonOptions());
        
        return input;
    }

    public static async Task<T> ReadFromJsonFileAsync<T>(string fileName)
    {
        if (!File.Exists(fileName))
        {
            throw new Exception($"File {fileName} not exist!");
        }

        await using Stream fileStream = File.OpenRead(fileName);
        var output = await JsonSerializer.DeserializeAsync<T>(fileStream, MainJsonOptions());

        if (output == null)
        {
            throw new Exception($"Error Deserialize from file {fileName} to {typeof(T)}!");
        }

        return output;
    }

}
