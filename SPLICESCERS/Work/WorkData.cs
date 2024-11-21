﻿using SPLICESCERS.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPLICESCERS.Work
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

        private List<ERF> erf31676_10;
        private List<ERF> erf31664;
        private List<ERF> erf31752A;
        private List<ERF> erf31752B;

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

		public List<ERF> ERF31676_10 { get => erf31676_10; set=> erf31676_10 = value;}
        public List<ERF> ERF31664 { get => erf31664; set => erf31664 = value; }
        public List<ERF> ERF31752A { get => erf31752A; set => erf31752A = value; }
        public List<ERF> ERF31752B { get => erf31752B; set => erf31752B = value; }

    }
}   
