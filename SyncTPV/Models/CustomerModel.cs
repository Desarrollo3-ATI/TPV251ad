using SyncTPV.Helpers.SqliteDatabaseHelper;
using SyncTPV.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Globalization;
using wsROMClases.Models.Panel;

namespace SyncTPV
{

    public class CustomerModel
    {
        public static int saveCustomers(List<ClsClienteModel> customersList)
        {
            int lastId = 0;
            if (customersList != null)
            {
                var db = new SQLiteConnection();
                try
                {
                    db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                    db.Open();
                    foreach (ClsClienteModel cliente in customersList)
                    {
                        string customerName = "";
                        customerName = cliente.NOMBRE.Replace("'", "").Trim();
                        string query = "INSERT INTO " + LocalDatabase.TABLA_CLIENTES + " VALUES (@id, @name, @code, @creditLimit, " +
                            "@condicionDePago, @listaPrecio, @direccion, @telefono, @precioEmpresaId, @zonaClienteId, @aval, @referencia, " +
                            "@formaCobro, @formaCobroCcId, @historialCredito, @claveQr, @clienteVisitado, @clienteNoVisitado, " +
                            "@papeleraReciclaje, @photo, @rfc, @curp, @denominacionComercial, @fechaAlta, @repLegal, @monedaId, @descDocto, " +
                            "@descMovto, @banCredito, @clasificacionId1, @clasificacionId2, @clasificacionId3, @clasificacionId4, " +
                            "@clasificacionId5, @clasificacionId6, @tipo, @status, @fechaBaja, @diasDeCredito, @excederLimiteCredito, " +
                            "@descuentoProntoPago, @diasProntoPago, @interesMoratorio, @mensajeria, @cuentaMensajeria, @almacenId, " +
                            "@agenteVentaId, @agenteCobroId, @restriccionAgente, @imp1, @imp2, @imp3, @reten1, @reten2, " +
                            "@tipoContribuyente, @regimenFiscal, @usoCFDI)";
                        using (SQLiteCommand command = new SQLiteCommand(query, db))
                        {
                            command.Parameters.AddWithValue("@id", cliente.CLIENTE_ID);
                            command.Parameters.AddWithValue("@name", customerName);
                            command.Parameters.AddWithValue("@code", cliente.CLAVE);
                            command.Parameters.AddWithValue("@creditLimit", cliente.LIMITE_CREDITO);
                            command.Parameters.AddWithValue("@condicionDePago", cliente.CONDICIONPAGO);
                            command.Parameters.AddWithValue("@listaPrecio", cliente.LISTAPRECIO);
                            command.Parameters.AddWithValue("@direccion", cliente.DIRECCION);
                            command.Parameters.AddWithValue("@telefono", cliente.TELEFONO);
                            command.Parameters.AddWithValue("@precioEmpresaId", cliente.PRECIO_EMPRESA_ID);
                            command.Parameters.AddWithValue("@zonaClienteId", cliente.ZONA_CLIENTE_ID);
                            command.Parameters.AddWithValue("@aval", cliente.AVAL);
                            command.Parameters.AddWithValue("@referencia", cliente.REFERENCIA);
                            command.Parameters.AddWithValue("@formaCobro", cliente.FORMA_COBRO);
                            command.Parameters.AddWithValue("@formaCobroCcId", cliente.FORMA_COBRO_CC_ID);
                            command.Parameters.AddWithValue("@historialCredito", cliente.HISTORIA_CREDITO);
                            command.Parameters.AddWithValue("@claveQr", cliente.CLAVEQR);
                            command.Parameters.AddWithValue("@clienteVisitado", cliente.CLIVISITADO);
                            command.Parameters.AddWithValue("@clienteNoVisitado", cliente.CLINOVISITADO);
                            command.Parameters.AddWithValue("@papeleraReciclaje", cliente.papelera_reciclaje);
                            command.Parameters.AddWithValue("@photo", cliente.photo);
                            command.Parameters.AddWithValue("@rfc", cliente.rfc);
                            command.Parameters.AddWithValue("@curp", cliente.curp);
                            command.Parameters.AddWithValue("@denominacionComercial", cliente.denominacionComercial);
                            command.Parameters.AddWithValue("@fechaAlta", cliente.fechaAlta);
                            command.Parameters.AddWithValue("@repLegal", cliente.representanteLegal);
                            command.Parameters.AddWithValue("@monedaId", cliente.monedaId);
                            command.Parameters.AddWithValue("@descDocto", cliente.descuentoDocumento);
                            command.Parameters.AddWithValue("@descMovto", cliente.descuentoMovimiento);
                            command.Parameters.AddWithValue("@banCredito", cliente.banCredito);
                            command.Parameters.AddWithValue("@clasificacionId1", cliente.clasificacionId1);
                            command.Parameters.AddWithValue("@clasificacionId2", cliente.clasificacionId2);
                            command.Parameters.AddWithValue("@clasificacionId3", cliente.clasificacionId3);
                            command.Parameters.AddWithValue("@clasificacionId4", cliente.clasificacionId4);
                            command.Parameters.AddWithValue("@clasificacionId5", cliente.clasificacionId5);
                            command.Parameters.AddWithValue("@clasificacionId6", cliente.clasificacionId6);
                            command.Parameters.AddWithValue("@tipo", cliente.tipo);
                            command.Parameters.AddWithValue("@status", cliente.status);
                            command.Parameters.AddWithValue("@fechaBaja", cliente.fechaBaja);
                            command.Parameters.AddWithValue("@diasDeCredito", cliente.diasDeCredito);
                            command.Parameters.AddWithValue("@excederLimiteCredito", cliente.excederLimiteCredito);
                            command.Parameters.AddWithValue("@descuentoProntoPago", cliente.descuentoProntoPago);
                            command.Parameters.AddWithValue("@diasProntoPago", cliente.diasProntoPago);
                            command.Parameters.AddWithValue("@interesMoratorio", cliente.interesMoratorio);
                            command.Parameters.AddWithValue("@mensajeria", cliente.mensajeria);
                            command.Parameters.AddWithValue("@cuentaMensajeria", cliente.cuentaMensajeria);
                            command.Parameters.AddWithValue("@almacenId", cliente.almacenId);
                            command.Parameters.AddWithValue("@agenteVentaId", cliente.agenteVentaId);
                            command.Parameters.AddWithValue("@agenteCobroId", cliente.agenteCobroId);
                            command.Parameters.AddWithValue("@restriccionAgente", cliente.restriccionAgente);
                            command.Parameters.AddWithValue("@imp1", cliente.imp1);
                            command.Parameters.AddWithValue("@imp2", cliente.imp2);
                            command.Parameters.AddWithValue("@imp3", cliente.imp3);
                            command.Parameters.AddWithValue("@reten1", cliente.reten1);
                            command.Parameters.AddWithValue("@reten2", cliente.reten2);
                            if (cliente.rfc.Length == 13)
                                command.Parameters.AddWithValue("@tipoContribuyente", 0);
                            else command.Parameters.AddWithValue("@tipoContribuyente", 1);
                            command.Parameters.AddWithValue("@regimenFiscal", cliente.codigoRegimenFiscal);
                            command.Parameters.AddWithValue("@usoCFDI", cliente.codigoUsoCFDI);
                            int recordInserted = command.ExecuteNonQuery();
                            if (recordInserted != 0)
                                lastId = Convert.ToInt32(cliente.CLIENTE_ID);
                        }
                    }
                }
                catch (SQLiteException e)
                {
                    SECUDOC.writeLog(e.ToString()); ;
                }
                finally
                {
                    if (db != null && db.State == ConnectionState.Open)
                        db.Close();
                }
            }
            return lastId;
        }

        public static int saveCustomersLAN(List<ClsClienteModel> customersList)
        {
            int count = 0;
            if (customersList != null)
            {
                var db = new SQLiteConnection();
                try
                {
                    db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                    db.Open();
                    foreach (ClsClienteModel cliente in customersList)
                    {
                        string query = "INSERT INTO " + LocalDatabase.TABLA_CLIENTES + " VALUES (@id, @name, @code, @creditLimit, " +
                            "@condicionDePago, @listaPrecio, @direccion, @telefono, @precioEmpresaId, @zonaClienteId, @aval, @referencia, " +
                            "@formaCobro, @formaCobroCcId, @historialCredito, @claveQr, @clienteVisitado, @clienteNoVisitado, " +
                            "@papeleraReciclaje, @photo, @rfc, @curp, @denominacionComercial, @fechaAlta, @repLegal, @monedaId, @descDocto, " +
                            "@descMovto, @banCredito, @clasificacionId1, @clasificacionId2, @clasificacionId3, @clasificacionId4, " +
                            "@clasificacionId5, @clasificacionId6, @tipo, @status, @fechaBaja, @diasDeCredito, @excederLimiteCredito, " +
                            "@descuentoProntoPago, @diasProntoPago, @interesMoratorio, @mensajeria, @cuentaMensajeria, @almacenId, " +
                            "@agenteVentaId, @agenteCobroId, @restriccionAgente, @imp1, @imp2, @imp3, @reten1, @reten2, " +
                            "@tipoContribuyente, @regimenFiscal, @usoCFDI)";
                        using (SQLiteCommand command = new SQLiteCommand(query, db))
                        {
                            command.Parameters.AddWithValue("@id", cliente.CLIENTE_ID);
                            command.Parameters.AddWithValue("@name", cliente.NOMBRE);
                            command.Parameters.AddWithValue("@code", cliente.CLAVE);
                            command.Parameters.AddWithValue("@creditLimit", cliente.LIMITE_CREDITO);
                            command.Parameters.AddWithValue("@condicionDePago", cliente.CONDICIONPAGO);
                            command.Parameters.AddWithValue("@listaPrecio", cliente.LISTAPRECIO);
                            command.Parameters.AddWithValue("@direccion", cliente.DIRECCION);
                            command.Parameters.AddWithValue("@telefono", cliente.TELEFONO);
                            command.Parameters.AddWithValue("@precioEmpresaId", cliente.PRECIO_EMPRESA_ID);
                            command.Parameters.AddWithValue("@zonaClienteId", cliente.ZONA_CLIENTE_ID);
                            command.Parameters.AddWithValue("@aval", cliente.AVAL);
                            command.Parameters.AddWithValue("@referencia", cliente.REFERENCIA);
                            command.Parameters.AddWithValue("@formaCobro", cliente.FORMA_COBRO);
                            command.Parameters.AddWithValue("@formaCobroCcId", cliente.FORMA_COBRO_CC_ID);
                            command.Parameters.AddWithValue("@historialCredito", cliente.HISTORIA_CREDITO);
                            command.Parameters.AddWithValue("@claveQr", cliente.CLAVEQR);
                            command.Parameters.AddWithValue("@clienteVisitado", 0);
                            command.Parameters.AddWithValue("@clienteNoVisitado", 0);
                            command.Parameters.AddWithValue("@papeleraReciclaje", 0);
                            command.Parameters.AddWithValue("@photo", "");
                            command.Parameters.AddWithValue("@rfc", cliente.rfc);
                            command.Parameters.AddWithValue("@curp", cliente.curp);
                            command.Parameters.AddWithValue("@denominacionComercial", cliente.denominacionComercial);
                            command.Parameters.AddWithValue("@fechaAlta", cliente.fechaAlta);
                            command.Parameters.AddWithValue("@repLegal", cliente.representanteLegal);
                            command.Parameters.AddWithValue("@monedaId", cliente.monedaId);
                            command.Parameters.AddWithValue("@descDocto", cliente.descuentoDocumento);
                            command.Parameters.AddWithValue("@descMovto", cliente.descuentoMovimiento);
                            command.Parameters.AddWithValue("@banCredito", cliente.banCredito);
                            command.Parameters.AddWithValue("@clasificacionId1", cliente.clasificacionId1);
                            command.Parameters.AddWithValue("@clasificacionId2", cliente.clasificacionId2);
                            command.Parameters.AddWithValue("@clasificacionId3", cliente.clasificacionId3);
                            command.Parameters.AddWithValue("@clasificacionId4", cliente.clasificacionId4);
                            command.Parameters.AddWithValue("@clasificacionId5", cliente.clasificacionId5);
                            command.Parameters.AddWithValue("@clasificacionId6", cliente.clasificacionId6);
                            command.Parameters.AddWithValue("@tipo", cliente.tipo);
                            command.Parameters.AddWithValue("@status", cliente.status);
                            command.Parameters.AddWithValue("@fechaBaja", cliente.fechaBaja);
                            command.Parameters.AddWithValue("@diasDeCredito", cliente.diasDeCredito);
                            command.Parameters.AddWithValue("@excederLimiteCredito", cliente.excederLimiteCredito);
                            command.Parameters.AddWithValue("@descuentoProntoPago", cliente.descuentoProntoPago);
                            command.Parameters.AddWithValue("@diasProntoPago", cliente.diasProntoPago);
                            command.Parameters.AddWithValue("@interesMoratorio", cliente.interesMoratorio);
                            command.Parameters.AddWithValue("@mensajeria", cliente.mensajeria);
                            command.Parameters.AddWithValue("@cuentaMensajeria", cliente.cuentaMensajeria);
                            command.Parameters.AddWithValue("@almacenId", cliente.almacenId);
                            command.Parameters.AddWithValue("@agenteVentaId", cliente.agenteVentaId);
                            command.Parameters.AddWithValue("@agenteCobroId", cliente.agenteCobroId);
                            command.Parameters.AddWithValue("@restriccionAgente", cliente.restriccionAgente);
                            command.Parameters.AddWithValue("@imp1", cliente.imp1);
                            command.Parameters.AddWithValue("@imp2", cliente.imp2);
                            command.Parameters.AddWithValue("@imp3", cliente.imp3);
                            command.Parameters.AddWithValue("@reten1", cliente.reten1);
                            command.Parameters.AddWithValue("@reten2", cliente.reten2);
                            if (cliente.rfc.Length == 13)
                                command.Parameters.AddWithValue("@tipoContribuyente", 0);
                            else command.Parameters.AddWithValue("@tipoContribuyente", 1);
                            command.Parameters.AddWithValue("@regimenFiscal", cliente.codigoRegimenFiscal);
                            command.Parameters.AddWithValue("@usoCFDI", cliente.codigoUsoCFDI);
                            int recordInserted = command.ExecuteNonQuery();
                            if (recordInserted != 0)
                                count++;
                        }
                    }
                }
                catch (SQLiteException e)
                {
                    SECUDOC.writeLog(e.ToString()); ;
                }
                finally
                {
                    if (db != null && db.State == ConnectionState.Open)
                        db.Close();
                }
            }
            return count;
        }

