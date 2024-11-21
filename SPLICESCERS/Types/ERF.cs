using System.Text.Json.Serialization;

namespace SPLICESCERS.Types
{
	public class ERF
    {
        private string ageAtRetirement;
        private string fraction;

        [JsonPropertyName("Age at retirement")]
        public string AgeAtRetirement { get => ageAtRetirement; set => ageAtRetirement = value; }
        public string Fraction { get => fraction; set => fraction = value; }
    }
}
