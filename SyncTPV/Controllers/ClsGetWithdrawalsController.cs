using Newtonsoft.Json;
using RestSharp;
using SyncTPV.Helpers.SqliteDatabaseHelper;
using SyncTPV.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;

namespace SyncTPV.Controllers
{
    public class ClsGetWithdrawalsController
    {
        public int userId, lastIdRetiro, times;
        public String startDate, endDate;

        public ClsGetWithdrawalsController(int userId, String startDate, String endDate, int lastIdRetiro, int times)
        {
            this.userId = userId;
            this.startDate = startDate;
            this.endDate = endDate;
            this.lastIdRetiro = lastIdRetiro;
            this.times = times;
        }

        private void handleActionDownloadWithdrawals(int userId, String startDate, String endDate)
        {
            String dataJson = "{\n" +
                    " \"idRetiro\":" + lastIdRetiro + ",\n" +
                    " \"idUsuario\":" + userId + ",\n" +
                    " \"fechaInicio\":\"" + startDate + "\",\n" +
                    " \"fechaFin\":\"" + endDate + "\"\n" +
                    "}";
            ResponseWithdrawal rw = sendParametersWithdrawalsToWs(dataJson);
            int withdrawalsCount = 0;
            var db = new SQLiteConnection(ClsSQLiteDbHelper.instanceSQLite);
            db.Open();
            try
            {
                withdrawalsCount = rw.withdrawalsCount;
                List<Withdrawal> withdrawalsList = rw.withdrawals;
                for (int i = 0; i < withdrawalsList.Count; i++)
                {
                    Withdrawal objectWithdrawal = withdrawalsList[i];
                    String query = "SELECT * FROM " + LocalDatabase.TABLA_RETIROS + " WHERE " + LocalDatabase.CAMPO_IDSERVER_RETIRO +
                            " = " + lastIdRetiro;
                    using (SQLiteCommand command = new SQLiteCommand(query, db))
                    {
                        using (SQLiteDataReader reader = command.ExecuteReader())
                        {
                            if (!reader.HasRows)
                            {
                                int lastId = RetiroModel.getLastId() + 1;
                                String query1 = "INSERT INTO " + LocalDatabase.TABLA_RETIROS + " (" + LocalDatabase.CAMPO_ID_RETIRO + ", " +
                                    LocalDatabase.CAMPO_NUMERO_RETIRO + ", " + LocalDatabase.CAMPO_IDUSUARIO_RETIRO + ", " +
                                    LocalDatabase.CAMPO_CLAVEUSUARIO_RETIRO + ", " + LocalDatabase.CAMPO_FECHAHORA_RETIRO + ", " +
                                    LocalDatabase.CAMPO_IDSERVER_RETIRO + ", " + LocalDatabase.CAMPO_ENVIADO_RETIRO + ", " + LocalDatabase.CAMPO_DOWNLOADED_RETIRO + ") " +
                                    "VALUES(@id, @number, @userId, @userCode, @fechaHora, @idServer, @enviado, @downloaded)";
                                using (SQLiteCommand command1 = new SQLiteCommand(query1, db))
                                {
                                    command.Parameters.AddWithValue("@id", lastId);
                                    command.Parameters.AddWithValue("@number", objectWithdrawal.numero);
                                    command.Parameters.AddWithValue("@userId", objectWithdrawal.idUsuario);
                                    command.Parameters.AddWithValue("@userCode", objectWithdrawal.claveUsuario);
                                    command.Parameters.AddWithValue("@fechaHora", objectWithdrawal.fechaHora);
                                    command.Parameters.AddWithValue("@idServer", objectWithdrawal.idServer);
                                    command.Parameters.AddWithValue("@enviado", 1);
                                    command.Parameters.AddWithValue("@downloaded", 1);
                                    int records = command.ExecuteNonQuery();
                                    if (records > 0)
                                    {
                                        List<WithdrawalAmount> montosList = objectWithdrawal.montos;
                                        for (int j = 0; j < montosList.Count; j++)
                                        {
                                            WithdrawalAmount monto = montosList[i];
                                            int idMonto = MontoRetiroModel.getLastId() + 1;
                                            String query3 = "INSERT INTO " + LocalDatabase.TABLA_MONTORETIROS + " (" + LocalDatabase.CAMPO_ID_MONTORETIROS + ", " +
                                                LocalDatabase.CAMPO_FORMACOBROID_MONTORETIROS + ", " + LocalDatabase.CAMPO_IMPORTE_MONTORETIROS + ", " +
                                                LocalDatabase.CAMPO_RETIROID_MONTORETIRO + ", " + LocalDatabase.CAMPO_ENVIADO_MONTORETIRO + ") VALUES(@id," +
                                                "@fcId, @amount, @retiroId, @sended)";
                                            using (SQLiteCommand command2 = new SQLiteCommand(query3, db))
                                            {
                                                command.Parameters.AddWithValue("@id", idMonto);
                                                command.Parameters.AddWithValue("@fcId", monto.formaCobroIdAbono);
                                                command.Parameters.AddWithValue("@amount", monto.importe);
                                                command.Parameters.AddWithValue("@retiroId", lastId);
                                                command.Parameters.AddWithValue("@sended", 1);
                                                command.ExecuteNonQuery();
                                            }
                                        }
                                    }
                                }
                            }
                            if (reader != null && !reader.IsClosed)
                                reader.Close();
                        }
                    }
                }
            }
            catch (Exception e)
            {
                SECUDOC.writeLog(e.ToString());
            }
            finally
            {
                if (db != null && db.State == ConnectionState.Open)
                    db.Close();
            }
        }

