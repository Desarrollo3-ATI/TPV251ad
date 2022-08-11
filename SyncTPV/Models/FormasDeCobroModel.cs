using SyncTPV.Helpers.SqliteDatabaseHelper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using wsROMClases.Models.Panel;

namespace SyncTPV
{

    public class FormasDeCobroModel
    {
        public int FORMA_COBRO_ID { set; get; }
        public string NOMBRE { set; get; }

        public static int saveAllFormasDeCobro(List<FormasDeCobroModel> fcList)
        {
            int lastId = 0;
            if (fcList != null)
            {
                var db = new SQLiteConnection();
                try
                {
                    db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                    db.Open();
                    foreach (var fc in fcList)
                    {
                        String query = "INSERT INTO " + LocalDatabase.TABLA_FORMASCOBRO + " VALUES(@id, @name)";
                        using (SQLiteCommand command = new SQLiteCommand(query, db))
                        {
                            command.Parameters.AddWithValue("@id", fc.FORMA_COBRO_ID);
                            command.Parameters.AddWithValue("@name", fc.NOMBRE.Trim());
                            int recordSaved = command.ExecuteNonQuery();
                            if (recordSaved != 0)
                                lastId = Convert.ToInt32(fc.FORMA_COBRO_ID);
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

        public static int saveFormasDeCobro(SQLiteConnection db, FormasDeCobroModel fc)
        {
            int lastId = 0;
            try
            {
                String query = "INSERT INTO " + LocalDatabase.TABLA_FORMASCOBRO + " VALUES(@id, @name)";
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    command.Parameters.AddWithValue("@id", fc.FORMA_COBRO_ID);
                    command.Parameters.AddWithValue("@name", fc.NOMBRE.Trim());
                    int recordSaved = command.ExecuteNonQuery();
                    if (recordSaved != 0)
                        lastId = Convert.ToInt32(fc.FORMA_COBRO_ID);
                }
            }
            catch (SQLiteException e)
            {
                SECUDOC.writeLog("Exception: " + e.ToString());
            }
            finally
            {
                
            }
            return lastId;
        }

        public static int saveFormasDeCobroLAN(SQLiteConnection db, ClsFormasDePagoModel fc)
        {
            int lastId = 0;
            try
            {
                String query = "INSERT INTO " + LocalDatabase.TABLA_FORMASCOBRO + " VALUES(@id, @name)";
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    command.Parameters.AddWithValue("@id", fc.FORMA_COBRO_ID);
                    command.Parameters.AddWithValue("@name", fc.NOMBRE.Trim());
                    int recordSaved = command.ExecuteNonQuery();
                    if (recordSaved != 0)
                        lastId = Convert.ToInt32(fc.FORMA_COBRO_ID);
                }
            }
            catch (SQLiteException e)
            {
                SECUDOC.writeLog("Exception: " + e.ToString());
            }
            finally
            {

            }
            return lastId;
        }

        public static bool updateFormasDeCobro(SQLiteConnection db, FormasDeCobroModel fc)
        {
            bool updated = false;
            try
            {
                String query = "UPDATE " + LocalDatabase.TABLA_FORMASCOBRO + " SET NOMBRE = @name WHERE FORMA_COBRO_CC_ID = @id";
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    command.Parameters.AddWithValue("@id", fc.FORMA_COBRO_ID);
                    command.Parameters.AddWithValue("@name", fc.NOMBRE.Trim());
                    int recordSaved = command.ExecuteNonQuery();
                    if (recordSaved > 0)
                        updated = true;
                }
            }
            catch (SQLiteException e)
            {
                SECUDOC.writeLog("Exception: " + e.ToString());
            }
            finally
            {
                
            }
            return updated;
        }

        public static bool updateFormasDeCobroLAN(SQLiteConnection db, ClsFormasDePagoModel fc)
        {
            bool updated = false;
            try
            {
                String query = "UPDATE " + LocalDatabase.TABLA_FORMASCOBRO + " SET NOMBRE = @name WHERE FORMA_COBRO_CC_ID = @id";
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    command.Parameters.AddWithValue("@id", fc.FORMA_COBRO_ID);
                    command.Parameters.AddWithValue("@name", fc.NOMBRE.Trim());
                    int recordSaved = command.ExecuteNonQuery();
                    if (recordSaved > 0)
                        updated = true;
                }
            }
            catch (SQLiteException e)
            {
                SECUDOC.writeLog("Exception: " + e.ToString());
            }
            finally
            {

            }
            return updated;
        }

        public static bool checkIfFcExist(SQLiteConnection db, int idFc)
        {
            bool exist = false;
            try
            {
                String query = "SELECT COUNT(*) FROM " + LocalDatabase.TABLA_FORMASCOBRO + " WHERE FORMA_COBRO_CC_ID = @id";
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    command.Parameters.AddWithValue("@id", idFc);
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                if (reader.GetValue(0) != DBNull.Value)
                                    if (Convert.ToInt32(reader.GetValue(0).ToString().Trim()) > 0)
                                        exist = true;
                            }
                        }
                        if (reader != null && !reader.IsClosed)
                            reader.Close();
                    }
                }
            }
            catch (SQLiteException e)
            {
                SECUDOC.writeLog("Exception: " + e.ToString());
            }
            finally
            {

            }
            return exist;
        }

        public static int saveAllFormasDeCobroLAN(List<ClsFormasDePagoModel> fcList)
        {
            int lastId = 0;
            if (fcList != null)
            {
                var db = new SQLiteConnection();
                try
                {
                    db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                    db.Open();
                    foreach (var fc in fcList)
                    {
                        String query = "INSERT INTO " + LocalDatabase.TABLA_FORMASCOBRO + " VALUES(@id, @name)";
                        using (SQLiteCommand command = new SQLiteCommand(query, db))
                        {
                            command.Parameters.AddWithValue("@id", fc.FORMA_COBRO_ID);
                            command.Parameters.AddWithValue("@name", fc.NOMBRE.Trim());
                            int recordSaved = command.ExecuteNonQuery();
                            if (recordSaved != 0)
                                lastId = Convert.ToInt32(fc.FORMA_COBRO_ID);
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

        public static int updateAllFormasDeCobroLAN(List<FormasDeCobroModel> fcList)
        {
            int lastId = 0;
            if (fcList != null)
            {
                var db = new SQLiteConnection();
                try
                {
                    db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                    db.Open();
                    foreach (var fc in fcList)
                    {
                        String query = "UPDATE " + LocalDatabase.TABLA_FORMASCOBRO + " SET NOMBRE = @name WHERE FORMA_COBRO_CC_ID = @id";
                        using (SQLiteCommand command = new SQLiteCommand(query, db))
                        {
                            command.Parameters.AddWithValue("@id", fc.FORMA_COBRO_ID);
                            command.Parameters.AddWithValue("@name", fc.NOMBRE.Trim());
                            int recordSaved = command.ExecuteNonQuery();
                            if (recordSaved != 0)
                                lastId = Convert.ToInt32(fc.FORMA_COBRO_ID);
                        }
                    }
                }
                catch (SQLiteException e)
                {
                    SECUDOC.writeLog("Exception: " + e.ToString());
                }
                finally
                {
                    if (db != null && db.State == ConnectionState.Open)
                        db.Close();
                }
            }
            return lastId;
        }

        public static List<FormasDeCobroModel> getAllFormasDeCobro()
        {
            List<FormasDeCobroModel> listaFormasDeCobro = null;
            FormasDeCobroModel formasCobro;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "SELECT * FROM " + LocalDatabase.TABLA_FORMASCOBRO;
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            listaFormasDeCobro = new List<FormasDeCobroModel>();
                            while (reader.Read())
                            {
                                formasCobro = new FormasDeCobroModel();
                                formasCobro.FORMA_COBRO_ID = reader.GetInt32(0);
                                formasCobro.NOMBRE = reader.GetString(1);
                                listaFormasDeCobro.Add(formasCobro);
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
            return listaFormasDeCobro;
        }

        public static List<FormasDeCobroModel> getAllFormasDeCobro(String query)
        {
            List<FormasDeCobroModel> listaFormasDeCobro = null;
            FormasDeCobroModel formasCobro;
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
                            listaFormasDeCobro = new List<FormasDeCobroModel>();
                            while (reader.Read())
                            {
                                formasCobro = new FormasDeCobroModel();
                                formasCobro.FORMA_COBRO_ID = Convert.ToInt32(reader.GetValue(0).ToString().Trim());
                                formasCobro.NOMBRE = reader.GetValue(1).ToString().Trim();
                                listaFormasDeCobro.Add(formasCobro);
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
            return listaFormasDeCobro;
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

        public static int getTotalOfFc()
        {
            int value = 0;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "SELECT COUNT(*) FROM "+LocalDatabase.TABLA_FORMASCOBRO;
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

        public static String getAStringValueByFormaDeCobro(String query)
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

        public static String getANameFrromAFomaDeCobroWithId(int fcId)
        {
            String name = "";
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "SELECT " + LocalDatabase.CAMPO_NOMBRE_FORMASCOBRO + " FROM " + LocalDatabase.TABLA_FORMASCOBRO +
                        " WHERE " + LocalDatabase.CAMPO_ID_FORMASCOBRO + " = " + fcId;
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                name = reader.GetString(0);
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

        public static Boolean deleteAllFormasDeCobro()
        {
            bool deleted = false;
            var db = new SQLiteConnection(ClsSQLiteDbHelper.instanceSQLite);
            db.Open();
            try
            {
                String query = "DELETE FROM " + LocalDatabase.TABLA_FORMASCOBRO;
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
            return false;
        }
    }
}
