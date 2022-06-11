using PalletStorage;
using PalletStorage.Infrastructure;
using PalletStorage.Сlasses;
using PalletStorage.Сlasses.Extensions;

// Read from file or generate new storage collection - // StorageCollection.CollectionForWork()
Storage storage = await StorageCollection.CollectionForWorkAsync();

// Output of information about the collection
storage.Print();

// Save Storage in file - // StorageCollection.SaveCollection(storage);
StorageCollection.SaveCollectionAsync(storage);

// Reports
storage.ReportTopWithMaxExpirationOrderByVolume(3);
storage.ReportPalletsOrderByExpirationAndWeight();



