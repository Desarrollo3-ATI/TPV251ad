using SyncTPV.Controllers;
using SyncTPV.Models;
using System;
using System.Data.SQLite;

namespace SyncTPV.Helpers.SqliteDatabaseHelper
{
    public class LocalDatabase
    {
        public const String TABLA_APERTURATURNO = "AperturaTurno";
        public const String CAMPO_ID_APERTURATURNO = "id";
        public const String CAMPO_USERID_APERTURATURNO = "user_id";
        public const String CAMPO_IMPORTE_APERTURATURNO = "importe";
        public const String CAMPO_FECHAHORA_APERTURATURNO = "fecha_hora";
        public const String CAMPO_CREATEDAT_APERTURATURNO = "created_at";
        public const String CAMPO_STATUS_APERTURATURNO = "status";
        public const String CAMPO_SERVERID_APERTURATURNO = "server_id";

        public const String CREAR_TABLA_APERTURATURNO = "CREATE TABLE IF NOT EXISTS " + TABLA_APERTURATURNO + " (" + CAMPO_ID_APERTURATURNO + " INTEGER PRIMARY KEY, " +
            CAMPO_USERID_APERTURATURNO + " INTEGER DEFAULT 0, " + CAMPO_IMPORTE_APERTURATURNO + " NUMERIC DEFAULT 0, " + CAMPO_FECHAHORA_APERTURATURNO + " TEXT, " +
            CAMPO_CREATEDAT_APERTURATURNO + " TEXT, " + CAMPO_STATUS_APERTURATURNO + " INTEGER DEFAULT 0, " + CAMPO_SERVERID_APERTURATURNO + " INTEGER DEFAULT 0)";

        public const String TABLA_CONCEPTOS = "configConceptos";
        public const String CAMPO_ID_CONCEPTOS = "id";
        public const String CAMPO_WSID_CONCEPTOS = "ws_id";
        public const String CAMPO_CONCEPTOPORRUTA_CONCEPTOS = "conceptoPorRuta";//tipoConcepto = 8
        public const String CAMPO_RUTACAJA_CONCEPTOS = "rutaCaja";
        public const String CAMPO_COIDGO_CONCEPTOS = "codigoConcepto";

        public const String CREAR_TABLA_CONCEPTOS = "CREATE TABLE IF NOT EXISTS " + TABLA_CONCEPTOS + " (" + CAMPO_ID_CONCEPTOS + " INTEGER PRIMARY KEY, " +
            CAMPO_WSID_CONCEPTOS + " INTEGER DEFAULT 0, " + CAMPO_CONCEPTOPORRUTA_CONCEPTOS + " NUMERIC DEFAULT 0, " + CAMPO_RUTACAJA_CONCEPTOS + " INTEGER DEFAULT 0, " +
            CAMPO_SERVERID_APERTURATURNO + " TEXT)";

        public const String TABLA_IMPUESTOSCONCEPTOS = "impuestosConceptos";
        public const String CAMPO_ID_IMPUESTOSCONCEPTOS = "id";
        public const String CAMPO_IDCONCEPTO_IMPUESTOSCONCEPTOS = "idConcepto";
        public const String CAMPO_CUSAIMPUESTO_IMPUESTOSCONCEPTOS = "CUSAIMPUESTO";//tipoConcepto = 8
        public const String CAMPO_CUSAPORCENTAJEIMPUESTO_IMPUESTOSCONCEPTOS = "CUSAPORCENTAJEIMPUESTO";
        public const String CAMPO_CIDFORMULAPORCIMPUESTO_IMPUESTOSCONCEPTOS = "CIDFORMULAPORCIMPUESTO";
        public const String CAMPO_CIDFORMULAIMPUESTO_IMPUESTOSCONCEPTOS = "CIDFORMULAIMPUESTO";

        public const String CREAR_TABLA_IMPUESTOSCONCEPTOS = "CREATE TABLE IF NOT EXISTS " + TABLA_IMPUESTOSCONCEPTOS + " (" + CAMPO_ID_IMPUESTOSCONCEPTOS + " INTEGER PRIMARY KEY, " +
            CAMPO_IDCONCEPTO_IMPUESTOSCONCEPTOS + " INTEGER DEFAULT 0, " + CAMPO_CUSAIMPUESTO_IMPUESTOSCONCEPTOS + " NUMERIC DEFAULT 0, " + CAMPO_CUSAPORCENTAJEIMPUESTO_IMPUESTOSCONCEPTOS + " INTEGER DEFAULT 0, " +
            CAMPO_CIDFORMULAPORCIMPUESTO_IMPUESTOSCONCEPTOS + " INTEGER DEFAULT 0, "+ CAMPO_CIDFORMULAIMPUESTO_IMPUESTOSCONCEPTOS + " INTEGER DEFAULT 0 " + ")";

        public const String TABLA_CONFIGURACION = "configuracion";
        public const String CAMPO_ID_CONFIGURACION = "id";
        public const String CAMPO_VALOR_CONFIGURACION = "valor";
        public const String CAMPO_MODODEBUG_CONFIGURACION = "mododebug";
        public const String CAMPO_SMS_CONFIG = "sms_premission";
        public const String CAMPO_PRINTPERMISSION_CONFIG = "print_permission";
        public const String CAMPO_SERVERMODE_CONFIG = "server_mode";
        public const String CAMPO_NUMERODECOPIAS_CONFIG = "number_of_copies";
        public const String CAMPO_PERMISOBASCULA_CONFIG = "scales_permission";
        public const String CAMPO_CAPTURAPESOMANUAL_CONFIG = "capture_weights_manually";
        public const String CAMPO_APPTYPE_CONFIG = "appType";
        public const String CAMPO_SERVERID_CONFIG = "serverId";
        public const String CAMPO_ENTERPRISEID_CONFIG = "enterpriseId";
        public const String CAMPO_USEFICALFIELD_CONFIG = "useFiscalField";
        public const String CAMPO_CERRARCOM_CONFIG = "cerrarCOM";
        public const String CAMPO_FISCALITEMFIELD_CONFIG = "fiscalItemField";
        public const String CAMPO_WEBACTIVE_CONFIG = "webActive";
        public const String CAMPO_COTMOS_CONFIG = "cotmos";
        public const String CAMPO_CAJAPADRE_CONFIG = "codigoCajaPadre";
        public const String CAMPO_VENTARAPIDA_CONFIG = "ventaRapida";
        public const String CAMPO_REPORTEERROR_CONFIG = "reporteError";

        public static String CREAR_TABLA_CONFIGURACION = "CREATE TABLE IF NOT EXISTS " + TABLA_CONFIGURACION + " (" + CAMPO_ID_CONFIGURACION + " INTEGER PRIMARY KEY, " +
            CAMPO_VALOR_CONFIGURACION + " VARCHAR(200), " + CAMPO_MODODEBUG_CONFIGURACION + " INTEGER DEFAULT 1, " + CAMPO_SMS_CONFIG + " INTEGER DEFAULT 1, " +
            CAMPO_PRINTPERMISSION_CONFIG + " INTEGER DEFAULT 1, " + CAMPO_SERVERMODE_CONFIG + " INTEGER DEFAULT 0, " +
            CAMPO_NUMERODECOPIAS_CONFIG + " INTEGER DEFAULT 1, " + CAMPO_PERMISOBASCULA_CONFIG + " INTEGER DEFAULT 0, " +
            CAMPO_CAPTURAPESOMANUAL_CONFIG + " INTEGER DEFAULT 0, " + CAMPO_APPTYPE_CONFIG + " INT NOT NULL DEFAULT 0, " +
            CAMPO_SERVERID_CONFIG + " INT NOT NULL DEFAULT 0, " + CAMPO_ENTERPRISEID_CONFIG + " INT NOT NULL DEFAULT 0, " +
            CAMPO_USEFICALFIELD_CONFIG + " INT NOT NULL DEFAULT 0, " + CAMPO_CERRARCOM_CONFIG + " INT NOT NULL DEFAULT 0, " +
            CAMPO_FISCALITEMFIELD_CONFIG+" INT NOT NULL DEFAULT 6, "+CAMPO_WEBACTIVE_CONFIG+" INT NOT NULL DEFAULT 1, " +
            CAMPO_COTMOS_CONFIG+" INT NOT NULL DEFAULT 0, "+CAMPO_CAJAPADRE_CONFIG+" TEXT NOT NULL DEFAULT ''," +
            CAMPO_VENTARAPIDA_CONFIG + " INT NOT NULL DEFAULT 1,"+
            CAMPO_REPORTEERROR_CONFIG + " INT NOT NULL DEFAULT 1)";

        public static void validateAndCreateNewFieldsInConfiguration(SQLiteConnection db)
        {
            String queryItem = "SELECT * FROM " + TABLA_CONFIGURACION;
            using (SQLiteCommand command = new SQLiteCommand(queryItem, db))
            {
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    int campos = reader.FieldCount;  // see if the column is there
                    if (campos == 5)
                    {
                        SQLiteCommand cmd = new SQLiteCommand(db);
                        cmd.CommandText = "ALTER TABLE " + TABLA_CONFIGURACION + " ADD " + CAMPO_SERVERMODE_CONFIG + " INTEGER DEFAULT 0";
                        cmd.ExecuteNonQuery();
                        cmd.CommandText = "ALTER TABLE " + TABLA_CONFIGURACION + " ADD " + CAMPO_NUMERODECOPIAS_CONFIG + " INTEGER DEFAULT 1";
                        cmd.ExecuteNonQuery();
                        cmd.CommandText = "ALTER TABLE " + TABLA_CONFIGURACION + " ADD " + CAMPO_PERMISOBASCULA_CONFIG + " INTEGER DEFAULT 0";
                        cmd.ExecuteNonQuery();
                        cmd.CommandText = "ALTER TABLE " + TABLA_CONFIGURACION + " ADD " + CAMPO_CAPTURAPESOMANUAL_CONFIG + " INTEGER DEFAULT 0";
                        cmd.ExecuteNonQuery();
                    }
                    if (campos == 6)
                    {
                        SQLiteCommand cmd = new SQLiteCommand(db);
                        cmd.CommandText = "ALTER TABLE " + TABLA_CONFIGURACION + " ADD " + CAMPO_NUMERODECOPIAS_CONFIG + " INTEGER DEFAULT 1";
                        cmd.ExecuteNonQuery();
                        cmd.CommandText = "ALTER TABLE " + TABLA_CONFIGURACION + " ADD " + CAMPO_PERMISOBASCULA_CONFIG + " INTEGER DEFAULT 0";
                        cmd.ExecuteNonQuery();
                        cmd.CommandText = "ALTER TABLE " + TABLA_CONFIGURACION + " ADD " + CAMPO_CAPTURAPESOMANUAL_CONFIG + " INTEGER DEFAULT 0";
                        cmd.ExecuteNonQuery();
                    }
                    if (campos == 8)
                    {
                        SQLiteCommand cmd = new SQLiteCommand(db);
                        cmd.CommandText = "ALTER TABLE " + TABLA_CONFIGURACION + " ADD " + CAMPO_CAPTURAPESOMANUAL_CONFIG + " INTEGER DEFAULT 0";
                        cmd.ExecuteNonQuery();
                    }
                    if (campos == 9)
                    {
                        SQLiteCommand cmd = new SQLiteCommand(db);
                        cmd.CommandText = "ALTER TABLE " + TABLA_CONFIGURACION + " ADD " + CAMPO_APPTYPE_CONFIG + " INT NOT NULL DEFAULT 0";
                        cmd.ExecuteNonQuery();
                        cmd.CommandText = "ALTER TABLE " + TABLA_CONFIGURACION + " ADD " + CAMPO_SERVERID_CONFIG + " INT NOT NULL DEFAULT 0";
                        cmd.ExecuteNonQuery();
                        cmd.CommandText = "ALTER TABLE " + TABLA_CONFIGURACION + " ADD " + CAMPO_ENTERPRISEID_CONFIG + " INT NOT NULL DEFAULT 0";
                        cmd.ExecuteNonQuery();
                    }
                    if (campos == 12)
                    {
                        SQLiteCommand cmd = new SQLiteCommand(db);
                        cmd.CommandText = "ALTER TABLE " + TABLA_CONFIGURACION + " ADD " + CAMPO_USEFICALFIELD_CONFIG + " INT NOT NULL DEFAULT 0";
                        cmd.ExecuteNonQuery();
                    }
                    if (campos == 13)
                    {
                        SQLiteCommand cmd = new SQLiteCommand(db);
                        cmd.CommandText = "ALTER TABLE " + TABLA_CONFIGURACION + " ADD " + CAMPO_CERRARCOM_CONFIG + " INT NOT NULL DEFAULT 0";
                        cmd.ExecuteNonQuery();
                    }
                    if (campos == 14)
                    {
                        SQLiteCommand cmd = new SQLiteCommand(db);
                        cmd.CommandText = "ALTER TABLE " + TABLA_CONFIGURACION + " ADD " + CAMPO_FISCALITEMFIELD_CONFIG + " INT NOT NULL DEFAULT 6";
                        cmd.ExecuteNonQuery();
                    }
                    if (campos == 15)
                    {
                        SQLiteCommand cmd = new SQLiteCommand(db);
                        cmd.CommandText = "ALTER TABLE " + TABLA_CONFIGURACION + " ADD " + CAMPO_WEBACTIVE_CONFIG 
                            + " INT NOT NULL DEFAULT 1";
                        cmd.ExecuteNonQuery();
                    }
                    if (campos == 16)
                    {
                        SQLiteCommand cmd = new SQLiteCommand(db);
                        cmd.CommandText = "ALTER TABLE " + TABLA_CONFIGURACION + " ADD " + CAMPO_COTMOS_CONFIG
                            + " INT NOT NULL DEFAULT 0";
                        cmd.ExecuteNonQuery();
                    }
                    if (campos == 17)
                    {
                        SQLiteCommand cmd = new SQLiteCommand(db);
                        cmd.CommandText = "ALTER TABLE " + TABLA_CONFIGURACION + " ADD " + CAMPO_CAJAPADRE_CONFIG
                            + " TEXT NOT NULL DEFAULT ''";
                        cmd.ExecuteNonQuery();
                    }
                    if (campos == 18)
                    {
                        SQLiteCommand cmd = new SQLiteCommand(db);
                        cmd.CommandText = "ALTER TABLE " + TABLA_CONFIGURACION + " ADD " + CAMPO_VENTARAPIDA_CONFIG
                            + " INT NOT NULL DEFAULT 1";
                        cmd.ExecuteNonQuery();
                    }
                    if (campos == 19)
                    {
                        SQLiteCommand cmd = new SQLiteCommand(db);
                        cmd.CommandText = "ALTER TABLE " + TABLA_CONFIGURACION + " ADD " + CAMPO_REPORTEERROR_CONFIG
                            + " INT NOT NULL DEFAULT 1";
                        cmd.ExecuteNonQuery();
                    }
                    if (reader != null && !reader.IsClosed)
                        reader.Close();
                }
            }
        }

        public const String TABLA_CAJA = "Caja";
        public const String CAMPO_ID_CAJA = "id";
        public const String CAMPO_CODIGO_CAJA = "codigo";
        public const String CAMPO_NOMBRE_CAJA = "nombre";
        public const String CAMPO_ALMACENID_CAJA = "almacenId";
        public const String CAMPO_CREATEDAT_CAJA = "createdAt";

        public static String CREATE_TABLE_CAJA = "CREATE TABLE IF NOT EXISTS " + TABLA_CAJA + " (" + CAMPO_ID_CAJA + " INT NOT NULL PRIMARY KEY, " +
            CAMPO_CODIGO_CAJA + " VARCHAR(30) NOT NULL DEFAULT '', " + CAMPO_NOMBRE_CAJA + " VARCHAR(100) NOT NULL DEFAULT '', " +
            CAMPO_ALMACENID_CAJA + " INT NOT NULL DEFAULT 0, " + CAMPO_CREATEDAT_CAJA + " TEXT NOT NULL DEFAULT '')";

        public const String TABLA_DIRECTORIOS = "Directorios";
        public const String CAMPO_ID_DIRECTORIO = "id";
        public const String CAMPO_TIPO_DIRECTORIO = "type";
        public const String CAMPO_NOMBRE_DIRECTORIO = "nombre";
        public const String CAMPO_RUTA_DIRECTORIO = "ruta";
        public const String CAMPO_EMPRESAID_DIRECTORIO = "empresa_id";

        public static String CREATE_TABLE_DIRECTORIOS = "CREATE TABLE IF NOT EXISTS " + TABLA_DIRECTORIOS + " (" +
            CAMPO_ID_DIRECTORIO + " INTEGER PRIMARY KEY, " +
            CAMPO_TIPO_DIRECTORIO + " INTEGER NOT NULL DEFAULT 0, " + CAMPO_NOMBRE_DIRECTORIO + " VARCHAR(150) DEFAULT '', " +
            CAMPO_RUTA_DIRECTORIO + " TEXT NOT NULL DEFAULT '', " + CAMPO_EMPRESAID_DIRECTORIO + " INTEGER NOT NULL DEFAULT 0)";

        public const String TABLA_INSTANCESQLSE = "IntancesSQLServer";
        public const String CAMPO_ID_INSTANCESQLSE = "id";
        public const String CAMPO_INSTANCE_INSTANCESQLSE = "intance";
        public const String CAMPO_DBNAME_INSTANCESQLSE = "db_name";
        public const String CAMPO_USER_INSTANCESQLSE = "user";
        public const String CAMPO_PASS_INSTANCESQLSE = "pass";
        public const String CAMPO_IPSERVER_INSTANCESQLSE = "ip_server";

        public static String CREAR_TABLA_INSTANCESQLSE = "CREATE TABLE IF NOT EXISTS " + TABLA_INSTANCESQLSE + " (" +
            CAMPO_ID_INSTANCESQLSE + " INTEGER PRIMARY KEY, " +
            CAMPO_INSTANCE_INSTANCESQLSE + " TEXT, " + CAMPO_DBNAME_INSTANCESQLSE + " TEXT, " + CAMPO_USER_INSTANCESQLSE + " TEXT, " +
            CAMPO_PASS_INSTANCESQLSE + " TEXT, " + CAMPO_IPSERVER_INSTANCESQLSE + " TEXT)";

