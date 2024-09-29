using ERPWebAPI.Class.Response;
using ERPWebAPI.Class;
using System.Collections;
using System.Data;
using ERPWebAPI.Models;

namespace ERPWebAPI.Data
{
    public class FacturaRepository
    {

        private readonly IConfiguration configuration;
        public FacturaRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
        }


        /// <summary>
        /// Devuelve la consulta por id de cliente
        /// </summary>
        /// <param name="reqCliente"></param>
        /// <returns></returns>
        public GeneralResponse GetFacturaById(int id)
        {

            GeneralResponse respuesta = new();


            var parametros = new Hashtable(){
                { "id_factura", id }};

            string sp = "sp_obtieneFacturaByParam";


            DataTable dt;
            Conexion oconexion = new(false, this.configuration["connectionStrings:ConnectionStringSQLBG"]);
            try
            {
                DataSet ds = oconexion.EjecutarProcedimiento(sp, parametros);
                if (ds != null)
                {
                    dt = ds.Tables[0];
                    if (dt.Rows.Count == 0)
                    {
                        respuesta.Data = null;
                        respuesta.Message = "NO EXISTEN REGISTROS DE CLIENTES";
                        respuesta.Success = -1;
                    }

                    else
                    {
                        respuesta.Data = ConvertDataTableToDictionary(dt);
                        respuesta.Message = "";
                        respuesta.Success = 0;
                    }
                }
                else
                {
                    respuesta.Message = oconexion.Mensaje_Error;
                    respuesta.Success = -1;
                }
            }
            catch (Exception ex)
            {
                respuesta.Message = ex.Message;

            }
            oconexion.Cerrarconexion();
            return respuesta;

        }


        private List<Dictionary<string, object>> ConvertDataTableToDictionary(DataTable dt)
        {
            var list = new List<Dictionary<string, object>>();

            foreach (DataRow row in dt.Rows)
            {
                var dict = new Dictionary<string, object>();

                foreach (DataColumn col in dt.Columns)
                {
                    dict[col.ColumnName] = row[col];
                }

                list.Add(dict);
            }

            return list;
        }


    }
}
