using SPLICESCERS.Types;
using System;
using System.Collections.Generic;
using System.IO;
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
			
			//Update Nx, Ny[]... Value 
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

			//TODO - 0.5 why are we using this Can't we use factor to convert frequency to pay
			//ETx and ETy
			#region ETx and ETy
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
			#endregion

			#region Rx and Prime Computation
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
				LifeTables[i].vxPrime = Math.Pow(1 / (1 + workData.IRCOLA), LifeTables[i].Age);
				LifeTables[i].DxPrime = LifeTables[i].vxPrime * LifeTables[i].lx;
				LifeTables[i].vyPrime = Math.Pow(1 / (1 + workData.IRCOLA), LifeTables[i].Age);
				LifeTables[i].DyPrime = LifeTables[i].vyPrime * LifeTables[i].ly;
				LifeTables[i].DxyPrime = LifeTables[i].lxy * LifeTables[i].vxPrime;
				LifeTables[i].Dxy1Prime = LifeTables[i].lxy1 * LifeTables[i].vxPrime;
				LifeTables[i].Dx1yPrime = LifeTables[i].lx1y * LifeTables[i].vxPrime;
				LifeTables[i].Dx1y1Prime = LifeTables[i].lx1y1 * LifeTables[i].vxPrime;
			} 
			#endregion

			#region Prime Table Computation
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
			#endregion

			FileServices.ListToCsv( LifeTables );

			//A Due Factor Computaton 
			var _age = Math.Truncate(workData.MemberInfo.Age);
			var _bage = Math.Truncate(workData.BeneficiaryInfo.Age);
			
			workData.XY = (LifeTables.FirstOrDefault(x => x.Age == _age).Nxy /
				 LifeTables.FirstOrDefault(x => x.Age == _age).Dxy) - workData.FeqToPay;
			workData.XY1 = (LifeTables.FirstOrDefault(x => x.Age == _age).Nxy1 /
				 LifeTables.FirstOrDefault(x => x.Age == _age).Dxy1) - workData.FeqToPay;
			workData.X1Y = (LifeTables.FirstOrDefault(x => x.Age == _age +1 ).Nx1y /
				 LifeTables.FirstOrDefault(x => x.Age == _age+1).Dx1y) - workData.FeqToPay;
			workData.X1Y1 = (LifeTables.FirstOrDefault(x => x.Age == _age + 1).Nx1y1 /
				 LifeTables.FirstOrDefault(x => x.Age == _age + 1).Dx1y1) - workData.FeqToPay;

			workData.XY14 = (workData.XY1 - workData.XY) * 
								(workData.BeneficiaryInfo.Age1by4 - _bage) + workData.XY;
			workData.X1Y14 = (workData.X1Y1 - workData.X1Y) *
								(workData.BeneficiaryInfo.Age1by4 - _bage) + workData.X1Y;
			//*****a due Joint Life(12)*****//
			workData.X14Y14 = (workData.X1Y14 - workData.XY14) *
								(workData.MemberInfo.Age1by4 - _age) + workData.XY14;
			//Member Factor
			workData.X = (LifeTables.FirstOrDefault(x => x.Age == _age).Nx /
				 LifeTables.FirstOrDefault(x => x.Age == _age).Dx) - workData.FeqToPay;
			workData.X1 = (LifeTables.FirstOrDefault(x => x.Age == _age+1).Nx /
				 LifeTables.FirstOrDefault(x => x.Age == _age+1).Dx) - workData.FeqToPay;
			//*****a due Retirement Age(12)*****//
			workData.X14 = workData.X + (workData.X1 - workData.X) 
									* (workData.MemberInfo.Age1by4 - _age);

			//Beneficiary Factor
			workData.Y = (LifeTables.FirstOrDefault(x => x.Age == _bage).Ny /
				 LifeTables.FirstOrDefault(x => x.Age == _bage).Dy) - workData.FeqToPay;
			workData.Y1 = (LifeTables.FirstOrDefault(x => x.Age == _bage + 1).Ny /
				 LifeTables.FirstOrDefault(x => x.Age == _bage + 1).Dy) - workData.FeqToPay;
			//*****a due Beneficiary Age(12)*****//
			workData.Y14 = workData.Y + (workData.Y1 - workData.Y) 
				* (workData.BeneficiaryInfo.Age1by4 - _bage);


			//Prime Computation
			workData.XYPrime = (LifeTables.FirstOrDefault(x => x.Age == _age).NxyPrime /
				 LifeTables.FirstOrDefault(x => x.Age == _age).DxyPrime) - workData.FeqToPay;
			workData.XY1Prime = (LifeTables.FirstOrDefault(x => x.Age == _age).Nxy1Prime /
				 LifeTables.FirstOrDefault(x => x.Age == _age).Dxy1Prime) - workData.FeqToPay;
			workData.X1YPrime = (LifeTables.FirstOrDefault(x => x.Age == _age + 1).Nx1yPrime /
				 LifeTables.FirstOrDefault(x => x.Age == _age + 1).Dx1yPrime) - workData.FeqToPay;
			workData.X1Y1Prime = (LifeTables.FirstOrDefault(x => x.Age == _age + 1).Nx1y1Prime /
				 LifeTables.FirstOrDefault(x => x.Age == _age + 1).Dx1y1Prime) - workData.FeqToPay;

			workData.XY14Prime = (workData.XY1Prime - workData.XYPrime) *
								(workData.BeneficiaryInfo.Age1by4 - _bage) + workData.XYPrime;
			workData.X1Y14Prime = (workData.X1Y1Prime - workData.X1YPrime) *
								(workData.BeneficiaryInfo.Age1by4 - _bage) + workData.X1YPrime;
			//*****a due Joint Life(12)*****//
			workData.X14Y14Prime = (workData.X1Y14Prime - workData.XY14Prime) *
								(workData.MemberInfo.Age1by4 - _age) + workData.XY14Prime;
			//Member Factor
			workData.XPrime = (LifeTables.FirstOrDefault(x => x.Age == _age).NxPrime /
				 LifeTables.FirstOrDefault(x => x.Age == _age).DxPrime) - workData.FeqToPay;
			workData.X1Prime = (LifeTables.FirstOrDefault(x => x.Age == _age + 1).NxPrime /
				 LifeTables.FirstOrDefault(x => x.Age == _age + 1).DxPrime) - workData.FeqToPay;
			//*****a due Retirement Age(12)*****//
			workData.X14Prime = workData.XPrime + (workData.X1 - workData.XPrime)
									* (workData.MemberInfo.Age1by4 - _age);

			//Beneficiary Factor
			workData.YPrime = (LifeTables.FirstOrDefault(x => x.Age == _bage).NyPrime /
				 LifeTables.FirstOrDefault(x => x.Age == _bage).DyPrime) - workData.FeqToPay;
			workData.Y1Prime = (LifeTables.FirstOrDefault(x => x.Age == _bage + 1).NyPrime /
				 LifeTables.FirstOrDefault(x => x.Age == _bage + 1).DyPrime) - workData.FeqToPay;
			//*****a due Beneficiary Age(12)*****//
			workData.Y14Prime = workData.YPrime + (workData.Y1Prime - workData.YPrime)
				* (workData.BeneficiaryInfo.Age1by4 - _bage);

			//Mx
			workData.MX = (LifeTables.FirstOrDefault(x => x.Age == _age).Mx * workData.InterestRate)
						  /(Math.Log(1+workData.InterestRate));
			workData.MX1 = (LifeTables.FirstOrDefault(x => x.Age == _age+1).Mx * workData.InterestRate)
						  / (Math.Log(1 + workData.InterestRate));
			workData.MX14 = (workData.MemberInfo.Age1by4 - _age) * (workData.MX1 - workData.MX) + workData.MX;

			//Rx
			workData.RX = (LifeTables.FirstOrDefault(x => x.Age == _age).Rx * workData.InterestRate)
						  / (Math.Log(1 + workData.InterestRate));
			workData.RX1 = (LifeTables.FirstOrDefault(x => x.Age == _age + 1).Rx * workData.InterestRate)
						  / (Math.Log(1 + workData.InterestRate));
			workData.RX14 = (workData.MemberInfo.Age1by4 - _age) * (workData.RX1 - workData.RX) + workData.RX;

			//Dx
			workData.DX = LifeTables.FirstOrDefault(x => x.Age == _age).Dx;						 
			workData.DX1 = LifeTables.FirstOrDefault(x => x.Age == _age + 1).Dx ;
			workData.DX14 = (workData.MemberInfo.Age1by4 - _age) * (workData.DX1 - workData.DX) + workData.DX;
			Option1Computation();


		}	


		public void Option1Computation() 
		{
			double age = Math.Truncate(workData.MemberInfo.Age);
			double agex1 = 1 + Math.Truncate(workData.MemberInfo.Age);
			double agex14 = Math.Truncate(workData.MemberInfo.Age1by4);
			int currTrail = 0;
			double presentValue = 0;
			double annualAmount = 0;
			double interPolation = 0;
			double interPlaceHolder = 0;
			double diff = 1;



			while (diff > 0.00005 & currTrail < 20)
			{
				currTrail++;
				age = Math.Truncate(workData.MemberInfo.Age + interPlaceHolder) ;
				agex1 = 1 + Math.Truncate(workData.MemberInfo.Age + interPlaceHolder) ;
				agex14 = AppServices.Ceiling(workData.MemberInfo.Age1by4 + interPlaceHolder, workData.RoundOption1Fac);

				//MxN
				workData.MXn = (LifeTables.FirstOrDefault(x => x.Age == age).Mx * workData.InterestRate)
							  / (Math.Log(1 + workData.InterestRate));
				workData.MX1n = (LifeTables.FirstOrDefault(x => x.Age == agex1).Mx * workData.InterestRate)
							  / (Math.Log(1 + workData.InterestRate));
				workData.MX14n = (agex14 - age) * (workData.MX1n - workData.MXn) + workData.MXn;

				//RxN
				workData.RXn = (LifeTables.FirstOrDefault(x => x.Age == age).Rx * workData.InterestRate)
							  / (Math.Log(1 + workData.InterestRate));
				workData.RX1n = (LifeTables.FirstOrDefault(x => x.Age == agex1).Rx * workData.InterestRate)
							  / (Math.Log(1 + workData.InterestRate));
				workData.RX14n = (agex14 - age) * (workData.RX1n - workData.RXn ) + workData.RXn;

				presentValue = workData.X14 + AppServices.Ceiling(interPlaceHolder, workData.RoundOption1Fac) * (workData.MX14 - workData.MX14n) / workData.DX14
					- (workData.RX14 - workData.RX14n - AppServices.Ceiling(interPlaceHolder, workData.RoundOption1Fac) * workData.MX14n) / workData.DX14;

				annualAmount = (workData.EEContrBasic - workData.EEContrBasic *( workData.MX14-workData.MX14n)/workData.DX14)/
								(workData.X14 - (workData.RX14 - workData.RX14n - AppServices.Ceiling(interPlaceHolder, workData.RoundOption1Fac) * workData.MX14n)/ workData.DX14);

				interPolation = workData.EEContrBasic / annualAmount;

				workData.Option1 =  workData.X14/ interPolation;

				diff = Math.Abs(interPolation - interPlaceHolder);

				interPlaceHolder = interPolation;			

			} ;

			//Option 2
			workData.Option2 = workData.X14 / (workData.X14 + 1 * (workData.Y14 - workData.XY14));

			//Option 3 : This function never hits in Excel because its looking for spouse in the wrong place
			workData.Option3 = workData.RelationShip != "Spouse" ?
				workData.X14 / (workData.X14 + 0.5 * (workData.Y14 - workData.XY14)) : 0.0;//TODO:How to handle N/A condition

			//TODO: We need to set NA value
			workData.Option4 = workData.ContinueOption4 > 0 ?
				workData.X14 / (workData.X14 + workData.ContinueOption4 * (workData.Y14 - workData.XY14)) : 0.0;


			//Calc Computation
			//workData.MonthlyBenefits = if (workData.TypeOfRetirement == RetirementType.SCD) 
			//{
			//	return Math.Max(workData.ServiceRetirementBenefits, workData.SCD1by2FAS);
			//}
			

		}

	}
}
