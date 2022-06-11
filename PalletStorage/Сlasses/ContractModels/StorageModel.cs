namespace PalletStorage.Сlasses.ContractModels;

public class StorageModel
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public IDictionary<string, BoxModel> Boxes { get; set; } = new Dictionary<string, BoxModel>();
    public IDictionary<string, PalletModel> Pallets { get; set; } = new Dictionary<string, PalletModel>();
}
