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
			_test.printName("Load Memeber information from App.Config to WorkData ");
			
            WorkService workService = new WorkService();
            workService.LoadData();
			workService.ComputeWorkSheet();

			Console.ReadKey();
		}
	}
}
