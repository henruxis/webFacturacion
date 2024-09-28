using ERPWebAPI.Class;
using ERPWebAPI.Class.Response;
using ERPWebAPI.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ERPWebAPI.Controllers
{
    [Route("api/Producto/[action]")]
    [ApiController]
    public class ApiProductoController : ControllerBase
    {

        private readonly ProductoRepository db;

        private readonly IConfiguration configuration;

        public ApiProductoController(IConfiguration conf)
        {
            this.configuration = conf;
            this.db = new ProductoRepository(this.configuration);
        }


        [HttpGet("GetProducto")]
        public GeneralResponse GetProducto()
        {
            GeneralResponse dtresp = this.db.GetConsulta();

            return dtresp;
        }

        [HttpPost("")]
        public GeneralResponse Save(RequestProducto req)
        {
            GeneralResponse dtresp;

            if ((req.id == 0) || (req.id == null))
            {
                dtresp = this.db.InsertProducto(req);
            }
            else
            {
                dtresp = this.db.UpdateProducto(req);
            }

            return dtresp;
        }

    }
}
