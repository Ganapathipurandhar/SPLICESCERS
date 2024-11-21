using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPLICESCERS.Types
{
    public class ERF
    {
        private double ageAtRetirement;
        private double fraction;

        public double AgeAtRetirement { get => ageAtRetirement; set => ageAtRetirement = value; }
        public double Fraction { get => fraction; set => fraction = value; }
    }
}
