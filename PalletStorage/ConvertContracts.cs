
namespace Ex.PalletStorage;

public class ContractStorage
{
    public string? Id { get; set; }
    public string? Name { get; set; }
    public IDictionary<string, ContractBox>? Boxes { get; set; }
    public IDictionary<string, ContractPallet>? Pallets { get; set; }

}

public class ContractBox
{
    public string? Id { get; set; }
    public DateTime ProductionDate { get; set; }
    public DateTime ExpirationDate { get; set; }
    public int Height { get; set; }
    public int Width { get; set; }
    public int Length { get; set; }
    public int Weight { get; set; }
    public int Volume { get; set; }
}

public class ContractPallet
{
    public IList<ContractBox>? Boxes { get; set; }
    public int Weight { get; set; }
    public int Volume { get; set; }
    public DateTime ExpirationDate { get; set; }
    public string? Id { get; set; }
    public DateTime ProductionDate { get; set; }
    public int Height { get; set; }
    public int Width { get; set; }
    public int Length { get; set; }
}
