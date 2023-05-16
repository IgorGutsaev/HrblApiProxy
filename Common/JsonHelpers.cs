//using Newtonsoft.Json;
//using Newtonsoft.Json.Linq;
//using System;

using System.Runtime.CompilerServices;

namespace Filuet.Hrbl.Ordering.Common
{

    public static class HrblResponseHelpers
    {
        public static string ResolveHrblMess(this string response)
            => response.Replace("{{\"@nil\":\"true\"}}", "null")
            .Replace("\"\"", "null"); // Deserialize empty string as null
    }
//    public class HrblNullableResponseConverter<T> : JsonCreationConverter<T> where T : class, new()
//    {
//        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
//        {
//            throw new NotImplementedException();
//        }

//        protected override T Create(Type objectType, JObject jObject)
//            => JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(jObject).Replace("{\"@nil\":\"true\"}", "null"));
//    }

//    public abstract class JsonCreationConverter<T> : JsonConverter where T : class
//    {
//        /// <summary>
//        /// Create an instance of objectType, based properties in the JSON object
//        /// </summary>
//        /// <param name="objectType">type of object expected</param>
//        /// <param name="jObject">
//        /// contents of JSON object that will be deserialized
//        /// </param>
//        /// <returns></returns>
//        protected abstract T Create(Type objectType, JObject jObject);

//        public override bool CanConvert(Type objectType)
//        {
//            return typeof(T).IsAssignableFrom(objectType);
//        }

//        public override bool CanWrite
//        {
//            get { return false; }
//        }

//        public override object ReadJson(JsonReader reader,
//                                        Type objectType,
//                                         object existingValue,
//                                         JsonSerializer serializer)
//        {
//            // Load JObject from stream
//            JObject jObject = JObject.Load(reader);

//            // Create target object based on JObject
//            T target = Create(objectType, jObject);

//            return target;
//        }
//    }
}