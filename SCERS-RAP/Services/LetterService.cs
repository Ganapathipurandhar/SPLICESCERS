using SCERS_RAP.Type;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCERS_RAP.Services
{
	public class LetterService
	{
		private RPAData rd;
		private RPAWork work;
		private PreLoad pl;
		private Factor f;		
		private Calc calc;
		private Letter letter;

		public LetterService(RPAData rd) 
		{
			this.rd = rd;
			this.work = rd.Work;
			this.pl = rd.PreLoad;
			this.f = rd.Factor;		
			this.calc = rd.Calc;
			rd.Letter = new Letter();
			this.letter = rd.Letter;			
		}

		public void BuildLetterData() 
		{
			letter.MemberName  = pl.MemberInfo.Name;
		
			letter.Membership = pl.Membership.ToString();
			letter.Tier =((int)pl.Tier).ToString();
			letter.RetirementDate = pl.DateOfRetirement.ToString("MMDDYYYY");
			letter.MemberDOB = pl.MemberInfo.DOB.ToString("MMDDYYYY");
			letter.Integrated = work.TotalIS.ToString("0.##");
			letter.NonIntegrated = work.TotalNonIS.ToString("0.##");
			letter.AvgMonthlyComp = pl.FinalComp.ToString("0.##");

			letter.BeneficiaryName = pl.BeneficiaryInfo.Name;
			letter.RelationShip = pl.RelationShip.ToString();
			letter.BeneficiaryDOB = pl.BeneficiaryInfo.DOB.ToString("MMDDYYYY");

			letter.UnmodifiedCSA = calc.AnnuityBenefit.ToString("0.##");
			letter.UnmodifiedCSP = calc.PensionBenefit.ToString("0.##");
			letter.UnmodifiedTP = calc.MonthlyBenefits.ToString("0.##");
			letter.UnmodifiedCTS = calc.CtB.ToString("0.##");

			letter.Option1CSA = calc.OP1AnnuityBenefit.ToString("0.##");
			letter.Option1CSP = calc.OP1PensionBenefit.ToString("0.##");
			letter.Option1TP = calc.OP1Total.ToString("0.##");
			letter.Option1CTS = "0.00";

			letter.Option2CSA = calc.OP2AnnuityBenefit.ToString("0.##");
			letter.Option2CSP = calc.OP2PensionBenefit.ToString("0.##");
			letter.Option2TP = calc.OP2Total.ToString("0.##");
			letter.Option2CTS = calc.OP2CtB.ToString("0.##");


			letter.Option3CSA = calc.OP3AnnuityBenefit.ToString("0.##");
			letter.Option3CSP = calc.OP3PensionBenefit.ToString("0.##");
			letter.Option3TP = calc.OP3Total.ToString("0.##");
			letter.Option3CTS = calc.OP3CtB.ToString("0.##");

			letter.BasicCSA = pl.EEContrBasic.ToString("0.##");
			letter.BasicCSP = calc.PensionReserve.ToString("0.##");
			letter.BasicTP = calc.Total.ToString("0.##");
			letter.BasicCTS = calc.BasicSpouse.ToString("0.##");

			letter.COLCSA = pl.EEContrCol.ToString("0.##");
			letter.COLCSP = calc.COLCSP.ToString("0.##");
			letter.COLTP = calc.COLTotal.ToString("0.##");
			letter.COLCTS = calc.COLSpouse.ToString("0.##");
		}

		public void PrintLetter(object t, string templatePath) 
		{
			//Load HTML Files @".\Data\PreLoad.json"
			string  temp = File.ReadAllText(templatePath);
			string placeholder ="";
			foreach (PropertyDescriptor descriptor in TypeDescriptor.GetProperties(t))
			{
				string name = "["+descriptor.Name+"]";
				object value = descriptor.GetValue(t);
				if (temp.Contains(name))
				{
					placeholder = temp.Replace(name, value.ToString());
				}
				temp = placeholder;
			}

			string outputFilepath = @"./Output1.html";
			if (!string.IsNullOrEmpty(temp))
			{
				using (StreamWriter writer = new StreamWriter(outputFilepath))
				{
					writer.WriteLine(temp);
				}
			}

		}
	}
}
