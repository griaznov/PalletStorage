using System.Text;
using static System.Console;

namespace PalletStorage;

public class StorageBox : UniversalBox
{
    protected string id;
    protected DateTime productionDate; // дата производства
    protected DateTime expirationDate; // дата годности
    private const int minDaysExpirationDate = 100;

    public StorageBox(
        double width,
        double length,
        double height,
        double weight,
        DateTime productionDate = default,
        DateTime expirationDate = default, 
        string id = "",
        double volume = 0) 
        : base(width, length, height, weight)
    {
        // Verifying parameters
        // The box must have an expiration date or production date.
        if (!ValidateDateParams(productionDate, expirationDate))
        {
            StringBuilder stringBuilder = new();

            stringBuilder.AppendFormat($"It is required to specify the expiration date or production date! ");
            stringBuilder.AppendFormat($"Expiration date: {expirationDate}, Production date: {productionDate}");
            WriteLine(stringBuilder.ToString());

            throw new ArgumentOutOfRangeException(stringBuilder.ToString());
        }

        // If only the production date is specified,
        // the expiration date is calculated from the production date plus 100 days
        if (expirationDate == default)
        {
            expirationDate = productionDate.AddDays(minDaysExpirationDate);
        }

        this.productionDate = productionDate;
        this.expirationDate = expirationDate;
        this.id = id;

        if (string.IsNullOrEmpty(this.id))
        {
            this.id = Guid.NewGuid().ToString();
        }
    }

    public virtual string ID { get { return id; } }
    public virtual DateTime ProductionDate { get { return productionDate; } }
    public virtual DateTime ExpirationDate { get { return expirationDate; } }

    public override void Print()
    {
        StringBuilder stringBuilder = new();

        stringBuilder.AppendFormat($"Box: id: {id}, (w/l/h): {width}/{length}/{height}, weight: {weight}, volume: {volume}, ");
        stringBuilder.AppendFormat($"prod.date: {productionDate}, exp.date: {expirationDate},");

        WriteLine(stringBuilder.ToString());
    }

    public static StorageBox? Create(double width,
        double length,
        double height,
        double weight,
        DateTime prodDate = default,
        DateTime expDate = default)
    {
        try
        {
            return new StorageBox(width, length, height, weight, prodDate, expDate);
        }
        catch
        {
            return null;
        }
    }

    public static bool ValidateDateParams(DateTime prodDate = default, DateTime expDate = default)
    {
        if ((expDate == default) && (prodDate == default))
        {
            return false;
        }
        
        return true;
    }
}
