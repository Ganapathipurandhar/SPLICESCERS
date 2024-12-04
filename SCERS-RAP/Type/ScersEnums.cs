using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SCERS_RAP.Type
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum RetirementType
	{
		SR, //Social Security Retirement ?
		NSCD,//Nonservice-Connected Disability Retirement
		SCD //Service Computation Date (SCD) Retirement
	}

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum MembershipType
	{
		General,
		Safety
	}

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Tiers : ushort
	{
		One = 1,
		Two = 2,
		Three = 3
	}

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum YesNo 
	{
		Yes,
		No
	}


	[JsonConverter(typeof(JsonStringEnumConverter))]
	public enum RelationShip
	{
		Spouse,
		Partner
	}
}
