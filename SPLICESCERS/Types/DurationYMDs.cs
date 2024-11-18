using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace SPLICESCERS.Types
{
	/// <summary>
	/// DUration of Service, sick leave etc., 
	/// that need sreporting on Yeras, Months and days
	/// Days it defined as double because it had decimal in the excel template
	/// </summary>
	public class DurationYMDs
	{
		public int Years { get; set; }
		public int Months { get; set; }
		public double Days { get; set; }
	}
}
