using SyncTPV.Controllers;
using SyncTPV.Helpers.SqliteDatabaseHelper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Dynamic;

namespace SyncTPV
{

    public class UserModel
    {
        public static readonly int TYPE_ROM = 1;
        public static readonly int TYPE_CAJA_GENERAL = 2;
        public static readonly int TYPE_SUPERVISOR = 3;
        public static readonly int TYPE_TELLER = 4;
        public int id { get; set; }
        public String Clave { get; set; }
        public String pass { get; set; }
        public String Nombre { get; set; }
        public String Ruta { get; set; }
        public int almacen_id { get; set; }
        public String Valida_Inventario_Ventas { get; set; }
        public String UsaQR { get; set; }
        public String ModVentas { get; set; }
        public String ModCobranza { get; set; }
        public String datos { get; set; }
        public String Gps { get; set; }
        public String AbrirCaja { get; set; }
        public String PCobranza { get; set; }
        public String Pventa { get; set; }
        public String PPedido { get; set; }
        public String PCotizacion { get; set; }
        public String PInformacion { get; set; }
        public String PVisita { get; set; }
        public String PHistorial { get; set; }
        public String PDevolucion { get; set; }
        public String ModPedido { get; set; }
        public String ModPrecio { get; set; }
        public String Factura { get; set; }
        public String ValidaCaja { get; set; }
        public int Limite_Credito { get; set; }
        public int Precio_Empresa_ID { get; set; }
        public String Mod_Descuento { get; set; }
        public String Alta_Cliente { get; set; }
        public String Sig_Folio { get; set; }
        public int banSesion { get; set; }
        public double descMaximo { get; set; }
        public int tipoUsuario { get; set; }
        public int idCaja { get; set; }
        public String cajaCodigo { get; set; }
        public String cajaNombre { get; set; }
        public int permisoLimitarCredito { get; set; }
        public int connected { get; set; }
        public String lastLogin { get; set; }
        public int permisoFacturarCredito { get; set; }
        public String lastSeen { get; set; }
        public int traspasoAlmacen { get; set; }
        public int salidaAlmacen { get; set; }
        public int selectPrice { get; set; }
        public int prepedido { get; set; }
        public int todosLosClientes { get; set; }
        public int enterpriseId { get; set; }
        public int venderSinExistencia { get; set; }
        public int venderACredito { get; set; }

