using SyncTPV.Helpers.SqliteDatabaseHelper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyncTPV.Models.Server.Panel
{
    public class RutaServerModel
    {
        public int id { get; set; }
        public String code { get; set; }
        public String name { get; set; }
        public String color { get; set; }
        public String createdAt { get; set; }

        public static int createUpdateOrDeleteRecords(String query)
        {
            int response = 0;
            var db = new SqlConnection();
            try {
                db.ConnectionString = InstanceSQLSEModel.getStringPanelInstance();
                db.Open();
                using (SqlCommand command = new SqlCommand(query, db)) {
                    int records = command.ExecuteNonQuery();
                    if (records > 0)
                        response = 1;
                }
            } catch (SqlException e) {
                SECUDOC.writeLog(e.ToString());
            } finally {
                if (db != null && db.State == System.Data.ConnectionState.Open)
                    db.Close();
            }
            return response;
        }

        public static int getIntValue(String panelInstance, String query)
        {
            int value = 0;
            var db = new SqlConnection();
            try
            {
                db.ConnectionString = panelInstance;
                db.Open();
                using (SqlCommand command = new SqlCommand(query, db))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                if (reader.GetValue(0) != DBNull.Value)
                                    value = Convert.ToInt32(reader.GetValue(0).ToString().Trim());
                            }
                        }
                        if (reader != null && !reader.IsClosed)
                            reader.Close();
                    }
                }
            }
            catch (SqlException e)
            {
                SECUDOC.writeLog(e.ToString());
            }
            finally
            {
                if (db != null && db.State == ConnectionState.Open)
                    db.Close();
            }
            return value;
        }

        public static List<RutaServerModel> getRecordsList(String query)
        {
            List<RutaServerModel> routesList = null;
            RutaServerModel rm = null;
            var db = new SqlConnection();
            try
            {
                db.ConnectionString = InstanceSQLSEModel.getStringPanelInstance();
                db.Open();
                using (SqlCommand command = new SqlCommand(query, db)) {
                    using (SqlDataReader reader = command.ExecuteReader()) {
                        if (reader.HasRows) {
                            routesList = new List<RutaServerModel>();
                            while (reader.Read()) {
                                rm = new RutaServerModel();
                                rm.id = Convert.ToInt32(reader[DbStructure.RomDb.CAMPO_ID_ROUTES].ToString().Trim());
                                rm.code = reader[DbStructure.RomDb.CAMPO_CODE_ROUTES].ToString().Trim();
                                rm.name = reader[DbStructure.RomDb.CAMPO_NAME_ROUTES].ToString().Trim();
                                rm.color = reader[DbStructure.RomDb.CAMPO_COLOR_ROUTES].ToString().Trim();
                                rm.createdAt = reader[DbStructure.RomDb.CAMPO_CREATEDAT_ROUTES].ToString().Trim();
                                routesList.Add(rm);
                            }
                        }
                        if (reader != null && !reader.IsClosed)
                            reader.Close();
                    }
                }
            } catch (SqlException e) {
                SECUDOC.writeLog(e.ToString());
            } finally {
                if (db != null && db.State == System.Data.ConnectionState.Open)
                    db.Close();
            }
            return routesList;
        }

    }
}
