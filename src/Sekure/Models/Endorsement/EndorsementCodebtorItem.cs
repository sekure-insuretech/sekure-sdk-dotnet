using System;

namespace Sekure.Models.Endorsement
{
    /// <summary>
    /// Codeudor presente en la subtabla del listado de endosos.
    /// Solo se incluye si el codeudor tiene Document relleno.
    /// </summary>
    public class EndorsementCodebtorItem
    {
        /// <summary>1, 2 o 3.</summary>
        public byte Role { get; set; }

        public string Name { get; set; }
        public string Document { get; set; }
        public string InsurerName { get; set; }
        public string PolicyNumber { get; set; }
        public decimal? InsuredValue { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public byte? Status { get; set; }
    }
}
