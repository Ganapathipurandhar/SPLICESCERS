using SCERS_RAP.Services;
using SCERS_RAP.Type;
using System;
using System.Collections.Generic;
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
			//On Application Start
			//Load Static Data
			DataServices.LoadStaticData();//var test = DataServices.GetValue<ERF, double>(DataServices.ERF31676_10, "AgeAtRetirement", 61, "Fraction");
			//Run RPA Service in sequence of Data needs
			RAPService RAP = new RAPService();
			RAP.Run();
			Console.ReadKey();
		}
	}
}
