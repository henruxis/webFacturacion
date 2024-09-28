using System.ComponentModel.DataAnnotations;

namespace ERPWebAPI.Models
{
    public class Cliente
    {

        public int id { get; set; } = 0;

        public string nombre { get; set; } = string.Empty;
        
        public string direccion { get; set; } = string.Empty;

        public string telefono { get; set; } = string.Empty;

        public string correo { get; set; } = string.Empty;


        
    }
}
