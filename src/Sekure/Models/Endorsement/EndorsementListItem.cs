using System;
using System.Collections.Generic;

namespace Sekure.Models.Endorsement
{
    /// <summary>
    /// Una fila del listado de endosos (POST /Endorsement/search).
    /// Un hipotecario con VD e I&amp;T son dos filas distintas (no se agrupan).
    /// </summary>
    public class EndorsementListItem
    {
        public Guid Id { get; set; }
        public string CreditNumber { get; set; }
        public string ClientName { get; set; }
        public string ClientDocument { get; set; }
        public DateTime DisbursementDate { get; set; }

        /// <summary>1 = Vida Deudor, 2 = I&amp;T.</summary>
        public byte EndorsementType { get; set; }

        /// <summary>1 (principal) + codeudores que tienen PolicyNumber relleno.</summary>
        public int PolicyCount { get; set; }

        /// <summary>Fin de vigencia de la póliza del deudor/inmueble.</summary>
        public DateTime EndDate { get; set; }

        /// <summary>Vencimiento del crédito.</summary>
        public DateTime CreditEndDate { get; set; }

        public string InsurerName { get; set; }
        public decimal InsuredValue { get; set; }
        public byte Status { get; set; }
        public string VoBo { get; set; }
        public string AssignedAnalyst { get; set; }

        /// <summary>Eventos DocumentoCargado con DocumentPhase=1 y no eliminados.</summary>
        public int DocumentCount { get; set; }

        public bool IsRenewal { get; set; }

        /// <summary>Codeudores presentes (con Document relleno), para la subtabla.</summary>
        public List<EndorsementCodebtorItem> Codebtors { get; set; } = new List<EndorsementCodebtorItem>();
    }
}
