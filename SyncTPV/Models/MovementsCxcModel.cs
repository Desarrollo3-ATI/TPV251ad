using SyncTPV.Helpers.SqliteDatabaseHelper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;

namespace SyncTPV.Models
{
    public class MovementsCxcModel
    {
        public int id { get; set; }
        public int creditDocumentId { get; set; }
        public int itemID { get; set; }
        public String itemCode { get; set; }
        public int number { get; set; }
        public double price { get; set; }
        public double capturedUnits { get; set; }
        public int capturedUnitId { get; set; }
        public double subtotal { get; set; }
        public double discount { get; set; }
        public double total { get; set; }

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

        public static List<MovementsCxcModel> getAllMovementsOfACxc(String query)
        {
            List<MovementsCxcModel> movesList = null;
            MovementsCxcModel moves;
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
                            movesList = new List<MovementsCxcModel>();
                            while (reader.Read())
                            {
                                moves = new MovementsCxcModel();
                                moves.id = Convert.ToInt32(reader[LocalDatabase.CAMPO_ID_MOVEMENTCXC].ToString().Trim());
                                moves.creditDocumentId = Convert.ToInt32(reader[LocalDatabase.CAMPO_CXCID_MOVEMENTCXC].ToString().Trim());
                                moves.itemID = Convert.ToInt32(reader[LocalDatabase.CAMPO_ITEMID_MOVEMENTCXC].ToString().Trim());
                                moves.itemCode = reader[LocalDatabase.CAMPO_ITEMCODE_MOVEMENTCXC].ToString().Trim();
                                moves.number = Convert.ToInt32(reader[LocalDatabase.CAMPO_NUMBER_MOVEMENTCXC].ToString().Trim());
                                moves.price = Convert.ToDouble(reader[LocalDatabase.CAMPO_PRICE_MOVEMENTCXC].ToString().Trim());
                                moves.capturedUnits = Convert.ToDouble(reader[LocalDatabase.CAMPO_CAPTUREDUNITS_MOVEMENTCXC].ToString().Trim());
                                moves.capturedUnitId = Convert.ToInt32(reader[LocalDatabase.CAMPO_CAPTUREDUNITSID_MOVEMENTCXC].ToString().Trim());
                                moves.subtotal = Convert.ToDouble(reader[LocalDatabase.CAMPO_SUBTOTAL_MOVEMENTCXC].ToString().Trim());
                                moves.discount = Convert.ToDouble(reader[LocalDatabase.CAMPO_DESCUENTO_MOVEMENTCXC].ToString().Trim());
                                moves.total = Convert.ToDouble(reader[LocalDatabase.CAMPO_TOTAL_MOVEMENTCXC].ToString().Trim());
                                movesList.Add(moves);
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
            return movesList;
        }

        public static void deleteAllMovementsCxc()
        {
            var db = new SQLiteConnection(ClsSQLiteDbHelper.instanceSQLite);
            db.Open();
            try
            {
                using (SQLiteCommand command = new SQLiteCommand("DELETE FROM " + LocalDatabase.TABLA_MOVEMENTCXC, db))
                {
                    int records = command.ExecuteNonQuery();
                }
            }
            catch (SQLiteException e)
            {
                SECUDOC.writeLog(e.Message);
            }
            finally
            {
                if (db != null && db.State == ConnectionState.Open)
                    db.Close();
            }
        }

        public static void deleteAllMovementsCxcByDocument(int idDocumentoCredito)
        {
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                using (SQLiteCommand command = new SQLiteCommand("DELETE FROM " + LocalDatabase.TABLA_MOVEMENTCXC+" WHERE "+
                    LocalDatabase.CAMPO_CXCID_MOVEMENTCXC+" = @idDocumentoCredito", db))
                {
                    command.Parameters.AddWithValue("@idDocumentoCredito", idDocumentoCredito);
                    int records = command.ExecuteNonQuery();
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

        public static bool createUpdateOrDeleteRecords(String query)
        {
            bool response = false;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    int records = command.ExecuteNonQuery();
                    if (records > 0)
                        response = true;
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

    }
}
