namespace PalletStorage;

interface IUniversalBox
{
    //public IUniversalBox Create();
}

public interface IFileStorage
{
    public void WriteToJsonFile<T>(T value, string fileName);
    public T? ReadFromJsonFile<T>(string fileName);
}
