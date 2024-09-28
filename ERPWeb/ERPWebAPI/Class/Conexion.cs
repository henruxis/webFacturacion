using Microsoft.Data.SqlClient;
using System.Collections;
using System.ComponentModel;
using System.Data.Common;
using System.Data;
using System.Data.OleDb;


namespace ERPWebAPI.Class
{
    public sealed class Conexion
    {
        private SqlCommand mocomando;

        private SqlConnection moconexion = null;

        private OleDbConnection cnn;

        private OleDbCommand moldbcomando;

        private bool mestado = false;

        private string merror;

        private SqlTransaction mtransaccion;

        public string Mensaje_Error => merror;

        public bool EstadoConexion => mestado;

        public Conexion(string archivo)
        {
            //IL_0058: Unknown result type (might be due to invalid IL or missing references)
            //IL_005d: Unknown result type (might be due to invalid IL or missing references)
            //IL_006f: Expected O, but got Unknown
            merror = string.Empty;
            if (cnn == null || ((DbConnection)(object)cnn).State == ConnectionState.Closed)
            {
                AbrirConexionArchivo(archivo);
            }
            else if (moldbcomando == null)
            {
                moldbcomando = new OleDbCommand
                {
                    Connection = cnn
                };
            }
        }

        public Conexion(bool f, string cadena_conexion)
        {
            merror = string.Empty;
            if (f)
            {
                moconexion = new SqlConnection(cadena_conexion);
                mocomando = new SqlCommand(cadena_conexion)
                {
                    Connection = moconexion
                };
            }
            else if (moconexion == null || moconexion.State == ConnectionState.Closed)
            {
                if (!Abrirconexion(cadena_conexion))
                {
                    mestado = false;
                }
                else
                {
                    mestado = true;
                }
            }
            else if (mocomando == null)
            {
                mocomando = new SqlCommand
                {
                    Connection = moconexion
                };
            }
        }

        public Conexion(bool f, string server, string basedatos, string user, string pwd, string puerto)
        {
            string text = null;
            merror = string.Empty;
            if (f)
            {
                text = "server=" + server + "," + puerto + ";user id=" + user + "," + puerto + ";pwd=" + pwd + ";database=" + basedatos + ";Connection TimeOut=60";
                moconexion = new SqlConnection(text);
                mocomando = new SqlCommand(text)
                {
                    Connection = moconexion
                };
                return;
            }

            text = "server=" + server + "," + puerto + ";user id=" + user + ";pwd=" + pwd + ";database=" + basedatos + ";Connection TimeOut=60";
            if (moconexion == null || moconexion.State == ConnectionState.Closed)
            {
                if (!Abrirconexion(text))
                {
                    mestado = false;
                }
                else
                {
                    mestado = true;
                }
            }
            else if (mocomando == null)
            {
                mocomando = new SqlCommand
                {
                    Connection = moconexion
                };
            }
        }

        public void Comenzar_ejecucion(string nombre_transaccion)
        {
            mtransaccion = moconexion.BeginTransaction(nombre_transaccion);
        }

        public int Ejecutar(Hashtable coleccion, string sql, int opcion)
        {
            int num = 0;
            try
            {
                mocomando.CommandText = sql;
                mocomando.CommandTimeout = 9000000;
                mocomando.Transaction = mtransaccion;
                switch (opcion)
                {
                    case 0:
                        mocomando.CommandType = CommandType.StoredProcedure;
                        break;
                    case 1:
                        mocomando.CommandType = CommandType.Text;
                        break;
                }

                if (coleccion != null)
                {
                    foreach (DictionaryEntry item in coleccion)
                    {
                        if (item.Value != null)
                        {
                            mocomando.Parameters.AddWithValue(item.Key.ToString(), item.Value);
                        }
                        else
                        {
                            mocomando.Parameters.AddWithValue(item.Key.ToString(), DBNull.Value);
                        }
                    }
                }

                num = mocomando.ExecuteNonQuery();
                if (coleccion != null)
                {
                    mocomando.Parameters.Clear();
                }
            }
            catch (SqlException ex)
            {
                merror = ex.Message;
                try
                {
                    if (moconexion.State == ConnectionState.Open && mtransaccion != null)
                    {
                        mtransaccion.Rollback();
                    }
                }
                catch (SqlException ex2)
                {
                    merror = ex2.Message;
                }
                finally
                {
                    num = -2;
                }
            }

            return num;
        }

        public void Confirmar_ejecucion()
        {
            mtransaccion.Commit();
        }

