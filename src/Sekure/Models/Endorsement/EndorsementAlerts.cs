namespace Sekure.Models.Endorsement
{
    /// <summary>
    /// Alertas del listado calculadas con todos los filtros activos (incluido el tab).
    /// Vencidos = Status 3, ProximosAVencer = Status 2, EnGestion = Status 5.
    /// </summary>
    public class EndorsementAlerts
    {
        public int Vencidos { get; set; }
        public int ProximosAVencer { get; set; }
        public int EnGestion { get; set; }
    }
}
