using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPLICESCERS.Types
{
	/// <summary>
	/// Name of the Member or Benificiary 
	/// DOB of the Member or Benificiary
	/// Calculate the Age of the Benificiary based on retirement date
	/// Calculate the Age (to next lower 1/4):
	/// </summary>
	public class PersonInfo
	{
		private string name;
		private DateTime dOB;
		private double age;
		private double age1by4;

		public string Name { get => name; set => name = value; }
		public DateTime DOB { get => dOB; set => dOB = value; }
		public double Age { get => age; set => age = value; }
		public double Age1by4 { get => age1by4; set => age1by4 = value; }
	}
}
