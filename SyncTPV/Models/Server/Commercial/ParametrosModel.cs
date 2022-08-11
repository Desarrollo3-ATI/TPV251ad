using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyncTPV.Models.Server.Commercial
{
    public class ParametrosModel
    {

        public static int getIntValue(String comInstance, String query)
        {
            int value = 0;
            var db = new SqlConnection();
            try
            {
                db.ConnectionString = comInstance;
                db.Open();
                using (SqlCommand command = new SqlCommand(query, db))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
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
            catch (SqlException e)
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

        public static int getIntValue(SqlConnection dbCom, String query)
        {
            int value = 0;
            try
            {
                using (SqlCommand command = new SqlCommand(query, dbCom))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
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
            catch (SqlException e)
            {
                SECUDOC.writeLog(e.ToString());
            }
            finally
            {
            
            }
            return value;
        }

    }
}
