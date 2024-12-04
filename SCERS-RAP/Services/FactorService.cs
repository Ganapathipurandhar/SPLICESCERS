using SCERS_RAP.Type;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace SCERS_RAP.Services
{
	public class FactorService
	{
		private RPAData rd;
		private RPAWork work;
		private PreLoad pl;
		private Factor f;
		private List<LifeTable> lts;

		public FactorService(RPAData rd) 
		{
			this.rd = rd;
			this.work = this.rd.Work;
			this.pl = rd.PreLoad;
			this.rd.Factor = new Factor();
			this.f = this.rd.Factor;
		}

		public void CalculateFactor() 
		{
			prepareForLifeTable();
			computeLifeTable();
			//After Life Table Compute values dependent on 
			computeADueFactor();
			Option1Computation();
		}

		private void prepareForLifeTable() 
		{
			//Life Table Computation Age Options
			f.XMinusY = (int)(pl.MemberInfo.AgeTrunc - pl.BeneficiaryInfo.AgeTrunc);
			f.XMinusYPlus1 = (int)(pl.MemberInfo.AgeTrunc - (pl.BeneficiaryInfo.AgeTrunc + 1));
			f.XPlus1MinusY = (int)((pl.MemberInfo.AgeTrunc + 1) -pl.BeneficiaryInfo.AgeTrunc);
			f.XPlus1MinusYPlus1 = (int)((pl.MemberInfo.AgeTrunc + 1) - (pl.BeneficiaryInfo.AgeTrunc + 1));
			f.IRCOLA = (1 + pl.InterestRate) / (1 + pl.COLA) - 1;
		}

		private void computeLifeTable() 
		{
			LifeTableService lifeTableService = new LifeTableService(rd);
			lifeTableService.CalculateLifeTable();
			lts = rd.LifeTables;
		}

		public void computeADueFactor() 
		{
			var _age = pl.MemberInfo.AgeTrunc;
			var _bage = pl.BeneficiaryInfo.AgeTrunc;


			f.XY = (lts.FirstOrDefault(x => x.Age == _age).Nxy /
				 lts.FirstOrDefault(x => x.Age == _age).Dxy) - pl.FeqToPay;
			f.XY1 = (lts.FirstOrDefault(x => x.Age == _age).Nxy1 /
				 lts.FirstOrDefault(x => x.Age == _age).Dxy1) - pl.FeqToPay;
			f.X1Y = (lts.FirstOrDefault(x => x.Age == _age + 1).Nx1y /
				 lts.FirstOrDefault(x => x.Age == _age + 1).Dx1y) - pl.FeqToPay;
			f.X1Y1 = (lts.FirstOrDefault(x => x.Age == _age + 1).Nx1y1 /
				 lts.FirstOrDefault(x => x.Age == _age + 1).Dx1y1) - pl.FeqToPay;

			f.XY14 = (f.XY1 - f.XY) *
								(pl.BeneficiaryInfo.Age1by4 - _bage) + f.XY;
			f.X1Y14 = (f.X1Y1 - f.X1Y) *
								(pl.BeneficiaryInfo.Age1by4 - _bage) + f.X1Y;
			//*****a due Joint Life(12)*****//
			f.X14Y14 = (f.X1Y14 - f.XY14) *
								(pl.MemberInfo.Age1by4 - _age) + f.XY14;
			//Member Factor
			f.X = (lts.FirstOrDefault(x => x.Age == _age).Nx /
				 lts.FirstOrDefault(x => x.Age == _age).Dx) - pl.FeqToPay;
			f.X1 = (lts.FirstOrDefault(x => x.Age == _age + 1).Nx /
				 lts.FirstOrDefault(x => x.Age == _age + 1).Dx) - pl.FeqToPay;
			//*****a due Retirement Age(12)*****//
			f.X14 = f.X + (f.X1 - f.X)
									* (pl.MemberInfo.Age1by4 - _age);

			//Beneficiary Factor
			f.Y = (lts.FirstOrDefault(x => x.Age == _bage).Ny /
				 lts.FirstOrDefault(x => x.Age == _bage).Dy) - pl.FeqToPay;
			f.Y1 = (lts.FirstOrDefault(x => x.Age == _bage + 1).Ny /
				 lts.FirstOrDefault(x => x.Age == _bage + 1).Dy) - pl.FeqToPay;
			//*****a due Beneficiary Age(12)*****//
			f.Y14 = f.Y + (f.Y1 - f.Y)
				* (pl.BeneficiaryInfo.Age1by4 - _bage);


			//Prime Computation
			f.XYPrime = (lts.FirstOrDefault(x => x.Age == _age).NxyPrime /
				 lts.FirstOrDefault(x => x.Age == _age).DxyPrime) - pl.FeqToPay;
			f.XY1Prime = (lts.FirstOrDefault(x => x.Age == _age).Nxy1Prime /
				 lts.FirstOrDefault(x => x.Age == _age).Dxy1Prime) - pl.FeqToPay;
			f.X1YPrime = (lts.FirstOrDefault(x => x.Age == _age + 1).Nx1yPrime /
				 lts.FirstOrDefault(x => x.Age == _age + 1).Dx1yPrime) - pl.FeqToPay;
			f.X1Y1Prime = (lts.FirstOrDefault(x => x.Age == _age + 1).Nx1y1Prime /
				 lts.FirstOrDefault(x => x.Age == _age + 1).Dx1y1Prime) - pl.FeqToPay;

			f.XY14Prime = (f.XY1Prime - f.XYPrime) *
								(pl.BeneficiaryInfo.Age1by4 - _bage) + f.XYPrime;
			f.X1Y14Prime = (f.X1Y1Prime - f.X1YPrime) *
								(pl.BeneficiaryInfo.Age1by4 - _bage) + f.X1YPrime;
			//*****a due Joint Life(12)*****//
			f.X14Y14Prime = (f.X1Y14Prime - f.XY14Prime) *
								(pl.MemberInfo.Age1by4 - _age) + f.XY14Prime;
			//Member Factor
			f.XPrime = (lts.FirstOrDefault(x => x.Age == _age).NxPrime /
				 lts.FirstOrDefault(x => x.Age == _age).DxPrime) - pl.FeqToPay;
			f.X1Prime = (lts.FirstOrDefault(x => x.Age == _age + 1).NxPrime /
				 lts.FirstOrDefault(x => x.Age == _age + 1).DxPrime) - pl.FeqToPay;
			//*****a due Retirement Age(12)*****//
			f.X14Prime = f.XPrime + (f.X1 - f.XPrime)
									* (pl.MemberInfo.Age1by4 - _age);

			//Beneficiary Factor
			f.YPrime = (lts.FirstOrDefault(x => x.Age == _bage).NyPrime /
				 lts.FirstOrDefault(x => x.Age == _bage).DyPrime) - pl.FeqToPay;
			f.Y1Prime = (lts.FirstOrDefault(x => x.Age == _bage + 1).NyPrime /
				 lts.FirstOrDefault(x => x.Age == _bage + 1).DyPrime) - pl.FeqToPay;
			//*****a due Beneficiary Age(12)*****//
			f.Y14Prime = f.YPrime + (f.Y1Prime - f.YPrime)
				* (pl.BeneficiaryInfo.Age1by4 - _bage);

			//Mx
			f.MX = (lts.FirstOrDefault(x => x.Age == _age).Mx * pl.InterestRate)
						  / (Math.Log(1 + pl.InterestRate));
			f.MX1 = (lts.FirstOrDefault(x => x.Age == _age + 1).Mx * pl.InterestRate)
						  / (Math.Log(1 + pl.InterestRate));
			f.MX14 = (pl.MemberInfo.Age1by4 - _age) * (f.MX1 - f.MX) + f.MX;

			//Rx
			f.RX = (lts.FirstOrDefault(x => x.Age == _age).Rx * pl.InterestRate)
						  / (Math.Log(1 + pl.InterestRate));
			f.RX1 = (lts.FirstOrDefault(x => x.Age == _age + 1).Rx * pl.InterestRate)
						  / (Math.Log(1 + pl.InterestRate));
			f.RX14 = (pl.MemberInfo.Age1by4 - _age) * (f.RX1 - f.RX) + f.RX;

			//Dx
			f.DX = lts.FirstOrDefault(x => x.Age == _age).Dx;
			f.DX1 = lts.FirstOrDefault(x => x.Age == _age + 1).Dx;
			f.DX14 = (pl.MemberInfo.Age1by4 - _age) * (f.DX1 - f.DX) + f.DX;
		}

		public void Option1Computation()
		{
			double age = Math.Truncate(pl.MemberInfo.Age);
			double agex1 = 1 + Math.Truncate(pl.MemberInfo.Age);
			double agex14 = Math.Truncate(pl.MemberInfo.Age1by4);
			int currTrail = 0;
			double presentValue = 0;
			double annualAmount = 0;
			double interPolation = 0;
			double interPlaceHolder = 0;
			double diff = 1;

			while (diff > 0.00005 & currTrail < 20)
			{
				currTrail++;
				age = Math.Truncate(pl.MemberInfo.Age + interPlaceHolder);
				agex1 = 1 + Math.Truncate(pl.MemberInfo.Age + interPlaceHolder);
				agex14 = AppServices.Ceiling(pl.MemberInfo.Age1by4 + interPlaceHolder, pl.RoundOption1Fac);

				//MxN
				f.MXn = (lts.FirstOrDefault(x => x.Age == age).Mx * pl.InterestRate)
							  / (Math.Log(1 + pl.InterestRate));
				f.MX1n = (lts.FirstOrDefault(x => x.Age == agex1).Mx * pl.InterestRate)
							  / (Math.Log(1 + pl.InterestRate));
				f.MX14n = (agex14 - age) * (f.MX1n - f.MXn) + f.MXn;

				//RxN
				f.RXn = (lts.FirstOrDefault(x => x.Age == age).Rx * pl.InterestRate)
							  / (Math.Log(1 + pl.InterestRate));
				f.RX1n = (lts.FirstOrDefault(x => x.Age == agex1).Rx * pl.InterestRate)
							  / (Math.Log(1 + pl.InterestRate));
				f.RX14n = (agex14 - age) * (f.RX1n - f.RXn) + f.RXn;

				presentValue = f.X14 + AppServices.Ceiling(interPlaceHolder, pl.RoundOption1Fac) * (f.MX14 - f.MX14n) / f.DX14
					- (f.RX14 - f.RX14n - AppServices.Ceiling(interPlaceHolder, pl.RoundOption1Fac) * f.MX14n) / f.DX14;

				annualAmount = (pl.EEContrBasic - pl.EEContrBasic * (f.MX14 - f.MX14n) / f.DX14) /
								(f.X14 - (f.RX14 - f.RX14n - AppServices.Ceiling(interPlaceHolder, pl.RoundOption1Fac) * f.MX14n) / f.DX14);

				interPolation = pl.EEContrBasic / annualAmount;

				f.Option1 = f.X14 / interPolation;

				diff = Math.Abs(interPolation - interPlaceHolder);

				interPlaceHolder = interPolation;

			};

			//Option 2
			f.Option2 = f.X14 / (f.X14 + 1 * (f.Y14 - f.XY14));

			//Option 3 : This function never hits in Excel because its looking for spouse in the wrong place
			f.Option3 = pl.RelationShip != RelationShip.Spouse ?
				f.X14 / (f.X14 + 0.5 * (f.Y14 - f.XY14)) : 0.0;//TODO:How to handle N/A condition

			//TODO: We need to set NA value
			f.Option4 = pl.ContinueOption4 > 0 ?
				f.X14 / (f.X14 + pl.ContinueOption4 * (f.Y14 - f.XY14)) : 0.0;

		

		}
	}
}
