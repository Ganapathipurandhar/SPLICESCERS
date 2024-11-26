using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SPLICESCERS
{
    public static class JsonFileReader
    {
        public static List<T> Read<T>(string filePath)
        {
            string text = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<List<T>>(text);
        }

        public static string ListToJson<T>(List<T> list)
        {
            return JsonSerializer.Serialize(list); 
        }

    }

}
