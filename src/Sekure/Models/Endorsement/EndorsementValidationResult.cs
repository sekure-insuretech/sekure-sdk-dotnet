using System;

namespace Sekure.Models.Endorsement
{
    /// <summary>
    /// Resultado de una regla de validación de política de aceptación.
    /// Cada resultado tiene un código estable (END-B01..END-B09, END-W01..END-W03)
    /// que el SaaS y las HU siguientes usan para identificar la regla.
    /// </summary>
    public class EndorsementValidationResult
    {
        /// <summary>
        /// Código estable de la regla. Ejemplo: "END-B01", "END-W01".
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Gravedad: Bloquea (impide guardar) o Advierte (permite guardar con aviso).
        /// </summary>
        public ValidationSeverity Severity { get; set; }

        /// <summary>
        /// Rol de la persona afectada: "Deudor", "Codeudor 1", "Codeudor 2", "Codeudor 3".
        /// Null cuando la regla aplica al endoso en general y no a una persona específica.
        /// NUNCA contiene cédulas, nombres, correos ni valores.
        /// </summary>
        public string PersonAffected { get; set; }

        /// <summary>
        /// Mensaje descriptivo con la política incumplida y la acción para corregirla.
        /// No incluye cédulas, nombres, correos ni valores monetarios.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Solo poblado en END-W01: Id del endoso existente que el handler de Registro
        /// debe convertir en renovación en lugar de crear uno nuevo.
        /// </summary>
        public Guid? ExistingEndorsementId { get; set; }
    }
}
