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
    public class ConceptoSinRutaModel
    {
        public static class ConceptsTypeValues
        {
            public const int DOC_CONTADO_CFDI = 0;
            public const int DOC_CONTADO = 1;
            public const int DOC_CREDITO_CFDI = 2;
            public const int DOC_CREDITO = 3;
            public const int DOC_REP_CFDI = 4;
            public const int DOC_REP = 5;
            public const int DOC_COTIZACION = 6;
            public const int DOC_PEDIDO = 7;
            public const int DOC_REMISION = 8;
            public const int DOC_DEVOLUCION_CFDI = 9;
            public const int DOC_DEVOLUCION = 10;
        }

        public static ExpandoObject getCodeAndSerieForAConceptTypeByRoute(String panelInstance, int conceptType)
        {
            dynamic codeAndSerieObject = null;
            SqlConnection connPanel = new SqlConnection();
            connPanel.ConnectionString = panelInstance;
            connPanel.Open();
            try
            {
                String query = "SELECT codigo_concepto, serie FROM ConceptosSinRuta WHERE tipo_concepto = @conceptType";
                using (SqlCommand command = new SqlCommand(query, connPanel))
                {
                    command.Parameters.AddWithValue("@conceptType", conceptType);
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
                SECUDOC.writeLog("Exception getCodeAndSerieForAConceptTypeByRoute: " + e.Message);
            }
            finally
            {
                if (connPanel != null && connPanel.State == ConnectionState.Open)
                    connPanel.Close();
            }
            return codeAndSerieObject;
        }

        public static ExpandoObject getCodeAndSerieForAConceptTypeByRoute(SqlConnection dbPanel, int conceptType)
        {
            dynamic codeAndSerieObject = null;
            try
            {
                String query = "SELECT codigo_concepto, serie FROM ConceptosSinRuta WHERE tipo_concepto = @conceptType";
                using (SqlCommand command = new SqlCommand(query, dbPanel))
                {
                    command.Parameters.AddWithValue("@conceptType", conceptType);
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
