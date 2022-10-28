using SyncTPV.Helpers.SqliteDatabaseHelper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;

namespace SyncTPV.Models
{
    public class CustomerADCModel
    {
        public int id { get; set; }
        public String nombre { get; set; }
        public int zona { get; set; }
        public String calle { get; set; }
        public String numero { get; set; }
        public String colonia { get; set; }
        public String poblacion { get; set; }
        public int ciudad { get; set; }
        public int estado { get; set; }
        public String referencia { get; set; }
        public String telefono { get; set; }
        public String cp { get; set; }
        public String email { get; set; }
        public String rfc { get; set; }
        public int clienteIdSistema { get; set; }
        public int enviado { get; set; }
        public String codigoRegimenFiscal { get; set; }
        public String codigoUsoCFDI { get; set; }
        public int tipoContribuyente { get; set; }

        public static int createANewCustomerADC(String nombre, String nombreCalle, String numeroExterior, String colonia, String estado, 
            String cuidad, String poblacion, String referencia, String telefono, String cp, String email, String rfc,
            int tipoContribuyente, String codigoRegimenFiscal, String codigoUsoCFDI)
        {
            int idCreated = 0;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                int lastId = getLastId(db);
                lastId++;
                String query = "INSERT INTO " + LocalDatabase.TABLA_CLIENTEADC + " VALUES(@id, @nombre, @zona, @calle, @numero, " +
                    "@colonia, @poblacion, @ciudad, @estado, @referencia, @telefono, @cp, @email, @rfc, @idServer, @enviado,"+
                    "@tipoContribuyente,@regimenFiscal,@usoCfdi)";
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    command.Parameters.AddWithValue("@id", lastId);
                    command.Parameters.AddWithValue("@nombre", nombre);
                    command.Parameters.AddWithValue("@zona", "0");
                    command.Parameters.AddWithValue("@calle", nombreCalle);
                    command.Parameters.AddWithValue("@numero", numeroExterior);
                    command.Parameters.AddWithValue("@colonia", colonia);
                    command.Parameters.AddWithValue("@poblacion", poblacion);
                    command.Parameters.AddWithValue("@ciudad", cuidad);
                    command.Parameters.AddWithValue("@estado", estado);
                    command.Parameters.AddWithValue("@referencia", referencia);
                    command.Parameters.AddWithValue("@telefono", telefono);
                    command.Parameters.AddWithValue("@cp", cp);
                    command.Parameters.AddWithValue("@email", email);
                    command.Parameters.AddWithValue("@rfc", rfc);
                    command.Parameters.AddWithValue("@idServer", 0);
                    command.Parameters.AddWithValue("@enviado", 0);
                    command.Parameters.AddWithValue("@tipoContribuyente", tipoContribuyente);
                    command.Parameters.AddWithValue("@regimenFiscal", codigoRegimenFiscal);
                    command.Parameters.AddWithValue("@usoCfdi", codigoUsoCFDI);
                    int records = command.ExecuteNonQuery();
                    if (records > 0)
                        idCreated = lastId;
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
            return idCreated;
        }

