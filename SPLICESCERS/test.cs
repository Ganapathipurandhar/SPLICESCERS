using System;

using System.Configuration;

namespace SPLICESCERS
{
	public class test
	{
        public test()
        {
            
        }

        public void printName(string name) 
		{
			string sValue = ConfigurationManager.AppSettings["Name"];
			Console.WriteLine("My Name is :" + name);

			Console.WriteLine("My Config Name is :"  + sValue);
		}

		public string GetAppConfig(string appConfigName) 
		{
			string appConfigValue ="";
			//appConfigValue = ConfigurationManager.AppSettings[appConfigName];
			if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings[appConfigName]))
				appConfigValue = ConfigurationManager.AppSettings[appConfigName];
			return appConfigValue;

		}
		public void print(string stringValue)
		{		
			Console.WriteLine(stringValue);			
		}
	}
}
