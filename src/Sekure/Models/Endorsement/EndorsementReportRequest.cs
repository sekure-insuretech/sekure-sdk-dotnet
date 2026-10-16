namespace Sekure.Models.Endorsement
{
    /// <summary>
    /// Cuerpo del POST /Endorsement/reports y POST /Endorsement/reports/export (IOS-12A).
    /// </summary>
    public class EndorsementReportRequest
    {
        /// <summary>Ano del periodo a reportar. Obligatorio.</summary>
        public int Year { get; set; }

        /// <summary>Mes del periodo (1-12). Opcional: si se omite, aplica a todo el ano.</summary>
        public int? Month { get; set; }

        /// <summary>1 = Consumo, 2 = Hipotecario. Opcional.</summary>
        public byte? ProductType { get; set; }

        /// <summary>1 = Vida Deudor, 2 = Incendio y Terremoto. Opcional.</summary>
        public byte? EndorsementType { get; set; }
    }
}
