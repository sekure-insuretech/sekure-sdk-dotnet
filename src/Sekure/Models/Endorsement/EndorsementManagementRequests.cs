using System.Collections.Generic;

namespace Sekure.Models.Endorsement
{
    /// <summary>
    /// Cuerpo para rechazar documentos de una persona (IOS-06 POST .../reject).
    /// Solo aplica si la persona está En Trámite (4); si no → 409.
    /// </summary>
    public class RejectPersonRequest
    {
        /// <summary>Rol de la persona: 0 Deudor/Inmueble, 1-3 Codeudor.</summary>
        public byte PersonRole { get; set; }

        /// <summary>Motivo del rechazo. Obligatorio, entre 5 y 500 caracteres.</summary>
        public string Reason { get; set; }
    }

    /// <summary>
    /// Cuerpo para marcar Gestión del Cliente (IOS-06 POST .../client-management).
    /// </summary>
    public class ClientManagementRequest
    {
        /// <summary>Rol de la persona: 0 Deudor/Inmueble, 1-3 Codeudor.</summary>
        public byte PersonRole { get; set; }

        /// <summary>Canal de contacto: 1 Llamada, 2 Correo.</summary>
        public byte Channel { get; set; }

        /// <summary>Nota obligatoria, máximo 1000 caracteres.</summary>
        public string Note { get; set; }
    }

    /// <summary>
    /// Cuerpo para marcar En Trámite manualmente (IOS-06 POST .../in-process).
    /// </summary>
    public class InProcessRequest
    {
        /// <summary>Rol de la persona: 0 Deudor/Inmueble, 1-3 Codeudor.</summary>
        public byte PersonRole { get; set; }

        /// <summary>Nota obligatoria, máximo 1000 caracteres.</summary>
        public string Note { get; set; }
    }

    /// <summary>
    /// Cuerpo para reasignar analista (IOS-06 POST .../reassign).
    /// </summary>
    public class ReassignRequest
    {
        /// <summary>Correo electrónico del nuevo analista. Formato email, máximo 256 caracteres.</summary>
        public string NewAnalyst { get; set; }
    }

    /// <summary>
    /// Cuerpo para agregar nota manual (IOS-06 POST .../notes).
    /// </summary>
    public class NoteRequest
    {
        /// <summary>Texto de la nota. Obligatorio, máximo 2000 caracteres.</summary>
        public string Text { get; set; }
    }

    /// <summary>
    /// Cuerpo para recordatorio manual (IOS-08C POST .../reminders).
    /// </summary>
    public class ReminderRequest
    {
        /// <summary>Rol de la persona: 0 Deudor/Inmueble, 1-3 Codeudor.</summary>
        public byte Role { get; set; }
    }

    /// <summary>
    /// Cuerpo para renovación explícita desde el módulo (IOS-06 POST .../renew).
    /// Delega en IEndorsementService.RenewAsync.
    /// </summary>
    public class RenewEndorsementRequest
    {
        /// <summary>Personas a renovar con sus nuevas vigencias.</summary>
        public List<RenewPersonRequest> Persons { get; set; }

        /// <summary>Saldo insoluto actualizado. Solo aplica en Vida Deudor.</summary>
        public decimal? OutstandingBalance { get; set; }

        /// <summary>VoBo actualizado.</summary>
        public string VoBo { get; set; }
    }
}
