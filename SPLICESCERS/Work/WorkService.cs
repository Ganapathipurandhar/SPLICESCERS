using SPLICESCERS.Types;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Runtime;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace SPLICESCERS.Work
{
	public class WorkService
	{
        WorkData _workData;
		public WorkService() {
            _workData = new WorkData();
        }
		public WorkService(string name) { }

        public void LoadData()
        {
            var applicationSettings = ConfigurationManager.GetSection("appSettings") as NameValueCollection;
            if (applicationSettings.Count == 0)
            {
                Console.WriteLine("Application Settings are not defined");
            }
            else
            {
                foreach (var key in applicationSettings.AllKeys)
                {

                    Console.WriteLine(key + " = " + applicationSettings[key]);
                }


                _workData.TypeOfRetirement = (RetirementType)Enum.Parse(typeof(RetirementType), applicationSettings["RetirementType"]);

                _workData.DateOfRetirement = DateTime.Parse(applicationSettings["DateOfRetirement"]);

                _workData.Membership = (MembershipType)Enum.Parse(typeof(MembershipType), applicationSettings["Membership"]);

                _workData.Tier = (Tiers)Enum.Parse(typeof(Tiers), applicationSettings["TIER"]);

                _workData.EEContrBasic = Convert.ToDouble(applicationSettings["EEContrBasic"]);
                _workData.EEContrCol = Convert.ToDouble(applicationSettings["EEContrCol"]);


                _workData.MoneyPurchase = (YesNo)Enum.Parse(typeof(YesNo), applicationSettings["MoneyPurchase"]);

                _workData.MemberInfo.Name = applicationSettings["MemberName"];
                _workData.MemberInfo.DOB = DateTime.Parse(applicationSettings["MemberDOB"]);

                _workData.BeneficiaryInfo.Name = applicationSettings["BeneficiaryName"];
                _workData.BeneficiaryInfo.DOB = DateTime.Parse(applicationSettings["BeneficiaryDOB"]);

                _workData.RelationShip = applicationSettings["Relationship"];

                _workData.IntegratedService.Years = Convert.ToInt32(applicationSettings["ISY"]);
                _workData.IntegratedService.Months = Convert.ToDouble(applicationSettings["ISM"]);
                _workData.IntegratedService.Days = Convert.ToDouble(applicationSettings["ISD"]);

                _workData.ISSick.Years = Convert.ToInt32(applicationSettings["SLIY"]);
                _workData.ISSick.Months = Convert.ToDouble(applicationSettings["SLIM"]);
                _workData.ISSick.Days = Convert.ToDouble(applicationSettings["SLID"]);

                _workData.NonIntegratedService.Years = Convert.ToInt32(applicationSettings["NISY"]);
                _workData.NonIntegratedService.Months = Convert.ToDouble(applicationSettings["NISM"]);
                _workData.NonIntegratedService.Days = Convert.ToDouble(applicationSettings["NISD"]);

                _workData.NonISSick.Years = Convert.ToInt32(applicationSettings["SLNIY"]);
                _workData.NonISSick.Months = Convert.ToDouble(applicationSettings["SLNIM"]);
                _workData.NonISSick.Days = Convert.ToDouble(applicationSettings["SLNID"]);

                _workData.FinalComp = Convert.ToDouble(applicationSettings["FinalComp"]);
            }

            var ERF31676_10_Settings = ConfigurationManager.GetSection("ERF31676.10") as NameValueCollection;
            if (ERF31676_10_Settings.Count == 0)
            {
                Console.WriteLine("ERF31676_10 Settings are not defined");
            }
            else
            {
                foreach (var key in ERF31676_10_Settings.AllKeys)
                {

                    Console.WriteLine(key + " = " + ERF31676_10_Settings[key]);
                }

                _workData.ERF31676_10_45.Value1 = double.Parse(ERF31676_10_Settings["45.00"]);
                _workData.ERF31676_10_45.Value2 = double.Parse(ERF31676_10_Settings["45.25"]);
                _workData.ERF31676_10_45.Value3 = double.Parse(ERF31676_10_Settings["45.50"]);
                _workData.ERF31676_10_45.Value4 = double.Parse(ERF31676_10_Settings["45.75"]);

                _workData.ERF31676_10_46.Value1 = double.Parse(ERF31676_10_Settings["46.00"]);
                _workData.ERF31676_10_46.Value2 = double.Parse(ERF31676_10_Settings["46.25"]);
                _workData.ERF31676_10_46.Value3 = double.Parse(ERF31676_10_Settings["46.50"]);
                _workData.ERF31676_10_46.Value4 = double.Parse(ERF31676_10_Settings["46.75"]);

                _workData.ERF31676_10_47.Value1 = double.Parse(ERF31676_10_Settings["47.00"]);
                _workData.ERF31676_10_47.Value2 = double.Parse(ERF31676_10_Settings["47.25"]);
                _workData.ERF31676_10_47.Value3 = double.Parse(ERF31676_10_Settings["47.50"]);
                _workData.ERF31676_10_47.Value4 = double.Parse(ERF31676_10_Settings["47.75"]);

                _workData.ERF31676_10_48.Value1 = double.Parse(ERF31676_10_Settings["48.00"]);
                _workData.ERF31676_10_48.Value2 = double.Parse(ERF31676_10_Settings["48.25"]);
                _workData.ERF31676_10_48.Value3 = double.Parse(ERF31676_10_Settings["48.50"]);
                _workData.ERF31676_10_48.Value4 = double.Parse(ERF31676_10_Settings["48.75"]);

                _workData.ERF31676_10_49.Value1 = double.Parse(ERF31676_10_Settings["49.00"]);
                _workData.ERF31676_10_49.Value2 = double.Parse(ERF31676_10_Settings["49.25"]);
                _workData.ERF31676_10_49.Value3 = double.Parse(ERF31676_10_Settings["49.50"]);
                _workData.ERF31676_10_49.Value4 = double.Parse(ERF31676_10_Settings["49.75"]);

                _workData.ERF31676_10_50.Value1 = double.Parse(ERF31676_10_Settings["50.00"]);
                _workData.ERF31676_10_50.Value2 = double.Parse(ERF31676_10_Settings["50.25"]);
                _workData.ERF31676_10_50.Value3 = double.Parse(ERF31676_10_Settings["50.50"]);
                _workData.ERF31676_10_50.Value4 = double.Parse(ERF31676_10_Settings["50.75"]);
            }

        }

        public void ComputeWorkSheet() 
        {
            //Age computatione of Member and Beneficiary
            CalculateAgeAtRetirement(_workData.DateOfRetirement, _workData.MemberInfo);
			CalculateAgeAtRetirement(_workData.DateOfRetirement, _workData.BeneficiaryInfo);

            _workData.ISDuration = CalculateServiceDuration(_workData.IntegratedService);
			_workData.ISSickDuration = CalculateServiceDuration(_workData.ISSick);
            _workData.TotalIS = _workData.ISDuration + _workData.ISSickDuration;

			_workData.NonISDuration = CalculateServiceDuration(_workData.NonIntegratedService); 
            _workData.NonISSickDuration = CalculateServiceDuration(_workData.NonISSick);
            _workData.TotalNonIS = _workData.NonISDuration + _workData.NonISSickDuration;
			//
			PrintProperty(_workData);
		}

        public void CalculateAgeAtRetirement(DateTime retirementDate, PersonInfo member) 
        {
			var totalDays = (retirementDate - member.DOB).TotalDays;
			member.Age = Math.Round((totalDays / 365.25), 2);
			member.Age1by4 = Math.Floor(member.Age / 0.25 + 0) * 0.25;
		}

        public double CalculateServiceDuration(DurationYMDs duration) 
        {
            double _yearInDecimal = duration.Years + (duration.Months/12) + (duration.Days/261);

            return _yearInDecimal;
        }

        public void PrintProperty(object t) 
        {
			foreach (PropertyDescriptor descriptor in TypeDescriptor.GetProperties(t))
			{
				string name = descriptor.Name;
				object value = descriptor.GetValue(t);
                Type _type = value.GetType();
                Console.WriteLine("{0}={1}", name, value);

				if (_type.IsEnum || (_type.Namespace == "System")) 
                { } 
                else
                { PrintProperty(value); }
				
			}
		}
	}
}
