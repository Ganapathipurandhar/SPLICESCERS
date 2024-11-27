using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace SPLICESCERS.Types
{
	/// <summary>
	/// https://www.ssa.gov/oact/HistEst/CohLifeTables/LifeTableDefinitions.pdf
	/// qx: Probability of dying between ages x and x+1 
	/// lx: Number of individuals surviving to age x 
	/// dx: Number of individuals dying between ages x and x+1
	/// vx
	/// 
	/// </summary>
	public class LifeTable
	{
        public  int Age{ get; set; }

		//Member factors
		//"qx" represents the probability of death between age x and x+1,
		//"dx" is the number of people dying between age x and x+1, and
		//"lx" is the number of people surviving to age x.
		//"vx" represents the "discount factor at age x : vx = 1 / (1 + i)^x
		//"Nx" represents the number of individuals alive at the start of age interval "x" :  Nx = lx * (x+1) - lx+1
		public double qx { get; set; }
		public double lx { get; set; }
		public double vx { get; set; }
		public double Dx { get; set; }
		public double Nx{ get; set; }
		//Beneficiary factors
		public double qy { get; set; }
		public double ly { get; set; }
		public double vy { get; set; }
		public double Dy { get; set; }
		public double Ny { get; set; }
		//X-Y Factors
		public double lxy { get; set; }
		public double Dxy { get; set; }
		public double Nxy { get; set; }
		//X-(Y+1) Factors
		public double lxy1 { get; set; }
		public double Dxy1 { get; set; }
		public double Nxy1 { get; set; }
		//(X+1)-(Y) Factors
		public double lx1y { get; set; }
		public double Dx1y { get; set; }
		public double Nx1y { get; set; }
		//(X+1)-(Y+1) Factors
		public double lx1y1 { get; set; }
		public double Dx1y1 { get; set; }
		public double Nx1y1 { get; set; }
		//E[Tx] and E[Ty] which represents the expected remaining lifetime at age x, is calculated as E[Tx] = Tx / lx
		public double ETx { get; set; }
		public double ETy { get; set; }

		//"Mx" represents the "central death rate" and is calculated as "dx / Lx",
		//"Rx" represents the proportion of the year lived between age x and x+1, usually assumed to be 0.5,
		//meaning individuals are considered to die halfway through the age interval
		public double Mx { get; set; }
		public double Rx { get; set; }

		public double vxPrime { get; set; }
		public double DxPrime { get; set; }
		public double NxPrime { get; set; }
		public double vyPrime { get; set; }
		public double DyPrime { get; set; }
		public double NyPrime { get; set; }

	}
}
