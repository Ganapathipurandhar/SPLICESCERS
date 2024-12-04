using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SCERS_RAP.Type
{
	public class PreLoad
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
		public RelationShip RelationShip { get; set; }

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
		public double ContinueOption4 { get; set; }
		#endregion
	}
}
