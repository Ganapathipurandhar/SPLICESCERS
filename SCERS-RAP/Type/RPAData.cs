using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCERS_RAP.Type
{
	public class RPAData
	{		
		public PreLoad PreLoad {  get; set; }
		public RPAWork Work { get; set; }
		public Factor Factor { get; set; }
		public List<LifeTable> LifeTables { get; set; }
		public Calc Calc { get; set; }
		public Letter Letter { get; set; }
	}
}
