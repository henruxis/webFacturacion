using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using ERPWebAPI.Class.Response;
using ERPWebAPI.Data;
using ERPWebAPI.Class;
using Newtonsoft.Json;

namespace ERPWebAPI.Controllers
{
    
    [Route("api/login")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly Login db;
        private readonly IConfiguration configuration;

        public LoginController(IConfiguration conf)
        {
            this.configuration = conf;
            this.db = new Login(this.configuration);
        }

        [HttpPost("InicioSesion")]
        public GeneralResponse Login(RequestLogin req)
        {

            GeneralResponse dtresp = this.db.LoginSesion(req);

            return dtresp;
        }

    }
}
