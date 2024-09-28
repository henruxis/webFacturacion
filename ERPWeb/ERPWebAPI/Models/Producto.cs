namespace ERPWebAPI.Models
{
    public class Articulo
    {

        public int id { get; set; } = 0;

        public string nombre { get; set; } = string.Empty;

        public string descripcion { get; set; } = string.Empty;

        public decimal precioUnitario { get; set; }

        public int stockActual { get; set; }


    }
}
