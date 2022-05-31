using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;
using static System.IO.Path;
using static System.Console;
using static System.Environment;
using System.IO;

namespace Ex.PalletStorage;

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
        WriteToFile(storage, filePath);
    }

    public void WriteToFile<T>(T input, string fileName)
    {
        string filePath = Combine(Directory.GetCurrentDirectory(), fileName);

        using Stream fileStream = File.OpenWrite(filePath);
        JsonSerializer.Serialize(fileStream, input, MainJsonOptions());
    }

    //public T? ReadFromFile<T>(string fileName)
    //{
    //    T? output;

    //    using (Stream fileStream = File.OpenRead(filePath))
    //    {
    //        output = JsonSerializer.Deserialize<T>(fileStream, MainJsonOptions());
    //    }

    //    if (output == null)
    //    {
    //        WriteLine($"Error reading Storage from file {filePath}!");
    //    }

    //    return output;
    //}

    public Storage? ReadStorageFromFile()
    {
        Storage? storage;

        using (Stream fileStream = File.OpenRead(filePath))
        {
            storage = JsonSerializer.Deserialize<Storage>(fileStream, MainJsonOptions());
        }

        if (storage == null)
        {
            WriteLine($"Error reading Storage from file {filePath}!");
        }

        return storage;
    }

    public ContractStorage? ReadContractStorageFromFile()
    {
        ContractStorage? storage;

        using (Stream fileStream = File.OpenRead(filePath))
        {
            storage = JsonSerializer.Deserialize<ContractStorage>(fileStream, MainJsonOptions());
        }

        if (storage == null)
        {
            WriteLine($"Error reading Storage from file {filePath}!");
        }

        return storage;
    }

    public Pallet? ReadPallletFromFile(string fileName)
    {
        Pallet? storage;

        using (Stream fileStream = File.OpenRead(fileName))
        {
            storage = JsonSerializer.Deserialize<Pallet>(fileStream, MainJsonOptions());
        }

        if (storage == null)
        {
            WriteLine($"Error reading Storage from file {filePath}!");
        }

        return storage;
    }
}
