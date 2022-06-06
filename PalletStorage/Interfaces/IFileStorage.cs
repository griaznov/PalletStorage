namespace PalletStorage;

public interface IFileStorage
{
    public void WriteToFile<T>(T value);
    public T? ReadFromFile<T>();
}
