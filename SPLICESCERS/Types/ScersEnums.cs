using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPLICESCERS.Types
{
	public enum RetirementType
	{
		SR, //Social Security Retirement ?
		NSCD,//Nonservice-Connected Disability Retirement
		SCD //Service Computation Date (SCD) Retirement
	}

	public enum MembershipType
	{
		General,
		Safety
	}

	public enum Tiers : ushort
	{
		One = 1,
		Two = 2,
		Three = 3
	}

	public enum YesNo 
	{
		Yes,
		No
	}
}
