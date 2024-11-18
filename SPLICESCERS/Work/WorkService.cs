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
            }

		}
	}
}
