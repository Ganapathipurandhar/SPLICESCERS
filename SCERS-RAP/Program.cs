using SCERS_RAP.Services;
using SCERS_RAP.Type;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCERS_RAP
{
	//SACRAMENTO COUNTY EMPLOYEES' RETIREMENT SYSTEM (SCERS)
	//RETIREMENT ANNUITY PLAN
	//HKK 
	public class Program
	{
		static void Main(string[] args)
		{
			Stopwatch stopwatch = new Stopwatch();

			stopwatch.Start();
			//On Application Start
			//Load Static Data
			DataServices.LoadStaticData();//var test = DataServices.GetValue<ERF, double>(DataServices.ERF31676_10, "AgeAtRetirement", 61, "Fraction");
										  //Run RPA Service in sequence of Data needs
			//stopwatch.Start();
			RAPService RAP = new RAPService();
			RAP.Run();
			stopwatch.Stop();

			TimeSpan elapsedTime = stopwatch.Elapsed;
			Console.WriteLine($"Program execution time: {elapsedTime.TotalSeconds} seconds");

			Console.ReadKey();
		}
	}
}
