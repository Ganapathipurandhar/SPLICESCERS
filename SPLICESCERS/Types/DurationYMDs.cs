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
		private int years = 0;
		private double months = 0;
		private double days = 0;

		public int Years { get => years; set => years = value; }
		public double Months { get => months; set => months = value; }
		public double Days { get => days; set => days = value; }
	}
}
