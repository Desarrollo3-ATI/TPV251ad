using SyncTPV.Helpers.SqliteDatabaseHelper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using wsROMClase;

namespace SyncTPV.Models
{
    public class ResponseUnitsOfMeasureAndWeight
    {
        public ClsResponseUnitsOdMeasureAndWeight response { get; set; }
    }
    public class ClsResponseUnitsOdMeasureAndWeight
    {
        public int unitMeasureWeightCount { get; set; }
        public List<ClsUnitsMeasureWeightModel> unitMeasureWeight = new List<ClsUnitsMeasureWeightModel>();
    }

    public class UnitsOfMeasureAndWeightModel
    {
        /*public int id { get; set; }
        public int idServer { get; set; }
        public String name { get; set; }
        public String abbreviation { get; set; }
        public String deployment { get; set; }
        public String claveSAT { get; set; }
        public String claveComercioExterior { get; set; }*/

        public static bool unitExist(int idServer)
        {
            bool exist = false;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "SELECT COUNT(*) FROM " + LocalDatabase.TABLA_UNITMEASUREWEIGHT + " WHERE " +
                    LocalDatabase.CAMPO_IDSERVER_UNITMEASUREWEIGHT + " = @idServer";
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    command.Parameters.AddWithValue("@idServer", idServer);
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                                if (reader.GetValue(0) != DBNull.Value)
                                    if (Convert.ToInt32(reader.GetValue(0).ToString().Trim()) > 0)
                                        exist = true;
                        }
                        if (reader != null && !reader.IsClosed)
                            reader.Close();
                    }
                }
            } catch (SQLiteException e)
            {
                SECUDOC.writeLog(e.ToString());
            }
            finally
            {
                if (db != null && db.State == ConnectionState.Open)
                    db.Close();
            }
            return exist;
        }

        public static int saveAllUnitsOfMeasureAndWeight(List<ClsUnitsMeasureWeightModel> unitsList)
        {
            int lastId = 0;
            if (unitsList != null)
            {
                var db = new SQLiteConnection(ClsSQLiteDbHelper.instanceSQLite);
                db.Open();
                try
                {
                    foreach (var unit in unitsList)
                    {
                        String query = "INSERT INTO " + LocalDatabase.TABLA_UNITMEASUREWEIGHT +
                            " VALUES (@id, @idServer, @name, @abbreviation, @deployment, @claveSAT, @claveComercioExterior)";
                        using (SQLiteCommand command = new SQLiteCommand(query, db))
                        {
                            command.Parameters.AddWithValue("@id", null);
                            command.Parameters.AddWithValue("@idServer", unit.id);
                            command.Parameters.AddWithValue("@name", unit.name);
                            command.Parameters.AddWithValue("@abbreviation", unit.abbreviation);
                            command.Parameters.AddWithValue("@deployment", unit.deployment);
                            command.Parameters.AddWithValue("@claveSAT", unit.claveSAT);
                            command.Parameters.AddWithValue("@claveComercioExterior", unit.claveComercioExterior);
                            int recordInserted = command.ExecuteNonQuery();
                            if (recordInserted != 0)
                                lastId = unit.id;
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
            }
            return lastId;
        }

        public static bool createUnitOfMeasureAndWeight(int id, int idServer, String name, String abbreviation, String deployment, String claveSAT, 
            String claveComercioExterior)
        {
            bool saved = false;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "INSERT INTO " + LocalDatabase.TABLA_UNITMEASUREWEIGHT +
                        " VALUES (@id, @idServer, @name, @abbreviation, @deployment, @claveSAT, @claveComercioExterior)";
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    command.Parameters.AddWithValue("@id", id);
                    command.Parameters.AddWithValue("@idServer", idServer);
                    command.Parameters.AddWithValue("@name", name);
                    command.Parameters.AddWithValue("@abbreviation", abbreviation);
                    command.Parameters.AddWithValue("@deployment", deployment);
                    command.Parameters.AddWithValue("@claveSAT", claveSAT);
                    command.Parameters.AddWithValue("@claveComercioExterior", claveComercioExterior);
                    int recordInserted = command.ExecuteNonQuery();
                    if (recordInserted != 0)
                        saved = true;
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
            return saved;
        }

        public static int saveAllUnitsOfMeasureAndWeightLAN(List<ClsUnitsMeasureWeightModel> unitsList)
        {
            int lastId = 0;
            if (unitsList != null)
            {
                var db = new SQLiteConnection();                
                try
                {
                    db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                    db.Open();
                    foreach (var unit in unitsList)
                    {
                        String query = "INSERT INTO " + LocalDatabase.TABLA_UNITMEASUREWEIGHT +
                            " VALUES (@id, @idServer, @name, @abbreviation, @deployment, @claveSAT, @claveComercioExterior)";
                        using (SQLiteCommand command = new SQLiteCommand(query, db))
                        {
                            command.Parameters.AddWithValue("@id", null);
                            command.Parameters.AddWithValue("@idServer", unit.id);
                            command.Parameters.AddWithValue("@name", unit.name);
                            command.Parameters.AddWithValue("@abbreviation", unit.abbreviation);
                            command.Parameters.AddWithValue("@deployment", unit.deployment);
                            command.Parameters.AddWithValue("@claveSAT", unit.claveSAT);
                            command.Parameters.AddWithValue("@claveComercioExterior", unit.claveComercioExterior);
                            int recordInserted = command.ExecuteNonQuery();
                            if (recordInserted != 0)
                                lastId = unit.id;
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
            }
            return lastId;
        }

        public static bool updateUnitOfMeasureAndWeight(int idServer, String name, String abbreviation, String deployment, String claveSAT,
            String claveComercioExterior)
        {
            bool saved = false;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "UPDATE " + LocalDatabase.TABLA_UNITMEASUREWEIGHT +
                        " SET name = @name, "+LocalDatabase.CAMPO_ABBREVIATION_UNITMEASUREWEIGHT+" = @abbreviation, " +
                        LocalDatabase.CAMPO_DEPLOYMENT_UNITMEASUREWEIGHT+" = @deployment, "+
                        LocalDatabase.CAMPO_SATKEY_UNITMEASUREWEIGHT+" = @claveSAT, " +
                        LocalDatabase.CAMPO_FOREIGNTRADEKEY_UNITMEASUREWEIGHT+" = @claveComercioExterior " +
                        "WHERE "+LocalDatabase.CAMPO_IDSERVER_UNITMEASUREWEIGHT+ " = @idServer";
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    command.Parameters.AddWithValue("@name", name);
                    command.Parameters.AddWithValue("@abbreviation", abbreviation);
                    command.Parameters.AddWithValue("@deployment", deployment);
                    command.Parameters.AddWithValue("@claveSAT", claveSAT);
                    command.Parameters.AddWithValue("@claveComercioExterior", claveComercioExterior);
                    command.Parameters.AddWithValue("@idServer", idServer);
                    int recordInserted = command.ExecuteNonQuery();
                    if (recordInserted != 0)
                        saved = true;
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
            return saved;
        }

        public static List<ClsUnitsMeasureWeightModel> obtainUnitsEquivalentToBaseUnit(int baseUnitId)
        {
            List<ClsUnitsMeasureWeightModel> umwList = new List<ClsUnitsMeasureWeightModel>();
            ClsUnitsMeasureWeightModel umwm;
            if (baseUnitId > 0)
            {
                List<int> equivalentUnitsList = ConversionsUnitsModel.getAllEquivalentUnitsIdOfTheBaseUnitFromAnItem(baseUnitId);
                if (equivalentUnitsList != null)
                {
                    List<int> newUnitsList = new List<int>();
                    newUnitsList.AddRange(equivalentUnitsList);
                    newUnitsList.Add(baseUnitId);
                    var db = new SQLiteConnection(ClsSQLiteDbHelper.instanceSQLite);
                    db.Open();
                    try
                    {
                        for (int i = 0; i < newUnitsList.Count; i++)
                        {
                            String query = "SELECT * FROM " + LocalDatabase.TABLA_UNITMEASUREWEIGHT + " WHERE " +
                                    LocalDatabase.CAMPO_IDSERVER_UNITMEASUREWEIGHT + " = " + newUnitsList[i];
                            using (SQLiteCommand command = new SQLiteCommand(query, db))
                            {
                                using (SQLiteDataReader reader = command.ExecuteReader())
                                {
                                    if (reader.HasRows)
                                    {
                                        while (reader.Read())
                                        {
                                            umwm = new ClsUnitsMeasureWeightModel();
                                            umwm.id = Convert.ToInt32(reader[LocalDatabase.CAMPO_ID_UNITMEASUREWEIGHT].ToString());
                                            umwm.idServer = Convert.ToInt32(reader[LocalDatabase.CAMPO_IDSERVER_UNITMEASUREWEIGHT].ToString());
                                            umwm.name = reader[LocalDatabase.CAMPO_NAME_UNITMEASUREWEIGHT].ToString().Trim();
                                            umwm.deployment = reader[LocalDatabase.CAMPO_DEPLOYMENT_UNITMEASUREWEIGHT].ToString().Trim();
                                            umwm.claveSAT = reader[LocalDatabase.CAMPO_SATKEY_UNITMEASUREWEIGHT].ToString().Trim();
                                            umwm.claveComercioExterior = reader[LocalDatabase.CAMPO_FOREIGNTRADEKEY_UNITMEASUREWEIGHT].ToString().Trim();
                                            umwList.Add(umwm);
                                        }
                                    }
                                    if (reader != null && !reader.IsClosed)
                                        reader.Close();
                                }
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
                    return umwList;
                }
                else
                {
                    return umwList;
                }
            }
            else
            {
                return umwList;
            }
        }

        public static String getStringValueFromUnitsMeasureAndWeight(String query)
        {
            String value = "";
            var db = new SQLiteConnection(ClsSQLiteDbHelper.instanceSQLite);
            db.Open();
            try
            {
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                value = reader.GetString(0);
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

        public static ClsUnitsMeasureWeightModel getAUnidad(int idUnidad)
        {
            ClsUnitsMeasureWeightModel umwm = null;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "SELECT * FROM " + LocalDatabase.TABLA_UNITMEASUREWEIGHT + " WHERE " + LocalDatabase.CAMPO_IDSERVER_UNITMEASUREWEIGHT + " = " +
                    idUnidad;
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                umwm = new ClsUnitsMeasureWeightModel();
                                umwm.id = Convert.ToInt32(reader[LocalDatabase.CAMPO_ID_UNITMEASUREWEIGHT].ToString());
                                umwm.idServer = Convert.ToInt32(reader[LocalDatabase.CAMPO_IDSERVER_UNITMEASUREWEIGHT].ToString());
                                umwm.name = reader[LocalDatabase.CAMPO_NAME_UNITMEASUREWEIGHT].ToString().Trim();
                                umwm.deployment = reader[LocalDatabase.CAMPO_DEPLOYMENT_UNITMEASUREWEIGHT].ToString().Trim();
                                umwm.claveSAT = reader[LocalDatabase.CAMPO_SATKEY_UNITMEASUREWEIGHT].ToString().Trim();
                                umwm.claveComercioExterior = reader[LocalDatabase.CAMPO_FOREIGNTRADEKEY_UNITMEASUREWEIGHT].ToString().Trim();
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
            return umwm;
        }

        public static int getTotalUnits()
        {
            int total = 0;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "SELECT COUNT(*) FROM "+LocalDatabase.TABLA_UNITMEASUREWEIGHT;
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                if (reader.GetValue(0) != DBNull.Value)
                                    total = Convert.ToInt32(reader.GetValue(0).ToString().Trim());
                            }
                        }
                        if (reader != null && !reader.IsClosed)
                            reader.Close();
                    }
                }
            } catch (SQLiteException e)
            {
                SECUDOC.writeLog(e.ToString());
            } finally
            {
                if (db != null && db.State == ConnectionState.Open)
                    db.Close();
            }
            return total;
        }

        public static String getNameOfAndUnitMeasureWeight(int idServer)
        {
            String name = "";
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "SELECT " + LocalDatabase.CAMPO_NAME_UNITMEASUREWEIGHT + " FROM " +
                        LocalDatabase.TABLA_UNITMEASUREWEIGHT + " WHERE " +
                        LocalDatabase.CAMPO_IDSERVER_UNITMEASUREWEIGHT + " = " + idServer;
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                name = reader[LocalDatabase.CAMPO_NAME_UNITMEASUREWEIGHT].ToString().Trim();
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
            return name;
        }

        public static Boolean deleteAllUnitsOfMeasureAndWeight()
        {
            bool deleted = false;
            var db = new SQLiteConnection(ClsSQLiteDbHelper.instanceSQLite);
            db.Open();
            try
            {
                String query = "DELETE FROM " + LocalDatabase.TABLA_UNITMEASUREWEIGHT;
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    int deletedRecords = command.ExecuteNonQuery();
                    deleted = true;
                }
            }
            catch (Exception ex)
            {
                SECUDOC.writeLog(ex.ToString());
            }
            finally
            {
                if (db != null && db.State == ConnectionState.Open)
                    db.Close();
            }
            return deleted;
        }

        public static int getLastId()
        {
            int lastId = 0;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "SELECT MAX(id) AS id FROM UnitMeasureWeight LIMIT 1";
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                                if (reader.GetValue(0) != DBNull.Value)
                                    lastId = Convert.ToInt32(reader.GetValue(0).ToString().Trim());
                        }
                        if (reader != null && !reader.IsClosed)
                            reader.Close();
                    }
                }
            } catch (SQLiteException e)
            {
                SECUDOC.writeLog(e.ToString());
            }
            finally
            {
                if (db != null && db.State == ConnectionState.Open)
                    db.Close();
            }
            return lastId;
        }
    }
}
