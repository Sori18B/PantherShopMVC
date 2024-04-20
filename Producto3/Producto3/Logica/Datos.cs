using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Producto3.Logica
{
    public class Datos
    {
        string cc;
        string msgErr;

        public Datos()
        {
            msgErr = "";
            cc = System.Configuration.ConfigurationManager.ConnectionStrings["PantherShop"].ConnectionString;
        }
        public SqlDataReader ObtenerDatos(string query)
        {
            SqlConnection connection = new SqlConnection(cc);
            SqlCommand command = new SqlCommand(query, connection);

            connection.Open();
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);

            return reader;
        }

        public SqlDataReader ObtenerDatos2arg(string query, SqlParameter[] parametros)
        {
            SqlConnection connection = new SqlConnection(cc);
            SqlCommand command = new SqlCommand(query, connection);

            if (parametros != null)
            {
                command.Parameters.AddRange(parametros);
            }

            connection.Open();
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);

            return reader;
        }



        public int EjecutaSql(string Instr, SqlParameter[] pars)
        {
            int resultado = 0;

            using (SqlConnection CONEXION = new SqlConnection(cc))
            {
                using (SqlCommand COMANDO = new SqlCommand(Instr, CONEXION))
                {
                    foreach (SqlParameter p in pars)
                        COMANDO.Parameters.Add(p);

                    try
                    {
                        Console.WriteLine("Cadena de conexión: " + cc);
                        Console.WriteLine("Ejecutando consulta SQL: " + Instr);

                        CONEXION.Open();

                        foreach (SqlParameter p in pars)
                        {
                            Console.WriteLine("Parámetro " + p.ParameterName + ": " + p.Value);
                        }

                        var result = COMANDO.ExecuteScalar();

                        if (result != null && result != DBNull.Value)
                        {
                            resultado = Convert.ToInt32(result);
                        }
                    }
                    catch (Exception ex)
                    {
                        msgErr = ex.Message;
                        Console.WriteLine("Error: " + msgErr);
                    }
                    finally
                    {
                        CONEXION.Close();
                    }
                }
            }

            return resultado;
        }

        public int EjecutaSqlENQ(string Instr, SqlParameter[] pars)
        {
            int numCam = 0;

            using (SqlConnection CONEXION = new SqlConnection(cc))
            {
                using (SqlCommand COMANDO = new SqlCommand(Instr, CONEXION))
                {
                    foreach (SqlParameter p in pars)
                        COMANDO.Parameters.Add(p);

                    try
                    {
                        Console.WriteLine("Cadena de conexión: " + cc);
                        Console.WriteLine("Ejecutando consulta SQL: " + Instr);

                        CONEXION.Open();

                        foreach (SqlParameter p in pars)
                        {
                            Console.WriteLine("Parámetro " + p.ParameterName + ": " + p.Value);
                        }

                        numCam = COMANDO.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        msgErr = ex.Message;
                        Console.WriteLine("Error: " + msgErr);
                    }
                    finally
                    {
                        CONEXION.Close();
                    }
                }
            }

            return numCam;
        }


    }
}
