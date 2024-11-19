using SPLICESCERS.Types;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Globalization;
using System.Linq;
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

                Enum.TryParse(applicationSettings["RetirementType"], out RetirementType rt);
                _workData.TypeOfRetirement = rt;

                _workData.DateOfRetirement = Convert.ToDateTime(applicationSettings["DateOfRetirement"], CultureInfo.CurrentCulture);

                Enum.TryParse(applicationSettings["Membership"], out MembershipType mt);
                _workData.Membership = mt;

                Enum.TryParse(applicationSettings["TIER"], out Tiers ti);
                _workData.Tier = ti;

                _workData.EEContrBasic = Convert.ToDouble(applicationSettings["EEContBasic"]);
                _workData.EEContrCol = Convert.ToDouble(applicationSettings["EEContCol"]);


                Enum.TryParse(applicationSettings["MoneyPurchase"], out YesNo yesNo);
                _workData.MoneyPurchase = yesNo;

                _workData.MemberInfo = new PersonInfo();
                _workData.MemberInfo.Name = applicationSettings["MemberName"];
                _workData.MemberInfo.DOB = DateTime.Parse(applicationSettings["MemberDOB"]);

                _workData.BeneficiaryInfo = new PersonInfo();
                _workData.BeneficiaryInfo.Name = applicationSettings["BeneficiaryName"];
                _workData.BeneficiaryInfo.DOB = DateTime.Parse(applicationSettings["BeneficiaryDOB"]);

                _workData.RelationShip = applicationSettings["Relationship"];

                _workData.IntegratedService = new DurationYMDs();
                _workData.IntegratedService.Years = Convert.ToInt32(applicationSettings["ISY"]);
                _workData.IntegratedService.Months = Convert.ToInt32(applicationSettings["ISM"]);
                _workData.IntegratedService.Days = Convert.ToDouble(applicationSettings["ISD"]);

                _workData.ISSick = new DurationYMDs();
                _workData.ISSick.Years = Convert.ToInt32(applicationSettings["SLIY"]);
                _workData.ISSick.Months = Convert.ToInt32(applicationSettings["SLIM"]);
                _workData.ISSick.Days = Convert.ToDouble(applicationSettings["SLID"]);

                _workData.NonIntegratedService = new DurationYMDs();
                _workData.NonIntegratedService.Years = Convert.ToInt32(applicationSettings["NISY"]);
                _workData.NonIntegratedService.Months = Convert.ToInt32(applicationSettings["NISM"]);
                _workData.NonIntegratedService.Days = Convert.ToInt32(applicationSettings["NISD"]);

                _workData.NonISSick = new DurationYMDs();
                _workData.NonISSick.Years = Convert.ToInt32(applicationSettings["SLNIY"]);
                _workData.NonISSick.Years = Convert.ToInt32(applicationSettings["SLNIM"]);
                _workData.NonISSick.Years = Convert.ToInt32(applicationSettings["SLNID"]);

                _workData.FinalComp = Convert.ToDouble(applicationSettings["FinalComp"]);

            }

		}
	}
}
