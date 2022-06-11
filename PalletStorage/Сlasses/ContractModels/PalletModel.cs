using Newtonsoft.Json;

namespace PalletStorage.Сlasses.ContractModels;

public class PalletModel
{
    public IList<BoxModel> Boxes { get; set; } = new List<BoxModel>();
    public double Weight { get; set; }
    public double Volume { get; set; }
    public DateTime ExpirationDate { get; set; }
    public string? Id { get; set; }
    public double Height { get; set; }
    public double Width { get; set; }
    public double Length { get; set; }

    public static explicit operator Pallet?(PalletModel inputObject)
    {
        return JsonConvert.DeserializeObject<Pallet>(JsonConvert.SerializeObject(inputObject));
    }
}
