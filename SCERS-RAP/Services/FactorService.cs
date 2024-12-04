using SCERS_RAP.Type;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCERS_RAP.Services
{
	public class FactorService
	{
		private RPAData rd;
		private RPAWork work;
		private PreLoad pl;
		private Factor f;
		private List<LifeTable> lts;

		public FactorService(RPAData rd) 
		{
			this.rd = rd;
			this.work = this.rd.Work;
			this.pl = rd.PreLoad;
			this.rd.Factor = new Factor();
			this.f = this.rd.Factor;
		}

		public void CalculateFactor() 
		{
			prepareForLifeTable();
			computeLifeTable();
		}

		private void prepareForLifeTable() 
		{
			//Life Table Computation Age Options
			f.XMinusY = (int)(pl.MemberInfo.AgeTrunc - pl.BeneficiaryInfo.AgeTrunc);
			f.XMinusYPlus1 = (int)(pl.MemberInfo.AgeTrunc - (pl.BeneficiaryInfo.AgeTrunc + 1));
			f.XPlus1MinusY = (int)((pl.MemberInfo.AgeTrunc + 1) -pl.BeneficiaryInfo.AgeTrunc);
			f.XPlus1MinusYPlus1 = (int)((pl.MemberInfo.AgeTrunc + 1) - (pl.BeneficiaryInfo.AgeTrunc + 1));
			f.IRCOLA = (1 + pl.InterestRate) / (1 + pl.COLA) - 1;
		}

		private void computeLifeTable() 
		{
			LifeTableService lifeTableService = new LifeTableService(rd);
			lifeTableService.CalculateLifeTable();
			lts = rd.LifeTables;
		}
	}
}
