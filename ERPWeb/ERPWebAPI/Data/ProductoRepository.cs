using ERPWebAPI.Class.Response;
using ERPWebAPI.Class;
using System.Collections;
using System.Data;
using ERPWebAPI.Class.Validadores;
using System.ComponentModel.DataAnnotations;

namespace ERPWebAPI.Data
{
    public class ProductoRepository
    {

        private readonly IConfiguration configuration;
        public ProductoRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        /// <summary>
        /// Devuelve la lista de Productos
        /// </summary>
        /// <returns></returns>
        public GeneralResponse GetConsulta()
        {

            GeneralResponse respuesta = new();


            string sp = "sp_obtieneProducto";
            var parametros = new Hashtable() { };

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
                        respuesta.Message = "NO EXISTEN REGISTROS DE PRODUCTOS";
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



        
        /// <summary>
        /// Insercion de producto
        /// </summary>
        /// <param name="reqProducto"></param>
        /// <returns></returns>
        public GeneralResponse InsertProducto(RequestProducto reqProducto)
        {
            #region <<validacion de campos>>
            var credentials = new ArticuloValidador
            {
                codigo = reqProducto.codigo,
                descripcion = reqProducto.descripcion,
                precioUnitario = reqProducto.precioUnitario
            };

            var validationResults = credentials.Validate(new ValidationContext(credentials));

            GeneralResponse respuesta = new();

            if (validationResults.Any())
            {
                respuesta.Data = null;
                respuesta.Message = string.Join("; ", validationResults.Select(v => v.ErrorMessage));
                respuesta.Success = -1;
                return respuesta;
            }
            #endregion

            var parametros = new Hashtable(){
                { "codigo_articulo", reqProducto.codigo },
                { "descripcion_articulo", reqProducto.descripcion },
                { "precio_unitario", reqProducto.precioUnitario },
                { "stock_actual", reqProducto.stockActual }};

            string sp = "sp_InsertarProducto";

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
                        respuesta.Message = "NO EXISTEN REGISTROS DE PRODUCTOS";
                        respuesta.Success = -1;
                    }

                    else
                    {
                        respuesta.Data = null;
                        respuesta.Message = "REGISTRO INSERTADO EXITOSAMENTE";
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



        /// <summary>
        /// actualiza datos del producto/ articulo
        /// </summary>
        /// <param name="reqCliente"></param>
        /// <returns></returns>
        public GeneralResponse UpdateProducto(RequestProducto reqProducto)
        {
            #region <<validacion de campos>>
            var credentials = new ArticuloValidador
            {
                codigo = reqProducto.codigo,
                descripcion = reqProducto.descripcion,
                precioUnitario = reqProducto.precioUnitario
            };

            var validationResults = credentials.Validate(new ValidationContext(credentials));

            GeneralResponse respuesta = new();

            if (validationResults.Any())
            {
                respuesta.Data = null;
                respuesta.Message = string.Join("; ", validationResults.Select(v => v.ErrorMessage));
                respuesta.Success = -1;
                return respuesta;
            }
            #endregion

            var parametros = new Hashtable(){
                { "codigo_articulo", reqProducto.codigo },
                { "descripcion_articulo", reqProducto.descripcion },
                { "precio_unitario", reqProducto.precioUnitario },
                { "stock_actual", reqProducto.stockActual }};

            string sp = "sp_actualizarProductoByID";

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
                        respuesta.Message = "NO EXISTEN REGISTROS DE PRODUCTO";
                        respuesta.Success = -1;
                    }

                    else
                    {
                        respuesta.Data = null;
                        respuesta.Message = "REGISTRO ACTUALIZADO EXITOSAMENTE";
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
