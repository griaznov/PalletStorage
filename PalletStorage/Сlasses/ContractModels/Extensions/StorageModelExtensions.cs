using System.Text.Json;
namespace PalletStorage.Сlasses.ContractModels.Extensions;

public static class StorageModelExtensions
{
    public static Storage FromModel(this StorageModel model) => new(
        name: model.Name,
        id: model.Id,
        boxes: model.Boxes.ToDictionary(pair => pair.Key, pair => pair.Value.FromModel()),
        pallets: model.Pallets.ToDictionary(pair => pair.Key, pair => pair.Value.FromModel()));

    public static StorageModel ToModel(this Storage input) => new()
    {
        Id = input.Id,
        Name = input.Name,
        Boxes = input.Boxes.ToDictionary(pair => pair.Key, pair => pair.Value.ToModel()),
        Pallets = input.Pallets.ToDictionary(pair => pair.Key, pair => pair.Value.ToModel())
    };
}

