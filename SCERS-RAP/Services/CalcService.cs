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

			calc.AnnuityFactor = f.X14 * 12;
			//TODO: Rounding increases the compensation value by 67 cents
			calc.Total = pl.MoneyPurchase == YesNo.Yes ? pl.EEContrBasic * 2 :
							Math.Round(calc.MonthlyBenefits * calc.AnnuityFactor, 2);

			calc.AnnuityBenefit = pl.EEContrBasic / calc.AnnuityFactor;
			calc.PensionReserve = Math.Round(calc.Total - pl.EEContrBasic,2);
			calc.PensionBenefit = calc.MonthlyBenefits - calc.AnnuityBenefit;
			//TODO: Is 0.6 a standard multiplier or should be passed as pre-load
			calc.CtB = Math.Round(calc.MonthlyBenefits * 0.6, 2);
			calc.OP1AnnuityBenefit = Math.Round(calc.AnnuityBenefit * f.Option1, 2);
			calc.OP1PensionBenefit = calc.PensionBenefit;
			calc.OP1Total = calc.OP1AnnuityBenefit + calc.OP1PensionBenefit;
			;

			//Option 2 - 100% Continuance
			calc.OP2AnnuityFactor = (f.X14 +  f.Y14 - f.XY14) * 12;
			calc.OP2AnnuityBenefit = pl.EEContrBasic / calc.OP2AnnuityFactor;
			calc.OP2PensionBenefit = calc.PensionReserve/ calc.OP2AnnuityFactor;
			calc.OP2Total = calc.OP2PensionBenefit + calc.OP2AnnuityBenefit;
			calc.OP2CtB = calc.OP2Total;

			//Option 3
			calc.OP3AnnuityFactor = (f.X14 +  0.5 *(f.Y14 - f.XY14)) * 12;
			//TOD
			calc.OP3AnnuityBenefit = pl.EEContrBasic / calc.OP3AnnuityFactor;
			calc.OP3PensionBenefit = calc.PensionReserve / calc.OP3AnnuityFactor;
			calc.OP3Total = calc.OP3PensionBenefit + calc.OP3AnnuityBenefit;
			calc.OP3CtB = calc.OP3Total/2;

			//ROUND(G20 * 12 * (0.6 * (Calc!C18 - Calc!C19)),2)," ")
			calc.BasicSpouse = pl.RelationShip == RelationShip.Spouse ? 
							(calc.MonthlyBenefits * 12 *(0.6 *(f.Y14 - f.XY14))) : 0;

			//= IF(LOWER(Work!F$11) = "spouse", ROUND(G20 * 12 * (0.6 * (Work!E56 - Work!E57)),2)-M28," ")
			calc.COLSpouse = pl.RelationShip == RelationShip.Spouse ?
							(calc.MonthlyBenefits * 12 * (0.6 * (f.Y14Prime - f.XY14Prime))) - calc.BasicSpouse : 0;
			//=ROUND((G20*Work!$E55*12)-G28,2)
			calc.COLTotal = (calc.MonthlyBenefits * f.X14Prime * 12) - calc.Total;
			calc.COLCSP = calc.COLTotal - pl.EEContrCol;
		}

	}
}
