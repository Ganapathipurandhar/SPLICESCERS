using SPLICESCERS.Types;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPLICESCERS.Services
{
	public static class DataServices
	{
		private static List<ERF> erf31676_10;
		private static List<ERF> erf31664;
		private static List<ERF> erf31752A;
		private static List<ERF> erf31752B;
		private static List<GAM> gAMqx;
		private static List<DisGAM> disGAMqx;

		public static List<ERF> ERF31676_10 { get => erf31676_10; set => erf31676_10 = value; }
		public static List<ERF> ERF31664 { get => erf31664; set => erf31664 = value; }
		public static List<ERF> ERF31752A { get => erf31752A; set => erf31752A = value; }
		public static List<ERF> ERF31752B { get => erf31752B; set => erf31752B = value; }
		public static List<GAM> GAMqx { get => gAMqx; set => gAMqx = value; }
		public static List<DisGAM> DisGAMqx { get => disGAMqx; set => disGAMqx = value; }


		public static void LoadERF() 
		{
			ERF31676_10 = LoadJson<ERF>(@".\Data\31676_10.json");
			ERF31664 = LoadJson<ERF>(@".\Data\31664.json");
			ERF31752A = LoadJson<ERF>(@".\Data\31752A.json");
			ERF31752B = LoadJson<ERF>(@".\Data\31752B.json");
			GAMqx = LoadJson<GAM>(@".\Data\GAMqx.json");
			DisGAMqx = LoadJson<DisGAM>(@".\Data\DisGAMqx.json");
		}

		public static  List<T> LoadJson<T>(string path)
		{
			var list = FileServices.ReadJson<T>(path);
			return list;
		}

		public static double GetFraction(List<ERF> list, double age) 
		{
			var value = list.First(item => item.AgeAtRetirement == age).Fraction;
			return value;

		}
	}
}
