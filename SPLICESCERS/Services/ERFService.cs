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
		private List<ERF> erf31676_10;
		private List<ERF> erf31664;
		private List<ERF> erf31752A;
		private List<ERF> erf31752B;

		public List<ERF> ERF31676_10 { get => erf31676_10; set => erf31676_10 = value; }
		public List<ERF> ERF31664 { get => erf31664; set => erf31664 = value; }
		public List<ERF> ERF31752A { get => erf31752A; set => erf31752A = value; }
		public List<ERF> ERF31752B { get => erf31752B; set => erf31752B = value; }


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
