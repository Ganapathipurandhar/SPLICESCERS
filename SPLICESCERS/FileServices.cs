using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace SPLICESCERS
{
	public static class FileServices
    {
        public static List<T> ReadJson<T>(string filePath)
        {
            string text = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<List<T>>(text);
        }

        public static void ListToJson<T>(List<T> list, string destPath=@"./JsonData.json")
        {
            string  convertedJson = JsonSerializer.Serialize(list);

            using (StreamWriter writer = new StreamWriter(destPath))
            {
                writer.Write(convertedJson);
            }

        }

        public static void ListToCsv<T>(List<T> list, string destPath=@"./Data.csv")
        {
            string csv = list.ToCsv<T>();

            using(StreamWriter writer = new StreamWriter(destPath))
            {
                writer.Write(csv);
            }
        }

        public static void ObjectToJson<T>(T data, string destPath=@"./ObjectData.json")
        {
            string convertedJson = JsonSerializer.Serialize(data);

            using (StreamWriter writer = new StreamWriter(destPath))
            {
                writer.Write(convertedJson);
            }
        }

    }

}
