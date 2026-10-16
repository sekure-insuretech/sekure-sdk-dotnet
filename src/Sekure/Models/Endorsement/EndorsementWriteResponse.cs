using System;
using System.Collections.Generic;

namespace Sekure.Models.Endorsement
{
    /// <summary>
    /// Respuesta para operaciones de escritura (POST, PUT).
    /// Incluye todos los campos del endoso (con codeudores) más las advertencias de validación.
    /// No reemplaza EndorsementResponse (GET); se usa exclusivamente para 201/200 de escritura.
    /// </summary>
    public class EndorsementWriteResponse
    {
        public Guid Id { get; set; }
        public Guid TenantId { get; set; }
        public byte ProductType { get; set; }
        public byte EndorsementType { get; set; }
        public string CreditNumber { get; set; }
        public DateTime DisbursementDate { get; set; }
        public DateTime CreditEndDate { get; set; }
        public DateTime CollectiveWithdrawalDate { get; set; }
        public decimal CollectivePremium { get; set; }
        public short InitialYear { get; set; }
        public decimal? OutstandingBalance { get; set; }
        public bool IsRenewal { get; set; }
        public string ClientName { get; set; }
        public string InsurerName { get; set; }
        public string PolicyNumber { get; set; }
        public decimal InsuredValue { get; set; }
        public decimal? PropertyValue { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public byte PolicyStatus { get; set; }

        // Codeudor 1
        public string Codebtor1Name { get; set; }
        public string Codebtor1InsurerName { get; set; }
        public string Codebtor1PolicyNumber { get; set; }
        public decimal? Codebtor1InsuredValue { get; set; }
        public DateTime? Codebtor1StartDate { get; set; }
        public DateTime? Codebtor1EndDate { get; set; }
        public byte? Codebtor1PolicyStatus { get; set; }

        // Codeudor 2
        public string Codebtor2Name { get; set; }
        public string Codebtor2InsurerName { get; set; }
        public string Codebtor2PolicyNumber { get; set; }
        public decimal? Codebtor2InsuredValue { get; set; }
        public DateTime? Codebtor2StartDate { get; set; }
        public DateTime? Codebtor2EndDate { get; set; }
        public byte? Codebtor2PolicyStatus { get; set; }

        // Codeudor 3
        public string Codebtor3Name { get; set; }
        public string Codebtor3InsurerName { get; set; }
        public string Codebtor3PolicyNumber { get; set; }
        public decimal? Codebtor3InsuredValue { get; set; }
        public DateTime? Codebtor3StartDate { get; set; }
        public DateTime? Codebtor3EndDate { get; set; }
        public byte? Codebtor3PolicyStatus { get; set; }

        public string VoBo { get; set; }
        public string AssignedAnalyst { get; set; }
        public byte Status { get; set; }
        public string Observation { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }

        /// <summary>
        /// Advertencias de validación (Severity == Advierte). Vacío si no hay advertencias.
        /// </summary>
        public List<EndorsementValidationResult> Warnings { get; set; } = new List<EndorsementValidationResult>();
    }
}
