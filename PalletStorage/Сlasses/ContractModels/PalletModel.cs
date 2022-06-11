namespace PalletStorage.Сlasses.ContractModels;

public class PalletModel
{
    public IList<BoxModel> Boxes { get; set; } = new List<BoxModel>();
    public double Weight { get; set; }
    public double Volume { get; set; }
    public DateTime ExpirationDate { get; set; }
    public string Id { get; set; } = string.Empty;
    public double Height { get; set; }
    public double Width { get; set; }
    public double Length { get; set; }
}
