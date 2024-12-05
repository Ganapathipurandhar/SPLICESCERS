using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
    }
}