        public static int saveAllUsers(List<clsAgentes> usersList)
        {
            int lastId = 0;
            var conn = new SQLiteConnection();
            try {
                conn.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                conn.Open();
                if (usersList != null) {
                    int countUsers = 0;
                    int items = usersList.Count;
                    foreach (var user in usersList) {
                        String query = "INSERT INTO " + LocalDatabase.TABLA_USUARIO +
                            " VALUES (@id, @codigo, @password, @name, @route, @almacenId, @validarInventarioVentas, @usarQR, @modVentas," +
                                       " @modCobranza, @usoDatos, @gps, @abrirCaja, @pCobranza, @pVenta, @pPedido, @pCotizacion," +
                                       " @pInformacion, @pVisita, @pHistorial, @pDevolucion, @modPedidos, @modPrecio, @factura, @validaCaja," +
                                       " @limiteCredito, @precioEmpresaId, @modDescuento, @altaCliente, @sigFolio, @banSesion, @tipoUsuario," +
                                       " @cajaId, @cajaCodigo, @cajaNombre, @permisoLimitarCredito, @connected, @lastLogin, " +
                                       "@facturarCredito, @lastSeen, @traspasoAlmacen, @salidaAlmacen, @selectPrice, @prepedido, @todosLosClientes," +
                                       "@enterpriseId, @venderSinExistencia, @venderACredito, @descMaximo)";
                        using (SQLiteCommand command = new SQLiteCommand(query, conn)) {
                            command.Parameters.AddWithValue("@id", user.USUARIO_ID);
                            command.Parameters.AddWithValue("@codigo", user.CLAVE);
                            command.Parameters.AddWithValue("@password", user.PASS);
                            command.Parameters.AddWithValue("@name", user.NOMBRE);
                            command.Parameters.AddWithValue("@route", user.RUTA);
                            command.Parameters.AddWithValue("@almacenId", user.ALMACEN_ID);
                            command.Parameters.AddWithValue("@validarInventarioVentas", user.VALIDA_INVENTARIO_VENTAS);
                            command.Parameters.AddWithValue("@usarQR", user.USAQR);
                            command.Parameters.AddWithValue("@modVentas", user.MODVENTAS);
                            command.Parameters.AddWithValue("@modCobranza", user.MODCOBRANZA);
                            command.Parameters.AddWithValue("@usoDatos", user.DATOS);
                            command.Parameters.AddWithValue("@gps", user.GPS);
                            command.Parameters.AddWithValue("@abrirCaja", user.ABRIRCAJA);
                            command.Parameters.AddWithValue("@pCobranza", user.PCOBRANZA);
                            command.Parameters.AddWithValue("@pVenta", user.PVENTA);
                            command.Parameters.AddWithValue("@pPedido", user.PPEDIDO);
                            command.Parameters.AddWithValue("@pCotizacion", user.PCOTIZACION);
                            command.Parameters.AddWithValue("@pInformacion", user.PINFORMACION);
                            command.Parameters.AddWithValue("@pVisita", user.PVISITA);
                            command.Parameters.AddWithValue("@pHistorial", user.PHISTORIAL);
                            command.Parameters.AddWithValue("@pDevolucion", user.PDEVOLUCIONES);
                            command.Parameters.AddWithValue("@modPedidos", user.MODPEDIDOS);
                            command.Parameters.AddWithValue("@modPrecio", user.MODPRECIO);
                            command.Parameters.AddWithValue("@factura", user.FACTURA);
                            command.Parameters.AddWithValue("@validaCaja", user.VALIDACAJA);
                            command.Parameters.AddWithValue("@limiteCredito", user.LIMITE_CREDITO);
                            command.Parameters.AddWithValue("@precioEmpresaId", user.PRECIO_EMPRESA_ID);
                            command.Parameters.AddWithValue("@modDescuento", user.MOD_DESCUENTO);
                            command.Parameters.AddWithValue("@altaCliente", user.ALTACLIENTE);
                            command.Parameters.AddWithValue("@sigFolio", user.SIG_FOLIO);
                            command.Parameters.AddWithValue("@banSesion", "0");
                            command.Parameters.AddWithValue("@tipoUsuario", user.TIPOUSUARIO);
                            command.Parameters.AddWithValue("@cajaId", user.CAJAID);
                            command.Parameters.AddWithValue("@cajaCodigo", user.CAJACODIGO);
                            command.Parameters.AddWithValue("@cajaNombre", user.CAJANOMBRE);
                            command.Parameters.AddWithValue("@permisoLimitarCredito", user.permisoLimitarCredito);
                            command.Parameters.AddWithValue("@connected", user.connected);
                            command.Parameters.AddWithValue("@lastLogin", user.lastLogin);
                            command.Parameters.AddWithValue("@facturarCredito", user.permisoFacturarCredito);
                            command.Parameters.AddWithValue("@lastSeen", user.lastSeen);
                            command.Parameters.AddWithValue("@traspasoAlmacen", user.traspasosAlmacen);
                            command.Parameters.AddWithValue("@salidaAlmacen", user.salidasAlmacen);
                            command.Parameters.AddWithValue("@selectPrice", user.permisoSelectPrice);
                            command.Parameters.AddWithValue("@prepedido", user.permisoPrePedido);
                            command.Parameters.AddWithValue("@todosLosClientes", user.todosLosClientes);
                            command.Parameters.AddWithValue("@enterpriseId", user.enterpriseId);
                            command.Parameters.AddWithValue("@venderSinExistencia", user.venderSinExistencia);
                            command.Parameters.AddWithValue("@venderACredito", user.venderACredito);
                            command.Parameters.AddWithValue("@descMaximo", user.descMaximo);
                            lastId = command.ExecuteNonQuery();
                            if (lastId != 0)
                                countUsers++;
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
                if (conn != null && conn.State == ConnectionState.Open)
                    conn.Close();
            }
            return lastId;
        }

        public static bool createAgent(clsAgentes user)
        {
            bool created = false;
            var conn = new SQLiteConnection();
            try
            {
                conn.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                conn.Open();
                String query = "INSERT INTO " + LocalDatabase.TABLA_USUARIO +
                            " VALUES (@id, @codigo, @password, @name, @route, @almacenId, @validarInventarioVentas, @usarQR, @modVentas," +
                                       " @modCobranza, @usoDatos, @gps, @abrirCaja, @pCobranza, @pVenta, @pPedido, @pCotizacion," +
                                       " @pInformacion, @pVisita, @pHistorial, @pDevolucion, @modPedidos, @modPrecio, @factura, @validaCaja," +
                                       " @limiteCredito, @precioEmpresaId, @modDescuento, @altaCliente, @sigFolio, @banSesion, @tipoUsuario," +
                                       " @cajaId, @cajaCodigo, @cajaNombre, @permisoLimitarCredito, @connected, @lastLogin, " +
                                       "@facturarCredito, @lastSeen, @traspasoAlmacen, @salidaAlmacen, @selectPrice, @prepedido, @todosLosClientes," +
                                       "@enterpriseId, @venderSinExistencia, @venderACredito, @descMaximo)";
                using (SQLiteCommand command = new SQLiteCommand(query, conn))
                {
                    command.Parameters.AddWithValue("@id", user.USUARIO_ID);
                    command.Parameters.AddWithValue("@codigo", user.CLAVE);
                    command.Parameters.AddWithValue("@password", user.PASS);
                    command.Parameters.AddWithValue("@name", user.NOMBRE);
                    command.Parameters.AddWithValue("@route", user.RUTA);
                    command.Parameters.AddWithValue("@almacenId", user.ALMACEN_ID);
                    command.Parameters.AddWithValue("@validarInventarioVentas", user.VALIDA_INVENTARIO_VENTAS);
                    command.Parameters.AddWithValue("@usarQR", user.USAQR);
                    command.Parameters.AddWithValue("@modVentas", user.MODVENTAS);
                    command.Parameters.AddWithValue("@modCobranza", user.MODCOBRANZA);
                    command.Parameters.AddWithValue("@usoDatos", user.DATOS);
                    command.Parameters.AddWithValue("@gps", user.GPS);
                    command.Parameters.AddWithValue("@abrirCaja", user.ABRIRCAJA);
                    command.Parameters.AddWithValue("@pCobranza", user.PCOBRANZA);
                    command.Parameters.AddWithValue("@pVenta", user.PVENTA);
                    command.Parameters.AddWithValue("@pPedido", user.PPEDIDO);
                    command.Parameters.AddWithValue("@pCotizacion", user.PCOTIZACION);
                    command.Parameters.AddWithValue("@pInformacion", user.PINFORMACION);
                    command.Parameters.AddWithValue("@pVisita", user.PVISITA);
                    command.Parameters.AddWithValue("@pHistorial", user.PHISTORIAL);
                    command.Parameters.AddWithValue("@pDevolucion", user.PDEVOLUCIONES);
                    command.Parameters.AddWithValue("@modPedidos", user.MODPEDIDOS);
                    command.Parameters.AddWithValue("@modPrecio", user.MODPRECIO);
                    command.Parameters.AddWithValue("@factura", user.FACTURA);
                    command.Parameters.AddWithValue("@validaCaja", user.VALIDACAJA);
                    command.Parameters.AddWithValue("@limiteCredito", user.LIMITE_CREDITO);
                    command.Parameters.AddWithValue("@precioEmpresaId", user.PRECIO_EMPRESA_ID);
                    command.Parameters.AddWithValue("@modDescuento", user.MOD_DESCUENTO);
                    command.Parameters.AddWithValue("@altaCliente", user.ALTACLIENTE);
                    command.Parameters.AddWithValue("@sigFolio", user.SIG_FOLIO);
                    command.Parameters.AddWithValue("@banSesion", "0");
                    command.Parameters.AddWithValue("@tipoUsuario", user.TIPOUSUARIO);
                    command.Parameters.AddWithValue("@cajaId", user.CAJAID);
                    command.Parameters.AddWithValue("@cajaCodigo", user.CAJACODIGO);
                    command.Parameters.AddWithValue("@cajaNombre", user.CAJANOMBRE);
                    command.Parameters.AddWithValue("@permisoLimitarCredito", user.permisoLimitarCredito);
                    command.Parameters.AddWithValue("@connected", user.connected);
                    command.Parameters.AddWithValue("@lastLogin", user.lastLogin);
                    command.Parameters.AddWithValue("@facturarCredito", user.permisoFacturarCredito);
                    command.Parameters.AddWithValue("@lastSeen", user.lastSeen);
                    command.Parameters.AddWithValue("@traspasoAlmacen", user.traspasosAlmacen);
                    command.Parameters.AddWithValue("@salidaAlmacen", user.salidasAlmacen);
                    command.Parameters.AddWithValue("@selectPrice", user.permisoSelectPrice);
                    command.Parameters.AddWithValue("@prepedido", user.permisoPrePedido);
                    command.Parameters.AddWithValue("@todosLosClientes", user.todosLosClientes);
                    command.Parameters.AddWithValue("@enterpriseId", user.enterpriseId);
                    command.Parameters.AddWithValue("@venderSinExistencia", user.venderSinExistencia);
                    command.Parameters.AddWithValue("@venderACredito", user.venderACredito);
                    command.Parameters.AddWithValue("@descMaximo", user.descMaximo);
                    int records = command.ExecuteNonQuery();
                    if (records > 0)
                        created = true;
                }
            }
            catch (SQLiteException e)
            {
                SECUDOC.writeLog(e.ToString());
            }
            finally
            {
                if (conn != null && conn.State == ConnectionState.Open)
                    conn.Close();
            }
            return created;
        }

        public static bool updateAgent(clsAgentes user)
        {
            bool updated = false;
            var conn = new SQLiteConnection();
            try
            {
                conn.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                conn.Open();
                String query = "UPDATE " + LocalDatabase.TABLA_USUARIO +
                           " SET "+LocalDatabase.CAMPO_ClAVE_USUARIO+" = @codigo, " +
                           LocalDatabase.CAMPO_PASSWORD_USUARIO+" = @password, "+LocalDatabase.CAMPO_NOMBRE_USER+" = @name, " +
                           LocalDatabase.CAMPO_RUTA_USER+" = @route, "+LocalDatabase.CAMPO_ALMACENID_USER + " = @almacenId, " +
                           LocalDatabase.CAMPO_VALIDA_INVENTARIO+" = @validarInventarioVentas, "+LocalDatabase.CAMPO_USAQR+" = @usarQR, " +
                           LocalDatabase.CAMPO_PERMISO_MODVENTAS+" = @modVentas,"+LocalDatabase.CAMPO_PERMISO_MODCOBRANZA+" = @modCobranza, " +
                           LocalDatabase.CAMPO_DATOS+" = @usoDatos, "+LocalDatabase.CAMPO_PERMISO_GPS_USUARIO+" = @gps, " +
                           LocalDatabase.CAMPO_ABRIRCAJA+" = @abrirCaja, "+LocalDatabase.CAMPO_PERMISO_COBRANZA_USUARIO+" = @pCobranza, " +
                           LocalDatabase.CAMPO_PVENTA+" = @pVenta, "+LocalDatabase.CAMPO_PPEDIDO+" = @pPedido, " +
                           LocalDatabase.CAMPO_PERMISO_COTIZACION_USUARIO+" = @pCotizacion,"+LocalDatabase.CAMPO_PINFORMACION+" = @pInformacion, " +
                           LocalDatabase.CAMPO_PVISITA+" = @pVisita, "+LocalDatabase.CAMPO_PHISTORIAL+" = @pHistorial, "+
                           LocalDatabase.CAMPO_PDEVOLUCIONES+" = @pDevolucion, "+LocalDatabase.CAMPO_PERMISO_MODPEDIDOS_USUARIO+" = @modPedidos, " +
                           LocalDatabase.CAMPO_MODPRECIO_USER+" = @modPrecio, "+LocalDatabase.CAMPO_PERMISO_FACTURAR_USUARIO+" = @factura, " +
                           LocalDatabase.CAMPO_VALIDACAJA+" = @validaCaja,"+LocalDatabase.CAMPO_LIMITE_CREDITO+" = @limiteCredito, " +
                           LocalDatabase.CAMPO_PRECIO_EMPRESA_ID_USUARIO+" = @precioEmpresaId, "+
                           LocalDatabase.CAMPO_MODPRECIO_USER+" = @modDescuento, "+LocalDatabase.CAMPO_PERMISO_ALTACLIENTE+" = @altaCliente, " +
                           LocalDatabase.CAMPO_SIG_FOLIO+" = @sigFolio, "+LocalDatabase.CAMPO_BANSESION+" = @banSesion, "+
                           LocalDatabase.CAMPO_TIPOUSUARIO_USUARIO+" = @tipoUsuario,"+LocalDatabase.CAMPO_CAJAID_USUARIO+" = @cajaId, " +
                           LocalDatabase.CAMPO_CAJACODIGO_USUARIO+" = @cajaCodigo, "+LocalDatabase.CAMPO_CAJANOMBRE_USUARIO+" = @cajaNombre, " +
                           LocalDatabase.CAMPO_PERMISO_LIMITARCREDITO_USUARIO+" = @permisoLimitarCredito, "+
                           LocalDatabase.CAMPO_CONNECTED_USUARIO+" = @connected, "+LocalDatabase.CAMPO_LASTLOGIN_USUARIO+" = @lastLogin, " +
                           LocalDatabase.CAMPO_PERMISO_FACTURAR_USUARIO+" = @facturarCredito, "+LocalDatabase.CAMPO_LASTSEEN_USUARIO+" = @lastSeen, " +
                           LocalDatabase.CAMPO_PERMTRASPASOALMACEN_USUARIO+" = @traspasoAlmacen, "+
                           LocalDatabase.CAMPO_PERMSALIDAALMACEN_USUARIO + " = @salidaAlmacen, "+
                           LocalDatabase.CAMPO_PERMSELECTPRICE_USUARIO + " = @selectPrice, "+
                           LocalDatabase.CAMPO_PERMPREPEDIDO_USUARIO + " = @prepedido, "+
                           LocalDatabase.CAMPO_TODOSLOSCLIENTES_USUARIO + " = @todosLosClientes, "+
                           LocalDatabase.CAMPO_ENTERPRISEID_USUARIO+ " = @enterpriseId, " +
                           LocalDatabase.CAMPO_VENDERSINEXISTENCIAS_USUARIO+" = @venderSinExistencia, " +
                        LocalDatabase.CAMPO_VENDERACREDITO_USUARIO+" = @venderACredito, " +
                        LocalDatabase.CAMPO_DESCMAXIMO_USUARIO+" = @descMaximo WHERE " + LocalDatabase.CAMPO_ID_USUARIO+ " = @id";
                using (SQLiteCommand command = new SQLiteCommand(query, conn))
                {
                    command.Parameters.AddWithValue("@id", user.USUARIO_ID);
                    command.Parameters.AddWithValue("@codigo", user.CLAVE);
                    command.Parameters.AddWithValue("@password", user.PASS);
                    command.Parameters.AddWithValue("@name", user.NOMBRE);
                    command.Parameters.AddWithValue("@route", user.RUTA);
                    command.Parameters.AddWithValue("@almacenId", user.ALMACEN_ID);
                    command.Parameters.AddWithValue("@validarInventarioVentas", user.VALIDA_INVENTARIO_VENTAS);
                    command.Parameters.AddWithValue("@usarQR", user.USAQR);
                    command.Parameters.AddWithValue("@modVentas", user.MODVENTAS);
                    command.Parameters.AddWithValue("@modCobranza", user.MODCOBRANZA);
                    command.Parameters.AddWithValue("@usoDatos", user.DATOS);
                    command.Parameters.AddWithValue("@gps", user.GPS);
                    command.Parameters.AddWithValue("@abrirCaja", user.ABRIRCAJA);
                    command.Parameters.AddWithValue("@pCobranza", user.PCOBRANZA);
                    command.Parameters.AddWithValue("@pVenta", user.PVENTA);
                    command.Parameters.AddWithValue("@pPedido", user.PPEDIDO);
                    command.Parameters.AddWithValue("@pCotizacion", user.PCOTIZACION);
                    command.Parameters.AddWithValue("@pInformacion", user.PINFORMACION);
                    command.Parameters.AddWithValue("@pVisita", user.PVISITA);
                    command.Parameters.AddWithValue("@pHistorial", user.PHISTORIAL);
                    command.Parameters.AddWithValue("@pDevolucion", user.PDEVOLUCIONES);
                    command.Parameters.AddWithValue("@modPedidos", user.MODPEDIDOS);
                    command.Parameters.AddWithValue("@modPrecio", user.MODPRECIO);
                    command.Parameters.AddWithValue("@factura", user.FACTURA);
                    command.Parameters.AddWithValue("@validaCaja", user.VALIDACAJA);
                    command.Parameters.AddWithValue("@limiteCredito", user.LIMITE_CREDITO);
                    command.Parameters.AddWithValue("@precioEmpresaId", user.PRECIO_EMPRESA_ID);
                    command.Parameters.AddWithValue("@modDescuento", user.MOD_DESCUENTO);
                    command.Parameters.AddWithValue("@altaCliente", user.ALTACLIENTE);
                    command.Parameters.AddWithValue("@sigFolio", user.SIG_FOLIO);
                    command.Parameters.AddWithValue("@banSesion", "0");
                    command.Parameters.AddWithValue("@tipoUsuario", user.TIPOUSUARIO);
                    command.Parameters.AddWithValue("@cajaId", user.CAJAID);
                    command.Parameters.AddWithValue("@cajaCodigo", user.CAJACODIGO);
                    command.Parameters.AddWithValue("@cajaNombre", user.CAJANOMBRE);
                    command.Parameters.AddWithValue("@permisoLimitarCredito", user.permisoLimitarCredito);
                    command.Parameters.AddWithValue("@connected", user.connected);
                    command.Parameters.AddWithValue("@lastLogin", user.lastLogin);
                    command.Parameters.AddWithValue("@facturarCredito", user.permisoFacturarCredito);
                    command.Parameters.AddWithValue("@lastSeen", user.lastSeen);
                    command.Parameters.AddWithValue("@traspasoAlmacen", user.traspasosAlmacen);
                    command.Parameters.AddWithValue("@salidaAlmacen", user.salidasAlmacen);
                    command.Parameters.AddWithValue("@selectPrice", user.permisoSelectPrice);
                    command.Parameters.AddWithValue("@prepedido", user.permisoPrePedido);
                    command.Parameters.AddWithValue("@todosLosClientes", user.todosLosClientes);
                    command.Parameters.AddWithValue("@enterpriseId", user.enterpriseId);
                    command.Parameters.AddWithValue("@venderSinExistencia", user.venderSinExistencia);
                    command.Parameters.AddWithValue("@venderACredito", user.venderACredito);
                    command.Parameters.AddWithValue("@descMaximo", user.descMaximo);
                    int lastId = command.ExecuteNonQuery();
                    if (lastId > 0)
                        updated = true;
                }
            }
            catch (SQLiteException e)
            {
                SECUDOC.writeLog(e.ToString());
            }
            finally
            {
                if (conn != null && conn.State == ConnectionState.Open)
                    conn.Close();
            }
            return updated;
        }

        public static bool updateAgent(ClsAgentesModel user)
        {
            bool updated = false;
            var conn = new SQLiteConnection();
            try
            {
                conn.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                conn.Open();
                String query = "UPDATE " + LocalDatabase.TABLA_USUARIO +
                           " SET " + LocalDatabase.CAMPO_ClAVE_USUARIO + " = @codigo, " +
                           LocalDatabase.CAMPO_PASSWORD_USUARIO + " = @password, " + LocalDatabase.CAMPO_NOMBRE_USER + " = @name, " +
                           LocalDatabase.CAMPO_RUTA_USER + " = @route, " + LocalDatabase.CAMPO_ALMACENID_USER + " = @almacenId, " +
                           LocalDatabase.CAMPO_VALIDA_INVENTARIO + " = @validarInventarioVentas, " + LocalDatabase.CAMPO_USAQR + " = @usarQR, " +
                           LocalDatabase.CAMPO_PERMISO_MODVENTAS + " = @modVentas," + LocalDatabase.CAMPO_PERMISO_MODCOBRANZA + " = @modCobranza, " +
                           LocalDatabase.CAMPO_DATOS + " = @usoDatos, " + LocalDatabase.CAMPO_PERMISO_GPS_USUARIO + " = @gps, " +
                           LocalDatabase.CAMPO_ABRIRCAJA + " = @abrirCaja, " + LocalDatabase.CAMPO_PERMISO_COBRANZA_USUARIO + " = @pCobranza, " +
                           LocalDatabase.CAMPO_PVENTA + " = @pVenta, " + LocalDatabase.CAMPO_PPEDIDO + " = @pPedido, " +
                           LocalDatabase.CAMPO_PERMISO_COTIZACION_USUARIO + " = @pCotizacion," + LocalDatabase.CAMPO_PINFORMACION + " = @pInformacion, " +
                           LocalDatabase.CAMPO_PVISITA + " = @pVisita, " + LocalDatabase.CAMPO_PHISTORIAL + " = @pHistorial, " +
                           LocalDatabase.CAMPO_PDEVOLUCIONES + " = @pDevolucion, " + LocalDatabase.CAMPO_PERMISO_MODPEDIDOS_USUARIO + " = @modPedidos, " +
                           LocalDatabase.CAMPO_MODPRECIO_USER + " = @modPrecio, " + LocalDatabase.CAMPO_PERMISO_FACTURAR_USUARIO + " = @factura, " +
                           LocalDatabase.CAMPO_VALIDACAJA + " = @validaCaja," + LocalDatabase.CAMPO_LIMITE_CREDITO + " = @limiteCredito, " +
                           LocalDatabase.CAMPO_PRECIO_EMPRESA_ID_USUARIO + " = @precioEmpresaId, " +
                           LocalDatabase.CAMPO_MOD_DESCUENTO_USER + " = @modDescuento, " + LocalDatabase.CAMPO_PERMISO_ALTACLIENTE + " = @altaCliente, " +
                           LocalDatabase.CAMPO_SIG_FOLIO + " = @sigFolio, " + LocalDatabase.CAMPO_BANSESION + " = @banSesion, " +
                           LocalDatabase.CAMPO_TIPOUSUARIO_USUARIO + " = @tipoUsuario," + LocalDatabase.CAMPO_CAJAID_USUARIO + " = @cajaId, " +
                           LocalDatabase.CAMPO_CAJACODIGO_USUARIO + " = @cajaCodigo, " + LocalDatabase.CAMPO_CAJANOMBRE_USUARIO + " = @cajaNombre, " +
                           LocalDatabase.CAMPO_PERMISO_LIMITARCREDITO_USUARIO + " = @permisoLimitarCredito, " +
                           LocalDatabase.CAMPO_CONNECTED_USUARIO + " = @connected, " + LocalDatabase.CAMPO_LASTLOGIN_USUARIO + " = @lastLogin, " +
                           LocalDatabase.CAMPO_PERMISO_FACTURAR_USUARIO + " = @facturarCredito, " + LocalDatabase.CAMPO_LASTSEEN_USUARIO + " = @lastSeen, " +
                           LocalDatabase.CAMPO_PERMTRASPASOALMACEN_USUARIO + " = @traspasoAlmacen, " +
                           LocalDatabase.CAMPO_PERMSALIDAALMACEN_USUARIO + " = @salidaAlmacen, " +
                           LocalDatabase.CAMPO_PERMSELECTPRICE_USUARIO + " = @selectPrice, " +
                           LocalDatabase.CAMPO_PERMPREPEDIDO_USUARIO + " = @prepedido, " +
                           LocalDatabase.CAMPO_TODOSLOSCLIENTES_USUARIO + " = @todosLosClientes, "+
                           LocalDatabase.CAMPO_ENTERPRISEID_USUARIO+ " = @enterpriseId, " +
                           LocalDatabase.CAMPO_VENDERSINEXISTENCIAS_USUARIO+" = @venderSinExistencia, " +
                           LocalDatabase.CAMPO_VENDERACREDITO_USUARIO+" = @venderACredito, " +
                           LocalDatabase.CAMPO_DESCMAXIMO_USUARIO+" = @descMaximo WHERE " + LocalDatabase.CAMPO_ID_USUARIO + " = @id";
                using (SQLiteCommand command = new SQLiteCommand(query, conn))
                {
                    command.Parameters.AddWithValue("@id", user.USUARIO_ID);
                    command.Parameters.AddWithValue("@codigo", user.CLAVE);
                    command.Parameters.AddWithValue("@password", user.PASS);
                    command.Parameters.AddWithValue("@name", user.NOMBRE);
                    command.Parameters.AddWithValue("@route", user.RUTA);
                    command.Parameters.AddWithValue("@almacenId", user.ALMACEN_ID);
                    command.Parameters.AddWithValue("@validarInventarioVentas", user.VALIDA_INVENTARIO_VENTAS);
                    command.Parameters.AddWithValue("@usarQR", user.USAQR);
                    command.Parameters.AddWithValue("@modVentas", user.MODVENTAS);
                    command.Parameters.AddWithValue("@modCobranza", user.MODCOBRANZA);
                    command.Parameters.AddWithValue("@usoDatos", user.DATOS);
                    command.Parameters.AddWithValue("@gps", user.GPS);
                    command.Parameters.AddWithValue("@abrirCaja", user.ABRIRCAJA);
                    command.Parameters.AddWithValue("@pCobranza", user.PCOBRANZA);
                    command.Parameters.AddWithValue("@pVenta", user.PVENTA);
                    command.Parameters.AddWithValue("@pPedido", user.PPEDIDO);
                    command.Parameters.AddWithValue("@pCotizacion", user.PCOTIZACION);
                    command.Parameters.AddWithValue("@pInformacion", user.PINFORMACION);
                    command.Parameters.AddWithValue("@pVisita", user.PVISITA);
                    command.Parameters.AddWithValue("@pHistorial", user.PHISTORIAL);
                    command.Parameters.AddWithValue("@pDevolucion", user.PDEVOLUCIONES);
                    command.Parameters.AddWithValue("@modPedidos", user.MODPEDIDOS);
                    command.Parameters.AddWithValue("@modPrecio", user.MODPRECIO);
                    command.Parameters.AddWithValue("@factura", user.FACTURA);
                    command.Parameters.AddWithValue("@validaCaja", user.VALIDACAJA);
                    command.Parameters.AddWithValue("@limiteCredito", user.LIMITE_CREDITO);
                    command.Parameters.AddWithValue("@precioEmpresaId", user.PRECIO_EMPRESA_ID);
                    command.Parameters.AddWithValue("@modDescuento", user.MOD_DESCUENTO);
                    command.Parameters.AddWithValue("@altaCliente", user.ALTACLIENTE);
                    command.Parameters.AddWithValue("@sigFolio", user.SIG_FOLIO);
                    command.Parameters.AddWithValue("@banSesion", "0");
                    command.Parameters.AddWithValue("@tipoUsuario", user.TIPOUSUARIO);
                    command.Parameters.AddWithValue("@cajaId", user.CAJAID);
                    command.Parameters.AddWithValue("@cajaCodigo", user.CAJACODIGO);
                    command.Parameters.AddWithValue("@cajaNombre", user.CAJANOMBRE);
                    command.Parameters.AddWithValue("@permisoLimitarCredito", user.permisoLimitarCredito);
                    command.Parameters.AddWithValue("@connected", user.connected);
                    command.Parameters.AddWithValue("@lastLogin", user.lastLogin);
                    command.Parameters.AddWithValue("@facturarCredito", user.permisoFacturarCredito);
                    command.Parameters.AddWithValue("@lastSeen", user.lastSeen);
                    command.Parameters.AddWithValue("@traspasoAlmacen", user.traspasosAlmacen);
                    command.Parameters.AddWithValue("@salidaAlmacen", user.salidasAlmacen);
                    command.Parameters.AddWithValue("@selectPrice", user.permisoSelectPrice);
                    command.Parameters.AddWithValue("@prepedido", user.permisoPrePedido);
                    command.Parameters.AddWithValue("@todosLosClientes", user.todosLosClientes);
                    command.Parameters.AddWithValue("@enterpriseId", user.enterpriseId);
                    command.Parameters.AddWithValue("@venderSinExistencia", user.venderSinExistencia);
                    command.Parameters.AddWithValue("@venderACredito", user.venderACredito);
                    command.Parameters.AddWithValue("@descMaximo", user.descMaximo);
                    int lastId = command.ExecuteNonQuery();
                    if (lastId > 0)
                        updated = true;
                }
            }
            catch (SQLiteException e)
            {
                SECUDOC.writeLog(e.ToString());
            }
            finally
            {
                if (conn != null && conn.State == ConnectionState.Open)
                    conn.Close();
            }
            return updated;
        }

        public static int saveAllUsersLAN(List<ClsAgentesModel> usersList)
        {
            int count = 0;
            var conn = new SQLiteConnection();
            try
            {
                conn.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                conn.Open();
                if (usersList != null)
                {
                    int items = usersList.Count;
                    foreach (var user in usersList)
                    {
                        String query = "INSERT INTO " + LocalDatabase.TABLA_USUARIO +
                            " VALUES (@id, @codigo, @password, @name, @route, @almacenId, @validarInventarioVentas, @usarQR, @modVentas," +
                                       " @modCobranza, @usoDatos, @gps, @abrirCaja, @pCobranza, @pVenta, @pPedido, @pCotizacion," +
                                       " @pInformacion, @pVisita, @pHistorial, @pDevolucion, @modPedidos, @modPrecio, @factura, @validaCaja," +
                                       " @limiteCredito, @precioEmpresaId, @modDescuento, @altaCliente, @sigFolio, @banSesion, @tipoUsuario," +
                                       " @cajaId, @cajaCodigo, @cajaNombre, @permisoLimitarCredito, @connected, @lastLogin, " +
                                       "@facturarCredito, @lastSeen, @traspasoAlmacen, @salidaAlmacen, @selectPrice, @prepedido, @todosLosClientes," +
                                       "@enterpriseId, @venderSinExistencia, @venderACredito, @descMaximo)";
                        using (SQLiteCommand command = new SQLiteCommand(query, conn))
                        {
                            command.Parameters.AddWithValue("@id", user.USUARIO_ID);
                            command.Parameters.AddWithValue("@codigo", user.CLAVE);
                            command.Parameters.AddWithValue("@password", user.PASS);
                            command.Parameters.AddWithValue("@name", user.NOMBRE);
                            command.Parameters.AddWithValue("@route", user.RUTA);
                            command.Parameters.AddWithValue("@almacenId", user.ALMACEN_ID);
                            command.Parameters.AddWithValue("@validarInventarioVentas", user.VALIDA_INVENTARIO_VENTAS);
                            command.Parameters.AddWithValue("@usarQR", user.USAQR);
                            command.Parameters.AddWithValue("@modVentas", user.MODVENTAS);
                            command.Parameters.AddWithValue("@modCobranza", user.MODCOBRANZA);
                            command.Parameters.AddWithValue("@usoDatos", user.DATOS);
                            command.Parameters.AddWithValue("@gps", user.GPS);
                            command.Parameters.AddWithValue("@abrirCaja", user.ABRIRCAJA);
                            command.Parameters.AddWithValue("@pCobranza", user.PCOBRANZA);
                            command.Parameters.AddWithValue("@pVenta", user.PVENTA);
                            command.Parameters.AddWithValue("@pPedido", user.PPEDIDO);
                            command.Parameters.AddWithValue("@pCotizacion", user.PCOTIZACION);
                            command.Parameters.AddWithValue("@pInformacion", user.PINFORMACION);
                            command.Parameters.AddWithValue("@pVisita", user.PVISITA);
                            command.Parameters.AddWithValue("@pHistorial", user.PHISTORIAL);
                            command.Parameters.AddWithValue("@pDevolucion", user.PDEVOLUCIONES);
                            command.Parameters.AddWithValue("@modPedidos", user.MODPEDIDOS);
                            command.Parameters.AddWithValue("@modPrecio", user.MODPRECIO);
                            command.Parameters.AddWithValue("@factura", user.FACTURA);
                            command.Parameters.AddWithValue("@validaCaja", user.VALIDACAJA);
                            command.Parameters.AddWithValue("@limiteCredito", user.LIMITE_CREDITO);
                            command.Parameters.AddWithValue("@precioEmpresaId", user.PRECIO_EMPRESA_ID);
                            command.Parameters.AddWithValue("@modDescuento", user.MOD_DESCUENTO);
                            command.Parameters.AddWithValue("@altaCliente", user.ALTACLIENTE);
                            command.Parameters.AddWithValue("@sigFolio", user.SIG_FOLIO);
                            command.Parameters.AddWithValue("@banSesion", "0");
                            command.Parameters.AddWithValue("@tipoUsuario", user.TIPOUSUARIO);
                            command.Parameters.AddWithValue("@cajaId", user.CAJAID);
                            command.Parameters.AddWithValue("@cajaCodigo", user.CAJACODIGO);
                            command.Parameters.AddWithValue("@cajaNombre", user.CAJANOMBRE);
                            command.Parameters.AddWithValue("@permisoLimitarCredito", user.permisoLimitarCredito);
                            command.Parameters.AddWithValue("@connected", user.connected);
                            command.Parameters.AddWithValue("@lastLogin", user.lastLogin);
                            command.Parameters.AddWithValue("@facturarCredito", user.permisoFacturarCredito);
                            command.Parameters.AddWithValue("@lastSeen", user.lastSeen);
                            command.Parameters.AddWithValue("@traspasoAlmacen", user.traspasosAlmacen);
                            command.Parameters.AddWithValue("@salidaAlmacen", user.salidasAlmacen);
                            command.Parameters.AddWithValue("@selectPrice", user.permisoSelectPrice);
                            command.Parameters.AddWithValue("@prepedido", user.permisoPrePedido);
                            command.Parameters.AddWithValue("@todosLosClientes", user.todosLosClientes);
                            command.Parameters.AddWithValue("@enterpriseId", user.enterpriseId);
                            command.Parameters.AddWithValue("@venderSinExistencia", user.venderSinExistencia);
                            command.Parameters.AddWithValue("@venderACredito", user.venderACredito);
                            command.Parameters.AddWithValue("@descMaximo", user.descMaximo);
                            int lastId = command.ExecuteNonQuery();
                            if (lastId != 0)
                                count++;
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
                if (conn != null && conn.State == ConnectionState.Open)
                    conn.Close();
            }
            return count;
        }

        public static bool createAgent(ClsAgentesModel user)
        {
            bool created = false;
            var conn = new SQLiteConnection();
            try
            {
                conn.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                conn.Open();
                String query = "INSERT INTO " + LocalDatabase.TABLA_USUARIO +
                            " VALUES (@id, @codigo, @password, @name, @route, @almacenId, @validarInventarioVentas, @usarQR, @modVentas," +
                                       " @modCobranza, @usoDatos, @gps, @abrirCaja, @pCobranza, @pVenta, @pPedido, @pCotizacion," +
                                       " @pInformacion, @pVisita, @pHistorial, @pDevolucion, @modPedidos, @modPrecio, @factura, @validaCaja," +
                                       " @limiteCredito, @precioEmpresaId, @modDescuento, @altaCliente, @sigFolio, @banSesion, @tipoUsuario," +
                                       " @cajaId, @cajaCodigo, @cajaNombre, @permisoLimitarCredito, @connected, @lastLogin, " +
                                       "@facturarCredito, @lastSeen, @traspasoAlmacen, @salidaAlmacen, @selectPrice, @prepedido, @todosLosClientes," +
                                       "@enterpriseId, @venderSinExistencia, @venderACredito, @descMaximo)";
                using (SQLiteCommand command = new SQLiteCommand(query, conn))
                {
                    command.Parameters.AddWithValue("@id", user.USUARIO_ID);
                    command.Parameters.AddWithValue("@codigo", user.CLAVE);
                    command.Parameters.AddWithValue("@password", user.PASS);
                    command.Parameters.AddWithValue("@name", user.NOMBRE);
                    command.Parameters.AddWithValue("@route", user.RUTA);
                    command.Parameters.AddWithValue("@almacenId", user.ALMACEN_ID);
                    command.Parameters.AddWithValue("@validarInventarioVentas", user.VALIDA_INVENTARIO_VENTAS);
                    command.Parameters.AddWithValue("@usarQR", user.USAQR);
                    command.Parameters.AddWithValue("@modVentas", user.MODVENTAS);
                    command.Parameters.AddWithValue("@modCobranza", user.MODCOBRANZA);
                    command.Parameters.AddWithValue("@usoDatos", user.DATOS);
                    command.Parameters.AddWithValue("@gps", user.GPS);
                    command.Parameters.AddWithValue("@abrirCaja", user.ABRIRCAJA);
                    command.Parameters.AddWithValue("@pCobranza", user.PCOBRANZA);
                    command.Parameters.AddWithValue("@pVenta", user.PVENTA);
                    command.Parameters.AddWithValue("@pPedido", user.PPEDIDO);
                    command.Parameters.AddWithValue("@pCotizacion", user.PCOTIZACION);
                    command.Parameters.AddWithValue("@pInformacion", user.PINFORMACION);
                    command.Parameters.AddWithValue("@pVisita", user.PVISITA);
                    command.Parameters.AddWithValue("@pHistorial", user.PHISTORIAL);
                    command.Parameters.AddWithValue("@pDevolucion", user.PDEVOLUCIONES);
                    command.Parameters.AddWithValue("@modPedidos", user.MODPEDIDOS);
                    command.Parameters.AddWithValue("@modPrecio", user.MODPRECIO);
                    command.Parameters.AddWithValue("@factura", user.FACTURA);
                    command.Parameters.AddWithValue("@validaCaja", user.VALIDACAJA);
                    command.Parameters.AddWithValue("@limiteCredito", user.LIMITE_CREDITO);
                    command.Parameters.AddWithValue("@precioEmpresaId", user.PRECIO_EMPRESA_ID);
                    command.Parameters.AddWithValue("@modDescuento", user.MOD_DESCUENTO);
                    command.Parameters.AddWithValue("@altaCliente", user.ALTACLIENTE);
                    command.Parameters.AddWithValue("@sigFolio", user.SIG_FOLIO);
                    command.Parameters.AddWithValue("@banSesion", "0");
                    command.Parameters.AddWithValue("@tipoUsuario", user.TIPOUSUARIO);
                    command.Parameters.AddWithValue("@cajaId", user.CAJAID);
                    command.Parameters.AddWithValue("@cajaCodigo", user.CAJACODIGO);
                    command.Parameters.AddWithValue("@cajaNombre", user.CAJANOMBRE);
                    command.Parameters.AddWithValue("@permisoLimitarCredito", user.permisoLimitarCredito);
                    command.Parameters.AddWithValue("@connected", user.connected);
                    command.Parameters.AddWithValue("@lastLogin", user.lastLogin);
                    command.Parameters.AddWithValue("@facturarCredito", user.permisoFacturarCredito);
                    command.Parameters.AddWithValue("@lastSeen", user.lastSeen);
                    command.Parameters.AddWithValue("@traspasoAlmacen", user.traspasosAlmacen);
                    command.Parameters.AddWithValue("@salidaAlmacen", user.salidasAlmacen);
                    command.Parameters.AddWithValue("@selectPrice", user.permisoSelectPrice);
                    command.Parameters.AddWithValue("@prepedido", user.permisoPrePedido);
                    command.Parameters.AddWithValue("@todosLosClientes", user.todosLosClientes);
                    command.Parameters.AddWithValue("@enterpriseId", user.enterpriseId);
                    command.Parameters.AddWithValue("@venderSinExistencia", user.venderSinExistencia);
                    command.Parameters.AddWithValue("@venderACredito", user.venderACredito);
                    command.Parameters.AddWithValue("@descMaximo", user.descMaximo);
                    int records = command.ExecuteNonQuery();
                    if (records != 0)
                        created = true;
                }
            }
            catch (SQLiteException e)
            {
                SECUDOC.writeLog(e.ToString());
            }
            finally
            {
                if (conn != null && conn.State == ConnectionState.Open)
                    conn.Close();
            }
            return created;
        }

        public static ExpandoObject getAgentByCode(String agentCode)
        {
            dynamic response = new ExpandoObject();
            int value = 0;
            String description = "";
            UserModel um = null;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "SELECT * FROM Users WHERE CLAVE = @agentCode";
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    command.Parameters.AddWithValue("@agentCode", agentCode);
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                um = new UserModel();
                                um.id = Convert.ToInt32(reader["USUARIO_ID"].ToString().Trim());
                                um.Nombre = reader["NOMBRE"].ToString().Trim();
                                um.Clave = reader["CLAVE"].ToString().Trim();
                                um.enterpriseId = Convert.ToInt32(reader["enterpriseId"].ToString().Trim());
                                value = 1;
                            }
                        }
                        else
                        {
                            description = "Agente con código "+agentCode+" No fue encontrado en la base de datos!";
                        }
                        if (reader != null && !reader.IsClosed)
                            reader.Close();
                    }
                }
            }
            catch (SQLiteException e)
            {
                SECUDOC.writeLog(e.ToString());
                value = -1;
                description = e.Message;
            }
            finally
            {
                if (db != null && db.State == ConnectionState.Open)
                    db.Close();
                response.value = value;
                response.description = description;
                response.um = um;
            }
            return response;
        }

        public static ExpandoObject getAgentById(int idAgente)
        {
            dynamic response = new ExpandoObject();
            int value = 0;
            String description = "";
            UserModel um = null;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "SELECT * FROM Users WHERE USUARIO_ID = @idAgente";
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    command.Parameters.AddWithValue("@idAgente", idAgente);
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                um = new UserModel();
                                um.id = idAgente;
                                um.Nombre = reader["NOMBRE"].ToString().Trim();
                                um.Clave = reader["CLAVE"].ToString().Trim();
                                um.almacen_id = Convert.ToInt32(reader["ALMACEN_ID"].ToString().Trim());
                                um.enterpriseId = Convert.ToInt32(reader["enterpriseId"].ToString().Trim());
                                value = 1;
                            }
                        }
                        else
                        {
                            description = "Agente con id " + idAgente + " No fue encontrado en la base de datos!";
                        }
                        if (reader != null && !reader.IsClosed)
                            reader.Close();
                    }
                }
            }
            catch (SQLiteException e)
            {
                SECUDOC.writeLog(e.ToString());
                value = -1;
                description = e.Message;
            }
            finally
            {
                if (db != null && db.State == ConnectionState.Open)
                    db.Close();
                response.value = value;
                response.description = description;
                response.um = um;
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
                                value = Convert.ToInt32(reader.GetValue(0).ToString().Trim());
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

        public static String getNameUser(int idUser)
        {
            String value = "";
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "SELECT "+LocalDatabase.CAMPO_NOMBRE_USER+" FROM "+LocalDatabase.TABLA_USUARIO+" WHERE "+
                    LocalDatabase.CAMPO_ID_USUARIO+" = @idUser";
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    command.Parameters.AddWithValue("@idUser", idUser);
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                                if (reader.GetValue(0) != DBNull.Value)
                                    value = reader.GetValue(0).ToString().Trim();
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
                if (db != null && db.State == ConnectionState.Open)
                    db.Close();
            }
            return value;
        }

        public static bool userExist(int idUserCom)
        {
            bool exist = false;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "SELECT * FROM " + LocalDatabase.TABLA_USUARIO + " WHERE " + LocalDatabase.CAMPO_ID_USUARIO + " = @idUserCom";
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    command.Parameters.AddWithValue("@idUserCom", idUserCom);
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
            catch (SQLiteException e)
            {
                SECUDOC.writeLog("Exception: " + e.ToString());
            }
            finally
            {
                if (db != null && db.State == ConnectionState.Open)
                    db.Close();
            }
            return exist;
        }

        public static int getAlmacenIdFromTheUser(int idUser)
        {
            int value = 0;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "SELECT " + LocalDatabase.CAMPO_ALMACENID_USER + " FROM "+LocalDatabase.TABLA_USUARIO+" WHERE "+LocalDatabase.CAMPO_ID_USUARIO+" = @idUser";
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    command.Parameters.AddWithValue("@idUser", idUser);
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                                if (reader.GetValue(0) != DBNull.Value)
                                    value = Convert.ToInt32(reader.GetValue(0).ToString().Trim());    
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
                if (db != null && db.State == ConnectionState.Open)
                    db.Close();
            }
            return value;
        }

        public static int getIntValue(SQLiteConnection db, String query)
        {
            int value = 0;
            try
            {
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                                value = Convert.ToInt32(reader.GetValue(0));
                        }
                        if (reader != null && !reader.IsClosed)
                            reader.Close();
                    }
                }
            }
            catch (SQLiteException e)
            {
                SECUDOC.writeLog("getInValue: " + e.Message + " 50");
            }
            finally
            {

            }
            return value;
        }

        public static String getTheNameOfTheUserWhoGeneratedThePreorder(int idDocumento)
        {
            String name = "";
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "SELECT U.NOMBRE FROM Users U " +
                    "INNER JOIN PedidoEncabezado PE ON U.usuario_id = PE.CNOMBREAGENTECC " +
                    "LEFT JOIN Documentos D ON D.CIDDOCTOPEDIDOCC = PE.CIDDOCTOPEDIDOCC " +
                    "WHERE D.id = "+ idDocumento;
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
            } catch (SQLiteException e)
            {
                SECUDOC.writeLog(e.ToString());
            } finally
            {
                if (db != null && db.State == ConnectionState.Open)
                    db.Close();
            }
            return name;
        }

        public static void loginUserInLocalDb(String clave, String pass, bool rememberLogin)
        {
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                string query = "SELECT * FROM " + LocalDatabase.TABLA_USUARIO + " WHERE " +
                            LocalDatabase.CAMPO_ClAVE_USUARIO + " = UPPER(@clave) AND " +
                            LocalDatabase.CAMPO_PASSWORD_USUARIO + " = @pass AND " + LocalDatabase.CAMPO_ALMACENID_USER + " > 0";
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    command.Parameters.AddWithValue("@clave", clave);
                    command.Parameters.AddWithValue("@pass", pass);
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                ClsRegeditController.saveIdUserInTurn(Convert.ToInt32(reader[LocalDatabase.CAMPO_ID_USUARIO].ToString()));
                                ClsRegeditController.saveLoginStatus(true);
                                ClsRegeditController.saveRememberLogin(rememberLogin);
                                ClsRegeditController.saveCurrentIdEnterprise(Convert.ToInt32(reader[LocalDatabase.CAMPO_ENTERPRISEID_USUARIO].ToString()));
                            }
                        } else {
                            ClsRegeditController.saveIdUserInTurn(Convert.ToInt32("0"));
                            ClsRegeditController.saveLoginStatus(false);
                            ClsRegeditController.saveRememberLogin(false);
                            ClsRegeditController.saveCurrentIdEnterprise(0);
                        }
                        if (reader != null && !reader.IsClosed)
                            reader.Close();
                    }
                }
            }
            catch (SQLiteException ex)
            {
                SECUDOC.writeLog(ex.ToString());
            }
            finally
            {
                if (db != null && db.State == ConnectionState.Open)
                    db.Close();
            }
        }

        public static String getCodeBox(int userId)
        {
            String codeBox = "";
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                string query = "SELECT " + LocalDatabase.CAMPO_CAJACODIGO_USUARIO + " FROM " + LocalDatabase.TABLA_USUARIO + " WHERE " +
                            LocalDatabase.CAMPO_ID_USUARIO + " = @userId";
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    command.Parameters.AddWithValue("@userId", userId);
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                codeBox = reader.GetString(0);
                            }
                        }
                        if (reader != null && !reader.IsClosed)
                            reader.Close();
                    }
                }
            }
            catch (SQLiteException ex)
            {
                SECUDOC.writeLog(ex.ToString());
            }
            finally
            {
                if (db != null && db.State == ConnectionState.Open)
                    db.Close();
            }
            return codeBox;
        }

        public static double getDescuentoMaximo(int userId)
        {
            double descMaximo = 0;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                string query = "SELECT " + LocalDatabase.CAMPO_DESCMAXIMO_USUARIO + " FROM " + LocalDatabase.TABLA_USUARIO + " WHERE " +
                            LocalDatabase.CAMPO_ID_USUARIO + " = @userId";
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    command.Parameters.AddWithValue("@userId", userId);
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                                if (reader.GetValue(0) != DBNull.Value)
                                    descMaximo = Convert.ToDouble(reader.GetValue(0).ToString().Trim());
                        }
                        if (reader != null && !reader.IsClosed)
                            reader.Close();
                    }
                }
            }
            catch (SQLiteException ex)
            {
                SECUDOC.writeLog(ex.ToString());
            }
            finally
            {
                if (db != null && db.State == ConnectionState.Open)
                    db.Close();
            }
            return descMaximo;
        }

        public static double getDescuentoMaximoByCheckoutCode(String codigoCaja)
        {
            double descMaximo = 0;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                string query = "SELECT " + LocalDatabase.CAMPO_DESCMAXIMO_USUARIO + " FROM " + LocalDatabase.TABLA_USUARIO + " WHERE " +
                            LocalDatabase.CAMPO_CAJACODIGO_USUARIO + " = @codigoCaja";
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    command.Parameters.AddWithValue("@codigoCaja", codigoCaja);
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                                if (reader.GetValue(0) != DBNull.Value)
                                    descMaximo = Convert.ToDouble(reader.GetValue(0).ToString().Trim());
                        }
                        if (reader != null && !reader.IsClosed)
                            reader.Close();
                    }
                }
            }
            catch (SQLiteException ex)
            {
                SECUDOC.writeLog(ex.ToString());
            }
            finally
            {
                if (db != null && db.State == ConnectionState.Open)
                    db.Close();
            }
            return descMaximo;
        }
        
        public static String getCode(int userId)
        {
            String code = "";
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                string query = "SELECT " + LocalDatabase.CAMPO_ClAVE_USUARIO + " FROM " + LocalDatabase.TABLA_USUARIO + " WHERE " +
                            LocalDatabase.CAMPO_ID_USUARIO + " = @userId";
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    command.Parameters.AddWithValue("@userId", userId);
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                if (reader.GetValue(0) != DBNull.Value)
                                    code = reader.GetValue(0).ToString().Trim();
                            }
                        }
                        if (reader != null && !reader.IsClosed)
                            reader.Close();
                    }
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
            return code;
        }

        public static String getTheLastFolioCob()
        {
            String sig_folio = "";
            var db = new SQLiteConnection(ClsSQLiteDbHelper.instanceSQLite);
            db.Open();
            try
            {

                String query = "SELECT " + LocalDatabase.CAMPO_SIG_FOLIO + " FROM " + LocalDatabase.TABLA_USUARIO +
                        " WHERE " + LocalDatabase.CAMPO_ClAVE_USUARIO + " = " + ClsRegeditController.getIdUserInTurn();
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                sig_folio = reader.GetString(0);
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
            return sig_folio;
        }

        public static String getAStringValueForAnyUser(String query)
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
                                    value = reader.GetValue(0).ToString();
                            }
                        }
                        if (reader != null && !reader.IsClosed)
                            reader.Close();
                    }
                }
            }
            catch (SQLiteException ex)
            {
                SECUDOC.writeLog(ex.ToString());
            }
            finally
            {
                if (db != null && db.State == ConnectionState.Open)
                    db.Close();
            }
            return value;
        }

        public static String getCurrentCheckoutName()
        {
            String value = "";
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "SELECT " + LocalDatabase.CAMPO_CAJANOMBRE_USUARIO + " FROM " + LocalDatabase.TABLA_USUARIO + " WHERE " +
                    LocalDatabase.CAMPO_ID_USUARIO + " = " + ClsRegeditController.getIdUserInTurn();
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                if (reader.GetValue(0) != DBNull.Value)
                                    value = reader.GetValue(0).ToString();
                            }
                        }
                        if (reader != null && !reader.IsClosed)
                            reader.Close();
                    }
                }
            }
            catch (SQLiteException ex)
            {
                SECUDOC.writeLog(ex.ToString());
            }
            finally
            {
                if (db != null && db.State == ConnectionState.Open)
                    db.Close();
            }
            return value;
        }

        public static int doYouHaveACollectionPermit()
        {
            int resp = 0;
            var db = new SQLiteConnection(ClsSQLiteDbHelper.instanceSQLite);
            db.Open();
            try
            {
                String query = "SELECT " + LocalDatabase.CAMPO_PERMISO_COBRANZA_USUARIO + " FROM " + LocalDatabase.TABLA_USUARIO +
                        " WHERE " + LocalDatabase.CAMPO_ID_USUARIO + " = " + ClsRegeditController.getIdUserInTurn();
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                if (reader.GetString(0).Equals("S"))
                                {
                                    resp = 1;
                                }
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
            return resp;
        }

        public static bool doYouHaveAAddNewCustomersPermission()
        {
            bool permission = false;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "SELECT " + LocalDatabase.CAMPO_PERMISO_ALTACLIENTE + " FROM " + LocalDatabase.TABLA_USUARIO +
                        " WHERE " + LocalDatabase.CAMPO_ID_USUARIO + " = " + ClsRegeditController.getIdUserInTurn();
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                if (reader.GetValue(0) != DBNull.Value)
                                {
                                    String letra = reader.GetString(0).Trim();
                                    if (letra.Equals("S"))
                                        permission = true;
                                }
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
            return permission;
        }

        public static ExpandoObject isItSupervisorPassword(String password)
        {
            dynamic response = new ExpandoObject();
            int value = 0;
            String description = "";
            bool existe = false;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "SELECT COUNT(*) FROM " + LocalDatabase.TABLA_USUARIO + " WHERE " +
                            LocalDatabase.CAMPO_PASSWORD_USUARIO + " = @password AND " + 
                            LocalDatabase.CAMPO_TIPOUSUARIO_USUARIO +" = @userType";
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    command.Parameters.AddWithValue("@password", password);
                    command.Parameters.AddWithValue("@userType", 3);
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                if (reader.GetValue(0) != DBNull.Value)
                                    if (Convert.ToInt32(reader.GetValue(0).ToString().Trim()) > 0)
                                    {
                                        existe = true;
                                        value = 1;
                                    }
                                    else description = "No se encontró ningún Agente Supervisor con esta contraseña\r\n" +
                                            "Contraseña Incorrecta!";
                            }
                        }
                        else description = "No se encontró ningún Agente Supervisor con esta contraseña\r\n" +
                                            "Contraseña Incorrecta!";
                        if (reader != null && !reader.IsClosed)
                            reader.Close();
                    }
                }
            }
            catch (SQLiteException ex)
            {
                SECUDOC.writeLog(ex.ToString());
                value = -1;
                description = ex.Message;
            }
            finally
            {
                if (db != null && db.State == ConnectionState.Open)
                    db.Close();
                response.value = value;
                response.description = description;
                response.existe = existe;
            }
            return response;
        }

        public static Boolean deleteAllUsers()
        {
            bool deleted = true;
            var conn = new SQLiteConnection(ClsSQLiteDbHelper.instanceSQLite);
            conn.Open();
            try
            {
                string query = "DELETE FROM " + LocalDatabase.TABLA_USUARIO;
                using (SQLiteCommand command = new SQLiteCommand(query, conn))
                {
                    command.ExecuteNonQuery();
                    deleted = true;
                }
            }
            catch (Exception ex)
            {
                SECUDOC.writeLog(ex.ToString());
            }
            finally
            {
                if (conn != null && conn.State == ConnectionState.Open)
                    conn.Close();
            }
            return deleted;
        }

        public static UserModel datosUsuarioParaElDocumento()
        {
            UserModel u = null;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                int idUser = ClsRegeditController.getIdUserInTurn();
                String query = "SELECT * FROM " + LocalDatabase.TABLA_USUARIO + " WHERE " + LocalDatabase.CAMPO_ID_USUARIO + "=" +
                    idUser;
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                u = new UserModel();
                                u.id = reader.GetInt32(0);
                                u.Nombre = reader.GetString(3);
                                u.almacen_id = reader.GetInt32(5);
                                u.Factura = reader.GetString(23);
                                u.Precio_Empresa_ID = reader.GetInt32(26);
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
            return u;
        }

        public static Boolean doYouHavePermissionToLimitCredit()
        {
            Boolean hasPermission = false;
            var db = new SQLiteConnection(ClsSQLiteDbHelper.instanceSQLite);
            db.Open();
            try
            {
                String query = "SELECT " + LocalDatabase.CAMPO_PERMISO_LIMITARCREDITO_USUARIO + " FROM " + LocalDatabase.TABLA_USUARIO +
                        " WHERE " + LocalDatabase.CAMPO_ID_USUARIO + " = @idUser";
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    command.Parameters.AddWithValue("@idUser", ClsRegeditController.getIdUserInTurn());
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                if (Convert.ToInt32(reader[LocalDatabase.CAMPO_PERMISO_LIMITARCREDITO_USUARIO].ToString().Trim()) == 1)
                                    hasPermission = true;
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
            return hasPermission;
        }

        public static ExpandoObject doYouHavePermissionToSellCredit()
        {
            dynamic response = new ExpandoObject();
            int value = 0;
            String description = "";
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "SELECT " + LocalDatabase.CAMPO_VENDERACREDITO_USUARIO + " FROM " + LocalDatabase.TABLA_USUARIO +
                        " WHERE " + LocalDatabase.CAMPO_ID_USUARIO + " = @idUser";
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    command.Parameters.AddWithValue("@idUser", ClsRegeditController.getIdUserInTurn());
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                if (reader.GetValue(0) != DBNull.Value)
                                    if (Convert.ToInt32(reader.GetValue(0).ToString().Trim()) == 1)
                                        value = 1;
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
                value = -1;
                description = e.Message;
            }
            finally
            {
                if (db != null && db.State == ConnectionState.Open)
                    db.Close();
                response.value = value;
                response.description = description;
            }
            return response;
        }

        public static Boolean doYouHavePermissionToBill()
        {
            Boolean havePermission = false;
            var db = new SQLiteConnection(ClsSQLiteDbHelper.instanceSQLite);
            db.Open();
            try
            {
                String query = "SELECT " + LocalDatabase.CAMPO_PERMISO_FACTURAR_USUARIO + " FROM " + LocalDatabase.TABLA_USUARIO +
                        " WHERE " + LocalDatabase.CAMPO_ID_USUARIO + " = @idUser";
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    command.Parameters.AddWithValue("@idUser", ClsRegeditController.getIdUserInTurn());
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                if (reader.GetValue(0) != null)
                                {
                                    String value = reader.GetValue(0).ToString().Trim();
                                    int num_var;
                                    bool result = Int32.TryParse(value, out num_var);
                                    if (result)
                                    {
                                        if (num_var == 1)
                                            havePermission = true;
                                    }
                                    else
                                    {
                                        if (value.Equals("S"))
                                            havePermission = true;
                                    }
                                }
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
            return havePermission;
        }

        public static bool doYouHavePermissionToFacturarCredito()
        {
            bool havePermission = false;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "SELECT " + LocalDatabase.CAMPO_PERMFACTURARACREDITO_USUARIO + " FROM " + LocalDatabase.TABLA_USUARIO +
                        " WHERE " + LocalDatabase.CAMPO_ID_USUARIO + " = @idUser";
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    command.Parameters.AddWithValue("@idUser", ClsRegeditController.getIdUserInTurn());
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                if (reader.GetValue(0) != DBNull.Value)
                                {
                                    if (Convert.ToInt32(reader.GetValue(0).ToString().Trim()) == 1)
                                        havePermission = true;
                                }
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
            return havePermission;
        }

        public static Boolean doYouHavePermissionToSell()
        {
            Boolean havePermission = false;
            var db = new SQLiteConnection(ClsSQLiteDbHelper.instanceSQLite);
            db.Open();
            try
            {
                String query = "SELECT " + LocalDatabase.CAMPO_PVENTA + " FROM " + LocalDatabase.TABLA_USUARIO +
                        " WHERE " + LocalDatabase.CAMPO_ID_USUARIO + " = @idUser";
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    command.Parameters.AddWithValue("@idUser", ClsRegeditController.getIdUserInTurn());
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                if (reader.GetString(0).Equals("S"))
                                {
                                    havePermission = true;
                                }
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
            return havePermission;
        }

        public static Boolean doYouHavePermissionToMakeQuotes()
        {
            Boolean havePermission = false;
            var db = new SQLiteConnection(ClsSQLiteDbHelper.instanceSQLite);
            db.Open();
            try
            {
                String query = "SELECT " + LocalDatabase.CAMPO_PERMISO_COTIZACION_USUARIO + " FROM " + LocalDatabase.TABLA_USUARIO +
                        " WHERE " + LocalDatabase.CAMPO_ID_USUARIO + " = @idUser";
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    command.Parameters.AddWithValue("@idUser", ClsRegeditController.getIdUserInTurn());
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                if (reader.GetString(0).Trim().Equals("S"))
                                {
                                    havePermission = true;
                                }
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
            return havePermission;
        }

        public static Boolean doYouHavePermissionToPlaceOrders()
        {
            Boolean havePermission = false;
            var db = new SQLiteConnection(ClsSQLiteDbHelper.instanceSQLite);
            db.Open();
            try
            {
                String query = "SELECT " + LocalDatabase.CAMPO_PPEDIDO + " FROM " + LocalDatabase.TABLA_USUARIO +
                        " WHERE " + LocalDatabase.CAMPO_ID_USUARIO + " = @idUser";
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    command.Parameters.AddWithValue("@idUser", ClsRegeditController.getIdUserInTurn());
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                if (reader.GetString(0).Trim().Equals("S"))
                                {
                                    havePermission = true;
                                }
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
            return havePermission;
        }

        public static Boolean doYouHavePermissionToMakeReturns()
        {
            Boolean havePermission = false;
            var db = new SQLiteConnection(ClsSQLiteDbHelper.instanceSQLite);
            db.Open();
            try
            {
                String query = "SELECT " + LocalDatabase.CAMPO_PDEVOLUCIONES + " FROM " + LocalDatabase.TABLA_USUARIO +
                        " WHERE " + LocalDatabase.CAMPO_ID_USUARIO + " = @idUser";
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    command.Parameters.AddWithValue("@idUser", ClsRegeditController.getIdUserInTurn());
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                if (reader.GetString(0).Trim().Equals("S"))
                                {
                                    havePermission = true;
                                }
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
            return havePermission;
        }

        public static Boolean doYouHavePermissionPrepedido()
        {
            Boolean havePermission = false;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "SELECT " + LocalDatabase.CAMPO_PERMPREPEDIDO_USUARIO + " FROM " + LocalDatabase.TABLA_USUARIO +
                        " WHERE " + LocalDatabase.CAMPO_ID_USUARIO + " = @idUser";
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    command.Parameters.AddWithValue("@idUser", ClsRegeditController.getIdUserInTurn());
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                if (reader.GetValue(0) != DBNull.Value)
                                {
                                    if (Convert.ToInt32(reader.GetValue(0).ToString().Trim()) == 1)
                                        havePermission = true;
                                }
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
            return havePermission;
        }

        public static Boolean doYouHavePermissionToSelectPrices(int idUser)
        {
            Boolean havePermission = false;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "SELECT " + LocalDatabase.CAMPO_PERMSELECTPRICE_USUARIO + " FROM " + LocalDatabase.TABLA_USUARIO + " WHERE " +
                    LocalDatabase.CAMPO_ID_USUARIO + " = @idUser";
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    command.Parameters.AddWithValue("@idUser", idUser);
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                if (reader.GetValue(0) != DBNull.Value)
                                    if (Convert.ToInt32(reader.GetValue(0).ToString().Trim()) == 1)
                                        havePermission = true;
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
            return havePermission;
        }

        public static Boolean doYouHavePermissionToModifyPrices(int idUser)
        {
            Boolean havePermission = false;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "SELECT " + LocalDatabase.CAMPO_MODPRECIO_USER + " FROM " + LocalDatabase.TABLA_USUARIO + " WHERE " +
                    LocalDatabase.CAMPO_ID_USUARIO + " = @idUser";
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    command.Parameters.AddWithValue("@idUser", idUser);
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                if (reader.GetValue(0) != DBNull.Value)
                                {
                                    if (reader.GetValue(0).ToString().Trim().Equals("S"))
                                        havePermission = true;
                                }
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
            return havePermission;
        }

        public static Boolean doYouHavePermissionToDoDiscounts(int idUser)
        {
            Boolean havePermission = false;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "SELECT " + LocalDatabase.CAMPO_MOD_DESCUENTO_USER + " FROM " + LocalDatabase.TABLA_USUARIO + " WHERE " +
                    LocalDatabase.CAMPO_ID_USUARIO + " = @idUser";
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    command.Parameters.AddWithValue("@idUser", idUser);
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                if (reader.GetValue(0) != DBNull.Value)
                                {
                                    if (reader.GetValue(0).ToString().Trim().Equals("S"))
                                        havePermission = true;
                                }
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
            return havePermission;
        }

        public static int permisoParaEnviarDocsAutomaticamente()
        {
            int permiso = 0;
            var db = new SQLiteConnection();            
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "SELECT " + LocalDatabase.CAMPO_DATOS + " FROM " + LocalDatabase.TABLA_USUARIO + " WHERE " +
                    LocalDatabase.CAMPO_ID_USUARIO + "= @idUser";
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    command.Parameters.AddWithValue("@idUser", ClsRegeditController.getIdUserInTurn());
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                if (reader.GetString(0).Equals("S"))
                                {
                                    permiso = 1;
                                }
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
            return permiso;
        }

        public static UserModel getNameIdAndRutaOfTheUser()
        {
            UserModel user = null;
            var db = new SQLiteConnection(ClsSQLiteDbHelper.instanceSQLite);
            db.Open();
            try
            {
                String query = "SELECT * FROM " + LocalDatabase.TABLA_USUARIO + " WHERE " + LocalDatabase.CAMPO_ID_USUARIO + " = @idUser";
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    command.Parameters.AddWithValue("@idUser", ClsRegeditController.getIdUserInTurn());
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                user = new UserModel();
                                user.id = reader.GetInt32(0);
                                user.Nombre = reader.GetString(1);
                                user.Ruta = reader.GetString(4);
                                user.cajaCodigo = reader.GetString(33);
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
            return user;
        }

        public static List<UserModel> getAllTellers()
        {
            List<UserModel> usersList = null;
            UserModel um;
            var db = new SQLiteConnection(ClsSQLiteDbHelper.instanceSQLite);
            db.Open();
            try
            {
                String query = "SELECT * FROM " + LocalDatabase.TABLA_USUARIO + " WHERE " +
                        LocalDatabase.CAMPO_TIPOUSUARIO_USUARIO + " = " + TYPE_TELLER;
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            usersList = new List<UserModel>();
                            while (reader.Read())
                            {
                                um = new UserModel();
                                um.id = Convert.ToInt32(reader[LocalDatabase.CAMPO_ID_USUARIO].ToString().Trim());
                                um.Nombre = reader[LocalDatabase.CAMPO_NOMBRE_USER].ToString().Trim();
                                usersList.Add(um);
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
            return usersList;
        }

        public static List<UserModel> getAllAgents()
        {
            List<UserModel> usersList = null;
            UserModel um;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "SELECT * FROM " + LocalDatabase.TABLA_USUARIO + " WHERE " +
                        LocalDatabase.CAMPO_TIPOUSUARIO_USUARIO + " = " + TYPE_ROM;
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            usersList = new List<UserModel>();
                            while (reader.Read())
                            {
                                um = new UserModel();
                                um.id = Convert.ToInt32(reader[LocalDatabase.CAMPO_ID_USUARIO].ToString().Trim());
                                um.Clave = reader[LocalDatabase.CAMPO_ClAVE_USUARIO].ToString().Trim();
                                um.Nombre = reader[LocalDatabase.CAMPO_NOMBRE_USER].ToString().Trim();
                                usersList.Add(um);
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
            return usersList;
        }

        public static String getCashNameFromUserInTurn()
        {
            String name = "";
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "SELECT " + LocalDatabase.CAMPO_CAJANOMBRE_USUARIO + " FROM " +
                        LocalDatabase.TABLA_USUARIO + " WHERE " + LocalDatabase.CAMPO_ClAVE_USUARIO + " = @idUserInTurn";
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    command.Parameters.AddWithValue("@idUserInTurn", ClsRegeditController.getIdUserInTurn());
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
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

    public class clsAgentes
    {
        public string USUARIO_ID { set; get; }
        public string CLAVE { set; get; }
        public string PASS { set; get; }
        public string NOMBRE { set; get; }
        public string RUTA { set; get; }
        public string ALMACEN_ID { set; get; }
        public string VALIDA_INVENTARIO_VENTAS { set; get; }
        public string USAQR { set; get; }
        public string MODVENTAS { set; get; }
        public string MODCOBRANZA { set; get; }
        public string DATOS { set; get; }
        public string GPS { set; get; }
        public string ABRIRCAJA { set; get; }
        public string PCOBRANZA { set; get; }
        public string PVENTA { set; get; }
        public string PPEDIDO { set; get; }
        public string PCOTIZACION { set; get; }
        public string PINFORMACION { set; get; }
        public string PVISITA { set; get; }
        public string PHISTORIAL { set; get; }
        public string PDEVOLUCIONES { set; get; }
        public string MODPEDIDOS { set; get; }
        public string MODPRECIO { set; get; }
        public string FACTURA { set; get; }
        public string VALIDACAJA { set; get; }
        public string LIMITE_CREDITO { set; get; }
        public string PRECIO_EMPRESA_ID { set; get; }
        public string MOD_DESCUENTO { set; get; }
        public string ALTACLIENTE { set; get; }
        public string SIG_FOLIO { set; get; }
        public double descMaximo { get; set; }
        public string TIPOUSUARIO { set; get; }
        public string CAJAID { set; get; }
        public string CAJACODIGO { set; get; }
        public string CAJANOMBRE { set; get; }
        public int permisoLimitarCredito { get; set; }
        public int traspasosAlmacen { get; set; }
        public int salidasAlmacen { get; set; }
        public int connected { get; set; }
        public String lastLogin { get; set; }
        public String lastSeen { get; set; }
        public int status { get; set; }
        public int permisoSelectPrice { get; set; }
        public int permisoPrePedido { get; set; }
        public int permisoFacturarCredito { get; set; }
        public int todosLosClientes { get; set; }
        public int enterpriseId { get; set; }
        public int venderSinExistencia { get; set; }
        public int venderACredito { get; set; }
    }

}
