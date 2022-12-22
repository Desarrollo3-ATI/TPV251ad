using SyncTPV.Helpers.SqliteDatabaseHelper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SQLite;
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

        public static ExpandoObject ActualizarConceptosEImpuestos(String panelInstance, String comercialInstance)
        {
            dynamic respuesta = null;
            String banderaR = getConceptosEImpuestos(panelInstance, comercialInstance);
            if(banderaR != null)
            {
                List<dynamic> listaConceptos = null;
                if (Convert.ToInt32(banderaR) == 1)
                {
                    listaConceptos = getConsultaQueryConcepto(panelInstance, "Conceptos", 0);
                }
                else
                {
                    listaConceptos = getConsultaQueryConcepto(panelInstance, "ConceptosSinRuta",0);
                }
                if (listaConceptos.Count > 0)
                {
                    Boolean limpio = deleteAllConceptos();
                    String arregloS = "";
                    for (int i =0;i<listaConceptos.Count;i++)
                    {
                        String auxiliar = listaConceptos[i].codigo_concepto;
                        arregloS = arregloS + "'" + auxiliar + "'";
                        if (limpio)
                        {
                            AgregarConceptoTablaConceptos(listaConceptos[i]);
                        }
                        if((i+1) == listaConceptos.Count)
                        {
                            //nada
                        }
                        else
                        {
                            
                            arregloS = arregloS + ",";
                        }
                    }
                    respuesta = OptenerAllInformacionConceptosEIngresos(comercialInstance, arregloS);
                }
            }
            return respuesta;
        }
        public static Boolean AgregarConceptoTablaConceptos(dynamic concepto)
        {
            Boolean removed = false;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "INSERT INTO " + LocalDatabase.TABLA_CONCEPTOS+" ("+
                    LocalDatabase.CAMPO_WSID_CONCEPTOS +","+
                    LocalDatabase.CAMPO_CONCEPTOPORRUTA_CONCEPTOS + ","+
                    LocalDatabase.CAMPO_RUTACAJA_CONCEPTOS + ","+
                    LocalDatabase.CAMPO_COIDGO_CONCEPTOS +
                    ") VALUES ("+
                    (int) concepto.idws +","+
                    (int) concepto.conceptoPorRuta + ","+
                    (int) concepto.ruta_caja_id + ","+
                    "'"+(String) concepto.codigo_concepto +"'"
                    + ")";
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    int records = command.ExecuteNonQuery();
                    if (records > 0)
                        removed = true;
                }
            }
            catch (SQLiteException e)
            {
                SECUDOC.writeLog(e.ToString());
            }
            finally
            {
                if (db != null && db.State == ConnectionState.Open)
                    db.Close();
            }
            return removed;
        }

        public static Boolean deleteAllConceptos()
        {
            Boolean removed = false;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "DELETE FROM " + LocalDatabase.TABLA_CONCEPTOS;
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    int records = command.ExecuteNonQuery();
                    if (records > 0)
                        removed = true;
                }
            }
            catch (SQLiteException e)
            {
                SECUDOC.writeLog(e.ToString());
            }
            finally
            {
                if (db != null && db.State == ConnectionState.Open)
                    db.Close();
            }
            return removed;
        }

        public static dynamic OptenerAllInformacionConceptosEIngresos(String ComInstance, String idsArmado)
        {
            dynamic respuesta = new ExpandoObject();
            SqlConnection sqlConnection = new SqlConnection();
            try
            {
                sqlConnection.ConnectionString = ComInstance;
                sqlConnection.Open();
                String query = "";
                String banRutas = "";
                query = " select "+
                    "  CUSAIMPUESTO1, CUSAPORCENTAJEIMPUESTO1, CIDFORMULAPORCIMPUESTO1, CIDFORMULAIMPUESTO1,"+
                    "  CUSAIMPUESTO2, CUSAPORCENTAJEIMPUESTO2, CIDFORMULAPORCIMPUESTO2, CIDFORMULAIMPUESTO2,"+
                    "  CUSAIMPUESTO3, CUSAPORCENTAJEIMPUESTO3, CIDFORMULAPORCIMPUESTO3, CIDFORMULAIMPUESTO3"+
                    "  from admConceptos where admConceptos.CCODIGOCONCEPTO in ("+idsArmado+")";
                using (SqlCommand command = new SqlCommand(query, sqlConnection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                            while (reader.Read())
                            {
                                String aux = reader["codigo_concepto"].ToString().Trim();
                                respuesta.Add(aux);
                            }
                    }
                }

            }
            catch (Exception e)
            {

            }
            return respuesta;
        }
        public static List<dynamic> getConsultaQueryConcepto(String panelInstance, String tabla, int bandera)
        {
            List<dynamic> respuesta = new List<dynamic>();
            SqlConnection sqlConnection = new SqlConnection();
            try
            {
                sqlConnection.ConnectionString = panelInstance;
                sqlConnection.Open();
                String query = "";
                String banRutas = "";
                query = "Select codigo_concepto,id,nombre_concepto ";
                if (bandera == 1)
                {
                    query = query + ",ruta_caja_id";
                }
                    
                    query = query+" from "+tabla+" where tipo_concepto = " +8;
                using (SqlCommand command = new SqlCommand(query, sqlConnection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                            while (reader.Read())
                            {
                                dynamic aux = new ExpandoObject(); 
                                   aux.codigo_concepto = reader["codigo_concepto"].ToString().Trim();
                                   aux.conceptoPorRuta = bandera;
                                if (bandera == 1)
                                {
                                    aux.ruta_caja_id = reader["ruta_caja_id"].ToString().Trim();
                                }
                                else
                                {
                                    aux.ruta_caja_id = "0";
                                }
                                aux.idws = reader["id"].ToString().Trim();
                                aux.nombre_concepto = reader["nombre_concepto"].ToString().Trim();
                                respuesta.Add(aux);
                            }
                    }
                }

            }
            catch (Exception e)
            {

            }
            return respuesta;
        }
        public static String getConceptosEImpuestos(String panelInstance, String comercialInstance)
        {
            String respuesta = null;
            SqlConnection sqlConnection = new SqlConnection();
            try
            {
                sqlConnection.ConnectionString = panelInstance;
                sqlConnection.Open();
                String query = "";
                String banRutas = "";
                query = "SELECT conceptoPorRuta FROM Configuracion";
                using (SqlCommand command = new SqlCommand(query, sqlConnection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                            while (reader.Read())
                            {
                                respuesta = reader.GetValue(0).ToString().Trim();
                            }
                    }
                }

            }
            catch(Exception e)
            {

            }
            return respuesta;
        }


    }
}
