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
		private PersonInfo memberInfo;
		private PersonInfo beneficiaryInfo;
		private DurationYMDs integratedService;
		private DurationYMDs iSSick;
		private DurationYMDs nonIntegratedService;
		private DurationYMDs nonISSick;
		private double finalComp;
		private string relationShip;

		/// <summary>
		/// Input Values - Get from App.Config until production
		/// 
		/// </summary>
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
		//RelationShip we will use string for now, but will change it to Enum later when we know all options
		public string RelationShip { get => relationShip; set => relationShip = value; }

		//Duration of Integrated Service(IS) and Sick Leave
		public DurationYMDs IntegratedService { get => integratedService; set => integratedService = value; }
		public DurationYMDs ISSick { get => iSSick; set => iSSick = value; }

		//Duration of Non Integrated Service(IS) and Sick Leave (Non Integrated
		public DurationYMDs NonIntegratedService { get => nonIntegratedService; set => nonIntegratedService = value; }
		public DurationYMDs NonISSick { get => nonISSick; set => nonISSick = value; }

		//Final Compensation
		public double FinalComp { get => finalComp; set => finalComp = value; }

	}
}   