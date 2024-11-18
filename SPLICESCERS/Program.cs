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
			_test.printName("Hemant");
						
			DateTime retirementDate = DateTime.Parse( _test.GetAppConfig("DateOfRetirement"));
			DateTime memberDoB = DateTime.Parse(_test.GetAppConfig("MemberDOB"));

			var timeSpan = memberDoB.Subtract(retirementDate);

			var totalDays = (retirementDate - memberDoB).TotalDays;
			var age = Math.Round((totalDays / 365.25), 2);
			_test.print("Age : " + age.ToString());
			_test.print("Age1by4 : " + Math.Floor(age));
			//var x = Math.Round(age * 4, MidpointRounding.ToEven) / 4;
			var x = Math.Floor(age / 0.25 + 0) * 0.25;
			_test.print("Age1by4 : " + x.ToString());
			double agey = 61.35;
			var y = Math.Floor(agey / 0.25 + 0) * 0.25;
			_test.print("Age1by4 : " + y.ToString());

            WorkService workService = new WorkService();
            workService.LoadData();

            Console.ReadKey();
		}


	}
}
