namespace Sekure.Models
{
    public class ProductReference
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PolicyTypeName { get; set; }
        public string InsuranceCompanyName { get; set; }

        public ProductReference() { }
        public ProductReference(int id, string name, string policyTypeName, string insuranceCompanyName)
        {
            Id = id;
            Name = name;
            PolicyTypeName = policyTypeName;
            InsuranceCompanyName = insuranceCompanyName;
        }
    }
}
