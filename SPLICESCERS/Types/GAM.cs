using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SPLICESCERS.Types
{
	/// <summary>
	/// 1994 Group Annuity Mortality Table (also known as the UP-94 Mortality Table) for males
	/// 1981 General Disability Mortality Table
	/// 1994 Group Annuity Mortality Table(also known as the UP-94 Mortality Table)
	/// 1981 Safety Disability Mortality Table
	/// </summary>
	public class GAM
	{
		private int age;
		private double gAM94qx;
		private double gAF94qx;
		private double disGenqx;
		private double disSafeqx;

		public int Age { get => age; set => age = value; }
		public double GAM94qx { get => gAM94qx; set => gAM94qx = value; }
		public double GAF94qx { get => gAF94qx; set => gAF94qx = value; }
		public double DisGenqx { get => disGenqx; set => disGenqx = value; }
		public double DisSafeqx { get => disSafeqx; set => disSafeqx = value; }
	}
}
