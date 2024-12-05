using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace SCERS_RAP.Type
{
	public class Letter
	{
		public string MemberName{ get; set; }
		public string Membership{ get; set; }
		public string RetirementDate { get; set; }
		public string MemberDOB { get; set; }
		public string Integrated {  get; set; }
		public string NonIntegrated { get; set; }
		public string AvgMonthlyComp {  get; set; }
		
		public string BeneficiaryName { get; set; }
		public string RelationShip {  get; set; }
		public string BeneficiaryDOB { get; set; }
		
		public string UnmodifiedCSA { get; set; }
		public string UnmodifiedCSP { get; set; }
		public string UnmodifiedTP { get; set; }
		public string UnmodifiedCTS { get; set; }

		public string Option1CSA { get; set; }
		public string Option1CSP { get; set; }
		public string Option1TP { get; set; }
		public string Option1CTS { get; set; }

		public string Option2CSA { get; set; }
		public string Option2CSP { get; set; }
		public string Option2TP { get; set; }
		public string Option2CTS { get; set; }

		public string Option3CSA { get; set; }
		public string Option3CSP { get; set; }
		public string Option3TP { get; set; }
		public string Option3CTS { get; set; }

		public string BasicCSA { get; set; }
		public string BasicCSP { get; set; }
		public string BasicTP { get; set; }
		public string BasicCTS { get; set; }

		public string COLCSA { get; set; }
		public string COLCSP { get; set; }
		public string COLTP { get; set; }
		public string COLCTS { get; set; }

	}
}