        public static int createCustomer(ClsClienteModel cliente)
        {
            int idInserted = 0;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "INSERT INTO " + LocalDatabase.TABLA_CLIENTES + " VALUES (@id, @name, @code, @creditLimit, " +
                            "@condicionDePago, @listaPrecio, @direccion, @telefono, @precioEmpresaId, @zonaClienteId, @aval, @referencia, " +
                            "@formaCobro, @formaCobroCcId, @historialCredito, @claveQr, @clienteVisitado, @clienteNoVisitado, " +
                            "@papeleraReciclaje, @photo, @rfc, @curp, @denominacionComercial, @fechaAlta, @repLegal, @monedaId, @descDocto, " +
                            "@descMovto, @banCredito, @clasificacionId1, @clasificacionId2, @clasificacionId3, @clasificacionId4, " +
                            "@clasificacionId5, @clasificacionId6, @tipo, @status, @fechaBaja, @diasDeCredito, @excederLimiteCredito, " +
                            "@descuentoProntoPago, @diasProntoPago, @interesMoratorio, @mensajeria, @cuentaMensajeria, @almacenId, " +
                            "@agenteVentaId, @agenteCobroId, @restriccionAgente, @imp1, @imp2, @imp3, @reten1, @reten2, " +
                            "@tipoContribuyente, @regimenFiscal, @usoCFDI)";
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    command.Parameters.AddWithValue("@id", cliente.CLIENTE_ID);
                    command.Parameters.AddWithValue("@name", cliente.NOMBRE);
                    command.Parameters.AddWithValue("@code", cliente.CLAVE);
                    command.Parameters.AddWithValue("@creditLimit", cliente.LIMITE_CREDITO);
                    command.Parameters.AddWithValue("@condicionDePago", cliente.CONDICIONPAGO);
                    command.Parameters.AddWithValue("@listaPrecio", cliente.LISTAPRECIO);
                    command.Parameters.AddWithValue("@direccion", cliente.DIRECCION);
                    command.Parameters.AddWithValue("@telefono", cliente.TELEFONO);
                    command.Parameters.AddWithValue("@precioEmpresaId", cliente.PRECIO_EMPRESA_ID);
                    command.Parameters.AddWithValue("@zonaClienteId", cliente.ZONA_CLIENTE_ID);
                    command.Parameters.AddWithValue("@aval", cliente.AVAL);
                    command.Parameters.AddWithValue("@referencia", cliente.REFERENCIA);
                    command.Parameters.AddWithValue("@formaCobro", cliente.FORMA_COBRO);
                    command.Parameters.AddWithValue("@formaCobroCcId", cliente.FORMA_COBRO_CC_ID);
                    command.Parameters.AddWithValue("@historialCredito", cliente.HISTORIA_CREDITO);
                    command.Parameters.AddWithValue("@claveQr", cliente.CLAVEQR);
                    command.Parameters.AddWithValue("@clienteVisitado", 0);
                    command.Parameters.AddWithValue("@clienteNoVisitado", 0);
                    command.Parameters.AddWithValue("@papeleraReciclaje", 0);
                    command.Parameters.AddWithValue("@photo", "");
                    command.Parameters.AddWithValue("@rfc", cliente.rfc);
                    command.Parameters.AddWithValue("@curp", cliente.curp);
                    command.Parameters.AddWithValue("@denominacionComercial", cliente.denominacionComercial);
                    command.Parameters.AddWithValue("@fechaAlta", cliente.fechaAlta);
                    command.Parameters.AddWithValue("@repLegal", cliente.representanteLegal);
                    command.Parameters.AddWithValue("@monedaId", cliente.monedaId);
                    command.Parameters.AddWithValue("@descDocto", cliente.descuentoDocumento);
                    command.Parameters.AddWithValue("@descMovto", cliente.descuentoMovimiento);
                    command.Parameters.AddWithValue("@banCredito", cliente.banCredito);
                    command.Parameters.AddWithValue("@clasificacionId1", cliente.clasificacionId1);
                    command.Parameters.AddWithValue("@clasificacionId2", cliente.clasificacionId2);
                    command.Parameters.AddWithValue("@clasificacionId3", cliente.clasificacionId3);
                    command.Parameters.AddWithValue("@clasificacionId4", cliente.clasificacionId4);
                    command.Parameters.AddWithValue("@clasificacionId5", cliente.clasificacionId5);
                    command.Parameters.AddWithValue("@clasificacionId6", cliente.clasificacionId6);
                    command.Parameters.AddWithValue("@tipo", cliente.tipo);
                    command.Parameters.AddWithValue("@status", cliente.status);
                    command.Parameters.AddWithValue("@fechaBaja", cliente.fechaBaja);
                    command.Parameters.AddWithValue("@diasDeCredito", cliente.diasDeCredito);
                    command.Parameters.AddWithValue("@excederLimiteCredito", cliente.excederLimiteCredito);
                    command.Parameters.AddWithValue("@descuentoProntoPago", cliente.descuentoProntoPago);
                    command.Parameters.AddWithValue("@diasProntoPago", cliente.diasProntoPago);
                    command.Parameters.AddWithValue("@interesMoratorio", cliente.interesMoratorio);
                    command.Parameters.AddWithValue("@mensajeria", cliente.mensajeria);
                    command.Parameters.AddWithValue("@cuentaMensajeria", cliente.cuentaMensajeria);
                    command.Parameters.AddWithValue("@almacenId", cliente.almacenId);
                    command.Parameters.AddWithValue("@agenteVentaId", cliente.agenteVentaId);
                    command.Parameters.AddWithValue("@agenteCobroId", cliente.agenteCobroId);
                    command.Parameters.AddWithValue("@restriccionAgente", cliente.restriccionAgente);
                    command.Parameters.AddWithValue("@imp1", cliente.imp1);
                    command.Parameters.AddWithValue("@imp2", cliente.imp2);
                    command.Parameters.AddWithValue("@imp3", cliente.imp3);
                    command.Parameters.AddWithValue("@reten1", cliente.reten1);
                    command.Parameters.AddWithValue("@reten2", cliente.reten2);
                    if (cliente.rfc.Length == 13)
                        command.Parameters.AddWithValue("@tipoContribuyente", 0);
                    else command.Parameters.AddWithValue("@tipoContribuyente", 1);
                    command.Parameters.AddWithValue("@regimenFiscal", cliente.codigoRegimenFiscal);
                    command.Parameters.AddWithValue("@usoCFDI", cliente.codigoUsoCFDI);
                    int recordInserted = command.ExecuteNonQuery();
                    if (recordInserted != 0)
                        idInserted = cliente.CLIENTE_ID;
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
            return idInserted;
        }

        public static int createCustomer(SQLiteConnection db, ClsClienteModel cliente)
        {
            int idInserted = 0;
            try
            {
                String query = "INSERT INTO " + LocalDatabase.TABLA_CLIENTES + " VALUES (@id, @name, @code, @creditLimit, " +
                            "@condicionDePago, @listaPrecio, @direccion, @telefono, @precioEmpresaId, @zonaClienteId, @aval, @referencia, " +
                            "@formaCobro, @formaCobroCcId, @historialCredito, @claveQr, @clienteVisitado, @clienteNoVisitado, " +
                            "@papeleraReciclaje, @photo, @rfc, @curp, @denominacionComercial, @fechaAlta, @repLegal, @monedaId, @descDocto, " +
                            "@descMovto, @banCredito, @clasificacionId1, @clasificacionId2, @clasificacionId3, @clasificacionId4, " +
                            "@clasificacionId5, @clasificacionId6, @tipo, @status, @fechaBaja, @diasDeCredito, @excederLimiteCredito, " +
                            "@descuentoProntoPago, @diasProntoPago, @interesMoratorio, @mensajeria, @cuentaMensajeria, @almacenId, " +
                            "@agenteVentaId, @agenteCobroId, @restriccionAgente, @imp1, @imp2, @imp3, @reten1, @reten2, " +
                            "@tipoCotribuyente, @regimenFiscal, @usoCFDI)";
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    command.Parameters.AddWithValue("@id", cliente.CLIENTE_ID);
                    command.Parameters.AddWithValue("@name", cliente.NOMBRE);
                    command.Parameters.AddWithValue("@code", cliente.CLAVE);
                    command.Parameters.AddWithValue("@creditLimit", cliente.LIMITE_CREDITO);
                    command.Parameters.AddWithValue("@condicionDePago", cliente.CONDICIONPAGO);
                    command.Parameters.AddWithValue("@listaPrecio", cliente.LISTAPRECIO);
                    command.Parameters.AddWithValue("@direccion", cliente.DIRECCION);
                    command.Parameters.AddWithValue("@telefono", cliente.TELEFONO);
                    command.Parameters.AddWithValue("@precioEmpresaId", cliente.PRECIO_EMPRESA_ID);
                    command.Parameters.AddWithValue("@zonaClienteId", cliente.ZONA_CLIENTE_ID);
                    command.Parameters.AddWithValue("@aval", cliente.AVAL);
                    command.Parameters.AddWithValue("@referencia", cliente.REFERENCIA);
                    command.Parameters.AddWithValue("@formaCobro", cliente.FORMA_COBRO);
                    command.Parameters.AddWithValue("@formaCobroCcId", cliente.FORMA_COBRO_CC_ID);
                    command.Parameters.AddWithValue("@historialCredito", cliente.HISTORIA_CREDITO);
                    command.Parameters.AddWithValue("@claveQr", cliente.CLAVEQR);
                    command.Parameters.AddWithValue("@clienteVisitado", 0);
                    command.Parameters.AddWithValue("@clienteNoVisitado", 0);
                    command.Parameters.AddWithValue("@papeleraReciclaje", 0);
                    command.Parameters.AddWithValue("@photo", "");
                    command.Parameters.AddWithValue("@rfc", cliente.rfc);
                    command.Parameters.AddWithValue("@curp", cliente.curp);
                    command.Parameters.AddWithValue("@denominacionComercial", cliente.denominacionComercial);
                    command.Parameters.AddWithValue("@fechaAlta", cliente.fechaAlta);
                    command.Parameters.AddWithValue("@repLegal", cliente.representanteLegal);
                    command.Parameters.AddWithValue("@monedaId", cliente.monedaId);
                    command.Parameters.AddWithValue("@descDocto", cliente.descuentoDocumento);
                    command.Parameters.AddWithValue("@descMovto", cliente.descuentoMovimiento);
                    command.Parameters.AddWithValue("@banCredito", cliente.banCredito);
                    command.Parameters.AddWithValue("@clasificacionId1", cliente.clasificacionId1);
                    command.Parameters.AddWithValue("@clasificacionId2", cliente.clasificacionId2);
                    command.Parameters.AddWithValue("@clasificacionId3", cliente.clasificacionId3);
                    command.Parameters.AddWithValue("@clasificacionId4", cliente.clasificacionId4);
                    command.Parameters.AddWithValue("@clasificacionId5", cliente.clasificacionId5);
                    command.Parameters.AddWithValue("@clasificacionId6", cliente.clasificacionId6);
                    command.Parameters.AddWithValue("@tipo", cliente.tipo);
                    command.Parameters.AddWithValue("@status", cliente.status);
                    command.Parameters.AddWithValue("@fechaBaja", cliente.fechaBaja);
                    command.Parameters.AddWithValue("@diasDeCredito", cliente.diasDeCredito);
                    command.Parameters.AddWithValue("@excederLimiteCredito", cliente.excederLimiteCredito);
                    command.Parameters.AddWithValue("@descuentoProntoPago", cliente.descuentoProntoPago);
                    command.Parameters.AddWithValue("@diasProntoPago", cliente.diasProntoPago);
                    command.Parameters.AddWithValue("@interesMoratorio", cliente.interesMoratorio);
                    command.Parameters.AddWithValue("@mensajeria", cliente.mensajeria);
                    command.Parameters.AddWithValue("@cuentaMensajeria", cliente.cuentaMensajeria);
                    command.Parameters.AddWithValue("@almacenId", cliente.almacenId);
                    command.Parameters.AddWithValue("@agenteVentaId", cliente.agenteVentaId);
                    command.Parameters.AddWithValue("@agenteCobroId", cliente.agenteCobroId);
                    command.Parameters.AddWithValue("@restriccionAgente", cliente.restriccionAgente);
                    command.Parameters.AddWithValue("@imp1", cliente.imp1);
                    command.Parameters.AddWithValue("@imp2", cliente.imp2);
                    command.Parameters.AddWithValue("@imp3", cliente.imp3);
                    command.Parameters.AddWithValue("@reten1", cliente.reten1);
                    command.Parameters.AddWithValue("@reten2", cliente.reten2);
                    if (cliente.rfc.Length == 13)
                        command.Parameters.AddWithValue("@tipoCotribuyente", 0);
                    else command.Parameters.AddWithValue("@tipoCotribuyente", 1);
                    command.Parameters.AddWithValue("@regimenFiscal", cliente.codigoRegimenFiscal);
                    command.Parameters.AddWithValue("@usoCFDI", cliente.codigoUsoCFDI);
                    int recordInserted = command.ExecuteNonQuery();
                    if (recordInserted != 0)
                        idInserted = cliente.CLIENTE_ID;
                }
            }
            catch (SQLiteException e)
            {
                SECUDOC.writeLog(e.ToString());
            }
            finally
            {
                
            }
            return idInserted;
        }

