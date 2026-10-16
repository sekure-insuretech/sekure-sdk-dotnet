using System;

namespace Sekure.Models.Endorsement
{
    /// <summary>
    /// Evento de la línea de tiempo del endoso.
    /// PayloadJson NO se expone crudo; solo se devuelven los campos tipificados.
    /// </summary>
    public class EndorsementTimelineEvent
    {
        /// <summary>Nombre del enum EndorsementEventType (ej: "DocumentoCargado").</summary>
        public string EventType { get; set; }

        public string Description { get; set; }

        /// <summary>0 = Deudor/Inmueble, 1-3 = Codeudor N; null = evento a nivel de endoso.</summary>
        public byte? PersonRole { get; set; }

        public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
