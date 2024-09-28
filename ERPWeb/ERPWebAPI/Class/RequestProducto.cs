namespace ERPWebAPI.Class
{
    public class RequestProducto
    {
        public int id { get; set; } = 0;

        public string codigo { get; set; } = string.Empty;

        public string descripcion { get; set; } = string.Empty;

        public decimal precioUnitario { get; set; }

        public int stockActual { get; set; }
    }
}
