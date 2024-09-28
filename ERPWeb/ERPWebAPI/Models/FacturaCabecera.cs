namespace ERPWebAPI.Models
{
    public class FacturaCabecera
    {
        public int IdFactura { get; set; }
        public int IdCliente { get; set; }
        public int IdLocal { get; set; }
        public int IdVendedor { get; set; }
        public DateTime FechaEmision { get; set; }
        public decimal TotalFactura { get; set; }
        public int UsuarioId { get; set; }

        public int idFormaPago { get; set; }

        // Relación con FacturaDetalle (una FacturaCabecera tiene muchos FacturaDetalle)
        public ICollection<FacturaDetalle> FacturaDetalles { get; set; }
    }
}