        public void Deshacer_ejecucion()
        {
            mtransaccion.Rollback();
        }

        public IDataReader Consultar(string sql, Hashtable coleccion, int opcion)
        {
            IDataReader dataReader = null;
            try
            {
                mocomando.CommandText = sql;
                if (opcion == 0)
                {
                    mocomando.CommandType = CommandType.StoredProcedure;
                }
                else
                {
                    mocomando.CommandType = CommandType.Text;
                }

                mocomando.CommandTimeout = 200000;
                if (mtransaccion != null)
                {
                    mocomando.Transaction = mtransaccion;
                }

                if (coleccion != null)
                {
                    foreach (DictionaryEntry item in coleccion)
                    {
                        if (item.Value != null)
                        {
                            if (item.Value is DataTable)
                            {
                                DataTable dataTable = (DataTable)item.Value;
                                mocomando.Parameters.AddWithValue(item.Key.ToString(), dataTable).TypeName = dataTable.TableName;
                                mocomando.Parameters[item.Key.ToString()].SqlDbType = SqlDbType.Structured;
                            }
                            else
                            {
                                mocomando.Parameters.AddWithValue(item.Key.ToString(), item.Value);
                            }
                        }
                        else
                        {
                            mocomando.Parameters.AddWithValue(item.Key.ToString(), DBNull.Value);
                        }
                    }
                }

                dataReader = mocomando.ExecuteReader();
                mocomando.Parameters.Clear();
                return dataReader;
            }
            catch (SqlException ex)
            {
                merror = ex.Message;
                return null;
            }
        }

        public object Ejecutar_escalar(string sql, Hashtable coleccion)
        {
            object obj = null;
            try
            {
                mocomando.CommandText = sql;
                mocomando.CommandType = CommandType.Text;
                mocomando.CommandTimeout = 200000;
                if (mtransaccion != null)
                {
                    mocomando.Transaction = mtransaccion;
                }

                if (coleccion != null)
                {
                    foreach (DictionaryEntry item in coleccion)
                    {
                        if (item.Value != null)
                        {
                            mocomando.Parameters.AddWithValue(item.Key.ToString(), item.Value);
                        }
                        else
                        {
                            mocomando.Parameters.AddWithValue(item.Key.ToString(), DBNull.Value);
                        }
                    }
                }

                obj = mocomando.ExecuteScalar();
                if (coleccion != null)
                {
                    mocomando.Parameters.Clear();
                }
            }
            catch (SqlException ex)
            {
                merror = ex.Message;
                if (mtransaccion != null)
                {
                    mtransaccion.Rollback();
                }

                return null;
            }

            return obj;
        }

        public bool Cerrarconexion()
        {
            bool result = false;
            if (moconexion != null && moconexion.State == ConnectionState.Open)
            {
                try
                {
                    moconexion.Close();
                    result = true;
                }
                catch (SqlException)
                {
                    result = false;
                }
                finally
                {
                    if (mocomando != null)
                    {
                        mocomando.Dispose();
                    }

                    if (moconexion != null)
                    {
                        moconexion.Dispose();
                    }
                }
            }

            return result;
        }

        public int Ejecutardeconectado(DataTable otabla, string sql)
        {
            int result = 0;
            using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sql, moconexion))
            {
                SqlCommandBuilder sqlCommandBuilder = new SqlCommandBuilder(sqlDataAdapter);
                try
                {
                    sqlDataAdapter.Fill(otabla);
                    result = sqlDataAdapter.Update(otabla);
                }
                catch (SqlException ex)
                {
                    merror = ex.Message;
                    result = -2;
                }
                finally
                {
                    sqlCommandBuilder?.Dispose();
                }
            }

