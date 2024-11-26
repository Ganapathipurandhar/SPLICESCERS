using SPLICESCERS.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SPLICESCERS.Services
{
	public class FactorService
	{
		WorkService workService;
		WorkData workData;
		private List<LifeTable> lifeTables;

		public List<LifeTable> LifeTables { get => lifeTables; set => lifeTables = value; }

		public FactorService(WorkService ws) 
		{
			this.workService = ws;
			workData = ws.WD;
		}

		public void CalcLifeTable() 
		{
			lifeTables = new List<LifeTable>();
			for (int i = 0; i < 115; i++)
			{
				var lf = new LifeTable();
				lf.Age = i;
				var MemAge = lf.Age + workData.MemberSetback;
				if (MemAge <= 0)
				{
					lf.Memberqx = 0;
					lf.Memberlx = 1;
				}
				else 
				{
					//TODO : Need condition for GAM vs GAF value
					lf.Memberqx = DataServices.GAMqx.FirstOrDefault(g => g.Age == MemAge).GAM94qx;
					lf.Memberlx = LifeTables[i - 1].Memberlx * (1 - LifeTables[i - 1].Memberqx);
				}

				lf.Membervx = 1 / Math.Pow((1 + workData.InterestRate), lf.Age);
				lf.MemberDx = lf.Memberlx * lf.Membervx;

				LifeTables.Add(lf);
			}


			
			//Update Nx Value
			for (int j=114 ; j > -1; j--) 
			{
				if (j == 114)
				{
					LifeTables[j].MemberNx = LifeTables[j].MemberDx;
				}
				else 
				{
					LifeTables[j].MemberNx = LifeTables[j].MemberDx + LifeTables[j+1].MemberNx;
				}
			}

			FileServices.ListToCsv(LifeTables);
		}

		public void print(test test) 
		{
			for (int i = 0; i < 115; i++)
			{
				test.print(LifeTables[i].Age.ToString() + " : " +
					LifeTables[i].Memberqx.ToString() + " : " +
					LifeTables[i].Memberlx.ToString() + " : " +
					LifeTables[i].Membervx.ToString() + " : " +
					LifeTables[i].MemberDx.ToString() + " : " +
					LifeTables[i].MemberNx.ToString() + " : "
					);
			}
		}


	}
}
