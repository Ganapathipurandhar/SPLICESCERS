using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
using Newtonsoft.Json;
using System.IO;

namespace SPLICESCERS
{
    public class ExcelToJson
    {
        public static void ConvertExcelToJson(string sourceXlPath,string strXLSheetName, string destJSONPath)
        {
            //This connection string works if you have Office 2007+ installed and your 
            //data is saved in a .xlsx file
            var connectionString = String.Format(@"Provider=Microsoft.ACE.OLEDB.12.0; Data Source={0}; Extended Properties=""Excel 12.0 Xml;HDR=YES""", sourceXlPath);

            //Creating and opening a data connection to the Excel sheet 
            using (var conn = new OleDbConnection(connectionString))
            {
                conn.Open();

                var cmd = conn.CreateCommand();
                cmd.CommandText = String.Format(@"SELECT * FROM [{0}$]", strXLSheetName);


                using (var rdr = cmd.ExecuteReader())
                {
                    //LINQ query - when executed will create anonymous objects for each row
                    var query = (from DbDataRecord row in rdr select row).Select(x =>
                         {
                             Dictionary<string, string> item = new Dictionary<string, string>();
                             item.Add(rdr.GetName(0), Convert.ToString(x[0]));
                             item.Add(rdr.GetName(1), Convert.ToString(x[1]));
                             return item;

                         });

                    //Generates JSON from the LINQ query
                    var json = JsonConvert.SerializeObject(query);

                    using (StreamWriter writer = new StreamWriter(destJSONPath))
                    {
                        writer.WriteLine(json);
                    }
                }
            }
        }
    }
}
