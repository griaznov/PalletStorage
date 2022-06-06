using Newtonsoft.Json;

namespace PalletStorage;

public class BoxModel
{
    public string? ID { get; set; }
    public DateTime ProductionDate { get; set; }
    public DateTime ExpirationDate { get; set; }
    public double Height { get; set; }
    public double Width { get; set; }
    public double Length { get; set; }
    public double Weight { get; set; }
    public double Volume { get; set; }

    public static explicit operator StorageBox?(BoxModel inputObject)
    {
        return JsonConvert.DeserializeObject<StorageBox>(JsonConvert.SerializeObject(inputObject));
    }
}