using System.ComponentModel.DataAnnotations;

namespace ERPWebAPI.Class
{
    public class RequestLogin
    {
        public string usuario { get; set; } = string.Empty;
        public string contraseña { get; set; } = string.Empty;

    }
}
