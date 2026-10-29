using System;

namespace Sekure.Models.Endorsement
{
    /// <summary>
    /// DTO de entrada para Crear o Editar un endoso.
    /// Lista blanca: solo los campos que el analista puede informar.
    /// Campos excluidos: Id, TenantId, Status, PolicyStatus, CodebtorNPolicyStatus,
    /// AssignedAnalyst, CreatedBy/At, UpdatedBy/At, IsDeleted, UploadToken*/UploadLink*.
    /// Máximo 3 codeudores (Codebtor1..3). No existe Codebtor4.
    /// </summary>
    public class EndorsementUpsertRequest
    {
        // ── Producto ──────────────────────────────────────────────────────────
        /// <summary>1 Consumo, 2 Hipotecario</summary>
        public byte ProductType { get; set; }

        /// <summary>1 Vida Deudor, 2 Incendio y Terremoto</summary>
        public byte EndorsementType { get; set; }

        // ── Datos del crédito ─────────────────────────────────────────────────
        public string CreditNumber { get; set; }
        public DateTime DisbursementDate { get; set; }
        public DateTime CreditEndDate { get; set; }
        public DateTime CollectiveWithdrawalDate { get; set; }
        public decimal CollectivePremium { get; set; }
        public short InitialYear { get; set; }

        /// <summary>Saldo insoluto: obligatorio en Vida Deudor, null en I&amp;T.</summary>
        public decimal? OutstandingBalance { get; set; }

        // ── Cliente (deudor / inmueble) ────────────────────────────────────────
        public string ClientName { get; set; }
        public string ClientDocument { get; set; }
        public string ClientPhone { get; set; }
        public string ClientEmail { get; set; }

        // ── Póliza del deudor / inmueble ──────────────────────────────────────
        public string InsurerName { get; set; }
        public string PolicyNumber { get; set; }
        public decimal InsuredValue { get; set; }

        /// <summary>Valor del inmueble: obligatorio en I&amp;T, null en Vida Deudor.</summary>
        public decimal? PropertyValue { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        // ── Gestión ───────────────────────────────────────────────────────────
        public string VoBo { get; set; }
        public DateTime CorbancoDate { get; set; }
        public string Manager { get; set; }
        public string Observation { get; set; }
        public string WithdrawalReason { get; set; }
        public string RetentionManagement { get; set; }

        // ── Bandera de renovación ─────────────────────────────────────────────
        public bool IsRenewal { get; set; }

        // ── Codeudores (máximo 3; solo en VD Hipotecario) ────────────────────
        public CodebtorRequest Codebtor1 { get; set; }
        public CodebtorRequest Codebtor2 { get; set; }
        public CodebtorRequest Codebtor3 { get; set; }
    }
}
