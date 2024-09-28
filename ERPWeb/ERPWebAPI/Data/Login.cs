using System.Collections;
using System.Data;
using ERPWebAPI.Class.Response;
using ERPWebAPI.Class;
using ERPWebAPI.Class.Validadores;
using System.ComponentModel.DataAnnotations;

namespace ERPWebAPI.Data
{
    public class Login
    {
        private readonly IConfiguration configuration;
        public Login(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public GeneralResponse LoginSesion(RequestLogin req)
        {
         
            var credentials = new SesionCredenciales
            {
                Usuario = req.usuario,
                Contraseña = req.contraseña
            };

            var validationResults = credentials.Validate(new ValidationContext(credentials));

            GeneralResponse respuesta = new();

            // Si hay errores de validación, devolver inmediatamente un mensaje de error
            if (validationResults.Any())
            {
                respuesta.Data = null;
                respuesta.Message = string.Join("; ", validationResults.Select(v => v.ErrorMessage));
                respuesta.Success = -1;  // Indicar fallo en la validación
                return respuesta;
            }

            
            var parametros = new Hashtable(){
                { "usuario", req.usuario },
                { "Clave", req.contraseña }};

            string sp = "sp_loginSession";


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
                        respuesta.Message = "LOGIN FALLIDO";
                        respuesta.Success = -1;
                    }

                    else
                    {
                        respuesta.Data = ConvertDataTableToDictionary(dt);
                        respuesta.Message = "LOGIN EXITOSO";
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
