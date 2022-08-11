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
    public class InstanceSQLSEModel
    {
        public static readonly int ID_PANEL = 2;
        public static readonly int ID_COM = 1;
        public int id { get; set; }
        public String instance { get; set; }
        public String dbName { get; set; }
        public String user { get; set; }
        public String pass { get; set; }
        public String IPServer { get; set; }

        public static List<InstanceSQLSEModel> getAllInstances(String query)
        {
            List<InstanceSQLSEModel> instancesList = null;
            InstanceSQLSEModel instance = null;
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
                            instancesList = new List<InstanceSQLSEModel>();
                            while (reader.Read())
                            {
                                instance = new InstanceSQLSEModel();
                                instance.id = Convert.ToInt32(reader[LocalDatabase.CAMPO_ID_INSTANCESQLSE].ToString().Trim());
                                instance.instance = reader[LocalDatabase.CAMPO_INSTANCE_INSTANCESQLSE].ToString().Trim();
                                instance.dbName = reader[LocalDatabase.CAMPO_DBNAME_INSTANCESQLSE].ToString().Trim();
                                instance.user = reader[LocalDatabase.CAMPO_USER_INSTANCESQLSE].ToString().Trim();
                                instance.pass = reader[LocalDatabase.CAMPO_PASS_INSTANCESQLSE].ToString().Trim();
                                if (reader[LocalDatabase.CAMPO_IPSERVER_INSTANCESQLSE] != DBNull.Value)
                                    instance.IPServer = reader[LocalDatabase.CAMPO_IPSERVER_INSTANCESQLSE].ToString().Trim();
                                else instance.IPServer = "";
                                instancesList.Add(instance);
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
            return instancesList;
        }

        public static bool insertARecord(int id, String instance, String dbName, String user, String password, String IPServer)
        {
            bool updated = false;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "INSERT INTO " + LocalDatabase.TABLA_INSTANCESQLSE + " VALUES(@id, @instance, @dbName, @user, " +
                                    "@password, @IPServer)"; ;
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    command.Parameters.AddWithValue("@id", id);
                    command.Parameters.AddWithValue("@instance", instance);
                    command.Parameters.AddWithValue("@dbName", dbName);
                    command.Parameters.AddWithValue("@user", user);
                    command.Parameters.AddWithValue("@password", password);
                    command.Parameters.AddWithValue("@IPServer", IPServer);
                    int records = command.ExecuteNonQuery();
                    if (records > 0)
                        updated = true;
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
            return updated;
        }

        public static bool createOrUpdateInstanceSQLSE(String query)
        {
            bool updated = false;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    int records = command.ExecuteNonQuery();
                    if (records > 0)
                        updated = true;
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
            return updated;
        }

        public static bool updateInstance(int id, String ip, String instance, String dbName, String user, String pass)
        {
            bool updated = false;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "UPDATE " + LocalDatabase.TABLA_INSTANCESQLSE + " SET " + LocalDatabase.CAMPO_IPSERVER_INSTANCESQLSE + " = @ipServer, " +
                    LocalDatabase.CAMPO_INSTANCE_INSTANCESQLSE+ " = @instance, " + LocalDatabase.CAMPO_DBNAME_INSTANCESQLSE+ " = @dbName, " +
                    LocalDatabase.CAMPO_USER_INSTANCESQLSE+ " = @user, "+LocalDatabase.CAMPO_PASS_INSTANCESQLSE+" = @pass WHERE " +
                    LocalDatabase.CAMPO_ID_INSTANCESQLSE + " =  @id";
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    command.Parameters.AddWithValue("@id", id);
                    command.Parameters.AddWithValue("@ipServer", ip);
                    command.Parameters.AddWithValue("@instance", instance);
                    command.Parameters.AddWithValue("@dbName", dbName);
                    command.Parameters.AddWithValue("@user", user);
                    command.Parameters.AddWithValue("@pass", pass);
                    int records = command.ExecuteNonQuery();
                    if (records > 0)
                        updated = true;
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
            return updated;
        }

        public static String getStringPanelInstance()
        {
            String panelInstance = "";
            InstanceSQLSEModel im = null;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "SELECT * FROM "+LocalDatabase.TABLA_INSTANCESQLSE+" WHERE "+LocalDatabase.CAMPO_ID_INSTANCESQLSE+" = 2";
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                im = new InstanceSQLSEModel();
                                im.id = Convert.ToInt32(reader.GetValue(0).ToString().Trim());
                                im.instance = reader.GetValue(1).ToString().Trim();
                                im.dbName = reader.GetValue(2).ToString().Trim();
                                im.user = reader.GetValue(3).ToString().Trim();
                                im.pass = reader.GetValue(4).ToString().Trim();
                                if (reader[LocalDatabase.CAMPO_IPSERVER_INSTANCESQLSE] != DBNull.Value)
                                    im.IPServer = reader[LocalDatabase.CAMPO_IPSERVER_INSTANCESQLSE].ToString().Trim();
                                else im.IPServer = "";
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
                if (im != null)
                {
                    if (im.IPServer.Equals(""))
                    {

                    } else
                    {
                        String instancia = "";
                        if (im.instance.Contains('\\'))
                        {
                            char[] separators = new char[] { '\\' };
                            String[] parts = im.instance.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                            if (parts[1] != null)
                                instancia = im.IPServer + "\\" + parts[1];
                        }
                        else instancia = im.IPServer + "\\" + im.instance;
                        panelInstance = "Server=" + instancia + ";Database=" + im.dbName + "; User Id=" + im.user + ";Pwd=" + im.pass + ";";
                    }
                }
            }
            return panelInstance;
        }

        public static String getStringCATSATInstance()
        {
            String panelInstance = "";
            InstanceSQLSEModel im = null;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "SELECT * FROM " + LocalDatabase.TABLA_INSTANCESQLSE + " WHERE " + LocalDatabase.CAMPO_ID_INSTANCESQLSE + " = 2";
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                im = new InstanceSQLSEModel();
                                im.id = Convert.ToInt32(reader.GetValue(0).ToString().Trim());
                                im.instance = reader.GetValue(1).ToString().Trim();
                                im.dbName = reader.GetValue(2).ToString().Trim();
                                im.user = reader.GetValue(3).ToString().Trim();
                                im.pass = reader.GetValue(4).ToString().Trim();
                                if (reader[LocalDatabase.CAMPO_IPSERVER_INSTANCESQLSE] != DBNull.Value)
                                    im.IPServer = reader[LocalDatabase.CAMPO_IPSERVER_INSTANCESQLSE].ToString().Trim();
                                else im.IPServer = "";
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
                if (im != null)
                {
                    if (im.IPServer.Equals(""))
                    {

                    }
                    else
                    {
                        String instancia = "";
                        if (im.instance.Contains('\\'))
                        {
                            char[] separators = new char[] { '\\' };
                            String[] parts = im.instance.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                            if (parts[1] != null)
                                instancia = im.IPServer + "\\" + parts[1];
                        }
                        else instancia = im.IPServer + "\\" + im.instance;
                        panelInstance = "Server=" + instancia + ";Database=CATSAT; User Id=" + im.user + ";Pwd=" + im.pass + ";";
                    }
                }
            }
            return panelInstance;
        }

        public static String getStringComInstance()
        {
            String comInstance = "";
            InstanceSQLSEModel im = null;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "SELECT * FROM " + LocalDatabase.TABLA_INSTANCESQLSE + " WHERE " + LocalDatabase.CAMPO_ID_INSTANCESQLSE + " = 1";
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                im = new InstanceSQLSEModel();
                                im.id = Convert.ToInt32(reader.GetValue(0).ToString().Trim());
                                im.instance = reader.GetValue(1).ToString().Trim();
                                im.dbName = reader.GetValue(2).ToString().Trim();
                                im.user = reader.GetValue(3).ToString().Trim();
                                im.pass = reader.GetValue(4).ToString().Trim();
                                if (reader[LocalDatabase.CAMPO_IPSERVER_INSTANCESQLSE] != DBNull.Value)
                                    im.IPServer = reader[LocalDatabase.CAMPO_IPSERVER_INSTANCESQLSE].ToString().Trim();
                                else im.IPServer = "";
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
                if (im != null)
                {
                    if (im.IPServer.Equals(""))
                    {

                    }
                    else
                    {
                        String instancia = "";
                        if (im.instance.Contains('\\'))
                        {
                            char[] separators = new char[] { '\\' };
                            String[] parts = im.instance.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                            if (parts[1] != null)
                                instancia = im.IPServer + "\\" + parts[1];
                        }
                        else instancia = im.IPServer + "\\" + im.instance;
                        comInstance = "Server=" + instancia + ";Database=" + im.dbName + "; User Id=" + im.user + ";Pwd=" + im.pass + ";";
                    }
                }
            }
            return comInstance;
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
                                value = Convert.ToInt32(reader.GetValue(0).ToString().Trim());
                            }
                        }
                        if (reader != null && !reader.IsClosed)
                            reader.Close();
                    }
                }
            }
            catch (Exception Ex)
            {
                SECUDOC.writeLog(Ex.ToString());
            }
            finally
            {
                if (db != null && db.State == ConnectionState.Open)
                    db.Close();
            }
            return value;
        }

        public static bool exist(int idInstance)
        {
            bool exist = false;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "SELECT * FROM " + LocalDatabase.TABLA_INSTANCESQLSE + " WHERE " +
                    LocalDatabase.CAMPO_ID_INSTANCESQLSE + " = @idInstance";
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    command.Parameters.AddWithValue("@idInstance", idInstance);
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            exist = true;
                        }
                        if (reader != null && !reader.IsClosed)
                            reader.Close();
                    }
                }
            }
            catch (Exception Ex)
            {
                SECUDOC.writeLog(Ex.ToString());
            }
            finally
            {
                if (db != null && db.State == ConnectionState.Open)
                    db.Close();
            }
            return exist;
        }

        public static String getIPServerValue(int idInstance)
        {
            String ip = "";
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "SELECT "+LocalDatabase.CAMPO_IPSERVER_INSTANCESQLSE+" FROM "+LocalDatabase.TABLA_INSTANCESQLSE+" WHERE "+
                    LocalDatabase.CAMPO_ID_INSTANCESQLSE+" = @id";
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    command.Parameters.AddWithValue("@id", idInstance);
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                if (reader[LocalDatabase.CAMPO_IPSERVER_INSTANCESQLSE] != DBNull.Value)
                                    ip = reader[LocalDatabase.CAMPO_IPSERVER_INSTANCESQLSE].ToString().Trim();
                            }
                        }
                        if (reader != null && !reader.IsClosed)
                            reader.Close();
                    }
                }
            }
            catch (Exception Ex)
            {
                SECUDOC.writeLog(Ex.ToString());
            }
            finally
            {
                if (db != null && db.State == ConnectionState.Open)
                    db.Close();
            }
            return ip;
        }

        public static String getDbName(int id)
        {
            String name = "";
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "SELECT "+LocalDatabase.CAMPO_DBNAME_INSTANCESQLSE+" FROM "+LocalDatabase.TABLA_INSTANCESQLSE+
                    " WHERE "+LocalDatabase.CAMPO_ID_INSTANCESQLSE+" = "+id;
                using (SQLiteCommand command = new SQLiteCommand(query, db)) 
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                if (reader.GetValue(0) != DBNull.Value)
                                    name = reader.GetValue(0).ToString().Trim();
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

    }
}
