using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCERS_RAP.Type
{	
	public class Calc
	{
		public double MonthlyBenefits { get; set; }	
		public double AnnuityFactor { get; set; }
		public double Total { get; set; }
		public double AnnuityBenefit { get; set; }
		public double PensionReserve { get; set; }
		public double PensionBenefit { get; set; }

		public double BasicSpouse { get; set; }
		

		public double CtB { get;set; }//Continuance To Beneficiary 
		public double OP1AnnuityBenefit { get; set; }//Option 1 Annuity Benefit
		public double OP1PensionBenefit { get; set; }//Option 1 Pension Benefit
		public double OP1Total { get; set; } //Option 1 Total

		public double OP2AnnuityFactor { get; set; } //Option 2 Annuity Factor
		public double OP2AnnuityBenefit { get; set; }//Option 2 Annuity Benefit
		public double OP2PensionBenefit { get; set; }//Option 2 Pension Benefit
		public double OP2Total { get; set; } //Option 2 Total
		public double OP2CtB { get; set; }

		public double OP3AnnuityFactor { get; set; } //Option 3 Annuity Factor
		public double OP3AnnuityBenefit { get; set; }//Option 3 Annuity Benefit
		public double OP3PensionBenefit { get; set; }//Option 3 Pension Benefit
		public double OP3Total { get; set; } //Option 3 Total
		public double OP3CtB { get; set; }

		public double COLSpouse { get; set; }
		public double COLTotal{ get; set; }
		public double COLCSP { get; set; }
	}

}
