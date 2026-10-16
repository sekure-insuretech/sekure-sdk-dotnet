namespace Sekure.Models.Endorsement
{
    /// <summary>
    /// Cuerpo del POST /Endorsement/export (IOS-11B).
    /// Mismos filtros que EndorsementSearchRequest pero sin Skip/Take.
    /// ProductType y EndorsementType son obligatorios: definen qué plantilla se genera.
    /// La cédula no puede ir en la URL (quedaría en logs de acceso), por eso la exportación es POST.
    /// </summary>
    public class EndorsementExportRequest
    {
        /// <summary>Requerido: 1 = Consumo, 2 = Hipotecario (tab activo).</summary>
        public byte ProductType { get; set; }

        /// <summary>Requerido: 1 = Vida Deudor, 2 = Incendio y Terremoto.</summary>
        public byte? EndorsementType { get; set; }

        // ── Filtros opcionales combinables (mismos que Endorsement/search) ──────

        /// <summary>Nombre de aseguradora, contiene (case-insensitive). Máx 100 chars.</summary>
        public string InsurerContains { get; set; }

        /// <summary>Estado del endoso: 1 Vigente · 2 Próximo a Vencer · 3 Vencido · 4 En Trámite · 5 Gestión del Cliente.</summary>
        public byte? Status { get; set; }

        /// <summary>true = Renovación, false = Nuevo.</summary>
        public bool? IsRenewal { get; set; }

        /// <summary>Año de fin de vigencia de la póliza principal (2000-2100).</summary>
        public int? PolicyEndYear { get; set; }

        /// <summary>Mes de fin de vigencia de la póliza principal (1-12).</summary>
        public int? PolicyEndMonth { get; set; }

        /// <summary>Año de vencimiento del crédito (2000-2100).</summary>
        public int? CreditEndYear { get; set; }

        /// <summary>Mes de vencimiento del crédito (1-12).</summary>
        public int? CreditEndMonth { get; set; }

        /// <summary>Cédula exacta; busca en ClientDocument y Codebtor1..3Document. Alfanumérico, máx 20 chars.</summary>
        public string Document { get; set; }

        /// <summary>Email del analista asignado (igualdad). Máx 256 chars.</summary>
        public string AssignedAnalyst { get; set; }
    }
}
