using Newtonsoft.Json;

namespace PalletStorageTests;

public class ObjectComparison
{
    public static bool EqualByJson(object firstObject, object secondObject)
    {
        var firstData = JsonConvert.SerializeObject(firstObject);
        var secondData = JsonConvert.SerializeObject(secondObject);

        return firstData.Equals(secondData);
    }

    public static bool EqualByProperties(object firstObject, object secondObject)
    {
        Type firstType = firstObject.GetType();
        System.Reflection.PropertyInfo[] properties = firstType.GetProperties();

        Type secondType = secondObject.GetType();

        var isSame = true;

        foreach (var firstProperty in properties)
        {
            var secondProperty = secondType.GetProperty(firstProperty.Name);

            if (secondProperty == null)
            {
                isSame = false;
                break;
            }

            var firstValue = firstProperty.GetValue(firstObject);
            var secondValue = secondProperty.GetValue(secondObject);

            if (!object.Equals(firstValue, secondValue))
            {
                isSame = false;
                break;
            }
        }

        return isSame;
    }
}
