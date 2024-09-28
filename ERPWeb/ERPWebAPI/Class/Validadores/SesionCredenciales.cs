using System.ComponentModel.DataAnnotations;

namespace ERPWebAPI.Class.Validadores
{
    public class SesionCredenciales : IValidatableObject
    {
        public string Usuario { get; set; } = string.Empty;
        public string Contraseña { get; set; } = string.Empty;
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var (valid, result) = GetVaslidationStatus(Usuario, Contraseña);

            if (!valid)
            {
                yield return result;
            }
        }

        private (bool, ValidationResult) GetVaslidationStatus(string usuario, string contraseña)
        {
            if (string.IsNullOrWhiteSpace(usuario))
            {
                return (false, new ValidationResult("El usuario es requerido"));
            }

            if (string.IsNullOrWhiteSpace(contraseña))
            {
                return (false, new ValidationResult("La contraseña es requerida"));
            }

            return (true, null);
        }


    }
}
