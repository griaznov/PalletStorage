
using static System.Console;

namespace PalletStorage.Сlasses.Extensions;

public static class UniversalBoxExtensions
{
    public static void Print(this UniversalBox universalBox)
    {
        WriteLine($"Box: {universalBox.Width}/{universalBox.Length}/{universalBox.Height} (w/l/h), " +
                  $"weight: {universalBox.Weight}, volume: {universalBox.Volume}");
    }
}

