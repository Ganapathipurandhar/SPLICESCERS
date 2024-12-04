using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCERS_RAP.Type
{
	public class RPAData
	{
		public RPAData() 
		{
			PreLoad = new PreLoad();
			Work = new RPAWork();
			Factor = new Factor();
		}
		public PreLoad PreLoad {  get; set; }
		public RPAWork Work { get; set; }
		public Factor Factor { get; set; }
		public List<LifeTable> LifeTables { get; set; }
	}
}
