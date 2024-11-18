using SPLICESCERS.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPLICESCERS.Work
{
	public  class WorkData
	{
        public RetirementType TypeOfRetirement { get; set; }
        public DateTime DateOfRetirement { get; set; }
        public MembershipType Membership{ get; set; }
        public Tiers Tier{ get; set; }
        public YesNo MoneyPurchase{ get; set; }
        public PersonInfo MemberInfo { get; set; }
		public PersonInfo BeneficiaryInfo { get; set; }

	}
}   
