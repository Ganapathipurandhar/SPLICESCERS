using SPLICESCERS.Work;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPLICESCERS
{
	internal class Program
	{
		static void Main(string[] args)
		{
			var _test = new test();
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

			Console.ReadKey();
		}
	}
}
