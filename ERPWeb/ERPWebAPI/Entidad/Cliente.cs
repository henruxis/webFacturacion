using System.ComponentModel.DataAnnotations;

namespace ERPWebAPI.Entidad
{
    public class Cliente
    {
        public string nombre { get; set; }
        
        public string direccion { get; set; }

        public string telefono { get; set; }

        public string correo { get; set; }


        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var (valid, result) = GetVaslidationStatus(nombre, direccion,correo);

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

            if (!string.IsNullOrWhiteSpace(nombre) && string.IsNullOrWhiteSpace(direccion))
            {
                return (false, new ValidationResult("La direccion es requerida"));
            }

            if (!string.IsNullOrWhiteSpace(direccion) && string.IsNullOrWhiteSpace(correo))
            {
                return (false, new ValidationResult("El email es requerido"));
            }

            return (true, null);
        }
    }
}
