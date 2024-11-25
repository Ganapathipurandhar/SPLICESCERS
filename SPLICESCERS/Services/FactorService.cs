using SPLICESCERS.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPLICESCERS.Services
{
	public class FactorService
	{
		WorkService workService;
		private List<LifeTable> lifeTable;

		public List<LifeTable> LifeTable { get => lifeTable; set => lifeTable = value; }

		public FactorService(WorkService ws) 
		{
			this.workService = ws;
		}

		public void CalcLifeTable() 
		{
			for (int i = 0; i < 115; i++)
			{
				if (i = 0)
				{

				}
			}

		}


	}
}
