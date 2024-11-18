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

		public string Name { get; set; }
		public DateTime DOB { get; set; }
		public double Age { get; set; }
		public double Age1by4 { get; set; }
	}
}
