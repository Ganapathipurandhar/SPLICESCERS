using SCERS_RAP.Type;
using System.Collections.Generic;
using System.Linq;

namespace SCERS_RAP.Services
{
	public class DataServices
	{
		//private static List<ERF> erf31676_10;
		//private static List<ERF> erf31664;
		//private static List<ERF> erf31752A;
		//private static List<ERF> erf31752B;
		//private static List<GAM> gAMqx;
		//private static List<DisGAM> disGAMqx;

		public static List<ERF> ERF31676_10 { get; set ; }
		public static List<ERF> ERF31664 { get; set; }
		public static List<ERF> ERF31752A { get; set; }
		public static List<ERF> ERF31752B { get; set; }
		public static List<GAM> GAMqx { get; set; }
		public static List<DisGAM> DisGAMqx { get; set; }


		public static void LoadStaticData()
		{			
			ERF31676_10 = LoadJson<ERF>(@".\Data\31676_10.json");
			ERF31664 = LoadJson<ERF>(@".\Data\31664.json");
			ERF31752A = LoadJson<ERF>(@".\Data\31752A.json");
			ERF31752B = LoadJson<ERF>(@".\Data\31752B.json");
			GAMqx = LoadJson<GAM>(@".\Data\GAMqx.json");
			DisGAMqx = LoadJson<DisGAM>(@".\Data\DisGAMqx.json");
		}

		public static List<T> LoadJson<T>(string path)
		{
			var list = AppServices.ReadJson<T>(path);
			return list;
		}

		public static double GetFraction(List<ERF> list, double age)
		{
			var value = list.First(item => item.AgeAtRetirement == age).Fraction;
			return value;
		}

		public static S GetValue<T, S>(List<T> list, string findVariable, double findValue, string returnVariable ) 
		{
			var value = list.First(item=>item.GetType().GetProperty(findVariable).GetValue(item,null).Equals(findValue));
			return (S)value.GetType().GetProperty(returnVariable).GetValue(value,null);
		}
	}
}
