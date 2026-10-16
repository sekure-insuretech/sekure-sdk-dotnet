namespace Sekure.Models
{
    public class CalculationInfoUpdate
    {
        public CalculationInfoUpdate()
        {
        }

        public CalculationInfoUpdate(
            int id,
            string name,
            string description,
            int presubscribedId,
            int calculationInfoTypeId,
            string value,
            int order,
            int? classificationId = null,
            bool? isProcessEditable = null,
            bool showAdmin = true)
        {
            Id = id;
            Name = name;
            Description = description;
            PresubscribedId = presubscribedId;
            CalculationInfoTypeId = calculationInfoTypeId;
            Value = value;
            Order = order;
            ClassificationId = classificationId;
            IsProcessEditable = isProcessEditable;
            ShowAdmin = showAdmin;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int PresubscribedId { get; set; }
        public int CalculationInfoTypeId { get; set; }
        public string Value { get; set; }
        public int Order { get; set; }
        public int? ClassificationId { get; set; }
        public bool? IsProcessEditable { get; set; }
        public bool ShowAdmin { get; set; } = true;
    }
}
