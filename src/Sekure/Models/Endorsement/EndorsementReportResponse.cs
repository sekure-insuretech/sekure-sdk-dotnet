using System.Collections.Generic;

namespace Sekure.Models.Endorsement
{
    /// <summary>
    /// Respuesta del POST /Endorsement/reports (IOS-12A).
    /// Contiene conteos por categoria y los 4 KPI gerenciales.
    /// </summary>
    public class EndorsementReportResponse
    {
        public int Year { get; set; }
        public int? Month { get; set; }
        public CountByStatusReport CountByStatus { get; set; }
        public RenovationReport Renovations { get; set; }
        public List<CountByGroupItem> CountByInsurer { get; set; }
        public List<CountByGroupItem> CountByEndorsementType { get; set; }
        public KpiReport Kpis { get; set; }
    }

    public class CountByStatusReport
    {
        public int Vigente { get; set; }
        public int ProximoAVencer { get; set; }
        public int Vencido { get; set; }
        public int EnTramite { get; set; }
        public int GestionCliente { get; set; }
        public int Total => Vigente + ProximoAVencer + Vencido + EnTramite + GestionCliente;
    }

    public class RenovationReport
    {
        /// <summary>Total de endosos con fecha de vencimiento en el periodo.</summary>
        public int TotalConVencimientoEnPeriodo { get; set; }

        /// <summary>Endosos que tienen al menos un evento Renovacion en el ciclo.</summary>
        public int RenovacionesGestionadas { get; set; }

        /// <summary>Endosos sin evento Renovacion en el ciclo (pendientes).</summary>
        public int RenovacionesPendientes { get; set; }
    }

    public class CountByGroupItem
    {
        public string Name { get; set; }
        public int Count { get; set; }
    }

    /// <summary>
    /// Los 4 KPI gerenciales de endosos Comfandi (IOS-12A).
    /// </summary>
    public class KpiReport
    {
        // ── KPI 1: Tasa de renovacion oportuna ────────────────────────────────

        /// <summary>Total endosos con EndDate en el periodo consultado.</summary>
        public int EndososConVencimientoEnPeriodo { get; set; }

        /// <summary>Documentacion recibida el dia del vencimiento o antes.</summary>
        public int RenovadosOportunamente { get; set; }

        /// <summary>Documentacion recibida hasta DiasGracia dias calendario despues del vencimiento.</summary>
        public int RenovadosEnPlazoGracia { get; set; }

        /// <summary>Documentacion recibida despues del plazo de gracia.</summary>
        public int RenovadosTardio { get; set; }

        /// <summary>Endosos sin documentacion recibida aun.</summary>
        public int RenovacionesPendientes { get; set; }

        /// <summary>Porcentaje oportuno sobre total (0-100). Null si no hay endosos en el periodo.</summary>
        public decimal? TasaRenovacionOportuna { get; set; }

        /// <summary>Dias de plazo de gracia aplicados (viene de Endosos_DiasAvisoInclusionColectiva).</summary>
        public int DiasGracia { get; set; }

        // ── KPI 2: Tiempo promedio de renovacion ──────────────────────────────

        /// <summary>Dias promedio entre primer recordatorio y paso a Vigente. Null si no hay ciclos cerrados.</summary>
        public decimal? TiempoPromedioRenovacion { get; set; }

        /// <summary>Ciclos cerrados (con Renovacion) en el periodo con ambas fechas disponibles.</summary>
        public int CiclosCerradosEnPeriodo { get; set; }

        // ── KPI 3: Endosos activos ────────────────────────────────────────────

        /// <summary>Endosos en estado Vigente (1) o Proximo a Vencer (2), no eliminados.</summary>
        public int EndososActivos { get; set; }

        // ── KPI 4: Vencidos sin gestion ───────────────────────────────────────

        /// <summary>
        /// Endosos en Vencido (3) sin documentos cargados y sin contacto registrado
        /// (ninguna gestion humana en el ciclo actual).
        /// </summary>
        public int VencidosSinGestion { get; set; }

        /// <summary>
        /// Descripcion de los eventos que definen la fecha de documentacion completa para KPI1.
        /// Solo se cuenta CarguePortal (EventType 11). El CambioEstado manual (EventType 3)
        /// a En Tramite no se incluye porque EndorsementHistory no almacena el estado destino
        /// en un campo dedicado; solo el texto libre de Description lo indicaria, lo que
        /// haria la consulta fragil ante cambios de redaccion.
        /// </summary>
        public string Kpi1Metodologia { get; set; }
    }
}
