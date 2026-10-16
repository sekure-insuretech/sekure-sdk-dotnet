using System;

namespace Sekure.Models.Endorsement
{
    /// <summary>
    /// Documento activo del crédito (todos los endosos del mismo CreditNumber + TenantId).
    /// Nunca incluye URL ni SAS — solo StoredFilePublicId (ADR-001).
    /// </summary>
    public class EndorsementDocumentItem
    {
        public string DocumentType { get; set; }
        public string FileName { get; set; }
        public DateTime CreatedAt { get; set; }

        /// <summary>Referencia opaca al archivo almacenado. Nunca URL ni SAS.</summary>
        public Guid? StoredFilePublicId { get; set; }
    }
}
