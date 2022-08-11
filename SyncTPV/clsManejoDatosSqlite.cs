using SyncTPV.Helpers.SqliteDatabaseHelper;
using System;
using System.Data.SQLite;

namespace SyncTPV
{
    public class clsManejoDatosSqlite
    {
        private SQLiteConnection conexion;
        public bool conected = false;
        public string ErrorDescription = "";
        public clsManejoDatosSqlite()
        {
            //conexion = new SQLiteConnection("Data Source=" + GeneralTxt.RutaInicial + "\\SyncTPV.db;Version=3;New=False;Compress=True;");
            conexion = new SQLiteConnection(ClsSQLiteDbHelper.instanceSQLite);
            try
            {
                conexion.Open();
                conected = true;
            }
            catch (Exception ex)
            {
                ErrorDescription = "Error al conectar con SyncTPV.db: " + ex.Message;
                SECUDOC.writeLog("Error al conectar con SyncTPV.db: " + ex.ToString());
                conected = false;
            }
        }

        public clsManejoDatosSqlite(string BaseDatos)
        {
            conexion = new SQLiteConnection("Data Source=" + MetodosGenerales.rootDirectory + "\\" + BaseDatos + ";Version=3;New=False;Compress=True;");
            conexion.Open();
        }

        public SQLiteDataReader ExecuteReader(string CadenaSQL)
        {
            SQLiteCommand cmd = new SQLiteCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = CadenaSQL;
            cmd.Connection = conexion;
            return cmd.ExecuteReader();
        }

        public int ExcecuteNonQuery(string CadenaSQL)
        {
            SQLiteCommand cmd = new SQLiteCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = CadenaSQL;
            cmd.Connection = conexion;
            return cmd.ExecuteNonQuery();
        }

        public void Cerrar()
        {
            conexion.Close();
        }

    }
}
