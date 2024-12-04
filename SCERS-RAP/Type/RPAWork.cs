using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCERS_RAP.Type
{
	public class RPAWork
	{
		//public PreLoad PreLoad { get; set; }

		//Computed Values for Work -Sheet
		public double ISDuration { get; set; }
		public double ISSickDuration { get; set; }
		public double TotalIS { get; set; }
		public double NonISDuration { get; set; }
		public double NonISSickDuration { get; set; }
		public double TotalNonIS { get; set; }
		public double TotalService { get; set; }

		//Article Number it will either 31676.1 or 31664"
		public string ERFArticle { get; set; }
		public double ERFFraction { get; set; }

		//Service Retirement Benefits
		public double IntegrateBenefits { get; set; }
		public double NonIntegrateBenefits { get; set; }
		public double ServiceRetirementBenefits { get; set; }
		public double MoneyPurchaseCalc { get; set; }//TODO: This needs to be calculated after calcData Sheet Completion
		public string NSCDArticle { get; set; }
		public double NSCDFraction { get; set; }
		public double Benefit90Perc { get; set; }
		public double FAS1by3 { get; set; }
		public double ServiceProjected { get; set; }
		public DateTime ServiceProjAge65 { get; set; }
		public double SerRetDiff { get; set; }//ServiceProjAge65 and DateOfRetirement difference
		public double SCD1by2FAS { get; set; }

	}
}
