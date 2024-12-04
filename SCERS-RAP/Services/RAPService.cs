using SCERS_RAP.Type;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCERS_RAP.Services
{
	public class RAPService
	{
		public RPAData RPAData { get; set; }
		private PreLoad pl;
		private WorkService ws;
		private FactorService fs;

		public RAPService() 
		{
			RPAData = new RPAData();
			//RPAData.PreLoad = new PreLoad();
		}

		public void Run() 
		{
			RPAData.PreLoad = AppServices.JsonToObject<PreLoad>(@".\Data\PreLoad.json");			
			postPreLoad();
			AppServices.Print("******************************************************");
			AppServices.Print("Pre Load Data");
			AppServices.Print("******************************************************");
			AppServices.PrintProperty(RPAData.PreLoad);
			ws = new WorkService(RPAData);
			ws.CalculateWork();
			AppServices.Print("******************************************************");
			AppServices.Print("Work Sheet Data");
			AppServices.Print("******************************************************");
			AppServices.PrintProperty(RPAData.Work);
			fs = new FactorService(RPAData);
			fs.CalculateFactor();
			//Save Life Table to CSV File
			AppServices.ListToCsv(RPAData.LifeTables, @"./Data.csv");
			AppServices.Print("******************************************************");
			AppServices.Print("Factor Data");
			AppServices.Print("******************************************************");
			AppServices.PrintProperty(RPAData.Factor);

		}

		private void postPreLoad() 
		{
			pl = RPAData.PreLoad;
			//Calculate Age
			calculateAgeAtRetirement(pl.DateOfRetirement, pl.MemberInfo);
			calculateAgeAtRetirement(pl.DateOfRetirement, pl.BeneficiaryInfo);
		}


		private void calculateAgeAtRetirement(DateTime retirementDate, PersonInfo member)
		{
			var totalDays = (retirementDate - member.DOB).TotalDays;
			member.Age = Math.Round((totalDays / 365.25), 2);
			member.Age1by4 = AppServices.Floor(member.Age,0.25);
			member.AgeTrunc = Math.Truncate(member.Age);
		}
		

	}
}
