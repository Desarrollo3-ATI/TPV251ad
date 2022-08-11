using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Windows.Forms;

namespace SyncTPV
{
    public partial class frmPedidosInfo : Form
    {
        int IdDocumento = 0, Error = 0;
        string query = "", MensajeError = "";
        List<clsCargaPedidoDetalle> lPedidoDetalle = new List<clsCargaPedidoDetalle>();
        public frmPedidosInfo(int IdDoc)
        {
            InitializeComponent();
            IdDocumento = IdDoc;
        }

        private void frmPedidosInfo_Load(object sender, EventArgs e)
        {
            Error = TraerArticulos();
            if (Error == 0)
            {
                LlenarGrid();
            }
        }
        private int TraerArticulos()
        {
            clsManejoDatosSqlite db = new clsManejoDatosSqlite();
            SQLiteDataReader dr;
            try
            {
                query = "SELECT * FROM PedidoDetalle WHERE CIDDOCTOPEDIDOCC = " + IdDocumento;
                dr = db.ExecuteReader(query);
                while (dr.Read())
                {
                    clsCargaPedidoDetalle PedidoDetalle = new clsCargaPedidoDetalle();
                    PedidoDetalle.CIDDOCTOPEDIDOCC = Convert.ToInt32(dr["CIDDOCTOPEDIDOCC"]);
                    PedidoDetalle.CCODIGOPRODUCTO = dr["CCODIGOPRODUCTO"].ToString().Trim();
                    PedidoDetalle.CNUMEROMOVIMIENTO = dr["CNUMEROMOVIMIENTO"].ToString().Trim();
                    PedidoDetalle.CPRECIO = dr["CPRECIO"].ToString().Trim();
                    PedidoDetalle.CUNIDADES = dr["CUNIDADES"].ToString().Trim();
                    PedidoDetalle.CSUBTOTAL = dr["CSUBTOTAL"].ToString().Trim();
                    PedidoDetalle.CDESCUENTO = dr["CDESCUENTO"].ToString().Trim();
                    PedidoDetalle.CTOTAL = dr["CTOTAL"].ToString().Trim();
                    lPedidoDetalle.Add(PedidoDetalle);
                }
                dr.Close();
                db.Cerrar();
                Error = 0;
            }
            catch (Exception Ex)
            {
                db.Cerrar();
                MensajeError = Ex.Message;
                SECUDOC.writeLog(Ex.ToString());
                Error = 1;
            }
            return Error;
        }
        private int LlenarGrid()
        {
            dtGridDetalles.Rows.Clear();
            dtGridDetalles.Rows.Add();
            if (lPedidoDetalle.Count > 0)
            {
                for (int i = 0; i < lPedidoDetalle.Count; i++)
                {
                    dtGridDetalles.Rows[i].Cells["CODIGO"].Value = lPedidoDetalle[i].CCODIGOPRODUCTO;
                    dtGridDetalles.Rows[i].Cells["PRECIO"].Value = lPedidoDetalle[i].CPRECIO;
                    dtGridDetalles.Rows[i].Cells["CANTIDAD"].Value = lPedidoDetalle[i].CUNIDADES;
                    dtGridDetalles.Rows[i].Cells["SUBTOTAL"].Value = lPedidoDetalle[i].CSUBTOTAL;
                    dtGridDetalles.Rows[i].Cells["DESCUENTO"].Value = lPedidoDetalle[i].CDESCUENTO;
                    dtGridDetalles.Rows[i].Cells["TOTAL"].Value = lPedidoDetalle[i].CTOTAL;
                }
            }
            return Error;
        }
    }
}
