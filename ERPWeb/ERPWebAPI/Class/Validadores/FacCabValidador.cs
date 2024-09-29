using System.ComponentModel.DataAnnotations;

namespace ERPWebAPI.Class.Validadores
{
    public class FacCabValidador : IValidatableObject
    {
        public int IdFactura { get; set; } = 0;
        public int IdCliente { get; set; } = 0;
        public int IdLocal { get; set; } = 0;
        public int IdVendedor { get; set; } = 0;
        //public DateTime FechaEmision { get; set; }
        public decimal TotalFactura { get; set; }
        public int UsuarioId { get; set; } = 0;

        public int idFormaPago { get; set; } = 0;


        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var (valid, result) = GetVaslidationStatus(IdCliente, IdVendedor, idFormaPago);

            if (!valid)
            {
                yield return result;
            }
        }


        private (bool, ValidationResult) GetVaslidationStatus(int IdCliente, int IdVendedor, int idFormaPago)
        {
            if (IdCliente == 0)
            {
                return (false, new ValidationResult("El Id cliente es requerido"));
            }

            if (IdVendedor == 0)
            {
                return (false, new ValidationResult("La Id vendedor es requerida"));
            }

            if (idFormaPago == 0)
            {
                return (false, new ValidationResult("La forma de pago es requerida"));
            }

            return (true, null);
        }

    }
}
