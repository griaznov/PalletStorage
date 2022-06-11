namespace PalletStorage.Сlasses.ContractModels;

public class StorageModel
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public IDictionary<Guid, BoxModel> Boxes { get; set; } = new Dictionary<Guid, BoxModel>();
    public IDictionary<Guid, PalletModel> Pallets { get; set; } = new Dictionary<Guid, PalletModel>();
}
