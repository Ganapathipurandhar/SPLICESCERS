﻿using SPLICESCERS.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPLICESCERS.Types
{
	public  class WorkData
	{
		private RetirementType typeOfRetirement;
		private DateTime dateOfRetirement;
		private MembershipType membership;
		private Tiers tier;
		private YesNo moneyPurchase;
		private PersonInfo memberInfo = new PersonInfo();
		private PersonInfo beneficiaryInfo = new PersonInfo();
		private DurationYMDs integratedService = new DurationYMDs();
		private DurationYMDs iSSick = new DurationYMDs();
		private DurationYMDs nonIntegratedService = new DurationYMDs();
		private DurationYMDs nonISSick = new DurationYMDs();
		private double finalComp;
		private string relationShip;
		private double eEContrBasic;
		private double eEContrCol;
		private double iSDuration;
		private double iSSickDuration;
		private double totalIS;
		private double nonISDuration;
		private double nonISSickDuration;
		private double totalNonIS;
		private double totalService;
		private string erfArticle;
		private double erfFraction;



		#region InputValueFromConfig

		public RetirementType TypeOfRetirement { get => typeOfRetirement; set => typeOfRetirement = value; }
		public DateTime DateOfRetirement { get => dateOfRetirement; set => dateOfRetirement = value; }
		public MembershipType Membership { get => membership; set => membership = value; }
		public Tiers Tier { get => tier; set => tier = value; }
		public YesNo MoneyPurchase { get => moneyPurchase; set => moneyPurchase = value; }

		//Member Information
		public PersonInfo MemberInfo { get => memberInfo; set => memberInfo = value; }
		//Beneficiary Information
		public PersonInfo BeneficiaryInfo { get => beneficiaryInfo; set => beneficiaryInfo = value; }

		//TODO
		//Options are Spouse and Partner (Registered Domestic Partner)
		//RelationShip we will use string for now, but will change it to Enum later when we know all options
		public string RelationShip { get => relationShip; set => relationShip = value; }

		//Duration of Integrated Service(IS) and Sick Leave
		public DurationYMDs IntegratedService { get => integratedService; set => integratedService = value; }
		public DurationYMDs ISSick { get => iSSick; set => iSSick = value; }

		//Duration of Non Integrated Service(IS) and Sick Leave (Non Integrated
		public DurationYMDs NonIntegratedService { get => nonIntegratedService; set => nonIntegratedService = value; }
		public DurationYMDs NonISSick { get => nonISSick; set => nonISSick = value; }

		//Employee Contriubtion (EE Contr)
		public double EEContrBasic { get => eEContrBasic; set => eEContrBasic = value; }
		public double EEContrCol { get => eEContrCol; set => eEContrCol = value; }

		//Final Compensation
		public double FinalComp { get => finalComp; set => finalComp = value; }
		#endregion

		//Computed Values for Work -Sheet
		public double ISDuration { get => iSDuration; set => iSDuration = value; }
		public double ISSickDuration { get => iSSickDuration; set => iSSickDuration = value; }
		public double TotalIS { get => totalIS; set => totalIS = value; }

		public double NonISDuration { get => nonISDuration; set => nonISDuration = value; }
		public double NonISSickDuration { get => nonISSickDuration; set => nonISSickDuration = value; }
		public double TotalNonIS { get => totalNonIS; set => totalNonIS = value; }

		public double TotalService { get => totalService; set => totalService = value; }

		//Article Number it will either 31676.1 or 31664"
		public string ERFArticle { get => erfArticle; set => erfArticle = value; }
		public double ERFFraction { get => erfFraction; set => erfFraction = value; }



	}
}   
