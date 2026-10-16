namespace Sekure.Models.Endorsement
{
    /// <summary>
    /// Gravedad de un resultado de validación de política de aceptación.
    /// Bloquea impide guardar; Advierte permite guardar con advertencia al usuario.
    /// </summary>
    public enum ValidationSeverity
    {
        Bloquea = 1,
        Advierte = 2
    }
}
