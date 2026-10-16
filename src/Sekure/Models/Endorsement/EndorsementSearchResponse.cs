using System.Collections.Generic;

namespace Sekure.Models.Endorsement
{
    /// <summary>
    /// Respuesta del POST /Endorsement/search.
    /// </summary>
    public class EndorsementSearchResponse
    {
        public List<EndorsementListItem> Items { get; set; } = new List<EndorsementListItem>();
        public int Total { get; set; }

        /// <summary>Conteos por tab con todos los filtros excepto ProductType.</summary>
        public EndorsementTabCounts TabCounts { get; set; }

        /// <summary>Alertas con todos los filtros activos (incluido el tab).</summary>
        public EndorsementAlerts Alerts { get; set; }
    }
}
