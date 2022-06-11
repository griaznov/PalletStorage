using PalletStorage.Infrastructure;
using PalletStorage.Сlasses;
using PalletStorage.Сlasses.Extensions;

// Read from file or generate new storage collection
Storage storage = await StorageCollection.CollectionForWorkAsync();

// Output of information about the collection
storage.Print();

// Save Storage in file with conversion in contract model
StorageCollection.SaveCollectionInModelAsync(storage);

// Reports
storage.PrintTopWithMaxExpirationOrderByVolume(3);
storage.PrintPalletsOrderByExpirationAndWeight();
