namespace ERPWebAPI.Class
{
    public class RequestCliente
    {
        public int id { get; set; } = 0;

        public string nombre { get; set; } = string.Empty;

        public string direccion { get; set; } = string.Empty;

        public string telefono { get; set; } = string.Empty;

        public string correo { get; set; } = string.Empty;

        public int estado { get; set; } = 0;

    }
}
