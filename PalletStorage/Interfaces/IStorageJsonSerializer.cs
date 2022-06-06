namespace PalletStorage;

public interface IStorageJsonSerializer
{
    public void WriteToJsonFile<T>(T value, string fileName);
    public T? ReadFromJsonFile<T>(string fileName);
}
