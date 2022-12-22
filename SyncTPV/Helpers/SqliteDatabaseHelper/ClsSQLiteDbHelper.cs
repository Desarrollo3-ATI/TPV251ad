using System;
using System.Data;
using System.Data.SQLite;
using System.IO;

namespace SyncTPV.Helpers.SqliteDatabaseHelper
{
    public class ClsSQLiteDbHelper
    {
        public static int DB_VERSION = 2;
        //public static final String DB_NAME = Environment.getExternalStorageDirectory()+"/SyncRutMobile";
        public static String DB_NAME = "SyncTPV.db";
        public static readonly String instanceSQLite = "Data Source=" + MetodosGenerales.rootDirectory + "\\Data\\" + DB_NAME + ";Version=3;New=False;Compress=True;";

        public ClsSQLiteDbHelper() {
            // Crea la base de datos y registra usuario solo una vez
        }

        public bool validateDb()
        { // base sqlite de synctpv 
            bool validated = false;
            if (!File.Exists(Path.GetFullPath(MetodosGenerales.rootDirectory + "\\Data\\" + DB_NAME)))
            {
                string folderPath = MetodosGenerales.rootDirectory + "\\Data";
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                    Console.WriteLine(folderPath);
                }
                SQLiteConnection.CreateFile(MetodosGenerales.rootDirectory + "\\Data\\" + DB_NAME);
                onCreateDb();
                updateDb();
                validated = true;
            }
            else
            {
                updateDb();
                validated = true;
            }
            return validated;
        }