        public static void validateAndCreateNewFieldsInInstanceSql(SQLiteConnection db)
        {
            String queryItem = "SELECT * FROM " + TABLA_INSTANCESQLSE;
            using (SQLiteCommand command = new SQLiteCommand(queryItem, db))
            {
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    int campos = reader.FieldCount;  // see if the column is there
                    if (campos == 5)
                    {
                        SQLiteCommand cmd = new SQLiteCommand(db);
                        cmd.CommandText = "ALTER TABLE " + TABLA_INSTANCESQLSE + " ADD " + CAMPO_IPSERVER_INSTANCESQLSE + " TEXT";
                        cmd.ExecuteNonQuery();
                    }
                    if (reader != null && !reader.IsClosed)
                        reader.Close();
                }
            }
        }

        public const String TABLA_USUARIO = "Users";
        public const String CAMPO_ID_USUARIO = "USUARIO_ID";
        public const String CAMPO_ClAVE_USUARIO = "CLAVE";
        public const String CAMPO_PASSWORD_USUARIO = "PASS";
        public const String CAMPO_NOMBRE_USER = "NOMBRE";
        public const String CAMPO_RUTA_USER = "RUTA";
        public const String CAMPO_ALMACENID_USER = "ALMACEN_ID";
        public const String CAMPO_VALIDA_INVENTARIO = "VALIDA_INVENTARIO_VENTAS";
        public const String CAMPO_USAQR = "USAQR";
        public const String CAMPO_PERMISO_MODVENTAS = "MODVENTAS";
        public const String CAMPO_PERMISO_MODCOBRANZA = "MODCOBRANZA";
        public const String CAMPO_DATOS = "DATOS";
        public const String CAMPO_PERMISO_GPS_USUARIO = "GPS";
        public const String CAMPO_ABRIRCAJA = "ABRIRCAJA";
        public const String CAMPO_PERMISO_COBRANZA_USUARIO = "PCOBRANZA";
        public const String CAMPO_PVENTA = "PVENTA";
        public const String CAMPO_PPEDIDO = "PPDEDIDO";
        public const String CAMPO_PERMISO_COTIZACION_USUARIO = "PCOTIZACION";
        public const String CAMPO_PINFORMACION = "PINFORMACION";
        public const String CAMPO_PVISITA = "PVISITA";
        public const String CAMPO_PHISTORIAL = "PHISTORIAL";
        public const String CAMPO_PDEVOLUCIONES = "PDEVOLUCIONES";
        public const String CAMPO_PERMISO_MODPEDIDOS_USUARIO = "MODPEDIDOS";
        public const String CAMPO_MODPRECIO_USER = "MODPRECIO";
        public const String CAMPO_PERMISO_FACTURAR_USUARIO = "FACTURA";
        public const String CAMPO_VALIDACAJA = "VALIDACAJA";
        public const String CAMPO_LIMITE_CREDITO = "LIMITE_CREDITO";
        public const String CAMPO_PRECIO_EMPRESA_ID_USUARIO = "PRECIO_EMPRESA_ID";
        public const String CAMPO_MOD_DESCUENTO_USER = "MOD_DESCUENTO";
        public const String CAMPO_PERMISO_ALTACLIENTE = "ALTACLIENTE";
        public const String CAMPO_SIG_FOLIO = "SIG_FOLIO";
        public const String CAMPO_BANSESION = "BANSESION";
        public const String CAMPO_TIPOUSUARIO_USUARIO = "tipo_usuario";
        public const String CAMPO_CAJAID_USUARIO = "caja_id";
        public const String CAMPO_CAJACODIGO_USUARIO = "caja_codigo";
        public const String CAMPO_CAJANOMBRE_USUARIO = "caja_nombre";
        public const String CAMPO_PERMISO_LIMITARCREDITO_USUARIO = "limitar_credito";
        public const String CAMPO_CONNECTED_USUARIO = "connected";
        public const String CAMPO_LASTLOGIN_USUARIO = "lastLogin";
        public const String CAMPO_PERMFACTURARACREDITO_USUARIO = "facturar_credito";
        public const String CAMPO_LASTSEEN_USUARIO = "last_seen";
        public const String CAMPO_PERMTRASPASOALMACEN_USUARIO = "traspaso_almacen";
        public const String CAMPO_PERMSALIDAALMACEN_USUARIO = "salida_almacen";
        public const String CAMPO_PERMSELECTPRICE_USUARIO = "select_price";
        public const String CAMPO_PERMPREPEDIDO_USUARIO = "prepedido";
        public const String CAMPO_TODOSLOSCLIENTES_USUARIO = "todos_los_clientes";
        public const String CAMPO_ENTERPRISEID_USUARIO = "enterpriseId";
        public const String CAMPO_VENDERSINEXISTENCIAS_USUARIO = "venderSinExistencias";
        public const String CAMPO_VENDERACREDITO_USUARIO = "venderACredito";
        public const String CAMPO_DESCMAXIMO_USUARIO = "descMaximo";

        public static String CREAR_TABLA_USUARIO = "CREATE TABLE IF NOT EXISTS " +
            TABLA_USUARIO + " (" + CAMPO_ID_USUARIO + " INTEGER, " + CAMPO_ClAVE_USUARIO + " TEXT, " + CAMPO_PASSWORD_USUARIO + " TEXT, " + CAMPO_NOMBRE_USER + " TEXT," + CAMPO_RUTA_USER + " TEXT," +
            "" + CAMPO_ALMACENID_USER + " INTEGER," + CAMPO_VALIDA_INVENTARIO + " TEXT," + CAMPO_USAQR + " TEXT," + CAMPO_PERMISO_MODVENTAS + " TEXT," +
            "" + CAMPO_PERMISO_MODCOBRANZA + " TEXT," + CAMPO_DATOS + " TEXT," + CAMPO_PERMISO_GPS_USUARIO + " TEXT," + CAMPO_ABRIRCAJA + " TEXT," + CAMPO_PERMISO_COBRANZA_USUARIO + " TEXT," +
            "" + CAMPO_PVENTA + " TEXT," + CAMPO_PPEDIDO + " TEXT," + CAMPO_PERMISO_COTIZACION_USUARIO + " TEXT," + CAMPO_PINFORMACION + " TEXT," + CAMPO_PVISITA + " TEXT," +
            "" + CAMPO_PHISTORIAL + " TEXT," + CAMPO_PDEVOLUCIONES + " TEXT," + CAMPO_PERMISO_MODPEDIDOS_USUARIO + " TEXT," + CAMPO_MODPRECIO_USER + " TEXT," + CAMPO_PERMISO_FACTURAR_USUARIO + " TEXT," +
            "" + CAMPO_VALIDACAJA + " TEXT," + CAMPO_LIMITE_CREDITO + " INTEGER," + CAMPO_PRECIO_EMPRESA_ID_USUARIO + " INTEGER," + CAMPO_MOD_DESCUENTO_USER + " TEXT," +
            "" + CAMPO_PERMISO_ALTACLIENTE + " INTEGER," + CAMPO_SIG_FOLIO + " TEXT," + CAMPO_BANSESION + " INTEGER, " + CAMPO_TIPOUSUARIO_USUARIO + " INTEGER DEFAULT 1, " +
            CAMPO_CAJAID_USUARIO + " INTEGER DEFAULT 0, " + CAMPO_CAJACODIGO_USUARIO + " TEXT, " + CAMPO_CAJANOMBRE_USUARIO + " TEXT, " +
            CAMPO_PERMISO_LIMITARCREDITO_USUARIO + " INTEGER DEFAULT 0, " + CAMPO_CONNECTED_USUARIO + " INTEGER DEFAULT 0, " +
            CAMPO_LASTLOGIN_USUARIO + " TEXT, " + CAMPO_PERMFACTURARACREDITO_USUARIO + " INTEGER DEFAULT 0, " + CAMPO_LASTSEEN_USUARIO + " TEXT, " +
            CAMPO_PERMTRASPASOALMACEN_USUARIO + " INTEGER DEFAULT 0, " + CAMPO_PERMSALIDAALMACEN_USUARIO + " INTEGER DEFAULT 0, " +
            CAMPO_PERMSELECTPRICE_USUARIO + " INTEGER DEFAULT 0, " + CAMPO_PERMPREPEDIDO_USUARIO + " INTEGER DEFAULT 0, " +
            CAMPO_TODOSLOSCLIENTES_USUARIO + " INTEGER DEFAULT 0, " + CAMPO_ENTERPRISEID_USUARIO + " INT NOT NULL DEFAULT 0, " +
            CAMPO_VENDERSINEXISTENCIAS_USUARIO + " INT NOT NULL DEFAULT 0, " + CAMPO_VENDERACREDITO_USUARIO + " INT NOT NULL DEFAULT 0, " +
            CAMPO_DESCMAXIMO_USUARIO + " NUMERIC NOT NULL DEFAULT 0)";

        public static void validateAndCreateNewFieldsInAgents(SQLiteConnection db)
        {
            String queryItem = "SELECT * FROM " + TABLA_USUARIO;
            using (SQLiteCommand command = new SQLiteCommand(queryItem, db))
            {
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    int campos = reader.FieldCount;  // see if the column is there
                    if (campos == 38) {
                        SQLiteCommand cmd = new SQLiteCommand(db);
                        cmd.CommandText = "ALTER TABLE " + TABLA_USUARIO + " ADD " + CAMPO_PERMFACTURARACREDITO_USUARIO + " INTEGER DEFAULT 0";
                        cmd.ExecuteNonQuery();
                        cmd.CommandText = "ALTER TABLE " + TABLA_USUARIO + " ADD " + CAMPO_LASTSEEN_USUARIO + " TEXT";
                        cmd.ExecuteNonQuery();
                        cmd.CommandText = "ALTER TABLE " + TABLA_USUARIO + " ADD " + CAMPO_PERMTRASPASOALMACEN_USUARIO + " INTEGER DEFAULT 0";
                        cmd.ExecuteNonQuery();
                        cmd.CommandText = "ALTER TABLE " + TABLA_USUARIO + " ADD " + CAMPO_PERMSALIDAALMACEN_USUARIO + " INTEGER DEFAULT 0";
                        cmd.ExecuteNonQuery();
                        cmd.CommandText = "ALTER TABLE " + TABLA_USUARIO + " ADD " + CAMPO_PERMSELECTPRICE_USUARIO + " INTEGER DEFAULT 0";
                        cmd.ExecuteNonQuery();
                        cmd.CommandText = "ALTER TABLE " + TABLA_USUARIO + " ADD " + CAMPO_PERMPREPEDIDO_USUARIO + " INTEGER DEFAULT 0";
                        cmd.ExecuteNonQuery();
                        cmd.CommandText = "ALTER TABLE " + TABLA_USUARIO + " ADD " + CAMPO_TODOSLOSCLIENTES_USUARIO + " INTEGER DEFAULT 0";
                        cmd.ExecuteNonQuery();
                    }
                    if (campos == 39)
                    {
                        SQLiteCommand cmd = new SQLiteCommand(db);
                        cmd.CommandText = "ALTER TABLE " + TABLA_USUARIO + " ADD " + CAMPO_LASTSEEN_USUARIO + " TEXT";
                        cmd.ExecuteNonQuery();
                        cmd.CommandText = "ALTER TABLE " + TABLA_USUARIO + " ADD " + CAMPO_PERMTRASPASOALMACEN_USUARIO + " INTEGER DEFAULT 0";
                        cmd.ExecuteNonQuery();
                        cmd.CommandText = "ALTER TABLE " + TABLA_USUARIO + " ADD " + CAMPO_PERMSALIDAALMACEN_USUARIO + " INTEGER DEFAULT 0";
                        cmd.ExecuteNonQuery();
                        cmd.CommandText = "ALTER TABLE " + TABLA_USUARIO + " ADD " + CAMPO_PERMSELECTPRICE_USUARIO + " INTEGER DEFAULT 0";
                        cmd.ExecuteNonQuery();
                        cmd.CommandText = "ALTER TABLE " + TABLA_USUARIO + " ADD " + CAMPO_PERMPREPEDIDO_USUARIO + " INTEGER DEFAULT 0";
                        cmd.ExecuteNonQuery();
                        cmd.CommandText = "ALTER TABLE " + TABLA_USUARIO + " ADD " + CAMPO_TODOSLOSCLIENTES_USUARIO + " INTEGER DEFAULT 0";
                        cmd.ExecuteNonQuery();
                    }
                    if (campos == 44)
                    {
                        SQLiteCommand cmd = new SQLiteCommand(db);
                        cmd.CommandText = "ALTER TABLE " + TABLA_USUARIO + " ADD " + CAMPO_TODOSLOSCLIENTES_USUARIO + " INTEGER DEFAULT 0";
                        cmd.ExecuteNonQuery();
                    }
                    if (campos == 45)
                    {
                        SQLiteCommand cmd = new SQLiteCommand(db);
                        cmd.CommandText = "ALTER TABLE " + TABLA_USUARIO + " ADD " + CAMPO_ENTERPRISEID_USUARIO + " INT NOT NULL DEFAULT 0";
                        cmd.ExecuteNonQuery();
                    }
                    if (campos == 46)
                    {
                        SQLiteCommand cmd = new SQLiteCommand(db);
                        cmd.CommandText = "ALTER TABLE " + TABLA_USUARIO + " ADD " + CAMPO_VENDERSINEXISTENCIAS_USUARIO + " INT NOT NULL DEFAULT 0";
                        cmd.ExecuteNonQuery();
                        cmd.CommandText = "ALTER TABLE " + TABLA_USUARIO + " ADD " + CAMPO_VENDERACREDITO_USUARIO + " INT NOT NULL DEFAULT 0";
                        cmd.ExecuteNonQuery();
                    }
                    if (campos == 48)
                    {
                        SQLiteCommand cmd = new SQLiteCommand(db);
                        cmd.CommandText = "ALTER TABLE " + TABLA_USUARIO + " ADD " + CAMPO_DESCMAXIMO_USUARIO + " NUMERIC NOT NULL DEFAULT 0";
                        cmd.ExecuteNonQuery();
                    }
                    if (reader != null && !reader.IsClosed)
                        reader.Close();
                }
            }
        }

        public const String TABLA_CLIENTES = "Customers";
        public const String CAMPO_ID_CLIENTE = "CLIENTE_ID";
        public const String CAMPO_NOMBRECLIENTE = "NOMBRE";
        public const String CAMPO_CLAVECLIENTE = "CLAVE";
        public const String CAMPO_LIMITECREDITO_CLIENTE = "LIMITE_CREDITO";
        public const String CAMPO_CONDICIONPAGO = "CONDICIONPAGO";
        public const String CAMPO_LISTAPRECIO = "LISTAPRECIO";
        public const String CAMPO_DIRECCION = "DIRECCION";
        public const String CAMPO_TELEFONO_CLIENTE = "TELEFONO";
        public const String CAMPO_PRECIOEMPRESA_ID_CLIENTE = "PRECIO_EMPRESA_ID";
        public const String CAMPO_ZONACLIENTE_ID = "ZONA_CLIENTE_ID";
        public const String CAMPO_AVAL = "AVAL";
        public const String CAMPO_REFERENCIA = "REFERENCIA";
        public const String CAMPO_FORMA_COBRO = "FORMA_COBRO";
        public const String CAMPO_FORMA_COBRO_CC = "FORMA_COBRO_CC_ID";
        public const String CAMPO_HISTORIA_CREDITO = "HISTORIA_CREDITO";
        public const String CAMPO_CLAVEQR = "CLAVEQR";
        public const String CAMPO_CLIVISITADO = "CLIVISITADO";
        public const String CAMPO_CLINOVISITADO_CLIENTE = "CLINOVISITADO";
        public const String CAMPO_PAPELERARECICLAJE_CLIENTE = "papelera_reciclaje";
        public const String CAMPO_PHOTONAME_CLIENTE = "photo";
        public const String CAMPO_RFC_CLIENTE = "rfc";
        public const String CAMPO_CURP_CLIENTE = "curp";
        public const String CAMPO_DENOMINACIONCOMCERCIAL_CLIENTE = "denominacion_comercial";
        public const String CAMPO_FECHAALTA_CLIENTE = "fecha_alta";
        public const String CAMPO_REPRELEGAL_CLIENTE = "representante_legal";
        public const String CAMPO_MONEDAID_CLIENTE = "monedaId";
        public const String CAMPO_DESCDOCTO_CLIENTE = "descuentoDocto";
        public const String CAMPO_DESCMOVTO_CLIENTE = "descuentoMovto";
        public const String CAMPO_BANCREDITO_CLIENTE = "banCredito";
        public const String CAMPO_CLASIFICACIONID1_CLIENTE = "clasificacionId1";
        public const String CAMPO_CLASIFICACIONID2_CLIENTE = "clasificacionId2";
        public const String CAMPO_CLASIFICACIONID3_CLIENTE = "clasificacionId3";
        public const String CAMPO_CLASIFICACIONID4_CLIENTE = "clasificacionId4";
        public const String CAMPO_CLASIFICACIONID5_CLIENTE = "clasificacionId5";
        public const String CAMPO_CLASIFICACIONID6_CLIENTE = "clasificacionId6";
        public const String CAMPO_TIPO_CLIENTE = "tipo";
        public const String CAMPO_STATUS_CLIENTE = "status";
        public const String CAMPO_FECHABAJA_CLIENTE = "fechaBaja";
        public const String CAMPO_DIASDECREDITO_CLIENTE = "diasDeCredito";
        public const String CAMPO_EXCEDERCREDITO_CLIENTE = "excederCredito";
        public const String CAMPO_DESCPRONTOPAGO_CLIENTE = "descuentoProntoPago";
        public const String CAMPO_DIASPRONTOPAGO_CLIENTE = "diasProntoPago";
        public const String CAMPO_INTERESMORATORIO_CLIENTE = "interesMoratorio";
        public const String CAMPO_MENSAJERIA_CLIENTE = "mensajeria";
        public const String CAMPO_CUENTAMENSAJERIA_CLIENTE = "cuentaMensajeria";
        public const String CAMPO_ALMACENID_CLIENTE = "almacenId";
        public const String CAMPO_AGENTEVENTAID_CLIENTE = "agenteVentaId";
        public const String CAMPO_AGENTECOBROID_CLIENTE = "agenteCobroId";
        public const String CAMPO_RESTRICCIONAGENTE_CLIENTE = "restriccionAgente";
        public const String CAMPO_IMP1_CLIENTE = "imp1";
        public const String CAMPO_IMP2_CLIENTE = "imp2";
        public const String CAMPO_IMP3_CLIENTE = "imp3";
        public const String CAMPO_RETEN1_CLIENTE = "reten1";
        public const String CAMPO_RETEN2_CLIENTE = "reten2";
        public const String CAMPO_TIPOCONTRIBUYENTE_CLIENTE = "tipoContribuyente";
        public const String CAMPO_CODIGOREGIMENFISCAL_CLIENTE = "codigoRegimenFiscal";
        public const String CAMPO_CODIGOUSOCFDI_CLIENTE = "codigoUsoCFDI";

        public static String CREAR_TABLA_CLIENTES = "CREATE TABLE IF NOT EXISTS " +
            "" + TABLA_CLIENTES + " (" + CAMPO_ID_CLIENTE + " " + "INTEGER," + CAMPO_NOMBRECLIENTE + " TEXT," + CAMPO_CLAVECLIENTE + " TEXT," +
            CAMPO_LIMITECREDITO_CLIENTE + " INTEGER," + CAMPO_CONDICIONPAGO + " TEXT," + CAMPO_LISTAPRECIO + " TEXT," + CAMPO_DIRECCION + " TEXT," +
            CAMPO_TELEFONO_CLIENTE + " TEXT," + CAMPO_PRECIOEMPRESA_ID_CLIENTE + " INTEGER," + CAMPO_ZONACLIENTE_ID + " INTEGER," + CAMPO_AVAL + " TEXT," +
            CAMPO_REFERENCIA + " TEXT," + CAMPO_FORMA_COBRO + " TEXT," + CAMPO_FORMA_COBRO_CC + " INTEGER," + CAMPO_HISTORIA_CREDITO + " TEXT," +
            CAMPO_CLAVEQR + " TEXT, " + CAMPO_CLIVISITADO + " INTEGER DEFAULT 0, " + CAMPO_CLINOVISITADO_CLIENTE + " INTEGER DEFAULT 0, " +
            CAMPO_PAPELERARECICLAJE_CLIENTE + " INTEGER DEFAULT 0, " + CAMPO_PHOTONAME_CLIENTE + " TEXT, " +
            CAMPO_RFC_CLIENTE + " TEXT, " + CAMPO_CURP_CLIENTE + " TEXT, " + CAMPO_DENOMINACIONCOMCERCIAL_CLIENTE + " TEXT, " +
            CAMPO_FECHAALTA_CLIENTE + " TEXT DEFAULT '', " + CAMPO_REPRELEGAL_CLIENTE + " TEXT DEFAULT '', " +
            CAMPO_MONEDAID_CLIENTE + " INT NOT NULL DEFAULT 0, " + CAMPO_DESCDOCTO_CLIENTE + " NUMERIC NOT NULL DEFAULT 0, " +
            CAMPO_DESCMOVTO_CLIENTE + " NUMERIC NOT NULL DEFAULT 0, " + CAMPO_BANCREDITO_CLIENTE + " INT NOT NULL DEFAULT 0, " +
            CAMPO_CLASIFICACIONID1_CLIENTE + " INT NOT NULL DEFAULT 0, " + CAMPO_CLASIFICACIONID2_CLIENTE + " INT NOT NULL DEFAULT 0, " +
            CAMPO_CLASIFICACIONID3_CLIENTE + " INT NOT NULL DEFAULT 0, " + CAMPO_CLASIFICACIONID4_CLIENTE + " INT NOT NULL DEFAULT 0, " +
            CAMPO_CLASIFICACIONID5_CLIENTE + " INT NOT NULL DEFAULT 0, " + CAMPO_CLASIFICACIONID6_CLIENTE + " INT NOT NULL DEFAULT 0, " +
            CAMPO_TIPO_CLIENTE + " INT NOT NULL DEFAULT 0, " + CAMPO_STATUS_CLIENTE + " INT NOT NULL DEFAULT 0, " +
            CAMPO_FECHABAJA_CLIENTE + " TEXT DEFAULT '', " + CAMPO_DIASDECREDITO_CLIENTE + " INT NOT NULL DEFAULT 0, " +
            CAMPO_EXCEDERCREDITO_CLIENTE + " INT NOT NULL DEFAULT 0, " + CAMPO_DESCPRONTOPAGO_CLIENTE + " NUMERIC NOT NULL DEFAULT 0, " +
            CAMPO_DIASPRONTOPAGO_CLIENTE + " INT NOT NULL DEFAULT 0, " + CAMPO_INTERESMORATORIO_CLIENTE + " NUMERIC NOT NULL DEFAULT 0, " +
            CAMPO_MENSAJERIA_CLIENTE + " TEXT DEFAULT '', " + CAMPO_CUENTAMENSAJERIA_CLIENTE + " TEXT DEFAULT '', " +
            CAMPO_ALMACENID_CLIENTE + " INT NOT NULL DEFAULT 0, " + CAMPO_AGENTEVENTAID_CLIENTE + " INT NOT NULL DEFAULT 0, " +
            CAMPO_AGENTECOBROID_CLIENTE + " INT NOT NULL DEFAULT 0, " + CAMPO_RESTRICCIONAGENTE_CLIENTE + " INT NOT NULL DEFAULT 0, " +
            CAMPO_IMP1_CLIENTE + " NUMERIC NOT NULL DEFAULT 0, " + CAMPO_IMP2_CLIENTE + " NUMERIC NOT NULL DEFAULT 0, " +
            CAMPO_IMP3_CLIENTE + " NUMERIC NOT NULL DEFAULT 0, " + CAMPO_RETEN1_CLIENTE + " NUMERIC NOT NULL DEFAULT 0, " +
            CAMPO_RETEN2_CLIENTE + " NUMERIC NOT NULL DEFAULT 0, " + CAMPO_TIPOCONTRIBUYENTE_CLIENTE + " INTEGER NOT NULL DEFAULT 0, " +
            CAMPO_CODIGOREGIMENFISCAL_CLIENTE + " VARCHAR(10) NOT NULL DEFAULT '', " +
            CAMPO_CODIGOUSOCFDI_CLIENTE + " VARCHAR(10) NOT NULL DEFAULT '')";
        
        public static void validateOrCreateFieldsInCustomers(SQLiteConnection db)
        {
            String queryItem = "SELECT * FROM " + LocalDatabase.TABLA_CLIENTES;// grab cursor for all data
            using (SQLiteCommand command = new SQLiteCommand(queryItem, db))
            {
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    int columns = reader.FieldCount;  // see if the column is there
                    if (columns == 20)
                    {
                        SQLiteCommand cmd = new SQLiteCommand(db);
                        cmd.CommandText = "ALTER TABLE " + TABLA_CLIENTES + " ADD " + CAMPO_RFC_CLIENTE + " TEXT";
                        cmd.ExecuteNonQuery();
                        cmd.CommandText = "ALTER TABLE " + TABLA_CLIENTES + " ADD " + CAMPO_CURP_CLIENTE + " TEXT";
                        cmd.ExecuteNonQuery();
                        cmd.CommandText = "ALTER TABLE " + TABLA_CLIENTES + " ADD " + CAMPO_DENOMINACIONCOMCERCIAL_CLIENTE + " TEXT";
                        cmd.ExecuteNonQuery();
                    }
                    if (columns == 21)
                    {
                        SQLiteCommand cmd = new SQLiteCommand(db);
                        cmd.CommandText = "ALTER TABLE " + TABLA_CLIENTES + " ADD " + CAMPO_CURP_CLIENTE + " TEXT";
                        cmd.ExecuteNonQuery();
                        cmd.CommandText = "ALTER TABLE " + TABLA_CLIENTES + " ADD " + CAMPO_DENOMINACIONCOMCERCIAL_CLIENTE + " TEXT";
                        cmd.ExecuteNonQuery();
                    }
                    if (columns == 22)
                    {
                        SQLiteCommand cmd = new SQLiteCommand(db);
                        cmd.CommandText = "ALTER TABLE " + TABLA_CLIENTES + " ADD " + CAMPO_DENOMINACIONCOMCERCIAL_CLIENTE + " TEXT";
                        cmd.ExecuteNonQuery();
                    }
                    if (columns == 23)
                    {
                        SQLiteCommand cmd = new SQLiteCommand(db);
                        cmd.CommandText = "ALTER TABLE " + TABLA_CLIENTES + " ADD " + CAMPO_FECHAALTA_CLIENTE + " TEXT DEFAULT ''";
                        cmd.ExecuteNonQuery();
                        cmd.CommandText = "ALTER TABLE " + TABLA_CLIENTES + " ADD " + CAMPO_REPRELEGAL_CLIENTE + " TEXT DEFAULT ''";
                        cmd.ExecuteNonQuery();
                        cmd.CommandText = "ALTER TABLE " + TABLA_CLIENTES + " ADD " + CAMPO_MONEDAID_CLIENTE + " INT NOT NULL DEFAULT 0";
                        cmd.ExecuteNonQuery();
                        cmd.CommandText = "ALTER TABLE " + TABLA_CLIENTES + " ADD " + CAMPO_DESCDOCTO_CLIENTE + " NUMERIC NOT NULL DEFAULT 0";
                        cmd.ExecuteNonQuery();
                        cmd.CommandText = "ALTER TABLE " + TABLA_CLIENTES + " ADD " + CAMPO_DESCMOVTO_CLIENTE + " NUMERIC NOT NULL DEFAULT 0";
                        cmd.ExecuteNonQuery();
                        cmd.CommandText = "ALTER TABLE " + TABLA_CLIENTES + " ADD " + CAMPO_BANCREDITO_CLIENTE + " INT NOT NULL DEFAULT 0";
                        cmd.ExecuteNonQuery();
                        cmd.CommandText = "ALTER TABLE " + TABLA_CLIENTES + " ADD " + CAMPO_CLASIFICACIONID1_CLIENTE + " INT NOT NULL DEFAULT 0";
                        cmd.ExecuteNonQuery();
                        cmd.CommandText = "ALTER TABLE " + TABLA_CLIENTES + " ADD " + CAMPO_CLASIFICACIONID2_CLIENTE + " INT NOT NULL DEFAULT 0";
                        cmd.ExecuteNonQuery();
                        cmd.CommandText = "ALTER TABLE " + TABLA_CLIENTES + " ADD " + CAMPO_CLASIFICACIONID3_CLIENTE + " INT NOT NULL DEFAULT 0";
                        cmd.ExecuteNonQuery();
                        cmd.CommandText = "ALTER TABLE " + TABLA_CLIENTES + " ADD " + CAMPO_CLASIFICACIONID4_CLIENTE + " INT NOT NULL DEFAULT 0";
                        cmd.ExecuteNonQuery();
                        cmd.CommandText = "ALTER TABLE " + TABLA_CLIENTES + " ADD " + CAMPO_CLASIFICACIONID5_CLIENTE + " INT NOT NULL DEFAULT 0";
                        cmd.ExecuteNonQuery();
                        cmd.CommandText = "ALTER TABLE " + TABLA_CLIENTES + " ADD " + CAMPO_CLASIFICACIONID6_CLIENTE + " INT NOT NULL DEFAULT 0";
                        cmd.ExecuteNonQuery();
                        cmd.CommandText = "ALTER TABLE " + TABLA_CLIENTES + " ADD " + CAMPO_TIPO_CLIENTE + " INT NOT NULL DEFAULT 0";
                        cmd.ExecuteNonQuery();
                        cmd.CommandText = "ALTER TABLE " + TABLA_CLIENTES + " ADD " + CAMPO_STATUS_CLIENTE + " INT NOT NULL DEFAULT 0";
                        cmd.ExecuteNonQuery();
                        cmd.CommandText = "ALTER TABLE " + TABLA_CLIENTES + " ADD " + CAMPO_FECHABAJA_CLIENTE + " TEXT DEFAULT ''";
                        cmd.ExecuteNonQuery();
                        cmd.CommandText = "ALTER TABLE " + TABLA_CLIENTES + " ADD " + CAMPO_DIASDECREDITO_CLIENTE + " INT NOT NULL DEFAULT 0";
                        cmd.ExecuteNonQuery();
                        cmd.CommandText = "ALTER TABLE " + TABLA_CLIENTES + " ADD " + CAMPO_EXCEDERCREDITO_CLIENTE + " INT NOT NULL DEFAULT 0";
                        cmd.ExecuteNonQuery();
                        cmd.CommandText = "ALTER TABLE " + TABLA_CLIENTES + " ADD " + CAMPO_DESCPRONTOPAGO_CLIENTE + " NUMERIC NOT NULL DEFAULT 0";
                        cmd.ExecuteNonQuery();
                        cmd.CommandText = "ALTER TABLE " + TABLA_CLIENTES + " ADD " + CAMPO_DIASPRONTOPAGO_CLIENTE + " INT NOT NULL DEFAULT 0";
                        cmd.ExecuteNonQuery();
                        cmd.CommandText = "ALTER TABLE " + TABLA_CLIENTES + " ADD " + CAMPO_INTERESMORATORIO_CLIENTE + " NUMERIC NOT NULL DEFAULT 0";
                        cmd.ExecuteNonQuery();
                        cmd.CommandText = "ALTER TABLE " + TABLA_CLIENTES + " ADD " + CAMPO_MENSAJERIA_CLIENTE + " TEXT DEFAULT ''";
                        cmd.ExecuteNonQuery();
                        cmd.CommandText = "ALTER TABLE " + TABLA_CLIENTES + " ADD " + CAMPO_CUENTAMENSAJERIA_CLIENTE + " TEXT DEFAULT ''";
                        cmd.ExecuteNonQuery();
                        cmd.CommandText = "ALTER TABLE " + TABLA_CLIENTES + " ADD " + CAMPO_ALMACENID_CLIENTE + " INT NOT NULL DEFAULT 0";
                        cmd.ExecuteNonQuery();
                        cmd.CommandText = "ALTER TABLE " + TABLA_CLIENTES + " ADD " + CAMPO_AGENTEVENTAID_CLIENTE + " INT NOT NULL DEFAULT 0";
                        cmd.ExecuteNonQuery();
                        cmd.CommandText = "ALTER TABLE " + TABLA_CLIENTES + " ADD " + CAMPO_AGENTECOBROID_CLIENTE + " INT NOT NULL DEFAULT 0";
                        cmd.ExecuteNonQuery();
                        cmd.CommandText = "ALTER TABLE " + TABLA_CLIENTES + " ADD " + CAMPO_RESTRICCIONAGENTE_CLIENTE + " INT NOT NULL DEFAULT 0";
                        cmd.ExecuteNonQuery();
                        cmd.CommandText = "ALTER TABLE " + TABLA_CLIENTES + " ADD " + CAMPO_IMP1_CLIENTE + " NUMERIC NOT NULL DEFAULT 0";
                        cmd.ExecuteNonQuery();
                        cmd.CommandText = "ALTER TABLE " + TABLA_CLIENTES + " ADD " + CAMPO_IMP2_CLIENTE + " NUMERIC NOT NULL DEFAULT 0";
                        cmd.ExecuteNonQuery();
                        cmd.CommandText = "ALTER TABLE " + TABLA_CLIENTES + " ADD " + CAMPO_IMP3_CLIENTE + " NUMERIC NOT NULL DEFAULT 0";
                        cmd.ExecuteNonQuery();
                        cmd.CommandText = "ALTER TABLE " + TABLA_CLIENTES + " ADD " + CAMPO_RETEN1_CLIENTE + " NUMERIC NOT NULL DEFAULT 0";
                        cmd.ExecuteNonQuery();
                        cmd.CommandText = "ALTER TABLE " + TABLA_CLIENTES + " ADD " + CAMPO_RETEN2_CLIENTE + " NUMERIC NOT NULL DEFAULT 0";
                        cmd.ExecuteNonQuery();
                    }
                    if (columns == 54)
                    {
                        SQLiteCommand cmd = new SQLiteCommand(db);
                        cmd.CommandText = "ALTER TABLE " + TABLA_CLIENTES + " ADD " + CAMPO_TIPOCONTRIBUYENTE_CLIENTE + " INT NOT NULL DEFAULT 0";
                        cmd.ExecuteNonQuery();
                        cmd.CommandText = "ALTER TABLE " + TABLA_CLIENTES + " ADD " + CAMPO_CODIGOREGIMENFISCAL_CLIENTE + " TEXT NOT NULL DEFAULT ''";
                        cmd.ExecuteNonQuery();
                        cmd.CommandText = "ALTER TABLE " + TABLA_CLIENTES + " ADD " + CAMPO_CODIGOUSOCFDI_CLIENTE + " TEXT NOT NULL DEFAULT ''";
                        cmd.ExecuteNonQuery();
                    }
                    if (reader != null && !reader.IsClosed)
                        reader.Close();
                }
            }
        }

        public const String TABLA_REGIMEN_FISCAL = "RegimenFiscal";
        public const String CAMPO_ID_REGIMEN_FISCAL = "id";
        public const String CAMPO_VALOR_REGIMEN_FISCAL = "valor";
        public const String CAMPO_NOMBRE_REGIMEN_FISCAL = "nombre";
        public const String CAMPO_DESCRIPCION_REGIMEN_FISCAL = "descripcion";
        public const String CAMPO_FECHA_ALTA_REGIMEN_FISCAL = "fechaAlta";
        public const String CAMPO_FECHA_REGIMEN_FISCAL = "fechaModificacion";
        public const String CAMPO_MORAL_REGIMEN_FISCAL = "moral";
        public const String CAMPO_FISICA_REGIMEN_FISCAL = "fisica";

        public static String CREAR_TABLA_REGIMEN_FISCAL = "CREATE TABLE IF NOT EXISTS " +
            "" + TABLA_REGIMEN_FISCAL +
            " (" + CAMPO_ID_REGIMEN_FISCAL + " INTEGER NOT NULL PRIMARY KEY ," +
            CAMPO_VALOR_REGIMEN_FISCAL + " TEXT NOT NULL, " +
            CAMPO_NOMBRE_REGIMEN_FISCAL + " TEXT NOT NULL, " +
            CAMPO_DESCRIPCION_REGIMEN_FISCAL + " TEXT DEFAULT '', " +
            CAMPO_FECHA_ALTA_REGIMEN_FISCAL + " TEXT NOT NULL DEFAULT '', " +
            CAMPO_FECHA_REGIMEN_FISCAL + " TEXT NOT NULL DEFAULT '', " +
            CAMPO_MORAL_REGIMEN_FISCAL + " INTEGER NOT NULL DEFAULT 0, " +
            CAMPO_FISICA_REGIMEN_FISCAL + " INTEGER NOT NULL DEFAULT 0); ";


        public const String TABLA_USO_CFDI = "UsoCFDI";
        public const String CAMPO_ID_USO_CFDI = "id";
        public const String CAMPO_VALOR_USO_CFDI = "valor";
        public const String CAMPO_NOMBRE_USO_CFDI = "nombre";
        public const String CAMPO_DESCRIPCION_USO_CFDI = "descripcion";
        public const String CAMPO_FECHA_ALTA_USO_CFDI = "fechaAlta";
        public const String CAMPO_FECHA_USO_CFDI = "fechaModificacion";
        public const String CAMPO_MORAL_USO_CFDI = "moral";
        public const String CAMPO_FISICA_USO_CFDI = "fisica";

        public static String CREAR_TABLA_USO_CFDI = "CREATE TABLE IF NOT EXISTS " +
           "" + TABLA_USO_CFDI +
           " (" + CAMPO_ID_USO_CFDI + " INTEGER NOT NULL PRIMARY KEY ," +
           CAMPO_VALOR_USO_CFDI + " TEXT NOT NULL DEFAULT '', " +
           CAMPO_NOMBRE_USO_CFDI + " TEXT NOT NULL DEFAULT '', " +
           CAMPO_DESCRIPCION_USO_CFDI + " TEXT DEFAULT '', " +
           CAMPO_FECHA_ALTA_USO_CFDI + " TEXT NOT NULL DEFAULT '', " +
           CAMPO_FECHA_USO_CFDI + " TEXT NOT NULL DEFAULT '', " +
           CAMPO_MORAL_USO_CFDI + " INTEGER NOT NULL DEFAULT 0, " +
           CAMPO_FISICA_USO_CFDI + " INTEGER NOT NULL DEFAULT 0); ";

        public const String TABLA_ITEM = "Item";
        public const String CAMPO_ID_ITEM = "id";
        public const String CAMPO_NOMBRE_ITEM = "name";
        public const String CAMPO_CODIGO_ITEM = "code";
        public const String CAMPO_CLASIFICATIONID1_ITEM = "clasification_1";
        public const String CAMPO_CLASIFICATIONID2_ITEM = "clasification_2";
        public const String CAMPO_CLASIFICATIONID3_ITEM = "clasification_3";
        public const String CAMPO_CLASIFICATIONID4_ITEM = "clasification_4";
        public const String CAMPO_CLASIFICATIONID5_ITEM = "clasification_5";
        public const String CAMPO_CLASIFICATIONID6_ITEM = "clasification_6";
        public const String CAMPO_STOCK_ITEM = "stock";
        public const String CAMPO_ORDENAR_ITEM = "ordenar";
        public const String CAMPO_CONSOLIDADO_ITEM = "consolidated";
        public const String CAMPO_DESCMAX_ITEM = "maximum_discount";
        public const String CAMPO_PRECIO1_ITEM = "precio_1";
        public const String CAMPO_PRECIO2_ITEM = "precio_2";
        public const String CAMPO_PRECIO3_ITEM = "precio_3";
        public const String CAMPO_PRECIO4_ITEM = "precio_4";
        public const String CAMPO_PRECIO5_ITEM = "precio_5";
        public const String CAMPO_PRECIO6_ITEM = "precio_6";
        public const String CAMPO_PRECIO7_ITEM = "precio_7";
        public const String CAMPO_PRECIO8_ITEM = "precio_8";
        public const String CAMPO_PRECIO9_ITEM = "precio_9";
        public const String CAMPO_PRECIO10_ITEM = "precio_10";
        public const String CAMPO_AGREGADO_ITEM = "agregado";
        public const String CAMPO_BASEUNITID_ITEM = "base_unit_id";
        public const String CAMPO_NONCONVERTIBLEUNITID_ITEM = "non_convertible_unit_id";
        public const String CAMPO_PURCHASEUNITID_ITEM = "purchase_unit_id";
        public const String CAMPO_SALESUNITID_ITEM = "sales_unit_id";
        public const String CAMPO_CODIGOALTERNO_ITEM = "alternate_code";
        public const String CAMPO_FISCALPRODUCT_ITEM = "fiscal_product";
        public const String CAMPO_IMP1_ITEM = "impuesto1";
        public const String CAMPO_IMP2_ITEM = "impuesto2";
        public const String CAMPO_IMP3_ITEM = "impuesto3";
        public const String CAMPO_RETEN1_ITEM = "retencion1";
        public const String CAMPO_RETEN2_ITEM = "retencion2";
        public const String CAMPO_IMP1EXCENTO_ITEM = "imp1Excento";
        public const String CAMPO_IMP2CUOTA_ITEM = "imp2Cuota";
        public const String CAMPO_IMP3CUOTA_ITEM = "imp3Cuota";
        public const String CAMPO_TEXTEXTRA1_ITEM = "textoExtra1";
        public const String CAMPO_TEXTEXTRA2_ITEM = "textoExtra2";
        public const String CAMPO_TEXTEXTRA3_ITEM = "textoExtra3";
        public const String CAMPO_IMPORTEEXTRA1_ITEM = "importeExtra1";
        public const String CAMPO_IMPORTEEXTRA2_ITEM = "importeExtra2";
        public const String CAMPO_IMPORTEEXTRA3_ITEM = "importeExtra3";
        public const String CAMPO_IMPORTEEXTRA4_ITEM = "importeExtra4";
        public const String CAMPO_CANTIDADFISCAL_ITEM = "cantidadFiscal";

        public static String CREAR_TABLA_ARTICULOS = "CREATE TABLE IF NOT EXISTS " +
            TABLA_ITEM + " (" + CAMPO_ID_ITEM + " INTEGER PRIMARY KEY," + CAMPO_NOMBRE_ITEM + " TEXT, " + CAMPO_CODIGO_ITEM + " TEXT, " +
            CAMPO_CLASIFICATIONID1_ITEM + " INTEGER, " + CAMPO_CLASIFICATIONID2_ITEM + " INTEGER, " + CAMPO_CLASIFICATIONID3_ITEM + " INTEGER, " +
            CAMPO_CLASIFICATIONID4_ITEM + " INTEGER, " + CAMPO_CLASIFICATIONID5_ITEM + " INTEGER, " + CAMPO_CLASIFICATIONID6_ITEM + " INTEGER, " +
            CAMPO_STOCK_ITEM + " NUMERIC, " + CAMPO_ORDENAR_ITEM + " INTEGER, " + CAMPO_CONSOLIDADO_ITEM + " INTEGER, " +
            CAMPO_DESCMAX_ITEM + " NUMERIC, " +
            CAMPO_PRECIO1_ITEM + " NUMERIC DEFAULT 0, " + CAMPO_PRECIO2_ITEM + " NUMERIC DEFAULT 0, " +
            CAMPO_PRECIO3_ITEM + " NUMERIC DEFAULT 0, " + CAMPO_PRECIO4_ITEM + " NUMERIC DEFAULT 0, " +
            CAMPO_PRECIO5_ITEM + " NUMERIC DEFAULT 0, " + CAMPO_PRECIO6_ITEM + " NUMERIC DEFAULT 0, " +
            CAMPO_PRECIO7_ITEM + " NUMERIC DEFAULT 0, " + CAMPO_PRECIO8_ITEM + " NUMERIC DEFAULT 0, " +
            CAMPO_PRECIO9_ITEM + " NUMERIC DEFAULT 0, " + CAMPO_PRECIO10_ITEM + " NUMERIC DEFAULT 0, " +
            CAMPO_AGREGADO_ITEM + " INTEGER DEFAULT 0, " + CAMPO_BASEUNITID_ITEM + " INTEGER DEFAULT 0, " +
            CAMPO_NONCONVERTIBLEUNITID_ITEM + " INTEGER DEFAULT 0, " + CAMPO_PURCHASEUNITID_ITEM + " INTEGER DEFAULT 0, " +
            CAMPO_SALESUNITID_ITEM + " INTEGER DEFAULT 0, " + CAMPO_CODIGOALTERNO_ITEM + " TEXT, " +
            CAMPO_FISCALPRODUCT_ITEM + " INTEGER DEFAULT 0, " + CAMPO_IMP1_ITEM + " NUMERIC DEFAULT 0, " + CAMPO_IMP2_ITEM + " NUMERIC DEFAULT 0, " +
            CAMPO_IMP3_ITEM + " NUMERIC DEFAULT 0, " + CAMPO_RETEN1_ITEM + " NUMERIC DEFAULT 0, " + 
            CAMPO_RETEN2_ITEM + " NUMERIC DEFAULT 0, "+ CAMPO_IMP1EXCENTO_ITEM + " INT NOT NULL DEFAULT 0, " +
            CAMPO_IMP2CUOTA_ITEM+" INT NOT NULL DEFAULT 0, "+CAMPO_IMP3CUOTA_ITEM+" INT NOT NULL DEFAULT 0, " +
            CAMPO_TEXTEXTRA1_ITEM+" TEXT NOT NULL DEFAULT '', "+CAMPO_TEXTEXTRA2_ITEM+" TEXT NOT NULL DEFAULT '', " +
            CAMPO_TEXTEXTRA3_ITEM+" TEXT NOT NULL DEFAULT '', "+CAMPO_IMPORTEEXTRA1_ITEM+" NUMERIC NOT NULL DEFAULT 0, " +
            CAMPO_IMPORTEEXTRA2_ITEM+" NUMERIC NOT NULL DEFAULT 0, "+CAMPO_IMPORTEEXTRA3_ITEM+" NUMERIC NOT NULL DEFAULT 0, " +
            CAMPO_IMPORTEEXTRA4_ITEM+" NUMERIC NOT NULL DEFAULT 0, "+CAMPO_CANTIDADFISCAL_ITEM+" NUMERIC NOT NULL DEFAULT 0)";

        public static void validateAndCreateNewFieldsInItems(SQLiteConnection db)
        {
            String queryItem = "SELECT * FROM " + LocalDatabase.TABLA_ITEM;
            using (SQLiteCommand command = new SQLiteCommand(queryItem, db))
            {
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    int campos = reader.FieldCount;  // see if the column is there
                    if (campos == 28)
                    {
                        SQLiteCommand cmd = new SQLiteCommand(db);
                        cmd.CommandText = "ALTER TABLE " + TABLA_ITEM + " ADD " + CAMPO_CODIGOALTERNO_ITEM + " TEXT";
                        cmd.ExecuteNonQuery();
                        cmd.CommandText = "ALTER TABLE " + TABLA_ITEM + " ADD " + CAMPO_FISCALPRODUCT_ITEM + " INTEGER DEFAULT 0";
                        cmd.ExecuteNonQuery();
                    }
                    if (campos == 29)
                    {
                        SQLiteCommand cmd = new SQLiteCommand(db);
                        cmd.CommandText = "ALTER TABLE " + TABLA_ITEM + " ADD " + CAMPO_FISCALPRODUCT_ITEM + " INTEGER DEFAULT 0";
                        cmd.ExecuteNonQuery();
                    }
                    if (campos == 30)
                    {
                        SQLiteCommand cmd = new SQLiteCommand(db);
                        cmd.CommandText = "ALTER TABLE " + TABLA_ITEM + " ADD " + CAMPO_IMP1_ITEM + " NUMERIC DEFAULT 0";
                        cmd.ExecuteNonQuery();
                        cmd.CommandText = "ALTER TABLE " + TABLA_ITEM + " ADD " + CAMPO_IMP2_ITEM + " NUMERIC DEFAULT 0";
                        cmd.ExecuteNonQuery();
                        cmd.CommandText = "ALTER TABLE " + TABLA_ITEM + " ADD " + CAMPO_IMP3_ITEM + " NUMERIC DEFAULT 0";
                        cmd.ExecuteNonQuery();
                        cmd.CommandText = "ALTER TABLE " + TABLA_ITEM + " ADD " + CAMPO_RETEN1_ITEM + " NUMERIC DEFAULT 0";
                        cmd.ExecuteNonQuery();
                        cmd.CommandText = "ALTER TABLE " + TABLA_ITEM + " ADD " + CAMPO_RETEN2_ITEM + " NUMERIC DEFAULT 0";
                        cmd.ExecuteNonQuery();
                    }
                    if (campos == 35)
                    {
                        SQLiteCommand cmd = new SQLiteCommand(db);
                        cmd.CommandText = "ALTER TABLE " + TABLA_ITEM + " ADD " + CAMPO_IMP1EXCENTO_ITEM + " INT NOT NULL DEFAULT 0";
                        cmd.ExecuteNonQuery();
                        cmd.CommandText = "ALTER TABLE " + TABLA_ITEM + " ADD " + CAMPO_IMP2CUOTA_ITEM + " INT NOT NULL DEFAULT 0";
                        cmd.ExecuteNonQuery();
                        cmd.CommandText = "ALTER TABLE " + TABLA_ITEM + " ADD " + CAMPO_IMP3CUOTA_ITEM + " INT NOT NULL DEFAULT 0";
                        cmd.ExecuteNonQuery();
                        cmd.CommandText = "ALTER TABLE " + TABLA_ITEM + " ADD " + CAMPO_TEXTEXTRA1_ITEM + " TEXT NOT NULL DEFAULT ''";
                        cmd.ExecuteNonQuery();
                        cmd.CommandText = "ALTER TABLE " + TABLA_ITEM + " ADD " + CAMPO_TEXTEXTRA2_ITEM + " TEXT NOT NULL DEFAULT ''";
                        cmd.ExecuteNonQuery();
                        cmd.CommandText = "ALTER TABLE " + TABLA_ITEM + " ADD " + CAMPO_TEXTEXTRA3_ITEM + " TEXT NOT NULL DEFAULT ''";
                        cmd.ExecuteNonQuery();
                        cmd.CommandText = "ALTER TABLE " + TABLA_ITEM + " ADD " + CAMPO_IMPORTEEXTRA1_ITEM + " NUMERIC NOT NULL DEFAULT 0";
                        cmd.ExecuteNonQuery();
                        cmd.CommandText = "ALTER TABLE " + TABLA_ITEM + " ADD " + CAMPO_IMPORTEEXTRA2_ITEM + " NUMERIC NOT NULL DEFAULT 0";
                        cmd.ExecuteNonQuery();
                        cmd.CommandText = "ALTER TABLE " + TABLA_ITEM + " ADD " + CAMPO_IMPORTEEXTRA3_ITEM + " NUMERIC NOT NULL DEFAULT 0";
                        cmd.ExecuteNonQuery();
                        cmd.CommandText = "ALTER TABLE " + TABLA_ITEM + " ADD " + CAMPO_IMPORTEEXTRA4_ITEM + " NUMERIC NOT NULL DEFAULT 0";
                        cmd.ExecuteNonQuery();
                        cmd.CommandText = "ALTER TABLE " + TABLA_ITEM + " ADD " + CAMPO_CANTIDADFISCAL_ITEM + " NUMERIC NOT NULL DEFAULT 0";
                        cmd.ExecuteNonQuery();
                    }
                    if (reader != null && !reader.IsClosed)
                        reader.Close();
                }
            }
        }

        public const String TABLA_RESPALDOTICKETS = "RespTickets";
        public const String CAMPO_ID_RESPALDOTICKETS = "id";
        public const String CAMPO_REFERENCIA_RESPALDOTICKETS = "referencia";
        public const String CAMPO_DATOS_RESPALDOTICKETS = "datos";
        public const String CAMPO_IDAGENTE_RESPALDOTICKETS = "idAgente";
        public const String CAMPO_TIPODOCUMENTO_RESPALDOTICKETS = "tipoDocumento";
        public const String CAMPO_FECHA_RESPALDOTICKETS = "fecha";
        public const String CAMPO_IDWS_RESPALDOTICKETS = "idPanel";
        public const String CAMPO_ESTATUS_RESPALDOTICKETS = "estatus";
        public static String CREAR_TABLA_RESPALDOTICKETS = "CREATE TABLE IF NOT EXISTS " +
        "" + TABLA_RESPALDOTICKETS + "("+ CAMPO_ID_RESPALDOTICKETS + " INTEGER PRIMARY KEY, "+ 
           "" + CAMPO_REFERENCIA_RESPALDOTICKETS + " TEXT, " +
            "" + CAMPO_DATOS_RESPALDOTICKETS + " TEXT, " +
            "" + CAMPO_IDAGENTE_RESPALDOTICKETS + " INTEGER, " +
            "" + CAMPO_TIPODOCUMENTO_RESPALDOTICKETS + " TEXT, " + 
            "" + CAMPO_FECHA_RESPALDOTICKETS + " TEXT, " + 
            "" + CAMPO_IDWS_RESPALDOTICKETS + " INTEGER DEFAULT 0, " +
            "" + CAMPO_ESTATUS_RESPALDOTICKETS + " INTEGER DEFAULT 0" +
            ")";


        public const String TABLA_CXC = "CuentasXcobrar";
        public const String CAMPO_ID_CXC = "id";
        public const String CAMPO_CLIENTE_ID_CXC = "CLIENTE_ID";
        public const String CAMPO_DOCTO_CC_ID_CXC = "DOCTO_CC_ID";
        public const String CAMPO_FOLIO_CXC = "FOLIO";
        public const String CAMPO_DIAS_ATRASO_CXC = "DIAS_ATRASO";
        public const String CAMPO_SALDO_ACTUAL_CXC = "SALDO_ACTUAL";
        public const String CAMPO_FECHA_VENCE_CXC = "FECHA_VENCE";
        public const String CAMPO_DESCRIPCION_CXC = "DESCRIPCION";
        public const String CAMPO_FACTURA_MOSTRADOR_CXC = "FACTURA_MOSTRADOR";
        public const String CAMPO_REFER_MOVTO_BANCARIO_CXC = "REFER_MOVTO_BANCARIO";
        public const String CAMPO_ABONO_CXC = "ABONO";
        public const String CAMPO_ENVIADO_CXC = "ENVIADO";
        public const String CAMPO_FORMA_COBRO_CC_ID_CXC = "FORMA_COBRO_CC_ID";
        public const String CAMPO_BANCO_ID_CXC = "BANCO_ID";
        public const String CAMPO_FECHA_CXC = "FECHA";
        public const String CAMPO_TIPO_CXC = "Tipo";
        public const String CAMPO_CANCELADO_CXC = "Cancelado";
        public const String CAMPO_FOLIOI_CXC = "FOLIOI";
        public const String CAMPO_PAPELERARECICLAJE_CXC = "papelera_reciclaje";

        public static String CREAR_TABLA_CREDITOS = "CREATE TABLE IF NOT EXISTS " +
            "" + TABLA_CXC + " (" + CAMPO_ID_CXC + " INTEGER PRIMARY KEY, " + CAMPO_CLIENTE_ID_CXC + " " + " INTEGER," + CAMPO_DOCTO_CC_ID_CXC + " INTEGER," + CAMPO_FOLIO_CXC + " TEXT," + CAMPO_DIAS_ATRASO_CXC + " INTEGER," +
            "" + CAMPO_SALDO_ACTUAL_CXC + " TEXT," + CAMPO_FECHA_VENCE_CXC + " TEXT," + CAMPO_DESCRIPCION_CXC + " TEXT," + CAMPO_FACTURA_MOSTRADOR_CXC + " TEXT, " +
            CAMPO_REFER_MOVTO_BANCARIO_CXC + " TEXT, " + CAMPO_ABONO_CXC + " NUMERIC DEFAULT 0.0, " + CAMPO_ENVIADO_CXC + " INTEGER DEFAULT 0, " + CAMPO_FORMA_COBRO_CC_ID_CXC + " INTEGER DEFAULT 0, " +
            CAMPO_BANCO_ID_CXC + " INTEGER DEFAULT 1, " + CAMPO_FECHA_CXC + " TEXT, " + CAMPO_TIPO_CXC + " INTEGER DEFAULT 1, " +
            CAMPO_CANCELADO_CXC + " INTEGER DEFAULT 0, " + CAMPO_FOLIOI_CXC + " TEXT, " + CAMPO_PAPELERARECICLAJE_CXC + " INTEGER DEFAULT 0)";

        public const String TABLA_MOVEMENTCXC = "MovementCuentaPorCobrar";
        public const String CAMPO_ID_MOVEMENTCXC = "id";
        public const String CAMPO_CXCID_MOVEMENTCXC = "idCreditDocument";
        public const String CAMPO_ITEMID_MOVEMENTCXC = "itemId";
        public const String CAMPO_ITEMCODE_MOVEMENTCXC = "itemCode";
        public const String CAMPO_NUMBER_MOVEMENTCXC = "number";
        public const String CAMPO_PRICE_MOVEMENTCXC = "price";
        public const String CAMPO_CAPTUREDUNITS_MOVEMENTCXC = "capturedUnits";
        public const String CAMPO_CAPTUREDUNITSID_MOVEMENTCXC = "capturedUnitsId";
        public const String CAMPO_SUBTOTAL_MOVEMENTCXC = "subtotal";
        public const String CAMPO_DESCUENTO_MOVEMENTCXC = "discount";
        public const String CAMPO_TOTAL_MOVEMENTCXC = "total";

        public static String CREATE_MOVEMENTCXC_TABLE = "CREATE TABLE IF NOT EXISTS " + TABLA_MOVEMENTCXC + " (" +
                CAMPO_ID_MOVEMENTCXC + " INTEGER PRIMARY KEY, " + CAMPO_CXCID_MOVEMENTCXC + " INTEGER NOT NULL, " +
                CAMPO_ITEMID_MOVEMENTCXC + " INTEGER, " + CAMPO_ITEMCODE_MOVEMENTCXC + " TEXT, " + CAMPO_NUMBER_MOVEMENTCXC + " INTEGER, " +
                CAMPO_PRICE_MOVEMENTCXC + " NUMERIC, " + CAMPO_CAPTUREDUNITS_MOVEMENTCXC + " NUMERIC, " +
                CAMPO_CAPTUREDUNITSID_MOVEMENTCXC + " INTEGER, " + CAMPO_SUBTOTAL_MOVEMENTCXC + " NUMERIC, " +
                CAMPO_DESCUENTO_MOVEMENTCXC + " NUMERIC, " + CAMPO_TOTAL_MOVEMENTCXC + " NUMERIC)";

        public const String TABLA_FOLIOSDIGITALES = "FoliosDigitales";
        public const String CAMPO_ID_FOLIODIGITAL = "id";
        public const String CAMPO_TIPODOC_FOLIODIGITAL = "tipoDocumento";
        public const String CAMPO_IDCONCEPTO_FOLIODIGITAL = "idConcepto";
        public const String CAMPO_IDDOCUMENTO_FOLIODIGITAL = "idDocumento";
        public const String CAMPO_SERIECONCEPTO_FOLIODIGITAL = "serieConcepto";
        public const String CAMPO_FOLIO_FOLIODIGITAL = "folio";
        public const String CAMPO_ESTADO_FOLIODIGITAL = "estado";//0 dispotible, 1 ocupado no emitido, 2 ocupado emitido, 3 cancelado,
                                                                  //4 por confirmar timbrado, 5 por autorizar cancelacion, 6 por confirmar cancelación,
                                                                  //10 disponible no asociado, 11 recibido no asociado,
                                                                  //12 recibido cancelado, 13 recibido asociado adw, 14 recibido asociado ctw
        public const String CAMPO_ENTREGADO_FOLIODIGITAL = "entregado";
        public const String CAMPO_FECHAEMISION_FOLIODIGITAL = "fechaEmision";
        public const String CAMPO_EMAILENTREGA_FOLIODIGITAL = "emailEntrega";
        public const String CAMPO_RUTADISCOENTREGA_FOLIODIGITAL = "rutaDiscoEntrega";
        public const String CAMPO_FECHACANCELACION_FOLIODIGITAL = "fechaCancelacion";
        public const String CAMPO_HORACANCELACION_FOLIODIGITAL = "horaCancelacion";
        public const String CAMPO_TRADICIONALDIGITAL_FOLIODIGITAL = "tradicionalDigital";
        public const String CAMPO_TIPOCFDI_FOLIODIGITAL = "tipoCFDI";
        public const String CAMPO_RFCEMISOR_FOLIODIGITAL = "rfcEmisor";
        public const String CAMPO_RAZONSOCIALEMISOR_FOLIODIGITAL = "razonSocialEmisor";
        public const String CAMPO_CODIGOUSOCFDI_FOLIODIGITAL = "codigoUsoCFDI";
        public const String CAMPO_UUID_FOLIODIGITAL = "uuid";
        public const String CAMPO_IDADD_FOLIODIGITAL = "idADD";
        public const String CAMPO_IDSERVER_FOLIODIGITAL = "idServer";

        public static String CREATE_TABLE_FOLIOSDIGITALES = "CREATE TABLE IF NOT EXISTS " + TABLA_FOLIOSDIGITALES + " (" +
            CAMPO_ID_FOLIODIGITAL + " INT PRIMARY KEY, " + CAMPO_TIPODOC_FOLIODIGITAL + " INT NOT NULL DEFAULT 0, " +
            CAMPO_IDCONCEPTO_FOLIODIGITAL + " INT NOT NULL DEFAULT 0, " + CAMPO_IDDOCUMENTO_FOLIODIGITAL + " INT NOT NULL DEFAULT 0, " +
            CAMPO_SERIECONCEPTO_FOLIODIGITAL + " VARCHAR(10) DEFAULT '', " + CAMPO_FOLIO_FOLIODIGITAL + " TEXT DEFAULT '', " +
            CAMPO_ESTADO_FOLIODIGITAL + " INT NOT NULL DEFAULT 0, " + CAMPO_ENTREGADO_FOLIODIGITAL + " INT NOT NULL DEFAULT 0, " +
            CAMPO_FECHAEMISION_FOLIODIGITAL + " DATETIME, " + CAMPO_EMAILENTREGA_FOLIODIGITAL + " VARCHAR(60) DEFAULT '', " +
            CAMPO_RUTADISCOENTREGA_FOLIODIGITAL + " VARCHAR(253) DEFAULT '', " + CAMPO_FECHACANCELACION_FOLIODIGITAL + " DATETIME, " +
            CAMPO_HORACANCELACION_FOLIODIGITAL + " VARCHAR(8) DEFAULT '', " + CAMPO_TRADICIONALDIGITAL_FOLIODIGITAL + " INT NOT NULL DEFAULT 0, " +
            CAMPO_TIPOCFDI_FOLIODIGITAL + " VARCHAR(1) NOT NULL DEFAULT 'I', " + CAMPO_RFCEMISOR_FOLIODIGITAL + " VARCHAR(20) NOT NULL DEFAULT '', " +
            CAMPO_RAZONSOCIALEMISOR_FOLIODIGITAL + " VARCHAR(253) NOT NULL DEFAULT '', " + CAMPO_CODIGOUSOCFDI_FOLIODIGITAL + " VARCHAR(30) NOT NULL," +
            CAMPO_UUID_FOLIODIGITAL + " VARCHAR(60) NOT NULL DEFAULT '', " + CAMPO_IDADD_FOLIODIGITAL + " VARCHAR(40) NOT NULL DEFAULT '', " +
            CAMPO_IDSERVER_FOLIODIGITAL + " INT NOT NULL DEFAULT 0)";

        public const String TABLA_CATVISITAS = "CatVisitas";
        public const String CAMPO_CATRAZON = "RAZON_VIS_INEF_ID";
        public const String CAMPO_CATNOMBRE = "NOMBRE";

        public static String CREAR_TABLA_CATVISITAS = "CREATE TABLE IF NOT EXISTS " +
            "" + TABLA_CATVISITAS + " (" + CAMPO_CATRAZON + " INTEGER," + CAMPO_CATNOMBRE + " TEXT)";

        public const String TABLA_BANCOS = "Bancos";
        public const String CAMPO_BANCO_ID = "BANCO_ID";
        public const String CAMPO_BNOMBRE = "NOMBRE";

        public static String CREAR_TABLA_BANCO = " CREATE TABLE IF NOT EXISTS " +
            "" + TABLA_BANCOS + " (" + CAMPO_BANCO_ID + " INTEGER," + CAMPO_BNOMBRE + " TEXT)";

        public const String TABLA_FORMASCOBRO = "FormasDeCobro";
        public const String CAMPO_ID_FORMASCOBRO = "FORMA_COBRO_CC_ID";
        public const String CAMPO_NOMBRE_FORMASCOBRO = "NOMBRE";

        public static String CREAR_TABLA_FORMAS_COBRO = " CREATE TABLE IF NOT EXISTS " +
            "" + TABLA_FORMASCOBRO + " (" + CAMPO_ID_FORMASCOBRO + " INTEGER," + CAMPO_NOMBRE_FORMASCOBRO + " TEXT)";

        public const String TABLA_DATOSTICKET = "Ticket";
        public const String CAMPO_TICCONFIGURA = "CONFIGURA_ID";
        public const String CAMPO_TICEMPRESA = "EMPRESA";
        public const String CAMPO_TICDIRECCION = "DIRECCION";
        public const String CAMPO_TICRFC = "RFC";
        public const String CAMPO_TICEXPENDIDO = "EXPEDIDO";
        public const String CAMPO_TICVENTA = "PIE_TICKETVENTAS";
        public const String CAMPO_TICCREDITO = "PIE_TICKETCREDITO";
        public const String CAMPO_TICPEDIDO = "PIE_TICKETPEDIDO";
        public const String CAMPO_TICCOTIZACION = "PIE_TICKETCOTIZACION";
        public const String CAMPO_TICCOBRANZA = "PIE_TICKETCOBRANZA";
        public const String CAMPO_TICDEVOLUCION = "PIE_TICKETDEVOLUCION";
        public const String CAMPO_TICFTPSERVER = "FTPSERVER";
        public const String CAMPO_TICFTPUSER = "FTPUSER";
        public const String CAMPO_TICFTPPASS = "FTPPASSWORD";
        public const String CAMPO_TICFTPPUETO = "FTPPUERTO";
        public const String CAMPO_TICCLAVESUP = "CLAVESUP";
        public const String CAMPO_TICPDEVOLUCION = "PDEVOLUCION";
        public const String CAMPO_TICVALVC = "VALVC";
        public const String CAMPO_EDITSUCINFO_DATOSTICKET = "modify_branch_information";
        public const String CAMPO_VENDERCONEXISTENCIA_DATOSTICKET = "venderConExistencia";
        public const String CAMPO_NAMEIMP1_DATOSTICKET = "nameImpuesto1";
        public const String CAMPO_NAMEIMP2_DATOSTICKET = "nameImpuesto2";
        public const String CAMPO_NAMEIMP3_DATOSTICKET = "nameImpuesto3";
        public const String CAMPO_NAMERETEN1_DATOSTICKET = "nameRetencion1";
        public const String CAMPO_NAMERETEN2_DATOSTICKET = "nameRetencion2";
        public const String CAMPO_ACTUALIZACIONLOCAL_DATOSTICKET = "banActualizacionDatosTicket";

        public static String CREAR_TABLA_DATOS_TICKET = " CREATE TABLE IF NOT EXISTS " +
            "" + TABLA_DATOSTICKET + " (" + CAMPO_TICCONFIGURA + " INTEGER," + CAMPO_TICEMPRESA + " TEXT," + CAMPO_TICDIRECCION + " TEXT," +
            "" + CAMPO_TICRFC + " TEXT," + CAMPO_TICEXPENDIDO + " TEXT," + CAMPO_TICVENTA + " TEXT," + CAMPO_TICCREDITO + " TEXT," + CAMPO_TICPEDIDO + " TEXT," +
            "" + CAMPO_TICCOTIZACION + " TEXT," + CAMPO_TICCOBRANZA + " TEXT," + CAMPO_TICDEVOLUCION + " TEXT," +
            "" + CAMPO_TICFTPSERVER + " TEXT," + CAMPO_TICFTPUSER + " TEXT," + CAMPO_TICFTPPASS + " TEXT," + CAMPO_TICFTPPUETO + " TEXT," +
            "" + CAMPO_TICCLAVESUP + " TEXT," + CAMPO_TICPDEVOLUCION + " TEXT," + CAMPO_TICVALVC + " TEXT, " +
            CAMPO_EDITSUCINFO_DATOSTICKET + " INT NOT NULL DEFAULT 0, " + CAMPO_VENDERCONEXISTENCIA_DATOSTICKET + " INT NOT NULL DEFAULT 0, " +
            CAMPO_NAMEIMP1_DATOSTICKET + " TEXT DEFAULT '', " + CAMPO_NAMEIMP2_DATOSTICKET + " TEXT DEFAULT '', " +
            CAMPO_NAMEIMP3_DATOSTICKET + " TEXT DEFAULT '', " + CAMPO_NAMERETEN1_DATOSTICKET + " TEXT DEFAULT '', " +
            CAMPO_NAMERETEN2_DATOSTICKET + " TEXT DEFAULT '', " +
            CAMPO_ACTUALIZACIONLOCAL_DATOSTICKET + " INT NOT NULL DEFAULT 0)"; 

        public static void validateAndCreateNewFieldsInDatosTicket(SQLiteConnection db)
        {
            String queryItem = "SELECT * FROM " + TABLA_DATOSTICKET;
            using (SQLiteCommand command = new SQLiteCommand(queryItem, db))
            {
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    int campos = reader.FieldCount;  // see if the column is there
                    if (campos == 18)
                    {
                        SQLiteCommand cmd = new SQLiteCommand(db);
                        cmd.CommandText = "ALTER TABLE " + TABLA_DATOSTICKET + " ADD " + CAMPO_EDITSUCINFO_DATOSTICKET + " INT NOT NULL DEFAULT 0";
                        cmd.ExecuteNonQuery();
                        cmd.CommandText = "ALTER TABLE " + TABLA_DATOSTICKET + " ADD " + CAMPO_VENDERCONEXISTENCIA_DATOSTICKET + " INT NOT NULL DEFAULT 0";
                        cmd.ExecuteNonQuery();
                    }
                    if (campos == 20)
                    {
                        SQLiteCommand cmd = new SQLiteCommand(db);
                        cmd.CommandText = "ALTER TABLE " + TABLA_DATOSTICKET + " ADD " + CAMPO_NAMEIMP1_DATOSTICKET + " TEXT DEFAULT ''";
                        cmd.ExecuteNonQuery();
                        cmd.CommandText = "ALTER TABLE " + TABLA_DATOSTICKET + " ADD " + CAMPO_NAMEIMP2_DATOSTICKET + " TEXT DEFAULT ''";
                        cmd.ExecuteNonQuery();
                        cmd.CommandText = "ALTER TABLE " + TABLA_DATOSTICKET + " ADD " + CAMPO_NAMEIMP3_DATOSTICKET + " TEXT DEFAULT ''";
                        cmd.ExecuteNonQuery();
                        cmd.CommandText = "ALTER TABLE " + TABLA_DATOSTICKET + " ADD " + CAMPO_NAMERETEN1_DATOSTICKET + " TEXT DEFAULT ''";
                        cmd.ExecuteNonQuery();
                        cmd.CommandText = "ALTER TABLE " + TABLA_DATOSTICKET + " ADD " + CAMPO_NAMERETEN2_DATOSTICKET + " TEXT DEFAULT ''";
                        cmd.ExecuteNonQuery();
                    }
                    if (campos == 25) {
                        SQLiteCommand cmd = new SQLiteCommand(db);
                        cmd.CommandText = "ALTER TABLE " + TABLA_DATOSTICKET + " ADD " + CAMPO_ACTUALIZACIONLOCAL_DATOSTICKET + " INT NOT NULL DEFAULT 0";
                        cmd.ExecuteNonQuery();
                    }
                    if (reader != null && !reader.IsClosed)
                        reader.Close();
                }
            }
        }

        public const String TABLA_PRECIOSEMPRESA = "PreciosEmpresa";
        public const String CAMPO_ID_PRECIOSEMPRESA = "PRECIO_EMPRESA_ID";
        public const String CAMPO_NOMBRE_PRECIOSEMPRESA = "NOMBRE";

        public static String CREAR_TABLA_PRECIOEMPRESA = " CREATE TABLE IF NOT EXISTS " +
            "" + TABLA_PRECIOSEMPRESA + " (" + CAMPO_ID_PRECIOSEMPRESA + " INTEGER," + CAMPO_NOMBRE_PRECIOSEMPRESA + " TEXT)";

        public const String TABLA_FORMASPAGOVENTAS = "FormasPagos";
        public const String CAMPO_ID_FORMASPAGOVENTAS = "FORMA_COBRO_ID";
        public const String CAMPO_NOMBRE_FORMASPAGOVENTAS = "NOMBRE";

        public static String CREAR_TABLA_FORMASPAGO = " CREATE TABLE IF NOT EXISTS " +
            "" + TABLA_FORMASPAGOVENTAS + " (" + CAMPO_ID_FORMASPAGOVENTAS + " INTEGER," + CAMPO_NOMBRE_FORMASPAGOVENTAS + " TEXT)";

        public const String TABLA_ZONACLIENTES = "Zonas";
        public const String CAMPO_ZONA_CLIENTE = "ZONA_CLIENTE_ID";
        public const String CAMPO_ZONA_NOMBRE = "NOMBRE";

        public static String CREAR_TABLA_ZONA_CLIENTES = " CREATE TABLE IF NOT EXISTS " +
            "" + TABLA_ZONACLIENTES + " (" + CAMPO_ZONA_CLIENTE + " INTEGER," + CAMPO_ZONA_NOMBRE + " TEXT)";

        public const String TABLA_CIUDADES = "Ciudades";
        public const String CAMPO_ID_CIUDAD = "CIUDAD_ID";
        public const String CAMPO_NOMBRE_CIUDAD = "NOMBRE";
        public const String CAMPO_ESTADOID_CIUDAD = "ESTADO_ID";

        public static String CREAR_TABLA_CIUDADES = " CREATE TABLE IF NOT EXISTS " +
            "" + TABLA_CIUDADES + " (" + CAMPO_ID_CIUDAD + " INTEGER," + CAMPO_NOMBRE_CIUDAD + " TEXT," + CAMPO_ESTADOID_CIUDAD + " INTEGER)";

        public const String TABLA_ESTADOS = "Estados";
        public const String CAMPO_ID_ESTADO = "ESTADO_ID";
        public const String CAMPO_NOMBRE_ESTADO = "NOMBRE";

        public static String CREAR_TABLA_ESTADOS = " CREATE TABLE IF NOT EXISTS " +
            "" + TABLA_ESTADOS + "(" + CAMPO_ID_ESTADO + " INTEGER," + CAMPO_NOMBRE_ESTADO + " TEXT)";

        public const String TABLA_PEDIDOENCABEZADO = "PedidoEncabezado";
        public const String CAMPO_ID_PEDIDOENCABEZADO = "id";
        public const String CAMPO_DOCUMENTOID_PE = "CIDDOCTOPEDIDOCC";
        public const String CAMPO_CLIENTEID_PE = "CLIENTE_ID";
        public const String CAMPO_CNOMBRECLIENTE_PE = "CNOMBRECLIENTE";
        public const String CAMPO_TELEFONO_PEDIDOSENCABEZADO = "CTELEFONO";
        public const String CAMPO_CNOMBREAGENTECC_PE = "CNOMBREAGENTECC";
        public const String CAMPO_CFECHA_PE = "CFECHA";
        public const String CAMPO_CFOLIO_PE = "CFOLIO";
        public const String CAMPO_CSUBTOTAL_PE = "CSUBTOTAL";
        public const String CAMPO_CDESCUENTO_PE = "CDESCUENTO";
        public const String CAMPO_CTOTAL_PE = "CTOTAL";
        public const String CAMPO_SURTIDO_PE = "surtido";
        public const String CAMPO_LISTO_PE = "listo";
        public const String CAMPO_TYPE_PE = "type";
        public const String CAMPO_OBSERVATION_PE = "observation";
        public const String CAMPO_FACTURAR_PE = "facturar";

        public static String CREAR_TABLA_PEDIDOENCABEZADO = " CREATE TABLE IF NOT EXISTS " +
            TABLA_PEDIDOENCABEZADO + " (" + CAMPO_ID_PEDIDOENCABEZADO + " INTEGER PRIMARY KEY, " + CAMPO_DOCUMENTOID_PE + " INTEGER," +
            CAMPO_CLIENTEID_PE + " INTEGER," + CAMPO_CNOMBRECLIENTE_PE + " TEXT," + CAMPO_TELEFONO_PEDIDOSENCABEZADO + " TEXT, " +
            CAMPO_CNOMBREAGENTECC_PE + " TEXT," + CAMPO_CFECHA_PE + " TEXT," + CAMPO_CFOLIO_PE + " TEXT," + CAMPO_CSUBTOTAL_PE + " NUMERIC," +
            CAMPO_CDESCUENTO_PE + " NUMERIC," + CAMPO_CTOTAL_PE + " NUMERIC, " + CAMPO_SURTIDO_PE + " INTEGER DEFAULT 0, " +
            CAMPO_LISTO_PE + " INTEGER DEFAULT 0, " + CAMPO_TYPE_PE + " INTEGER DEFAULT 1, " + CAMPO_OBSERVATION_PE + " TEXT, " +
            CAMPO_FACTURAR_PE + " INTEGER DEFAULT 0)";

        public static void validateAndCreateNewFieldsInPedidosEncabezado(SQLiteConnection db)
        {
            String queryItem = "SELECT * FROM " + LocalDatabase.TABLA_PEDIDOENCABEZADO;
            using (SQLiteCommand command = new SQLiteCommand(queryItem, db))
            {
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    int campos = reader.FieldCount;  // see if the column is there
                    if (campos == 14)
                    {
                        SQLiteCommand cmd = new SQLiteCommand(db);
                        cmd.CommandText = "ALTER TABLE " + TABLA_PEDIDOENCABEZADO + " ADD " + CAMPO_OBSERVATION_PE + " TEXT";
                        cmd.ExecuteNonQuery();
                        cmd.CommandText = "ALTER TABLE " + TABLA_PEDIDOENCABEZADO + " ADD " + CAMPO_FACTURAR_PE + " INTEGER DEFAULT 0";
                        cmd.ExecuteNonQuery();
                    }
                    if (campos == 15)
                    {
                        SQLiteCommand cmd = new SQLiteCommand(db);
                        cmd.CommandText = "ALTER TABLE " + TABLA_PEDIDOENCABEZADO + " ADD " + CAMPO_FACTURAR_PE + " INTEGER DEFAULT 0";
                        cmd.ExecuteNonQuery();
                    }
                    if (reader != null && !reader.IsClosed)
                        reader.Close();
                }
            }
        }

        public const String TABLA_PEDIDODETALLE = "PedidoDetalle";
        public const String CAMPO_ID_PD = "id";
        public const String CAMPO_CIDDOCTOPEDIDOCC_PD = "CIDDOCTOPEDIDOCC";
        public const String CAMPO_CIDPRODUCTO_PD = "CIDPRODUCTO";
        public const String CAMPO_CCODIGOPRODUCTO_PD = "CCODIGOPRODUCTO";
        public const String CAMPO_CNUMEROMOVIMIENTO_PD = "CNUMEROMOVIMIENTO";
        public const String CAMPO_CPRECIO_PD = "CPRECIO";
        public const String CAMPO_CUNIDADES_PD = "CUNIDADES";
        public const String CAMPO_CAPTUREDUNITID_PD = "capturedUnitId";
        public const String CAMPO_CSUBTOTAL_PD = "CSUBTOTAL";
        public const String CAMPO_CDESCUENTO_PD = "CDESCUENTO";
        public const String CAMPO_CTOTAL_PD = "CTOTAL";
        public const String CAMPO_NONCONVERTIBLEUNITS_PD = "nonConvertibleUnits";
        public const String CAMPO_NONCONVERTIBLEUNITID_PD = "nonConvertibleUnitId";
        public const String CAMPO_OBSERVATION_PD = "observation";

        public static String CREAR_TABLA_DETALLEPEDIDO = "CREATE TABLE IF NOT EXISTS " +
            TABLA_PEDIDODETALLE + " (" + CAMPO_ID_PD + " INTEGER PRIMARY KEY, " + CAMPO_CIDDOCTOPEDIDOCC_PD + " INTEGER," +
            CAMPO_CIDPRODUCTO_PD + " INTEGER," + CAMPO_CCODIGOPRODUCTO_PD + " TEXT, " + CAMPO_CNUMEROMOVIMIENTO_PD + " INTEGER, " +
            CAMPO_CPRECIO_PD + " NUMERIC, " + CAMPO_CUNIDADES_PD + " NUMERIC, " + CAMPO_CAPTUREDUNITID_PD + " INTEGER, " +
            CAMPO_CSUBTOTAL_PD + " NUMERIC, " + CAMPO_CDESCUENTO_PD + " NUMERIC, " + CAMPO_CTOTAL_PD + " NUMERIC, " +
            CAMPO_NONCONVERTIBLEUNITS_PD + " NUMERIC DEFAULT 0, " + CAMPO_NONCONVERTIBLEUNITID_PD + " INTEGER DEFAULT 0, " +
            CAMPO_OBSERVATION_PD + " TEXT NOT NULL DEFAULT '')";

        public static void validateAndCreateNewFieldsInPedidosDetalles(SQLiteConnection db)
        {
            String queryItem = "SELECT * FROM " + LocalDatabase.TABLA_PEDIDODETALLE;
            using (SQLiteCommand command = new SQLiteCommand(queryItem, db))
            {
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    int campos = reader.FieldCount;  // see if the column is there
                    if (campos == 11)
                    {
                        SQLiteCommand cmd = new SQLiteCommand(db);
                        cmd.CommandText = "ALTER TABLE " + TABLA_PEDIDODETALLE + " ADD " + CAMPO_NONCONVERTIBLEUNITS_PD + " NUMERIC DEFAULT 0";
                        cmd.ExecuteNonQuery();
                        cmd.CommandText = "ALTER TABLE " + TABLA_PEDIDODETALLE + " ADD " + CAMPO_NONCONVERTIBLEUNITID_PD + " INTEGER DEFAULT 0";
                        cmd.ExecuteNonQuery();
                    }
                    if (campos == 13)
                    {
                        SQLiteCommand cmd = new SQLiteCommand(db);
                        cmd.CommandText = "ALTER TABLE " + TABLA_PEDIDODETALLE + " ADD " + CAMPO_OBSERVATION_PD + " TEXT NOT NULL DEFAULT ''";
                        cmd.ExecuteNonQuery();
                    }
                    if (reader != null && !reader.IsClosed)
                        reader.Close();
                }
            }
        }

        public const String TABLA_BENEFICIARIO = "Beneficiario";
        public const String CAMPO_BENEBANCO = "BANCO";
        public const String CAMPO_BENEDESCRIPCION = "DESCRIPCION";
        public const String CAMPO_BENENUMCUENTA = "NUM_CUENTA";
        public const String CAMPO_BENECLABE = "CLABE";
        public const String CAMPO_BENEMONEDAID = "MONEDA_ID";
        public const String CAMPO_BENESUCURSAL = "SUCURSAL";
        public const String CAMPO_BENEMONEDA = "MONEDA";

        public static String CREAR_TABLA_BENEFICIARIO = " CREATE TABLE IF NOT EXISTS " +
            "" + TABLA_BENEFICIARIO + " (" + CAMPO_BENEBANCO + " TEXT," + CAMPO_BENEDESCRIPCION + " TEXT," +
            "" + CAMPO_BENENUMCUENTA + " TEXT," + CAMPO_BENECLABE + " TEXT," + CAMPO_BENEMONEDAID + " INTEGER," +
            "" + CAMPO_BENESUCURSAL + " TEXT," + CAMPO_BENEMONEDA + " TEXT)";

        public const String TABLA_PAGOSCREDITO = "Pagos";
        public const String CAMPO_CLIENTEID_PAGOSCREDITO = "CLIENTE_ID";
        public const String CAMPO_PAGODOCTO_CC = "DOCTO_CC_ID";
        public const String CAMPO_PAGODOCTO_ID = "DOCTO_ID_ABONO";
        public const String CAMPO_PAGOFOLIO = "FOLIO_ABONO";
        public const String CAMPO_PAGOABONO = "ABONO";
        public const String CAMPO_PAGOFECHAABONO = "FECHA_ABONO";

        public static String CREAR_TABLA_PAGOSCREDITO = " CREATE TABLE IF NOT EXISTS " +
            "" + TABLA_PAGOSCREDITO + " (" + CAMPO_CLIENTEID_PAGOSCREDITO + " INTEGER," + CAMPO_PAGODOCTO_CC + " INTEGER," +
            "" + CAMPO_PAGODOCTO_ID + " INTEGER," + CAMPO_PAGOFOLIO + " TEXT," + CAMPO_PAGOABONO + " NUMERIC," +
            "" + CAMPO_PAGOFECHAABONO + " TEXT)";

        public const String TABLA_CLIENTEADC = "ClientesADC";
        public const String CAMPO_ID_CLIENTEADC = "id";
        public const String CAMPO_NOMBRE_CLIENTEADC = "NOMBRE";
        public const String CAMPO_ZONA_CLIENTEADC = "ZONA";
        public const String CAMPO_CALLE_CLIENTEADC = "CALLE";
        public const String CAMPO_NUMERO_CLIENTEADC = "NUMERO";
        public const String CAMPO_COLONIA_ADC = "COLONIA";
        public const String CAMPO_POBLACION_CLIENTEADC = "POBLACION";
        public const String CAMPO_CIUDAD_CLIENTEADC = "CIUDAD";
        public const String CAMPO_ESTADO_CLIENTEADC = "ESTADO";
        public const String CAMPO_REFERENCIA_CLIENTEADC = "REFERENCIA";
        public const String CAMPO_TELEFONO_CLIENTEADC = "TELEFONO";
        public const String CAMPO_CP_CLIENTEADC = "CP";
        public const String CAMPO_EMAIL_CLIENTEADC = "EMAIL";
        public const String CAMPO_RFC_CLIENTEADC = "RFC";
        public const String CAMPO_IDSISTEMA_CLIENTEADC = "cliente_id_sistema";
        public const String CAMPO_ENVIADO_CLIENTEADC = "enviado";
        public const String CAMPO_TIPOCONTRIBUYENTE_CLIENTEADC = "tipoContribuyente";
        public const String CAMPO_CODIGOREGIMENFISCAL_CLIENTEADC = "codigoRegimenFiscal";
        public const String CAMPO_CODIGOUSOCFDI_CLIENTEADC = "codigoUsoCFDI";



        public static String CREAR_TABLA_CLIENTEADC = " CREATE TABLE IF NOT EXISTS " +
            "" + TABLA_CLIENTEADC + " (" + CAMPO_ID_CLIENTEADC + " INTEGER PRIMARY KEY," + CAMPO_NOMBRE_CLIENTEADC + " TEXT," + CAMPO_ZONA_CLIENTEADC + " TEXT," +
            "" + CAMPO_CALLE_CLIENTEADC + " TEXT," + CAMPO_NUMERO_CLIENTEADC + " TEXT," + CAMPO_COLONIA_ADC + " TEXT," +
            "" + CAMPO_POBLACION_CLIENTEADC + " TEXT," + CAMPO_CIUDAD_CLIENTEADC + " TEXT," + CAMPO_ESTADO_CLIENTEADC + " TEXT," + CAMPO_REFERENCIA_CLIENTEADC + " TEXT," +
            "" + CAMPO_TELEFONO_CLIENTEADC + " TEXT," + CAMPO_CP_CLIENTEADC + " TEXT, " + CAMPO_EMAIL_CLIENTEADC + " TEXT, " + CAMPO_RFC_CLIENTEADC + " TEXT," +
            CAMPO_IDSISTEMA_CLIENTEADC + " INTEGER DEFAULT 0, " + CAMPO_ENVIADO_CLIENTEADC + " INTEGER DEFAULT 0, " +
            CAMPO_TIPOCONTRIBUYENTE_CLIENTEADC + " INTEGER NOT NULL DEFAULT 0, " +
            CAMPO_CODIGOREGIMENFISCAL_CLIENTEADC +" VARCHAR(10) NOT NULL DEFAULT '', " +
            CAMPO_CODIGOUSOCFDI_CLIENTEADC + " VARCHAR(10) NOT NULL DEFAULT '')";

        public static void validateAndCreateNewFieldsInDatosClienteADC(SQLiteConnection db)
        {
            String queryItem = "SELECT * FROM " + TABLA_CLIENTEADC;
            using (SQLiteCommand command = new SQLiteCommand(queryItem, db))
            {
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    int campos = reader.FieldCount;  // see if the column is there
                    if (campos == 16)
                    {
                        SQLiteCommand cmd = new SQLiteCommand(db);
                        cmd.CommandText = "ALTER TABLE " + TABLA_CLIENTEADC + " ADD " + CAMPO_TIPOCONTRIBUYENTE_CLIENTEADC + " INT NOT NULL DEFAULT 0";
                        cmd.ExecuteNonQuery();
                        cmd.CommandText = "ALTER TABLE " + TABLA_CLIENTEADC + " ADD " + CAMPO_CODIGOREGIMENFISCAL_CLIENTEADC + " TEXT NOT NULL DEFAULT ''";
                        cmd.ExecuteNonQuery();
                        cmd.CommandText = "ALTER TABLE " + TABLA_CLIENTEADC + " ADD " + CAMPO_CODIGOUSOCFDI_CLIENTEADC + " TEXT NOT NULL DEFAULT ''";
                        cmd.ExecuteNonQuery();
                    }
                    if (reader != null && !reader.IsClosed)
                        reader.Close();
                }
            }
        }

        public const String TABLA_DOCUMENTOVENTA = "Documentos";
        public const String CAMPO_ID_DOC = "id";
        public const String CAMPO_CLAVECLIENTE_DOC = "clave_cliente";
        public const String CAMPO_CLIENTEID_DOC = "cliente_id";
        public const String CAMPO_DESCUENTO_DOC = "descuento";
        public const String CAMPO_TOTAL_DOC = "total";
        public const String CAMPO_NOMBREU_DOC = "NOMBREU";
        public const String CAMPO_ALMACENID_DOC = "ALMACEN_ID";
        public const String CAMPO_ANTICIPO_DOC = "ANTICIPO";
        public const String CAMPO_TIPODOCUMENTO_DOC = "TIPO_DOCUMENTO";
        public const String CAMPO_FORMACOBROID_DOC = "FORMA_COBRO_ID";
        public const String CAMPO_FACTURA_DOC = "FACTURA";
        public const String CAMPO_OBSERVACION_DOC = "observacion";
        public const String CAMPO_DEV_DOC = "DEV";
        public const String CAMPO_FVENTA_DOC = "FVENTA";
        public const String CAMPO_FECHAHORAMOV_DOC = "FECHAHORAMOV";
        public const String CAMPO_USUARIOID_DOC = "USUARIO_ID";
        public const String CAMPO_FORMACOBROIDABONO_DOC = "FORMA_COBRO_ID_ABONO";
        public const String CAMPO_CIDDOCTOPEDIDOCC_DOC = "CIDDOCTOPEDIDOCC";
        public const String CAMPO_CANCELADO_DOC = "cancelado";
        public const String CAMPO_ENVIADOALWS_DOC = "enviado_al_ws";
        public const String CAMPO_IDWEBSERVICE_DOC = "id_web_service";
        public const String CAMPO_ARCHIVADO_DOC = "archivado";
        public const String CAMPO_PAUSAR_DOC = "pausa";
        public const String CAMPO_PAPELERARECICLAJE_DOC = "papelera_reciclaje";

        public static String CREAR_TABLA_DOCUMENTOVENTA = " CREATE TABLE IF NOT EXISTS " +
            "" + TABLA_DOCUMENTOVENTA + " (" + CAMPO_ID_DOC + " INTEGER PRIMARY KEY," + CAMPO_CLAVECLIENTE_DOC + " TEXT," +
            "" + CAMPO_CLIENTEID_DOC + " INTEGER NOT NULL," + CAMPO_DESCUENTO_DOC + " NUMERIC," + CAMPO_TOTAL_DOC + " NUMERIC," +
            "" + CAMPO_NOMBREU_DOC + " TEXT," + CAMPO_ALMACENID_DOC + " INTEGER NOT NULL," + CAMPO_ANTICIPO_DOC + " NUMERIC," + CAMPO_TIPODOCUMENTO_DOC + " TEXT," +
            "" + CAMPO_FORMACOBROID_DOC + " INTEGER NOT NULL," + CAMPO_FACTURA_DOC + " INTEGER, " + CAMPO_OBSERVACION_DOC + " TEXT, " + CAMPO_DEV_DOC + " INTEGER, " +
            "" + CAMPO_FVENTA_DOC + " TEXT, " + CAMPO_FECHAHORAMOV_DOC + " TEXT, " + CAMPO_USUARIOID_DOC + " INTEGER NOT NULL, " + CAMPO_FORMACOBROIDABONO_DOC + " INTEGER, " +
        "" + CAMPO_CIDDOCTOPEDIDOCC_DOC + " INTEGER, " + CAMPO_CANCELADO_DOC + " INTEGER DEFAULT 0, " + CAMPO_ENVIADOALWS_DOC + " INTEGER DEFAULT 0, " +
            CAMPO_IDWEBSERVICE_DOC + " INTEGER DEFAULT 0, " + CAMPO_ARCHIVADO_DOC + " INTEGER DEFAULT 0, " +
            CAMPO_PAUSAR_DOC + " INTEGER DEFAULT 0, " + CAMPO_PAPELERARECICLAJE_DOC + " INTEGER DEFAULT 0)";

        public const String TABLA_MOVIMIENTO = "Movimientos";
        public const String CAMPO_ID_MOV = "id";
        public const String CAMPO_DOCUMENTOID_MOV = "DOCTO_ID_PEDIDO";
        public const String CAMPO_CLAVEART_MOV = "CLAVE_ART";
        public const String CAMPO_ARTICULOID_MOV = "ARTICULO_ID";
        public const String CAMPO_BASEUNIT_MOV = "unidad_base";
        public const String CAMPO_NONCONVERTIBLEUNIT_MOV = "unidad_no_convertible";
        public const String CAMPO_CAPTUREDUNIT_MOV = "unidades_capturadas";
        public const String CAMPO_NONCONVERTIBLEUNITID_MOV = "unidad_no_convertible_id";
        public const String CAMPO_CAPTUREDUNITID_MOV = "unidades_capturadas_id";
        public const String CAMPO_CAPTUREDUNITTYPE_MOV = "captured_unit_type";
        public const String CAMPO_PRECIO_MOV = "precio";
        public const String CAMPO_MONTO_MOV = "MONTO";
        public const String CAMPO_TOTAL_MOV = "TOTAL";
        public const String CAMPO_POSICION_MOV = "POSICION";
        public const String CAMPO_TIPODOCUMENTO_MOV = "TIPO_DOCUMENTO";
        public const String CAMPO_NOMBREU_MOV = "NOMBREU";
        public const String CAMPO_FACTURA_MOV = "FACTURA";
        public const String CAMPO_DESCUENTOPOR_MOV = "DESCUENTOPOR";
        public const String CAMPO_DESCUENTOIMP_MOV = "DESCUENTO";
        public const String CAMPO_OBSERVACIONES_MOV = "OBSERVACIONES";
        public const String CAMPO_IDDEV_MOV = "IDDEV";
        public const String CAMPO_COMENTARIO_MOV = "COMENTARIO";
        public const String CAMPO_USUARIOID_MOV = "USUARIO_ID";
        public const String CAMPO_ENVIADOALWS_MOV = "enviado_al_ws";
        public const String CAMPO_RATEDISCOUNTPROMO_MOV = "rate_discount_promo";
        public const String CAMPO_CANCEL_MOV = "cancel";

        public static String CREAR_TABLA_MOVIDOCVENTA = " CREATE TABLE IF NOT EXISTS " +
            TABLA_MOVIMIENTO + " (" + CAMPO_ID_MOV + " INTEGER PRIMARY KEY," + CAMPO_DOCUMENTOID_MOV + " INTEGER NOT NULL," +
            CAMPO_CLAVEART_MOV + " TEXT," + CAMPO_ARTICULOID_MOV + " INTEGER NOT NULL," + CAMPO_BASEUNIT_MOV + " NUMERIC, " +
            CAMPO_NONCONVERTIBLEUNIT_MOV + " NUMERIC DEFAULT 0, " + CAMPO_CAPTUREDUNIT_MOV + " NUMERIC, " +
            CAMPO_NONCONVERTIBLEUNITID_MOV + " INTEGER DEFAULT 0, " + CAMPO_CAPTUREDUNITID_MOV + " INTEGER, " +
            CAMPO_CAPTUREDUNITTYPE_MOV + " INTEGER, " + CAMPO_PRECIO_MOV + " NUMERIC," + CAMPO_MONTO_MOV + " NUMERIC NOT NULL," +
            CAMPO_TOTAL_MOV + " NUMERIC NOT NULL," + CAMPO_POSICION_MOV + " INTEGER," + CAMPO_TIPODOCUMENTO_MOV + " INTEGER," +
            CAMPO_NOMBREU_MOV + " TEXT," + CAMPO_FACTURA_MOV + " TEXT, " + CAMPO_DESCUENTOPOR_MOV + " NUMERIC, " +
            CAMPO_DESCUENTOIMP_MOV + " NUMERIC, " + CAMPO_OBSERVACIONES_MOV + " TEXT, " +
            CAMPO_IDDEV_MOV + " INTEGER, " + CAMPO_COMENTARIO_MOV + " TEXT, " + CAMPO_USUARIOID_MOV + " INTEGER NOT NULL, " +
            CAMPO_ENVIADOALWS_MOV + " INTEGER DEFAULT 0, " + CAMPO_RATEDISCOUNTPROMO_MOV + " NUMERIC, " +
            CAMPO_CANCEL_MOV + " INTEGER DEFAULT 0)";

        public const String TABLA_PESO = "Weight";
        public const String CAMPO_ID_PESO = "id";
        public const String CAMPO_IDSERVER_PESO = "idServer";
        public const String CAMPO_MOVIMIENTOID_PESO = "movementId";
        public const String CAMPO_PESOBRUTO_PESO = "gross_weight";
        public const String CAMPO_PESOCAJA_PESO = "box_weight";
        public const String CAMPO_PESONETO_PESO = "net_weight";
        public const String CAMPO_CREATEDAT_PESO = "createdAt";
        public const String CAMPO_CAJAS_PESO = "cajas";
        public const String CAMPO_PESOPOLLOLESIONADO_PESO = "pollo_mal_weight";
        public const String CAMPO_PESOPOLLOMUERTO_PESO = "pollo_muerto_weight";
        public const String CAMPO_PESOPOLLOBAJOPESO_PESO = "pollo_bajopeso_weight";
        public const String CAMPO_PESOPOLLOGOLPEADO_PESO = "pollo_golpeado_weight";
        public const String CAMPO_TIPO_PESO = "tipo";

        public static String CREAR_TABLA_PESO = "CREATE TABLE IF NOT EXISTS " +
            TABLA_PESO + " (" + CAMPO_ID_PESO + " INTEGER PRIMARY KEY, " + CAMPO_IDSERVER_PESO + " INTEGER DEFAULT 0, " +
            CAMPO_MOVIMIENTOID_PESO + " INTEGER DEFAULT 0, " + CAMPO_PESOBRUTO_PESO + " NUMERIC DEFAULT 0, " +
            CAMPO_PESOCAJA_PESO + " NUMERIC DEFAULT 0, " + CAMPO_PESONETO_PESO + " NUMERIC DEFAULT 0, " +
            CAMPO_CREATEDAT_PESO + " TEXT, "+ CAMPO_CAJAS_PESO + " INTEGER DEFAULT 0, "+ CAMPO_PESOPOLLOLESIONADO_PESO + " NUMERIC DEFAULT 0, "+
            CAMPO_PESOPOLLOMUERTO_PESO + " NUMERIC DEFAULT 0, "+ CAMPO_PESOPOLLOBAJOPESO_PESO + " NUMERIC DEFAULT 0, "+ 
            CAMPO_PESOPOLLOGOLPEADO_PESO + " NUMERIC DEFAULT 0, "+ CAMPO_TIPO_PESO + " INTEGER DEFAULT 0)";

        public static void validateAndCreateNewFieldsInPeso(SQLiteConnection db)
        {
            String queryItem = "SELECT * FROM " + LocalDatabase.TABLA_PESO;
            using (SQLiteCommand command = new SQLiteCommand(queryItem, db))
            {
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    int campos = reader.FieldCount;  // see if the column is there
                    if (campos == 7)
                    {
                        SQLiteCommand cmd = new SQLiteCommand(db);
                        cmd.CommandText = "ALTER TABLE " + TABLA_PESO + " ADD " + CAMPO_CAJAS_PESO + " INTEGER DEFAULT 0";
                        cmd.ExecuteNonQuery();
                        cmd.CommandText = "ALTER TABLE " + TABLA_PESO + " ADD " + CAMPO_PESOPOLLOLESIONADO_PESO + " NUMERIC DEFAULT 0";
                        cmd.ExecuteNonQuery();
                        cmd.CommandText = "ALTER TABLE " + TABLA_PESO + " ADD " + CAMPO_PESOPOLLOMUERTO_PESO + " NUMERIC DEFAULT 0";
                        cmd.ExecuteNonQuery();
                        cmd.CommandText = "ALTER TABLE " + TABLA_PESO + " ADD " + CAMPO_PESOPOLLOBAJOPESO_PESO + " NUMERIC DEFAULT 0";
                        cmd.ExecuteNonQuery();
                        cmd.CommandText = "ALTER TABLE " + TABLA_PESO + " ADD " + CAMPO_PESOPOLLOGOLPEADO_PESO + " NUMERIC DEFAULT 0";
                        cmd.ExecuteNonQuery();
                        cmd.CommandText = "ALTER TABLE " + TABLA_PESO + " ADD " + CAMPO_TIPO_PESO + " INTEGER DEFAULT 0";
                        cmd.ExecuteNonQuery();
                    }
                    if (campos == 8)
                    {
                        SQLiteCommand cmd = new SQLiteCommand(db);
                        cmd.CommandText = "ALTER TABLE " + TABLA_PESO + " ADD " + CAMPO_PESOPOLLOLESIONADO_PESO + " NUMERIC DEFAULT 0";
                        cmd.ExecuteNonQuery();
                        cmd.CommandText = "ALTER TABLE " + TABLA_PESO + " ADD " + CAMPO_PESOPOLLOMUERTO_PESO + " NUMERIC DEFAULT 0";
                        cmd.ExecuteNonQuery();
                        cmd.CommandText = "ALTER TABLE " + TABLA_PESO + " ADD " + CAMPO_PESOPOLLOBAJOPESO_PESO + " NUMERIC DEFAULT 0";
                        cmd.ExecuteNonQuery();
                        cmd.CommandText = "ALTER TABLE " + TABLA_PESO + " ADD " + CAMPO_PESOPOLLOGOLPEADO_PESO + " NUMERIC DEFAULT 0";
                        cmd.ExecuteNonQuery();
                        cmd.CommandText = "ALTER TABLE " + TABLA_PESO + " ADD " + CAMPO_TIPO_PESO + " INTEGER DEFAULT 0";
                        cmd.ExecuteNonQuery();
                    }
                    if (campos == 9)
                    {
                        SQLiteCommand cmd = new SQLiteCommand(db);
                        cmd.CommandText = "ALTER TABLE " + TABLA_PESO + " ADD " + CAMPO_PESOPOLLOMUERTO_PESO + " NUMERIC DEFAULT 0";
                        cmd.ExecuteNonQuery();
                        cmd.CommandText = "ALTER TABLE " + TABLA_PESO + " ADD " + CAMPO_PESOPOLLOBAJOPESO_PESO + " NUMERIC DEFAULT 0";
                        cmd.ExecuteNonQuery();
                        cmd.CommandText = "ALTER TABLE " + TABLA_PESO + " ADD " + CAMPO_PESOPOLLOGOLPEADO_PESO + " NUMERIC DEFAULT 0";
                        cmd.ExecuteNonQuery();
                        cmd.CommandText = "ALTER TABLE " + TABLA_PESO + " ADD " + CAMPO_TIPO_PESO + " INTEGER DEFAULT 0";
                        cmd.ExecuteNonQuery();
                    } 
                    if (campos == 12)
                    {
                        SQLiteCommand cmd = new SQLiteCommand(db);
                        cmd.CommandText = "ALTER TABLE " + TABLA_PESO + " ADD " + CAMPO_TIPO_PESO + " INTEGER DEFAULT 0";
                        cmd.ExecuteNonQuery();
                    }
                    if (reader != null && !reader.IsClosed)
                        reader.Close();
                }
            }
        }

        public const String TABLA_SINGLEWEIGHT = "SingleWeight";
        public const String CAMPO_ID_SINGLEWEIGHT = "id";
        public const String CAMPO_WEIGTH_SINGLEWEIGHT = "weight";
        public const String CAMPO_NUMBER_SINGLEWEIGHT = "number";
        public const String CAMPO_TARAS_SINGLEWEIGHT = "taras";
        public const String CAMPO_DATETIME_SINGLEWEIGHT = "date_time";
        public const String CAMPO_TYPE_SINGLEWEIGHT = "type";
        public const String CAMPO_WEIGHTID_SINGLEWEIGHT = "weightId";
        public const String CAMPO_MOVEMENTID_SINGLEWEIGHT = "movementId";

        public static String CREATE_TABLE_SINGLEWEIGHT = "CREATE TABLE IF NOT EXISTS " +
            TABLA_SINGLEWEIGHT + " (" + CAMPO_ID_SINGLEWEIGHT + " INTEGER PRIMARY KEY, " + CAMPO_WEIGTH_SINGLEWEIGHT + " NUMERIC DEFAULT 0, " +
            CAMPO_NUMBER_SINGLEWEIGHT + " INTEGER DEFAULT 0, " + CAMPO_TARAS_SINGLEWEIGHT+" INTEGER DEFAULT 0, "+
            CAMPO_DATETIME_SINGLEWEIGHT + " TEXT DEFAULT '', " +
            CAMPO_TYPE_SINGLEWEIGHT + " INTEGER DEFAULT 0, " + CAMPO_WEIGHTID_SINGLEWEIGHT + " INTEGER DEFAULT 0, " +
            CAMPO_MOVEMENTID_SINGLEWEIGHT + " INTEGER DEFAULT 0)";

        public const String TABLA_POSICION = "Location";
        public const String CAMPO_ID_POSICION = "id";
        public const String CAMPO_LAT_POSICION = "lat";
        public const String CAMPO_LON_POSICION = "lon";
        public const String CAMPO_CLIENTE_ID_POSICION = "cliente_id";
        public const String CAMPO_FECHA_POSICION = "fecha";
        public const String CAMPO_TIPO_MOV_POSICION = "tipo_mov";
        public const String CAMPO_VENDEDOR_ID_POSICION = "vendedor_id";
        public const String CAMPO_RUTA_POSICION = "ruta";
        public const String CAMPO_DOCTOID_POSICION = "documento_id";
        public const String CAMPO_DOCTOID_PANEL_POSICION = "documento_id_panel";
        public const String CAMPO_ENVIADO_POSICION = "enviado";

        public static String CREAR_TABLA_POSICION = " CREATE TABLE IF NOT EXISTS " + TABLA_POSICION +
            " (" + CAMPO_ID_POSICION + " INTEGER PRIMARY KEY," + CAMPO_LAT_POSICION + " TEXT," +
            "" + CAMPO_LON_POSICION + " TEXT," + CAMPO_CLIENTE_ID_POSICION + " INTEGER," + CAMPO_FECHA_POSICION + " TEXT," +
            "" + CAMPO_TIPO_MOV_POSICION + " INTEGER, " + CAMPO_VENDEDOR_ID_POSICION + " INTEGER, " + CAMPO_RUTA_POSICION + " TEXT, " +
            CAMPO_DOCTOID_POSICION + " INTEGER, " + CAMPO_DOCTOID_PANEL_POSICION + " INTEGER, " + CAMPO_ENVIADO_POSICION + " INETEGER DEFAULT 0)";

        public const String TABLA_BANDERASCI = "banderas_carga_inicial";
        public const String CAMPO_ID_BANDERACI = "id";
        public const String CAMPO_NOMBRE_METODO_BANDERACI = "nombre_metodo";
        public const String CAMPO_OBTENIDO_METODOBANDERACI = "registros_obtenidos";
        public const String CAMPO_GUARDADO_METODO_BANDERACI = "registros_guardados";
        public const String CAMPO_ERROR_OBTENIDO_METODOBANDERACI = "error_obtenido";
        public const String CAMPO_CREATEDAT_METODOBANDERACI = "created_at";
        public const String CAMPO_UPDATEDAT_METODOBANDERACI = "updated_at";

        public static String CREAR_TABLA_BANDERASCI = "CREATE TABLE IF NOT EXISTS " +
            TABLA_BANDERASCI + " (" + CAMPO_ID_BANDERACI + " INTEGER PRIMARY KEY, " + CAMPO_NOMBRE_METODO_BANDERACI + " TEXT, " +
            CAMPO_OBTENIDO_METODOBANDERACI + " INTEGER DEFAULT 0, " + CAMPO_GUARDADO_METODO_BANDERACI + " INTEGER DEFAULT 0, " +
            CAMPO_ERROR_OBTENIDO_METODOBANDERACI + " TEXT, " + CAMPO_CREATEDAT_METODOBANDERACI + " DATETIME DEFAULT CURRENT_TIMESTAMP, " +
            CAMPO_UPDATEDAT_METODOBANDERACI + " TEXT)";

        public const String TABLA_IMPRESORAS = "PrintersType";
        public const String CAMPO_ID_IMPRESORA = "id";
        public const String CAMPO_NOMBRE_IMPRESORA = "nombre";
        public const String CAMPO_DIRECCION_MAC = "direccion_mac";
        public const String CAMPO_TIPO_TICKET = "tamaño_ticket";
        public const String CAMPO_TIPO_IMPRESORA = "tipo";
        public const String CAMPO_ORIGINAL_IMPRESORA = "leyenda_original";
        public const String CAMPO_COPIA_IMPRESORA = "leyenda_copia";
        public const String CAMPO_SHOWFOLIO_IMPRESORA = "showFolio";
        public const String CAMPO_SHOWCODIGOCAJA_IMPRESORA = "showCodigoCaja";
        public const String CAMPO_SHOWNOMBREUSUARIO_IMPRESORA = "showNombreUsuario";
        public const String CAMPO_SHOWCODIGOUSUARIO_IMPRESORA = "showCodigoUsuario";
        public const String CAMPO_SHOWFECHAHORA_IMPRESORA = "showFechaHora";
        public const String CAMPO_SHOWPORCENTAJEDESCUENTOMOVIMIENTO_IMPRESORA = "showPorcentajeDescuentoMovimiento";

        public static String CREAR_TABLA_TIPO_IMPRESORAS = "CREATE TABLE IF NOT EXISTS " +
            TABLA_IMPRESORAS + " (" + CAMPO_ID_IMPRESORA + " INTEGER PRIMARY KEY, " + CAMPO_NOMBRE_IMPRESORA + " TEXT, " + 
            CAMPO_DIRECCION_MAC + " INTEGER, " + CAMPO_TIPO_TICKET + " INTEGER, " + CAMPO_TIPO_IMPRESORA + " INTEGER, "+
            CAMPO_ORIGINAL_IMPRESORA + " VARCHAR(30) NOT NULL DEFAULT '', "+ CAMPO_COPIA_IMPRESORA + " VARCHAR(30) NOT NULL DEFAULT '', " +
            CAMPO_SHOWFOLIO_IMPRESORA+" INT NOT NULL DEFAULT 1, "+CAMPO_SHOWCODIGOCAJA_IMPRESORA+" INT NOT NULL DEFAULT 1, "+
            CAMPO_SHOWNOMBREUSUARIO_IMPRESORA+" INT NOT NULL DEFAULT 1, "+ CAMPO_SHOWCODIGOUSUARIO_IMPRESORA + " INT NOT NULL DEFAULT 1, " +
            CAMPO_SHOWFECHAHORA_IMPRESORA + " INT NOT NULL DEFAULT 1, " +
            CAMPO_SHOWPORCENTAJEDESCUENTOMOVIMIENTO_IMPRESORA + " INT NOT NULL DEFAULT 1)";

        public static void validateAndCreateNewFieldsInTickets(SQLiteConnection db)
        {
            String queryItem = "SELECT * FROM " + TABLA_IMPRESORAS;
            using (SQLiteCommand command = new SQLiteCommand(queryItem, db))
            {
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    int campos = reader.FieldCount;  // see if the column is there
                    if (campos == 5)
                    {
                        SQLiteCommand cmd = new SQLiteCommand(db);
                        cmd.CommandText = "ALTER TABLE " + TABLA_IMPRESORAS + " ADD " + CAMPO_ORIGINAL_IMPRESORA + " VARCHAR(30) NOT NULL DEFAULT ''";
                        cmd.ExecuteNonQuery();
                        cmd.CommandText = "ALTER TABLE " + TABLA_IMPRESORAS + " ADD " + CAMPO_COPIA_IMPRESORA + " VARCHAR(30) NOT NULL DEFAULT ''";
                        cmd.ExecuteNonQuery();
                    }
                    if (campos == 7)
                    {
                        SQLiteCommand cmd = new SQLiteCommand(db);
                        cmd.CommandText = "ALTER TABLE " + TABLA_IMPRESORAS + " ADD " + CAMPO_SHOWFOLIO_IMPRESORA + " INT NOT NULL DEFAULT 1";
                        cmd.ExecuteNonQuery();
                        cmd.CommandText = "ALTER TABLE " + TABLA_IMPRESORAS + " ADD " + CAMPO_SHOWCODIGOCAJA_IMPRESORA + " INT NOT NULL DEFAULT 1";
                        cmd.ExecuteNonQuery();
                        cmd.CommandText = "ALTER TABLE " + TABLA_IMPRESORAS + " ADD " + CAMPO_SHOWNOMBREUSUARIO_IMPRESORA + " INT NOT NULL DEFAULT 1";
                        cmd.ExecuteNonQuery();
                        cmd.CommandText = "ALTER TABLE " + TABLA_IMPRESORAS + " ADD " + CAMPO_SHOWCODIGOUSUARIO_IMPRESORA + " INT NOT NULL DEFAULT 1";
                        cmd.ExecuteNonQuery();
                        cmd.CommandText = "ALTER TABLE " + TABLA_IMPRESORAS + " ADD " + CAMPO_SHOWFECHAHORA_IMPRESORA + " INT NOT NULL DEFAULT 1";
                        cmd.ExecuteNonQuery();
                    }
                    if (campos == 12)
                    {
                        SQLiteCommand cmd = new SQLiteCommand(db);
                        cmd.CommandText = "ALTER TABLE " + TABLA_IMPRESORAS + " ADD " + CAMPO_SHOWPORCENTAJEDESCUENTOMOVIMIENTO_IMPRESORA + " INT NOT NULL DEFAULT 1";
                        cmd.ExecuteNonQuery();
                    }
                        if (reader != null && !reader.IsClosed)
                        reader.Close();
                }
            }
        }

        public const String TABLA_BASCULA = "Scale";
        public const String CAMPO_ID_BASCULA = "id";
        public const String CAMPO_NOMBRE_BASCULA = "name";
        public const String CAMPO_NOMBREPUERTO_BASCULA = "port_name";
        public const String CAMPO_RANGODEBAUDIOS_BASCULA = "baud_rate";
        public const String CAMPO_PARIDAD_BASCULA = "parity";
        public const String CAMPO_BITSDEDATOS_BASCULA = "data_bits"; 
        public const String CAMPO_BITSDEPARADA_BASCULA = "stop_bits";

        public static String CREAR_TABLA_BASCULA = "CREATE TABLE IF NOT EXISTS " +
            TABLA_BASCULA + " (" + CAMPO_ID_BASCULA + " INTEGER PRIMARY KEY, " + CAMPO_NOMBRE_BASCULA + " TEXT, " + 
            CAMPO_NOMBREPUERTO_BASCULA + " TEXT, " + CAMPO_RANGODEBAUDIOS_BASCULA + " INTEGER DEFAULT 0, " + CAMPO_PARIDAD_BASCULA + " INTEGER DEFAULT 0, " +
            CAMPO_BITSDEDATOS_BASCULA + " INTEGER DEFAULT 8, " + CAMPO_BITSDEPARADA_BASCULA + " NUMERIC)";

        public const String TABLA_DATOSDISTRIBUIDOR = "DatosDistribuidor";
        public const String CAMPO_ID_DISTRIBUIDOR = "id";
        public const String CAMPO_NOMBRE_DISTRIBUIDOR = "nombre";
        public const String CAMPO_CALLE_DISTRIBUIDOR = "calle";
        public const String CAMPO_MUNICIPIO_DISTRIBUIDOR = "municipio";
        public const String CAMPO_NUMERO_DISTRIBUIDOR = "numero";
        public const String CAMPO_CP_DISTRIBUIDOR = "codigo_postal";
        public const String CAMPO_COLONIA_DISTRIBUIDOR = "colonia";
        public const String CAMPO_ESTADO_DISTRIBUIDOR = "estado";
        public const String CAMPO_CORREO1_DISTRIBUIDOR = "correo1";
        public const String CAMPO_CORREO2_DISTRIBUIDOR = "correo2";
        public const String CAMPO_PAGINAWEB_DISTRIBUIDOR = "pagina_web";
        public const String CAMPO_TELEFONO1_DISTRIBUIDOR = "telefono1";
        public const String CAMPO_TELEFONO2_DISTRIBUIDOR = "telefono2";
        public const String CAMPO_OTROS_DISTRIBUIDOR = "otros";
        public const String CAMPO_FOTO_DISTRIBUIDOR = "foto";

        public static String CREAR_TABLA_DATOSDISTRIBUIDOR = "CREATE TABLE IF NOT EXISTS " +
            TABLA_DATOSDISTRIBUIDOR + " (" + CAMPO_ID_DISTRIBUIDOR + " INTEGER PRIMARY KEY, " +
            CAMPO_NOMBRE_DISTRIBUIDOR + " TEXT, " + CAMPO_CALLE_DISTRIBUIDOR + " TEXT, " + CAMPO_MUNICIPIO_DISTRIBUIDOR + " TEXT, " + CAMPO_NUMERO_DISTRIBUIDOR + " TEXT, " +
            CAMPO_CP_DISTRIBUIDOR + " TEXT, " + CAMPO_COLONIA_DISTRIBUIDOR + " TEXT, " + CAMPO_ESTADO_DISTRIBUIDOR + " TEXT, " + CAMPO_CORREO1_DISTRIBUIDOR + " TEXT, " +
            CAMPO_CORREO2_DISTRIBUIDOR + " TEXT, " + CAMPO_PAGINAWEB_DISTRIBUIDOR + " TEXT, " + CAMPO_TELEFONO1_DISTRIBUIDOR + " TEXT, " + CAMPO_TELEFONO2_DISTRIBUIDOR + " TEXT, " +
            CAMPO_OTROS_DISTRIBUIDOR + " TEXT, " + CAMPO_FOTO_DISTRIBUIDOR + " TEXT)";

        public const String TABLA_LICENCIA = "Syncromlic";
        public const String CAMPO_ID_LICENCIA = "id";
        public const String CAMPO_CODIGO_DE_SITIO_LICENCIA = "codigo_sitio";
        public const String CAMPO_SYNCKEY_LICENCIA = "synckey_rom";
        public const String CAMPO_FECHA_FIN_LICENCIA = "fecha_fin";
        public const String CAMPO_X_LICENCIA = "x";
        public const String CAMPO_TIPOLIC_LICENCA = "tipo_lic";
        public const String CAMPO_IDE_LICENCIA = "idE";

        public static String CREAR_TABLA_LICENCIA = "CREATE TABLE IF NOT EXISTS " +
            TABLA_LICENCIA + " (" + CAMPO_ID_LICENCIA + " INTEGER PRIMARY KEY, " +
            CAMPO_CODIGO_DE_SITIO_LICENCIA + " TEXT, " + CAMPO_SYNCKEY_LICENCIA + " TEXT, "
            + CAMPO_FECHA_FIN_LICENCIA + " TEXT, " + CAMPO_X_LICENCIA + " TEXT, " + CAMPO_TIPOLIC_LICENCA + " INTEGER DEFAULT 2," + CAMPO_IDE_LICENCIA + " INTEGER DEFAULT 0)";

        public static void validateAndCreateNewFieldsInDatosLicencia(SQLiteConnection db)
        {
            String queryItem = "SELECT * FROM " + TABLA_LICENCIA;
            using (SQLiteCommand command = new SQLiteCommand(queryItem, db))
            {
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    int campoConcept = reader.FieldCount;  // see if the column is there
                    if (campoConcept < 7)
                    {
                        SQLiteCommand cmd = new SQLiteCommand(db);
                        cmd.CommandText = "ALTER TABLE " + TABLA_LICENCIA + " ADD " + CAMPO_IDE_LICENCIA + " INTEGER DEFAULT 0";
                        cmd.ExecuteNonQuery();
                    }
                    if (reader != null && !reader.IsClosed)
                        reader.Close();
                }
            }
        }

        public const String TABLA_SERVERDATA = "Syncromsdl";
        public const String CAMPO_ID_SERVERDATA = "id";
        public const String CAMPO_DATA_SERVERDATA = "data_encrypted";

        public static String CREAR_TABLA_SERVERDATA = "CREATE TABLE IF NOT EXISTS " +
            TABLA_SERVERDATA + " (" + CAMPO_ID_SERVERDATA + " INTEGER PRIMARY KEY, " +
            CAMPO_DATA_SERVERDATA + " TEXT)";

        public const String TABLA_FORMA_COBRO_DOCUMENTO = "FormaCobroDocumento";
        public const String CAMPO_ID_FORMACOBRODOC = "id";
        public const String CAMPO_FORMACOBROIDABONO_FORMACOBRODOC = "forma_cobro_id_abono";
        public const String CAMPO_IMPORTE_FORMACOBRODOC = "importe";
        public const String CAMPO_TOTALDOC_FORMACOBRODOC = "total_doc";
        public const String CAMPO_CAMBIO_FORMACOBRODOC = "cambio";
        public const String CAMPO_SALDODOC_FORMACOBRODOC = "saldo_doc";
        public const String CAMPO_DOCID_FORMACOBRODOC = "documento_id";
        public const String CAMPO_IDSERVER_FORMACOBRODOC = "id_server";

        public static String CREAR_TABLA_FORMACOBRODOC = "CREATE TABLE IF NOT EXISTS " + TABLA_FORMA_COBRO_DOCUMENTO + " (" + CAMPO_ID_FORMACOBRODOC + " INTEGER PRIMARY KEY, " +
            CAMPO_FORMACOBROIDABONO_FORMACOBRODOC + " INTEGER, " + CAMPO_IMPORTE_FORMACOBRODOC + " NUMERIC, " + CAMPO_TOTALDOC_FORMACOBRODOC + " NUMERIC, " +
            CAMPO_CAMBIO_FORMACOBRODOC + " NUMERIC, " + CAMPO_SALDODOC_FORMACOBRODOC + " NUMERIC, " + CAMPO_DOCID_FORMACOBRODOC + " INTEGER, " +
            CAMPO_IDSERVER_FORMACOBRODOC + " INTEGER DEFAULT 0)";

        public const String TABLA_CLIENTESNOVISITADOS = "ClientesNoVisitados";
        public const String CAMPO_ID_CLIENTESNOVISITADOS = "id";
        public const String CAMPO_CLAVECLI_CLIENTESNOVISITADOS = "clave_cliente";
        public const String CAMPO_IDAGENTE_CLIENTESNOVISITADOS = "usuario_id";
        public const String CAMPO_VISITAID_CLIENTESNOVISITADOS = "visita_id";
        public const String CAMPO_COMENTARIOS_CLIENTESNOVISITADOS = "comentarios";
        public const String CAMPO_FECHA_CLIENTESNOVISITADOS = "fecha";

        public static String CREAR_TABLA_CLIENTESNOVISITADOS = "CREATE TABLE IF NOT EXISTS " + TABLA_CLIENTESNOVISITADOS + " (" + CAMPO_ID_CLIENTESNOVISITADOS
            + " INTEGER PRIMARY KEY, " + CAMPO_CLAVECLI_CLIENTESNOVISITADOS + "  TEXT, " + CAMPO_IDAGENTE_CLIENTESNOVISITADOS + " INTEGER, " +
            CAMPO_VISITAID_CLIENTESNOVISITADOS + " INTEGER, " + CAMPO_COMENTARIOS_CLIENTESNOVISITADOS + " TEXT, " + CAMPO_FECHA_CLIENTESNOVISITADOS + " TEXT)";

        public const String TABLA_AYUDA = "Ayuda";
        public const String CAMPO_ID_AYUDA = "id";
        public const String CAMPO_SECCION_AYUDA = "seccion";
        public const String CAMPO_NOMBRE_AYUDA = "nombre";
        public const String CAMPO_DESCRIPCION_AYUDA = "descripción";
        public const String CAMPO_VISTO_AYUDA = "visto";

        public static String CREAR_TABLA_AYUDA = "CREATE TABLE IF NOT EXISTS " + TABLA_AYUDA + " (" + CAMPO_ID_AYUDA + " INTEGER PRIMARY KEY, " +
            CAMPO_SECCION_AYUDA + " TEXT, " + CAMPO_NOMBRE_AYUDA + " TEXT, " + CAMPO_DESCRIPCION_AYUDA + " TEXT, " + CAMPO_VISTO_AYUDA + " INTEGER DEFAULT 0)";

        public const String TABLA_INGRESO = "Ingreso";
        public const String CAMPO_ID_INGRESO = "id";
        public const String CAMPO_NUMERO_INGRESO = "numero";
        public const String CAMPO_IDUSUARIO_INGRESO = "id_usuario";
        public const String CAMPO_CLAVEUSUARIO_INGRESO = "clave_usuario";
        public const String CAMPO_FECHAHORA_INGRESO = "fecha_hora";
        public const String CAMPO_ENVIADO_INGRESO = "enviado";
        public const String CAMPO_IDSERVER_INGRESO = "id_server";
        public const String CAMPO_DOWNLOADED_INGRESO = "downloaded";
        public const String CAMPO_CONCEPT_INGRESO = "concept";
        public const String CAMPO_DESCRIPTION_INGRESO = "description";

        public static String CREATE_TABLE_INGRESO = "CREATE TABLE IF NOT EXISTS " + TABLA_INGRESO + " (" + CAMPO_ID_INGRESO + " INTEGER PRIMARY KEY, " +
            CAMPO_NUMERO_INGRESO + " INTEGER DEFAULT 0, " + CAMPO_IDUSUARIO_INGRESO + " INTEGER, " + CAMPO_CLAVEUSUARIO_INGRESO + " TEXT, " +
            CAMPO_FECHAHORA_INGRESO + " TEXT, " +
            CAMPO_ENVIADO_INGRESO + " INTEGER DEFAULT 0, " + CAMPO_IDSERVER_INGRESO + " INTEGER DEFAULT 0, " + CAMPO_DOWNLOADED_INGRESO + " INTEGER DEFAULT 0, " +
            CAMPO_CONCEPT_INGRESO + " TEXT, " + CAMPO_DESCRIPTION_INGRESO + " TEXT DEFAULT '')";

        public const String TABLA_MONTOINGRESO = "MontoIngreso";
        public const String CAMPO_ID_MONTOINGRESO = "id";
        public const String CAMPO_FORMACOBROID_MONTOINGRESO = "forma_cobro_id";
        public const String CAMPO_IMPORTE_MONTOINGRESO = "importe";
        public const String CAMPO_INGRESOID_MONTOINGRESO = "retiro_id";
        public const String CAMPO_ENVIADO_MONTOINGRESO = "enviado";
        public const String CAMPO_CREATEDAT_MONTOINGRESO = "createdAt";
        public const String CAMPO_UPDATEDAT_MONTOINGRESO = "updatedAt";

        public static String CREATE_TABLE_MONTOINGRESO = "CREATE TABLE IF NOT EXISTS " + TABLA_MONTOINGRESO + " (" + CAMPO_ID_MONTOINGRESO + " INTEGER PRIMARY KEY, " +
            CAMPO_FORMACOBROID_MONTOINGRESO + " INTEGER, " + CAMPO_IMPORTE_MONTOINGRESO + " NUMERIC, " + CAMPO_INGRESOID_MONTOINGRESO + " INTEGER, " +
            CAMPO_ENVIADO_MONTOINGRESO + " INTEGER DEFAULT 0, "+CAMPO_CREATEDAT_MONTOINGRESO+" TEXT NOT NULL DEFAULT '', "+
            CAMPO_UPDATEDAT_MONTOINGRESO+" TEXT NOT NULL DEFAULT '')";
        public static void validateAndCreateNewFieldsInMontoIngresos(SQLiteConnection db)
        {
            String queryItem = "SELECT * FROM " + TABLA_MONTOINGRESO;
            using (SQLiteCommand command = new SQLiteCommand(queryItem, db))
            {
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    int campoConcept = reader.FieldCount;  // see if the column is there
                    if (campoConcept == 5)
                    {
                        SQLiteCommand cmd = new SQLiteCommand(db);
                        cmd.CommandText = "ALTER TABLE " + TABLA_MONTOINGRESO + " ADD " + CAMPO_CREATEDAT_MONTOINGRESO + " TEXT NOT NULL DEFAULT ''";
                        cmd.ExecuteNonQuery();
                        cmd.CommandText = "ALTER TABLE " + TABLA_MONTOINGRESO + " ADD " + CAMPO_UPDATEDAT_MONTOINGRESO + " TEXT NOT NULL DEFAULT ''";
                        cmd.ExecuteNonQuery();
                    }
                    if (reader != null && !reader.IsClosed)
                        reader.Close();
                }
            }
        }


        public const String TABLA_RETIROS = "Retiro";
        public const String CAMPO_ID_RETIRO = "id";
        public const String CAMPO_NUMERO_RETIRO = "numero";
        public const String CAMPO_IDUSUARIO_RETIRO = "id_usuario";
        public const String CAMPO_CLAVEUSUARIO_RETIRO = "clave_usuario";
        public const String CAMPO_FECHAHORA_RETIRO = "fecha_hora";
        public const String CAMPO_ENVIADO_RETIRO = "enviado";
        public const String CAMPO_IDSERVER_RETIRO = "id_server";
        public const String CAMPO_DOWNLOADED_RETIRO = "downloaded";
        public const String CAMPO_CONCEPT_RETIRO = "concept";
        public const String CAMPO_DESCRIPTION_RETIRO = "description";

        public static String CREAR_TABLA_RETIRO = "CREATE TABLE IF NOT EXISTS " + TABLA_RETIROS + " (" + CAMPO_ID_RETIRO + " INTEGER PRIMARY KEY, " +
            CAMPO_NUMERO_RETIRO + " INTEGER, " + CAMPO_IDUSUARIO_RETIRO + " INTEGER, " + CAMPO_CLAVEUSUARIO_RETIRO + " TEXT, " + CAMPO_FECHAHORA_RETIRO + " TEXT, " +
            CAMPO_ENVIADO_RETIRO + " INTEGER DEFAULT 0, " + CAMPO_IDSERVER_RETIRO + " INTEGER DEFAULT 0, " + CAMPO_DOWNLOADED_RETIRO + " INTEGER DEFAULT 0, " +
            CAMPO_CONCEPT_RETIRO + " TEXT, "+ CAMPO_DESCRIPTION_RETIRO + " TEXT)";

        public static void validateAndCreateNewFieldsInRetiros(SQLiteConnection db)
        {
            String queryItem = "SELECT * FROM " + TABLA_RETIROS;
            using (SQLiteCommand command = new SQLiteCommand(queryItem, db))
            {
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    int campoConcept = reader.FieldCount;  // see if the column is there
                    if (campoConcept < 9)
                    {
                        SQLiteCommand cmd = new SQLiteCommand(db);
                        cmd.CommandText = "ALTER TABLE " + TABLA_RETIROS + " ADD " + CAMPO_CONCEPT_RETIRO + " TEXT";
                        cmd.ExecuteNonQuery();
                        cmd.CommandText = "ALTER TABLE " + TABLA_RETIROS + " ADD " + CAMPO_DESCRIPTION_RETIRO + " TEXT";
                        cmd.ExecuteNonQuery();
                    }
                    if (reader != null && !reader.IsClosed)
                        reader.Close();
                }
            }
        }

        public const String TABLA_MONTORETIROS = "MontoRetiro";
        public const String CAMPO_ID_MONTORETIROS = "id";
        public const String CAMPO_FORMACOBROID_MONTORETIROS = "forma_cobro_id";
        public const String CAMPO_IMPORTE_MONTORETIROS = "importe";
        public const String CAMPO_RETIROID_MONTORETIRO = "retiro_id";
        public const String CAMPO_ENVIADO_MONTORETIRO = "enviado";
        public const String CAMPO_CREATEDAT_MONTORETIRO = "createdAt";
        public const String CAMPO_UPDATEDAT_MONTORETIRO = "updatedAt";

        public static String CREAR_TABLA_MONTORETIRO = "CREATE TABLE IF NOT EXISTS " + TABLA_MONTORETIROS + " (" + CAMPO_ID_MONTORETIROS + " INTEGER PRIMARY KEY, " +
            CAMPO_FORMACOBROID_MONTORETIROS + " INTEGER, " + CAMPO_IMPORTE_MONTORETIROS + " NUMERIC, " + CAMPO_RETIROID_MONTORETIRO + " INTEGER, " +
            CAMPO_ENVIADO_MONTORETIRO + " INTEGER DEFAULT 0, "+ CAMPO_CREATEDAT_MONTORETIRO + " TEXT NOT NULL DEFAULT '', "+
            CAMPO_UPDATEDAT_MONTORETIRO+ " TEXT NOT NULL DEFAULT '')";

        public static void validateAndCreateNewFieldsInMontoRetiros(SQLiteConnection db)
        {
            String queryItem = "SELECT * FROM " + TABLA_MONTORETIROS;
            using (SQLiteCommand command = new SQLiteCommand(queryItem, db))
            {
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    int campoConcept = reader.FieldCount;  // see if the column is there
                    if (campoConcept == 5)
                    {
                        SQLiteCommand cmd = new SQLiteCommand(db);
                        cmd.CommandText = "ALTER TABLE " + TABLA_MONTORETIROS + " ADD " + CAMPO_CREATEDAT_MONTORETIRO + " TEXT NOT NULL DEFAULT ''";
                        cmd.ExecuteNonQuery();
                        cmd.CommandText = "ALTER TABLE " + TABLA_MONTORETIROS + " ADD " + CAMPO_UPDATEDAT_MONTORETIRO + " TEXT NOT NULL DEFAULT ''";
                        cmd.ExecuteNonQuery();
                    }
                    if (reader != null && !reader.IsClosed)
                        reader.Close();
                }
            }
        }

        public const String TABLA_CLASIFICACIONES = "Clasification";
        public const String CAMPO_ID_CLASIFICACION = "id";
        public const String CAMPO_NOMBRE_CLASIFICACION = "name";

        public static String CREAR_TABLA_CLASIFICACION = "CREATE TABLE IF NOT EXISTS " + TABLA_CLASIFICACIONES + " (" +
            CAMPO_ID_CLASIFICACION + " INTEGER PRIMARY KEY, " + CAMPO_NOMBRE_CLASIFICACION + " TEXT)";

        public const String TABLA_CLASIFICACION_VALOR = "ClasificationValue";
        public const String CAMPO_ID_CLASIVALOR = "id";
        public const String CAMPO_VALOR_CLASIVALOR = "value";
        public const String CAMPO_CLASIFICACIONID_CLASIVALOR = "clasification_id";
        public const String CAMPO_CODIGO_CLASIVALOR = "code";
        public const String CAMPO_SEGCONT1_CLASIVALOR = "seg_cont_1";
        public const String CAMPO_SEGCONT2_CLASIVALOR = "seg_cont_2";
        public const String CAMPO_SEGCONT3_CLASIVALOR = "seg_cont_3";

        public static String CREAR_TABLA_CLASIFICACIONVALOR = "CREATE TABLE IF NOT EXISTS " + TABLA_CLASIFICACION_VALOR + " (" +
            CAMPO_ID_CLASIVALOR + " INTEGER PRIMARY KEY, " + CAMPO_VALOR_CLASIVALOR + " TEXT, " + CAMPO_CLASIFICACIONID_CLASIVALOR + " INTEGER, " +
            CAMPO_CODIGO_CLASIVALOR + " TEXT, " + CAMPO_SEGCONT1_CLASIVALOR + " INTEGER, " + CAMPO_SEGCONT2_CLASIVALOR + " INTEGER, " +
            CAMPO_SEGCONT3_CLASIVALOR + " INTEGER)";

        public const String TABLA_PROMOCIONES = "Promotion";
        public const String CAMPO_ID_PROMOTION = "id";
        public const String CAMPO_CODIGO_PROMOTION = "code";
        public const String CAMPO_NOMBRE_PROMOTION = "name";
        public const String CAMPO_FECHAINICIO_PROMOTION = "start_date";
        public const String CAMPO_FECHAFIN_PROMOTION = "end_date";
        public const String CAMPO_VOLUMENMINIMO_PROMOTION = "min_volume";
        public const String CAMPO_VOLUMENMAXIMO_PROMOTION = "max_volume";
        public const String CAMPO_PORCENTAJEDESCUENTO_PROMOTION = "discount_rate";
        public const String CAMPO_CLASICLIENTEID1_PROMOTION = "customer_clasification_id_1";
        public const String CAMPO_CLASICLIENTEID2_PROMOTION = "customer_clasification_id_2";
        public const String CAMPO_CLASICLIENTEID3_PROMOTION = "customer_clasification_id_3";
        public const String CAMPO_CLASICLIENTEID4_PROMOTION = "customer_clasification_id_4";
        public const String CAMPO_CLASICLIENTEID5_PROMOTION = "customer_clasification_id_5";
        public const String CAMPO_CLASICLIENTEID6_PROMOTION = "customer_clasification_id_6";
        public const String CAMPO_CLASIPRODUCTOID1_PROMOTION = "item_clasification_id_1";
        public const String CAMPO_CLASIPRODUCTOID2_PROMOTION = "item_clasification_id_2";
        public const String CAMPO_CLASIPRODUCTOID3_PROMOTION = "item_clasification_id_3";
        public const String CAMPO_CLASIPRODUCTOID4_PROMOTION = "item_clasification_id_4";
        public const String CAMPO_CLASIPRODUCTOID5_PROMOTION = "item_clasification_id_5";
        public const String CAMPO_CLASIPRODUCTOID6_PROMOTION = "item_clasification_id_6";
        public const String CAMPO_CREADO_PROMOTION = "created_at";
        public const String CAMPO_TIPOPROMO_PROMOTION = "type";
        public const String CAMPO_CONCEPTODOCUMENTO_PROMOTION = "document_concept";
        public const String CAMPO_SUBTIPO_PROMOTION = "subtype";
        public const String CAMPO_HORAINICIO_PROMOTION = "start_time";
        public const String CAMPO_HORAFIN_PROMOTION = "end_time";
        public const String CAMPO_TIPOPRO_PROMOTION = "pro_type";
        public const String CAMPO_VALA_PROMOTION = "val_a";
        public const String CAMPO_VALB_PROMOTION = "val_b";
        public const String CAMPO_DIAS_PROMOTION = "days";
        public const String CAMPO_FECHAALTA_PROMOTION = "high_date";
        public const String CAMPO_STATUS_PROMOTION = "status";

        public static String CREAR_TABLA_PROMOCIONES = "CREATE TABLE IF NOT EXISTS " + TABLA_PROMOCIONES + " (" +
            CAMPO_ID_PROMOTION + " INTEGER PRIMARY KEY, " + CAMPO_CODIGO_PROMOTION + " TEXT, " + CAMPO_NOMBRE_PROMOTION + " TEXT, " +
            CAMPO_FECHAINICIO_PROMOTION + " TEXT, " + CAMPO_FECHAFIN_PROMOTION + " TEXT, " + CAMPO_VOLUMENMINIMO_PROMOTION + " NUMERIC, " +
            CAMPO_VOLUMENMAXIMO_PROMOTION + " NUMERIC, " + CAMPO_PORCENTAJEDESCUENTO_PROMOTION + " NUMERIC, " +
            CAMPO_CLASICLIENTEID1_PROMOTION + " INTEGER, " + CAMPO_CLASICLIENTEID2_PROMOTION + " INTEGER, " +
            CAMPO_CLASICLIENTEID3_PROMOTION + " INTEGER, " + CAMPO_CLASICLIENTEID4_PROMOTION + " INTEGER, " +
            CAMPO_CLASICLIENTEID5_PROMOTION + " INTEGER, " + CAMPO_CLASICLIENTEID6_PROMOTION + " INTEGER, " +
            CAMPO_CLASIPRODUCTOID1_PROMOTION + " INTEGER, " + CAMPO_CLASIPRODUCTOID2_PROMOTION + " INTEGER, " +
            CAMPO_CLASIPRODUCTOID3_PROMOTION + " INTEGER, " + CAMPO_CLASIPRODUCTOID4_PROMOTION + " INTEGER, " +
            CAMPO_CLASIPRODUCTOID5_PROMOTION + " INTEGER, " + CAMPO_CLASIPRODUCTOID6_PROMOTION + " INTEGER, " +
            CAMPO_CREADO_PROMOTION + " TEXT, " + CAMPO_TIPOPROMO_PROMOTION + " INTEGER, " + CAMPO_CONCEPTODOCUMENTO_PROMOTION + " INTEGER, " +
            CAMPO_SUBTIPO_PROMOTION + " INTEGER, " + CAMPO_HORAINICIO_PROMOTION + " TEXT, " + CAMPO_HORAFIN_PROMOTION + " TEXT, " +
            CAMPO_TIPOPRO_PROMOTION + " INTEGER, " + CAMPO_VALA_PROMOTION + " INTEGER, " + CAMPO_VALB_PROMOTION + " INTEGER, " +
            CAMPO_DIAS_PROMOTION + " INTEGER, " + CAMPO_FECHAALTA_PROMOTION + " TEXT, " + CAMPO_STATUS_PROMOTION + " INTEGER)";

        public const String TABLA_UNITMEASUREWEIGHT = "UnitMeasureWeight";
        public const String CAMPO_ID_UNITMEASUREWEIGHT = "id";
        public const String CAMPO_IDSERVER_UNITMEASUREWEIGHT = "id_server";
        public const String CAMPO_NAME_UNITMEASUREWEIGHT = "name";
        public const String CAMPO_ABBREVIATION_UNITMEASUREWEIGHT = "abbreviation";
        public const String CAMPO_DEPLOYMENT_UNITMEASUREWEIGHT = "deployment";
        public const String CAMPO_SATKEY_UNITMEASUREWEIGHT = "sat_key";
        public const String CAMPO_FOREIGNTRADEKEY_UNITMEASUREWEIGHT = "foreign_trade_key";

        public static String CREATE_TABLE_UNITMEASUREWEITH = "CREATE TABLE IF NOT EXISTS " + TABLA_UNITMEASUREWEIGHT + " (" +
            CAMPO_ID_UNITMEASUREWEIGHT + " INTEGER PRIMARY KEY, " + CAMPO_IDSERVER_UNITMEASUREWEIGHT + " INTEGER, "
            + CAMPO_NAME_UNITMEASUREWEIGHT + " TEXT, " + CAMPO_ABBREVIATION_UNITMEASUREWEIGHT + " TEXT, " +
            CAMPO_DEPLOYMENT_UNITMEASUREWEIGHT + " TEXT, " + CAMPO_SATKEY_UNITMEASUREWEIGHT + " TEXT, " +
            CAMPO_FOREIGNTRADEKEY_UNITMEASUREWEIGHT + " TEXT)";

        public const String TABLA_UNITCONVERSION = "UnitConversion";
        public const String CAMPO_ID_UNITCONVERSION = "id";
        public const String CAMPO_IDSERVER_UNITCONVERSION = "id_server";
        public const String CAMPO_UNITONE_UNITCONVERSION = "unit_one";
        public const String CAMPO_UNITTWO_UNITCONVERSION = "unit_two";
        public const String CAMPO_CONVERSIONFACTOR_UNITCONVERSION = "conversion_factor";

        public static String CREATE_TABLE_UNITCONVERSION = "CREATE TABLE IF NOT EXISTS " + TABLA_UNITCONVERSION + " (" +
            CAMPO_ID_UNITCONVERSION + " INTEGER PRIMARY KEY, " + CAMPO_IDSERVER_UNITCONVERSION + " INTEGER, " +
            CAMPO_UNITONE_UNITCONVERSION + " TEXT, " + CAMPO_UNITTWO_UNITCONVERSION + " TEXT, " +
            CAMPO_CONVERSIONFACTOR_UNITCONVERSION + " TEXT)";

        public const String TABLA_RUTA = "Route";
        public const String CAMPO_ID_RUTA = "id";
        public const String CAMPO_CODE_RUTA = "code";
        public const String CAMPO_NAME_RUTA = "name";
        public const String CAMPO_COLOR_RUTA = "color";
        public const String CAMPO_CREATEDAT_RUTA = "created_at";

        public static String CREATE_TABLE_RUTA = "CREATE TABLE IF NOT EXISTS " + TABLA_RUTA + " (" +
            CAMPO_ID_RUTA + " INTEGER PRIMARY KEY, " + CAMPO_CODE_RUTA + " TEXT, " +
            CAMPO_NAME_RUTA + " TEXT, " + CAMPO_COLOR_RUTA + " TEXT, " +
            CAMPO_CREATEDAT_RUTA + " TEXT)";

        public const String TABLA_TARA = "Taras";
        public const String CAMPO_ID_TARA = "id";
        public const String CAMPO_CODE_TARA = "code";
        public const String CAMPO_NAME_TARA = "name";
        public const String CAMPO_COLOR_TARA = "color";
        public const String CAMPO_PESO_TARA = "peso";
        public const String CAMPO_TIPO_TARA = "tipo";
        public const String CAMPO_CREATEDAT_TARA = "created_at";

        public static String CREATE_TABLE_TARA = "CREATE TABLE IF NOT EXISTS " + TABLA_TARA + " (" +
            CAMPO_ID_TARA + " INTEGER PRIMARY KEY, " + CAMPO_CODE_TARA + " TEXT, " +
            CAMPO_NAME_TARA + " TEXT, " + CAMPO_COLOR_TARA + " TEXT, " + CAMPO_PESO_TARA+" NUMERIC DEFAULT 0, "+
            CAMPO_TIPO_TARA+" TEXT, "+CAMPO_CREATEDAT_TARA + " DATETIME DEFAULT CURRENT_TIMESTAMP)";

        public static void validateAndCreateNewFieldsInTaras(SQLiteConnection db)
        {
            String query = "SELECT * FROM " + TABLA_TARA;
            using (SQLiteCommand command = new SQLiteCommand(query, db))
            {
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    if (!reader.HasRows)
                    {
                        query = "INSERT INTO " + TABLA_TARA + " VALUES("+1+", 'TARA1', 'Tara Número 1', 'Sin Color', 0, 'A', '"+DateTime.Now.ToString("yyyy-MM-dd HH:MM:ss") +"')";
                        TaraModel.createUpdateOrDelete(db, query);
                        query = "INSERT INTO " + TABLA_TARA + " VALUES(" + 2 + ", 'TARA2', 'Tara Número 2', 'Sin Color', 0, 'B', '" + DateTime.Now.ToString("yyyy-MM-dd HH:MM:ss") + "')";
                        TaraModel.createUpdateOrDelete(db, query);
                        query = "INSERT INTO " + TABLA_TARA + " VALUES(" + 3 + ", 'TARA3', 'Tara Número 3', 'Sin Color', 0, 'C', '" + DateTime.Now.ToString("yyyy-MM-dd HH:MM:ss") + "')";
                        TaraModel.createUpdateOrDelete(db, query);
                    }
                    if (reader != null && !reader.IsClosed)
                        reader.Close();
                }
            }
        }

        public const String TABLA_BUSQUEDAS = "Busquedas";
        public const String CAMPO_ID_BUSQUEDAS = "id";
        public const String CAMPO_MODELO_BUSQUEDAS = "modelo"; //1 = articulos, 2 = clientes
        public const String CAMPO_POSICIONCOINCIDENCIA_BUSQUEDAS = "posicionCoincidencia"; //0 = cualquier lado, 1 = al principio, 2 = al final
        public const String CAMPO_CAMPOSCONCIDERADOS_BUSQUEDAS = "camposConciderados"; //0 = codigo y nombre, 1 = código, 2 = nombre

        public static String CREATE_TABLE_BUSQUEDAS = "CREATE TABLE IF NOT EXISTS " + TABLA_BUSQUEDAS + " (" +
            CAMPO_ID_BUSQUEDAS + " INTEGER PRIMARY KEY, " + CAMPO_MODELO_BUSQUEDAS + " INT NOT NULL DEFAULT 0, " +
            CAMPO_POSICIONCOINCIDENCIA_BUSQUEDAS + " INT NOT NULL DEFAULT 0, " + CAMPO_CAMPOSCONCIDERADOS_BUSQUEDAS + " INT NOT NULL DEFAULT 0)";

    }
}
