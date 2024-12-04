using SCERS_RAP.Type;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCERS_RAP.Services
{
	public class LifeTableService
	{
		private RPAData rd;
		private RPAWork work;
		private PreLoad pl;
		private Factor f;
		private List<LifeTable> lts;


		public LifeTableService(RPAData rd) 
		{
			this.rd = rd;
			this.work = this.rd.Work;
			this.pl = rd.PreLoad;			
			this.f = this.rd.Factor;
			this.rd.LifeTables = new List<LifeTable>();
			lts = this.rd.LifeTables;
		}

		public void CalculateLifeTable() 
		{
			computeQlvdXY();
			computeNXY();
			computeETXY();
			computeRxPrime();
			computePrime();
		}

		private void computeQlvdXY() 
		{
			for (int i = 0; i < 116; i++)
			{
				var lf = new LifeTable();
				#region Memeber Life Table Computation

				lf.Age = i;
				var MemAge = lf.Age + f.MemberSetback;
				if (MemAge <= 0)
				{
					lf.qx = 0;
					lf.lx = 1;
				}
				else
				{
					//TODO : Need condition for GAM vs GAF value
					lf.qx = DataServices.GAMqx.FirstOrDefault(g => g.Age == MemAge).GAM94qx;
					lf.lx = lts[i - 1].lx * (1 - lts[i - 1].qx);
				}

				lf.vx = 1 / Math.Pow((1 + pl.InterestRate), lf.Age);
				lf.Dx = lf.lx * lf.vx;
				#endregion

				#region Beneficiary Life Table Computation
				var BenAge = lf.Age + f.BeneficiarySetback;
				if (BenAge <= 0)
				{
					lf.qy = 0;
					lf.ly = 1;
				}
				else
				{
					//TODO : Need condition for GAM vs GAF value
					lf.qy = DataServices.GAMqx.FirstOrDefault(g => g.Age == BenAge).GAM94qx;
					lf.ly = lts[i - 1].ly * (1 - lts[i - 1].qy);
				}

				lf.vy = 1 / Math.Pow((1 + pl.InterestRate), lf.Age);
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
					var ageF = (lts[0].Age > (lts[i - 1].Age - f.XMinusY)) ?
									lts[0].Age : (lts[i - 1].Age - f.XMinusY);

					lf.lxy = lts[i - 1].lxy * (1 - lts[i - 1].qx) *
						(1 - lts[ageF].qy);
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
					var ageF = (lts[0].Age > (lts[i - 1].Age - f.XMinusYPlus1)) ?
									lts[0].Age : (lts[i - 1].Age - f.XMinusYPlus1);

					lf.lxy1 = lts[i - 1].lxy1 * (1 - lts[i - 1].qx) *
						(1 - lts[ageF].qy);
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
					var ageF = (lts[0].Age > (lts[i - 1].Age - f.XPlus1MinusY)) ?
									lts[0].Age : (lts[i - 1].Age - f.XPlus1MinusY);

					lf.lx1y = lts[i - 1].lx1y * (1 - lts[i - 1].qx) *
						(1 - lts[ageF].qy);
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
					var ageF = (lts[0].Age > (lts[i - 1].Age - f.XPlus1MinusYPlus1)) ?
									lts[0].Age : (lts[i - 1].Age - f.XPlus1MinusYPlus1);

					lf.lx1y1 = lts[i - 1].lx1y1 * (1 - lts[i - 1].qx) *
						(1 - lts[ageF].qy);
					lf.Dx1y1 = lf.lx1y1 * lf.vx;
				}
				#endregion

				lts.Add(lf);
			}

		}

		private void computeNXY() 
		{
			// Update Nx, Ny[]... Value
			for (int j = 115; j > -1; j--)
			{
				if (j == 115)
				{
					lts[j].Nx = lts[j].Dx;
					lts[j].Ny = lts[j].Dy;
					lts[j].Nxy = lts[j].Dxy;
					lts[j].Nxy1 = lts[j].Dxy1;
					lts[j].Nx1y = lts[j].Dx1y;
					lts[j].Nx1y1 = lts[j].Dx1y1;

				}
				else
				{
					lts[j].Nx = lts[j].Dx + lts[j + 1].Nx;
					lts[j].Ny = lts[j].Dy + lts[j + 1].Ny;
					lts[j].Nxy = lts[j].Dxy + lts[j + 1].Nxy;
					lts[j].Nxy1 = lts[j].Dxy1 + lts[j + 1].Nxy1;
					lts[j].Nx1y = lts[j].Dx1y + lts[j + 1].Nx1y;
					lts[j].Nx1y1 = lts[j].Dx1y1 + lts[j + 1].Nx1y1;
				}
			}
		}

		private void computeETXY() 
		{
			//TODO - 0.5 why are we using this Can't we use factor to convert frequency to pay
			//ETx and ETy
			#region ETx and ETy
			for (int i = 0; i < 116; i++)
			{
				if (i != 115)
				{
					lts[i].ETx = lts.Skip(i + 1).Sum(x => x.lx) / lts[i].lx + 0.5;
					lts[i].ETy = lts.Skip(i + 1).Sum(x => x.ly) / lts[i].ly + 0.5;
					lts[i].Mx = lts.Skip(i).Sum(x => x.qx * x.Dx) / (1 + pl.InterestRate);
				}
				else
				{
					lts[i].ETx = lts.Skip(i).Sum(x => x.lx) / lts[i].lx + 0.5;
					lts[i].ETy = lts.Skip(i).Sum(x => x.ly) / lts[i].ly + 0.5;
					lts[i].Mx = lts.Skip(i).Sum(x => x.qx * x.Dx) / (1 + pl.InterestRate);
				}
			}
			#endregion
		}

		private void computeRxPrime() 
		{
			#region Rx and Prime Computation
			for (int i = 0; i < 116; i++)
			{
				if (i != 115)
				{
					lts[i].Rx = lts.Skip(i).Sum(x => x.Mx);
				}
				else
				{
					lts[i].Rx = lts.Skip(i).Sum(x => x.Mx);
				}
				lts[i].vxPrime = Math.Pow(1 / (1 + f.IRCOLA), lts[i].Age);
				lts[i].DxPrime = lts[i].vxPrime * lts[i].lx;
				lts[i].vyPrime = Math.Pow(1 / (1 + f.IRCOLA), lts[i].Age);
				lts[i].DyPrime = lts[i].vyPrime * lts[i].ly;
				lts[i].DxyPrime = lts[i].lxy * lts[i].vxPrime;
				lts[i].Dxy1Prime = lts[i].lxy1 * lts[i].vxPrime;
				lts[i].Dx1yPrime = lts[i].lx1y * lts[i].vxPrime;
				lts[i].Dx1y1Prime = lts[i].lx1y1 * lts[i].vxPrime;
			}
			#endregion
		}

		private void computePrime() 
		{
			#region Prime Table Computation
			for (int j = 115; j > -1; j--)
			{
				if (j != 115)
				{
					lts[j].NxPrime = lts[j].DxPrime + lts[j + 1].NxPrime;
					lts[j].NyPrime = lts[j].DyPrime + lts[j + 1].NyPrime;
					lts[j].NxyPrime = lts[j].DxyPrime + lts[j + 1].NxyPrime;
					lts[j].Nxy1Prime = lts[j].Dxy1Prime + lts[j + 1].Nxy1Prime;
					lts[j].Nx1yPrime = lts[j].Dx1yPrime + lts[j + 1].Nx1yPrime;
					lts[j].Nx1y1Prime = lts[j].Dx1y1Prime + lts[j + 1].Nx1y1Prime;
				}
				else
				{
					lts[j].NxPrime = lts[j].DxPrime;
					lts[j].NyPrime = lts[j].DyPrime;
					lts[j].NxyPrime = lts[j].DxyPrime;
					lts[j].Nxy1Prime = lts[j].Dxy1Prime;
					lts[j].Nx1yPrime = lts[j].Dx1yPrime;
					lts[j].Nx1y1Prime = lts[j].Dx1y1Prime;
				}
			}
			#endregion
		}
	}
}