        private class ResponseWithdrawal
        {
            public int withdrawalsCount { get; set; }
            public List<Withdrawal> withdrawals { get; set; }
        }

        private class Withdrawal
        {
            public int numero { get; set; }
            public int idUsuario { get; set; }
            public String claveUsuario { get; set; }
            public String fechaHora { get; set; }
            public int idServer { get; set; }
            public List<WithdrawalAmount> montos { get; set; }
        }

        private class WithdrawalAmount
        {
            public int formaCobroIdAbono { get; set; }
            public double importe { get; set; }
        }
        private ResponseWithdrawal sendParametersWithdrawalsToWs(String dataJSON)
        {
            ResponseWithdrawal rw = new ResponseWithdrawal();
            int correctResponse = 0;
            try
            {
                int itemsToEvaluate = 0;
                String url = ConfiguracionModel.getLinkWs();
                url = url.Replace(" ", "%20");
                var client = new RestClient(url);
                // client.Authenticator = new HttpBasicAuthenticator(username, password);
                var request = new RestRequest("/getAllWithdrawalsWithTheirAmountsByDateAndUser", Method.Post);
                request.AddParameter("application/json", dataJSON, ParameterType.RequestBody);
                //request.AddJsonBody(dataJSON);
                var responseHeader = client.ExecuteAsync(request);
                if (responseHeader.Result.ResponseStatus == ResponseStatus.Completed)
                {
                    var content = responseHeader.Result.Content; // Raw content as string
                    //var responseHttp = client.Post<String>(request);
                    var jsonResp = JsonConvert.DeserializeObject<ResponseWithdrawal>(content);
                    ResponseWithdrawal respJson = (ResponseWithdrawal)jsonResp;
                    rw = respJson;
                }
                else if (responseHeader.Result.ResponseStatus == ResponseStatus.Error)
                {
                    if (responseHeader.Result.ErrorMessage.Equals("Unable to connect to the remote server"))
                    {
                        correctResponse = 404;
                    }
                    else
                    {
                        correctResponse = 404;
                    }
                }
            }
            catch (Exception e)
            {
                SECUDOC.writeLog(e.ToString());
            }
            return rw;
        }

    }
}
