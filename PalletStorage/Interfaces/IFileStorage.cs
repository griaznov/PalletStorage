namespace PalletStorage.Interfaces;

public interface IFileStorage
{
    public Task<T> WriteToFileAsync<T>(T value);
    public Task<T> ReadFromFileAsync<T>();
}
