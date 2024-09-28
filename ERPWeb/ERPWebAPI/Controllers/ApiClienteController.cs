using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using ERPWebAPI.Models;
using ERPWebAPI.Class.Response;
using ERPWebAPI.Class;
using ERPWebAPI.Data;
using Microsoft.IdentityModel.Tokens;

namespace ERPWebAPI.Controllers
{
    [Route("api/cliente/[action]")]
    [ApiController]
    public class ApiClienteController : ControllerBase
    {

        private readonly ClienteRepository db;

        private readonly IConfiguration configuration;

        public ApiClienteController(IConfiguration conf)
        {
            this.configuration = conf;
            this.db = new ClienteRepository(this.configuration);
        }

        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}


        [HttpGet("GetCliente")]
        public GeneralResponse GetCliente()
        {
            GeneralResponse dtresp = this.db.GetConsulta();

            return dtresp;
        }

        [HttpPost("ID")]
        public GeneralResponse GetClienteID(RequestCliente req)
        {
            GeneralResponse dtresp = this.db.GetConsultabyID(req);

            return dtresp;
        }

        [HttpDelete("ID")]
        public GeneralResponse Delete(RequestCliente req)
        {
            GeneralResponse dtresp = this.db.DeleteClientbyID(req.id);

            return dtresp;
        }

        [HttpPost("")]
        public GeneralResponse Save(RequestCliente req)
        {
            GeneralResponse dtresp;

            if ((req.id == 0) || (req.id == null))
            {
                dtresp = this.db.InsertClient(req);
            }
            else
            {
                dtresp = this.db.UpdateClient(req);
            }

            return dtresp;
        }


    }
}
