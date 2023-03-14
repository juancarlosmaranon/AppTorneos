using System.Text.Json.Serialization;
using System.Text.Json;
using System.Text;
using Newtonsoft.Json;

namespace AppTorneos.Helpers
{
    public class HelperJsonSession
    {
        //public static T DeserializeObject<T>(string data)
        //{
        //    T objeto = JsonConvert.DeserializeObject<T>(data)!;
        //    return objeto;
        //}

        //public static string SerializeObject(object obj)
        //{
        //    string data = JsonConvert.SerializeObject(obj);
        //    return data;
        //}

        ///// <summary>
        ///// Convert an object to a Byte Array.
        ///// </summary>
        //public static byte[] ObjectToByteArray(object objData)
        //{
        //    if (objData == null)
        //        return default;

        //    return Encoding.UTF8.GetBytes(System.Text.Json.JsonSerializer.Serialize(objData, GetJsonSerializerOptions()));
        //}

        ///// <summary>
        ///// Convert a byte array to an Object of T.
        ///// </summary>
        //public static T ByteArrayToObject<T>(byte[] byteArray)
        //{
        //    if (byteArray == null || !byteArray.Any())
        //        return default;

        //    return System.Text.Json.JsonSerializer.Deserialize<T>(byteArray, GetJsonSerializerOptions());
        //}

        //private static JsonSerializerOptions GetJsonSerializerOptions()
        //{
        //    return new JsonSerializerOptions()
        //    {
        //        PropertyNamingPolicy = null,
        //        WriteIndented = true,
        //        AllowTrailingCommas = true,
        //        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
        //    };
        //}
    }
}
