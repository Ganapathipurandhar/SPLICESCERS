using System;
using System.Collections.Generic;
using System.Linq;
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

	}
}
