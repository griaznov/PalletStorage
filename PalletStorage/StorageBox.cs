using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;
using static System.Console;

namespace Ex.PalletStorage;

internal class StorageBox : UniversalBox
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
        this.productionDate = productionDate;
        this.expirationDate = expirationDate;
        this.id = id;

        //if (!(volume == 0))
        //{
        //    this.volume = volume;
        //}

        if (string.IsNullOrEmpty(this.id))
        {
            this.id = Guid.NewGuid().ToString();
        }
    }

    public virtual string ID
    {
        get { return id; }
    }

    public virtual DateTime ProductionDate
    {
        get { return productionDate; }
    }

    public virtual DateTime ExpirationDate
    {
        get { return expirationDate; }
    }

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
        // Verifying parameters
        // The box must have an expiration date or production date.
        if (!IsValidBoxParams(width, length, height) 
            || !IsValidWeight(weight) 
            || !ValidateDateParams(prodDate, expDate))
        {
            return null;
        }

        // If only the production date is specified,
        // the expiration date is calculated from the production date plus 100 days
        if (expDate == default)
        {
            expDate = prodDate.AddDays(minDaysExpirationDate);
        }

        return new StorageBox(width, length, height, weight, prodDate, expDate);
    }

    public static bool ValidateDateParams(DateTime prodDate = default, DateTime expDate = default)
    {
        if ((expDate == default) && (prodDate == default))
        {
            StringBuilder stringBuilder = new();

            stringBuilder.AppendFormat($"It is required to specify the expiration date or production date! ");
            stringBuilder.AppendFormat($"Expiration date: {expDate}, Production date: {prodDate}");
            WriteLine(stringBuilder.ToString());

            return false;
        }
        
        return true;
    }
}
