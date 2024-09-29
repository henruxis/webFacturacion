using ERPWebAPI.Class;
using ERPWebAPI.Class.Response;
using ERPWebAPI.Data;
using ERPWebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc; 
using Microsoft.EntityFrameworkCore;

namespace ERPWebAPI.Controllers
{
    [Route("api/Factura/[action]")]
    [ApiController]
    public class ApiFacturaController : ControllerBase
    {

        private readonly FacturaRepository db;

        private readonly IConfiguration configuration;

        public ApiFacturaController(IConfiguration conf)
        {
            this.configuration = conf;
            this.db = new FacturaRepository(this.configuration);
        }

        [HttpGet("{id}")]
        public GeneralResponse GetFacturaById(int id)
        {
            GeneralResponse dtresp = this.db.GetFacturaById(id);
            
            return dtresp;
        }

    }
}
