using System;

using System.Configuration;

namespace SPLICESCERS
{
	public class test
	{
        public test()
        {
            
        }

        public void printName(string name) 
		{
			string sValue = ConfigurationManager.AppSettings["Name"];
			Console.WriteLine("My Name is :" + name);

			Console.WriteLine("My Config Name is :"  + sValue);
		}
	}
}
