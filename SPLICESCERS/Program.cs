using SPLICESCERS.Work;
using System;
using System.Collections.Generic;
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
			_test.printName("Hemant");
			

			WorkService workService = new WorkService();
			workService.LoadData();

            Console.ReadKey();
        }


	}
}
