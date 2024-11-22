using SPLICESCERS.Types;
using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Configuration;

namespace SPLICESCERS.Services
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

            CalculateERF();
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

        public void CalculateERF() 
        {
            if (_workData.Membership == MembershipType.General)
            {
                _workData.ERFArticle = "ERF 31676.1";
				_workData.ERFFraction = ERFService.GetFraction(ERFService.ERF31676_10, _workData.MemberInfo.Age1by4);
			}
            else 
            {
                _workData.ERFArticle = "ERF 31664";
				_workData.ERFFraction = ERFService.GetFraction(ERFService.ERF31664, _workData.MemberInfo.Age1by4);

			}
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