        public static int updateANewCustomerADC(int idCustomer, String nombre, String nombreCalle, String numeroExterior, String colonia, String estado,
            String cuidad, String poblacion, String referencia, String telefono, String cp, String email, String rfc,
            int tipoContribuyente, String codigoRegimenFiscal, String codigoUsoCFDI)
        {
            int idCreated = 0;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "UPDATE " + LocalDatabase.TABLA_CLIENTEADC + " SET "+
                    LocalDatabase.CAMPO_NOMBRE_CLIENTEADC+" = @nombre, " +
                    LocalDatabase.CAMPO_ZONA_CLIENTEADC+" = @zona, "+LocalDatabase.CAMPO_CALLE_CLIENTEADC+" = @calle, "+
                    LocalDatabase.CAMPO_NUMERO_CLIENTEADC+" = @numero, " +LocalDatabase.CAMPO_COLONIA_ADC+" = @colonia, "+
                    LocalDatabase.CAMPO_POBLACION_CLIENTEADC+" = @poblacion, "+LocalDatabase.CAMPO_CIUDAD_CLIENTEADC+" = @ciudad, "+
                    LocalDatabase.CAMPO_ESTADO_CLIENTEADC+" = @estado, "+LocalDatabase.CAMPO_REFERENCIA_CLIENTEADC+" = @referencia, "+
                    LocalDatabase.CAMPO_TELEFONO_CLIENTEADC+" = @telefono, "+LocalDatabase.CAMPO_CP_CLIENTEADC+" = @cp, "+
                    LocalDatabase.CAMPO_EMAIL_CLIENTEADC+" = @email, "+LocalDatabase.CAMPO_RFC_CLIENTEADC+" = @rfc, "+
                    LocalDatabase.CAMPO_IDSISTEMA_CLIENTEADC+" = @idServer, "+
                    LocalDatabase.CAMPO_ENVIADO_CLIENTEADC+" = @enviado, " +
                    LocalDatabase.CAMPO_TIPOCONTRIBUYENTE_CLIENTEADC+" = @tipoContribuyente, "+
                    LocalDatabase.CAMPO_CODIGOREGIMENFISCAL_CLIENTEADC+" = @codigoRegimenFiscal, "+
                    LocalDatabase.CAMPO_CODIGOUSOCFDI_CLIENTEADC+" = @codigoUsoCFDI "+
                    "WHERE " +LocalDatabase.CAMPO_ID_CLIENTEADC+ " = @id";
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    command.Parameters.AddWithValue("@id", idCustomer);
                    command.Parameters.AddWithValue("@nombre", nombre);
                    command.Parameters.AddWithValue("@zona", "0");
                    command.Parameters.AddWithValue("@calle", nombreCalle);
                    command.Parameters.AddWithValue("@numero", numeroExterior);
                    command.Parameters.AddWithValue("@colonia", colonia);
                    command.Parameters.AddWithValue("@poblacion", poblacion);
                    command.Parameters.AddWithValue("@ciudad", cuidad);
                    command.Parameters.AddWithValue("@estado", estado);
                    command.Parameters.AddWithValue("@referencia", referencia);
                    command.Parameters.AddWithValue("@telefono", telefono);
                    command.Parameters.AddWithValue("@cp", cp);
                    command.Parameters.AddWithValue("@email", email);
                    command.Parameters.AddWithValue("@rfc", rfc);
                    command.Parameters.AddWithValue("@idServer", 0);
                    command.Parameters.AddWithValue("@enviado", 0);
                    command.Parameters.AddWithValue("@tipoContribuyente", tipoContribuyente);
                    command.Parameters.AddWithValue("@codigoRegimenFiscal", codigoRegimenFiscal);
                    command.Parameters.AddWithValue("@codigoUsoCFDI", codigoUsoCFDI);
                    int records = command.ExecuteNonQuery();
                    if (records > 0)
                        idCreated = idCustomer;
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
            return idCreated;
        }

        public static int updateEnviadoYComercialId(int idClienteAdc, int idClienteLocal)
        {
            int resp = 0;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "UPDATE " + LocalDatabase.TABLA_CLIENTEADC + " SET " + 
                    LocalDatabase.CAMPO_IDSISTEMA_CLIENTEADC + " = @idClienteAdc, " +
                    LocalDatabase.CAMPO_ENVIADO_CLIENTEADC + " = @customerSended " +
                    "WHERE " + LocalDatabase.CAMPO_ID_CLIENTEADC + " = @id";
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    command.Parameters.AddWithValue("@idClienteAdc", idClienteAdc);
                    command.Parameters.AddWithValue("@customerSended", 1);
                    command.Parameters.AddWithValue("@id", Math.Abs(idClienteLocal));
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

        public static String getNombreWithPanelId(int idClientePanel)
        {
            String name = "";
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "SELECT "+LocalDatabase.CAMPO_NOMBRE_CLIENTEADC+" FROM "+LocalDatabase.TABLA_CLIENTEADC+" WHERE "+
                    LocalDatabase.CAMPO_IDSISTEMA_CLIENTEADC+" = @idClientePanel";
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    command.Parameters.AddWithValue("@idClientePanel", idClientePanel);
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
            }
            finally
            {
                if (db != null && db.State == ConnectionState.Open)
                    db.Close();
            }
            return name;
        }

