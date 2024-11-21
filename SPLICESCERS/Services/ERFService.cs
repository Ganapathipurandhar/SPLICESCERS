using SPLICESCERS.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPLICESCERS.Services
{
	public class ERFService
	{
		public List<ERF> ERF31676_10 { get; set; }
		public List<ERF> ERF31664 { get; set; }
		public List<ERF> ERF31752A { get; set; }
		public List<ERF> ERF31752B { get; set; }


		public ERFService() 
		{
			ERF31676_10 = LoadJson(@".\Data\31676_10.json");
			ERF31664 = LoadJson(@".\Data\31664.json");
			ERF31752A = LoadJson(@".\Data\31752A.json");
			ERF31752B = LoadJson(@".\Data\31752B.json");
		}

		public List<ERF> LoadJson(string path)
		{
			var list = JsonFileReader.Read<ERF>(path);
			return list;
		}
	}
}
