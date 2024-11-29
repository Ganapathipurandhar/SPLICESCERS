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
			for (int i = 0; i < 116; i++)
			{
				var lf = new LifeTable();
				#region Memeber Life Table Computation

				lf.Age = i;
				var MemAge = lf.Age + workData.MemberSetback;
				if (MemAge <= 0)
				{
					lf.qx = 0;
					lf.lx = 1;
				}
				else
				{
					//TODO : Need condition for GAM vs GAF value
					lf.qx = DataServices.GAMqx.FirstOrDefault(g => g.Age == MemAge).GAM94qx;
					lf.lx = LifeTables[i - 1].lx * (1 - LifeTables[i - 1].qx);
				}

				lf.vx = 1 / Math.Pow((1 + workData.InterestRate), lf.Age);
				lf.Dx = lf.lx * lf.vx; 
				#endregion

				#region Beneficiary Life Table Computation
				var BenAge = lf.Age + workData.BeneficiarySetback;
				if (BenAge <= 0)
				{
					lf.qy = 0;
					lf.ly = 1;
				}
				else
				{
					//TODO : Need condition for GAM vs GAF value
					lf.qy = DataServices.GAMqx.FirstOrDefault(g => g.Age == BenAge).GAM94qx;
					lf.ly = LifeTables[i - 1].ly * (1 - LifeTables[i - 1].qy);
				}

				lf.vy = 1 / Math.Pow((1 + workData.InterestRate), lf.Age);
				lf.Dy = lf.ly * lf.vy;
				#endregion

				
				#region X-Y (Memeber Age - Beneficiary Age)
				if (i == 0)
				{
					lf.lxy = 1;
					lf.Dxy = lf.lxy * lf.vx;
				}
				else
				{
					var ageF = (LifeTables[0].Age > (LifeTables[i - 1].Age - workData.XMinusY)) ?
									LifeTables[0].Age : (LifeTables[i - 1].Age - workData.XMinusY);

					lf.lxy = LifeTables[i - 1].lxy * (1 - LifeTables[i - 1].qx) *
						(1 - LifeTables[ageF].qy);
					lf.Dxy = lf.lxy * lf.vx;
				}
				#endregion

				//X-(Y+1) (Memeber Age - Beneficiary Age + 1)
				#region X-(Y+1) (Memeber Age - Beneficiary Age + 1)
				if (i == 0)
				{
					lf.lxy1 = 1;
					lf.Dxy1 = lf.lxy1 * lf.vx;
				}
				else
				{
					var ageF = (LifeTables[0].Age > (LifeTables[i - 1].Age - workData.XMinusYPlus1)) ?
									LifeTables[0].Age : (LifeTables[i - 1].Age - workData.XMinusYPlus1);

					lf.lxy1 = LifeTables[i - 1].lxy1 * (1 - LifeTables[i - 1].qx) *
						(1 - LifeTables[ageF].qy);
					lf.Dxy1 = lf.lxy1 * lf.vx;
				}
				#endregion

				//(X+1)-Y (Memeber Age + 1 - Beneficiary Age)
				#region (X+1)-Y (Memeber Age + 1 - Beneficiary Age)
				if (i == 0)
				{
					lf.lx1y = 1;
					lf.Dx1y = lf.lx1y * lf.vx;
				}
				else
				{
					var ageF = (LifeTables[0].Age > (LifeTables[i - 1].Age - workData.XPlus1MinusY)) ?
									LifeTables[0].Age : (LifeTables[i - 1].Age - workData.XPlus1MinusY);

					lf.lx1y = LifeTables[i - 1].lx1y * (1 - LifeTables[i - 1].qx) *
						(1 - LifeTables[ageF].qy);
					lf.Dx1y = lf.lx1y * lf.vx;
				}
				#endregion

				//(X+1)-(Y+1) (Memeber Age + 1 - Beneficiary Age + )
				#region (X+1)-(Y+1) (Memeber Age + 1 - Beneficiary Age + )
				if (i == 0)
				{
					lf.lx1y1 = 1;
					lf.Dx1y1 = lf.lx1y1 * lf.vx;
				}
				else
				{
					var ageF = (LifeTables[0].Age > (LifeTables[i - 1].Age - workData.XPlus1MinusYPlus1)) ?
									LifeTables[0].Age : (LifeTables[i - 1].Age - workData.XPlus1MinusYPlus1);

					lf.lx1y1 = LifeTables[i - 1].lx1y1 * (1 - LifeTables[i - 1].qx) *
						(1 - LifeTables[ageF].qy);
					lf.Dx1y1 = lf.lx1y1 * lf.vx;
				}
				#endregion

				LifeTables.Add(lf);
			}
			
			//Update Nx Value for Beneficiary
			for (int j=115 ; j > -1; j--) 
			{
				if (j == 115)
				{
					LifeTables[j].Nx = LifeTables[j].Dx;
					LifeTables[j].Ny = LifeTables[j].Dy;
					LifeTables[j].Nxy = LifeTables[j].Dxy;
					LifeTables[j].Nxy1 = LifeTables[j].Dxy1;
					LifeTables[j].Nx1y = LifeTables[j].Dx1y;
					LifeTables[j].Nx1y1 = LifeTables[j].Dx1y1;

				}
				else 
				{
					LifeTables[j].Nx = LifeTables[j].Dx + LifeTables[j + 1].Nx;
					LifeTables[j].Ny = LifeTables[j].Dy + LifeTables[j+1].Ny;
					LifeTables[j].Nxy = LifeTables[j].Dxy + LifeTables[j + 1].Nxy;
					LifeTables[j].Nxy1 = LifeTables[j].Dxy1 + LifeTables[j + 1].Nxy1;
					LifeTables[j].Nx1y = LifeTables[j].Dx1y + LifeTables[j + 1].Nx1y;
					LifeTables[j].Nx1y1 = LifeTables[j].Dx1y1 + LifeTables[j + 1].Nx1y1;
				}
			}

			//TODO - 0.5 is
			//ETx and ETy
			for (int i = 0; i < 116; i++)
			{
				if (i != 115)
				{
					LifeTables[i].ETx = LifeTables.Skip(i + 1).Sum(x => x.lx) / LifeTables[i].lx + 0.5;
					LifeTables[i].ETy = LifeTables.Skip(i + 1).Sum(x => x.ly) / LifeTables[i].ly + 0.5;
					LifeTables[i].Mx = LifeTables.Skip(i).Sum(x => x.qx * x.Dx) / (1 + workData.InterestRate);					
				}
				else
				{
					LifeTables[i].ETx = LifeTables.Skip(i).Sum(x => x.lx) / LifeTables[i].lx + 0.5;
					LifeTables[i].ETy = LifeTables.Skip(i).Sum(x => x.ly) / LifeTables[i].ly + 0.5;
					LifeTables[i].Mx = LifeTables.Skip(i).Sum(x => x.qx * x.Dx) / (1 + workData.InterestRate);					
				}
			}

			//Rx and Prime Computation
			for (int i = 0; i < 116; i++)
			{
				if (i != 115)
				{					
					LifeTables[i].Rx = LifeTables.Skip(i).Sum(x => x.Mx);					
				}
				else
				{					
					LifeTables[i].Rx = LifeTables.Skip(i).Sum(x => x.Mx);
				}
				LifeTables[i].vxPrime = Math.Pow(1/(1 + workData.IRCOLA), LifeTables[i].Age);
				LifeTables[i].DxPrime = LifeTables[i].vxPrime * LifeTables[i].lx;
				LifeTables[i].vyPrime = Math.Pow(1 / (1 + workData.IRCOLA), LifeTables[i].Age);
				LifeTables[i].DyPrime = LifeTables[i].vyPrime * LifeTables[i].ly;
				LifeTables[i].DxyPrime = LifeTables[i].lxy * LifeTables[i].vxPrime;
				LifeTables[i].Dxy1Prime = LifeTables[i].lxy1 * LifeTables[i].vxPrime;
				LifeTables[i].Dx1yPrime = LifeTables[i].lx1y * LifeTables[i].vxPrime;
				LifeTables[i].Dx1y1Prime = LifeTables[i].lx1y1 * LifeTables[i].vxPrime;

			}

			for (int j = 115; j > -1; j--)
			{
				if (j != 115)
				{					
					LifeTables[j].NxPrime = LifeTables[j].DxPrime + LifeTables[j + 1].NxPrime;
					LifeTables[j].NyPrime = LifeTables[j].DyPrime + LifeTables[j + 1].NyPrime;
					LifeTables[j].NxyPrime = LifeTables[j].DxyPrime + LifeTables[j + 1].NxyPrime;
					LifeTables[j].Nxy1Prime = LifeTables[j].Dxy1Prime + LifeTables[j + 1].Nxy1Prime;
					LifeTables[j].Nx1yPrime = LifeTables[j].Dx1yPrime + LifeTables[j + 1].Nx1yPrime;
					LifeTables[j].Nx1y1Prime = LifeTables[j].Dx1y1Prime + LifeTables[j + 1].Nx1y1Prime;
				}
				else
				{
					LifeTables[j].NxPrime = LifeTables[j].DxPrime;
					LifeTables[j].NyPrime = LifeTables[j].DyPrime;
					LifeTables[j].NxyPrime = LifeTables[j].DxyPrime;
					LifeTables[j].Nxy1Prime = LifeTables[j].Dxy1Prime;
					LifeTables[j].Nx1yPrime = LifeTables[j].Dx1yPrime;
					LifeTables[j].Nx1y1Prime = LifeTables[j].Dx1y1Prime;
				}
			}
			FileServices.ListToCsv( LifeTables );

			//Factor 
			var _age = Math.Truncate(workData.MemberInfo.Age);
			var tnxy = LifeTables.FirstOrDefault(x => x.Age == _age).Nxy;
			var tdxy = LifeTables.FirstOrDefault(x => x.Age == _age).Dxy;
			workData.ADueXY_12 = (LifeTables.FirstOrDefault(x => x.Age == _age).Nxy /
				 LifeTables.FirstOrDefault(x => x.Age == _age).Dxy);
		}

		public void print(test test) 
		{
			for (int i = 0; i < 116; i++)
			{
				test.print(LifeTables[i].Age.ToString() + " : " +
					//LifeTables[i].qx.ToString() + " : " +
					//LifeTables[i].lx.ToString() + " : " +
					//LifeTables[i].vx.ToString() + " : " +
					//LifeTables[i].Dx.ToString() + " : " +
					//LifeTables[i].Nx.ToString() + " : " +
					//LifeTables[i].qy.ToString() + " : " +
					//LifeTables[i].ly.ToString() + " : " +
					//LifeTables[i].vy.ToString() + " : " +
					//LifeTables[i].Dy.ToString() + " : " +
					//LifeTables[i].Ny.ToString() + " : " +
					//LifeTables[i].lx1y1.ToString() + " : " +
					//LifeTables[i].vyPrime.ToString() + " : " +
					//LifeTables[i].Dx1y1Prime.ToString() + " : " +
					//LifeTables[i].Nx1y1Prime.ToString()
					""
					);
			}
		}


	}
}