            return result;
        }

        public DataSet Consultardesconectado(string sql, Hashtable coleccion)
        {
            DataSet dataSet = new DataSet();
            try
            {
                mocomando.CommandText = sql;
                mocomando.CommandType = CommandType.Text;
                mocomando.CommandTimeout = 200000;
                if (mtransaccion != null)
                {
                    mocomando.Transaction = mtransaccion;
                }

                if (coleccion != null)
                {
                    foreach (DictionaryEntry item in coleccion)
                    {
                        if (item.Value != null)
                        {
                            mocomando.Parameters.AddWithValue(item.Key.ToString(), item.Value);
                        }
                        else
                        {
                            mocomando.Parameters.AddWithValue(item.Key.ToString(), DBNull.Value);
                        }
                    }
                }

                using SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(mocomando);
                sqlDataAdapter.Fill(dataSet);
                if (coleccion != null)
                {
                    mocomando.Parameters.Clear();
                }
            }
            catch (SqlException ex)
            {
                merror = ex.Message;
                dataSet = null;
            }
            finally
            {
                mocomando.Dispose();
            }

            return dataSet;
        }

        public DataSet EjecutarProcedimiento(string sql, Hashtable coleccion)
        {
            DataSet dataSet = new DataSet();
            try
            {
                mocomando.CommandText = sql;
                mocomando.CommandType = CommandType.StoredProcedure;
                mocomando.CommandTimeout = 200000;
                if (mtransaccion != null)
                {
                    mocomando.Transaction = mtransaccion;
                }

                if (coleccion != null)
                {
                    foreach (DictionaryEntry item in coleccion)
                    {
                        if (item.Value != null)
                        {
                            mocomando.Parameters.AddWithValue(item.Key.ToString(), item.Value);
                        }
                        else
                        {
                            mocomando.Parameters.AddWithValue(item.Key.ToString(), DBNull.Value);
                        }
                    }
                }

                using SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(mocomando);
                sqlDataAdapter.Fill(dataSet);
                if (coleccion != null)
                {
                    mocomando.Parameters.Clear();
                }
            }
            catch (SqlException ex)
            {
                merror = ex.Message;
                dataSet = null;
            }
            finally
            {
                mocomando.Dispose();
            }

            return dataSet;
        }

        public DataSet Consultardesconectado(string sql)
        {
            DataSet dataSet = new DataSet();
            try
            {
                mocomando.CommandText = sql;
                mocomando.CommandType = CommandType.Text;
                mocomando.CommandTimeout = 200000;
                if (mtransaccion != null)
                {
                    mocomando.Transaction = mtransaccion;
                }

                using SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(mocomando);
                sqlDataAdapter.Fill(dataSet);
            }
            catch (SqlException ex)
            {
                merror = ex.Message;
                dataSet = null;
            }
            finally
            {
                mocomando.Dispose();
            }

            return dataSet;
        }

        private bool Abrirconexion(string cadena)
        {
            bool flag = false;
            try
            {
                moconexion = new SqlConnection(cadena);
                mocomando = new SqlCommand
                {
                    Connection = moconexion
                };
                if (moconexion.State != ConnectionState.Broken)
                {
                    mocomando.Connection.Open();
                }

                return true;
            }
            catch (SqlException ex)
            {
                merror = ex.Message;
                return false;
            }
        }

        private bool AbrirConexionArchivo(string file)
        {
            //IL_0014: Unknown result type (might be due to invalid IL or missing references)
            //IL_001e: Expected O, but got Unknown
            //IL_001f: Unknown result type (might be due to invalid IL or missing references)
            //IL_0024: Unknown result type (might be due to invalid IL or missing references)
            //IL_0036: Expected O, but got Unknown
            bool flag = false;
            try
            {
                string text = null;
                text = $"Provider=Microsoft.Jet.OLEDB.4.0; data source=\"{file}\"; Extended Properties=Excel 8.0;";
                cnn = new OleDbConnection(text);
                moldbcomando = new OleDbCommand
                {
                    Connection = cnn
                };
                if (((DbConnection)(object)cnn).State != ConnectionState.Broken)
                {
                    ((DbConnection)(object)moldbcomando.Connection).Open();
                }

                return true;
            }
            catch (OleDbException)
            {
                return false;
            }
        }

        public bool CerrarArchivo()
        {
            bool result = false;
            if (cnn != null && ((DbConnection)(object)cnn).State == ConnectionState.Open)
            {
                try
                {
                    ((DbConnection)(object)cnn).Close();
                    result = true;
                }
                catch (OleDbException)
                {
                    result = false;
                }
                finally
                {
                    if (moldbcomando != null)
                    {
                        ((Component)(object)moldbcomando).Dispose();
                    }

                    if (cnn != null)
                    {
                        ((Component)(object)cnn).Dispose();
                    }
                }
            }

            return result;
        }

        public OleDbDataReader ConsultaArchivo(string sql)
        {
            OleDbDataReader val = null;
            try
            {
                ((DbCommand)(object)moldbcomando).CommandText = sql;
                ((DbCommand)(object)moldbcomando).CommandType = CommandType.Text;
                ((DbCommand)(object)moldbcomando).CommandTimeout = 200000;
                return moldbcomando.ExecuteReader();
            }
            catch (OleDbException)
            {
                return null;
            }
        }


    }
}
