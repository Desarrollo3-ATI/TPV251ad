using SyncTPV.Helpers.SqliteDatabaseHelper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;

namespace SyncTPV
{
    public class ClsBeneficiario
    {
        public int MONEDA_ID { set; get; }
        public string BANCO { set; get; }
        public string DESCRIPCION { set; get; }
        public string NUM_CUENTA { set; get; }
        public string CLABE { set; get; }
        public string SUCURSAL { set; get; }
        public string MONEDA { set; get; }
    }

    public class ClsBeneficiarioModel
    {
        public static int saveAllBeneficiarios(List<ClsBeneficiario> beneficiariosList)
        {
            int count = 0;
            if (beneficiariosList != null)
            {
                var db = new SQLiteConnection();
                try
                {
                    db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                    db.Open();
                    foreach (var beneficiario in beneficiariosList)
                    {
                        String query = "INSERT INTO " + LocalDatabase.TABLA_BENEFICIARIO + " VALUES (@banco, @descripcion, @numCuenta, @clabe," +
                            " @monedaId, @sucursal, @moneda)";
                        using (SQLiteCommand command = new SQLiteCommand(query, db))
                        {
                            command.Parameters.AddWithValue("@banco", beneficiario.BANCO);
                            command.Parameters.AddWithValue("@descripcion", beneficiario.DESCRIPCION);
                            command.Parameters.AddWithValue("@numCuenta", beneficiario.NUM_CUENTA);
                            command.Parameters.AddWithValue("@clabe", beneficiario.CLABE);
                            command.Parameters.AddWithValue("@monedaId", beneficiario.MONEDA_ID);
                            command.Parameters.AddWithValue("@sucursal", beneficiario.SUCURSAL);
                            command.Parameters.AddWithValue("@moneda", beneficiario.MONEDA);
                            int recordInserted = command.ExecuteNonQuery();
                            if (recordInserted != 0)
                                count++;
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
            return count;
        }

        public static int saveAllBeneficiariosLAN(List<CuentasEmpresa> beneficiariosList)
        {
            int count = 0;
            if (beneficiariosList != null)
            {
                var db = new SQLiteConnection();
                try
                {
                    db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                    db.Open();
                    foreach (var beneficiario in beneficiariosList)
                    {
                        String query = "INSERT INTO " + LocalDatabase.TABLA_BENEFICIARIO + " VALUES (@banco, @descripcion, @numCuenta, @clabe," +
                            " @monedaId, @sucursal, @moneda)";
                        using (SQLiteCommand command = new SQLiteCommand(query, db))
                        {
                            command.Parameters.AddWithValue("@banco", beneficiario.BANCO);
                            command.Parameters.AddWithValue("@descripcion", beneficiario.DESCRIPCION);
                            command.Parameters.AddWithValue("@numCuenta", beneficiario.NUM_CUENTA);
                            command.Parameters.AddWithValue("@clabe", beneficiario.CLABE);
                            command.Parameters.AddWithValue("@monedaId", beneficiario.MONEDA_ID);
                            command.Parameters.AddWithValue("@sucursal", beneficiario.SUCURSAL);
                            command.Parameters.AddWithValue("@moneda", beneficiario.MONEDA);
                            int recordInserted = command.ExecuteNonQuery();
                            if (recordInserted != 0)
                                count++;
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
            return count;
        }

        public static Boolean deleteAllBeneficiarios()
        {
            bool deleted = false;
            var db = new SQLiteConnection(ClsSQLiteDbHelper.instanceSQLite);
            db.Open();
            try
            {
                String query = "DELETE FROM " + LocalDatabase.TABLA_BENEFICIARIO;
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
