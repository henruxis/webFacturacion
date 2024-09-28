using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using ERPWebAPI.Models;

namespace ERPWebAPI.Controllers
{
    [Route("api/cliente")]
    [ApiController]
    public class ApiClienteController : ControllerBase
    {
        [HttpGet]

        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // POST api/<ConsultController>
        //[HttpGet("GetCliente")]
        //public async Task<string> GetCliente()
        //{
        //    return await _repository.GetConsulta(param);
        //}
    }
}
