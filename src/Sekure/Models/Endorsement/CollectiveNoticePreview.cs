namespace Sekure.Models.Endorsement
{
    /// <summary>
    /// Respuesta del GET Endorsement/{id}/collective-notice?role=N.
    /// Contiene el asunto y el cuerpo HTML construidos a partir de la plantilla,
    /// con los marcadores reemplazados por los datos reales del endoso.
    /// </summary>
    public class CollectiveNoticePreview
    {
        /// <summary>Asunto propuesto (extraído de la plantilla configurada).</summary>
        public string Subject { get; set; }

        /// <summary>Cuerpo HTML con marcadores reemplazados.</summary>
        public string Body { get; set; }
    }
}
