using SCERS_RAP.Type;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCERS_RAP.Services
{
	public class CalcService
	{
		private RPAData rd;
		private RPAWork work;
		private PreLoad pl;
		private Factor f;
		private List<LifeTable> lts;
		private Calc calc;

		public CalcService(RPAData rd) 
		{
			this.rd = rd;
			this.work = rd.Work;
			this.pl = rd.PreLoad;			
			this.f = rd.Factor;
			this.lts = rd.LifeTables;
			rd.Calc = new Calc();
			this.calc = rd.Calc;
		}

		public void ComputeCalc() 
		{
			//Calc Computation
			calc.MonthlyBenefits = (pl.TypeOfRetirement == RetirementType.SCD) ? Math.Max(work.ServiceRetirementBenefits, work.SCD1by2FAS)
										: (pl.MoneyPurchase == YesNo.Yes) ? work.MoneyPurchaseCalc
										: (pl.TypeOfRetirement == RetirementType.NSCD) ? work.NSCDFraction : work.ServiceRetirementBenefits;
			

		}

	}
}
