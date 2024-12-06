using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SCERS_RAP.Services
{
    public class HTMLService
    {
        public string templateFileData { get; set; }
        public string newDataString { get; set; }
        public HTMLService(string templateFilepath=@"./HTMLTemplate.html") 
        {
            templateFileData = File.ReadAllText(templateFilepath);
        }

        public void findAndReplace(string propName, string propValue) 
        {
            if (templateFileData.Contains("[" + propName + "]"))
            {
                newDataString = templateFileData.Replace("[" + propName + "]", propValue);
            }

            templateFileData = newDataString;
        }

        public void BuildOutputFile(string outputFilepath= @"./Output.html")
        {
            if (!string.IsNullOrEmpty(templateFileData))
            {
                using (StreamWriter writer = new StreamWriter(outputFilepath))
                {
                    writer.WriteLine(templateFileData);
                }
            }
        }

        public static void ReplaceFromObject<T>(T obj, string templateFilepath = @"./HTMLTemplate.html")
        {
            string templateData = File.ReadAllText(templateFilepath);

            foreach (PropertyInfo property in typeof(T).GetProperties()) 
            {
                if (templateData.Contains("[" + property.Name + "]"))
                {
                    var newDataString = templateData.Replace("[" + property.Name + "]", Convert.ToString(property.GetValue(obj)));
                    templateData = newDataString;
                }
            }

            using (StreamWriter writer = new StreamWriter(@"./Output.html"))
            {
                writer.WriteLine(templateData);
            }
        }
    }
}
