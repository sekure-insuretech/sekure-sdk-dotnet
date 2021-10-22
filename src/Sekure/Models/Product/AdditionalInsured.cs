using System.Collections.Generic;

namespace Sekure.Models
{
    public class AdditionalInsured
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Value { get; set; }
        public List<CoverageResultAPI> Coverages { get; set; }

        public AdditionalInsured() { }

        public AdditionalInsured(string name, string description, string value, List<CoverageResultAPI> coverages)
        {
            Name = name;
            Description = description;
            Value = value;
            Coverages = coverages;
        }
    }
}
