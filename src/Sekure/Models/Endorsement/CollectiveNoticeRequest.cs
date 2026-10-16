namespace Sekure.Models.Endorsement
{
    /// <summary>
    /// Cuerpo del POST Endorsement/{id}/collective-notice.
    /// El asunto y el mensaje son editados por el analista antes de enviar.
    /// </summary>
    public class CollectiveNoticeRequest
    {
        /// <summary>Rol de la persona: 0 = Deudor, 1-3 = Codeudor.</summary>
        public byte Role { get; set; }

        /// <summary>Asunto del correo editado por el analista. Máximo 200 caracteres.</summary>
        public string Subject { get; set; }

        /// <summary>
        /// Texto del mensaje escrito por el analista. Máximo 5000 caracteres.
        /// Se inserta en el cuerpo HTML codificado como HTML (saltos de línea → &lt;br&gt;).
        /// </summary>
        public string Message { get; set; }
    }
}
