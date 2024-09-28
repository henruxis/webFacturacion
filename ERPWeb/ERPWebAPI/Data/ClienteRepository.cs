using ERPWebAPI.Class.Response;
using ERPWebAPI.Class.Validadores;
using ERPWebAPI.Class;
using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.Data;
using ERPWebAPI.Models;

namespace ERPWebAPI.Data
{
    public class ClienteRepository
    {

        private readonly IConfiguration configuration;
        public ClienteRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
        }


        /// <summary>
        /// Devuelve la lista de clientes activos
        /// </summary>
        /// <returns></returns>
        public GeneralResponse GetConsulta()
        {

            GeneralResponse respuesta = new();

            
            string sp = "sp_obtieneClientes";
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


        /// <summary>
        /// Devuelve la consulta por id de cliente
        /// </summary>
        /// <param name="reqCliente"></param>
        /// <returns></returns>
        public GeneralResponse GetConsultabyID(RequestCliente reqCliente)
        {

            GeneralResponse respuesta = new();


            var parametros = new Hashtable(){
                { "id_cliente", reqCliente.id }};

            string sp = "sp_obtieneClienteByID";
            

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


        /// <summary>
        /// Se anula cliente por ID
        /// </summary>
        /// <param name="idCliente"></param>
        /// <returns></returns>
        public GeneralResponse DeleteClientbyID(int idCliente)
        {

            GeneralResponse respuesta = new();
            var parametros = new Hashtable(){
                { "id_cliente", idCliente }};

            string sp = "sp_anularClienteByID";

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
                        respuesta.Data = null;
                        respuesta.Message = "REGISTRO ANULADO EXITOSAMENTE";
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
        /// Insercion de cliente
        /// </summary>
        /// <param name="idCliente"></param>
        /// <returns></returns>
        public GeneralResponse InsertClient(RequestCliente reqCliente)
        {
            #region <<validacion de campos>>
            var credentials = new ClienteValidador
            {
                nombre = reqCliente.nombre,
                direccion = reqCliente.direccion,
                telefono = reqCliente.telefono,
                correo = reqCliente.correo
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
                { "nombre_cliente", reqCliente.nombre },
                { "direccion_cliente", reqCliente.direccion },
                { "telefono_cliente", reqCliente.telefono },
                { "correo_cliente", reqCliente.correo }};

            string sp = "sp_InsertarCliente";

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
        /// Actualizar registro de cliente
        /// </summary>
        /// <param name="reqCliente"></param>
        /// <returns></returns>
        public GeneralResponse UpdateClient(RequestCliente reqCliente)
        {
            #region <<validacion de campos>>
            var credentials = new ClienteValidador
            {
                nombre = reqCliente.nombre,
                direccion = reqCliente.direccion,
                telefono = reqCliente.telefono,
                correo = reqCliente.correo
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
                { "id_cliente", reqCliente.id },
                { "direccion_cliente", reqCliente.direccion },
                { "telefono_cliente", reqCliente.telefono },
                { "correo_cliente", reqCliente.correo }};

            string sp = "sp_actualizarClienteByID";

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
