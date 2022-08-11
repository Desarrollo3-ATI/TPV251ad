using SyncTPV.Helpers.SqliteDatabaseHelper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyncTPV.Models
{
    public class RutaModel
    {

        public int id { get; set; }
        public String code { get; set; }
        public String name { get; set; }
        public String color { get; set; }
        public String createdAt { get; set; }

        public static int createUpdateOrDeleteRecords(String query)
        {
            int response = 0;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    int records = command.ExecuteNonQuery();
                    if (records > 0)
                        response = 1;
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
            return response;
        }

        public static int getIntValue(String query)
        {
            int value = 0;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
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
            catch (SQLiteException e)
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

        public static String getStringValue(String query)
        {
            String value = "";
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                if (reader.GetValue(0) != DBNull.Value)
                                    value = reader.GetValue(0).ToString().Trim();
                            }
                        }
                        if (reader != null && !reader.IsClosed)
                            reader.Close();
                    }
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
            return value;
        }

        public static List<RutaModel> getAllRoutes(String query)
        {
            List<RutaModel> routesList = null;
            RutaModel rm = null;
            var db = new SQLiteConnection();
            try {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                using (SQLiteCommand command = new SQLiteCommand(query, db)) {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows) {
                            routesList = new List<RutaModel>();
                            while (reader.Read()) {
                                rm = new RutaModel();
                                rm.id = Convert.ToInt32(reader[LocalDatabase.CAMPO_ID_RUTA].ToString().Trim());
                                rm.code = reader[LocalDatabase.CAMPO_CODE_RUTA].ToString().Trim();
                                rm.name = reader[LocalDatabase.CAMPO_NAME_RUTA].ToString().Trim();
                                rm.color = reader[LocalDatabase.CAMPO_COLOR_RUTA].ToString().Trim();
                                rm.createdAt = reader[LocalDatabase.CAMPO_CREATEDAT_RUTA].ToString().Trim();
                                routesList.Add(rm);
                            }
                        }
                        if (reader != null && !reader.IsClosed)
                            reader.Close();
                    }
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
            return routesList;
        }

        public static RutaModel getRoute(String query)
        {
            RutaModel rm = null;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                rm = new RutaModel();
                                rm.id = Convert.ToInt32(reader[LocalDatabase.CAMPO_ID_RUTA].ToString().Trim());
                                rm.code = reader[LocalDatabase.CAMPO_CODE_RUTA].ToString().Trim();
                                rm.name = reader[LocalDatabase.CAMPO_NAME_RUTA].ToString().Trim();
                                rm.color = reader[LocalDatabase.CAMPO_COLOR_RUTA].ToString().Trim();
                                rm.createdAt = reader[LocalDatabase.CAMPO_CREATEDAT_RUTA].ToString().Trim();
                            }
                        }
                        if (reader != null && !reader.IsClosed)
                            reader.Close();
                    }
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
            return rm;
        }

    }

    public class ResponseRutas
    {
        public int routesCount { get; set; }
        public List<RutaModel> routesList { get; set; }
    }
}
