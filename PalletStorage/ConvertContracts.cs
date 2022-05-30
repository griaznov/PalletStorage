
namespace Ex.PalletStorage;

public class ContractStorage
{
    public string? id { get; set; }
    public string? name { get; set; }
    public IDictionary<string, ContractBox>? boxes { get; set; }
    public IDictionary<string, ContractPallet>? pallets { get; set; }

}

public class ContractBox
{
    public string? id { get; set; }
    public DateTime productionDate { get; set; }
    public DateTime expirationDate { get; set; }
    public int height { get; set; }
    public int width { get; set; }
    public int length { get; set; }
    public int weight { get; set; }
    public int volume { get; set; }
}

public class ContractPallet
{
    public IList<ContractBox>? boxes { get; set; }
    public int weight { get; set; }
    public int volume { get; set; }
    public DateTime expirationDate { get; set; }
    public string? id { get; set; }
    public DateTime productionDate { get; set; }
    public int height { get; set; }
    public int width { get; set; }
    public int length { get; set; }
}
