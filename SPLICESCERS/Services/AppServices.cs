using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPLICESCERS.Services
{
	public static class AppServices
	{
		//
		//MathSerives
		
		public static double Ceiling(double value, double significance) 
		{
			if (value == 0 || significance == 0) 
			{
				return value;
			} 
			return Math.Ceiling(value / significance) * significance; 
		}

		public static string GetAppConfig(string appConfigName)
		{
			string appConfigValue = "";
			//appConfigValue = ConfigurationManager.AppSettings[appConfigName];
			if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings[appConfigName]))
				appConfigValue = ConfigurationManager.AppSettings[appConfigName];
			return appConfigValue;

		}
		public static void print(string stringValue)
		{
			Console.WriteLine(stringValue);
		}
	}
}
