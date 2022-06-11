using Newtonsoft.Json;

namespace PalletStorage.Сlasses.ContractModels;

public class StorageModel
{
    public string? Id { get; set; }
    public string? Name { get; set; }
    public IDictionary<string, BoxModel>? Boxes { get; set; }
    public IDictionary<string, PalletModel>? Pallets { get; set; }

    public static explicit operator Storage?(StorageModel inputObject)
    {
        return JsonConvert.DeserializeObject<Storage>(JsonConvert.SerializeObject(inputObject));
    }
}
