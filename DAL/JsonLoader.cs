using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Newtonsoft.Json;

namespace DataAccessLayer
{
    public class JsonLoader
    {
        public static T LoadFromFile<T>(string filePath)
        {
            using (StreamReader reader = new StreamReader(filePath))
            {
                string json = reader.ReadToEnd();
                T result = JsonConvert.DeserializeObject<T>(json);
                return result;
            }
        }

        public static T Deserialize<T>(object jsonObject)
        {
            return JsonConvert.DeserializeObject<T>(Convert.ToString(jsonObject));
        }
    }
}
