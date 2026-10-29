using System;
using System.Collections.Generic;

namespace Sekure.Models.Endorsement
{
    /// <summary>
    /// Respuesta del GET /Endorsement/{id}/detail.
    /// </summary>
    public class EndorsementDetailResponse
    {
        // ── Datos del endoso y crédito ────────────────────────────────────────
        public Guid Id { get; set; }
        public string CreditNumber { get; set; }
        public byte ProductType { get; set; }
        public byte EndorsementType { get; set; }
        public DateTime DisbursementDate { get; set; }
        public DateTime CreditEndDate { get; set; }
        public decimal? OutstandingBalance { get; set; }
        public bool IsRenewal { get; set; }
        public string VoBo { get; set; }
        public string AssignedAnalyst { get; set; }
        public string WithdrawalReason { get; set; }
        public string RetentionManagement { get; set; }
        public byte Status { get; set; }

        // ── Datos del cliente (deudor principal) ──────────────────────────────
        public string ClientName { get; set; }
        public string ClientDocument { get; set; }
        public string ClientEmail { get; set; }
        public string ClientPhone { get; set; }

        // ── Personas con su póliza y estado (deudor/inmueble + codeudores presentes) ──
        public List<EndorsementPersonDetail> Persons { get; set; } = new List<EndorsementPersonDetail>();

        // ── Alertas activas (personas en Vencido, ProximoAVencer o GestionDelCliente) ──
        public List<EndorsementPersonAlert> ActiveAlerts { get; set; } = new List<EndorsementPersonAlert>();

        // ── Línea de tiempo (no eliminados, orden CreatedAt desc) ─────────────
        public List<EndorsementTimelineEvent> Timeline { get; set; } = new List<EndorsementTimelineEvent>();

        // ── Recordatorios enviados (RecordatorioCliente) ──────────────────────
        public List<EndorsementReminderSent> Reminders { get; set; } = new List<EndorsementReminderSent>();

        // ── Documentos activos del crédito (todos los endosos del mismo crédito) ──
        public List<EndorsementDocumentItem> Documents { get; set; } = new List<EndorsementDocumentItem>();
    }

    /// <summary>
    /// Alerta activa: persona en estado que requiere atención.
    /// </summary>
    public class EndorsementPersonAlert
    {
        /// <summary>0 = Deudor/Inmueble, 1-3 = Codeudor N.</summary>
        public byte Role { get; set; }

        /// <summary>2 = ProximoAVencer, 3 = Vencido, 5 = GestionDelCliente.</summary>
        public byte Status { get; set; }
    }
}