        public static String getNombreWithLocalId(int idClienteLocal)
        {
            String name = "";
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "SELECT " + LocalDatabase.CAMPO_NOMBRE_CLIENTEADC + " FROM " + LocalDatabase.TABLA_CLIENTEADC + " WHERE " +
                    LocalDatabase.CAMPO_ID_CLIENTEADC + " = @idClienteLocal";
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    command.Parameters.AddWithValue("@idClienteLocal", idClienteLocal);
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

        public static int getTheTotalNumberOfAdditionalCustomersNotSent()
        {
            int total = 0;
            var db = new SQLiteConnection(ClsSQLiteDbHelper.instanceSQLite);
            db.Open();
            try
            {
                String query = "SELECT * FROM " + LocalDatabase.TABLA_CLIENTEADC + " WHERE " +
                        LocalDatabase.CAMPO_ENVIADO_CLIENTEADC + " = " + 0 + " AND " + LocalDatabase.CAMPO_IDSISTEMA_CLIENTEADC + " = " + 0;
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                String clave = CustomerModel.getClaveForAClientwithDb(db,
                                    Convert.ToInt32("-" + Convert.ToInt32(reader[LocalDatabase.CAMPO_ID_CLIENTEADC].ToString().Trim())));
                                if (DocumentModel.verifyIfACustomerHaveAnyDocument(db, clave))
                                {
                                    total++;
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
            return total;
        }

        public static bool isTheCustomerSendedByIdPanel(int idCustomer)
        {
            bool exist = false;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                int idCliente = 0;
                if (idCustomer < 0)
                    idCliente = Math.Abs(idCustomer);
                else idCliente = idCustomer;
                String query = "SELECT COUNT(*) FROM " + LocalDatabase.TABLA_CLIENTEADC + " WHERE " +
                        LocalDatabase.CAMPO_ENVIADO_CLIENTEADC + " = 1 AND " + LocalDatabase.CAMPO_ID_CLIENTEADC + " = "+ idCliente;
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                if (reader.GetValue(0) != DBNull.Value)
                                    if (Convert.ToInt32(reader.GetValue(0).ToString().Trim()) == 1)
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
                SECUDOC.writeLog(e.ToString());
            }
            finally
            {
                if (db != null && db.State == ConnectionState.Open)
                    db.Close();
            }
            return exist;
        }

        public static CustomerADCModel getAllDataForACustomerADCNotSent(int id)
        {
            CustomerADCModel cnm = null;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "SELECT * FROM " + LocalDatabase.TABLA_CLIENTEADC + " WHERE " +
                        LocalDatabase.CAMPO_ENVIADO_CLIENTEADC + " = " + 0 + " AND " + LocalDatabase.CAMPO_ID_CLIENTEADC + " = " + id;
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                cnm = new CustomerADCModel();
                                cnm.id = Convert.ToInt32(reader[LocalDatabase.CAMPO_ID_CLIENTEADC].ToString().Trim());
                                cnm.nombre = reader[LocalDatabase.CAMPO_NOMBRE_CLIENTEADC].ToString().Trim(); ;
                                cnm.zona = Convert.ToInt32(reader[LocalDatabase.CAMPO_ZONA_CLIENTEADC].ToString().Trim());
                                cnm.calle = reader[LocalDatabase.CAMPO_CALLE_CLIENTEADC].ToString().Trim();
                                cnm.numero = reader[LocalDatabase.CAMPO_NUMERO_CLIENTEADC].ToString().Trim();
                                cnm.colonia = reader[LocalDatabase.CAMPO_COLONIA_ADC].ToString().Trim();
                                cnm.poblacion = reader[LocalDatabase.CAMPO_POBLACION_CLIENTEADC].ToString().Trim();
                                cnm.ciudad = Convert.ToInt32(reader[LocalDatabase.CAMPO_CIUDAD_CLIENTEADC].ToString().Trim());
                                cnm.estado = Convert.ToInt32(reader[LocalDatabase.CAMPO_ESTADO_CLIENTEADC].ToString().Trim());
                                cnm.referencia = reader[LocalDatabase.CAMPO_REFERENCIA_CLIENTEADC].ToString().Trim();
                                cnm.telefono = reader[LocalDatabase.CAMPO_TELEFONO_CLIENTEADC].ToString().Trim();
                                cnm.cp = reader[LocalDatabase.CAMPO_CP_CLIENTEADC].ToString().Trim();
                                cnm.email = reader[LocalDatabase.CAMPO_EMAIL_CLIENTEADC].ToString().Trim();
                                cnm.rfc = reader[LocalDatabase.CAMPO_RFC_CLIENTEADC].ToString().Trim();
                                cnm.clienteIdSistema = Convert.ToInt32(reader[LocalDatabase.CAMPO_IDSISTEMA_CLIENTEADC].ToString().Trim());
                                cnm.enviado = Convert.ToInt32(reader[LocalDatabase.CAMPO_ENVIADO_CLIENTEADC].ToString().Trim());
                                cnm.tipoContribuyente = Convert.ToInt32(reader[LocalDatabase.CAMPO_TIPOCONTRIBUYENTE_CLIENTEADC].ToString().Trim());
                                cnm.codigoRegimenFiscal = reader[LocalDatabase.CAMPO_CODIGOREGIMENFISCAL_CLIENTEADC].ToString().Trim();
                                cnm.codigoUsoCFDI = reader[LocalDatabase.CAMPO_CODIGOUSOCFDI_CLIENTEADC].ToString().Trim();
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
            return cnm;
        }

        public static String getNameFromAnADCCustomer(int id)
        {
            String name = "";
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "SELECT "+LocalDatabase.CAMPO_NOMBRE_CLIENTEADC+" FROM " + LocalDatabase.TABLA_CLIENTEADC + " WHERE " +
                    LocalDatabase.CAMPO_ID_CLIENTEADC + " = " + id;
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

        public static List<CustomerADCModel> getAllNotSendClientesADC(int enviado)
        {
            List<CustomerADCModel> listaDeClientes = null;
            CustomerADCModel cnm;
            SQLiteConnection db = null;
            try
            {
                db = new SQLiteConnection();
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "SELECT * FROM " + LocalDatabase.TABLA_CLIENTEADC + " WHERE " +
                        LocalDatabase.CAMPO_ENVIADO_CLIENTEADC + " = " + enviado + " ORDER BY " + 
                        LocalDatabase.CAMPO_ID_CLIENTEADC + " DESC LIMIT " + 1;
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            listaDeClientes = new List<CustomerADCModel>();
                            while (reader.Read())
                            {
                                cnm = new CustomerADCModel();
                                cnm.id = reader.GetInt32(0);
                                cnm.nombre = reader.GetString(1);
                                if (reader[LocalDatabase.CAMPO_ZONA_CLIENTEADC] != DBNull.Value)
                                {
                                    if (reader[LocalDatabase.CAMPO_ZONA_CLIENTEADC].ToString().Trim().Equals(""))
                                        cnm.zona = 0;
                                    else cnm.zona = Convert.ToInt32(reader[LocalDatabase.CAMPO_ZONA_CLIENTEADC].ToString().Trim());
                                } else
                                {
                                    cnm.zona = 0;
                                }
                                cnm.calle = reader.GetString(3);
                                cnm.numero = reader.GetString(4);
                                cnm.colonia = reader.GetString(5);
                                cnm.poblacion = reader.GetString(6);
                                cnm.ciudad = Convert.ToInt32(reader[LocalDatabase.CAMPO_CIUDAD_CLIENTEADC].ToString().Trim());
                                cnm.estado = Convert.ToInt32(reader[LocalDatabase.CAMPO_ESTADO_CLIENTEADC].ToString().Trim());
                                cnm.referencia = reader.GetString(9);
                                cnm.telefono = reader.GetString(10);
                                cnm.cp = reader.GetString(11);
                                cnm.email = reader.GetString(12);
                                cnm.rfc = reader.GetString(13);
                                cnm.clienteIdSistema = reader.GetInt32(14);
                                cnm.enviado = reader.GetInt32(15);
                                cnm.tipoContribuyente = reader.GetInt32(16);
                                cnm.codigoRegimenFiscal = reader.GetString(17);
                                cnm.codigoUsoCFDI = reader.GetString(18);
                                listaDeClientes.Add(cnm);
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
            return listaDeClientes;
        }

        public static CustomerADCModel getAdditionalCustomerById(int id)
        {
            CustomerADCModel cnm = null;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "SELECT * FROM " + LocalDatabase.TABLA_CLIENTEADC + " WHERE "+LocalDatabase.CAMPO_ID_CLIENTEADC+" = @id";
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    command.Parameters.AddWithValue("@id", id);
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                cnm = new CustomerADCModel();
                                cnm.id = reader.GetInt32(0);
                                cnm.nombre = reader.GetValue(1).ToString().Trim();
                                if (reader[LocalDatabase.CAMPO_ZONA_CLIENTEADC] != DBNull.Value)
                                {
                                    if (reader[LocalDatabase.CAMPO_ZONA_CLIENTEADC].ToString().Trim().Equals(""))
                                        cnm.zona = 0;
                                    else cnm.zona = Convert.ToInt32(reader[LocalDatabase.CAMPO_ZONA_CLIENTEADC].ToString().Trim());
                                }
                                else cnm.zona = 0;
                                cnm.calle = reader.GetString(3);
                                cnm.numero = reader.GetString(4);
                                cnm.colonia = reader.GetString(5);
                                cnm.poblacion = reader.GetString(6);
                                cnm.ciudad = Convert.ToInt32(reader[LocalDatabase.CAMPO_CIUDAD_CLIENTEADC].ToString().Trim());
                                cnm.estado = Convert.ToInt32(reader[LocalDatabase.CAMPO_ESTADO_CLIENTEADC].ToString().Trim());
                                cnm.referencia = reader.GetString(9);
                                cnm.telefono = reader.GetString(10);
                                cnm.cp = reader.GetString(11);
                                cnm.email = reader.GetString(12);
                                cnm.rfc = reader.GetString(13);
                                cnm.clienteIdSistema = reader.GetInt32(14);
                                cnm.enviado = reader.GetInt32(15);
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
            return cnm;
        }

        public static int getNewClientIdPanel(int id)
        {
            int realId = 0;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "SELECT " + LocalDatabase.CAMPO_IDSISTEMA_CLIENTEADC + " FROM " +
                        LocalDatabase.TABLA_CLIENTEADC + " WHERE " + LocalDatabase.CAMPO_ID_CLIENTEADC + " = " + Math.Abs(id);
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                if (reader.GetValue(0) != DBNull.Value)
                                    realId = Convert.ToInt32(reader.GetValue(0).ToString().Trim());
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
            return realId;
        }

        public static int getNewClientIdLocal(int idPanel)
        {
            int realId = 0;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "SELECT " + LocalDatabase.CAMPO_ID_CLIENTEADC + " FROM " +
                        LocalDatabase.TABLA_CLIENTEADC + " WHERE " + LocalDatabase.CAMPO_IDSISTEMA_CLIENTEADC + " = " + Math.Abs(idPanel);
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                if (reader.GetValue(0) != DBNull.Value)
                                    realId = Convert.ToInt32(reader.GetValue(0).ToString().Trim());
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
            return realId;
        }

        public static int getNewClientIdLocal(SQLiteConnection db, int idPanel)
        {
            int realId = 0;
            try
            {
                String query = "SELECT " + LocalDatabase.CAMPO_ID_CLIENTEADC + " FROM " +
                        LocalDatabase.TABLA_CLIENTEADC + " WHERE " + LocalDatabase.CAMPO_IDSISTEMA_CLIENTEADC + " = " + Math.Abs(idPanel);
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                if (reader.GetValue(0) != DBNull.Value)
                                    realId = Convert.ToInt32(reader.GetValue(0).ToString().Trim());
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
            return realId;
        }

        public static int getLastId(SQLiteConnection db)
        {
            int lastId = 0;
            try
            {
                String query = "SELECT MAX(" + LocalDatabase.CAMPO_ID_CLIENTEADC + ") FROM " +
                        LocalDatabase.TABLA_CLIENTEADC+" LIMIT 1";
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
            catch (SQLiteException e)
            {
                SECUDOC.writeLog(e.ToString());
            }
            finally
            {
                
            }
            return lastId;
        }

        public static int getLastId()
        {
            int lastId = 0;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "SELECT MAX(" + LocalDatabase.CAMPO_ID_CLIENTEADC + ") FROM " +
                        LocalDatabase.TABLA_CLIENTEADC + " LIMIT 1";
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
            catch (SQLiteException e)
            {
                SECUDOC.writeLog(e.ToString());
            }
            finally
            {
                if (db != null && db.State == ConnectionState.Open)
                    db.Close();
            }
            return lastId;
        }

        public static bool deleteAllCustomersUploadedToComercial()
        {
            bool deleted = false;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "DELETE FROM " + LocalDatabase.TABLA_CLIENTEADC + " WHERE " +
                    LocalDatabase.CAMPO_IDSISTEMA_CLIENTEADC + " != 0 AND "+LocalDatabase.CAMPO_ENVIADO_CLIENTEADC+" != 0";
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    int records = command.ExecuteNonQuery();
                    if (records > 0)
                        deleted = true;
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
                if (deleted)
                {
                    CustomerModel.deleteAllNewCustomersUploadedToComercial();
                }
            }
            return deleted;
        }

        public static bool deleteClienteNuevoConSusDocumentos(int idCustomer)
        {
            bool deleted = false;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                List<int> idsToDelete = null;
                String query = "SELECT * FROM "+ LocalDatabase.TABLA_DOCUMENTOVENTA + " WHERE " +
                    LocalDatabase.CAMPO_CLIENTEID_DOC + " = @idCustomer";
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    command.Parameters.AddWithValue("@idCustomer", idCustomer);
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.IsClosed)
                        {
                            idsToDelete = new List<int>();
                            while (reader.Read())
                            {
                                idsToDelete.Add(Convert.ToInt32(reader[LocalDatabase.CAMPO_ID_DOC].ToString().Trim()));
                            }
                        }
                        if (reader != null && !reader.IsClosed)
                            reader.Close();
                    }
                }
                if (idsToDelete != null && idsToDelete.Count > 0)
                {
                    foreach (var idDocumento in idsToDelete)
                    {
                        String queryMoves = "DELETE FROM " + LocalDatabase.TABLA_MOVIMIENTO + " WHERE " +
                            LocalDatabase.CAMPO_DOCUMENTOID_MOV + " = @idDocumento";
                        using (SQLiteCommand command = new SQLiteCommand(queryMoves, db))
                        {
                            command.Parameters.AddWithValue("@idDocumento", idDocumento);
                            int records = command.ExecuteNonQuery();
                        }
                        String queryDocs = "DELETE FROM " + LocalDatabase.TABLA_DOCUMENTOVENTA + " WHERE " +
                            LocalDatabase.CAMPO_ID_DOC + " = @idDocumento";
                        using (SQLiteCommand command = new SQLiteCommand(queryDocs, db))
                        {
                            command.Parameters.AddWithValue("@idDocumento", idDocumento);
                            int records = command.ExecuteNonQuery();
                        }
                    }
                    String queryDCADC = "DELETE FROM " + LocalDatabase.TABLA_CLIENTEADC + " WHERE " +
                            LocalDatabase.CAMPO_ID_CLIENTEADC + " = @idCustomer";
                    using (SQLiteCommand command = new SQLiteCommand(queryDCADC, db))
                    {
                        command.Parameters.AddWithValue("@idCustomer", Math.Abs(idCustomer));
                        int records = command.ExecuteNonQuery();
                    }
                    String queryDC = "DELETE FROM " + LocalDatabase.TABLA_CLIENTES + " WHERE " +
                            LocalDatabase.CAMPO_ID_CLIENTE + " = @idCustomer";
                    using (SQLiteCommand command = new SQLiteCommand(queryDC, db))
                    {
                        command.Parameters.AddWithValue("@idCustomer", idCustomer);
                        int records = command.ExecuteNonQuery();
                    }
                } else
                {
                    String queryDCADC = "DELETE FROM " + LocalDatabase.TABLA_CLIENTEADC + " WHERE " +
                            LocalDatabase.CAMPO_ID_CLIENTEADC + " = @idCustomer";
                    using (SQLiteCommand command = new SQLiteCommand(queryDCADC, db))
                    {
                        command.Parameters.AddWithValue("@idCustomer", Math.Abs(idCustomer));
                        int records = command.ExecuteNonQuery();
                    }
                    String queryDC = "DELETE FROM " + LocalDatabase.TABLA_CLIENTES + " WHERE " +
                            LocalDatabase.CAMPO_ID_CLIENTE + " = @idCustomer";
                    using (SQLiteCommand command = new SQLiteCommand(queryDC, db))
                    {
                        command.Parameters.AddWithValue("@idCustomer", idCustomer);
                        int records = command.ExecuteNonQuery();
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
            return deleted;
        }

        public static bool deleteClienteNuevoSinDocumentosByPanelId(int idCustomer)
        {
            bool deleted = false;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String queryDCADC = "DELETE FROM " + LocalDatabase.TABLA_CLIENTEADC + " WHERE " +
                            LocalDatabase.CAMPO_IDSISTEMA_CLIENTEADC + " = @idCustomer";
                using (SQLiteCommand command = new SQLiteCommand(queryDCADC, db))
                {
                    command.Parameters.AddWithValue("@idCustomer", Math.Abs(idCustomer));
                    int records = command.ExecuteNonQuery();
                    if (records > 0)
                        deleted = true;
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
            return deleted;
        }

        public static bool deleteClienteNuevoSinDocumentosByPanelId(SQLiteConnection db, int idCustomer)
        {
            bool deleted = false;
            try
            {
                String queryDCADC = "DELETE FROM " + LocalDatabase.TABLA_CLIENTEADC + " WHERE " +
                            LocalDatabase.CAMPO_IDSISTEMA_CLIENTEADC + " = @idCustomer";
                using (SQLiteCommand command = new SQLiteCommand(queryDCADC, db))
                {
                    command.Parameters.AddWithValue("@idCustomer", Math.Abs(idCustomer));
                    int records = command.ExecuteNonQuery();
                    if (records > 0)
                        deleted = true;
                }
            }
            catch (SQLiteException e)
            {
                SECUDOC.writeLog(e.ToString());
            }
            finally
            {
               
            }
            return deleted;
        }

    }
}
