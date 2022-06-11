namespace PalletStorage.Сlasses.ContractModels.Extensions;
public static class PalletModelExtensions
{
    public static Pallet FromModel(this PalletModel model) => new(
        model.Width,
        model.Length,
        model.Height,
        model.Weight,
        id: model.Id,
        boxes: model.Boxes.Select(item => item.FromModel()).ToList());

    public static PalletModel ToModel(this Pallet input) => new()
    {
        Width = input.Width,
        Length = input.Length,
        Height = input.Height,
        Weight = input.Weight,
        Volume = input.Volume,
        Id = input.Id,
        Boxes = input.Boxes.Select(item => item.ToModel()).ToList(),
    };
}

