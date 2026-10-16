using System;

namespace Sekure.Models.Endorsement
{
    /// <summary>Respuesta al cargar un documento en un endoso (POST /Endorsement/{id}/documents).</summary>
    public class UploadDocumentResponse
    {
        public Guid EventId { get; set; }
        public string VisibleName { get; set; }
        public string DocumentType { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid PublicId { get; set; }
    }

    /// <summary>Ítem del listado de documentos activos del crédito (GET /Endorsement/{id}/documents).</summary>
    public class DocumentListItemResponse
    {
        public string VisibleName { get; set; }
        public string DocumentType { get; set; }
        public byte? PersonRole { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public Guid PublicId { get; set; }
    }
}
