using static System.Console;
using PalletStorage;

// Generate test values in Storage
Storage storage = TestGenerator.GenerateTestStorage();

WriteLine($"A new Storage test collection was generated:");
storage.Print();

// Save Storage in file
FileStorage fileStorage = new();
fileStorage.WriteToFile(storage);

// Read from file in new object
Storage? storageConverted = fileStorage.ReadFromFile<Storage>();

if (storageConverted is not null)
{
    WriteLine("");
    WriteLine($"Successful conversion from json to Storage");
}

// Read from file in new Contract-object for conversion to Storage
ContractStorage? storageConvertedToContract = fileStorage.ReadFromFile<ContractStorage>();

if (storageConvertedToContract is not null)
{
    WriteLine("");
    WriteLine($"Successful conversion from json to ContractStorage");
}

// Reports
storage.ReportTopWithMaxExpirationOrderByVolume(3);
storage.ReportPalletsOrderByExpirationAndWeight();


