namespace Sekure.Models
{
    /// <summary>Transaction status of the catalog (MSParametrics TransactionStatus).</summary>
    public class TransactionStatus
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    /// <summary>Body of PATCH v2/Products/UpdateTransactionStatus/{sessionId}.</summary>
    public class UpdateTransactionStatusRequest
    {
        public int StatusId { get; set; }
        public string Comments { get; set; }
    }
}
