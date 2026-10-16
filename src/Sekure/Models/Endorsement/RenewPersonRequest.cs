using System;

namespace Sekure.Models.Endorsement
{
    /// <summary>
    /// Datos de la nueva vigencia para renovar una persona en un endoso.
    /// Reutilizado por IOS-04 (renovación implícita en Nuevo) y IOS-06 (renovación explícita).
    /// </summary>
    public class RenewPersonRequest
    {
        /// <summary>
        /// Rol de la persona a renovar: 0 = Deudor/inmueble, 1 = Codeudor 1, 2 = Codeudor 2, 3 = Codeudor 3.
        /// </summary>
        public byte PersonRole { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string InsurerName { get; set; }
        public string PolicyNumber { get; set; }
        public decimal InsuredValue { get; set; }
    }
}
