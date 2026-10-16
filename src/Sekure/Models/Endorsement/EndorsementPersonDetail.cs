using System;

namespace Sekure.Models.Endorsement
{
    /// <summary>
    /// Detalle de una persona (deudor/inmueble o codeudor) en el GET /Endorsement/{id}/detail.
    /// Solo se incluye si la persona tiene Document relleno (es "presente").
    /// </summary>
    public class EndorsementPersonDetail
    {
        /// <summary>0 = Deudor/Inmueble, 1-3 = Codeudor N.</summary>
        public byte Role { get; set; }

        public string Name { get; set; }
        public string Document { get; set; }
        public string InsurerName { get; set; }
        public string PolicyNumber { get; set; }
        public decimal InsuredValue { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public byte Status { get; set; }
    }
}