        public void onCreateDb()
        {
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = instanceSQLite;
                db.Open();
                SQLiteCommand cmd = new SQLiteCommand(db);
                createAllTables(cmd);
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

        public void updateDb()
        {
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = instanceSQLite;
                db.Open();
                SQLiteCommand cmd = new SQLiteCommand(db);
                createAllTables(cmd);
                LocalDatabase.validateAndCreateNewFieldsInAgents(db);
                LocalDatabase.validateAndCreateNewFieldsInConfiguration(db);
                LocalDatabase.validateOrCreateFieldsInCustomers(db);
                LocalDatabase.validateAndCreateNewFieldsInItems(db);
                LocalDatabase.validateAndCreateNewFieldsInPedidosEncabezado(db);
                LocalDatabase.validateAndCreateNewFieldsInPedidosDetalles(db);
                LocalDatabase.validateAndCreateNewFieldsInPeso(db);
                LocalDatabase.validateAndCreateNewFieldsInRetiros(db);
                LocalDatabase.validateAndCreateNewFieldsInMontoRetiros(db);
                LocalDatabase.validateAndCreateNewFieldsInMontoIngresos(db);
                LocalDatabase.validateAndCreateNewFieldsInTaras(db);
                LocalDatabase.validateAndCreateNewFieldsInInstanceSql(db);
                LocalDatabase.validateAndCreateNewFieldsInTickets(db);
                LocalDatabase.validateAndCreateNewFieldsInDatosTicket(db);
                LocalDatabase.validateAndCreateNewFieldsInDatosClienteADC(db);
                LocalDatabase.validateAndCreateNewFieldsInDatosLicencia(db);
                //LocalDatabase.validateIFDatosUsosCFDI(db);
                //LocalDatabase.validateIFDatosRegimenFiscal(db);
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

        private void createAllTables(SQLiteCommand cmd)
        {
            try
            {
                cmd.CommandText = LocalDatabase.CREAR_TABLA_CONFIGURACION;
                cmd.ExecuteNonQuery();
                cmd.CommandText = LocalDatabase.CREAR_TABLA_INSTANCESQLSE;
                cmd.ExecuteNonQuery();
                cmd.CommandText = LocalDatabase.CREAR_TABLA_USUARIO;
                cmd.ExecuteNonQuery();
                cmd.CommandText = LocalDatabase.CREAR_TABLA_CLIENTES;
                cmd.ExecuteNonQuery();
                cmd.CommandText = LocalDatabase.CREAR_TABLA_ARTICULOS;
                cmd.ExecuteNonQuery();
                cmd.CommandText = LocalDatabase.CREAR_TABLA_CREDITOS;
                cmd.ExecuteNonQuery();
                cmd.CommandText = LocalDatabase.CREAR_TABLA_CATVISITAS;
                cmd.ExecuteNonQuery();
                cmd.CommandText = LocalDatabase.CREATE_TABLE_CAJA;
                cmd.ExecuteNonQuery();
                cmd.CommandText = LocalDatabase.CREAR_TABLA_BANCO;
                cmd.ExecuteNonQuery();
                cmd.CommandText = LocalDatabase.CREAR_TABLA_BENEFICIARIO;
                cmd.ExecuteNonQuery();
                cmd.CommandText = LocalDatabase.CREATE_TABLE_BUSQUEDAS;
                cmd.ExecuteNonQuery();
                cmd.CommandText = LocalDatabase.CREAR_TABLA_CLASIFICACION;
                cmd.ExecuteNonQuery();
                cmd.CommandText = LocalDatabase.CREAR_TABLA_CLASIFICACIONVALOR;
                cmd.ExecuteNonQuery();
                cmd.CommandText = LocalDatabase.CREAR_TABLA_PROMOCIONES;
                cmd.ExecuteNonQuery();
                cmd.CommandText = LocalDatabase.CREAR_TABLA_FORMAS_COBRO;
                cmd.ExecuteNonQuery();
                cmd.CommandText = LocalDatabase.CREAR_TABLA_DATOS_TICKET;
                cmd.ExecuteNonQuery();
                cmd.CommandText = LocalDatabase.CREAR_TABLA_PRECIOEMPRESA;
                cmd.ExecuteNonQuery();
                cmd.CommandText = LocalDatabase.CREAR_TABLA_FORMASPAGO;
                cmd.ExecuteNonQuery();
                cmd.CommandText = LocalDatabase.CREAR_TABLA_ZONA_CLIENTES;
                cmd.ExecuteNonQuery();
                cmd.CommandText = LocalDatabase.CREAR_TABLA_CIUDADES;
                cmd.ExecuteNonQuery();
                cmd.CommandText = LocalDatabase.CREAR_TABLA_ESTADOS;
                cmd.ExecuteNonQuery();
                cmd.CommandText = LocalDatabase.CREAR_TABLA_PEDIDOENCABEZADO;
                cmd.ExecuteNonQuery();
                cmd.CommandText = LocalDatabase.CREAR_TABLA_DETALLEPEDIDO;
                cmd.ExecuteNonQuery();
                cmd.CommandText = LocalDatabase.CREAR_TABLA_PAGOSCREDITO;
                cmd.ExecuteNonQuery();
                cmd.CommandText = LocalDatabase.CREAR_TABLA_CLIENTEADC;
                cmd.ExecuteNonQuery();
                cmd.CommandText = LocalDatabase.CREAR_TABLA_DOCUMENTOVENTA;
                cmd.ExecuteNonQuery();
                cmd.CommandText = LocalDatabase.CREAR_TABLA_MOVIDOCVENTA;
                cmd.ExecuteNonQuery();
                cmd.CommandText = LocalDatabase.CREAR_TABLA_POSICION;
                cmd.ExecuteNonQuery();
                cmd.CommandText = LocalDatabase.CREAR_TABLA_BANDERASCI;
                cmd.ExecuteNonQuery();
                cmd.CommandText = LocalDatabase.CREAR_TABLA_TIPO_IMPRESORAS;
                cmd.ExecuteNonQuery();
                cmd.CommandText = LocalDatabase.CREAR_TABLA_DATOSDISTRIBUIDOR;
                cmd.ExecuteNonQuery();
                cmd.CommandText = LocalDatabase.CREAR_TABLA_LICENCIA;
                cmd.ExecuteNonQuery();
                cmd.CommandText = LocalDatabase.CREAR_TABLA_SERVERDATA;
                cmd.ExecuteNonQuery();
                cmd.CommandText = LocalDatabase.CREAR_TABLA_CLIENTESNOVISITADOS;
                cmd.ExecuteNonQuery();
                cmd.CommandText = LocalDatabase.CREAR_TABLA_FORMACOBRODOC;
                cmd.ExecuteNonQuery();
                cmd.CommandText = LocalDatabase.CREAR_TABLA_AYUDA;
                cmd.ExecuteNonQuery();
                cmd.CommandText = LocalDatabase.CREAR_TABLA_RETIRO;
                cmd.ExecuteNonQuery();
                cmd.CommandText = LocalDatabase.CREAR_TABLA_MONTORETIRO;
                cmd.ExecuteNonQuery();
                cmd.CommandText = LocalDatabase.CREATE_TABLE_UNITMEASUREWEITH;
                cmd.ExecuteNonQuery();
                cmd.CommandText = LocalDatabase.CREATE_TABLE_UNITCONVERSION;
                cmd.ExecuteNonQuery();
                cmd.CommandText = LocalDatabase.CREATE_MOVEMENTCXC_TABLE;
                cmd.ExecuteNonQuery();
                cmd.CommandText = LocalDatabase.CREAR_TABLA_APERTURATURNO;
                cmd.ExecuteNonQuery();
                cmd.CommandText = LocalDatabase.CREAR_TABLA_BASCULA;
                cmd.ExecuteNonQuery();
                cmd.CommandText = LocalDatabase.CREATE_TABLE_RUTA;
                cmd.ExecuteNonQuery();
                cmd.CommandText = LocalDatabase.CREAR_TABLA_PESO;
                cmd.ExecuteNonQuery();
                cmd.CommandText = LocalDatabase.CREATE_TABLE_TARA;
                cmd.ExecuteNonQuery();
                cmd.CommandText = LocalDatabase.CREATE_TABLE_INGRESO;
                cmd.ExecuteNonQuery();
                cmd.CommandText = LocalDatabase.CREATE_TABLE_MONTOINGRESO;
                cmd.ExecuteNonQuery();
                cmd.CommandText = LocalDatabase.CREATE_TABLE_DIRECTORIOS;
                cmd.ExecuteNonQuery();
                cmd.CommandText = LocalDatabase.CREATE_TABLE_FOLIOSDIGITALES;
                cmd.ExecuteNonQuery();
                cmd.CommandText = LocalDatabase.CREATE_TABLE_SINGLEWEIGHT;
                cmd.ExecuteNonQuery();
                cmd.CommandText = LocalDatabase.CREAR_TABLA_CONCEPTOS;
                cmd.ExecuteNonQuery();
                cmd.CommandText = LocalDatabase.CREAR_TABLA_IMPUESTOSCONCEPTOS;
                cmd.ExecuteNonQuery();
                //UsoCFDI
                cmd.CommandText = LocalDatabase.CREAR_TABLA_USO_CFDI;
                cmd.ExecuteNonQuery();
                //RegimenFiscal
                cmd.CommandText = LocalDatabase.CREAR_TABLA_REGIMEN_FISCAL;
                cmd.ExecuteNonQuery();
                //respaldo de tickets
                cmd.CommandText = LocalDatabase.CREAR_TABLA_RESPALDOTICKETS;
                cmd.ExecuteNonQuery();
            } catch (SQLiteException e)
            {
                SECUDOC.writeLog(e.ToString());
            }
        }



    }
}
