using System;

namespace Sekure.Models.Endorsement
{
    /// <summary>
    /// Recordatorio enviado al cliente (EventType = RecordatorioCliente).
    /// El destinatario se devuelve enmascarado (p.ej. j***@dominio.com).
    /// </summary>
    public class EndorsementReminderSent
    {
        public DateTime CreatedAt { get; set; }
        public string Channel { get; set; }

        /// <summary>0 = Deudor/Inmueble, 1-3 = Codeudor N.</summary>
        public byte? PersonRole { get; set; }

        /// <summary>Primer carácter visible + *** hasta @ + dominio completo.</summary>
        public string MaskedRecipient { get; set; }
    }
}
