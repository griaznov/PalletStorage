using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex.PalletStorage;

interface IUniversalBox
{
    //public IUniversalBox Create();
}

public interface IFileStorage
{
    public void WriteToFile<T>(T value, string fileName);
    //public T? ReadFromFile<T>(string fileName);
}
