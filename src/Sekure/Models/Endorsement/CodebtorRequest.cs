using System;

namespace Sekure.Models.Endorsement
{
    /// <summary>
    /// Datos de un codeudor en la petición de alta o edición de endoso.
    /// Un codeudor se considera "presente" cuando Name no es null ni vacío.
    /// Solo aplica a Vida Deudor Hipotecario (EndorsementType=1, ProductType=2).
    /// </summary>
    public class CodebtorRequest
    {
        public string Name { get; set; }
        public string Document { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string InsurerName { get; set; }
        public string PolicyNumber { get; set; }
        public decimal? InsuredValue { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        /// <summary>
        /// Devuelve true si el codeudor está presente (tiene nombre).
        /// </summary>
        public bool IsPresent => !string.IsNullOrWhiteSpace(Name);
    }
}
