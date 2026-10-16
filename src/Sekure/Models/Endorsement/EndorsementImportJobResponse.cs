using System;
using System.Collections.Generic;

namespace Sekure.Models.Endorsement
{
    /// <summary>
    /// Respuesta del endpoint GET Endorsement/import/{jobId}.
    /// Incluye estado, avance y lista de problemas (sin datos personales).
    /// </summary>
    public class EndorsementImportJobResponse
    {
        public Guid JobId { get; set; }
        public byte ProductType { get; set; }
        public byte EndorsementType { get; set; }
        public byte Status { get; set; }
        public int TotalRows { get; set; }
        public int ProcessedRows { get; set; }
        public int SucceededCredits { get; set; }
        public int FailedCredits { get; set; }
        public int WarningCount { get; set; }
        public string SourceFileName { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? StartedAt { get; set; }
        public DateTime? FinishedAt { get; set; }
        public string ErrorMessage { get; set; }
        public List<EndorsementImportIssueItem> Issues { get; set; } = new List<EndorsementImportIssueItem>();
    }

    /// <summary>
    /// Item de problema en el reporte de importacion.
    /// Sin cedula, nombre, correo ni valores monetarios (CA03).
    /// </summary>
    public class EndorsementImportIssueItem
    {
        public int RowNumber { get; set; }
        public string ColumnName { get; set; }
        public string CreditNumber { get; set; }
        public string Code { get; set; }
        public byte Severity { get; set; }
        public string Message { get; set; }
    }

    /// <summary>
    /// Respuesta del endpoint POST Endorsement/import (202 Accepted).
    /// </summary>
    public class StartImportResponse
    {
        public Guid JobId { get; set; }
        public string Status { get; set; } = "Pendiente";
    }
}
