using ERPWebAPI.Models;
using System.ComponentModel.DataAnnotations;

namespace ERPWebAPI.Class.Validadores
{
    public class FacDetValidador : IValidatableObject
    {
        public int IdDetalle { get; set; }
        public int IdFactura { get; set; } // Clave foránea que apunta a FacturaCabecera
        public int IdArticulo { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal Subtotal { get; set; }

        // Relación con FacturaCabecera (muchos FacturaDetalle pertenecen a una FacturaCabecera)
        public FacturaCabecera FacturaCabecera { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var (valid, result) = GetValidationStatus(IdFactura, IdArticulo, Cantidad, PrecioUnitario);

            if (!valid)
            {
                yield return result;
            }
        }

        private (bool, ValidationResult) GetValidationStatus(int idFactura, int idArticulo, int cantidad, decimal precioUnitario)
        {
            if (idFactura == 0)
            {
                return (false, new ValidationResult("El Id de factura es requerido."));
            }

            if (idArticulo == 0)
            {
                return (false, new ValidationResult("El Id de artículo es requerido."));
            }

            if (cantidad <= 0)
            {
                return (false, new ValidationResult("La cantidad debe ser mayor que 0.00"));
            }

            if (precioUnitario <= 0)
            {
                return (false, new ValidationResult("El precio unitario debe ser mayor que 0.00"));
            }

            return (true, null);
        }

    }
}
