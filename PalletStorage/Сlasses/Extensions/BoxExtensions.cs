using System.Text;
using static System.Console;

namespace PalletStorage.Сlasses.Extensions;

public static class StorageBoxExtensions
{
    public static void Print(this StorageBox box)
    {
        StringBuilder stringBuilder = new();

        stringBuilder.AppendFormat($"Box: id: {box.Id}, (w/l/h): {box.Width}/{box.Length}/{box.Height}, weight: {box.Weight}, volume: {box.Volume}, ");
        stringBuilder.AppendFormat($"prod.date: {box.ProductionDate}, exp.date: {box.ExpirationDate},");

        WriteLine(stringBuilder.ToString());
    }
}

