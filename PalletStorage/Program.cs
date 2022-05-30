using Ex.PalletStorage;

Storage storage = TestGenerator.GenerateStorage();

FileStorage storageToFile = new();

storageToFile.WriteStorageToFile(storage);

Storage? storage1 = storageToFile.ReadStorageFromFile();

storage.ReportTopWithMaxExpirationOrderByVolume(3);

storage.ReportPalletsOrderByExpirationAndWeight();

//storageToFile.WriteToFile(Box1, "box1.json");
//var Box3 = storageToFile.ReadFromFile("box1.json");

//storageToFile.WriteToFile(pallet1, "pallet1.json");
//var pallet2 = storageToFile.ReadPallletFromFile("pallet1.json");
