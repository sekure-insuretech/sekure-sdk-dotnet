namespace Sekure.Models
{
    public class CalculationInfoUpdate
    {
        public CalculationInfoUpdate(int id, int presubscribedId, string description, string value, int type)
        {
            Id = id;
            PresubscribedId = presubscribedId;
            Description = description;
            Value = value;
            Type = type;
        }

        public CalculationInfoUpdate()
        {
        }

        public int Id { get; set; }
        public int PresubscribedId { get; set; }
        public string Description { get; set; }
        public string Value { get; set; }
        public int Type { get; set; }
    }
}
