using System.Text;
using static System.Console;

namespace PalletStorage.Сlasses.Extensions;

public static class PalletExtensions
{
    public static void Print(this Pallet obj)
    {
        StringBuilder stringBuilder = new();

        stringBuilder.AppendFormat($"Pallet: id: {obj.Id}, (w/l/h): {obj.Width}/{obj.Length}/{obj.Height}, weight: {obj.Weight}, volume: {obj.Volume},");
        stringBuilder.AppendFormat($"exp.date: {obj.ExpirationDate}, Count boxes: {obj.Boxes.Count}");

        WriteLine(stringBuilder.ToString());
    }
}

