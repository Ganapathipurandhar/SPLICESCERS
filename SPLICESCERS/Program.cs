using SPLICESCERS.Services;
using System;

namespace SPLICESCERS
{
	internal class Program
	{
		static void Main(string[] args)
		{			
			DataServices.LoadERF();

			//AppServices.printName("Load Memeber information from App.Config to WorkData ");
			AppServices.print("*******************************************************");
			AppServices.print("Load Memeber information from App.Config to WorkData ");
			AppServices.print("*******************************************************");

			WorkService workService = new WorkService();
            workService.LoadData();
			AppServices.print("****************************************************************");
			AppServices.print("Compute worksheet based on input and according to cell equation");
			AppServices.print("*****************************************************************");
			workService.ComputeWorkSheet();
			AppServices.print("****************************************************************");
			AppServices.print("Compute Factors based on input and according to cell equation");
			AppServices.print("*****************************************************************");
			FactorService fs = new FactorService(workService);
			fs.CalcLifeTable();
			workService.PrintProperty(workService.WD);
			//fs.print(_test);

			Console.ReadKey();
		}
	}
}
