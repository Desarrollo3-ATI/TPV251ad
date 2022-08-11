using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Drawing;
using System.Windows.Forms;

namespace SyncTPV
{
    public partial class frmPedidos : Form
    {
        string query = "", MensajeError = "";
        int Error = 0;
        FormMessage msj;
        List<clsCargaPedidoEncabezado> lPedidoEncabezado = new List<clsCargaPedidoEncabezado>();
        public frmPedidos()
        {
            InitializeComponent();
        }

        private void frmPedidos_Load(object sender, EventArgs e)
        {
            clsManejoDatosSqlite db = new clsManejoDatosSqlite();
            SQLiteDataReader dr;

            try
            {
                query = "SELECT * FROM PedidoEncabezado";
                dr = db.ExecuteReader(query);
                while (dr.Read())
                {
                    clsCargaPedidoEncabezado PedidoEncabezado = new clsCargaPedidoEncabezado();
                    PedidoEncabezado.CIDDOCTOPEDIDOCC = Convert.ToInt32(dr["CIDDOCTOPEDIDOCC"]);
                    PedidoEncabezado.CLIENTE_ID = Convert.ToInt32(dr["CLIENTE_ID"]);
                    PedidoEncabezado.CNOMBRECLIENTE = dr["CNOMBRECLIENTE"].ToString().Trim();
                    PedidoEncabezado.CNOMBREAGENTECC = dr["CNOMBREAGENTECC"].ToString().Trim();
                    PedidoEncabezado.CFECHA = dr["CFECHA"].ToString().Trim();
                    PedidoEncabezado.CFOLIO = dr["CFOLIO"].ToString().ToString();
                    PedidoEncabezado.CSUBTOTAL = Convert.ToDouble(dr["CSUBTOTAL"]);
                    PedidoEncabezado.CDESCUENTO = Convert.ToDouble(dr["CDESCUENTO"]);
                    PedidoEncabezado.CTOTAL = Convert.ToDouble(dr["CTOTAL"]);
                    PedidoEncabezado.SURTIDO = Convert.ToInt32(dr["SURTIDO"]);
                    lPedidoEncabezado.Add(PedidoEncabezado);
                }
                dr.Close();
                db.Cerrar();

                for (int i = 0; i < lPedidoEncabezado.Count; i++)
                {
                    clsManejoDatosSqlite db1 = new clsManejoDatosSqlite();
                    SQLiteDataReader dr1;
                    query = "SELECT * FROM Clientes WHERE CLIENTE_ID = " + lPedidoEncabezado[i].CLIENTE_ID;
                    dr1 = db1.ExecuteReader(query);
                    while (dr1.Read())
                    {
                        lPedidoEncabezado[i].Cliente.CLAVE = dr1["CLAVE"].ToString().Trim();
                        lPedidoEncabezado[i].Cliente.NOMBRE = dr1["NOMBRE"].ToString().Trim();
                        lPedidoEncabezado[i].Cliente.DIRECCION = dr1["DIRECCION"].ToString().Trim();
                    }
                    dr1.Close();
                    db1.Cerrar();
                }

            }

            catch (Exception Ex)
            {
                db.Cerrar();
                //db1.Cerrar();
                MensajeError = Ex.ToString();
                SECUDOC.writeLog(Ex.ToString());
                Error = 1;
            }
            LlenaGrid();
        }

        private void dtGridPedidos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int column = e.ColumnIndex;
            if (column == 0)
            {
                var IdDocumento = dtGridPedidos.Rows[e.RowIndex].Cells[6].Value;
                int IdDoc = (int)IdDocumento;

                frmPedidosInfo PedidosInfo = new frmPedidosInfo(IdDoc);
                PedidosInfo.ShowDialog();
            }
        }

        private void txtBuscarCliente_KeyUp(object sender, KeyEventArgs e)
        {
            LlenaGrid();
            bool banResultados = false;
            string buscar = txtBuscarPedido.Text.Trim().ToLower();
            List<DataGridViewRow> resultados = new List<DataGridViewRow>();
            resultados.Clear();

            for (int i = 0; i < dtGridPedidos.Rows.Count; i++)
            {
                DataGridViewRow Row = dtGridPedidos.Rows[i];
                bool itemAgregado = false;
                string Cliente = dtGridPedidos.Rows[i].Cells[1].Value.ToString().Trim().ToLower();
                if (Cliente.Contains(buscar))
                {
                    banResultados = true;
                    Row = dtGridPedidos.Rows[i];
                    resultados.Add(Row);
                    itemAgregado = true;
                }

                string Folio = dtGridPedidos.Rows[i].Cells[3].Value.ToString().Trim().ToLower();
                if (Folio.Contains(buscar) && !itemAgregado)
                {
                    banResultados = true;
                    Row = dtGridPedidos.Rows[i];
                    resultados.Add(Row);
                    itemAgregado = true;
                }
            }
            if (banResultados)
            {
                dtGridPedidos.Rows.Clear();
                for (int j = 0; j < resultados.Count; j++)
                {
                    dtGridPedidos.Rows.Add();
                    dtGridPedidos.Rows[j].Cells["Pedidos"].Value = Image.FromFile(MetodosGenerales.rootDirectory + @"\Imagenes\Estaticas\cliente_grid.png");
                    dtGridPedidos.Rows[j].Cells["Cliente"].Value = resultados[j].Cells[1].Value;
                    dtGridPedidos.Rows[j].Cells["Agente"].Value = resultados[j].Cells[2].Value;
                    dtGridPedidos.Rows[j].Cells["Folio"].Value = resultados[j].Cells[3].Value;
                    dtGridPedidos.Rows[j].Cells["Direccion"].Value = resultados[j].Cells[4].Value;
                    dtGridPedidos.Rows[j].Cells["Total"].Value = resultados[j].Cells[5].Value;
                    dtGridPedidos.Rows[j].Cells["IdDocumento"].Value = resultados[j].Cells[6].Value;
                }
            }
            else
            {
                dtGridPedidos.Rows.Clear();
                //MessageBox.Show("No se encontraron resultados.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                msj = new FormMessage("Información", "No se encontraron resultados.", 2);
                msj.ShowDialog();
            }
        }

        private void picCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void LlenaGrid()
        {
            dtGridPedidos.Enabled = true;
            dtGridPedidos.Rows.Clear();

            if (lPedidoEncabezado.Count > 0)
            {
                for (int i = 0; i < lPedidoEncabezado.Count; i++)
                {
                    dtGridPedidos.Rows.Add();
                    dtGridPedidos.Rows[i].Cells["Pedidos"].Value = Image.FromFile(MetodosGenerales.rootDirectory + @"\Imagenes\Estaticas\cliente_grid.png");
                    dtGridPedidos.Rows[i].Cells["Cliente"].Value = lPedidoEncabezado[i].CNOMBRECLIENTE;
                    dtGridPedidos.Rows[i].Cells["Agente"].Value = lPedidoEncabezado[i].CNOMBREAGENTECC;
                    dtGridPedidos.Rows[i].Cells["Folio"].Value = lPedidoEncabezado[i].CFOLIO;
                    dtGridPedidos.Rows[i].Cells["Direccion"].Value = lPedidoEncabezado[i].Cliente.DIRECCION;
                    dtGridPedidos.Rows[i].Cells["Total"].Value = lPedidoEncabezado[i].CTOTAL;
                    dtGridPedidos.Rows[i].Cells["IdDocumento"].Value = lPedidoEncabezado[i].CIDDOCTOPEDIDOCC;


                }
            }
        }

    }
}
