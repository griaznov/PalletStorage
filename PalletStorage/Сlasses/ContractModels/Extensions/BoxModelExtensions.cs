namespace PalletStorage.Сlasses.ContractModels.Extensions;

public static class StorageBoxExtensions
{
    public static StorageBox FromModel(this BoxModel model) =>
        new(model.Width, model.Length, model.Height, model.Weight, model.ProductionDate, model.ExpirationDate, model.Id);

    public static BoxModel ToModel(this StorageBox input) => new()
    {
        Width = input.Width,
        Length = input.Length,
        Height = input.Height,
        Weight = input.Weight,
        Volume = input.Volume,
        ProductionDate = input.ProductionDate,
        ExpirationDate = input.ExpirationDate,
        Id = input.Id
    };
}