        public static int validarCLientesADCyCustomers()
        {
            int Error = 0;
            decimal total = 0.0M, cambio = 0.0M;
            List<CustomerADCModel> clientesADCs = new List<CustomerADCModel>();
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "SELECT * from "+LocalDatabase.TABLA_CLIENTEADC;
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                int auxADC = Convert.ToInt32("-"+reader["id"].ToString().Trim());
                                ClsClienteModel auxclient = getAllDataFromACustomer(auxADC);
                                if(auxclient == null)
                                {
                                    CustomerADCModel ADC = new CustomerADCModel();
                                    ADC.id = Convert.ToInt32(reader["id"].ToString().Trim());
                                    ADC.nombre = reader["NOMBRE"].ToString().Trim();
                                    ADC.rfc = reader["RFC"].ToString().Trim();
                                    ADC.referencia = reader["REFERENCIA"].ToString().Trim();
                                    ADC.telefono = reader["TELEFONO"].ToString().Trim();
                                    ADC.tipoContribuyente = Convert.ToInt32(reader["tipoContribuyente"].ToString().Trim());
                                    ADC.codigoRegimenFiscal = reader["codigoRegimenFiscal"].ToString().Trim();
                                    ADC.codigoUsoCFDI = reader["codigoUsoCFDI"].ToString().Trim();
                                    clientesADCs.Add(ADC);
                                }
                            }
                        }
                        if (reader != null && !reader.IsClosed)
                            reader.Close();
                    }
                }
            }
            catch (Exception Ex)
            {
                Error = 1;
                SECUDOC.writeLog(Ex.ToString());

            }
            finally
            {
                if (db != null && db.State == ConnectionState.Open)
                    db.Close();
            }
            try
            {

            }catch(Exception e)
            {
                SECUDOC.writeLog(e.ToString());
            }
            for (int i=0; i< clientesADCs.Count; i++)
            {
                try
                {
                    String idClienteNegativo = "-" + clientesADCs[i].id;
                    CustomerModel.createNewCustomer(Convert.ToInt32(idClienteNegativo), clientesADCs[i].nombre, "", 0, "", "1", "",
                        clientesADCs[i].telefono, 1, 0, "", clientesADCs[i].referencia, "", 0, "Bueno", "", 0, 0, 0, "", clientesADCs[i].rfc, "", "", clientesADCs[i].tipoContribuyente, clientesADCs[i].codigoRegimenFiscal, clientesADCs[i].codigoUsoCFDI);
                    if (CustomerModel.updateClaveClienteAdditionalAgregado(Convert.ToInt32(idClienteNegativo)) > 0)
                    {
                        Error = Convert.ToInt32(idClienteNegativo);
                    }
                }
                catch(Exception e)
                {
                    Error = 1;
                    SECUDOC.writeLog(e.ToString());
                }
            }
            return Error;
        }

        public static int createNewCustomer(int idCliente, String nombre, String codigo, double limiteCredito, String condicionesDePago, 
            String listaDePrecio, String direccion, String telefono, int precioEmpresaId, int zonaDeClienteId, String aval, String referencia,
            String formaDeCobro, int formaDeCobroId, String historialCredito, String claveQR, int clienteVisitado, int clienteNoVisitado, 
            int papelera, String photo, String rfc, String curp, String denominacionComercial, int tipoContribuyente,String usoCFDI, String RegimenFiscal)
        {
            int count = 0;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "INSERT INTO " + LocalDatabase.TABLA_CLIENTES + " VALUES (@id, @name, @code, @creditLimit, " +
                            "@condicionDePago, @listaPrecio, @direccion, @telefono, @precioEmpresaId, @zonaClienteId, @aval, @referencia, " +
                            "@formaCobro, @formaCobroCcId, @historialCredito, @claveQr, @clienteVisitado, @clienteNoVisitado, " +
                            "@papeleraReciclaje, @photo, @rfc, @curp, @denominacionComercial, @fechaAlta, @repLegal, @monedaId, @descuentoDocto, " +
                            "@descuentoMovto, @banCredito, @clasificacionId1, @clasificacionId2, @clasificacionId3, @clasificacionId4, " +
                            "@clasificacionId5, @clasificacionId6, @tipo, @status, @fechaBaja, @diasDeCredito, @excederCredito, @descProntoPago, " +
                            "@diasProntoPago, @interesMoratorio, @mensajeria, @cuentaMensajeria, @almacenId, @agenteVentaId, @agenteCobroId, " +
                            "@restriccionAgente, @imp1, @imp2, @imp3, @reten1, @reten2, @tipoContribuyente, " +
                            "@regimenFiscal, @usoCFDI)";
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    command.Parameters.AddWithValue("@id", idCliente);
                    command.Parameters.AddWithValue("@name", nombre);
                    command.Parameters.AddWithValue("@code", codigo);
                    command.Parameters.AddWithValue("@creditLimit", limiteCredito);
                    command.Parameters.AddWithValue("@condicionDePago", condicionesDePago);
                    command.Parameters.AddWithValue("@listaPrecio", listaDePrecio);
                    command.Parameters.AddWithValue("@direccion", direccion);
                    command.Parameters.AddWithValue("@telefono", telefono);
                    command.Parameters.AddWithValue("@precioEmpresaId", precioEmpresaId);
                    command.Parameters.AddWithValue("@zonaClienteId", zonaDeClienteId);
                    command.Parameters.AddWithValue("@aval", aval);
                    command.Parameters.AddWithValue("@referencia", referencia);
                    command.Parameters.AddWithValue("@formaCobro", formaDeCobro);
                    command.Parameters.AddWithValue("@formaCobroCcId", formaDeCobroId);
                    command.Parameters.AddWithValue("@historialCredito", historialCredito);
                    command.Parameters.AddWithValue("@claveQr", claveQR);
                    command.Parameters.AddWithValue("@clienteVisitado", clienteVisitado);
                    command.Parameters.AddWithValue("@clienteNoVisitado", clienteNoVisitado);
                    command.Parameters.AddWithValue("@papeleraReciclaje", papelera);
                    command.Parameters.AddWithValue("@photo", photo);
                    command.Parameters.AddWithValue("@rfc", rfc);
                    command.Parameters.AddWithValue("@curp", curp);
                    command.Parameters.AddWithValue("@denominacionComercial", denominacionComercial);
                    command.Parameters.AddWithValue("@fechaAlta", "");
                    command.Parameters.AddWithValue("@repLegal", "");
                    command.Parameters.AddWithValue("@monedaId", 1);
                    command.Parameters.AddWithValue("@descuentoDocto", 0);
                    command.Parameters.AddWithValue("@descuentoMovto", 0);
                    command.Parameters.AddWithValue("@banCredito", 0);
                    command.Parameters.AddWithValue("@clasificacionId1", 0);
                    command.Parameters.AddWithValue("@clasificacionId2", 0);
                    command.Parameters.AddWithValue("@clasificacionId3", 0);
                    command.Parameters.AddWithValue("@clasificacionId4", 0);
                    command.Parameters.AddWithValue("@clasificacionId5", 0);
                    command.Parameters.AddWithValue("@clasificacionId6", 0);
                    command.Parameters.AddWithValue("@tipo", 1);
                    command.Parameters.AddWithValue("@status", 1);
                    command.Parameters.AddWithValue("@fechaBaja", "");
                    command.Parameters.AddWithValue("@diasDeCredito", 0);
                    command.Parameters.AddWithValue("@excederCredito", 0);
                    command.Parameters.AddWithValue("@descProntoPago", 0);
                    command.Parameters.AddWithValue("@diasProntoPago", 0);
                    command.Parameters.AddWithValue("@interesMoratorio", 0);
                    command.Parameters.AddWithValue("@mensajeria", "");
                    command.Parameters.AddWithValue("@cuentaMensajeria", "");
                    command.Parameters.AddWithValue("@almacenId", 0);
                    command.Parameters.AddWithValue("@agenteVentaId", 0);
                    command.Parameters.AddWithValue("@agenteCobroId", 0);
                    command.Parameters.AddWithValue("@restriccionAgente", 0);
                    command.Parameters.AddWithValue("@imp1", 0);
                    command.Parameters.AddWithValue("@imp2", 0);
                    command.Parameters.AddWithValue("@imp3", 0);
                    command.Parameters.AddWithValue("@reten1", 0);
                    command.Parameters.AddWithValue("@reten2", 0);
                    command.Parameters.AddWithValue("@tipoContribuyente", tipoContribuyente);
                    command.Parameters.AddWithValue("@regimenFiscal", RegimenFiscal);
                    command.Parameters.AddWithValue("@usoCFDI", usoCFDI);
                    int recordInserted = command.ExecuteNonQuery();
                    if (recordInserted != 0)
                        count++;
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
            return count;
        }

        public static int updateNewCustomer(int idCliente, String nombre, String codigo, double limiteCredito, String condicionesDePago,
            String listaDePrecio, String direccion, String telefono, int precioEmpresaId, int zonaDeClienteId, String aval, String referencia,
            String formaDeCobro, int formaDeCobroId, String historialCredito, String claveQR, int clienteVisitado, int clienteNoVisitado,
            int papelera, String photo, String rfc, String curp, String denominacionComercial, int tipoContribuyente, 
            String regimenFiscal, String usoCFDI)
        {
            int count = 0;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "UPDATE " + LocalDatabase.TABLA_CLIENTES + " SET "+LocalDatabase.CAMPO_NOMBRECLIENTE+" = @name, "+
                    LocalDatabase.CAMPO_CLAVECLIENTE+" = @code, "+LocalDatabase.CAMPO_LIMITECREDITO_CLIENTE+" = @creditLimit, "+
                    LocalDatabase.CAMPO_CONDICIONPAGO+" = @condicionDePago, "+LocalDatabase.CAMPO_LISTAPRECIO+ " = @listaPrecio, "+
                    LocalDatabase.CAMPO_DIRECCION+" = @direccion, "+LocalDatabase.CAMPO_TELEFONO_CLIENTE+" = @telefono, "+
                    LocalDatabase.CAMPO_PRECIOEMPRESA_ID_CLIENTE+" = @precioEmpresaId, "+LocalDatabase.CAMPO_ZONACLIENTE_ID+" = @zonaClienteId, "+
                    LocalDatabase.CAMPO_AVAL+" = @aval, "+LocalDatabase.CAMPO_REFERENCIA+" = @referencia, "+
                    LocalDatabase.CAMPO_FORMA_COBRO+" = @formaCobro, "+LocalDatabase.CAMPO_FORMA_COBRO_CC+" = @formaCobroCcId, "+
                    LocalDatabase.CAMPO_HISTORIA_CREDITO+" = @historialCredito, "+LocalDatabase.CAMPO_CLAVEQR+" = @claveQr, "+
                    LocalDatabase.CAMPO_CLIVISITADO + " = @clienteVisitado, "+LocalDatabase.CAMPO_CLINOVISITADO_CLIENTE+" = @clienteNoVisitado, "+
                    LocalDatabase.CAMPO_PAPELERARECICLAJE_CLIENTE+" = @papeleraReciclaje, "+LocalDatabase.CAMPO_PHOTONAME_CLIENTE+" = @photo, "+
                    LocalDatabase.CAMPO_RFC_CLIENTE+" = @rfc, "+LocalDatabase.CAMPO_CURP_CLIENTE+" = @curp, "+
                    LocalDatabase.CAMPO_DENOMINACIONCOMCERCIAL_CLIENTE+" = @denominacionComercial, " +
                    LocalDatabase.CAMPO_TIPOCONTRIBUYENTE_CLIENTE + " = @tipoContribuyente, " +
                    LocalDatabase.CAMPO_CODIGOREGIMENFISCAL_CLIENTE + " = @regimenFiscal, " +
                    LocalDatabase.CAMPO_CODIGOUSOCFDI_CLIENTE + " = @usoCFDI " +
                    " WHERE " +LocalDatabase.CAMPO_ID_CLIENTE+ " = @id";
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    command.Parameters.AddWithValue("@id", idCliente);
                    command.Parameters.AddWithValue("@name", nombre);
                    command.Parameters.AddWithValue("@code", codigo);
                    command.Parameters.AddWithValue("@creditLimit", limiteCredito);
                    command.Parameters.AddWithValue("@condicionDePago", condicionesDePago);
                    command.Parameters.AddWithValue("@listaPrecio", listaDePrecio);
                    command.Parameters.AddWithValue("@direccion", direccion);
                    command.Parameters.AddWithValue("@telefono", telefono);
                    command.Parameters.AddWithValue("@precioEmpresaId", precioEmpresaId);
                    command.Parameters.AddWithValue("@zonaClienteId", zonaDeClienteId);
                    command.Parameters.AddWithValue("@aval", aval);
                    command.Parameters.AddWithValue("@referencia", referencia);
                    command.Parameters.AddWithValue("@formaCobro", formaDeCobro);
                    command.Parameters.AddWithValue("@formaCobroCcId", formaDeCobroId);
                    command.Parameters.AddWithValue("@historialCredito", historialCredito);
                    command.Parameters.AddWithValue("@claveQr", claveQR);
                    command.Parameters.AddWithValue("@clienteVisitado", clienteVisitado);
                    command.Parameters.AddWithValue("@clienteNoVisitado", clienteNoVisitado);
                    command.Parameters.AddWithValue("@papeleraReciclaje", papelera);
                    command.Parameters.AddWithValue("@photo", photo);
                    command.Parameters.AddWithValue("@rfc", rfc);
                    command.Parameters.AddWithValue("@curp", curp);
                    command.Parameters.AddWithValue("@denominacionComercial", denominacionComercial);
                    command.Parameters.AddWithValue("@tipoContribuyente", tipoContribuyente);
                    command.Parameters.AddWithValue("@regimenFiscal", regimenFiscal);
                    command.Parameters.AddWithValue("@usoCFDI", usoCFDI);
                    int recordInserted = command.ExecuteNonQuery();
                    if (recordInserted != 0)
                        count++;
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
            return count;
        }

        public static int updateCustomers(List<ClsClienteModel> customersList)
        {
            int lastId = 0;
            if (customersList != null)
            {
                var db = new SQLiteConnection();
                try
                {
                    db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                    db.Open();
                    foreach (ClsClienteModel cliente in customersList)
                    {
                        if (CustomerModel.checkIfTheClientExists(cliente.CLIENTE_ID))
                        {
                            string query = "UPDATE " + LocalDatabase.TABLA_CLIENTES + " SET " + LocalDatabase.CAMPO_NOMBRECLIENTE + " = @name, " +
                                LocalDatabase.CAMPO_CLAVECLIENTE + " = @code, " + LocalDatabase.CAMPO_LIMITECREDITO_CLIENTE + " = @creditLimit, " +
                                LocalDatabase.CAMPO_CONDICIONPAGO + " = @condicionDePago, " + LocalDatabase.CAMPO_LISTAPRECIO + " = @listaPrecio, " +
                                LocalDatabase.CAMPO_DIRECCION + " = @direccion, " + LocalDatabase.CAMPO_TELEFONO_CLIENTE + " = @telefono, " +
                                LocalDatabase.CAMPO_PRECIOEMPRESA_ID_CLIENTE + " = @precioEmpresaId, " + LocalDatabase.CAMPO_ZONACLIENTE_ID + " = @zonaClienteId, " +
                                LocalDatabase.CAMPO_AVAL + " = @aval, " + LocalDatabase.CAMPO_REFERENCIA + " = @referencia, " +
                                LocalDatabase.CAMPO_FORMA_COBRO + " = @formaCobro, " + LocalDatabase.CAMPO_FORMA_COBRO_CC + " = @formaCobroCcId, " +
                                LocalDatabase.CAMPO_HISTORIA_CREDITO + " = @historialCredito, " + LocalDatabase.CAMPO_CLAVEQR + " = @claveQr, " +
                                LocalDatabase.CAMPO_CLINOVISITADO_CLIENTE + " = @clienteVisitado, " + LocalDatabase.CAMPO_CLINOVISITADO_CLIENTE + " = @clienteNoVisitado, " +
                                LocalDatabase.CAMPO_PAPELERARECICLAJE_CLIENTE + " = @papeleraReciclaje, " + LocalDatabase.CAMPO_PHOTONAME_CLIENTE + " = @photo, " +
                                LocalDatabase.CAMPO_RFC_CLIENTE + " = @rfc, " + LocalDatabase.CAMPO_CURP_CLIENTE + " = @curp, " +
                                LocalDatabase.CAMPO_DENOMINACIONCOMCERCIAL_CLIENTE + " = @denominacionComercial, " +
                                "fecha_alta = @fechaAlta, representante_legal = @repLegal, monedaId = @monedaId, " +
                                "descuentoDocto = @descDocto, descuentoMovto = @descMovto, banCredito = @banCredito, " +
                                "clasificacionId1 = @clasificacionId1, clasificacionId2 = @clasificacionId2, " +
                                "clasificacionId3 = @clasificacionId3, clasificacionId4 = @clasificacionId4, " +
                                "clasificacionId5 = @clasificacionId5, clasificacionId6 = @clasificacionId6, " +
                                "tipo = @tipo, status = @status, fechaBaja = @fechaBaja, diasDeCredito = @diasDeCredito, " +
                                "excederCredito = @excederLimiteCredito, descuentoProntoPago = @descuentoProntoPago, " +
                                "diasProntoPago = @diasProntoPago, interesMoratorio = @interesMoratorio, mensajeria = @mensajeria, " +
                                "cuentaMensajeria = @cuentaMensajeria, almacenId = @almacenId, agenteVentaId = @agenteVentaId, " +
                                "agenteCobroId = @agenteCobroId, restriccionAgente = @restriccionAgente, imp1 = @imp1, " +
                                "imp2 = @imp2, imp3 = @imp3, reten1 = @reten1, reten2 = @reten2, " +
                                LocalDatabase.CAMPO_TIPOCONTRIBUYENTE_CLIENTE+" = @tipoContribuyente, "+
                                LocalDatabase.CAMPO_CODIGOREGIMENFISCAL_CLIENTE + " = @regimenFiscal, " +
                                LocalDatabase.CAMPO_CODIGOUSOCFDI_CLIENTE + " = @usoCFDI " +
                                "WHERE " + LocalDatabase.CAMPO_ID_CLIENTE + " = " + cliente.CLIENTE_ID;
                            using (SQLiteCommand command = new SQLiteCommand(query, db))
                            {
                                command.Parameters.AddWithValue("@name", cliente.NOMBRE);
                                command.Parameters.AddWithValue("@code", cliente.CLAVE);
                                command.Parameters.AddWithValue("@creditLimit", cliente.LIMITE_CREDITO);
                                command.Parameters.AddWithValue("@condicionDePago", cliente.CONDICIONPAGO);
                                command.Parameters.AddWithValue("@listaPrecio", cliente.LISTAPRECIO);
                                command.Parameters.AddWithValue("@direccion", cliente.DIRECCION);
                                command.Parameters.AddWithValue("@telefono", cliente.TELEFONO);
                                command.Parameters.AddWithValue("@precioEmpresaId", cliente.PRECIO_EMPRESA_ID);
                                command.Parameters.AddWithValue("@zonaClienteId", cliente.ZONA_CLIENTE_ID);
                                command.Parameters.AddWithValue("@aval", cliente.AVAL);
                                command.Parameters.AddWithValue("@referencia", cliente.REFERENCIA);
                                command.Parameters.AddWithValue("@formaCobro", cliente.FORMA_COBRO);
                                command.Parameters.AddWithValue("@formaCobroCcId", cliente.FORMA_COBRO_CC_ID);
                                command.Parameters.AddWithValue("@historialCredito", cliente.HISTORIA_CREDITO);
                                command.Parameters.AddWithValue("@claveQr", cliente.CLAVEQR);
                                command.Parameters.AddWithValue("@clienteVisitado", cliente.CLIVISITADO);
                                command.Parameters.AddWithValue("@clienteNoVisitado", cliente.CLINOVISITADO);
                                command.Parameters.AddWithValue("@papeleraReciclaje", cliente.papelera_reciclaje);
                                command.Parameters.AddWithValue("@photo", cliente.photo);
                                command.Parameters.AddWithValue("@rfc", cliente.rfc);
                                command.Parameters.AddWithValue("@curp", cliente.curp);
                                command.Parameters.AddWithValue("@denominacionComercial", cliente.denominacionComercial);
                                command.Parameters.AddWithValue("@fechaAlta", cliente.fechaAlta);
                                command.Parameters.AddWithValue("@repLegal", cliente.representanteLegal);
                                command.Parameters.AddWithValue("@monedaId", cliente.monedaId);
                                command.Parameters.AddWithValue("@descDocto", cliente.descuentoDocumento);
                                command.Parameters.AddWithValue("@descMovto", cliente.descuentoMovimiento);
                                command.Parameters.AddWithValue("@banCredito", cliente.banCredito);
                                command.Parameters.AddWithValue("@clasificacionId1", cliente.clasificacionId1);
                                command.Parameters.AddWithValue("@clasificacionId2", cliente.clasificacionId2);
                                command.Parameters.AddWithValue("@clasificacionId3", cliente.clasificacionId3);
                                command.Parameters.AddWithValue("@clasificacionId4", cliente.clasificacionId4);
                                command.Parameters.AddWithValue("@clasificacionId5", cliente.clasificacionId5);
                                command.Parameters.AddWithValue("@clasificacionId6", cliente.clasificacionId6);
                                command.Parameters.AddWithValue("@tipo", cliente.tipo);
                                command.Parameters.AddWithValue("@status", cliente.status);
                                command.Parameters.AddWithValue("@fechaBaja", cliente.fechaBaja);
                                command.Parameters.AddWithValue("@diasDeCredito", cliente.diasDeCredito);
                                command.Parameters.AddWithValue("@excederLimiteCredito", cliente.excederLimiteCredito);
                                command.Parameters.AddWithValue("@descuentoProntoPago", cliente.descuentoProntoPago);
                                command.Parameters.AddWithValue("@diasProntoPago", cliente.diasProntoPago);
                                command.Parameters.AddWithValue("@interesMoratorio", cliente.interesMoratorio);
                                command.Parameters.AddWithValue("@mensajeria", cliente.mensajeria);
                                command.Parameters.AddWithValue("@cuentaMensajeria", cliente.cuentaMensajeria);
                                command.Parameters.AddWithValue("@almacenId", cliente.almacenId);
                                command.Parameters.AddWithValue("@agenteVentaId", cliente.agenteVentaId);
                                command.Parameters.AddWithValue("@agenteCobroId", cliente.agenteCobroId);
                                command.Parameters.AddWithValue("@restriccionAgente", cliente.restriccionAgente);
                                command.Parameters.AddWithValue("@imp1", cliente.imp1);
                                command.Parameters.AddWithValue("@imp2", cliente.imp2);
                                command.Parameters.AddWithValue("@imp3", cliente.imp3);
                                command.Parameters.AddWithValue("@reten1", cliente.reten1);
                                command.Parameters.AddWithValue("@reten2", cliente.reten2);
                                if (cliente.rfc.Length == 13)
                                    command.Parameters.AddWithValue("@tipoContribuyente", 0);
                                else command.Parameters.AddWithValue("@tipoContribuyente", 1);
                                command.Parameters.AddWithValue("@regimenFiscal", cliente.codigoRegimenFiscal);
                                command.Parameters.AddWithValue("@usoCFDI", cliente.codigoUsoCFDI);
                                int recordUpdated = command.ExecuteNonQuery();
                                if (recordUpdated != 0)
                                    lastId = Convert.ToInt32(cliente.CLIENTE_ID);
                            }
                        }
                        else
                        {
                            string query = "INSERT INTO " + LocalDatabase.TABLA_CLIENTES + " VALUES (@id, @name, @code, @creditLimit, " +
                                "@condicionDePago, @listaPrecio, @direccion, @telefono, @precioEmpresaId, @zonaClienteId, @aval, @referencia, " +
                                "@formaCobro, @formaCobroCcId, @historialCredito, @claveQr, @clienteVisitado, @clienteNoVisitado, " +
                                "@papeleraReciclaje, @photo, @rfc, @curp, @denominacionComercial, @fechaAlta, @repLegal, @monedaId, @descDocto, " +
                            "@descMovto, @banCredito, @clasificacionId1, @clasificacionId2, @clasificacionId3, @clasificacionId4, " +
                            "@clasificacionId5, @clasificacionId6, @tipo, @status, @fechaBaja, @diasDeCredito, @excederLimiteCredito, " +
                            "@descuentoProntoPago, @diasProntoPago, @interesMoratorio, @mensajeria, @cuentaMensajeria, @almacenId, " +
                            "@agenteVentaId, @agenteCobroId, @restriccionAgente, @imp1, @imp2, @imp3, @reten1, @reten2, " +
                            "@tipoContribuyente, @regimenFiscal, @usoCFDI)";
                            using (SQLiteCommand command = new SQLiteCommand(query, db))
                            {
                                command.Parameters.AddWithValue("@id", cliente.CLIENTE_ID);
                                command.Parameters.AddWithValue("@name", cliente.NOMBRE);
                                command.Parameters.AddWithValue("@code", cliente.CLAVE);
                                command.Parameters.AddWithValue("@creditLimit", cliente.LIMITE_CREDITO);
                                command.Parameters.AddWithValue("@condicionDePago", cliente.CONDICIONPAGO);
                                command.Parameters.AddWithValue("@listaPrecio", cliente.LISTAPRECIO);
                                command.Parameters.AddWithValue("@direccion", cliente.DIRECCION);
                                command.Parameters.AddWithValue("@telefono", cliente.TELEFONO);
                                command.Parameters.AddWithValue("@precioEmpresaId", cliente.PRECIO_EMPRESA_ID);
                                command.Parameters.AddWithValue("@zonaClienteId", cliente.ZONA_CLIENTE_ID);
                                command.Parameters.AddWithValue("@aval", cliente.AVAL);
                                command.Parameters.AddWithValue("@referencia", cliente.REFERENCIA);
                                command.Parameters.AddWithValue("@formaCobro", cliente.FORMA_COBRO);
                                command.Parameters.AddWithValue("@formaCobroCcId", cliente.FORMA_COBRO_CC_ID);
                                command.Parameters.AddWithValue("@historialCredito", cliente.HISTORIA_CREDITO);
                                command.Parameters.AddWithValue("@claveQr", cliente.CLAVEQR);
                                command.Parameters.AddWithValue("@clienteVisitado", cliente.CLIVISITADO);
                                command.Parameters.AddWithValue("@clienteNoVisitado", cliente.CLINOVISITADO);
                                command.Parameters.AddWithValue("@papeleraReciclaje", cliente.papelera_reciclaje);
                                command.Parameters.AddWithValue("@photo", cliente.photo);
                                command.Parameters.AddWithValue("@rfc", cliente.rfc);
                                command.Parameters.AddWithValue("@curp", cliente.curp);
                                command.Parameters.AddWithValue("@denominacionComercial", cliente.denominacionComercial);
                                command.Parameters.AddWithValue("@fechaAlta", cliente.fechaAlta);
                                command.Parameters.AddWithValue("@repLegal", cliente.representanteLegal);
                                command.Parameters.AddWithValue("@monedaId", cliente.monedaId);
                                command.Parameters.AddWithValue("@descDocto", cliente.descuentoDocumento);
                                command.Parameters.AddWithValue("@descMovto", cliente.descuentoMovimiento);
                                command.Parameters.AddWithValue("@banCredito", cliente.banCredito);
                                command.Parameters.AddWithValue("@clasificacionId1", cliente.clasificacionId1);
                                command.Parameters.AddWithValue("@clasificacionId2", cliente.clasificacionId2);
                                command.Parameters.AddWithValue("@clasificacionId3", cliente.clasificacionId3);
                                command.Parameters.AddWithValue("@clasificacionId4", cliente.clasificacionId4);
                                command.Parameters.AddWithValue("@clasificacionId5", cliente.clasificacionId5);
                                command.Parameters.AddWithValue("@clasificacionId6", cliente.clasificacionId6);
                                command.Parameters.AddWithValue("@tipo", cliente.tipo);
                                command.Parameters.AddWithValue("@status", cliente.status);
                                command.Parameters.AddWithValue("@fechaBaja", cliente.fechaBaja);
                                command.Parameters.AddWithValue("@diasDeCredito", cliente.diasDeCredito);
                                command.Parameters.AddWithValue("@excederLimiteCredito", cliente.excederLimiteCredito);
                                command.Parameters.AddWithValue("@descuentoProntoPago", cliente.descuentoProntoPago);
                                command.Parameters.AddWithValue("@diasProntoPago", cliente.diasProntoPago);
                                command.Parameters.AddWithValue("@interesMoratorio", cliente.interesMoratorio);
                                command.Parameters.AddWithValue("@mensajeria", cliente.mensajeria);
                                command.Parameters.AddWithValue("@cuentaMensajeria", cliente.cuentaMensajeria);
                                command.Parameters.AddWithValue("@almacenId", cliente.almacenId);
                                command.Parameters.AddWithValue("@agenteVentaId", cliente.agenteVentaId);
                                command.Parameters.AddWithValue("@agenteCobroId", cliente.agenteCobroId);
                                command.Parameters.AddWithValue("@restriccionAgente", cliente.restriccionAgente);
                                command.Parameters.AddWithValue("@imp1", cliente.imp1);
                                command.Parameters.AddWithValue("@imp2", cliente.imp2);
                                command.Parameters.AddWithValue("@imp3", cliente.imp3);
                                command.Parameters.AddWithValue("@reten1", cliente.reten1);
                                command.Parameters.AddWithValue("@reten2", cliente.reten2);
                                if (cliente.rfc.Length == 13)
                                    command.Parameters.AddWithValue("@tipoContribuyente", 0);
                                else command.Parameters.AddWithValue("@tipoContribuyente", 1);
                                command.Parameters.AddWithValue("@regimenFiscal", cliente.codigoRegimenFiscal);
                                command.Parameters.AddWithValue("@usoCFDI", cliente.codigoUsoCFDI);
                                int recordInserted = command.ExecuteNonQuery();
                                if (recordInserted != 0)
                                    lastId = Convert.ToInt32(cliente.CLIENTE_ID);
                            }
                        }
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
            return lastId;
        }

        public static int updateCustomersLAN(List<ClsClienteModel> customersList)
        {
            int count = 0;
            if (customersList != null)
            {
                var db = new SQLiteConnection();
                try
                {
                    db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                    db.Open();
                    foreach (ClsClienteModel cliente in customersList)
                    {
                        if (CustomerModel.checkIfTheClientExists(cliente.CLIENTE_ID))
                        {
                            string query = "UPDATE " + LocalDatabase.TABLA_CLIENTES + " SET " + LocalDatabase.CAMPO_NOMBRECLIENTE + " = @name, " +
                                LocalDatabase.CAMPO_CLAVECLIENTE + " = @code, " + LocalDatabase.CAMPO_LIMITECREDITO_CLIENTE + " = @creditLimit, " +
                                LocalDatabase.CAMPO_CONDICIONPAGO + " = @condicionDePago, " + LocalDatabase.CAMPO_LISTAPRECIO + " = @listaPrecio, " +
                                LocalDatabase.CAMPO_DIRECCION + " = @direccion, " + LocalDatabase.CAMPO_TELEFONO_CLIENTE + " = @telefono, " +
                                LocalDatabase.CAMPO_PRECIOEMPRESA_ID_CLIENTE + " = @precioEmpresaId, " + LocalDatabase.CAMPO_ZONACLIENTE_ID + " = @zonaClienteId, " +
                                LocalDatabase.CAMPO_AVAL + " = @aval, " + LocalDatabase.CAMPO_REFERENCIA + " = @referencia, " +
                                LocalDatabase.CAMPO_FORMA_COBRO + " = @formaCobro, " + LocalDatabase.CAMPO_FORMA_COBRO_CC + " = @formaCobroCcId, " +
                                LocalDatabase.CAMPO_HISTORIA_CREDITO + " = @historialCredito, " + LocalDatabase.CAMPO_CLAVEQR + " = @claveQr, " +
                                /*ClsLocalDatabase.CAMPO_CLINOVISITADO_CLIENTE + " = @clienteVisitado, " + ClsLocalDatabase.CAMPO_CLINOVISITADO_CLIENTE + " = @clienteNoVisitado, " +
                                ClsLocalDatabase.CAMPO_PAPELERARECICLAJE_CLIENTE + " = @papeleraReciclaje, " + ClsLocalDatabase.CAMPO_PHOTONAME_CLIENTE + " = @photo, " +*/
                                LocalDatabase.CAMPO_RFC_CLIENTE + " = @rfc, " + LocalDatabase.CAMPO_CURP_CLIENTE + " = @curp, " +
                                LocalDatabase.CAMPO_DENOMINACIONCOMCERCIAL_CLIENTE + " = @denominacionComercial, " +
                                "fecha_alta = @fechaAlta, representante_legal = @repLegal, monedaId = @monedaId, " +
                                "descuentoDocto = @descDocto, descuentoMovto = @descMovto, banCredito = @banCredito, " +
                                "clasificacionId1 = @clasificacionId1, clasificacionId2 = @clasificacionId2, " +
                                "clasificacionId3 = @clasificacionId3, clasificacionId4 = @clasificacionId4, " +
                                "clasificacionId5 = @clasificacionId5, clasificacionId6 = @clasificacionId6, " +
                                "tipo = @tipo, status = @status, fechaBaja = @fechaBaja, diasDeCredito = @diasDeCredito, " +
                                "excederCredito = @excederLimiteCredito, descuentoProntoPago = @descuentoProntoPago, " +
                                "diasProntoPago = @diasProntoPago, interesMoratorio = @interesMoratorio, mensajeria = @mensajeria, " +
                                "cuentaMensajeria = @cuentaMensajeria, almacenId = @almacenId, agenteVentaId = @agenteVentaId, " +
                                "agenteCobroId = @agenteCobroId, restriccionAgente = @restriccionAgente, imp1 = @imp1, " +
                                "imp2 = @imp2, imp3 = @imp3, reten1 = @reten1, reten2 = @reten2, " +
                                LocalDatabase.CAMPO_TIPOCONTRIBUYENTE_CLIENTE+" = @tipoContribuyente, "+
                                LocalDatabase.CAMPO_CODIGOREGIMENFISCAL_CLIENTE + " = @regimenFiscal, " +
                                LocalDatabase.CAMPO_CODIGOUSOCFDI_CLIENTE + " = @usoCFDI " +
                                "WHERE " + LocalDatabase.CAMPO_ID_CLIENTE + " = " + cliente.CLIENTE_ID;
                            using (SQLiteCommand command = new SQLiteCommand(query, db))
                            {
                                command.Parameters.AddWithValue("@name", cliente.NOMBRE);
                                command.Parameters.AddWithValue("@code", cliente.CLAVE);
                                command.Parameters.AddWithValue("@creditLimit", cliente.LIMITE_CREDITO);
                                command.Parameters.AddWithValue("@condicionDePago", cliente.CONDICIONPAGO);
                                command.Parameters.AddWithValue("@listaPrecio", cliente.LISTAPRECIO);
                                command.Parameters.AddWithValue("@direccion", cliente.DIRECCION);
                                command.Parameters.AddWithValue("@telefono", cliente.TELEFONO);
                                command.Parameters.AddWithValue("@precioEmpresaId", cliente.PRECIO_EMPRESA_ID);
                                command.Parameters.AddWithValue("@zonaClienteId", cliente.ZONA_CLIENTE_ID);
                                command.Parameters.AddWithValue("@aval", cliente.AVAL);
                                command.Parameters.AddWithValue("@referencia", cliente.REFERENCIA);
                                command.Parameters.AddWithValue("@formaCobro", cliente.FORMA_COBRO);
                                command.Parameters.AddWithValue("@formaCobroCcId", cliente.FORMA_COBRO_CC_ID);
                                command.Parameters.AddWithValue("@historialCredito", cliente.HISTORIA_CREDITO);
                                command.Parameters.AddWithValue("@claveQr", cliente.CLAVEQR);
                                /*command.Parameters.AddWithValue("@clienteVisitado", cliente.CLIVISITADO);
                                command.Parameters.AddWithValue("@clienteNoVisitado", cliente.CLINOVISITADO);
                                command.Parameters.AddWithValue("@papeleraReciclaje", cliente.papelera_reciclaje);
                                command.Parameters.AddWithValue("@photo", cliente.photo);*/
                                command.Parameters.AddWithValue("@rfc", cliente.rfc);
                                command.Parameters.AddWithValue("@curp", cliente.curp);
                                command.Parameters.AddWithValue("@denominacionComercial", cliente.denominacionComercial);
                                command.Parameters.AddWithValue("@fechaAlta", cliente.fechaAlta);
                                command.Parameters.AddWithValue("@repLegal", cliente.representanteLegal);
                                command.Parameters.AddWithValue("@monedaId", cliente.monedaId);
                                command.Parameters.AddWithValue("@descDocto", cliente.descuentoDocumento);
                                command.Parameters.AddWithValue("@descMovto", cliente.descuentoMovimiento);
                                command.Parameters.AddWithValue("@banCredito", cliente.banCredito);
                                command.Parameters.AddWithValue("@clasificacionId1", cliente.clasificacionId1);
                                command.Parameters.AddWithValue("@clasificacionId2", cliente.clasificacionId2);
                                command.Parameters.AddWithValue("@clasificacionId3", cliente.clasificacionId3);
                                command.Parameters.AddWithValue("@clasificacionId4", cliente.clasificacionId4);
                                command.Parameters.AddWithValue("@clasificacionId5", cliente.clasificacionId5);
                                command.Parameters.AddWithValue("@clasificacionId6", cliente.clasificacionId6);
                                command.Parameters.AddWithValue("@tipo", cliente.tipo);
                                command.Parameters.AddWithValue("@status", cliente.status);
                                command.Parameters.AddWithValue("@fechaBaja", cliente.fechaBaja);
                                command.Parameters.AddWithValue("@diasDeCredito", cliente.diasDeCredito);
                                command.Parameters.AddWithValue("@excederLimiteCredito", cliente.excederLimiteCredito);
                                command.Parameters.AddWithValue("@descuentoProntoPago", cliente.descuentoProntoPago);
                                command.Parameters.AddWithValue("@diasProntoPago", cliente.diasProntoPago);
                                command.Parameters.AddWithValue("@interesMoratorio", cliente.interesMoratorio);
                                command.Parameters.AddWithValue("@mensajeria", cliente.mensajeria);
                                command.Parameters.AddWithValue("@cuentaMensajeria", cliente.cuentaMensajeria);
                                command.Parameters.AddWithValue("@almacenId", cliente.almacenId);
                                command.Parameters.AddWithValue("@agenteVentaId", cliente.agenteVentaId);
                                command.Parameters.AddWithValue("@agenteCobroId", cliente.agenteCobroId);
                                command.Parameters.AddWithValue("@restriccionAgente", cliente.restriccionAgente);
                                command.Parameters.AddWithValue("@imp1", cliente.imp1);
                                command.Parameters.AddWithValue("@imp2", cliente.imp2);
                                command.Parameters.AddWithValue("@imp3", cliente.imp3);
                                command.Parameters.AddWithValue("@reten1", cliente.reten1);
                                command.Parameters.AddWithValue("@reten2", cliente.reten2);
                                if (cliente.rfc.Length == 13)
                                    command.Parameters.AddWithValue("@tipoContribuyente", 0);
                                else command.Parameters.AddWithValue("@tipoContribuyente", 1);
                                command.Parameters.AddWithValue("@regimenFiscal", cliente.codigoRegimenFiscal);
                                command.Parameters.AddWithValue("@usoCFDI", cliente.codigoUsoCFDI);
                                int recordUpdated = command.ExecuteNonQuery();
                                if (recordUpdated != 0)
                                    count++;
                            }
                        }
                        else
                        {
                            string query = "INSERT INTO " + LocalDatabase.TABLA_CLIENTES + " VALUES (@id, @name, @code, @creditLimit, " +
                                "@condicionDePago, @listaPrecio, @direccion, @telefono, @precioEmpresaId, @zonaClienteId, @aval, @referencia, " +
                                "@formaCobro, @formaCobroCcId, @historialCredito, @claveQr, @clienteVisitado, @clienteNoVisitado, " +
                                "@papeleraReciclaje, @photo, @rfc, @curp, @denominacionComercial, @fechaAlta, @repLegal, @monedaId, @descDocto, " +
                            "@descMovto, @banCredito, @clasificacionId1, @clasificacionId2, @clasificacionId3, @clasificacionId4, " +
                            "@clasificacionId5, @clasificacionId6, @tipo, @status, @fechaBaja, @diasDeCredito, @excederLimiteCredito, " +
                            "@descuentoProntoPago, @diasProntoPago, @interesMoratorio, @mensajeria, @cuentaMensajeria, @almacenId, " +
                            "@agenteVentaId, @agenteCobroId, @restriccionAgente, @imp1, @imp2, @imp3, @reten1, @reten2, " +
                            "@tipoContribuyente, @regimenFiscal, @usoCFDI)";
                            using (SQLiteCommand command = new SQLiteCommand(query, db))
                            {
                                command.Parameters.AddWithValue("@id", cliente.CLIENTE_ID);
                                command.Parameters.AddWithValue("@name", cliente.NOMBRE);
                                command.Parameters.AddWithValue("@code", cliente.CLAVE);
                                command.Parameters.AddWithValue("@creditLimit", cliente.LIMITE_CREDITO);
                                command.Parameters.AddWithValue("@condicionDePago", cliente.CONDICIONPAGO);
                                command.Parameters.AddWithValue("@listaPrecio", cliente.LISTAPRECIO);
                                command.Parameters.AddWithValue("@direccion", cliente.DIRECCION);
                                command.Parameters.AddWithValue("@telefono", cliente.TELEFONO);
                                command.Parameters.AddWithValue("@precioEmpresaId", cliente.PRECIO_EMPRESA_ID);
                                command.Parameters.AddWithValue("@zonaClienteId", cliente.ZONA_CLIENTE_ID);
                                command.Parameters.AddWithValue("@aval", cliente.AVAL);
                                command.Parameters.AddWithValue("@referencia", cliente.REFERENCIA);
                                command.Parameters.AddWithValue("@formaCobro", cliente.FORMA_COBRO);
                                command.Parameters.AddWithValue("@formaCobroCcId", cliente.FORMA_COBRO_CC_ID);
                                command.Parameters.AddWithValue("@historialCredito", cliente.HISTORIA_CREDITO);
                                command.Parameters.AddWithValue("@claveQr", cliente.CLAVEQR);
                                command.Parameters.AddWithValue("@clienteVisitado", 0);
                                command.Parameters.AddWithValue("@clienteNoVisitado", 0);
                                command.Parameters.AddWithValue("@papeleraReciclaje", 0);
                                command.Parameters.AddWithValue("@photo", "");
                                command.Parameters.AddWithValue("@rfc", cliente.rfc);
                                command.Parameters.AddWithValue("@curp", cliente.curp);
                                command.Parameters.AddWithValue("@denominacionComercial", cliente.denominacionComercial);
                                command.Parameters.AddWithValue("@fechaAlta", cliente.fechaAlta);
                                command.Parameters.AddWithValue("@repLegal", cliente.representanteLegal);
                                command.Parameters.AddWithValue("@monedaId", cliente.monedaId);
                                command.Parameters.AddWithValue("@descDocto", cliente.descuentoDocumento);
                                command.Parameters.AddWithValue("@descMovto", cliente.descuentoMovimiento);
                                command.Parameters.AddWithValue("@banCredito", cliente.banCredito);
                                command.Parameters.AddWithValue("@clasificacionId1", cliente.clasificacionId1);
                                command.Parameters.AddWithValue("@clasificacionId2", cliente.clasificacionId2);
                                command.Parameters.AddWithValue("@clasificacionId3", cliente.clasificacionId3);
                                command.Parameters.AddWithValue("@clasificacionId4", cliente.clasificacionId4);
                                command.Parameters.AddWithValue("@clasificacionId5", cliente.clasificacionId5);
                                command.Parameters.AddWithValue("@clasificacionId6", cliente.clasificacionId6);
                                command.Parameters.AddWithValue("@tipo", cliente.tipo);
                                command.Parameters.AddWithValue("@status", cliente.status);
                                command.Parameters.AddWithValue("@fechaBaja", cliente.fechaBaja);
                                command.Parameters.AddWithValue("@diasDeCredito", cliente.diasDeCredito);
                                command.Parameters.AddWithValue("@excederLimiteCredito", cliente.excederLimiteCredito);
                                command.Parameters.AddWithValue("@descuentoProntoPago", cliente.descuentoProntoPago);
                                command.Parameters.AddWithValue("@diasProntoPago", cliente.diasProntoPago);
                                command.Parameters.AddWithValue("@interesMoratorio", cliente.interesMoratorio);
                                command.Parameters.AddWithValue("@mensajeria", cliente.mensajeria);
                                command.Parameters.AddWithValue("@cuentaMensajeria", cliente.cuentaMensajeria);
                                command.Parameters.AddWithValue("@almacenId", cliente.almacenId);
                                command.Parameters.AddWithValue("@agenteVentaId", cliente.agenteVentaId);
                                command.Parameters.AddWithValue("@agenteCobroId", cliente.agenteCobroId);
                                command.Parameters.AddWithValue("@restriccionAgente", cliente.restriccionAgente);
                                command.Parameters.AddWithValue("@imp1", cliente.imp1);
                                command.Parameters.AddWithValue("@imp2", cliente.imp2);
                                command.Parameters.AddWithValue("@imp3", cliente.imp3);
                                command.Parameters.AddWithValue("@reten1", cliente.reten1);
                                command.Parameters.AddWithValue("@reten2", cliente.reten2);
                                if (cliente.rfc.Length == 13)
                                    command.Parameters.AddWithValue("@tipoContribuyente", 0);
                                else command.Parameters.AddWithValue("@tipoContribuyente", 1);
                                command.Parameters.AddWithValue("@regimenFiscal", cliente.codigoRegimenFiscal);
                                command.Parameters.AddWithValue("@usoCFDI", cliente.codigoUsoCFDI);
                                int recordInserted = command.ExecuteNonQuery();
                                if (recordInserted != 0)
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
                    if (db != null && db.State == ConnectionState.Open)
                        db.Close();
                }
            }
            return count;
        }

        public static int updateCustomer(ClsClienteModel cliente)
        {
            int idInserted = 0;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                string query = "UPDATE " + LocalDatabase.TABLA_CLIENTES + " SET " + LocalDatabase.CAMPO_NOMBRECLIENTE + " = @name, " +
                                LocalDatabase.CAMPO_CLAVECLIENTE + " = @code, " + LocalDatabase.CAMPO_LIMITECREDITO_CLIENTE + " = @creditLimit, " +
                                LocalDatabase.CAMPO_CONDICIONPAGO + " = @condicionDePago, " + LocalDatabase.CAMPO_LISTAPRECIO + " = @listaPrecio, " +
                                LocalDatabase.CAMPO_DIRECCION + " = @direccion, " + LocalDatabase.CAMPO_TELEFONO_CLIENTE + " = @telefono, " +
                                LocalDatabase.CAMPO_PRECIOEMPRESA_ID_CLIENTE + " = @precioEmpresaId, " + LocalDatabase.CAMPO_ZONACLIENTE_ID + " = @zonaClienteId, " +
                                LocalDatabase.CAMPO_AVAL + " = @aval, " + LocalDatabase.CAMPO_REFERENCIA + " = @referencia, " +
                                LocalDatabase.CAMPO_FORMA_COBRO + " = @formaCobro, " + LocalDatabase.CAMPO_FORMA_COBRO_CC + " = @formaCobroCcId, " +
                                LocalDatabase.CAMPO_HISTORIA_CREDITO + " = @historialCredito, " + LocalDatabase.CAMPO_CLAVEQR + " = @claveQr, " +
                                /*ClsLocalDatabase.CAMPO_CLINOVISITADO_CLIENTE + " = @clienteVisitado, " + ClsLocalDatabase.CAMPO_CLINOVISITADO_CLIENTE + " = @clienteNoVisitado, " +
                                ClsLocalDatabase.CAMPO_PAPELERARECICLAJE_CLIENTE + " = @papeleraReciclaje, " + ClsLocalDatabase.CAMPO_PHOTONAME_CLIENTE + " = @photo, " +*/
                                LocalDatabase.CAMPO_RFC_CLIENTE + " = @rfc, " + LocalDatabase.CAMPO_CURP_CLIENTE + " = @curp, " +
                                LocalDatabase.CAMPO_DENOMINACIONCOMCERCIAL_CLIENTE + " = @denominacionComercial, " +
                                LocalDatabase.CAMPO_TIPOCONTRIBUYENTE_CLIENTE + " = @tipoContribuyente, " +
                                LocalDatabase.CAMPO_CODIGOREGIMENFISCAL_CLIENTE + " = @regimenFiscal, " +
                                LocalDatabase.CAMPO_CODIGOUSOCFDI_CLIENTE + " = @usoCFDI " +
                                "WHERE " + LocalDatabase.CAMPO_ID_CLIENTE + " = " + cliente.CLIENTE_ID;
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    command.Parameters.AddWithValue("@name", cliente.NOMBRE);
                    command.Parameters.AddWithValue("@code", cliente.CLAVE);
                    command.Parameters.AddWithValue("@creditLimit", cliente.LIMITE_CREDITO);
                    command.Parameters.AddWithValue("@condicionDePago", cliente.CONDICIONPAGO);
                    command.Parameters.AddWithValue("@listaPrecio", cliente.LISTAPRECIO);
                    command.Parameters.AddWithValue("@direccion", cliente.DIRECCION);
                    command.Parameters.AddWithValue("@telefono", cliente.TELEFONO);
                    command.Parameters.AddWithValue("@precioEmpresaId", cliente.PRECIO_EMPRESA_ID);
                    command.Parameters.AddWithValue("@zonaClienteId", cliente.ZONA_CLIENTE_ID);
                    command.Parameters.AddWithValue("@aval", cliente.AVAL);
                    command.Parameters.AddWithValue("@referencia", cliente.REFERENCIA);
                    command.Parameters.AddWithValue("@formaCobro", cliente.FORMA_COBRO);
                    command.Parameters.AddWithValue("@formaCobroCcId", cliente.FORMA_COBRO_CC_ID);
                    command.Parameters.AddWithValue("@historialCredito", cliente.HISTORIA_CREDITO);
                    command.Parameters.AddWithValue("@claveQr", cliente.CLAVEQR);
                    /*command.Parameters.AddWithValue("@clienteVisitado", cliente.CLIVISITADO);
                    command.Parameters.AddWithValue("@clienteNoVisitado", cliente.CLINOVISITADO);
                    command.Parameters.AddWithValue("@papeleraReciclaje", cliente.papelera_reciclaje);
                    command.Parameters.AddWithValue("@photo", cliente.photo);*/
                    command.Parameters.AddWithValue("@rfc", cliente.rfc);
                    command.Parameters.AddWithValue("@curp", cliente.curp);
                    command.Parameters.AddWithValue("@denominacionComercial", cliente.denominacionComercial);
                    if (cliente.rfc.Length == 13)
                        command.Parameters.AddWithValue("@tipoContribuyente", 0);
                    else command.Parameters.AddWithValue("@tipoContribuyente", 1);
                    command.Parameters.AddWithValue("@regimenFiscal", cliente.codigoRegimenFiscal);
                    command.Parameters.AddWithValue("@usoCFDI", cliente.codigoUsoCFDI);
                    int recordUpdated = command.ExecuteNonQuery();
                    if (recordUpdated != 0)
                        idInserted = cliente.CLIENTE_ID;
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
            return idInserted;
        }

        public static int updateCustomer(SQLiteConnection db, ClsClienteModel cliente)
        {
            int count = 0;
            try
            {
                string query = "UPDATE " + LocalDatabase.TABLA_CLIENTES + " SET " + LocalDatabase.CAMPO_NOMBRECLIENTE + " = @name, " +
                                LocalDatabase.CAMPO_CLAVECLIENTE + " = @code, " + LocalDatabase.CAMPO_LIMITECREDITO_CLIENTE + " = @creditLimit, " +
                                LocalDatabase.CAMPO_CONDICIONPAGO + " = @condicionDePago, " + LocalDatabase.CAMPO_LISTAPRECIO + " = @listaPrecio, " +
                                LocalDatabase.CAMPO_DIRECCION + " = @direccion, " + LocalDatabase.CAMPO_TELEFONO_CLIENTE + " = @telefono, " +
                                LocalDatabase.CAMPO_PRECIOEMPRESA_ID_CLIENTE + " = @precioEmpresaId, " + LocalDatabase.CAMPO_ZONACLIENTE_ID + " = @zonaClienteId, " +
                                LocalDatabase.CAMPO_AVAL + " = @aval, " + LocalDatabase.CAMPO_REFERENCIA + " = @referencia, " +
                                LocalDatabase.CAMPO_FORMA_COBRO + " = @formaCobro, " + LocalDatabase.CAMPO_FORMA_COBRO_CC + " = @formaCobroCcId, " +
                                LocalDatabase.CAMPO_HISTORIA_CREDITO + " = @historialCredito, " + LocalDatabase.CAMPO_CLAVEQR + " = @claveQr, " +
                                LocalDatabase.CAMPO_RFC_CLIENTE + " = @rfc, " + LocalDatabase.CAMPO_CURP_CLIENTE + " = @curp, " +
                                LocalDatabase.CAMPO_DENOMINACIONCOMCERCIAL_CLIENTE + " = @denominacionComercial, " +
                                "fecha_alta = @fechaAlta, representante_legal = @repLegal, monedaId = @monedaId, " +
                                "descuentoDocto = @descDocto, descuentoMovto = @descMovto, banCredito = @banCredito, " +
                                "clasificacionId1 = @clasificacionId1, clasificacionId2 = @clasificacionId2, " +
                                "clasificacionId3 = @clasificacionId3, clasificacionId4 = @clasificacionId4, " +
                                "clasificacionId5 = @clasificacionId5, clasificacionId6 = @clasificacionId6, " +
                                "tipo = @tipo, status = @status, fechaBaja = @fechaBaja, diasDeCredito = @diasDeCredito, " +
                                "excederCredito = @excederLimiteCredito, descuentoProntoPago = @descuentoProntoPago, " +
                                "diasProntoPago = @diasProntoPago, interesMoratorio = @interesMoratorio, mensajeria = @mensajeria, " +
                                "cuentaMensajeria = @cuentaMensajeria, almacenId = @almacenId, agenteVentaId = @agenteVentaId, " +
                                "agenteCobroId = @agenteCobroId, restriccionAgente = @restriccionAgente, imp1 = @imp1, " +
                                "imp2 = @imp2, imp3 = @imp3, reten1 = @reten1, reten2 = @reten2, " +
                                LocalDatabase.CAMPO_TIPOCONTRIBUYENTE_CLIENTE+" = @tipoContribuyente, "+
                                LocalDatabase.CAMPO_CODIGOREGIMENFISCAL_CLIENTE + " = @regimenFiscal, " +
                                LocalDatabase.CAMPO_CODIGOUSOCFDI_CLIENTE + " = @usoCFDI " +
                                "WHERE " + LocalDatabase.CAMPO_ID_CLIENTE + " = " + cliente.CLIENTE_ID;
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    command.Parameters.AddWithValue("@name", cliente.NOMBRE);
                    command.Parameters.AddWithValue("@code", cliente.CLAVE);
                    command.Parameters.AddWithValue("@creditLimit", cliente.LIMITE_CREDITO);
                    command.Parameters.AddWithValue("@condicionDePago", cliente.CONDICIONPAGO);
                    command.Parameters.AddWithValue("@listaPrecio", cliente.LISTAPRECIO);
                    command.Parameters.AddWithValue("@direccion", cliente.DIRECCION);
                    command.Parameters.AddWithValue("@telefono", cliente.TELEFONO);
                    command.Parameters.AddWithValue("@precioEmpresaId", cliente.PRECIO_EMPRESA_ID);
                    command.Parameters.AddWithValue("@zonaClienteId", cliente.ZONA_CLIENTE_ID);
                    command.Parameters.AddWithValue("@aval", cliente.AVAL);
                    command.Parameters.AddWithValue("@referencia", cliente.REFERENCIA);
                    command.Parameters.AddWithValue("@formaCobro", cliente.FORMA_COBRO);
                    command.Parameters.AddWithValue("@formaCobroCcId", cliente.FORMA_COBRO_CC_ID);
                    command.Parameters.AddWithValue("@historialCredito", cliente.HISTORIA_CREDITO);
                    command.Parameters.AddWithValue("@claveQr", cliente.CLAVEQR);
                    /*command.Parameters.AddWithValue("@clienteVisitado", cliente.CLIVISITADO);
                    command.Parameters.AddWithValue("@clienteNoVisitado", cliente.CLINOVISITADO);
                    command.Parameters.AddWithValue("@papeleraReciclaje", cliente.papelera_reciclaje);
                    command.Parameters.AddWithValue("@photo", cliente.photo);*/
                    command.Parameters.AddWithValue("@rfc", cliente.rfc);
                    command.Parameters.AddWithValue("@curp", cliente.curp);
                    command.Parameters.AddWithValue("@denominacionComercial", cliente.denominacionComercial);
                    command.Parameters.AddWithValue("@fechaAlta", cliente.fechaAlta);
                    command.Parameters.AddWithValue("@repLegal", cliente.representanteLegal);
                    command.Parameters.AddWithValue("@monedaId", cliente.monedaId);
                    command.Parameters.AddWithValue("@descDocto", cliente.descuentoDocumento);
                    command.Parameters.AddWithValue("@descMovto", cliente.descuentoMovimiento);
                    command.Parameters.AddWithValue("@banCredito", cliente.banCredito);
                    command.Parameters.AddWithValue("@clasificacionId1", cliente.clasificacionId1);
                    command.Parameters.AddWithValue("@clasificacionId2", cliente.clasificacionId2);
                    command.Parameters.AddWithValue("@clasificacionId3", cliente.clasificacionId3);
                    command.Parameters.AddWithValue("@clasificacionId4", cliente.clasificacionId4);
                    command.Parameters.AddWithValue("@clasificacionId5", cliente.clasificacionId5);
                    command.Parameters.AddWithValue("@clasificacionId6", cliente.clasificacionId6);
                    command.Parameters.AddWithValue("@tipo", cliente.tipo);
                    command.Parameters.AddWithValue("@status", cliente.status);
                    command.Parameters.AddWithValue("@fechaBaja", cliente.fechaBaja);
                    command.Parameters.AddWithValue("@diasDeCredito", cliente.diasDeCredito);
                    command.Parameters.AddWithValue("@excederLimiteCredito", cliente.excederLimiteCredito);
                    command.Parameters.AddWithValue("@descuentoProntoPago", cliente.descuentoProntoPago);
                    command.Parameters.AddWithValue("@diasProntoPago", cliente.diasProntoPago);
                    command.Parameters.AddWithValue("@interesMoratorio", cliente.interesMoratorio);
                    command.Parameters.AddWithValue("@mensajeria", cliente.mensajeria);
                    command.Parameters.AddWithValue("@cuentaMensajeria", cliente.cuentaMensajeria);
                    command.Parameters.AddWithValue("@almacenId", cliente.almacenId);
                    command.Parameters.AddWithValue("@agenteVentaId", cliente.agenteVentaId);
                    command.Parameters.AddWithValue("@agenteCobroId", cliente.agenteCobroId);
                    command.Parameters.AddWithValue("@restriccionAgente", cliente.restriccionAgente);
                    command.Parameters.AddWithValue("@imp1", cliente.imp1);
                    command.Parameters.AddWithValue("@imp2", cliente.imp2);
                    command.Parameters.AddWithValue("@imp3", cliente.imp3);
                    command.Parameters.AddWithValue("@reten1", cliente.reten1);
                    command.Parameters.AddWithValue("@reten2", cliente.reten2);
                    if (cliente.rfc.Length == 13)
                        command.Parameters.AddWithValue("@tipoContribuyente", 0);
                    else command.Parameters.AddWithValue("@tipoContribuyente", 1);
                    command.Parameters.AddWithValue("@regimenFiscal", cliente.codigoRegimenFiscal);
                    command.Parameters.AddWithValue("@usoCFDI", cliente.codigoUsoCFDI);
                    int recordUpdated = command.ExecuteNonQuery();
                    if (recordUpdated != 0)
                        count++;
                }
            }
            catch (SQLiteException e)
            {
                SECUDOC.writeLog(e.ToString());
            }
            finally
            {
                
            }
            return count;
        }

        public static int updateClaveClienteAdditionalAgregado(int id)
        {
            int resp = 0;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "UPDATE "+ LocalDatabase.TABLA_CLIENTES+" SET "+LocalDatabase.CAMPO_CLAVECLIENTE+" = @codigo WHERE "+
                    LocalDatabase.CAMPO_ID_CLIENTE + " = @idCliente";
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    command.Parameters.AddWithValue("@codigo", "CADCL" + Math.Abs(id));
                    command.Parameters.AddWithValue("@idCliente", id);
                    int records = command.ExecuteNonQuery();
                    if (records > 0)
                        resp = 1;
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

        public static List<ClsClienteModel> getAllCustomers(String query)
        {
            List<ClsClienteModel> customersList = null;
            ClsClienteModel clientes;
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
                            customersList = new List<ClsClienteModel>();
                            while (reader.Read())
                            {
                                clientes = new ClsClienteModel();
                                clientes.CLIENTE_ID = Convert.ToInt32(reader[LocalDatabase.CAMPO_ID_CLIENTE].ToString().Trim());
                                clientes.NOMBRE = reader[LocalDatabase.CAMPO_NOMBRECLIENTE].ToString().Trim();
                                clientes.CLAVE = reader[LocalDatabase.CAMPO_CLAVECLIENTE].ToString().Trim();
                                clientes.rfc = reader[LocalDatabase.CAMPO_RFC_CLIENTE].ToString().Trim();
                                clientes.LIMITE_CREDITO = Convert.ToDouble(reader[LocalDatabase.CAMPO_LIMITECREDITO_CLIENTE].ToString().Trim());
                                clientes.TELEFONO = reader[LocalDatabase.CAMPO_TELEFONO_CLIENTE].ToString().Trim();
                                clientes.HISTORIA_CREDITO = reader[LocalDatabase.CAMPO_HISTORIA_CREDITO].ToString().Trim();
                                clientes.PRECIO_EMPRESA_ID = Convert.ToInt32(reader[LocalDatabase.CAMPO_PRECIOEMPRESA_ID_CLIENTE].ToString().Trim());
                                clientes.denominacionComercial = reader[LocalDatabase.CAMPO_DENOMINACIONCOMCERCIAL_CLIENTE].ToString().Trim();
                                customersList.Add(clientes);
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
            return customersList;
        }


        public static List<ClsClienteModel> getAllClientesADC(String query)
        {
            List<ClsClienteModel> customersList = null;
            ClsClienteModel clientes;
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
                            customersList = new List<ClsClienteModel>();
                            while (reader.Read())
                            {
                                clientes = new ClsClienteModel();
                                clientes.CLIENTE_ID = Convert.ToInt32(reader[LocalDatabase.CAMPO_ID_CLIENTE].ToString().Trim());
                                clientes.NOMBRE = reader[LocalDatabase.CAMPO_NOMBRECLIENTE].ToString().Trim();
                                clientes.CLAVE = reader[LocalDatabase.CAMPO_CLAVECLIENTE].ToString().Trim();
                                clientes.rfc = reader[LocalDatabase.CAMPO_RFC_CLIENTE].ToString().Trim();
                                clientes.LIMITE_CREDITO = Convert.ToDouble(reader[LocalDatabase.CAMPO_LIMITECREDITO_CLIENTE].ToString().Trim());
                                clientes.TELEFONO = reader[LocalDatabase.CAMPO_TELEFONO_CLIENTE].ToString().Trim();
                                clientes.HISTORIA_CREDITO = reader[LocalDatabase.CAMPO_HISTORIA_CREDITO].ToString().Trim();
                                clientes.PRECIO_EMPRESA_ID = Convert.ToInt32(reader[LocalDatabase.CAMPO_PRECIOEMPRESA_ID_CLIENTE].ToString().Trim());
                                clientes.denominacionComercial = reader[LocalDatabase.CAMPO_DENOMINACIONCOMCERCIAL_CLIENTE].ToString().Trim();
                                customersList.Add(clientes);
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
            return customersList;
        }

        public static List<ClsClienteModel> getAllCustomersWithParameters(String query, String parameterName, String parameterValue, int withValues)
        {
            List<ClsClienteModel> customersList = null;
            ClsClienteModel clientes;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    if (withValues == 1)
                        command.Parameters.AddWithValue("@" + parameterName, "%" + parameterValue + "%");
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            customersList = new List<ClsClienteModel>();
                            while (reader.Read())
                            {
                                clientes = new ClsClienteModel();
                                clientes.CLIENTE_ID = Convert.ToInt32(reader[LocalDatabase.CAMPO_ID_CLIENTE].ToString().Trim());
                                clientes.NOMBRE = reader[LocalDatabase.CAMPO_NOMBRECLIENTE].ToString().Trim();
                                clientes.CLAVE = reader[LocalDatabase.CAMPO_CLAVECLIENTE].ToString().Trim();
                                clientes.rfc = reader[LocalDatabase.CAMPO_RFC_CLIENTE].ToString().Trim();
                                clientes.LIMITE_CREDITO = Convert.ToDouble(reader[LocalDatabase.CAMPO_LIMITECREDITO_CLIENTE].ToString().Trim());
                                clientes.TELEFONO = reader[LocalDatabase.CAMPO_TELEFONO_CLIENTE].ToString().Trim();
                                clientes.HISTORIA_CREDITO = reader[LocalDatabase.CAMPO_HISTORIA_CREDITO].ToString().Trim();
                                clientes.PRECIO_EMPRESA_ID = Convert.ToInt32(reader[LocalDatabase.CAMPO_PRECIOEMPRESA_ID_CLIENTE].ToString().Trim());
                                clientes.denominacionComercial = reader[LocalDatabase.CAMPO_DENOMINACIONCOMCERCIAL_CLIENTE].ToString().Trim();
                                customersList.Add(clientes);
                            }
                        }
                        if (reader != null && !reader.IsClosed)
                            reader.Close();
                    }
                }
            }
            catch (SQLiteException Ex)
            {
                SECUDOC.writeLog(Ex.ToString());
            }
            finally
            {
                if (db != null && db.State == ConnectionState.Open)
                    db.Close();
            }
            return customersList;
        }

        public static DataTable getAllCustomersDt(String query)
        {
            DataTable dt = null;
            var db = new SQLiteConnection(ClsSQLiteDbHelper.instanceSQLite);
            db.Open();
            try
            {
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    SQLiteDataAdapter adapter = new SQLiteDataAdapter();
                    adapter.SelectCommand = command;
                    dt = new DataTable();
                    adapter.Fill(dt);
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
            return dt;
        }

        public static List<ClsClienteModel> getAllCustomerInAZone(int zoneId)
        {
            List<ClsClienteModel> customersList = null;
            var db = new SQLiteConnection(ClsSQLiteDbHelper.instanceSQLite);
            db.Open();
            try
            {
                String query = "SELECT * FROM " + LocalDatabase.TABLA_ZONACLIENTES + " ZC LEFT JOIN " + LocalDatabase.TABLA_CLIENTES + " C " +
                    "ON C." + LocalDatabase.CAMPO_ZONACLIENTE_ID + " = ZC." + LocalDatabase.CAMPO_ZONA_CLIENTE +
                    " WHERE ZC." + LocalDatabase.CAMPO_ZONA_CLIENTE + " = @clasificationId";
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    command.Parameters.AddWithValue("@clasificationId", zoneId);
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            customersList = new List<ClsClienteModel>();
                            while (reader.Read())
                            {
                                ClsClienteModel clientes = new ClsClienteModel();
                                clientes.CLIENTE_ID = Convert.ToInt32(reader[LocalDatabase.CAMPO_ID_CLIENTE].ToString().Trim());
                                clientes.NOMBRE = reader[LocalDatabase.CAMPO_NOMBRECLIENTE].ToString().Trim();
                                clientes.CLAVE = reader[LocalDatabase.CAMPO_CLAVECLIENTE].ToString().Trim();
                                clientes.TELEFONO = reader[LocalDatabase.CAMPO_TELEFONO_CLIENTE].ToString().Trim();
                                clientes.LIMITE_CREDITO = Convert.ToDouble(reader[LocalDatabase.CAMPO_LIMITECREDITO_CLIENTE].ToString().Trim());
                                clientes.HISTORIA_CREDITO = reader[LocalDatabase.CAMPO_HISTORIA_CREDITO].ToString().Trim();
                                customersList.Add(clientes);
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
            return customersList;
        }

        public static ClsClienteModel getAllDataFromACustomer(int idCustomer)
        {
            ClsClienteModel cliente = null;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "SELECT * FROM " + LocalDatabase.TABLA_CLIENTES + " WHERE " + LocalDatabase.CAMPO_ID_CLIENTE + " = @idCustomer";
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    command.Parameters.AddWithValue("@idCustomer", idCustomer);
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                cliente = new ClsClienteModel();
                                cliente.CLIENTE_ID = Convert.ToInt32(reader[LocalDatabase.CAMPO_ID_CLIENTE].ToString().Trim());
                                cliente.NOMBRE = reader[LocalDatabase.CAMPO_NOMBRECLIENTE].ToString().Trim();
                                cliente.CLAVE = reader[LocalDatabase.CAMPO_CLAVECLIENTE].ToString().Trim();
                                cliente.LIMITE_CREDITO = Convert.ToDouble(reader[LocalDatabase.CAMPO_LIMITECREDITO_CLIENTE].ToString().Trim());
                                cliente.CONDICIONPAGO = reader[LocalDatabase.CAMPO_CONDICIONPAGO].ToString();
                                if (reader[LocalDatabase.CAMPO_LISTAPRECIO] != DBNull.Value && !reader[LocalDatabase.CAMPO_LISTAPRECIO].Equals(""))
                                    cliente.LISTAPRECIO = Convert.ToInt32(reader[LocalDatabase.CAMPO_LISTAPRECIO].ToString().Trim());
                                else cliente.LISTAPRECIO = 1;
                                cliente.DIRECCION = reader[LocalDatabase.CAMPO_DIRECCION].ToString().Trim();
                                cliente.TELEFONO = reader[LocalDatabase.CAMPO_TELEFONO_CLIENTE].ToString().Trim();
                                if (reader[LocalDatabase.CAMPO_PRECIOEMPRESA_ID_CLIENTE] != DBNull.Value && !reader[LocalDatabase.CAMPO_PRECIOEMPRESA_ID_CLIENTE].Equals(""))
                                    cliente.PRECIO_EMPRESA_ID = Convert.ToInt32(reader[LocalDatabase.CAMPO_PRECIOEMPRESA_ID_CLIENTE].ToString());
                                else cliente.PRECIO_EMPRESA_ID = 1;
                                cliente.ZONA_CLIENTE_ID = Convert.ToInt32(reader[LocalDatabase.CAMPO_ZONACLIENTE_ID].ToString());
                                cliente.AVAL = reader[LocalDatabase.CAMPO_AVAL].ToString();
                                cliente.REFERENCIA = reader[LocalDatabase.CAMPO_REFERENCIA].ToString();
                                cliente.FORMA_COBRO = reader[LocalDatabase.CAMPO_FORMA_COBRO].ToString();
                                cliente.FORMA_COBRO_CC_ID = Convert.ToInt32(reader[LocalDatabase.CAMPO_FORMA_COBRO_CC].ToString());
                                cliente.HISTORIA_CREDITO = reader[LocalDatabase.CAMPO_HISTORIA_CREDITO].ToString().Trim();
                                cliente.CLAVEQR = reader[LocalDatabase.CAMPO_CLAVEQR].ToString();
                                cliente.CLIVISITADO = reader[LocalDatabase.CAMPO_CLINOVISITADO_CLIENTE].ToString();
                                cliente.denominacionComercial = reader[LocalDatabase.CAMPO_DENOMINACIONCOMCERCIAL_CLIENTE].ToString().Trim();
                                cliente.representanteLegal = reader[LocalDatabase.CAMPO_REPRELEGAL_CLIENTE].ToString().Trim();
                                cliente.descuentoMovimiento = Convert.ToDouble(reader[LocalDatabase.CAMPO_DESCMOVTO_CLIENTE].ToString().Trim());
                            }
                        }
                        if (reader != null && !reader.IsClosed)
                            reader.Close();
                    }
                }
            }
            catch (SQLiteException Ex)
            {
                SECUDOC.writeLog(Ex.ToString());
            }
            finally
            {
                if (db != null && db.State == ConnectionState.Open)
                    db.Close();
            }
            return cliente;
        }

        public static String getAStringValueFromACustomer(String query)
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

        public static String getName(int idCustomer)
        {
            String value = "";
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "SELECT " + LocalDatabase.CAMPO_NOMBRECLIENTE + " FROM " + LocalDatabase.TABLA_CLIENTES + " WHERE " +
                    LocalDatabase.CAMPO_ID_CLIENTE + " = @idCustomer";
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    command.Parameters.AddWithValue("@idCustomer", idCustomer);
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                if (reader.GetValue(0) != DBNull.Value)
                                    value = reader.GetValue(0).ToString().Trim();
                            }
                        }
                        if (reader != null && !reader.IsClosed)
                            reader.Close();
                    }
                }
            }
            catch (SQLiteException Ex)
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

        public static int getLastId()
        {
            int lastId = 0;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "SELECT MAX(" + LocalDatabase.CAMPO_ID_CLIENTE + ") FROM " + LocalDatabase.TABLA_CLIENTES + " LIMIT 1";
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                if (reader.GetValue(0) != DBNull.Value)
                                    lastId = Convert.ToInt32(reader.GetValue(0).ToString().Trim());
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
            return lastId;
        }

        public static String getName(SQLiteConnection db, int idCustomer)
        {
            String value = "";
            try
            {
                String query = "SELECT " + LocalDatabase.CAMPO_NOMBRECLIENTE + " FROM " + LocalDatabase.TABLA_CLIENTES + " WHERE " +
                    LocalDatabase.CAMPO_ID_CLIENTE + " = @idCustomer";
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    command.Parameters.AddWithValue("@idCustomer", idCustomer);
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                if (reader.GetValue(0) != DBNull.Value)
                                    value = reader.GetValue(0).ToString().Trim();
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

            }
            return value;
        }

        public static Boolean checkIfTheClientExists(int idCliente)
        {
            bool exist = false;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "SELECT * FROM " + LocalDatabase.TABLA_CLIENTES + " WHERE " + LocalDatabase.CAMPO_ID_CLIENTE + " = " + idCliente;
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                            exist = true;
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

        public static Boolean checkIfTheClientExists(SQLiteConnection db, int idCliente)
        {
            bool exist = false;
            try
            {
                String query = "SELECT * FROM " + LocalDatabase.TABLA_CLIENTES + " WHERE " + LocalDatabase.CAMPO_ID_CLIENTE + " = " + idCliente;
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                            exist = true;
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
                
            }
            return exist;
        }

        public static int getIntValueFromACustomer(String query)
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

        public static int getTotalRecordsWithParameters(String query, String parameterName, String parameterValue, int withValues)
        {
            int value = 0;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    if (withValues == 1)
                        command.Parameters.AddWithValue("@"+parameterName, "%"+parameterValue+"%");
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
            catch (SQLiteException Ex)
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

        public static String getClaveForAClient(int id)
        {
            String clave = "";
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "SELECT * FROM " + LocalDatabase.TABLA_CLIENTES + " WHERE " + LocalDatabase.CAMPO_ID_CLIENTE + " = " + id;
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                clave = reader.GetString(2);
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
            return clave;
        }

        public static double getAvailableBalance(int idCustomer)
        {
            double saldoDisponible = 0;
            double saldoPendiente = CuentasXCobrarModel.getCurrentCustomerBalance(idCustomer);
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "SELECT " + LocalDatabase.CAMPO_LIMITECREDITO_CLIENTE + " FROM " + LocalDatabase.TABLA_CLIENTES +
                        " WHERE " + LocalDatabase.CAMPO_ID_CLIENTE + " = " + idCustomer;
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                double limite = Convert.ToDouble(reader[LocalDatabase.CAMPO_LIMITECREDITO_CLIENTE].ToString().Trim());
                                if (limite > 0)
                                    saldoDisponible = limite - saldoPendiente;
                                else saldoDisponible = -1;
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
            return saldoDisponible;
        }

        public static String getClaveForAClientwithDb(SQLiteConnection db, int id)
        {
            String clave = "";
            try
            {
                String query = "SELECT * FROM " + LocalDatabase.TABLA_CLIENTES + " WHERE " + LocalDatabase.CAMPO_ID_CLIENTE + " = " + id;
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                clave = reader.GetString(2);
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
            }
            return clave;
        }

        public static double getDescuentoDelCliente(int idCustomer)
        {
            double discount = 0;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "SELECT "+LocalDatabase.CAMPO_DESCMOVTO_CLIENTE+" FROM " + LocalDatabase.TABLA_CLIENTES + 
                    " WHERE " + LocalDatabase.CAMPO_ID_CLIENTE + " = " + idCustomer;
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                if (reader.GetValue(0) != DBNull.Value)
                                    discount = Convert.ToDouble(reader.GetValue(0).ToString().Trim());
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
            return discount;
        }

        public static bool deleteACustomer(int idCustomer)
        {
            bool deleted = false;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "DELETE FROM " + LocalDatabase.TABLA_CLIENTES+" WHERE "+LocalDatabase.CAMPO_ID_CLIENTE+" = @idCustomer";
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    command.Parameters.AddWithValue("@idCustomer", idCustomer);
                    int deletedRecords = command.ExecuteNonQuery();
                    if (deletedRecords > 0)
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

        public static bool deleteACustomer(SQLiteConnection db, int idCustomer)
        {
            bool deleted = false;
            try
            {
                String query = "DELETE FROM " + LocalDatabase.TABLA_CLIENTES + " WHERE " + LocalDatabase.CAMPO_ID_CLIENTE + " = @idCustomer";
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    command.Parameters.AddWithValue("@idCustomer", idCustomer);
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
                
            }
            return deleted;
        }

        public static bool deleteAllCustomersInLocalDbWithParameters(String parameterValue)
        {
            bool deleted = false;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "DELETE FROM " + LocalDatabase.TABLA_CLIENTES+" WHERE "+
                    LocalDatabase.CAMPO_CLAVECLIENTE+" LIKE @parameterName1 OR "+LocalDatabase.CAMPO_NOMBRECLIENTE+" LIKE @parameterName2";
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    command.Parameters.AddWithValue("@parameterName1", "%"+ parameterValue+"%");
                    command.Parameters.AddWithValue("@parameterName2", "%"+ parameterValue+"%");
                    int deletedRecords = command.ExecuteNonQuery();
                    deleted = true;
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
            return deleted;
        }

        public static bool deleteAllNewCustomersUploadedToComercial()
        {
            bool deleted = false;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "DELETE FROM " + LocalDatabase.TABLA_CLIENTES+" WHERE "+LocalDatabase.CAMPO_ID_CLIENTE+" < 0";
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    int deletedRecords = command.ExecuteNonQuery();
                    if (deletedRecords > 0)
                        deleted = true;
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
            return deleted;
        }

        public static bool deleteAllCustomersInLocalDb()
        {
            bool deleted = false;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "DELETE FROM " + LocalDatabase.TABLA_CLIENTES;
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    int deletedRecords = command.ExecuteNonQuery();
                    deleted = true;
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
            return deleted;
        }
    }
}
