using System.ComponentModel.DataAnnotations;

namespace ERPWebAPI.Class.Validadores
{
    public class ClienteValidador : IValidatableObject
    {

        public int id { get; set; } = 0;

        public string nombre { get; set; } = string.Empty;

        public string direccion { get; set; } = string.Empty;

        public string telefono { get; set; } = string.Empty;

        public string correo { get; set; } = string.Empty;

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var (valid, result) = GetVaslidationStatus(nombre, direccion, correo);

            if (!valid)
            {
                yield return result;
            }
        }

        private (bool, ValidationResult) GetVaslidationStatus(string nombre, string direccion, string correo)
        {
            if (string.IsNullOrWhiteSpace(nombre))
            {
                return (false, new ValidationResult("El nombre es requerido"));
            }

            if (string.IsNullOrWhiteSpace(direccion))
            {
                return (false, new ValidationResult("La direccion es requerida"));
            }

            if (string.IsNullOrWhiteSpace(correo))
            {
                return (false, new ValidationResult("El email es requerido"));
            }

            return (true, null);
        }

    }
}
