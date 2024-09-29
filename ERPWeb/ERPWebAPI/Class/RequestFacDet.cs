using ERPWebAPI.Models;

namespace ERPWebAPI.Class
{
    public class RequestFacDet
    {
        public int IdDetalle { get; set; }
        public int IdFactura { get; set; } // Clave foránea que apunta a FacturaCabecera
        public int IdArticulo { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal Subtotal { get; set; }

        // Relación con FacturaCabecera (muchos FacturaDetalle pertenecen a una FacturaCabecera)
        public FacturaCabecera FacturaCabecera { get; set; }
    }
}
