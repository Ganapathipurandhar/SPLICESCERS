using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCERS_RAP.Type
{
	public class Factor
	{
		//TODO : This defaults will need to be calculated, temp setup to run factors
		private int memberMortalityTable = 1;
		private int memberSetback = -3;
		private int beneficiaryMortalityTable = 1;
		private int beneficiarySetback = -3;

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
		public double MX1n { get; set; }
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
