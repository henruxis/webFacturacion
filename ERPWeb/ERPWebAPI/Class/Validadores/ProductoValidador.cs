using System.ComponentModel.DataAnnotations;

namespace ERPWebAPI.Class.Validadores
{
    public class ArticuloValidador
    {

        public int id { get; set; } = 0;

        public string codigo { get; set; } = string.Empty;

        public string descripcion { get; set; } = string.Empty;

        public decimal precioUnitario { get; set; } = decimal.Zero;

        public int stockActual { get; set; }


        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var (valid, result) = GetVaslidationStatus(codigo, descripcion,precioUnitario);

            if (!valid)
            {
                yield return result;
            }
        }

        private (bool, ValidationResult) GetVaslidationStatus(string codigo, string descripcion, decimal precioUnitario)
        {
            if (string.IsNullOrWhiteSpace(codigo))
            {
                return (false, new ValidationResult("El nombre del articulo es requerido"));
            }

            if (string.IsNullOrWhiteSpace(descripcion))
            {
                return (false, new ValidationResult("La descripcion del articulo es requerida"));
            }

            if (precioUnitario == decimal.Zero)
            {
                return (false, new ValidationResult("El precio Unitario del articulo es requerida"));
            }

            return (true, null);
        }
    }
}
