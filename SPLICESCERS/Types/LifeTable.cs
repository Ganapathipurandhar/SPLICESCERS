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
		public double Memberqx { get; set; }
		public double Memberlx { get; set; }
		public double Membervx { get; set; }
		public double MemberDx { get; set; }
		public double MemberNx{ get; set; }
	}
}
