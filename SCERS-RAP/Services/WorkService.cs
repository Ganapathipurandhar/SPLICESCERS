using SCERS_RAP.Type;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SCERS_RAP.Services
{
	public class WorkService
	{
		private RPAData rd;
		private RPAWork work;
		private PreLoad pl;

		double _divider;

		public WorkService(RPAData rd)
		{
			this.rd = rd;
			this.rd.Work = new RPAWork();
			this.work = this.rd.Work;
			this.pl = rd.PreLoad;
		}

		public void CalculateWork()
		{
			calculateServiceDuration();
			calculateERF();
			calculateServiceBenefit();
			calculateNSCD();
			calculateSCD();

		}

		private void calculateServiceDuration()
		{
			work.ISDuration = yearInDecimal(pl.IntegratedService);
			work.ISSickDuration = yearInDecimal(pl.ISSick);
			work.TotalIS = work.ISDuration + work.ISSickDuration;

			work.NonISDuration = yearInDecimal(pl.NonIntegratedService);
			work.NonISSickDuration = yearInDecimal(pl.NonISSick);
			work.TotalNonIS = work.NonISDuration + work.NonISSickDuration;

			work.TotalService = work.TotalIS + work.TotalNonIS;
		}

		private double yearInDecimal(DurationYMDs duration)
		{
			double _yearInDecimal = duration.Years + (duration.Months / 12) + (duration.Days / 261);
			return _yearInDecimal;
		}


		private void calculateERF()
		{
			if (pl.Membership == MembershipType.General)
			{
				work.ERFArticle = "ERF 31676.1";
				work.ERFFraction = DataServices.GetFraction(DataServices.ERF31676_10, pl.MemberInfo.Age1by4);
			}
			else
			{
				work.ERFArticle = "ERF 31664";
				work.ERFFraction = DataServices.GetFraction(DataServices.ERF31664, pl.MemberInfo.Age1by4);
			}
		}

		private void calculateServiceBenefit()
		{
			_divider = pl.Membership == MembershipType.Safety ? 50 : 60;

			//= DATE((YEAR(B8) - 1900) + IF(LOWER(B3) = "general", 65, 55), MONTH(B8), DAY(B8))
			work.ServiceProjAge65 = pl.MemberInfo.DOB
					.AddYears((pl.Membership == MembershipType.General) ? 65 : 55);
			work.SerRetDiff = Math.Round(((work.ServiceProjAge65 - pl.DateOfRetirement).TotalDays) / 365, 4);

			//116.67 is reduced retirement allowance
			work.IntegrateBenefits = ((pl.FinalComp - 116.67) / _divider) * (work.TotalIS * work.ERFFraction);
			work.NonIntegrateBenefits = (pl.FinalComp / _divider) * (work.TotalNonIS * work.ERFFraction);
			work.ServiceRetirementBenefits = work.IntegrateBenefits + work.NonIntegrateBenefits;

			//TODO - Calculate this after Calc Sheet Is complete
			//_workData.MoneyPurchaseCalc// TODO
		}

		private void calculateNSCD() 
		{
			//Calculate NSCD (Non-Service connected Disability)
			work.NSCDArticle = (pl.Tier == Tiers.Two || pl.Tier == Tiers.Three) ? "NSCD: 31727.7" : "NSCD";
			if (pl.Tier == Tiers.Two || pl.Tier == Tiers.Three)
			{
				//(0.1 + 0.02 * MINA(15, FLOOR(B28, 1)))*B31                
				work.NSCDFraction = (0.1 + 0.02 * (Math.Min(15, Math.Floor(work.TotalService)))) * pl.FinalComp;
				//_workData.Benefit90Perc = null;
				//_workData.FAS1by3 = null;
				//_workData.ServiceProjected = null;	
			}
			else
			{
				//TODO - Have NA Flag
				if (pl.TypeOfRetirement == RetirementType.NSCD)
				{
					//IF(UPPER(B1) = "NSCD", ROUND(0.9 * (B31 * B28 / IF(LOWER(B3) = "safety", 50, 60)), 2), "N/A"))
					work.Benefit90Perc = Math.Round(0.9 * (pl.FinalComp * work.TotalService / _divider), 2);
					//IF(UPPER(B1) = "NSCD", ROUND(B31 / 3, 2), "N/A")
					work.FAS1by3 = Math.Round(pl.FinalComp / 3, 2);
					//IF(UPPER(B1) = "NSCD", ROUND(0.9 * (B31 * (B46 + B28) / IF(LOWER(B3) = "safety", 50, 60)), 2), "N/A"))
					work.ServiceProjected = Math.Round(0.9 * (pl.FinalComp * (work.SerRetDiff + work.TotalService) / _divider));
				}
				else
				{
					//TODO - Set Everything to N/A or how to handle NA
					//_workData.Benefit90Perc =
					//_workData.FAS1by3 =
				}

				//MAXA(MINA(B43, B42), B35, B41))
				double[] maxlist = { Math.Min(work.FAS1by3, work.ServiceProjected),
									work.ServiceRetirementBenefits, work.Benefit90Perc };
				work.NSCDFraction = maxlist.Max();
			}
		}

		private void calculateSCD() 
		{
			//Calculate SCD (Service connected Disability)
			if (pl.TypeOfRetirement == RetirementType.SCD)
			{
				work.SCD1by2FAS = Math.Round(pl.FinalComp / 2, 2);
			}
		}

	}
}
