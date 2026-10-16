namespace Sekure.Models.Endorsement
{
    /// <summary>
    /// Conteo de endosos por tab, calculado con todos los filtros excepto el ProductType
    /// para que ambos contadores se actualicen al cambiar cualquier otro filtro.
    /// </summary>
    public class EndorsementTabCounts
    {
        public int Consumo { get; set; }
        public int Hipotecario { get; set; }
    }
}
