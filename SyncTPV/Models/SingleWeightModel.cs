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
    public class SingleWeightModel
    {
        public int id { get; set; }
        public double weight { get; set; }
        public int number { get; set; }
        public int taras { get; set; }
        public String dateTime { get; set; }
        public int type { get; set; }
        public int weightId { get; set; }
        public int movementId { get; set; }

        public static bool createSingleWeight(int id, double weight, int number, int taras, String dateTime, int type, int weightId, int movementId)
        {
            bool created = false;
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                String query = "";
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    int records = command.ExecuteNonQuery();
                    if (records > 0)
                        created = true;
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
            return created;
        }
    }
}
