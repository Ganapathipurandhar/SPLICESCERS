using SPLICESCERS.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPLICESCERS.Types
{
	public class WorkData
	{
		private PersonInfo memberInfo = new PersonInfo();
		private PersonInfo beneficiaryInfo = new PersonInfo();
		private DurationYMDs integratedService = new DurationYMDs();
		private DurationYMDs iSSick = new DurationYMDs();
		private DurationYMDs nonIntegratedService = new DurationYMDs();
		private DurationYMDs nonISSick = new DurationYMDs();

		//TODO : This defaults will need to be calculated, temp setup to run factors
		private int memberMortalityTable = 1;
		private int memberSetback = -3;
		private int beneficiaryMortalityTable = 1;
		private int beneficiarySetback = -3;

		#region InputValueFromConfig
		public RetirementType TypeOfRetirement { get; set; }
		public DateTime DateOfRetirement { get; set; }
		public MembershipType Membership { get; set; }
		public Tiers Tier { get; set; }
		public YesNo MoneyPurchase { get; set; }

		//Member Information
		public PersonInfo MemberInfo { get => memberInfo; set => memberInfo = value; }
		//Beneficiary Information
		public PersonInfo BeneficiaryInfo { get => beneficiaryInfo; set => beneficiaryInfo = value; }

		//TODO
		//Options are Spouse and Partner (Registered Domestic Partner)
		//RelationShip we will use string for now, but will change it to Enum later when we know all options
		public string RelationShip { get; set; }

		//Duration of Integrated Service(IS) and Sick Leave
		public DurationYMDs IntegratedService { get => integratedService; set => integratedService = value; }
		public DurationYMDs ISSick { get => iSSick; set => iSSick = value; }

		//Duration of Non Integrated Service(IS) and Sick Leave (Non Integrated
		public DurationYMDs NonIntegratedService { get => nonIntegratedService; set => nonIntegratedService = value; }
		public DurationYMDs NonISSick { get => nonISSick; set => nonISSick = value; }

		//Employee Contriubtion (EE Contr)
		public double EEContrBasic { get; set; }
		public double EEContrCol { get; set; }

		//Final Compensation
		//public double FinalComp { get => finalComp; set => finalComp = value; }
		public double FinalComp { get; set; }

		public double InterestRate { get; set; }
		public double COLA { get; set; } //Cost of Living Allowance
		public double FeqToPay { get; set; } //Factor to convert frequency of payment
		public double RoundOption1Fac { get; set; }//TODO: rounding factor for option 1 - where do you get this from? right now we use it from config

		#endregion

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

		//Mortality Table
		//TODO
		//Values for this will be hardcoded temporarily and programmed later
		public int MemberMortalityTable { get => memberMortalityTable; set => memberMortalityTable = value; }
		public int MemberSetback { get => memberSetback; set => memberSetback = value; }
		public int BeneficiaryMortalityTable { get => beneficiaryMortalityTable; set => beneficiaryMortalityTable = value; }
		public int BeneficiarySetback { get => beneficiarySetback; set => beneficiarySetback = value; }

		//Member Age - Beneficiary Age - Calculating Life Table for 4 different Option
		public int XMinusY { get; set; }
		public int XMinusYPlus1 { get; set; }
		public int XPlus1MinusY { get; set; }
		public int XPlus1MinusYPlus1 { get; set; }

		public double IRCOLA { get; set; }

		////A Due Factor Computation
		public double XY { get; set; }
		public double XY1 { get; set; }
		public double X1Y { get; set; }
		public double X1Y1 { get; set; }
		//A Due Factor Computation for Age 1/4th
		public double XY14 { get; set; }
		public double X14Y14 { get; set; }//a due Joint Life (12)
		public double X1Y14 { get; set; }
		//MemFact = Member Factor
		public double X { get; set; }
		public double X14 { get; set; } //a due Retirement Age(12)
		public double X1 { get; set; }
		//BenFact = Beneficiary Factor
		public double Y { get; set; }
		public double Y14 { get; set; }//a due Beneficiary Age(12)
		public double Y1 { get; set; }

		////A Due Factor Computation Prime
		public double XYPrime { get; set; }
		public double XY1Prime { get; set; }
		public double X1YPrime { get; set; }
		public double X1Y1Prime { get; set; }
		//A Due Factor Computation for Age 1/4th Prime
		public double XY14Prime { get; set; }
		public double X14Y14Prime { get; set; }//a due Joint Life (12) Prime
		public double X1Y14Prime { get; set; }
		//MemFact = Member Factor Prime
		public double XPrime { get; set; }
		public double X14Prime { get; set; } //a due Retirement Age(12) Prime
		public double X1Prime { get; set; }
		//BenFact = Beneficiary Factor 
		public double YPrime { get; set; }
		public double Y14Prime { get; set; }//a due Beneficiary Age(12)Prime
		public double Y1Prime { get; set; }

		//MX
		public double MX { get; set; }
		public double MX14 { get; set; }
		public double MX1 { get; set; }
		//RX
		public double RX { get; set; }
		public double RX14 { get; set; }
		public double RX1 { get; set; }
		//DX
		public double DX { get; set; }
		public double DX14 { get; set; }
		public double DX1 { get; set; }

		//MXN
		public double MXn { get; set; }
		public double MX14n { get; set; }
		public double MX1n{ get; set; }
		//RX
		public double RXn { get; set; }
		public double RX14n { get; set; }
		public double RX1n { get; set; }


		public double Option1 { get; set; }
		public double Option2 { get; set; }
		public double Option3 { get; set; }
		public double Option4 { get; set; }



	}
}   
