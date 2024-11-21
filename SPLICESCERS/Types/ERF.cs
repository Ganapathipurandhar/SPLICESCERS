using System.Text.Json.Serialization;

namespace SPLICESCERS.Types
{
	public class ERF
    {
        private double ageAtRetirement;
        private double fraction;

        [JsonPropertyName("Age at retirement")]
        public double AgeAtRetirement { get => ageAtRetirement; set => ageAtRetirement = value; }
        public double Fraction { get => fraction; set => fraction = value; }
    }
}
