using AdminDll;
using MySQLDriverCS;
using System.Data;
using System.Windows.Forms;

namespace SyncTPV
{
    class clsDatosMySQL
    {
        MySQLConnection DBCon;
        public bool conexion { get; set; }
        public string MensajeError = "";

        public clsDatosMySQL()
        {
            DBCon = new MySQLConnection();
            try
            {
                string[] lines = System.IO.File.ReadAllLines(MetodosGenerales.rootDirectory + "\\crip.dll");
                lines = BajoNivel.Desencriptar(lines);
                DBCon.ConnectionString = new MySQLConnectionString(lines[0], lines[1], lines[2], lines[3], 3306).AsString;
                DBCon.Open();
                conexion = true;
            }
            catch (MySQLException ex)
            {
                MessageBox.Show(ex.Message.ToString());
                MensajeError = ex.Message;
                conexion = false;
            }
        }

        public IDataReader EjecutarCadenaDR(string consulta)
        {
            MySQLCommand sql = new MySQLCommand();
            sql.Connection = DBCon;
            sql.CommandText = consulta;
            sql.CommandType = CommandType.Text;
            IDataReader dr = sql.ExecuteReader();
            return dr;
        }

        public int EjecutaCadena(string consulta)
        {
            MySQLCommand cmd = new MySQLCommand(consulta, DBCon);
            cmd.Connection.Open();
            return cmd.ExecuteNonQuery();
        }

        public void Cerrar()
        {
            DBCon.Close();
        }
    }
}
