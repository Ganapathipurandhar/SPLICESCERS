using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.IO;
using System.Text.Json;

namespace SCERS_RAP.Services
{
	public class AppServices
	{
		#region Math Functions
		/// <summary>
		/// Calculate Ceiling with Significance - Equivalent to Excel ceiling function;
		/// </summary>
		/// <param name="value"></param>
		/// <param name="significance"></param>
		/// <returns></returns>
		public static double Ceiling(double value, double significance)
		{
			if (value == 0 || significance == 0)
			{
				return value;
			}
			return Math.Ceiling(value / significance) * significance;
		}

		/// <summary>
		/// Calculate Floor with Significance - Equivalent to Excel Floor function;
		/// </summary>
		/// <param name="value"></param>
		/// <param name="significance"></param>
		/// <returns></returns>
		public static double Floor(double value, double significance)
		{
			if (value == 0 || significance == 0)
			{
				return value;
			}
			return Math.Floor(value / significance) * significance;
		}
		#endregion

		#region File Services
		public static List<T> ReadJson<T>(string filePath)
		{
			string text = File.ReadAllText(filePath);
			return JsonSerializer.Deserialize<List<T>>(text);
		}

		public static T JsonToObject<T>(string filePath)
		{
			string text = File.ReadAllText(filePath);
			return JsonSerializer.Deserialize<T>(text);
		}

		public static void ListToJson<T>(List<T> list, string destPath)
		{
			string convertedJson = JsonSerializer.Serialize(list);

			using (StreamWriter writer = new StreamWriter(destPath))
			{
				writer.Write(convertedJson);
			}

		}

		public static void ListToCsv<T>(List<T> list, string destPath)
		{
			string csv = list.ToCsv<T>();

			using (StreamWriter writer = new StreamWriter(destPath))
			{
				writer.Write(csv);
			}
		}

		public static void ObjectToJson<T>(T data, string destPath)
		{
			string convertedJson = JsonSerializer.Serialize(data);

			using (StreamWriter writer = new StreamWriter(destPath))
			{
				writer.Write(convertedJson);
			}
		}
		#endregion

		public static string GetAppConfig(string appConfigName)
		{
			string appConfigValue = "";
			if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings[appConfigName]))
				appConfigValue = ConfigurationManager.AppSettings[appConfigName];
			return appConfigValue;
		}

		public void PrintProperty(object t)
		{
			foreach (PropertyDescriptor descriptor in TypeDescriptor.GetProperties(t))
			{
				string name = descriptor.Name;
				object value = descriptor.GetValue(t);
				System.Type _type = value.GetType();
				Console.WriteLine("{0}={1}", name, value);

				if (_type.IsEnum || (_type.Namespace == "System"))
				{ }
				else
				{ PrintProperty(value); }
			}
		}
	}
}
