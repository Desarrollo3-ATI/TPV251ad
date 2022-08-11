using SyncTPV.Helpers.SqliteDatabaseHelper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using wsROMClase;

namespace SyncTPV.Models
{
    public class ResponseConversionsUnits
    {
        public ClsResponseConversionsUnits response { get; set; }
    }
    public class ClsResponseConversionsUnits
    {
        public int unitConversionsCount { get; set; }
        public List<ConversionsUnitsModel> unitConversions = new List<ConversionsUnitsModel>();
    }

    public class ConversionsUnitsModel
    {
        public int id { get; set; }
        public int unitOneId { get; set; }
        public int unitTwoId { get; set; }
        public double conversionFactor { get; set; }

        public static int saveAllConversionsUnits(List<ConversionsUnitsModel> conversionsList)
        {
            int lastId = 0;
            if (conversionsList != null)
            {
                var db = new SQLiteConnection();
                try
                {
                    db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                    db.Open();
                    foreach (var conversion in conversionsList)
                    {
                        String query = "INSERT INTO " + LocalDatabase.TABLA_UNITCONVERSION +
                            " VALUES (@id, @idServer, @unitOneId, @unitTwoId, @conversionFactor)";
                        using (SQLiteCommand command = new SQLiteCommand(query, db))
                        {
                            command.Parameters.AddWithValue("@id", null);
                            command.Parameters.AddWithValue("@idServer", conversion.id);
                            command.Parameters.AddWithValue("@unitOneId", conversion.unitOneId);
                            command.Parameters.AddWithValue("@unitTwoId", conversion.unitTwoId);
                            command.Parameters.AddWithValue("@conversionFactor", conversion.conversionFactor);
                            int recordInserted = command.ExecuteNonQuery();
                            if (recordInserted != 0)
                                lastId = conversion.id;
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

        public static int saveAllConversionsUnitsLAN(List<ClsUnitConversionsModel> conversionsList)
        {
            int lastId = 0;
            if (conversionsList != null)
            {
                var db = new SQLiteConnection();
                try
                {
                    db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                    db.Open();
                    foreach (var conversion in conversionsList)
                    {
                        String query = "INSERT INTO " + LocalDatabase.TABLA_UNITCONVERSION +
                            " VALUES (@id, @idServer, @unitOneId, @unitTwoId, @conversionFactor)";
                        using (SQLiteCommand command = new SQLiteCommand(query, db))
                        {
                            command.Parameters.AddWithValue("@id", null);
                            command.Parameters.AddWithValue("@idServer", conversion.id);
                            command.Parameters.AddWithValue("@unitOneId", conversion.unitOneId);
                            command.Parameters.AddWithValue("@unitTwoId", conversion.unitTwoId);
                            command.Parameters.AddWithValue("@conversionFactor", conversion.conversionFactor);
                            int recordInserted = command.ExecuteNonQuery();
                            if (recordInserted != 0)
                                lastId = conversion.id;
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

        public static List<int> getAllEquivalentUnitsIdOfTheBaseUnitFromAnItem(int baseUnitId)
        {
            List<int> equivalentUnitsList = null;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "SELECT " + LocalDatabase.CAMPO_UNITONE_UNITCONVERSION + " FROM " +
                        LocalDatabase.TABLA_UNITCONVERSION + " WHERE " + LocalDatabase.CAMPO_UNITTWO_UNITCONVERSION + " = @baseUnitId " +
                        "ORDER BY " + LocalDatabase.CAMPO_UNITONE_UNITCONVERSION + " DESC";
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    command.Parameters.AddWithValue("@baseUnitId", baseUnitId);
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            equivalentUnitsList = new List<int>();
                            while (reader.Read())
                            {
                                equivalentUnitsList.Add(Convert.ToInt32(reader[LocalDatabase.CAMPO_UNITONE_UNITCONVERSION].ToString()));
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
            return equivalentUnitsList;
        }

        public static int checkIfTheCapturedUnitIsHigher(int baseUnitId, int capturedUnitId)
        {
            int salesUnitIsHigher = -1;
            double factorOne = 0;
            double factorTwo = 0;
            if (baseUnitId > 0)
            {
                factorOne = getAConversionFactor(capturedUnitId, baseUnitId);
                factorTwo = getAConversionFactor(baseUnitId, capturedUnitId);
                if (factorOne > 0 && factorTwo > 0)
                {
                    if (factorOne > factorTwo)
                    {
                        /** Unidad capturada es mayor */
                        salesUnitIsHigher = 1;
                    }
                    else if (factorOne < factorTwo)
                    {
                        /** Unidad capturada es menor */
                        salesUnitIsHigher = 0;
                    }
                    else if (factorOne == factorTwo)
                    {
                        /** Unidades Iguales */
                        salesUnitIsHigher = 2;
                    }
                }
                else
                {
                    salesUnitIsHigher = 3;
                }
            }
            return salesUnitIsHigher;
        }

        public static double getMajorOrMinorConversionFactorFromAnItem(int unitOneId, int unitTwoId, Boolean getMajor)
        {
            double majorFactor = 0;
            double factorOne = 0;
            double factorTwo = 0;
            if (unitOneId > 0)
            {
                factorOne = getAConversionFactor(unitTwoId, unitOneId);
                factorTwo = getAConversionFactor(unitOneId, unitTwoId);
                if (factorOne > factorTwo)
                {
                    if (getMajor)
                    {
                        majorFactor = factorOne;
                    }
                    else
                    {
                        majorFactor = factorTwo;
                    }
                }
                else
                {
                    if (getMajor)
                    {
                        majorFactor = factorTwo;
                    }
                    else
                    {
                        majorFactor = factorOne;
                    }
                }
            }
            return majorFactor;
        }

        public static double getAConversionFactor(int unitOneID, int unitTwoId)
        {
            double factor = 0;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "SELECT " + LocalDatabase.CAMPO_CONVERSIONFACTOR_UNITCONVERSION + " FROM " +
                        LocalDatabase.TABLA_UNITCONVERSION + " WHERE " +
                        LocalDatabase.CAMPO_UNITONE_UNITCONVERSION + " = " + unitOneID +
                        " AND " + LocalDatabase.CAMPO_UNITTWO_UNITCONVERSION + " = " + unitTwoId;
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                factor = Convert.ToDouble(reader[LocalDatabase.CAMPO_CONVERSIONFACTOR_UNITCONVERSION].ToString());
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
            return factor;
        }

        public static Boolean deleteAllConversionsUnits()
        {
            bool deleted = false;
            var db = new SQLiteConnection(ClsSQLiteDbHelper.instanceSQLite);
            db.Open();
            try
            {
                String query = "DELETE FROM " + LocalDatabase.TABLA_UNITCONVERSION;
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
    }
}
