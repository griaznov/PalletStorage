using static System.Console;
using Ex.PalletStorage;

// Generate test values in Storage
Storage storage = TestGenerator.GenerateTestStorage();
storage.Print();

// Save Storage in file
FileStorage storageToFile = new();

// Read from file in new object
storageToFile.WriteStorageToFile(storage);
Storage? storageConverted = storageToFile.ReadStorageFromFile();

if (storageConverted is not null)
{
    WriteLine("");
    WriteLine($"Successful conversion from json to Storage");
}

// Read from file in new Contract object for cinversion in Storage
ContractStorage? storageConvertedToContract = storageToFile.ReadContractStorageFromFile();

if (storageConvertedToContract is not null)
{
    WriteLine("");
    WriteLine($"Successful conversion from json to ContractStorage");
}

// Reports
storage.ReportTopWithMaxExpirationOrderByVolume(3);
storage.ReportPalletsOrderByExpirationAndWeight();


