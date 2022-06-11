using System.Text;
using static System.Console;

namespace PalletStorage.Сlasses;

public class StorageBox : UniversalBox
{
    private const int MinDaysExpirationDate = 100;

    protected string id;
    protected DateTime productionDate;
    protected DateTime expirationDate;

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
            expirationDate = productionDate.AddDays(MinDaysExpirationDate);
        }

        this.productionDate = productionDate;
        this.expirationDate = expirationDate;
        this.id = id;

        if (string.IsNullOrEmpty(this.id))
        {
            this.id = Guid.NewGuid().ToString();
        }
    }

    public virtual string Id => id;
    public virtual DateTime ProductionDate => productionDate;
    public virtual DateTime ExpirationDate => expirationDate;

    public static StorageBox Create(double width,
        double length,
        double height,
        double weight,
        DateTime prodDate = default,
        DateTime expDate = default)
    {
        return new StorageBox(width, length, height, weight, prodDate, expDate);
    }

    public static bool ValidateDateParams(DateTime prodDate = default, DateTime expDate = default)
    {
        return (expDate != default) || (prodDate != default);
    }
}
