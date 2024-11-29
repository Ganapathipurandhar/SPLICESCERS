using SPLICESCERS.Services;
using System;

namespace SPLICESCERS
{
	internal class Program
	{
		static void Main(string[] args)
		{
			var _test = new test();
			DataServices.LoadERF();

			//_test.printName("Load Memeber information from App.Config to WorkData ");
			_test.print("*******************************************************");
			_test.print("Load Memeber information from App.Config to WorkData ");
			_test.print("*******************************************************");

			WorkService workService = new WorkService();
            workService.LoadData();
			_test.print("****************************************************************");
			_test.print("Compute worksheet based on input and according to cell equation");
			_test.print("*****************************************************************");
			workService.ComputeWorkSheet();
			_test.print("****************************************************************");
			_test.print("Compute Factors based on input and according to cell equation");
			_test.print("*****************************************************************");
			FactorService fs = new FactorService(workService);
			fs.CalcLifeTable();
			workService.PrintProperty(workService.WD);
			//fs.print(_test);

			Console.ReadKey();
		}
	}
}
