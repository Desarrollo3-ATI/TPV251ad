using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyncTPV.Models
{
    public class ConceptoModel
    {

        public static ExpandoObject getCodeAndSerieForAConceptTypeByRoute(String panelInstance, int documentType, int conceptType, int idRoute)
        {
            dynamic codeAndSerieObject = null;
            SqlConnection connPanel = new SqlConnection();
            connPanel.ConnectionString = panelInstance;
            connPanel.Open();
            try
            {
                String query = "SELECT codigo_concepto, serie FROM Conceptos WHERE tipo_documento = @documentType AND " +
                    "tipo_concepto = @conceptType AND ruta_caja_id = @idRoute";
                using (SqlCommand command = new SqlCommand(query, connPanel))
                {
                    command.Parameters.AddWithValue("@documentType", documentType);
                    command.Parameters.AddWithValue("@conceptType", conceptType);
                    command.Parameters.AddWithValue("@idRoute", idRoute);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                codeAndSerieObject = new ExpandoObject();
                                codeAndSerieObject.code = reader["codigo_concepto"].ToString();
                                codeAndSerieObject.serie = reader["serie"].ToString();
                            }
                        }
                        if (reader != null && !reader.IsClosed)
                            reader.Close();
                    }
                }
            }
            catch (SqlException e)
            {
                SECUDOC.writeLog("Exception: " + e.ToString());
            }
            finally
            {
                if (connPanel != null && connPanel.State == ConnectionState.Open)
                    connPanel.Close();
            }
            return codeAndSerieObject;
        }

        public static ExpandoObject getCodeAndSerieForAConceptTypeByRoute(SqlConnection dbPanel, int documentType, int conceptType, int idRoute)
        {
            dynamic codeAndSerieObject = null;
            try
            {
                String query = "SELECT codigo_concepto, serie FROM Conceptos WHERE tipo_documento = @documentType AND " +
                    "tipo_concepto = @conceptType AND ruta_caja_id = @idRoute";
                using (SqlCommand command = new SqlCommand(query, dbPanel))
                {
                    command.Parameters.AddWithValue("@documentType", documentType);
                    command.Parameters.AddWithValue("@conceptType", conceptType);
                    command.Parameters.AddWithValue("@idRoute", idRoute);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                codeAndSerieObject = new ExpandoObject();
                                codeAndSerieObject.code = reader["codigo_concepto"].ToString();
                                codeAndSerieObject.serie = reader["serie"].ToString();
                            }
                        }
                        if (reader != null && !reader.IsClosed)
                            reader.Close();
                    }
                }
            }
            catch (SqlException e)
            {
                SECUDOC.writeLog("Exception: " + e.ToString());
            }
            finally
            {
                
            }
            return codeAndSerieObject;
        }

    }
}
