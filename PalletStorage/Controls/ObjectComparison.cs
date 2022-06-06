using Newtonsoft.Json;

namespace PalletStorage;

public class ObjectComparison
{
    public static bool EqualByJson(object firstObject, object secondObject)
    {
        string? firstData = JsonConvert.SerializeObject(firstObject);
        string? secondData = JsonConvert.SerializeObject(secondObject);

        return firstData.Equals(secondData);
    }

    public static bool EqualByProperties(object firstObject, object secondObjectj)
    {
        Type firstType = firstObject.GetType();
        System.Reflection.PropertyInfo[] properties = firstType.GetProperties();

        Type secondType = secondObjectj.GetType();

        bool isSame = true;

        foreach (var firstProperty in properties)
        {
            var secondProperty = secondType.GetProperty(firstProperty.Name);

            if (secondProperty == null)
            {
                isSame = false;
                break;
            }

            var firstValue = firstProperty.GetValue(firstObject);
            var secondValue = secondProperty.GetValue(secondObjectj);

            if (!object.Equals(firstValue, secondValue))
            {
                isSame = false;
                break;
            }
        }

        return isSame;
    }
}
