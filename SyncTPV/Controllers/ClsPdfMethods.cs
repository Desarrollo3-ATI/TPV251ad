using iTextSharp.text;
using iTextSharp.text.pdf;
using SyncTPV.Helpers.SqliteDatabaseHelper;
using SyncTPV.Models;
using SyncTPV.Views.Reports;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using wsROMClases;
using wsROMClases.Models.Panel;
using static ClsDocumentoModel;

namespace SyncTPV.Controllers
{
    public class ClsPdfMethods
    {
        private static string Sangria_1 = " ";
        private static string Sangria_2 = "  ";
        private static int Bordes_Ninguno = 0; //indica que no pintará bordes para esta celda
        private static int Bordes_Superior = 1; //indica que pintará un borde superior para esta celda
        private static int Bordes_Inferior = 2; //indica que pintará un borde inferior para esta celda
        private static int Bordes_Superior_Inferior = 3; //indica que pintará un borde superior e inferior para esta celda
        private static int Bordes_Izquierdo = 4; //indica que pintará un borde izquierdo para esta celda
        private static int Bordes_Izquierdo_Superior = 5; //indica que pintará un borde izquierdo y superior para esta celda
        private static int Bordes_Izquierdo_Inferior = 6; //indica que pintará un borde izquierdo e inferior para esta celda
        private static int Bordes_Izquierdo_Superior_Inferior = 7; //indica que pintará un borde izquierdo, superior e inferior para esta celda
        private static int Bordes_Derecho = 8; //indica que pintará un borde derecho para esta celda
        private static int Bordes_Derecho_Superior = 9; //indica que pintará un borde derecho y superior para esta celda
        private static int Bordes_Derecho_Inferior = 10; //indica que pintará un borde derecho e inferior para esta celda
        private static int Bordes_Derecho_Superior_Inferior = 11; //indica que pintará un borde derecho, superior e inferior para esta celda
        private static int Bordes_Derecho_Izquierdo = 12; //indica que pintará un borde derecho e izquierdo para esta celda
        private static int Bordes_Derecho_Izquierdo_Superior = 13; //indica que pintará un borde derecho, izquierdo y superior para esta celda
        private static int Bordes_Derecho_Izquierdo_Inferior = 14; //indica que pintará un borde derecho, izquierdo e inferior para esta celda
        private static int Bordes_Todos = 15; //indica que pintará todos los bordes para esta celda
        private static BaseFont bfFuente = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, false);
        private static Font Fuente_Arial_Negra_6 = new Font(bfFuente, 6, Font.NORMAL, BaseColor.BLACK);
        private static Font Fuente_Arial_Negra_8 = new Font(bfFuente, 8, Font.NORMAL, BaseColor.BLACK);
        private static Font Fuente_Arial_Azul_Bold_9 = new Font(bfFuente, 9, Font.NORMAL, BaseColor.BLACK);
        private static Font Fuente_Arial_Azul_Bold_10 = new Font(bfFuente, 10, Font.NORMAL, BaseColor.BLACK);
        private static Font Fuente_Arial_Azul_Bold_12 = new Font(bfFuente, 12, Font.NORMAL, BaseColor.BLACK);
        private static Font Fuente_Arial_Azul_Bold_16 = new Font(bfFuente, 16, Font.NORMAL, BaseColor.BLACK);
        private static Font Fuente_Espacio_2 = new Font(bfFuente, 2, Font.NORMAL, BaseColor.WHITE);

        public async Task<bool> createPdfDocuments(String nombreEmpresa, String rutaArchivo, String nameReport, int userID, bool permissionPrepedido,
            String startDate, String endDate, int lastId, int LIMIT,String horaInicio, String horaFin)
        {
            bool created = false;
            await Task.Run(async () =>
            {
                List<DocumentoModel> documentos = null;
                GetDataService gds = new GetDataService();
                dynamic respuesta = null;
                if (ConfiguracionModel.isLANPermissionActivated())
                    respuesta = await gds.downloadAllDocumentsLAN(userID, startDate, endDate, lastId, LIMIT);
                else respuesta = await gds.downloadAllDocuments(userID, startDate, endDate, lastId, LIMIT);
                if (respuesta != null)
                {
                    int value = respuesta.value;
                    String description = respuesta.description;
                    int information = respuesta.information;
                    if (value < 0)
                    {
                        FormMessage fm = new FormMessage("Exception", description, 2);
                        fm.ShowDialog();
                    }
                    else
                    {
                        if (information == GetDataService.GET_DOCUMENT)
                        {
                            if (value > 0)
                            {
                                documentos = respuesta.documentos;
                            }
                            else if (value == 0)
                            {
                                FormMessage formMessage = new FormMessage("No se encontro información", description, 3);
                                formMessage.ShowDialog();
                            }
                            else
                            {
                                FormMessage formMessage = new FormMessage("Exception", description, 2);
                                formMessage.ShowDialog();
                            }
                        }
                    }
                }
                else
                {

                }
                //Creates an instance of the iTextSharp.text.Document-object:
                //Document document = new Document(PageSize.LETTER, 30, 30, 20, 30);
                //                                tamañoPagina, mIzq, mDer, mTop, mBot                            
                Document document = new Document(PageSize.LETTER, 30, 30, 9, 1);
                //float dimension = 570f;
                float dimension = 556f;
                string sourceFile = Application.StartupPath + @"\Temp.pdf";
                string destinationFile = rutaArchivo;
                PdfWriter.GetInstance(document, new FileStream(sourceFile, FileMode.Create));
                document.Open();

                //Agrega una tabla (espacio en blanco)
                PdfPTable TablaEspacio = new PdfPTable(1);
                TablaEspacio.TotalWidth = dimension;
                TablaEspacio.LockedWidth = true;
                PdfPCell CeldaEspacio = new PdfPCell(new Phrase(" ", Fuente_Espacio_2));
                CeldaEspacio.Border = Bordes_Ninguno;
                TablaEspacio.AddCell(CeldaEspacio);

                PdfPCell CeldaImprimeTexto;

                //Comienza llenado de archivo
                PdfPTable TablaEncabezado = new PdfPTable(1);
                TablaEncabezado.TotalWidth = dimension;
                TablaEncabezado.LockedWidth = true;

                CeldaImprimeTexto = new PdfPCell(new Phrase(nombreEmpresa, Fuente_Arial_Azul_Bold_16));
                CeldaImprimeTexto.Colspan = 1;
                CeldaImprimeTexto.Border = Bordes_Ninguno;
                CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_CENTER;
                TablaEncabezado.AddCell(CeldaImprimeTexto);

                CeldaImprimeTexto = new PdfPCell(new Phrase("Documentos", Fuente_Arial_Azul_Bold_12));
                CeldaImprimeTexto.Colspan = 1;
                CeldaImprimeTexto.Border = Bordes_Ninguno;
                CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_CENTER;
                TablaEncabezado.AddCell(CeldaImprimeTexto);

                CeldaImprimeTexto = new PdfPCell(new Phrase(Sangria_2, Fuente_Arial_Azul_Bold_12));
                CeldaImprimeTexto.Colspan = 1;
                CeldaImprimeTexto.Border = Bordes_Ninguno;
                CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_CENTER;
                TablaEncabezado.AddCell(CeldaImprimeTexto);

                PdfPTable tabla2 = new PdfPTable(20);
                tabla2.TotalWidth = dimension;
                tabla2.LockedWidth = true;

                CeldaImprimeTexto = new PdfPCell(new Phrase("Cliente", Fuente_Arial_Negra_8));
                CeldaImprimeTexto.Colspan = 2;
                CeldaImprimeTexto.Border = Bordes_Inferior;
                CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_CENTER;
                tabla2.AddCell(CeldaImprimeTexto);

                CeldaImprimeTexto = new PdfPCell(new Phrase("Tipo", Fuente_Arial_Negra_8));
                CeldaImprimeTexto.Colspan = 2;
                CeldaImprimeTexto.Border = Bordes_Inferior;
                CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_CENTER;
                tabla2.AddCell(CeldaImprimeTexto);

                CeldaImprimeTexto = new PdfPCell(new Phrase("Fecha", Fuente_Arial_Negra_8));
                CeldaImprimeTexto.Colspan = 4;
                CeldaImprimeTexto.Border = Bordes_Inferior;
                CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_CENTER;
                tabla2.AddCell(CeldaImprimeTexto);

                CeldaImprimeTexto = new PdfPCell(new Phrase("Folio", Fuente_Arial_Negra_8));
                CeldaImprimeTexto.Colspan = 3;
                CeldaImprimeTexto.Border = Bordes_Inferior;
                CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_CENTER;
                tabla2.AddCell(CeldaImprimeTexto);

                CeldaImprimeTexto = new PdfPCell(new Phrase("Movimientos", Fuente_Arial_Negra_8));
                CeldaImprimeTexto.Colspan = 3;
                CeldaImprimeTexto.Border = Bordes_Inferior;
                CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_CENTER;
                tabla2.AddCell(CeldaImprimeTexto);

                CeldaImprimeTexto = new PdfPCell(new Phrase("Descuento", Fuente_Arial_Negra_8));
                CeldaImprimeTexto.Colspan = 3;
                CeldaImprimeTexto.Border = Bordes_Inferior;
                CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_CENTER;
                tabla2.AddCell(CeldaImprimeTexto);

                CeldaImprimeTexto = new PdfPCell(new Phrase("Total", Fuente_Arial_Negra_8));
                CeldaImprimeTexto.Colspan = 3;
                CeldaImprimeTexto.Border = Bordes_Inferior;
                CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_CENTER;
                tabla2.AddCell(CeldaImprimeTexto);

                double totalDocumentos = 0;
                decimal TotalValor = 0;
                if (documentos != null)
                {
                    foreach (var documento in documentos)
                    {
                        // TotalVentas += liquidacionProducto.Valor;
                        CeldaImprimeTexto = new PdfPCell(new Phrase(documento.clave_cliente.ToString(), Fuente_Arial_Negra_8));
                        CeldaImprimeTexto.Colspan = 2;
                        CeldaImprimeTexto.Border = Bordes_Ninguno;
                        CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_CENTER;
                        tabla2.AddCell(CeldaImprimeTexto);

                        String documentType = "";
                        if (documento.tipo_documento == 1)
                            documentType = "Cotización";
                        else if (documento.tipo_documento == 2)
                            documentType = "Venta";
                        else if (documento.tipo_documento == 3)
                            documentType = "Pedido";
                        else if (documento.tipo_documento == 4)
                            documentType = "Remisión";
                        else if (documento.tipo_documento == 9)
                            documentType = "Pago del Cliente";
                        else if (documento.tipo_documento == 50)
                            documentType = "Prepedido";
                        else if (documento.tipo_documento == 51)
                            documentType = "Cotización de Mostrador";
                        if (documentType.Equals(""))
                            documentType = documento.tipo_documento.ToString();
                        CeldaImprimeTexto = new PdfPCell(new Phrase(documentType, Fuente_Arial_Negra_8));
                        CeldaImprimeTexto.Colspan = 2;
                        CeldaImprimeTexto.Border = Bordes_Ninguno;
                        CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_LEFT;
                        tabla2.AddCell(CeldaImprimeTexto);

                        CeldaImprimeTexto = new PdfPCell(new Phrase(documento.fechahoramov.ToString(), Fuente_Arial_Negra_8));
                        CeldaImprimeTexto.Colspan = 4;
                        CeldaImprimeTexto.Border = Bordes_Ninguno;
                        CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_CENTER;
                        tabla2.AddCell(CeldaImprimeTexto);

                        CeldaImprimeTexto = new PdfPCell(new Phrase(documento.fventa.ToString(), Fuente_Arial_Negra_8));
                        CeldaImprimeTexto.Colspan = 3;
                        CeldaImprimeTexto.Border = Bordes_Ninguno;
                        CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_CENTER;
                        tabla2.AddCell(CeldaImprimeTexto);
                        String movimientosText = "";
                        double totalMovs = 0;
                        List<ClsMovimientosModel> movimientos = documento.movimientos;
                        if (movimientos != null)
                        {
                            foreach (var movimiento in movimientos)
                            {
                                //String fcName = ClsFormasDeCobroModel.getANameFrromAFomaDeCobroWithId(monto.formaCobroId);
                                movimientosText += movimiento.itemCode + " " + movimiento.total.ToString("C",CultureInfo.CurrentCulture) + "\r\n";
                                //total += monto.importe;
                            }
                        }
                        CeldaImprimeTexto = new PdfPCell(new Phrase(movimientosText, Fuente_Arial_Negra_8));
                        CeldaImprimeTexto.Colspan = 3;
                        CeldaImprimeTexto.Border = Bordes_Ninguno;
                        CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_CENTER;
                        tabla2.AddCell(CeldaImprimeTexto);

                        CeldaImprimeTexto = new PdfPCell(new Phrase(documento.descuento.ToString("C", CultureInfo.CurrentCulture), Fuente_Arial_Negra_8));
                        CeldaImprimeTexto.Colspan = 3;
                        CeldaImprimeTexto.Border = Bordes_Ninguno;
                        CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_CENTER;
                        tabla2.AddCell(CeldaImprimeTexto);
                        if (documento.tipo_documento == 9)
                        {
                            //double total = MontoRetiroModel.getTotalOfAWithdrawal(retiro.id);
                            CeldaImprimeTexto = new PdfPCell(new Phrase(documento.anticipo.ToString("C", CultureInfo.CurrentCulture) + " MXN", Fuente_Arial_Negra_8));
                            CeldaImprimeTexto.Colspan = 3;
                            CeldaImprimeTexto.Border = Bordes_Ninguno;
                            CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_RIGHT;
                            tabla2.AddCell(CeldaImprimeTexto);
                        }
                        else{
                            //double total = MontoRetiroModel.getTotalOfAWithdrawal(retiro.id);
                            CeldaImprimeTexto = new PdfPCell(new Phrase(documento.total.ToString("C", CultureInfo.CurrentCulture) + " MXN", Fuente_Arial_Negra_8));
                            CeldaImprimeTexto.Colspan = 3;
                            CeldaImprimeTexto.Border = Bordes_Ninguno;
                            CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_RIGHT;
                            tabla2.AddCell(CeldaImprimeTexto);
                        }

                        totalDocumentos += documento.total;
                        //TotalValor += liquidacionProducto.Valor;
                    }
                }

                PdfPTable Tabla3 = new PdfPTable(10);
                Tabla3.TotalWidth = dimension;
                Tabla3.LockedWidth = true;

                #region ENCABEZADO
                CeldaImprimeTexto = new PdfPCell(new Phrase("", Fuente_Arial_Azul_Bold_12));
                CeldaImprimeTexto.Colspan = 1;
                CeldaImprimeTexto.Border = Bordes_Ninguno;
                CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_LEFT;
                Tabla3.AddCell(CeldaImprimeTexto);

                CeldaImprimeTexto = new PdfPCell(new Phrase("", Fuente_Arial_Azul_Bold_10));
                CeldaImprimeTexto.Colspan = 4;
                CeldaImprimeTexto.Border = Bordes_Ninguno;
                CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_CENTER;
                Tabla3.AddCell(CeldaImprimeTexto);

                CeldaImprimeTexto = new PdfPCell(new Phrase("", Fuente_Arial_Azul_Bold_10));
                CeldaImprimeTexto.Colspan = 3;
                CeldaImprimeTexto.Border = Bordes_Ninguno;
                CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_CENTER;
                Tabla3.AddCell(CeldaImprimeTexto);

                CeldaImprimeTexto = new PdfPCell(new Phrase("Total General", Fuente_Arial_Azul_Bold_12));
                CeldaImprimeTexto.Colspan = 4;
                CeldaImprimeTexto.Border = Bordes_Ninguno;
                CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_CENTER;
                Tabla3.AddCell(CeldaImprimeTexto);

                CeldaImprimeTexto = new PdfPCell(new Phrase("", Fuente_Arial_Azul_Bold_12));
                CeldaImprimeTexto.Colspan = 1;
                CeldaImprimeTexto.Border = Bordes_Ninguno;
                CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_LEFT;
                Tabla3.AddCell(CeldaImprimeTexto);

                CeldaImprimeTexto = new PdfPCell(new Phrase("", Fuente_Arial_Azul_Bold_10));
                CeldaImprimeTexto.Colspan = 4;
                CeldaImprimeTexto.Border = Bordes_Ninguno;
                CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_CENTER;
                Tabla3.AddCell(CeldaImprimeTexto);

                CeldaImprimeTexto = new PdfPCell(new Phrase("", Fuente_Arial_Azul_Bold_10));
                CeldaImprimeTexto.Colspan = 3;
                CeldaImprimeTexto.Border = Bordes_Ninguno;
                CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_CENTER;
                Tabla3.AddCell(CeldaImprimeTexto);

                CeldaImprimeTexto = new PdfPCell(new Phrase(totalDocumentos.ToString("C") + " MXN", Fuente_Arial_Azul_Bold_12));
                CeldaImprimeTexto.Colspan = 4;
                CeldaImprimeTexto.Border = Bordes_Superior;
                CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_CENTER;
                Tabla3.AddCell(CeldaImprimeTexto);
                #endregion


                #region T4
                /*PdfPTable Tabla4 = new PdfPTable(20);
                Tabla4.TotalWidth = dimension;
                Tabla4.LockedWidth = true;

                //ESPACIO
                CeldaImprimeTexto = new PdfPCell(new Phrase(Sangria_1, Fuente_Arial_Azul_Bold_9));
                CeldaImprimeTexto.Colspan = 20;
                CeldaImprimeTexto.Border = Bordes_Superior;
                CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_RIGHT;
                Tabla4.AddCell(CeldaImprimeTexto);

                //LINEAS DE TOTALES GENERALES
                #region EFECTIVO ENTREGADO
                CeldaImprimeTexto = new PdfPCell(new Phrase("", Fuente_Arial_Azul_Bold_9));
                CeldaImprimeTexto.Colspan = 2;
                CeldaImprimeTexto.Border = Bordes_Ninguno;
                CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_RIGHT;
                Tabla4.AddCell(CeldaImprimeTexto);

                CeldaImprimeTexto = new PdfPCell(new Phrase("", Fuente_Arial_Azul_Bold_9));
                CeldaImprimeTexto.Colspan = 3;
                CeldaImprimeTexto.Border = Bordes_Ninguno;
                CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_RIGHT;
                Tabla4.AddCell(CeldaImprimeTexto);

                CeldaImprimeTexto = new PdfPCell(new Phrase("EFECTIVO ENTREGADO", Fuente_Arial_Azul_Bold_9));
                CeldaImprimeTexto.Colspan = 5;
                CeldaImprimeTexto.Border = Bordes_Ninguno;
                CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_LEFT;
                Tabla4.AddCell(CeldaImprimeTexto);

                CeldaImprimeTexto = new PdfPCell(new Phrase(FormatearCadena_Moneda(TotalEfectivo), Fuente_Arial_Azul_Bold_9));
                CeldaImprimeTexto.Colspan = 2;
                CeldaImprimeTexto.Border = Bordes_Ninguno;
                CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_RIGHT;
                Tabla4.AddCell(CeldaImprimeTexto);

                CeldaImprimeTexto = new PdfPCell(new Phrase("", Fuente_Arial_Azul_Bold_9));
                CeldaImprimeTexto.Colspan = 6;
                CeldaImprimeTexto.Border = Bordes_Ninguno;
                CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_RIGHT;
                Tabla4.AddCell(CeldaImprimeTexto);

                CeldaImprimeTexto = new PdfPCell(new Phrase("", Fuente_Arial_Azul_Bold_9));
                CeldaImprimeTexto.Colspan = 2;
                CeldaImprimeTexto.Border = Bordes_Ninguno;
                CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_RIGHT;
                Tabla4.AddCell(CeldaImprimeTexto);
                #endregion

                #region NOTAS
                CeldaImprimeTexto = new PdfPCell(new Phrase("", Fuente_Arial_Azul_Bold_9));
                CeldaImprimeTexto.Colspan = 2;
                CeldaImprimeTexto.Border = Bordes_Ninguno;
                CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_RIGHT;
                Tabla4.AddCell(CeldaImprimeTexto);

                CeldaImprimeTexto = new PdfPCell(new Phrase("", Fuente_Arial_Azul_Bold_9));
                CeldaImprimeTexto.Colspan = 3;
                CeldaImprimeTexto.Border = Bordes_Ninguno;
                CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_RIGHT;
                Tabla4.AddCell(CeldaImprimeTexto);

                CeldaImprimeTexto = new PdfPCell(new Phrase("NOTAS", Fuente_Arial_Azul_Bold_9));
                CeldaImprimeTexto.Colspan = 5;
                CeldaImprimeTexto.Border = Bordes_Ninguno;
                CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_LEFT;
                Tabla4.AddCell(CeldaImprimeTexto);

                CeldaImprimeTexto = new PdfPCell(new Phrase(FormatearCadena_Moneda(TotalNotas), Fuente_Arial_Azul_Bold_9));
                CeldaImprimeTexto.Colspan = 2;
                CeldaImprimeTexto.Border = Bordes_Ninguno;
                CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_RIGHT;
                Tabla4.AddCell(CeldaImprimeTexto);

                CeldaImprimeTexto = new PdfPCell(new Phrase("", Fuente_Arial_Azul_Bold_9));
                CeldaImprimeTexto.Colspan = 6;
                CeldaImprimeTexto.Border = Bordes_Ninguno;
                CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_RIGHT;
                Tabla4.AddCell(CeldaImprimeTexto);

                CeldaImprimeTexto = new PdfPCell(new Phrase("", Fuente_Arial_Azul_Bold_9));
                CeldaImprimeTexto.Colspan = 2;
                CeldaImprimeTexto.Border = Bordes_Ninguno;
                CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_RIGHT;
                Tabla4.AddCell(CeldaImprimeTexto);
                #endregion

                #region C. COMERCIALES
                CeldaImprimeTexto = new PdfPCell(new Phrase("", Fuente_Arial_Azul_Bold_9));
                CeldaImprimeTexto.Colspan = 2;
                CeldaImprimeTexto.Border = Bordes_Ninguno;
                CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_RIGHT;
                Tabla4.AddCell(CeldaImprimeTexto);

                CeldaImprimeTexto = new PdfPCell(new Phrase("", Fuente_Arial_Azul_Bold_9));
                CeldaImprimeTexto.Colspan = 3;
                CeldaImprimeTexto.Border = Bordes_Ninguno;
                CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_RIGHT;
                Tabla4.AddCell(CeldaImprimeTexto);

                CeldaImprimeTexto = new PdfPCell(new Phrase("C. COMERCIALES", Fuente_Arial_Azul_Bold_9));
                CeldaImprimeTexto.Colspan = 5;
                CeldaImprimeTexto.Border = Bordes_Ninguno;
                CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_LEFT;
                Tabla4.AddCell(CeldaImprimeTexto);

                CeldaImprimeTexto = new PdfPCell(new Phrase(FormatearCadena_Moneda(TotalCentrosComerciales), Fuente_Arial_Azul_Bold_9));
                CeldaImprimeTexto.Colspan = 2;
                CeldaImprimeTexto.Border = Bordes_Ninguno;
                CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_RIGHT;
                Tabla4.AddCell(CeldaImprimeTexto);

                CeldaImprimeTexto = new PdfPCell(new Phrase("", Fuente_Arial_Azul_Bold_9));
                CeldaImprimeTexto.Colspan = 6;
                CeldaImprimeTexto.Border = Bordes_Ninguno;
                CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_RIGHT;
                Tabla4.AddCell(CeldaImprimeTexto);

                CeldaImprimeTexto = new PdfPCell(new Phrase("", Fuente_Arial_Azul_Bold_9));
                CeldaImprimeTexto.Colspan = 2;
                CeldaImprimeTexto.Border = Bordes_Ninguno;
                CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_RIGHT;
                Tabla4.AddCell(CeldaImprimeTexto);
                #endregion

                #region TOTAL EFECTIVO Y NOTAS
                CeldaImprimeTexto = new PdfPCell(new Phrase("", Fuente_Arial_Azul_Bold_9));
                CeldaImprimeTexto.Colspan = 2;
                CeldaImprimeTexto.Border = Bordes_Ninguno;
                CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_RIGHT;
                Tabla4.AddCell(CeldaImprimeTexto);

                CeldaImprimeTexto = new PdfPCell(new Phrase("", Fuente_Arial_Azul_Bold_9));
                CeldaImprimeTexto.Colspan = 3;
                CeldaImprimeTexto.Border = Bordes_Ninguno;
                CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_RIGHT;
                Tabla4.AddCell(CeldaImprimeTexto);

                CeldaImprimeTexto = new PdfPCell(new Phrase("TOTAL EFECTIVO Y NOTAS", Fuente_Arial_Azul_Bold_9));
                CeldaImprimeTexto.Colspan = 5;
                CeldaImprimeTexto.Border = Bordes_Ninguno;
                CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_LEFT;
                Tabla4.AddCell(CeldaImprimeTexto);

                CeldaImprimeTexto = new PdfPCell(new Phrase(FormatearCadena_Moneda(TotalEfectivoNotas), Fuente_Arial_Azul_Bold_9));
                CeldaImprimeTexto.Colspan = 2;
                CeldaImprimeTexto.Border = Bordes_Ninguno;
                CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_RIGHT;
                Tabla4.AddCell(CeldaImprimeTexto);

                CeldaImprimeTexto = new PdfPCell(new Phrase("", Fuente_Arial_Azul_Bold_9));
                CeldaImprimeTexto.Colspan = 6;
                CeldaImprimeTexto.Border = Bordes_Ninguno;
                CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_RIGHT;
                Tabla4.AddCell(CeldaImprimeTexto);

                CeldaImprimeTexto = new PdfPCell(new Phrase("", Fuente_Arial_Azul_Bold_9));
                CeldaImprimeTexto.Colspan = 2;
                CeldaImprimeTexto.Border = Bordes_Ninguno;
                CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_RIGHT;
                Tabla4.AddCell(CeldaImprimeTexto);
                #endregion

                #region DIFERENCIA
                CeldaImprimeTexto = new PdfPCell(new Phrase("", Fuente_Arial_Azul_Bold_9));
                CeldaImprimeTexto.Colspan = 2;
                CeldaImprimeTexto.Border = Bordes_Ninguno;
                CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_RIGHT;
                Tabla4.AddCell(CeldaImprimeTexto);

                CeldaImprimeTexto = new PdfPCell(new Phrase("", Fuente_Arial_Azul_Bold_9));
                CeldaImprimeTexto.Colspan = 3;
                CeldaImprimeTexto.Border = Bordes_Ninguno;
                CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_RIGHT;
                Tabla4.AddCell(CeldaImprimeTexto);

                CeldaImprimeTexto = new PdfPCell(new Phrase("DIFERENCIA", Fuente_Arial_Azul_Bold_9));
                CeldaImprimeTexto.Colspan = 5;
                CeldaImprimeTexto.Border = Bordes_Ninguno;
                CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_LEFT;
                Tabla4.AddCell(CeldaImprimeTexto);

                /*CeldaImprimeTexto = new PdfPCell(new Phrase(FormatearCadena_Moneda(TotalDiferencia), Fuente_Arial_Azul_Bold_9));
                CeldaImprimeTexto.Colspan = 2;
                CeldaImprimeTexto.Border = Bordes_Ninguno;
                CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_RIGHT;
                Tabla4.AddCell(CeldaImprimeTexto);

                CeldaImprimeTexto = new PdfPCell(new Phrase("", Fuente_Arial_Azul_Bold_9));
                CeldaImprimeTexto.Colspan = 6;
                CeldaImprimeTexto.Border = Bordes_Ninguno;
                CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_RIGHT;
                Tabla4.AddCell(CeldaImprimeTexto);

                CeldaImprimeTexto = new PdfPCell(new Phrase("", Fuente_Arial_Azul_Bold_9));
                CeldaImprimeTexto.Colspan = 2;
                CeldaImprimeTexto.Border = Bordes_Ninguno;
                CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_RIGHT;
                Tabla4.AddCell(CeldaImprimeTexto);
                #endregion

                CeldaImprimeTexto = new PdfPCell(new Phrase(Sangria_1, Fuente_Arial_Azul_Bold_9));
                CeldaImprimeTexto.Colspan = 1;
                CeldaImprimeTexto.Border = Bordes_Ninguno;
                CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_RIGHT;
                Tabla4.AddCell(CeldaImprimeTexto);

                CeldaImprimeTexto = new PdfPCell(new Phrase(Sangria_1, Fuente_Arial_Azul_Bold_9));
                CeldaImprimeTexto.Colspan = 18;
                CeldaImprimeTexto.Border = Bordes_Inferior;
                CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_RIGHT;
                Tabla4.AddCell(CeldaImprimeTexto);

                CeldaImprimeTexto = new PdfPCell(new Phrase(Sangria_1, Fuente_Arial_Azul_Bold_9));
                CeldaImprimeTexto.Colspan = 1;
                CeldaImprimeTexto.Border = Bordes_Ninguno;
                CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_RIGHT;
                Tabla4.AddCell(CeldaImprimeTexto);*/

                #endregion

                #region T5
                /*PdfPTable Tabla5 = new PdfPTable(20);
                Tabla5.TotalWidth = dimension;
                Tabla5.LockedWidth = true;

                #region Espacios para firmar
                CeldaImprimeTexto = new PdfPCell(new Phrase(Sangria_1, Fuente_Arial_Azul_Bold_9));
                CeldaImprimeTexto.Colspan = 20;
                CeldaImprimeTexto.Border = Bordes_Ninguno;
                CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_RIGHT;
                Tabla5.AddCell(CeldaImprimeTexto);

                CeldaImprimeTexto = new PdfPCell(new Phrase(Sangria_1, Fuente_Arial_Azul_Bold_9));
                CeldaImprimeTexto.Colspan = 20;
                CeldaImprimeTexto.Border = Bordes_Ninguno;
                CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_RIGHT;
                Tabla5.AddCell(CeldaImprimeTexto);

                CeldaImprimeTexto = new PdfPCell(new Phrase(Sangria_1, Fuente_Arial_Azul_Bold_9));
                CeldaImprimeTexto.Colspan = 20;
                CeldaImprimeTexto.Border = Bordes_Ninguno;
                CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_RIGHT;
                Tabla5.AddCell(CeldaImprimeTexto);

                CeldaImprimeTexto = new PdfPCell(new Phrase(Sangria_1, Fuente_Arial_Azul_Bold_9));
                CeldaImprimeTexto.Colspan = 20;
                CeldaImprimeTexto.Border = Bordes_Ninguno;
                CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_RIGHT;
                Tabla5.AddCell(CeldaImprimeTexto);

                CeldaImprimeTexto = new PdfPCell(new Phrase(Sangria_1, Fuente_Arial_Azul_Bold_9));
                CeldaImprimeTexto.Colspan = 20;
                CeldaImprimeTexto.Border = Bordes_Ninguno;
                CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_RIGHT;
                Tabla5.AddCell(CeldaImprimeTexto);
                #endregion

                #region Firmas_Lineas
                string linea = "______________________________";
                CeldaImprimeTexto = new PdfPCell(new Phrase(linea, Fuente_Arial_Azul_Bold_9));
                CeldaImprimeTexto.Colspan = 10;
                CeldaImprimeTexto.Border = Bordes_Ninguno;
                CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_CENTER;
                Tabla5.AddCell(CeldaImprimeTexto);

                CeldaImprimeTexto = new PdfPCell(new Phrase(linea, Fuente_Arial_Azul_Bold_9));
                CeldaImprimeTexto.Colspan = 10;
                CeldaImprimeTexto.Border = Bordes_Ninguno;
                CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_CENTER;
                Tabla5.AddCell(CeldaImprimeTexto);
                #endregion

                #region Firmas_Nombres

                CeldaImprimeTexto = new PdfPCell(new Phrase("Administrador de Almacén", Fuente_Arial_Azul_Bold_9));
                CeldaImprimeTexto.Colspan = 10;
                CeldaImprimeTexto.Border = Bordes_Ninguno;
                CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_CENTER;
                Tabla5.AddCell(CeldaImprimeTexto);

                CeldaImprimeTexto = new PdfPCell(new Phrase(Agente.nombre, Fuente_Arial_Azul_Bold_9));
                CeldaImprimeTexto.Colspan = 10;
                CeldaImprimeTexto.Border = Bordes_Ninguno;
                CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_CENTER;
                Tabla5.AddCell(CeldaImprimeTexto);*/

                #endregion

                //#endregion

                //Agrega las tablas con la información al documento.
                document.Add(TablaEncabezado);
                document.Add(TablaEspacio);
                document.Add(tabla2);
                document.Add(TablaEspacio);
                document.Add(TablaEspacio);
                document.Add(TablaEspacio);
                document.Add(Tabla3);
                //document.Add(Tabla4);
                //document.Add(Tabla5);

                document.Close();
                try
                {
                    if (File.Exists(destinationFile))
                    {
                        File.Delete(destinationFile);
                    }
                    File.Move(sourceFile, destinationFile);
                    created = true;
                }
                catch (Exception ex)
                {
                    //MensajeError = ex.Message;
                    SECUDOC.writeLog("-Crear PDF de Retiros-");
                    SECUDOC.writeLog(ex.ToString());
                }
            });
            return created;
        }

        public async Task<bool> createPdfDocumentsDetallesBDLOCAL(String nombreEmpresa, String rutaArchivo, String nameReport, int userID, bool permissionPrepedido,
            String startDate, String endDate, int lastId, int LIMIT,String horaInicio, String horaFin)
        {
            bool created = false;
            await Task.Run(async () =>
            {
                List<dynamic> documentos = null;
                GetDataService gds = new GetDataService();
                dynamic respuesta = null;

                respuesta = FormGeneralsReports.fillDataGridDocumentsDBLocal(startDate, endDate, horaInicio, horaFin);
                if (respuesta != null)
                {
                    int value = respuesta.value;
                    String description = respuesta.description;
                    if (value < 0)
                    {
                        FormMessage fm = new FormMessage("Exception", description, 2);
                        fm.ShowDialog();
                    }
                    else
                    {
                        
                        if (value > 0)
                        {
                            documentos = respuesta.documentos;
                        }
                        else if (value == 0)
                        {
                            FormMessage formMessage = new FormMessage("No se encontro información", description, 3);
                            formMessage.ShowDialog();
                        }
                        else
                        {
                            FormMessage formMessage = new FormMessage("Exception", description, 2);
                            formMessage.ShowDialog();
                        }
                        
                    }
                }
                else
                {

                }
                //Creates an instance of the iTextSharp.text.Document-object:
                //Document document = new Document(PageSize.LETTER, 30, 30, 20, 30);
                //                                tamañoPagina, mIzq, mDer, mTop, mBot                            
                Document document = new Document(PageSize.LETTER, 30, 30, 9, 1);
                //float dimension = 570f;
                float dimension = 556f;
                string sourceFile = Application.StartupPath + @"\Temp.pdf";
                string destinationFile = rutaArchivo;
                PdfWriter.GetInstance(document, new FileStream(sourceFile, FileMode.Create));
                document.Open();

                //Agrega una tabla (espacio en blanco)
                PdfPTable TablaEspacio = new PdfPTable(1);
                TablaEspacio.TotalWidth = dimension;
                TablaEspacio.LockedWidth = true;
                PdfPCell CeldaEspacio = new PdfPCell(new Phrase(" ", Fuente_Espacio_2));
                CeldaEspacio.Border = Bordes_Ninguno;
                TablaEspacio.AddCell(CeldaEspacio);

                PdfPCell CeldaImprimeTexto;

                //Comienza llenado de archivo
                PdfPTable TablaEncabezado = new PdfPTable(1);
                TablaEncabezado.TotalWidth = dimension;
                TablaEncabezado.LockedWidth = true;

                CeldaImprimeTexto = new PdfPCell(new Phrase(nombreEmpresa, Fuente_Arial_Azul_Bold_16));
                CeldaImprimeTexto.Colspan = 1;
                CeldaImprimeTexto.Border = Bordes_Ninguno;
                CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_CENTER;
                TablaEncabezado.AddCell(CeldaImprimeTexto);

                CeldaImprimeTexto = new PdfPCell(new Phrase("Documentos", Fuente_Arial_Azul_Bold_12));
                CeldaImprimeTexto.Colspan = 1;
                CeldaImprimeTexto.Border = Bordes_Ninguno;
                CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_CENTER;
                TablaEncabezado.AddCell(CeldaImprimeTexto);

                CeldaImprimeTexto = new PdfPCell(new Phrase(Sangria_2, Fuente_Arial_Azul_Bold_12));
                CeldaImprimeTexto.Colspan = 1;
                CeldaImprimeTexto.Border = Bordes_Ninguno;
                CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_CENTER;
                TablaEncabezado.AddCell(CeldaImprimeTexto);

                PdfPTable tabla2 = new PdfPTable(20);
                tabla2.TotalWidth = dimension;
                tabla2.LockedWidth = true;

                CeldaImprimeTexto = new PdfPCell(new Phrase("Cliente", Fuente_Arial_Negra_8));
                CeldaImprimeTexto.Colspan = 2;
                CeldaImprimeTexto.Border = Bordes_Inferior;
                CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_CENTER;
                tabla2.AddCell(CeldaImprimeTexto);

                CeldaImprimeTexto = new PdfPCell(new Phrase("Tipo", Fuente_Arial_Negra_8));
                CeldaImprimeTexto.Colspan = 2;
                CeldaImprimeTexto.Border = Bordes_Inferior;
                CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_CENTER;
                tabla2.AddCell(CeldaImprimeTexto);

                CeldaImprimeTexto = new PdfPCell(new Phrase("Fecha", Fuente_Arial_Negra_8));
                CeldaImprimeTexto.Colspan = 4;
                CeldaImprimeTexto.Border = Bordes_Inferior;
                CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_CENTER;
                tabla2.AddCell(CeldaImprimeTexto);

                CeldaImprimeTexto = new PdfPCell(new Phrase("Folio", Fuente_Arial_Negra_8));
                CeldaImprimeTexto.Colspan = 3;
                CeldaImprimeTexto.Border = Bordes_Inferior;
                CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_CENTER;
                tabla2.AddCell(CeldaImprimeTexto);

                CeldaImprimeTexto = new PdfPCell(new Phrase("Movimientos", Fuente_Arial_Negra_8));
                CeldaImprimeTexto.Colspan = 3;
                CeldaImprimeTexto.Border = Bordes_Inferior;
                CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_CENTER;
                tabla2.AddCell(CeldaImprimeTexto);

                CeldaImprimeTexto = new PdfPCell(new Phrase("Descuento", Fuente_Arial_Negra_8));
                CeldaImprimeTexto.Colspan = 3;
                CeldaImprimeTexto.Border = Bordes_Inferior;
                CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_CENTER;
                tabla2.AddCell(CeldaImprimeTexto);

                CeldaImprimeTexto = new PdfPCell(new Phrase("Total", Fuente_Arial_Negra_8));
                CeldaImprimeTexto.Colspan = 3;
                CeldaImprimeTexto.Border = Bordes_Inferior;
                CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_CENTER;
                tabla2.AddCell(CeldaImprimeTexto);

                double totalDocumentos = 0;
                decimal TotalValor = 0;
                if (documentos != null)
                {
                    foreach (var documento in documentos)
                    {
                        // TotalVentas += liquidacionProducto.Valor;
                        CeldaImprimeTexto = new PdfPCell(new Phrase(documento.clave_cliente.ToString(), Fuente_Arial_Negra_8));
                        CeldaImprimeTexto.Colspan = 2;
                        CeldaImprimeTexto.Border = Bordes_Ninguno;
                        CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_CENTER;
                        tabla2.AddCell(CeldaImprimeTexto);

                        String documentType = "";
                        if (documento.tipo_documento == 1)
                            documentType = "Cotización";
                        else if (documento.tipo_documento == 2)
                            documentType = "Venta";
                        else if (documento.tipo_documento == 3)
                            documentType = "Pedido";
                        else if (documento.tipo_documento == 4)
                            documentType = "Remisión";
                        else if (documento.tipo_documento == 9)
                            documentType = "Pago del Cliente";
                        else if (documento.tipo_documento == 50)
                            documentType = "Prepedido";
                        else if (documento.tipo_documento == 51)
                            documentType = "Cotización de Mostrador";
                        if (documentType.Equals(""))
                            documentType = documento.tipo_documento.ToString();
                        CeldaImprimeTexto = new PdfPCell(new Phrase(documentType, Fuente_Arial_Negra_8));
                        CeldaImprimeTexto.Colspan = 2;
                        CeldaImprimeTexto.Border = Bordes_Ninguno;
                        CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_LEFT;
                        tabla2.AddCell(CeldaImprimeTexto);

                        CeldaImprimeTexto = new PdfPCell(new Phrase(documento.fechahoramov.ToString(), Fuente_Arial_Negra_8));
                        CeldaImprimeTexto.Colspan = 4;
                        CeldaImprimeTexto.Border = Bordes_Ninguno;
                        CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_CENTER;
                        tabla2.AddCell(CeldaImprimeTexto);

                        CeldaImprimeTexto = new PdfPCell(new Phrase(documento.fventa.ToString(), Fuente_Arial_Negra_8));
                        CeldaImprimeTexto.Colspan = 3;
                        CeldaImprimeTexto.Border = Bordes_Ninguno;
                        CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_CENTER;
                        tabla2.AddCell(CeldaImprimeTexto);
                        String movimientosText = "";
                        double totalMovs = 0;
                        movimientosText = documento.totalMovimientos+"";
                        
                        CeldaImprimeTexto = new PdfPCell(new Phrase(movimientosText, Fuente_Arial_Negra_8));
                        CeldaImprimeTexto.Colspan = 3;
                        CeldaImprimeTexto.Border = Bordes_Ninguno;
                        CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_CENTER;
                        tabla2.AddCell(CeldaImprimeTexto);

                        CeldaImprimeTexto = new PdfPCell(new Phrase(documento.descuento.ToString("C", CultureInfo.CurrentCulture), Fuente_Arial_Negra_8));
                        CeldaImprimeTexto.Colspan = 3;
                        CeldaImprimeTexto.Border = Bordes_Ninguno;
                        CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_CENTER;
                        tabla2.AddCell(CeldaImprimeTexto);
                        if (documento.tipo_documento == 9)
                        {
                            //double total = MontoRetiroModel.getTotalOfAWithdrawal(retiro.id);
                            CeldaImprimeTexto = new PdfPCell(new Phrase(documento.anticipo.ToString("C", CultureInfo.CurrentCulture) + " MXN", Fuente_Arial_Negra_8));
                            CeldaImprimeTexto.Colspan = 3;
                            CeldaImprimeTexto.Border = Bordes_Ninguno;
                            CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_RIGHT;
                            tabla2.AddCell(CeldaImprimeTexto);
                        }
                        else
                        {
                            //double total = MontoRetiroModel.getTotalOfAWithdrawal(retiro.id);
                            CeldaImprimeTexto = new PdfPCell(new Phrase(documento.total.ToString("C", CultureInfo.CurrentCulture) + " MXN", Fuente_Arial_Negra_8));
                            CeldaImprimeTexto.Colspan = 3;
                            CeldaImprimeTexto.Border = Bordes_Ninguno;
                            CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_RIGHT;
                            tabla2.AddCell(CeldaImprimeTexto);
                        }

                        totalDocumentos += documento.total;
                        //TotalValor += liquidacionProducto.Valor;
                        List<dynamic> movimientos = new List<dynamic>();
                        try
                        {
                            movimientos = documento.movimientos;
                        }
                        catch(Exception e)
                        {
                            SECUDOC.writeLog(e.ToString());
                        }
                        for (int i =0; i < movimientos.Count; i++)
                        {
                            movimientosText = documento.totalMovimientos + "";
                            CeldaImprimeTexto = new PdfPCell(new Phrase(movimientos[i].name.ToString("C", CultureInfo.CurrentCulture), Fuente_Arial_Negra_6));
                            CeldaImprimeTexto.Colspan = 3;
                            CeldaImprimeTexto.Border = Bordes_Ninguno;
                            CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_CENTER;
                            tabla2.AddCell(CeldaImprimeTexto);

                            CeldaImprimeTexto = new PdfPCell(new Phrase(movimientos[i].CLAVE_ART.ToString("C", CultureInfo.CurrentCulture), Fuente_Arial_Negra_6));
                            CeldaImprimeTexto.Colspan = 2;
                            CeldaImprimeTexto.Border = Bordes_Ninguno;
                            CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_LEFT;
                            tabla2.AddCell(CeldaImprimeTexto);

                            CeldaImprimeTexto = new PdfPCell(new Phrase(movimientos[i].TOTAL.ToString("C", CultureInfo.CurrentCulture), Fuente_Arial_Negra_6));
                            CeldaImprimeTexto.Colspan = 4;
                            CeldaImprimeTexto.Border = Bordes_Ninguno;
                            CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_CENTER;
                            tabla2.AddCell(CeldaImprimeTexto);

                            CeldaImprimeTexto = new PdfPCell(new Phrase(movimientos[i].unidades_capturadas.ToString("C", CultureInfo.CurrentCulture), Fuente_Arial_Negra_6));
                            CeldaImprimeTexto.Colspan = 3;
                            CeldaImprimeTexto.Border = Bordes_Ninguno;
                            CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_CENTER;
                            tabla2.AddCell(CeldaImprimeTexto);
                        }
                    }
                }

                PdfPTable Tabla3 = new PdfPTable(10);
                Tabla3.TotalWidth = dimension;
                Tabla3.LockedWidth = true;

                #region ENCABEZADO
                CeldaImprimeTexto = new PdfPCell(new Phrase("", Fuente_Arial_Azul_Bold_12));
                CeldaImprimeTexto.Colspan = 1;
                CeldaImprimeTexto.Border = Bordes_Ninguno;
                CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_LEFT;
                Tabla3.AddCell(CeldaImprimeTexto);

                CeldaImprimeTexto = new PdfPCell(new Phrase("", Fuente_Arial_Azul_Bold_10));
                CeldaImprimeTexto.Colspan = 4;
                CeldaImprimeTexto.Border = Bordes_Ninguno;
                CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_CENTER;
                Tabla3.AddCell(CeldaImprimeTexto);

                CeldaImprimeTexto = new PdfPCell(new Phrase("", Fuente_Arial_Azul_Bold_10));
                CeldaImprimeTexto.Colspan = 3;
                CeldaImprimeTexto.Border = Bordes_Ninguno;
                CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_CENTER;
                Tabla3.AddCell(CeldaImprimeTexto);

                CeldaImprimeTexto = new PdfPCell(new Phrase("Total General", Fuente_Arial_Azul_Bold_12));
                CeldaImprimeTexto.Colspan = 4;
                CeldaImprimeTexto.Border = Bordes_Ninguno;
                CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_CENTER;
                Tabla3.AddCell(CeldaImprimeTexto);

                CeldaImprimeTexto = new PdfPCell(new Phrase("", Fuente_Arial_Azul_Bold_12));
                CeldaImprimeTexto.Colspan = 1;
                CeldaImprimeTexto.Border = Bordes_Ninguno;
                CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_LEFT;
                Tabla3.AddCell(CeldaImprimeTexto);

                CeldaImprimeTexto = new PdfPCell(new Phrase("", Fuente_Arial_Azul_Bold_10));
                CeldaImprimeTexto.Colspan = 4;
                CeldaImprimeTexto.Border = Bordes_Ninguno;
                CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_CENTER;
                Tabla3.AddCell(CeldaImprimeTexto);

                CeldaImprimeTexto = new PdfPCell(new Phrase("", Fuente_Arial_Azul_Bold_10));
                CeldaImprimeTexto.Colspan = 3;
                CeldaImprimeTexto.Border = Bordes_Ninguno;
                CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_CENTER;
                Tabla3.AddCell(CeldaImprimeTexto);

                CeldaImprimeTexto = new PdfPCell(new Phrase(totalDocumentos.ToString("C") + " MXN", Fuente_Arial_Azul_Bold_12));
                CeldaImprimeTexto.Colspan = 4;
                CeldaImprimeTexto.Border = Bordes_Superior;
                CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_CENTER;
                Tabla3.AddCell(CeldaImprimeTexto);
                #endregion


                #region T4
                /*PdfPTable Tabla4 = new PdfPTable(20);
                Tabla4.TotalWidth = dimension;
                Tabla4.LockedWidth = true;

                //ESPACIO
                CeldaImprimeTexto = new PdfPCell(new Phrase(Sangria_1, Fuente_Arial_Azul_Bold_9));
                CeldaImprimeTexto.Colspan = 20;
                CeldaImprimeTexto.Border = Bordes_Superior;
                CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_RIGHT;
                Tabla4.AddCell(CeldaImprimeTexto);

                //LINEAS DE TOTALES GENERALES
                #region EFECTIVO ENTREGADO
                CeldaImprimeTexto = new PdfPCell(new Phrase("", Fuente_Arial_Azul_Bold_9));
                CeldaImprimeTexto.Colspan = 2;
                CeldaImprimeTexto.Border = Bordes_Ninguno;
                CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_RIGHT;
                Tabla4.AddCell(CeldaImprimeTexto);

                CeldaImprimeTexto = new PdfPCell(new Phrase("", Fuente_Arial_Azul_Bold_9));
                CeldaImprimeTexto.Colspan = 3;
                CeldaImprimeTexto.Border = Bordes_Ninguno;
                CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_RIGHT;
                Tabla4.AddCell(CeldaImprimeTexto);

                CeldaImprimeTexto = new PdfPCell(new Phrase("EFECTIVO ENTREGADO", Fuente_Arial_Azul_Bold_9));
                CeldaImprimeTexto.Colspan = 5;
                CeldaImprimeTexto.Border = Bordes_Ninguno;
                CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_LEFT;
                Tabla4.AddCell(CeldaImprimeTexto);

                CeldaImprimeTexto = new PdfPCell(new Phrase(FormatearCadena_Moneda(TotalEfectivo), Fuente_Arial_Azul_Bold_9));
                CeldaImprimeTexto.Colspan = 2;
                CeldaImprimeTexto.Border = Bordes_Ninguno;
                CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_RIGHT;
                Tabla4.AddCell(CeldaImprimeTexto);

                CeldaImprimeTexto = new PdfPCell(new Phrase("", Fuente_Arial_Azul_Bold_9));
                CeldaImprimeTexto.Colspan = 6;
                CeldaImprimeTexto.Border = Bordes_Ninguno;
                CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_RIGHT;
                Tabla4.AddCell(CeldaImprimeTexto);

                CeldaImprimeTexto = new PdfPCell(new Phrase("", Fuente_Arial_Azul_Bold_9));
                CeldaImprimeTexto.Colspan = 2;
                CeldaImprimeTexto.Border = Bordes_Ninguno;
                CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_RIGHT;
                Tabla4.AddCell(CeldaImprimeTexto);
                #endregion

                #region NOTAS
                CeldaImprimeTexto = new PdfPCell(new Phrase("", Fuente_Arial_Azul_Bold_9));
                CeldaImprimeTexto.Colspan = 2;
                CeldaImprimeTexto.Border = Bordes_Ninguno;
                CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_RIGHT;
                Tabla4.AddCell(CeldaImprimeTexto);

                CeldaImprimeTexto = new PdfPCell(new Phrase("", Fuente_Arial_Azul_Bold_9));
                CeldaImprimeTexto.Colspan = 3;
                CeldaImprimeTexto.Border = Bordes_Ninguno;
                CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_RIGHT;
                Tabla4.AddCell(CeldaImprimeTexto);

                CeldaImprimeTexto = new PdfPCell(new Phrase("NOTAS", Fuente_Arial_Azul_Bold_9));
                CeldaImprimeTexto.Colspan = 5;
                CeldaImprimeTexto.Border = Bordes_Ninguno;
                CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_LEFT;
                Tabla4.AddCell(CeldaImprimeTexto);

                CeldaImprimeTexto = new PdfPCell(new Phrase(FormatearCadena_Moneda(TotalNotas), Fuente_Arial_Azul_Bold_9));
                CeldaImprimeTexto.Colspan = 2;
                CeldaImprimeTexto.Border = Bordes_Ninguno;
                CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_RIGHT;
                Tabla4.AddCell(CeldaImprimeTexto);

                CeldaImprimeTexto = new PdfPCell(new Phrase("", Fuente_Arial_Azul_Bold_9));
                CeldaImprimeTexto.Colspan = 6;
                CeldaImprimeTexto.Border = Bordes_Ninguno;
                CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_RIGHT;
                Tabla4.AddCell(CeldaImprimeTexto);

                CeldaImprimeTexto = new PdfPCell(new Phrase("", Fuente_Arial_Azul_Bold_9));
                CeldaImprimeTexto.Colspan = 2;
                CeldaImprimeTexto.Border = Bordes_Ninguno;
                CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_RIGHT;
                Tabla4.AddCell(CeldaImprimeTexto);
                #endregion

                #region C. COMERCIALES
                CeldaImprimeTexto = new PdfPCell(new Phrase("", Fuente_Arial_Azul_Bold_9));
                CeldaImprimeTexto.Colspan = 2;
                CeldaImprimeTexto.Border = Bordes_Ninguno;
                CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_RIGHT;
                Tabla4.AddCell(CeldaImprimeTexto);

                CeldaImprimeTexto = new PdfPCell(new Phrase("", Fuente_Arial_Azul_Bold_9));
                CeldaImprimeTexto.Colspan = 3;
                CeldaImprimeTexto.Border = Bordes_Ninguno;
                CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_RIGHT;
                Tabla4.AddCell(CeldaImprimeTexto);

                CeldaImprimeTexto = new PdfPCell(new Phrase("C. COMERCIALES", Fuente_Arial_Azul_Bold_9));
                CeldaImprimeTexto.Colspan = 5;
                CeldaImprimeTexto.Border = Bordes_Ninguno;
                CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_LEFT;
                Tabla4.AddCell(CeldaImprimeTexto);

                CeldaImprimeTexto = new PdfPCell(new Phrase(FormatearCadena_Moneda(TotalCentrosComerciales), Fuente_Arial_Azul_Bold_9));
                CeldaImprimeTexto.Colspan = 2;
                CeldaImprimeTexto.Border = Bordes_Ninguno;
                CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_RIGHT;
                Tabla4.AddCell(CeldaImprimeTexto);

                CeldaImprimeTexto = new PdfPCell(new Phrase("", Fuente_Arial_Azul_Bold_9));
                CeldaImprimeTexto.Colspan = 6;
                CeldaImprimeTexto.Border = Bordes_Ninguno;
                CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_RIGHT;
                Tabla4.AddCell(CeldaImprimeTexto);

                CeldaImprimeTexto = new PdfPCell(new Phrase("", Fuente_Arial_Azul_Bold_9));
                CeldaImprimeTexto.Colspan = 2;
                CeldaImprimeTexto.Border = Bordes_Ninguno;
                CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_RIGHT;
                Tabla4.AddCell(CeldaImprimeTexto);
                #endregion

                #region TOTAL EFECTIVO Y NOTAS
                CeldaImprimeTexto = new PdfPCell(new Phrase("", Fuente_Arial_Azul_Bold_9));
                CeldaImprimeTexto.Colspan = 2;
                CeldaImprimeTexto.Border = Bordes_Ninguno;
                CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_RIGHT;
                Tabla4.AddCell(CeldaImprimeTexto);

                CeldaImprimeTexto = new PdfPCell(new Phrase("", Fuente_Arial_Azul_Bold_9));
                CeldaImprimeTexto.Colspan = 3;
                CeldaImprimeTexto.Border = Bordes_Ninguno;
                CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_RIGHT;
                Tabla4.AddCell(CeldaImprimeTexto);

                CeldaImprimeTexto = new PdfPCell(new Phrase("TOTAL EFECTIVO Y NOTAS", Fuente_Arial_Azul_Bold_9));
                CeldaImprimeTexto.Colspan = 5;
                CeldaImprimeTexto.Border = Bordes_Ninguno;
                CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_LEFT;
                Tabla4.AddCell(CeldaImprimeTexto);

                CeldaImprimeTexto = new PdfPCell(new Phrase(FormatearCadena_Moneda(TotalEfectivoNotas), Fuente_Arial_Azul_Bold_9));
                CeldaImprimeTexto.Colspan = 2;
                CeldaImprimeTexto.Border = Bordes_Ninguno;
                CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_RIGHT;
                Tabla4.AddCell(CeldaImprimeTexto);

                CeldaImprimeTexto = new PdfPCell(new Phrase("", Fuente_Arial_Azul_Bold_9));
                CeldaImprimeTexto.Colspan = 6;
                CeldaImprimeTexto.Border = Bordes_Ninguno;
                CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_RIGHT;
                Tabla4.AddCell(CeldaImprimeTexto);

                CeldaImprimeTexto = new PdfPCell(new Phrase("", Fuente_Arial_Azul_Bold_9));
                CeldaImprimeTexto.Colspan = 2;
                CeldaImprimeTexto.Border = Bordes_Ninguno;
                CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_RIGHT;
                Tabla4.AddCell(CeldaImprimeTexto);
                #endregion

                #region DIFERENCIA
                CeldaImprimeTexto = new PdfPCell(new Phrase("", Fuente_Arial_Azul_Bold_9));
                CeldaImprimeTexto.Colspan = 2;
                CeldaImprimeTexto.Border = Bordes_Ninguno;
                CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_RIGHT;
                Tabla4.AddCell(CeldaImprimeTexto);

                CeldaImprimeTexto = new PdfPCell(new Phrase("", Fuente_Arial_Azul_Bold_9));
                CeldaImprimeTexto.Colspan = 3;
                CeldaImprimeTexto.Border = Bordes_Ninguno;
                CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_RIGHT;
                Tabla4.AddCell(CeldaImprimeTexto);

                CeldaImprimeTexto = new PdfPCell(new Phrase("DIFERENCIA", Fuente_Arial_Azul_Bold_9));
                CeldaImprimeTexto.Colspan = 5;
                CeldaImprimeTexto.Border = Bordes_Ninguno;
                CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_LEFT;
                Tabla4.AddCell(CeldaImprimeTexto);

                /*CeldaImprimeTexto = new PdfPCell(new Phrase(FormatearCadena_Moneda(TotalDiferencia), Fuente_Arial_Azul_Bold_9));
                CeldaImprimeTexto.Colspan = 2;
                CeldaImprimeTexto.Border = Bordes_Ninguno;
                CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_RIGHT;
                Tabla4.AddCell(CeldaImprimeTexto);

                CeldaImprimeTexto = new PdfPCell(new Phrase("", Fuente_Arial_Azul_Bold_9));
                CeldaImprimeTexto.Colspan = 6;
                CeldaImprimeTexto.Border = Bordes_Ninguno;
                CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_RIGHT;
                Tabla4.AddCell(CeldaImprimeTexto);

                CeldaImprimeTexto = new PdfPCell(new Phrase("", Fuente_Arial_Azul_Bold_9));
                CeldaImprimeTexto.Colspan = 2;
                CeldaImprimeTexto.Border = Bordes_Ninguno;
                CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_RIGHT;
                Tabla4.AddCell(CeldaImprimeTexto);
                #endregion

                CeldaImprimeTexto = new PdfPCell(new Phrase(Sangria_1, Fuente_Arial_Azul_Bold_9));
                CeldaImprimeTexto.Colspan = 1;
                CeldaImprimeTexto.Border = Bordes_Ninguno;
                CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_RIGHT;
                Tabla4.AddCell(CeldaImprimeTexto);

                CeldaImprimeTexto = new PdfPCell(new Phrase(Sangria_1, Fuente_Arial_Azul_Bold_9));
                CeldaImprimeTexto.Colspan = 18;
                CeldaImprimeTexto.Border = Bordes_Inferior;
                CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_RIGHT;
                Tabla4.AddCell(CeldaImprimeTexto);

                CeldaImprimeTexto = new PdfPCell(new Phrase(Sangria_1, Fuente_Arial_Azul_Bold_9));
                CeldaImprimeTexto.Colspan = 1;
                CeldaImprimeTexto.Border = Bordes_Ninguno;
                CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_RIGHT;
                Tabla4.AddCell(CeldaImprimeTexto);*/

                #endregion

                #region T5
                /*PdfPTable Tabla5 = new PdfPTable(20);
                Tabla5.TotalWidth = dimension;
                Tabla5.LockedWidth = true;

                #region Espacios para firmar
                CeldaImprimeTexto = new PdfPCell(new Phrase(Sangria_1, Fuente_Arial_Azul_Bold_9));
                CeldaImprimeTexto.Colspan = 20;
                CeldaImprimeTexto.Border = Bordes_Ninguno;
                CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_RIGHT;
                Tabla5.AddCell(CeldaImprimeTexto);

                CeldaImprimeTexto = new PdfPCell(new Phrase(Sangria_1, Fuente_Arial_Azul_Bold_9));
                CeldaImprimeTexto.Colspan = 20;
                CeldaImprimeTexto.Border = Bordes_Ninguno;
                CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_RIGHT;
                Tabla5.AddCell(CeldaImprimeTexto);

                CeldaImprimeTexto = new PdfPCell(new Phrase(Sangria_1, Fuente_Arial_Azul_Bold_9));
                CeldaImprimeTexto.Colspan = 20;
                CeldaImprimeTexto.Border = Bordes_Ninguno;
                CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_RIGHT;
                Tabla5.AddCell(CeldaImprimeTexto);

                CeldaImprimeTexto = new PdfPCell(new Phrase(Sangria_1, Fuente_Arial_Azul_Bold_9));
                CeldaImprimeTexto.Colspan = 20;
                CeldaImprimeTexto.Border = Bordes_Ninguno;
                CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_RIGHT;
                Tabla5.AddCell(CeldaImprimeTexto);

                CeldaImprimeTexto = new PdfPCell(new Phrase(Sangria_1, Fuente_Arial_Azul_Bold_9));
                CeldaImprimeTexto.Colspan = 20;
                CeldaImprimeTexto.Border = Bordes_Ninguno;
                CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_RIGHT;
                Tabla5.AddCell(CeldaImprimeTexto);
                #endregion

                #region Firmas_Lineas
                string linea = "______________________________";
                CeldaImprimeTexto = new PdfPCell(new Phrase(linea, Fuente_Arial_Azul_Bold_9));
                CeldaImprimeTexto.Colspan = 10;
                CeldaImprimeTexto.Border = Bordes_Ninguno;
                CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_CENTER;
                Tabla5.AddCell(CeldaImprimeTexto);

                CeldaImprimeTexto = new PdfPCell(new Phrase(linea, Fuente_Arial_Azul_Bold_9));
                CeldaImprimeTexto.Colspan = 10;
                CeldaImprimeTexto.Border = Bordes_Ninguno;
                CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_CENTER;
                Tabla5.AddCell(CeldaImprimeTexto);
                #endregion

                #region Firmas_Nombres

                CeldaImprimeTexto = new PdfPCell(new Phrase("Administrador de Almacén", Fuente_Arial_Azul_Bold_9));
                CeldaImprimeTexto.Colspan = 10;
                CeldaImprimeTexto.Border = Bordes_Ninguno;
                CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_CENTER;
                Tabla5.AddCell(CeldaImprimeTexto);

                CeldaImprimeTexto = new PdfPCell(new Phrase(Agente.nombre, Fuente_Arial_Azul_Bold_9));
                CeldaImprimeTexto.Colspan = 10;
                CeldaImprimeTexto.Border = Bordes_Ninguno;
                CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_CENTER;
                Tabla5.AddCell(CeldaImprimeTexto);*/

                #endregion

                //#endregion

                //Agrega las tablas con la información al documento.
                document.Add(TablaEncabezado);
                document.Add(TablaEspacio);
                document.Add(tabla2);
                document.Add(TablaEspacio);
                document.Add(TablaEspacio);
                document.Add(TablaEspacio);
                document.Add(Tabla3);
                //document.Add(Tabla4);
                //document.Add(Tabla5);

                document.Close();
                try
                {
                    if (File.Exists(destinationFile))
                    {
                        File.Delete(destinationFile);
                    }
                    File.Move(sourceFile, destinationFile);
                    created = true;
                }
                catch (Exception ex)
                {
                    //MensajeError = ex.Message;
                    SECUDOC.writeLog("-Crear PDF de Retiros-");
                    SECUDOC.writeLog(ex.ToString());
                }
            });
            return created;
        }

        public async Task<bool> createPdfDocumentsBDLOCAL(String nombreEmpresa, String rutaArchivo, String nameReport, int userID, bool permissionPrepedido,
            String startDate, String endDate, int lastId, int LIMIT, String horaInicio, String horaFin)
        {
            bool created = false;
            await Task.Run(async () =>
            {
                List<dynamic> documentos = null;
                GetDataService gds = new GetDataService();
                dynamic respuesta = null;

                respuesta = FormGeneralsReports.fillDataGridDocumentsDBLocal(startDate, endDate, horaInicio, horaFin);
                if (respuesta != null)
                {
                    int value = respuesta.value;
                    String description = respuesta.description;
                    if (value < 0)
                    {
                        FormMessage fm = new FormMessage("Exception", description, 2);
                        fm.ShowDialog();
                    }
                    else
                    {

                        if (value > 0)
                        {
                            documentos = respuesta.documentos;
                        }
                        else if (value == 0)
                        {
                            FormMessage formMessage = new FormMessage("No se encontro información", description, 3);
                            formMessage.ShowDialog();
                        }
                        else
                        {
                            FormMessage formMessage = new FormMessage("Exception", description, 2);
                            formMessage.ShowDialog();
                        }

                    }
                }
                else
                {

                }
                //Creates an instance of the iTextSharp.text.Document-object:
                //Document document = new Document(PageSize.LETTER, 30, 30, 20, 30);
                //                                tamañoPagina, mIzq, mDer, mTop, mBot                            
                Document document = new Document(PageSize.LETTER, 30, 30, 9, 1);
                //float dimension = 570f;
                float dimension = 556f;
                string sourceFile = Application.StartupPath + @"\Temp.pdf";
                string destinationFile = rutaArchivo;
                PdfWriter.GetInstance(document, new FileStream(sourceFile, FileMode.Create));
                document.Open();

                //Agrega una tabla (espacio en blanco)
                PdfPTable TablaEspacio = new PdfPTable(1);
                TablaEspacio.TotalWidth = dimension;
                TablaEspacio.LockedWidth = true;
                PdfPCell CeldaEspacio = new PdfPCell(new Phrase(" ", Fuente_Espacio_2));
                CeldaEspacio.Border = Bordes_Ninguno;
                TablaEspacio.AddCell(CeldaEspacio);

                PdfPCell CeldaImprimeTexto;

                //Comienza llenado de archivo
                PdfPTable TablaEncabezado = new PdfPTable(1);
                TablaEncabezado.TotalWidth = dimension;
                TablaEncabezado.LockedWidth = true;

                CeldaImprimeTexto = new PdfPCell(new Phrase(nombreEmpresa, Fuente_Arial_Azul_Bold_16));
                CeldaImprimeTexto.Colspan = 1;
                CeldaImprimeTexto.Border = Bordes_Ninguno;
                CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_CENTER;
                TablaEncabezado.AddCell(CeldaImprimeTexto);

                CeldaImprimeTexto = new PdfPCell(new Phrase("Documentos", Fuente_Arial_Azul_Bold_12));
                CeldaImprimeTexto.Colspan = 1;
                CeldaImprimeTexto.Border = Bordes_Ninguno;
                CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_CENTER;
                TablaEncabezado.AddCell(CeldaImprimeTexto);

                CeldaImprimeTexto = new PdfPCell(new Phrase(Sangria_2, Fuente_Arial_Azul_Bold_12));
                CeldaImprimeTexto.Colspan = 1;
                CeldaImprimeTexto.Border = Bordes_Ninguno;
                CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_CENTER;
                TablaEncabezado.AddCell(CeldaImprimeTexto);

                PdfPTable tabla2 = new PdfPTable(20);
                tabla2.TotalWidth = dimension;
                tabla2.LockedWidth = true;

                CeldaImprimeTexto = new PdfPCell(new Phrase("Cliente", Fuente_Arial_Negra_8));
                CeldaImprimeTexto.Colspan = 2;
                CeldaImprimeTexto.Border = Bordes_Inferior;
                CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_CENTER;
                tabla2.AddCell(CeldaImprimeTexto);

                CeldaImprimeTexto = new PdfPCell(new Phrase("Tipo", Fuente_Arial_Negra_8));
                CeldaImprimeTexto.Colspan = 2;
                CeldaImprimeTexto.Border = Bordes_Inferior;
                CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_CENTER;
                tabla2.AddCell(CeldaImprimeTexto);

                CeldaImprimeTexto = new PdfPCell(new Phrase("Fecha", Fuente_Arial_Negra_8));
                CeldaImprimeTexto.Colspan = 4;
                CeldaImprimeTexto.Border = Bordes_Inferior;
                CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_CENTER;
                tabla2.AddCell(CeldaImprimeTexto);

                CeldaImprimeTexto = new PdfPCell(new Phrase("Folio", Fuente_Arial_Negra_8));
                CeldaImprimeTexto.Colspan = 3;
                CeldaImprimeTexto.Border = Bordes_Inferior;
                CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_CENTER;
                tabla2.AddCell(CeldaImprimeTexto);

                CeldaImprimeTexto = new PdfPCell(new Phrase("Movimientos", Fuente_Arial_Negra_8));
                CeldaImprimeTexto.Colspan = 3;
                CeldaImprimeTexto.Border = Bordes_Inferior;
                CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_CENTER;
                tabla2.AddCell(CeldaImprimeTexto);

                CeldaImprimeTexto = new PdfPCell(new Phrase("Descuento", Fuente_Arial_Negra_8));
                CeldaImprimeTexto.Colspan = 3;
                CeldaImprimeTexto.Border = Bordes_Inferior;
                CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_CENTER;
                tabla2.AddCell(CeldaImprimeTexto);

                CeldaImprimeTexto = new PdfPCell(new Phrase("Total", Fuente_Arial_Negra_8));
                CeldaImprimeTexto.Colspan = 3;
                CeldaImprimeTexto.Border = Bordes_Inferior;
                CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_CENTER;
                tabla2.AddCell(CeldaImprimeTexto);

                double totalDocumentos = 0;
                decimal TotalValor = 0;
                if (documentos != null)
                {
                    foreach (var documento in documentos)
                    {
                        // TotalVentas += liquidacionProducto.Valor;
                        CeldaImprimeTexto = new PdfPCell(new Phrase(documento.clave_cliente.ToString(), Fuente_Arial_Negra_8));
                        CeldaImprimeTexto.Colspan = 2;
                        CeldaImprimeTexto.Border = Bordes_Ninguno;
                        CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_CENTER;
                        tabla2.AddCell(CeldaImprimeTexto);

                        String documentType = "";
                        if (documento.tipo_documento == 1)
                            documentType = "Cotización";
                        else if (documento.tipo_documento == 2)
                            documentType = "Venta";
                        else if (documento.tipo_documento == 3)
                            documentType = "Pedido";
                        else if (documento.tipo_documento == 4)
                            documentType = "Remisión";
                        else if (documento.tipo_documento == 9)
                            documentType = "Pago del Cliente";
                        else if (documento.tipo_documento == 50)
                            documentType = "Prepedido";
                        else if (documento.tipo_documento == 51)
                            documentType = "Cotización de Mostrador";
                        if (documentType.Equals(""))
                            documentType = documento.tipo_documento.ToString();
                        CeldaImprimeTexto = new PdfPCell(new Phrase(documentType, Fuente_Arial_Negra_8));
                        CeldaImprimeTexto.Colspan = 2;
                        CeldaImprimeTexto.Border = Bordes_Ninguno;
                        CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_LEFT;
                        tabla2.AddCell(CeldaImprimeTexto);

                        CeldaImprimeTexto = new PdfPCell(new Phrase(documento.fechahoramov.ToString(), Fuente_Arial_Negra_8));
                        CeldaImprimeTexto.Colspan = 4;
                        CeldaImprimeTexto.Border = Bordes_Ninguno;
                        CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_CENTER;
                        tabla2.AddCell(CeldaImprimeTexto);

                        CeldaImprimeTexto = new PdfPCell(new Phrase(documento.fventa.ToString(), Fuente_Arial_Negra_8));
                        CeldaImprimeTexto.Colspan = 3;
                        CeldaImprimeTexto.Border = Bordes_Ninguno;
                        CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_CENTER;
                        tabla2.AddCell(CeldaImprimeTexto);
                        String movimientosText = "";
                        double totalMovs = 0;
                        movimientosText = documento.totalMovimientos + "";

                        CeldaImprimeTexto = new PdfPCell(new Phrase(movimientosText, Fuente_Arial_Negra_8));
                        CeldaImprimeTexto.Colspan = 3;
                        CeldaImprimeTexto.Border = Bordes_Ninguno;
                        CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_CENTER;
                        tabla2.AddCell(CeldaImprimeTexto);

                        CeldaImprimeTexto = new PdfPCell(new Phrase(documento.descuento.ToString("C", CultureInfo.CurrentCulture), Fuente_Arial_Negra_8));
                        CeldaImprimeTexto.Colspan = 3;
                        CeldaImprimeTexto.Border = Bordes_Ninguno;
                        CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_CENTER;
                        tabla2.AddCell(CeldaImprimeTexto);
                        if (documento.tipo_documento == 9)
                        {
                            //double total = MontoRetiroModel.getTotalOfAWithdrawal(retiro.id);
                            CeldaImprimeTexto = new PdfPCell(new Phrase(documento.anticipo.ToString("C", CultureInfo.CurrentCulture) + " MXN", Fuente_Arial_Negra_8));
                            CeldaImprimeTexto.Colspan = 3;
                            CeldaImprimeTexto.Border = Bordes_Ninguno;
                            CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_RIGHT;
                            tabla2.AddCell(CeldaImprimeTexto);
                        }
                        else
                        {
                            //double total = MontoRetiroModel.getTotalOfAWithdrawal(retiro.id);
                            CeldaImprimeTexto = new PdfPCell(new Phrase(documento.total.ToString("C", CultureInfo.CurrentCulture) + " MXN", Fuente_Arial_Negra_8));
                            CeldaImprimeTexto.Colspan = 3;
                            CeldaImprimeTexto.Border = Bordes_Ninguno;
                            CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_RIGHT;
                            tabla2.AddCell(CeldaImprimeTexto);
                        }

                        totalDocumentos += documento.total;
                        //TotalValor += liquidacionProducto.Valor;
                    }
                }

                PdfPTable Tabla3 = new PdfPTable(10);
                Tabla3.TotalWidth = dimension;
                Tabla3.LockedWidth = true;

                #region ENCABEZADO
                CeldaImprimeTexto = new PdfPCell(new Phrase("", Fuente_Arial_Azul_Bold_12));
                CeldaImprimeTexto.Colspan = 1;
                CeldaImprimeTexto.Border = Bordes_Ninguno;
                CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_LEFT;
                Tabla3.AddCell(CeldaImprimeTexto);

                CeldaImprimeTexto = new PdfPCell(new Phrase("", Fuente_Arial_Azul_Bold_10));
                CeldaImprimeTexto.Colspan = 4;
                CeldaImprimeTexto.Border = Bordes_Ninguno;
                CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_CENTER;
                Tabla3.AddCell(CeldaImprimeTexto);

                CeldaImprimeTexto = new PdfPCell(new Phrase("", Fuente_Arial_Azul_Bold_10));
                CeldaImprimeTexto.Colspan = 3;
                CeldaImprimeTexto.Border = Bordes_Ninguno;
                CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_CENTER;
                Tabla3.AddCell(CeldaImprimeTexto);

                CeldaImprimeTexto = new PdfPCell(new Phrase("Total General", Fuente_Arial_Azul_Bold_12));
                CeldaImprimeTexto.Colspan = 4;
                CeldaImprimeTexto.Border = Bordes_Ninguno;
                CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_CENTER;
                Tabla3.AddCell(CeldaImprimeTexto);

                CeldaImprimeTexto = new PdfPCell(new Phrase("", Fuente_Arial_Azul_Bold_12));
                CeldaImprimeTexto.Colspan = 1;
                CeldaImprimeTexto.Border = Bordes_Ninguno;
                CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_LEFT;
                Tabla3.AddCell(CeldaImprimeTexto);

                CeldaImprimeTexto = new PdfPCell(new Phrase("", Fuente_Arial_Azul_Bold_10));
                CeldaImprimeTexto.Colspan = 4;
                CeldaImprimeTexto.Border = Bordes_Ninguno;
                CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_CENTER;
                Tabla3.AddCell(CeldaImprimeTexto);

                CeldaImprimeTexto = new PdfPCell(new Phrase("", Fuente_Arial_Azul_Bold_10));
                CeldaImprimeTexto.Colspan = 3;
                CeldaImprimeTexto.Border = Bordes_Ninguno;
                CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_CENTER;
                Tabla3.AddCell(CeldaImprimeTexto);

                CeldaImprimeTexto = new PdfPCell(new Phrase(totalDocumentos.ToString("C") + " MXN", Fuente_Arial_Azul_Bold_12));
                CeldaImprimeTexto.Colspan = 4;
                CeldaImprimeTexto.Border = Bordes_Superior;
                CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_CENTER;
                Tabla3.AddCell(CeldaImprimeTexto);
                #endregion


                #region T4
                /*PdfPTable Tabla4 = new PdfPTable(20);
                Tabla4.TotalWidth = dimension;
                Tabla4.LockedWidth = true;

                //ESPACIO
                CeldaImprimeTexto = new PdfPCell(new Phrase(Sangria_1, Fuente_Arial_Azul_Bold_9));
                CeldaImprimeTexto.Colspan = 20;
                CeldaImprimeTexto.Border = Bordes_Superior;
                CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_RIGHT;
                Tabla4.AddCell(CeldaImprimeTexto);

                //LINEAS DE TOTALES GENERALES
                #region EFECTIVO ENTREGADO
                CeldaImprimeTexto = new PdfPCell(new Phrase("", Fuente_Arial_Azul_Bold_9));
                CeldaImprimeTexto.Colspan = 2;
                CeldaImprimeTexto.Border = Bordes_Ninguno;
                CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_RIGHT;
                Tabla4.AddCell(CeldaImprimeTexto);

                CeldaImprimeTexto = new PdfPCell(new Phrase("", Fuente_Arial_Azul_Bold_9));
                CeldaImprimeTexto.Colspan = 3;
                CeldaImprimeTexto.Border = Bordes_Ninguno;
                CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_RIGHT;
                Tabla4.AddCell(CeldaImprimeTexto);

                CeldaImprimeTexto = new PdfPCell(new Phrase("EFECTIVO ENTREGADO", Fuente_Arial_Azul_Bold_9));
                CeldaImprimeTexto.Colspan = 5;
                CeldaImprimeTexto.Border = Bordes_Ninguno;
                CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_LEFT;
                Tabla4.AddCell(CeldaImprimeTexto);

                CeldaImprimeTexto = new PdfPCell(new Phrase(FormatearCadena_Moneda(TotalEfectivo), Fuente_Arial_Azul_Bold_9));
                CeldaImprimeTexto.Colspan = 2;
                CeldaImprimeTexto.Border = Bordes_Ninguno;
                CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_RIGHT;
                Tabla4.AddCell(CeldaImprimeTexto);

                CeldaImprimeTexto = new PdfPCell(new Phrase("", Fuente_Arial_Azul_Bold_9));
                CeldaImprimeTexto.Colspan = 6;
                CeldaImprimeTexto.Border = Bordes_Ninguno;
                CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_RIGHT;
                Tabla4.AddCell(CeldaImprimeTexto);

                CeldaImprimeTexto = new PdfPCell(new Phrase("", Fuente_Arial_Azul_Bold_9));
                CeldaImprimeTexto.Colspan = 2;
                CeldaImprimeTexto.Border = Bordes_Ninguno;
                CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_RIGHT;
                Tabla4.AddCell(CeldaImprimeTexto);
                #endregion

                #region NOTAS
                CeldaImprimeTexto = new PdfPCell(new Phrase("", Fuente_Arial_Azul_Bold_9));
                CeldaImprimeTexto.Colspan = 2;
                CeldaImprimeTexto.Border = Bordes_Ninguno;
                CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_RIGHT;
                Tabla4.AddCell(CeldaImprimeTexto);

                CeldaImprimeTexto = new PdfPCell(new Phrase("", Fuente_Arial_Azul_Bold_9));
                CeldaImprimeTexto.Colspan = 3;
                CeldaImprimeTexto.Border = Bordes_Ninguno;
                CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_RIGHT;
                Tabla4.AddCell(CeldaImprimeTexto);

                CeldaImprimeTexto = new PdfPCell(new Phrase("NOTAS", Fuente_Arial_Azul_Bold_9));
                CeldaImprimeTexto.Colspan = 5;
                CeldaImprimeTexto.Border = Bordes_Ninguno;
                CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_LEFT;
                Tabla4.AddCell(CeldaImprimeTexto);

                CeldaImprimeTexto = new PdfPCell(new Phrase(FormatearCadena_Moneda(TotalNotas), Fuente_Arial_Azul_Bold_9));
                CeldaImprimeTexto.Colspan = 2;
                CeldaImprimeTexto.Border = Bordes_Ninguno;
                CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_RIGHT;
                Tabla4.AddCell(CeldaImprimeTexto);

                CeldaImprimeTexto = new PdfPCell(new Phrase("", Fuente_Arial_Azul_Bold_9));
                CeldaImprimeTexto.Colspan = 6;
                CeldaImprimeTexto.Border = Bordes_Ninguno;
                CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_RIGHT;
                Tabla4.AddCell(CeldaImprimeTexto);

                CeldaImprimeTexto = new PdfPCell(new Phrase("", Fuente_Arial_Azul_Bold_9));
                CeldaImprimeTexto.Colspan = 2;
                CeldaImprimeTexto.Border = Bordes_Ninguno;
                CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_RIGHT;
                Tabla4.AddCell(CeldaImprimeTexto);
                #endregion

                #region C. COMERCIALES
                CeldaImprimeTexto = new PdfPCell(new Phrase("", Fuente_Arial_Azul_Bold_9));
                CeldaImprimeTexto.Colspan = 2;
                CeldaImprimeTexto.Border = Bordes_Ninguno;
                CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_RIGHT;
                Tabla4.AddCell(CeldaImprimeTexto);

                CeldaImprimeTexto = new PdfPCell(new Phrase("", Fuente_Arial_Azul_Bold_9));
                CeldaImprimeTexto.Colspan = 3;
                CeldaImprimeTexto.Border = Bordes_Ninguno;
                CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_RIGHT;
                Tabla4.AddCell(CeldaImprimeTexto);

                CeldaImprimeTexto = new PdfPCell(new Phrase("C. COMERCIALES", Fuente_Arial_Azul_Bold_9));
                CeldaImprimeTexto.Colspan = 5;
                CeldaImprimeTexto.Border = Bordes_Ninguno;
                CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_LEFT;
                Tabla4.AddCell(CeldaImprimeTexto);

                CeldaImprimeTexto = new PdfPCell(new Phrase(FormatearCadena_Moneda(TotalCentrosComerciales), Fuente_Arial_Azul_Bold_9));
                CeldaImprimeTexto.Colspan = 2;
                CeldaImprimeTexto.Border = Bordes_Ninguno;
                CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_RIGHT;
                Tabla4.AddCell(CeldaImprimeTexto);

                CeldaImprimeTexto = new PdfPCell(new Phrase("", Fuente_Arial_Azul_Bold_9));
                CeldaImprimeTexto.Colspan = 6;
                CeldaImprimeTexto.Border = Bordes_Ninguno;
                CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_RIGHT;
                Tabla4.AddCell(CeldaImprimeTexto);

                CeldaImprimeTexto = new PdfPCell(new Phrase("", Fuente_Arial_Azul_Bold_9));
                CeldaImprimeTexto.Colspan = 2;
                CeldaImprimeTexto.Border = Bordes_Ninguno;
                CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_RIGHT;
                Tabla4.AddCell(CeldaImprimeTexto);
                #endregion

                #region TOTAL EFECTIVO Y NOTAS
                CeldaImprimeTexto = new PdfPCell(new Phrase("", Fuente_Arial_Azul_Bold_9));
                CeldaImprimeTexto.Colspan = 2;
                CeldaImprimeTexto.Border = Bordes_Ninguno;
                CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_RIGHT;
                Tabla4.AddCell(CeldaImprimeTexto);

                CeldaImprimeTexto = new PdfPCell(new Phrase("", Fuente_Arial_Azul_Bold_9));
                CeldaImprimeTexto.Colspan = 3;
                CeldaImprimeTexto.Border = Bordes_Ninguno;
                CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_RIGHT;
                Tabla4.AddCell(CeldaImprimeTexto);

                CeldaImprimeTexto = new PdfPCell(new Phrase("TOTAL EFECTIVO Y NOTAS", Fuente_Arial_Azul_Bold_9));
                CeldaImprimeTexto.Colspan = 5;
                CeldaImprimeTexto.Border = Bordes_Ninguno;
                CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_LEFT;
                Tabla4.AddCell(CeldaImprimeTexto);

                CeldaImprimeTexto = new PdfPCell(new Phrase(FormatearCadena_Moneda(TotalEfectivoNotas), Fuente_Arial_Azul_Bold_9));
                CeldaImprimeTexto.Colspan = 2;
                CeldaImprimeTexto.Border = Bordes_Ninguno;
                CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_RIGHT;
                Tabla4.AddCell(CeldaImprimeTexto);

                CeldaImprimeTexto = new PdfPCell(new Phrase("", Fuente_Arial_Azul_Bold_9));
                CeldaImprimeTexto.Colspan = 6;
                CeldaImprimeTexto.Border = Bordes_Ninguno;
                CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_RIGHT;
                Tabla4.AddCell(CeldaImprimeTexto);

                CeldaImprimeTexto = new PdfPCell(new Phrase("", Fuente_Arial_Azul_Bold_9));
                CeldaImprimeTexto.Colspan = 2;
                CeldaImprimeTexto.Border = Bordes_Ninguno;
                CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_RIGHT;
                Tabla4.AddCell(CeldaImprimeTexto);
                #endregion

                #region DIFERENCIA
                CeldaImprimeTexto = new PdfPCell(new Phrase("", Fuente_Arial_Azul_Bold_9));
                CeldaImprimeTexto.Colspan = 2;
                CeldaImprimeTexto.Border = Bordes_Ninguno;
                CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_RIGHT;
                Tabla4.AddCell(CeldaImprimeTexto);

                CeldaImprimeTexto = new PdfPCell(new Phrase("", Fuente_Arial_Azul_Bold_9));
                CeldaImprimeTexto.Colspan = 3;
                CeldaImprimeTexto.Border = Bordes_Ninguno;
                CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_RIGHT;
                Tabla4.AddCell(CeldaImprimeTexto);

                CeldaImprimeTexto = new PdfPCell(new Phrase("DIFERENCIA", Fuente_Arial_Azul_Bold_9));
                CeldaImprimeTexto.Colspan = 5;
                CeldaImprimeTexto.Border = Bordes_Ninguno;
                CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_LEFT;
                Tabla4.AddCell(CeldaImprimeTexto);

                /*CeldaImprimeTexto = new PdfPCell(new Phrase(FormatearCadena_Moneda(TotalDiferencia), Fuente_Arial_Azul_Bold_9));
                CeldaImprimeTexto.Colspan = 2;
                CeldaImprimeTexto.Border = Bordes_Ninguno;
                CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_RIGHT;
                Tabla4.AddCell(CeldaImprimeTexto);

                CeldaImprimeTexto = new PdfPCell(new Phrase("", Fuente_Arial_Azul_Bold_9));
                CeldaImprimeTexto.Colspan = 6;
                CeldaImprimeTexto.Border = Bordes_Ninguno;
                CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_RIGHT;
                Tabla4.AddCell(CeldaImprimeTexto);

                CeldaImprimeTexto = new PdfPCell(new Phrase("", Fuente_Arial_Azul_Bold_9));
                CeldaImprimeTexto.Colspan = 2;
                CeldaImprimeTexto.Border = Bordes_Ninguno;
                CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_RIGHT;
                Tabla4.AddCell(CeldaImprimeTexto);
                #endregion

                CeldaImprimeTexto = new PdfPCell(new Phrase(Sangria_1, Fuente_Arial_Azul_Bold_9));
                CeldaImprimeTexto.Colspan = 1;
                CeldaImprimeTexto.Border = Bordes_Ninguno;
                CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_RIGHT;
                Tabla4.AddCell(CeldaImprimeTexto);

                CeldaImprimeTexto = new PdfPCell(new Phrase(Sangria_1, Fuente_Arial_Azul_Bold_9));
                CeldaImprimeTexto.Colspan = 18;
                CeldaImprimeTexto.Border = Bordes_Inferior;
                CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_RIGHT;
                Tabla4.AddCell(CeldaImprimeTexto);

                CeldaImprimeTexto = new PdfPCell(new Phrase(Sangria_1, Fuente_Arial_Azul_Bold_9));
                CeldaImprimeTexto.Colspan = 1;
                CeldaImprimeTexto.Border = Bordes_Ninguno;
                CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_RIGHT;
                Tabla4.AddCell(CeldaImprimeTexto);*/

                #endregion

                #region T5
                /*PdfPTable Tabla5 = new PdfPTable(20);
                Tabla5.TotalWidth = dimension;
                Tabla5.LockedWidth = true;

                #region Espacios para firmar
                CeldaImprimeTexto = new PdfPCell(new Phrase(Sangria_1, Fuente_Arial_Azul_Bold_9));
                CeldaImprimeTexto.Colspan = 20;
                CeldaImprimeTexto.Border = Bordes_Ninguno;
                CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_RIGHT;
                Tabla5.AddCell(CeldaImprimeTexto);

                CeldaImprimeTexto = new PdfPCell(new Phrase(Sangria_1, Fuente_Arial_Azul_Bold_9));
                CeldaImprimeTexto.Colspan = 20;
                CeldaImprimeTexto.Border = Bordes_Ninguno;
                CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_RIGHT;
                Tabla5.AddCell(CeldaImprimeTexto);

                CeldaImprimeTexto = new PdfPCell(new Phrase(Sangria_1, Fuente_Arial_Azul_Bold_9));
                CeldaImprimeTexto.Colspan = 20;
                CeldaImprimeTexto.Border = Bordes_Ninguno;
                CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_RIGHT;
                Tabla5.AddCell(CeldaImprimeTexto);

                CeldaImprimeTexto = new PdfPCell(new Phrase(Sangria_1, Fuente_Arial_Azul_Bold_9));
                CeldaImprimeTexto.Colspan = 20;
                CeldaImprimeTexto.Border = Bordes_Ninguno;
                CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_RIGHT;
                Tabla5.AddCell(CeldaImprimeTexto);

                CeldaImprimeTexto = new PdfPCell(new Phrase(Sangria_1, Fuente_Arial_Azul_Bold_9));
                CeldaImprimeTexto.Colspan = 20;
                CeldaImprimeTexto.Border = Bordes_Ninguno;
                CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_RIGHT;
                Tabla5.AddCell(CeldaImprimeTexto);
                #endregion

                #region Firmas_Lineas
                string linea = "______________________________";
                CeldaImprimeTexto = new PdfPCell(new Phrase(linea, Fuente_Arial_Azul_Bold_9));
                CeldaImprimeTexto.Colspan = 10;
                CeldaImprimeTexto.Border = Bordes_Ninguno;
                CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_CENTER;
                Tabla5.AddCell(CeldaImprimeTexto);

                CeldaImprimeTexto = new PdfPCell(new Phrase(linea, Fuente_Arial_Azul_Bold_9));
                CeldaImprimeTexto.Colspan = 10;
                CeldaImprimeTexto.Border = Bordes_Ninguno;
                CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_CENTER;
                Tabla5.AddCell(CeldaImprimeTexto);
                #endregion

                #region Firmas_Nombres

                CeldaImprimeTexto = new PdfPCell(new Phrase("Administrador de Almacén", Fuente_Arial_Azul_Bold_9));
                CeldaImprimeTexto.Colspan = 10;
                CeldaImprimeTexto.Border = Bordes_Ninguno;
                CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_CENTER;
                Tabla5.AddCell(CeldaImprimeTexto);

                CeldaImprimeTexto = new PdfPCell(new Phrase(Agente.nombre, Fuente_Arial_Azul_Bold_9));
                CeldaImprimeTexto.Colspan = 10;
                CeldaImprimeTexto.Border = Bordes_Ninguno;
                CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_CENTER;
                Tabla5.AddCell(CeldaImprimeTexto);*/

                #endregion

                //#endregion

                //Agrega las tablas con la información al documento.
                document.Add(TablaEncabezado);
                document.Add(TablaEspacio);
                document.Add(tabla2);
                document.Add(TablaEspacio);
                document.Add(TablaEspacio);
                document.Add(TablaEspacio);
                document.Add(Tabla3);
                //document.Add(Tabla4);
                //document.Add(Tabla5);

                document.Close();
                try
                {
                    if (File.Exists(destinationFile))
                    {
                        File.Delete(destinationFile);
                    }
                    File.Move(sourceFile, destinationFile);
                    created = true;
                }
                catch (Exception ex)
                {
                    //MensajeError = ex.Message;
                    SECUDOC.writeLog("-Crear PDF de Retiros-");
                    SECUDOC.writeLog(ex.ToString());
                }
            });
            return created;
        }

        public static bool CreatePdfTicketTPV(String rutaArchivo, String TextoTicket)
        {
            bool created = false;
            Document document = new Document(PageSize.LETTER, 30, 30, 9, 1);
            float dimension = 556f;
            string sourceFile = Application.StartupPath + @"\Temp.pdf";
            string destinationFile = rutaArchivo;
            PdfWriter.GetInstance(document, new FileStream(sourceFile, FileMode.Create));
            document.Open();

            //Agrega una tabla (espacio en blanco)
            PdfPTable TablaEspacio = new PdfPTable(1);
            TablaEspacio.TotalWidth = dimension;
            TablaEspacio.LockedWidth = true;
            PdfPCell CeldaEspacio = new PdfPCell(new Phrase(" ", Fuente_Espacio_2));
            CeldaEspacio.Border = Bordes_Ninguno;
            TablaEspacio.AddCell(CeldaEspacio);

            PdfPCell CeldaImprimeTexto;

            //Comienza llenado de archivo
            PdfPTable TablaEncabezado = new PdfPTable(1);
            TablaEncabezado.TotalWidth = dimension;
            TablaEncabezado.LockedWidth = true;

            CeldaImprimeTexto = new PdfPCell(new Phrase(TextoTicket, Fuente_Arial_Azul_Bold_12));
            CeldaImprimeTexto.Colspan = 1;
            CeldaImprimeTexto.Border = Bordes_Ninguno;
            CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_LEFT;
            TablaEncabezado.AddCell(CeldaImprimeTexto);

            //Agrega las tablas con la información al documento.
            document.Add(TablaEncabezado);

            document.Close();
            try
            {
                if (File.Exists(destinationFile))
                {
                    File.Delete(destinationFile);
                }
                File.Move(sourceFile, destinationFile);
                created = true;
            }
            catch (Exception ex)
            {
                //MensajeError = ex.Message;
                SECUDOC.writeLog("-Crear PDF de TicketTPV-");
                SECUDOC.writeLog(ex.ToString());
            }
            return created;
        }

        public static async Task<bool> crearPdfEgresosIngresos(String nombreEmpresa, String rutaArchivo, bool entry,
            int idUser, String startDate, String endDate, int lastId, int LIMIT)
        {
            List<ClsRetirosModel> retirosList = null;
            GetDataService gds = new GetDataService();
            dynamic respuesta = null;
            if (ConfiguracionModel.isLANPermissionActivated())
                respuesta = await gds.downloadAllWithdrawalsLAN(idUser, startDate, endDate, lastId, LIMIT);
            else respuesta = await gds.downloadAllWithdrawals(idUser, startDate, endDate, lastId, LIMIT);
            if (respuesta != null)
            {
                int value = respuesta.value;
                String description = respuesta.description;
                int information = respuesta.information;
                if (value < 0)
                {
                    FormMessage fm = new FormMessage("Exception", description, 2);
                    fm.ShowDialog();
                }
                else
                {
                    if (information == GetDataService.GET_WITHDRAWAL)
                    {
                        if (value > 0)
                        {
                            retirosList = respuesta.retiros;
                        }
                        else if (value == 0)
                        {
                            FormMessage formMessage = new FormMessage("No se encontro información", description, 3);
                            formMessage.ShowDialog();
                        }
                        else
                        {
                            FormMessage formMessage = new FormMessage("Exception", description, 2);
                            formMessage.ShowDialog();
                        }
                    }
                }
            } else
            {

            }
            bool created = false;
            //Creates an instance of the iTextSharp.text.Document-object:
            //Document document = new Document(PageSize.LETTER, 30, 30, 20, 30);
            //                                tamañoPagina, mIzq, mDer, mTop, mBot                            
            Document document = new Document(PageSize.LETTER, 30, 30, 9, 1);
            //float dimension = 570f;
            float dimension = 556f;
            string sourceFile = Application.StartupPath + @"\Temp.pdf";
            string destinationFile = rutaArchivo;
            PdfWriter.GetInstance(document, new FileStream(sourceFile, FileMode.Create));
            document.Open();

            //Agrega una tabla (espacio en blanco)
            PdfPTable TablaEspacio = new PdfPTable(1);
            TablaEspacio.TotalWidth = dimension;
            TablaEspacio.LockedWidth = true;
            PdfPCell CeldaEspacio = new PdfPCell(new Phrase(" ", Fuente_Espacio_2));
            CeldaEspacio.Border = Bordes_Ninguno;
            TablaEspacio.AddCell(CeldaEspacio);

            PdfPCell CeldaImprimeTexto;

            //Comienza llenado de archivo
            PdfPTable TablaEncabezado = new PdfPTable(1);
            TablaEncabezado.TotalWidth = dimension;
            TablaEncabezado.LockedWidth = true;

            CeldaImprimeTexto = new PdfPCell(new Phrase(nombreEmpresa, Fuente_Arial_Azul_Bold_16));
            CeldaImprimeTexto.Colspan = 1;
            CeldaImprimeTexto.Border = Bordes_Ninguno;
            CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_CENTER;
            TablaEncabezado.AddCell(CeldaImprimeTexto);

            if (entry)
            {
                CeldaImprimeTexto = new PdfPCell(new Phrase("Ingresos", Fuente_Arial_Azul_Bold_12));
                CeldaImprimeTexto.Colspan = 1;
                CeldaImprimeTexto.Border = Bordes_Ninguno;
                CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_CENTER;
                TablaEncabezado.AddCell(CeldaImprimeTexto);
            } else
            {
                CeldaImprimeTexto = new PdfPCell(new Phrase("Egresos", Fuente_Arial_Azul_Bold_12));
                CeldaImprimeTexto.Colspan = 1;
                CeldaImprimeTexto.Border = Bordes_Ninguno;
                CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_CENTER;
                TablaEncabezado.AddCell(CeldaImprimeTexto);
            }

            CeldaImprimeTexto = new PdfPCell(new Phrase(Sangria_2, Fuente_Arial_Azul_Bold_12));
            CeldaImprimeTexto.Colspan = 1;
            CeldaImprimeTexto.Border = Bordes_Ninguno;
            CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_CENTER;
            TablaEncabezado.AddCell(CeldaImprimeTexto);

            PdfPTable tabla2 = new PdfPTable(20);
            tabla2.TotalWidth = dimension;
            tabla2.LockedWidth = true;

            CeldaImprimeTexto = new PdfPCell(new Phrase("Número", Fuente_Arial_Negra_8));
            CeldaImprimeTexto.Colspan = 2;
            CeldaImprimeTexto.Border = Bordes_Inferior;
            CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_CENTER;
            tabla2.AddCell(CeldaImprimeTexto);

            CeldaImprimeTexto = new PdfPCell(new Phrase("Concepto", Fuente_Arial_Negra_8));
            CeldaImprimeTexto.Colspan = 4;
            CeldaImprimeTexto.Border = Bordes_Inferior;
            CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_CENTER;
            tabla2.AddCell(CeldaImprimeTexto);

            CeldaImprimeTexto = new PdfPCell(new Phrase("Descripción", Fuente_Arial_Negra_8));
            CeldaImprimeTexto.Colspan = 6;
            CeldaImprimeTexto.Border = Bordes_Inferior;
            CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_CENTER;
            tabla2.AddCell(CeldaImprimeTexto);

            CeldaImprimeTexto = new PdfPCell(new Phrase("Fecha", Fuente_Arial_Negra_8));
            CeldaImprimeTexto.Colspan = 3;
            CeldaImprimeTexto.Border = Bordes_Inferior;
            CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_CENTER;
            tabla2.AddCell(CeldaImprimeTexto);

            CeldaImprimeTexto = new PdfPCell(new Phrase("Importes", Fuente_Arial_Negra_8));
            CeldaImprimeTexto.Colspan = 3;
            CeldaImprimeTexto.Border = Bordes_Inferior;
            CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_CENTER;
            tabla2.AddCell(CeldaImprimeTexto);

            CeldaImprimeTexto = new PdfPCell(new Phrase("Total", Fuente_Arial_Negra_8));
            CeldaImprimeTexto.Colspan = 2;
            CeldaImprimeTexto.Border = Bordes_Inferior;
            CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_CENTER;
            tabla2.AddCell(CeldaImprimeTexto);

            double totalRetiros = 0;
            decimal TotalValor = 0;
            if (retirosList != null)
            {
                foreach (var retiro in retirosList)
                {
                    // TotalVentas += liquidacionProducto.Valor;
                    CeldaImprimeTexto = new PdfPCell(new Phrase(retiro.number.ToString(), Fuente_Arial_Negra_8));
                    CeldaImprimeTexto.Colspan = 2;
                    CeldaImprimeTexto.Border = Bordes_Ninguno;
                    CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_CENTER;
                    tabla2.AddCell(CeldaImprimeTexto);

                    CeldaImprimeTexto = new PdfPCell(new Phrase(retiro.concept, Fuente_Arial_Negra_8));
                    CeldaImprimeTexto.Colspan = 4;
                    CeldaImprimeTexto.Border = Bordes_Ninguno;
                    CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_LEFT;
                    tabla2.AddCell(CeldaImprimeTexto);

                    CeldaImprimeTexto = new PdfPCell(new Phrase(retiro.description.ToString(), Fuente_Arial_Negra_8));
                    CeldaImprimeTexto.Colspan = 6;
                    CeldaImprimeTexto.Border = Bordes_Ninguno;
                    CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_CENTER;
                    tabla2.AddCell(CeldaImprimeTexto);

                    CeldaImprimeTexto = new PdfPCell(new Phrase(retiro.fechaHora.ToString(), Fuente_Arial_Negra_8));
                    CeldaImprimeTexto.Colspan = 3;
                    CeldaImprimeTexto.Border = Bordes_Ninguno;
                    CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_CENTER;
                    tabla2.AddCell(CeldaImprimeTexto);
                    String montos = "";
                    double total = 0;
                    List<ClsMontosRetirosModel> montosList = retiro.montos;
                    if (montosList != null)
                    {
                        foreach (var monto in montosList)
                        {
                            String fcName = FormasDeCobroModel.getANameFrromAFomaDeCobroWithId(monto.formaCobroId);
                            montos += fcName + " " + monto.importe.ToString("C") + "\r\n";
                            total += monto.importe;
                        }
                    }
                    CeldaImprimeTexto = new PdfPCell(new Phrase(montos, Fuente_Arial_Negra_8));
                    CeldaImprimeTexto.Colspan = 3;
                    CeldaImprimeTexto.Border = Bordes_Ninguno;
                    CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_CENTER;
                    tabla2.AddCell(CeldaImprimeTexto);

                    //double total = MontoRetiroModel.getTotalOfAWithdrawal(retiro.id);
                    CeldaImprimeTexto = new PdfPCell(new Phrase(total .ToString("C", CultureInfo.CurrentCulture)+ " MXN", Fuente_Arial_Negra_8));
                    CeldaImprimeTexto.Colspan = 2;
                    CeldaImprimeTexto.Border = Bordes_Ninguno;
                    CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_RIGHT;
                    tabla2.AddCell(CeldaImprimeTexto);

                    totalRetiros += total;
                    //TotalValor += liquidacionProducto.Valor;
                }
            }

            PdfPTable Tabla3 = new PdfPTable(10);
            Tabla3.TotalWidth = dimension;
            Tabla3.LockedWidth = true;

            #region ENCABEZADO
            CeldaImprimeTexto = new PdfPCell(new Phrase("", Fuente_Arial_Azul_Bold_12));
            CeldaImprimeTexto.Colspan = 1;
            CeldaImprimeTexto.Border = Bordes_Ninguno;
            CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_LEFT;
            Tabla3.AddCell(CeldaImprimeTexto);

            CeldaImprimeTexto = new PdfPCell(new Phrase("", Fuente_Arial_Azul_Bold_10));
            CeldaImprimeTexto.Colspan = 4;
            CeldaImprimeTexto.Border = Bordes_Ninguno;
            CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_CENTER;
            Tabla3.AddCell(CeldaImprimeTexto);

            CeldaImprimeTexto = new PdfPCell(new Phrase("", Fuente_Arial_Azul_Bold_10));
            CeldaImprimeTexto.Colspan = 3;
            CeldaImprimeTexto.Border = Bordes_Ninguno;
            CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_CENTER;
            Tabla3.AddCell(CeldaImprimeTexto);

            CeldaImprimeTexto = new PdfPCell(new Phrase("Total General", Fuente_Arial_Azul_Bold_12));
            CeldaImprimeTexto.Colspan = 4;
            CeldaImprimeTexto.Border = Bordes_Ninguno;
            CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_CENTER;
            Tabla3.AddCell(CeldaImprimeTexto);

            CeldaImprimeTexto = new PdfPCell(new Phrase("", Fuente_Arial_Azul_Bold_12));
            CeldaImprimeTexto.Colspan = 1;
            CeldaImprimeTexto.Border = Bordes_Ninguno;
            CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_LEFT;
            Tabla3.AddCell(CeldaImprimeTexto);

            CeldaImprimeTexto = new PdfPCell(new Phrase("", Fuente_Arial_Azul_Bold_10));
            CeldaImprimeTexto.Colspan = 4;
            CeldaImprimeTexto.Border = Bordes_Ninguno;
            CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_CENTER;
            Tabla3.AddCell(CeldaImprimeTexto);

            CeldaImprimeTexto = new PdfPCell(new Phrase("", Fuente_Arial_Azul_Bold_10));
            CeldaImprimeTexto.Colspan = 3;
            CeldaImprimeTexto.Border = Bordes_Ninguno;
            CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_CENTER;
            Tabla3.AddCell(CeldaImprimeTexto);

            CeldaImprimeTexto = new PdfPCell(new Phrase(totalRetiros.ToString("C")+" MXN", Fuente_Arial_Azul_Bold_12));
            CeldaImprimeTexto.Colspan = 4;
            CeldaImprimeTexto.Border = Bordes_Superior;
            CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_CENTER;
            Tabla3.AddCell(CeldaImprimeTexto);
            #endregion


            #region T4
            /*PdfPTable Tabla4 = new PdfPTable(20);
            Tabla4.TotalWidth = dimension;
            Tabla4.LockedWidth = true;

            //ESPACIO
            CeldaImprimeTexto = new PdfPCell(new Phrase(Sangria_1, Fuente_Arial_Azul_Bold_9));
            CeldaImprimeTexto.Colspan = 20;
            CeldaImprimeTexto.Border = Bordes_Superior;
            CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_RIGHT;
            Tabla4.AddCell(CeldaImprimeTexto);

            //LINEAS DE TOTALES GENERALES
            #region EFECTIVO ENTREGADO
            CeldaImprimeTexto = new PdfPCell(new Phrase("", Fuente_Arial_Azul_Bold_9));
            CeldaImprimeTexto.Colspan = 2;
            CeldaImprimeTexto.Border = Bordes_Ninguno;
            CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_RIGHT;
            Tabla4.AddCell(CeldaImprimeTexto);

            CeldaImprimeTexto = new PdfPCell(new Phrase("", Fuente_Arial_Azul_Bold_9));
            CeldaImprimeTexto.Colspan = 3;
            CeldaImprimeTexto.Border = Bordes_Ninguno;
            CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_RIGHT;
            Tabla4.AddCell(CeldaImprimeTexto);

            CeldaImprimeTexto = new PdfPCell(new Phrase("EFECTIVO ENTREGADO", Fuente_Arial_Azul_Bold_9));
            CeldaImprimeTexto.Colspan = 5;
            CeldaImprimeTexto.Border = Bordes_Ninguno;
            CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_LEFT;
            Tabla4.AddCell(CeldaImprimeTexto);

            CeldaImprimeTexto = new PdfPCell(new Phrase(FormatearCadena_Moneda(TotalEfectivo), Fuente_Arial_Azul_Bold_9));
            CeldaImprimeTexto.Colspan = 2;
            CeldaImprimeTexto.Border = Bordes_Ninguno;
            CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_RIGHT;
            Tabla4.AddCell(CeldaImprimeTexto);

            CeldaImprimeTexto = new PdfPCell(new Phrase("", Fuente_Arial_Azul_Bold_9));
            CeldaImprimeTexto.Colspan = 6;
            CeldaImprimeTexto.Border = Bordes_Ninguno;
            CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_RIGHT;
            Tabla4.AddCell(CeldaImprimeTexto);

            CeldaImprimeTexto = new PdfPCell(new Phrase("", Fuente_Arial_Azul_Bold_9));
            CeldaImprimeTexto.Colspan = 2;
            CeldaImprimeTexto.Border = Bordes_Ninguno;
            CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_RIGHT;
            Tabla4.AddCell(CeldaImprimeTexto);
            #endregion

            #region NOTAS
            CeldaImprimeTexto = new PdfPCell(new Phrase("", Fuente_Arial_Azul_Bold_9));
            CeldaImprimeTexto.Colspan = 2;
            CeldaImprimeTexto.Border = Bordes_Ninguno;
            CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_RIGHT;
            Tabla4.AddCell(CeldaImprimeTexto);

            CeldaImprimeTexto = new PdfPCell(new Phrase("", Fuente_Arial_Azul_Bold_9));
            CeldaImprimeTexto.Colspan = 3;
            CeldaImprimeTexto.Border = Bordes_Ninguno;
            CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_RIGHT;
            Tabla4.AddCell(CeldaImprimeTexto);

            CeldaImprimeTexto = new PdfPCell(new Phrase("NOTAS", Fuente_Arial_Azul_Bold_9));
            CeldaImprimeTexto.Colspan = 5;
            CeldaImprimeTexto.Border = Bordes_Ninguno;
            CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_LEFT;
            Tabla4.AddCell(CeldaImprimeTexto);

            CeldaImprimeTexto = new PdfPCell(new Phrase(FormatearCadena_Moneda(TotalNotas), Fuente_Arial_Azul_Bold_9));
            CeldaImprimeTexto.Colspan = 2;
            CeldaImprimeTexto.Border = Bordes_Ninguno;
            CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_RIGHT;
            Tabla4.AddCell(CeldaImprimeTexto);

            CeldaImprimeTexto = new PdfPCell(new Phrase("", Fuente_Arial_Azul_Bold_9));
            CeldaImprimeTexto.Colspan = 6;
            CeldaImprimeTexto.Border = Bordes_Ninguno;
            CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_RIGHT;
            Tabla4.AddCell(CeldaImprimeTexto);

            CeldaImprimeTexto = new PdfPCell(new Phrase("", Fuente_Arial_Azul_Bold_9));
            CeldaImprimeTexto.Colspan = 2;
            CeldaImprimeTexto.Border = Bordes_Ninguno;
            CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_RIGHT;
            Tabla4.AddCell(CeldaImprimeTexto);
            #endregion

            #region C. COMERCIALES
            CeldaImprimeTexto = new PdfPCell(new Phrase("", Fuente_Arial_Azul_Bold_9));
            CeldaImprimeTexto.Colspan = 2;
            CeldaImprimeTexto.Border = Bordes_Ninguno;
            CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_RIGHT;
            Tabla4.AddCell(CeldaImprimeTexto);

            CeldaImprimeTexto = new PdfPCell(new Phrase("", Fuente_Arial_Azul_Bold_9));
            CeldaImprimeTexto.Colspan = 3;
            CeldaImprimeTexto.Border = Bordes_Ninguno;
            CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_RIGHT;
            Tabla4.AddCell(CeldaImprimeTexto);

            CeldaImprimeTexto = new PdfPCell(new Phrase("C. COMERCIALES", Fuente_Arial_Azul_Bold_9));
            CeldaImprimeTexto.Colspan = 5;
            CeldaImprimeTexto.Border = Bordes_Ninguno;
            CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_LEFT;
            Tabla4.AddCell(CeldaImprimeTexto);

            CeldaImprimeTexto = new PdfPCell(new Phrase(FormatearCadena_Moneda(TotalCentrosComerciales), Fuente_Arial_Azul_Bold_9));
            CeldaImprimeTexto.Colspan = 2;
            CeldaImprimeTexto.Border = Bordes_Ninguno;
            CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_RIGHT;
            Tabla4.AddCell(CeldaImprimeTexto);

            CeldaImprimeTexto = new PdfPCell(new Phrase("", Fuente_Arial_Azul_Bold_9));
            CeldaImprimeTexto.Colspan = 6;
            CeldaImprimeTexto.Border = Bordes_Ninguno;
            CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_RIGHT;
            Tabla4.AddCell(CeldaImprimeTexto);

            CeldaImprimeTexto = new PdfPCell(new Phrase("", Fuente_Arial_Azul_Bold_9));
            CeldaImprimeTexto.Colspan = 2;
            CeldaImprimeTexto.Border = Bordes_Ninguno;
            CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_RIGHT;
            Tabla4.AddCell(CeldaImprimeTexto);
            #endregion

            #region TOTAL EFECTIVO Y NOTAS
            CeldaImprimeTexto = new PdfPCell(new Phrase("", Fuente_Arial_Azul_Bold_9));
            CeldaImprimeTexto.Colspan = 2;
            CeldaImprimeTexto.Border = Bordes_Ninguno;
            CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_RIGHT;
            Tabla4.AddCell(CeldaImprimeTexto);

            CeldaImprimeTexto = new PdfPCell(new Phrase("", Fuente_Arial_Azul_Bold_9));
            CeldaImprimeTexto.Colspan = 3;
            CeldaImprimeTexto.Border = Bordes_Ninguno;
            CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_RIGHT;
            Tabla4.AddCell(CeldaImprimeTexto);

            CeldaImprimeTexto = new PdfPCell(new Phrase("TOTAL EFECTIVO Y NOTAS", Fuente_Arial_Azul_Bold_9));
            CeldaImprimeTexto.Colspan = 5;
            CeldaImprimeTexto.Border = Bordes_Ninguno;
            CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_LEFT;
            Tabla4.AddCell(CeldaImprimeTexto);

            CeldaImprimeTexto = new PdfPCell(new Phrase(FormatearCadena_Moneda(TotalEfectivoNotas), Fuente_Arial_Azul_Bold_9));
            CeldaImprimeTexto.Colspan = 2;
            CeldaImprimeTexto.Border = Bordes_Ninguno;
            CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_RIGHT;
            Tabla4.AddCell(CeldaImprimeTexto);

            CeldaImprimeTexto = new PdfPCell(new Phrase("", Fuente_Arial_Azul_Bold_9));
            CeldaImprimeTexto.Colspan = 6;
            CeldaImprimeTexto.Border = Bordes_Ninguno;
            CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_RIGHT;
            Tabla4.AddCell(CeldaImprimeTexto);

            CeldaImprimeTexto = new PdfPCell(new Phrase("", Fuente_Arial_Azul_Bold_9));
            CeldaImprimeTexto.Colspan = 2;
            CeldaImprimeTexto.Border = Bordes_Ninguno;
            CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_RIGHT;
            Tabla4.AddCell(CeldaImprimeTexto);
            #endregion

            #region DIFERENCIA
            CeldaImprimeTexto = new PdfPCell(new Phrase("", Fuente_Arial_Azul_Bold_9));
            CeldaImprimeTexto.Colspan = 2;
            CeldaImprimeTexto.Border = Bordes_Ninguno;
            CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_RIGHT;
            Tabla4.AddCell(CeldaImprimeTexto);

            CeldaImprimeTexto = new PdfPCell(new Phrase("", Fuente_Arial_Azul_Bold_9));
            CeldaImprimeTexto.Colspan = 3;
            CeldaImprimeTexto.Border = Bordes_Ninguno;
            CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_RIGHT;
            Tabla4.AddCell(CeldaImprimeTexto);

            CeldaImprimeTexto = new PdfPCell(new Phrase("DIFERENCIA", Fuente_Arial_Azul_Bold_9));
            CeldaImprimeTexto.Colspan = 5;
            CeldaImprimeTexto.Border = Bordes_Ninguno;
            CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_LEFT;
            Tabla4.AddCell(CeldaImprimeTexto);

            /*CeldaImprimeTexto = new PdfPCell(new Phrase(FormatearCadena_Moneda(TotalDiferencia), Fuente_Arial_Azul_Bold_9));
            CeldaImprimeTexto.Colspan = 2;
            CeldaImprimeTexto.Border = Bordes_Ninguno;
            CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_RIGHT;
            Tabla4.AddCell(CeldaImprimeTexto);

            CeldaImprimeTexto = new PdfPCell(new Phrase("", Fuente_Arial_Azul_Bold_9));
            CeldaImprimeTexto.Colspan = 6;
            CeldaImprimeTexto.Border = Bordes_Ninguno;
            CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_RIGHT;
            Tabla4.AddCell(CeldaImprimeTexto);

            CeldaImprimeTexto = new PdfPCell(new Phrase("", Fuente_Arial_Azul_Bold_9));
            CeldaImprimeTexto.Colspan = 2;
            CeldaImprimeTexto.Border = Bordes_Ninguno;
            CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_RIGHT;
            Tabla4.AddCell(CeldaImprimeTexto);
            #endregion

            CeldaImprimeTexto = new PdfPCell(new Phrase(Sangria_1, Fuente_Arial_Azul_Bold_9));
            CeldaImprimeTexto.Colspan = 1;
            CeldaImprimeTexto.Border = Bordes_Ninguno;
            CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_RIGHT;
            Tabla4.AddCell(CeldaImprimeTexto);

            CeldaImprimeTexto = new PdfPCell(new Phrase(Sangria_1, Fuente_Arial_Azul_Bold_9));
            CeldaImprimeTexto.Colspan = 18;
            CeldaImprimeTexto.Border = Bordes_Inferior;
            CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_RIGHT;
            Tabla4.AddCell(CeldaImprimeTexto);

            CeldaImprimeTexto = new PdfPCell(new Phrase(Sangria_1, Fuente_Arial_Azul_Bold_9));
            CeldaImprimeTexto.Colspan = 1;
            CeldaImprimeTexto.Border = Bordes_Ninguno;
            CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_RIGHT;
            Tabla4.AddCell(CeldaImprimeTexto);*/

            #endregion

            #region T5
            /*PdfPTable Tabla5 = new PdfPTable(20);
            Tabla5.TotalWidth = dimension;
            Tabla5.LockedWidth = true;

            #region Espacios para firmar
            CeldaImprimeTexto = new PdfPCell(new Phrase(Sangria_1, Fuente_Arial_Azul_Bold_9));
            CeldaImprimeTexto.Colspan = 20;
            CeldaImprimeTexto.Border = Bordes_Ninguno;
            CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_RIGHT;
            Tabla5.AddCell(CeldaImprimeTexto);

            CeldaImprimeTexto = new PdfPCell(new Phrase(Sangria_1, Fuente_Arial_Azul_Bold_9));
            CeldaImprimeTexto.Colspan = 20;
            CeldaImprimeTexto.Border = Bordes_Ninguno;
            CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_RIGHT;
            Tabla5.AddCell(CeldaImprimeTexto);

            CeldaImprimeTexto = new PdfPCell(new Phrase(Sangria_1, Fuente_Arial_Azul_Bold_9));
            CeldaImprimeTexto.Colspan = 20;
            CeldaImprimeTexto.Border = Bordes_Ninguno;
            CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_RIGHT;
            Tabla5.AddCell(CeldaImprimeTexto);

            CeldaImprimeTexto = new PdfPCell(new Phrase(Sangria_1, Fuente_Arial_Azul_Bold_9));
            CeldaImprimeTexto.Colspan = 20;
            CeldaImprimeTexto.Border = Bordes_Ninguno;
            CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_RIGHT;
            Tabla5.AddCell(CeldaImprimeTexto);

            CeldaImprimeTexto = new PdfPCell(new Phrase(Sangria_1, Fuente_Arial_Azul_Bold_9));
            CeldaImprimeTexto.Colspan = 20;
            CeldaImprimeTexto.Border = Bordes_Ninguno;
            CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_RIGHT;
            Tabla5.AddCell(CeldaImprimeTexto);
            #endregion

            #region Firmas_Lineas
            string linea = "______________________________";
            CeldaImprimeTexto = new PdfPCell(new Phrase(linea, Fuente_Arial_Azul_Bold_9));
            CeldaImprimeTexto.Colspan = 10;
            CeldaImprimeTexto.Border = Bordes_Ninguno;
            CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_CENTER;
            Tabla5.AddCell(CeldaImprimeTexto);

            CeldaImprimeTexto = new PdfPCell(new Phrase(linea, Fuente_Arial_Azul_Bold_9));
            CeldaImprimeTexto.Colspan = 10;
            CeldaImprimeTexto.Border = Bordes_Ninguno;
            CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_CENTER;
            Tabla5.AddCell(CeldaImprimeTexto);
            #endregion

            #region Firmas_Nombres

            CeldaImprimeTexto = new PdfPCell(new Phrase("Administrador de Almacén", Fuente_Arial_Azul_Bold_9));
            CeldaImprimeTexto.Colspan = 10;
            CeldaImprimeTexto.Border = Bordes_Ninguno;
            CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_CENTER;
            Tabla5.AddCell(CeldaImprimeTexto);

            CeldaImprimeTexto = new PdfPCell(new Phrase(Agente.nombre, Fuente_Arial_Azul_Bold_9));
            CeldaImprimeTexto.Colspan = 10;
            CeldaImprimeTexto.Border = Bordes_Ninguno;
            CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_CENTER;
            Tabla5.AddCell(CeldaImprimeTexto);*/

            #endregion

            //#endregion

            //Agrega las tablas con la información al documento.
            document.Add(TablaEncabezado);
            document.Add(TablaEspacio);
            document.Add(tabla2);
            document.Add(TablaEspacio);
            document.Add(TablaEspacio);
            document.Add(TablaEspacio);
            document.Add(Tabla3);
            //document.Add(Tabla4);
            //document.Add(Tabla5);

            document.Close();
            try
            {
                if (File.Exists(destinationFile))
                {
                    File.Delete(destinationFile);
                }
                File.Move(sourceFile, destinationFile);
                created = true;
            }
            catch (Exception ex)
            {
                //MensajeError = ex.Message;
                SECUDOC.writeLog("-Crear PDF de Retiros-");
                SECUDOC.writeLog(ex.ToString());
            }
            return created;
        }

        public static async Task<bool> crearPdfDocumentosPrepedidos(String nombreEmpresa, String rutaArchivo, String queryDocuments,
            String parameterName1, String parameterName2, String parameterValue1, String parameterValue2, int queryType)
        {
            bool created = false;
            //Creates an instance of the iTextSharp.text.Document-object:
            //Document document = new Document(PageSize.LETTER, 30, 30, 20, 30);
            //                                tamañoPagina, mIzq, mDer, mTop, mBot                            
            Document document = new Document(PageSize.LETTER, 30, 30, 9, 1);
            //float dimension = 570f;
            float dimension = 556f;
            string sourceFile = Application.StartupPath + @"\Temp.pdf";
            string destinationFile = rutaArchivo;
            PdfWriter.GetInstance(document, new FileStream(sourceFile, FileMode.Create));
            document.Open();

            //Agrega una tabla (espacio en blanco)
            PdfPTable TablaEspacio = new PdfPTable(1);
            TablaEspacio.TotalWidth = dimension;
            TablaEspacio.LockedWidth = true;
            PdfPCell CeldaEspacio = new PdfPCell(new Phrase(" ", Fuente_Espacio_2));
            CeldaEspacio.Border = Bordes_Ninguno;
            TablaEspacio.AddCell(CeldaEspacio);

            PdfPCell CeldaImprimeTexto;

            //Comienza llenado de archivo
            PdfPTable TablaEncabezado = new PdfPTable(1);
            TablaEncabezado.TotalWidth = dimension;
            TablaEncabezado.LockedWidth = true;

            CeldaImprimeTexto = new PdfPCell(new Phrase(nombreEmpresa, Fuente_Arial_Azul_Bold_16));
            CeldaImprimeTexto.Colspan = 1;
            CeldaImprimeTexto.Border = Bordes_Ninguno;
            CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_CENTER;
            TablaEncabezado.AddCell(CeldaImprimeTexto);

            CeldaImprimeTexto = new PdfPCell(new Phrase("Documentos de Entrega", Fuente_Arial_Azul_Bold_12));
            CeldaImprimeTexto.Colspan = 1;
            CeldaImprimeTexto.Border = Bordes_Ninguno;
            CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_CENTER;
            TablaEncabezado.AddCell(CeldaImprimeTexto);

            CeldaImprimeTexto = new PdfPCell(new Phrase(Sangria_2, Fuente_Arial_Azul_Bold_12));
            CeldaImprimeTexto.Colspan = 1;
            CeldaImprimeTexto.Border = Bordes_Ninguno;
            CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_CENTER;
            TablaEncabezado.AddCell(CeldaImprimeTexto);

            PdfPTable tabla2 = new PdfPTable(20);
            tabla2.TotalWidth = dimension;
            tabla2.LockedWidth = true;

            /*CeldaImprimeTexto = new PdfPCell(new Phrase("Tipo", Fuente_Arial_Negra_8));
            CeldaImprimeTexto.Colspan = 2;
            CeldaImprimeTexto.Border = Bordes_Inferior;
            CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_CENTER;
            tabla2.AddCell(CeldaImprimeTexto);*/

            CeldaImprimeTexto = new PdfPCell(new Phrase("Cliente", Fuente_Arial_Negra_8));
            CeldaImprimeTexto.Colspan = 4;
            CeldaImprimeTexto.Border = Bordes_Inferior;
            CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_CENTER;
            tabla2.AddCell(CeldaImprimeTexto);

            CeldaImprimeTexto = new PdfPCell(new Phrase("Folio", Fuente_Arial_Negra_8));
            CeldaImprimeTexto.Colspan = 2;
            CeldaImprimeTexto.Border = Bordes_Inferior;
            CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_CENTER;
            tabla2.AddCell(CeldaImprimeTexto);

            CeldaImprimeTexto = new PdfPCell(new Phrase("Fecha", Fuente_Arial_Negra_8));
            CeldaImprimeTexto.Colspan = 3;
            CeldaImprimeTexto.Border = Bordes_Inferior;
            CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_CENTER;
            tabla2.AddCell(CeldaImprimeTexto);

            CeldaImprimeTexto = new PdfPCell(new Phrase("Producto", Fuente_Arial_Negra_8));
            CeldaImprimeTexto.Colspan = 3;
            CeldaImprimeTexto.Border = Bordes_Inferior;
            CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_CENTER;
            tabla2.AddCell(CeldaImprimeTexto);

            CeldaImprimeTexto = new PdfPCell(new Phrase("Total de Pollos", Fuente_Arial_Negra_8));
            CeldaImprimeTexto.Colspan = 2;
            CeldaImprimeTexto.Border = Bordes_Inferior;
            CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_CENTER;
            tabla2.AddCell(CeldaImprimeTexto);

            CeldaImprimeTexto = new PdfPCell(new Phrase("Observación", Fuente_Arial_Negra_8));
            CeldaImprimeTexto.Colspan = 4;
            CeldaImprimeTexto.Border = Bordes_Inferior;
            CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_CENTER;
            tabla2.AddCell(CeldaImprimeTexto);

            CeldaImprimeTexto = new PdfPCell(new Phrase("Total", Fuente_Arial_Negra_8));
            CeldaImprimeTexto.Colspan = 2;
            CeldaImprimeTexto.Border = Bordes_Inferior;
            CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_CENTER;
            tabla2.AddCell(CeldaImprimeTexto);

            double kilosTotales = 0;
            double piezasTotales = 0;
            double totalGeneral = 0;
            List<DocumentModel> documentsList = null;
            if (queryType != 4)
            {
                documentsList = DocumentModel.getAllDocumentsWithParamtersDates(queryDocuments, parameterName1, parameterName2,
                parameterValue1, parameterValue2);
            }
            else
            {
                documentsList = DocumentModel.getAllDocumentsWithParamtersToSearch(queryDocuments, parameterName1, parameterValue1, parameterName2,
                    parameterValue2);
            }
            if (documentsList != null)
            {
                foreach (var documento in documentsList)
                {
                    String customerName = CustomerModel.getName(documento.cliente_id);
                    CeldaImprimeTexto = new PdfPCell(new Phrase(customerName, Fuente_Arial_Negra_8));
                    CeldaImprimeTexto.Colspan = 4;
                    CeldaImprimeTexto.Border = Bordes_Ninguno;
                    CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_LEFT;
                    tabla2.AddCell(CeldaImprimeTexto);

                    CeldaImprimeTexto = new PdfPCell(new Phrase(documento.fventa.ToString(), Fuente_Arial_Negra_8));
                    CeldaImprimeTexto.Colspan = 2;
                    CeldaImprimeTexto.Border = Bordes_Ninguno;
                    CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_CENTER;
                    tabla2.AddCell(CeldaImprimeTexto);

                    CeldaImprimeTexto = new PdfPCell(new Phrase(documento.fechahoramov.ToString(), Fuente_Arial_Negra_8));
                    CeldaImprimeTexto.Colspan = 3;
                    CeldaImprimeTexto.Border = Bordes_Ninguno;
                    CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_CENTER;
                    tabla2.AddCell(CeldaImprimeTexto);
                    String movimientos = "";
                    String cantidadPollos = "";
                    String observations = "";
                    if (documento.observacion != null)
                        observations += documento.observacion;
                    String capturedUnitsName = "";
                    String nonCapturedUnitsName = "";
                    List<MovimientosModel> movementsList = MovimientosModel.getAllMovementsFromADocument(documento.id);
                    if (movementsList != null)
                    {
                        foreach (var movement in movementsList)
                        {
                            String itemName = ItemModel.getTheNameOfAnItem(movement.itemId);
                            capturedUnitsName = UnitsOfMeasureAndWeightModel.getNameOfAndUnitMeasureWeight(movement.capturedUnitId);
                            movimientos += itemName + "\r\nPeso Neto: " + movement.capturedUnits+" "+ capturedUnitsName + "\r\n";
                            kilosTotales += movement.capturedUnits;
                            nonCapturedUnitsName = UnitsOfMeasureAndWeightModel.getNameOfAndUnitMeasureWeight(movement.nonConvertibleUnitId);
                            cantidadPollos = movement.nonConvertibleUnits + " " + nonCapturedUnitsName;
                            piezasTotales += movement.nonConvertibleUnits;
                            observations += "\r\n"+movement.observations;
                        }
                    }
                    CeldaImprimeTexto = new PdfPCell(new Phrase(movimientos, Fuente_Arial_Negra_8));
                    CeldaImprimeTexto.Colspan = 3;
                    CeldaImprimeTexto.Border = Bordes_Ninguno;
                    CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_CENTER;
                    tabla2.AddCell(CeldaImprimeTexto);

                    CeldaImprimeTexto = new PdfPCell(new Phrase(cantidadPollos, Fuente_Arial_Negra_8));
                    CeldaImprimeTexto.Colspan = 2;
                    CeldaImprimeTexto.Border = Bordes_Ninguno;
                    CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_RIGHT;
                    tabla2.AddCell(CeldaImprimeTexto);

                    CeldaImprimeTexto = new PdfPCell(new Phrase(observations, Fuente_Arial_Negra_8));
                    CeldaImprimeTexto.Colspan = 4;
                    CeldaImprimeTexto.Border = Bordes_Ninguno;
                    CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_RIGHT;
                    tabla2.AddCell(CeldaImprimeTexto);

                    CeldaImprimeTexto = new PdfPCell(new Phrase(documento.total.ToString("C", CultureInfo.CurrentCulture), Fuente_Arial_Negra_8));
                    CeldaImprimeTexto.Colspan = 2;
                    CeldaImprimeTexto.Border = Bordes_Ninguno;
                    CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_RIGHT;
                    tabla2.AddCell(CeldaImprimeTexto);
                    totalGeneral += documento.total;
                }
            }

            PdfPTable Tabla3 = new PdfPTable(10);
            Tabla3.TotalWidth = dimension;
            Tabla3.LockedWidth = true;

            #region ENCABEZADO
            CeldaImprimeTexto = new PdfPCell(new Phrase("", Fuente_Arial_Azul_Bold_12));
            CeldaImprimeTexto.Colspan = 2;
            CeldaImprimeTexto.Border = Bordes_Ninguno;
            CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_LEFT;
            Tabla3.AddCell(CeldaImprimeTexto);

            CeldaImprimeTexto = new PdfPCell(new Phrase("Piezas Totales", Fuente_Arial_Azul_Bold_12));
            CeldaImprimeTexto.Colspan = 3;
            CeldaImprimeTexto.Border = Bordes_Ninguno;
            CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_CENTER;
            Tabla3.AddCell(CeldaImprimeTexto);

            CeldaImprimeTexto = new PdfPCell(new Phrase("Kilos Netos Totales", Fuente_Arial_Azul_Bold_12));
            CeldaImprimeTexto.Colspan = 3;
            CeldaImprimeTexto.Border = Bordes_Ninguno;
            CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_CENTER;
            Tabla3.AddCell(CeldaImprimeTexto);

            CeldaImprimeTexto = new PdfPCell(new Phrase("Total General", Fuente_Arial_Azul_Bold_12));
            CeldaImprimeTexto.Colspan = 3;
            CeldaImprimeTexto.Border = Bordes_Ninguno;
            CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_LEFT;
            Tabla3.AddCell(CeldaImprimeTexto);

            CeldaImprimeTexto = new PdfPCell(new Phrase("", Fuente_Arial_Azul_Bold_10));
            CeldaImprimeTexto.Colspan = 2;
            CeldaImprimeTexto.Border = Bordes_Ninguno;
            CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_CENTER;
            Tabla3.AddCell(CeldaImprimeTexto);

            CeldaImprimeTexto = new PdfPCell(new Phrase(piezasTotales.ToString() + " Pzs", Fuente_Arial_Azul_Bold_12));
            CeldaImprimeTexto.Colspan = 3;
            CeldaImprimeTexto.Border = Bordes_Superior;
            CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_CENTER;
            Tabla3.AddCell(CeldaImprimeTexto);

            CeldaImprimeTexto = new PdfPCell(new Phrase(kilosTotales.ToString() + " Kg", Fuente_Arial_Azul_Bold_12));
            CeldaImprimeTexto.Colspan = 3;
            CeldaImprimeTexto.Border = Bordes_Superior;
            CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_CENTER;
            Tabla3.AddCell(CeldaImprimeTexto);

            CeldaImprimeTexto = new PdfPCell(new Phrase(totalGeneral.ToString("C", CultureInfo.CurrentCulture)+" MXN", Fuente_Arial_Azul_Bold_12));
            CeldaImprimeTexto.Colspan = 3;
            CeldaImprimeTexto.Border = Bordes_Superior;
            CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_CENTER;
            Tabla3.AddCell(CeldaImprimeTexto);
            #endregion

            //Agrega las tablas con la información al documento.
            document.Add(TablaEncabezado);
            document.Add(TablaEspacio);
            document.Add(tabla2);
            document.Add(TablaEspacio);
            document.Add(TablaEspacio);
            document.Add(TablaEspacio);
            document.Add(Tabla3);
            //document.Add(Tabla4);
            //document.Add(Tabla5);

            document.Close();
            try
            {
                if (File.Exists(destinationFile))
                {
                    File.Delete(destinationFile);
                }
                File.Move(sourceFile, destinationFile);
                created = true;
            }
            catch (Exception ex)
            {
                //MensajeError = ex.Message;
                SECUDOC.writeLog("-Crear PDF de Retiros-");
                SECUDOC.writeLog(ex.ToString());
            }
            return created;
        }

        public static async Task<bool> crearPdfPrepedidos(String nombreEmpresa, String rutaArchivo, String queryPrepedido,
            String parameterName1, String parameterName2, String parameterValue1, String parameterValue2, int queryType)
        {
            bool created = false;
            await Task.Run(async () =>
            {
                //Creates an instance of the iTextSharp.text.Document-object:
                //Document document = new Document(PageSize.LETTER, 30, 30, 20, 30);
                //                                tamañoPagina, mIzq, mDer, mTop, mBot                            
                Document document = new Document(PageSize.LETTER, 30, 30, 9, 1);
                //float dimension = 570f;
                float dimension = 556f;
                string sourceFile = Application.StartupPath + @"\Temp.pdf";
                string destinationFile = rutaArchivo;
                PdfWriter.GetInstance(document, new FileStream(sourceFile, FileMode.Create));
                document.Open();

                //Agrega una tabla (espacio en blanco)
                PdfPTable TablaEspacio = new PdfPTable(1);
                TablaEspacio.TotalWidth = dimension;
                TablaEspacio.LockedWidth = true;
                PdfPCell CeldaEspacio = new PdfPCell(new Phrase(" ", Fuente_Espacio_2));
                CeldaEspacio.Border = Bordes_Ninguno;
                TablaEspacio.AddCell(CeldaEspacio);

                PdfPCell CeldaImprimeTexto;

                //Comienza llenado de archivo
                PdfPTable TablaEncabezado = new PdfPTable(1);
                TablaEncabezado.TotalWidth = dimension;
                TablaEncabezado.LockedWidth = true;

                CeldaImprimeTexto = new PdfPCell(new Phrase(nombreEmpresa, Fuente_Arial_Azul_Bold_16));
                CeldaImprimeTexto.Colspan = 1;
                CeldaImprimeTexto.Border = Bordes_Ninguno;
                CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_CENTER;
                TablaEncabezado.AddCell(CeldaImprimeTexto);

                CeldaImprimeTexto = new PdfPCell(new Phrase("Pedidos", Fuente_Arial_Azul_Bold_12));
                CeldaImprimeTexto.Colspan = 1;
                CeldaImprimeTexto.Border = Bordes_Ninguno;
                CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_CENTER;
                TablaEncabezado.AddCell(CeldaImprimeTexto);

                CeldaImprimeTexto = new PdfPCell(new Phrase(Sangria_2, Fuente_Arial_Azul_Bold_12));
                CeldaImprimeTexto.Colspan = 1;
                CeldaImprimeTexto.Border = Bordes_Ninguno;
                CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_CENTER;
                TablaEncabezado.AddCell(CeldaImprimeTexto);

                PdfPTable tabla2 = new PdfPTable(20);
                tabla2.TotalWidth = dimension;
                tabla2.LockedWidth = true;

                CeldaImprimeTexto = new PdfPCell(new Phrase("Tipo", Fuente_Arial_Negra_8));
                CeldaImprimeTexto.Colspan = 2;
                CeldaImprimeTexto.Border = Bordes_Inferior;
                CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_CENTER;
                tabla2.AddCell(CeldaImprimeTexto);

                CeldaImprimeTexto = new PdfPCell(new Phrase("Cliente", Fuente_Arial_Negra_8));
                CeldaImprimeTexto.Colspan = 4;
                CeldaImprimeTexto.Border = Bordes_Inferior;
                CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_CENTER;
                tabla2.AddCell(CeldaImprimeTexto);

                CeldaImprimeTexto = new PdfPCell(new Phrase("Folio", Fuente_Arial_Negra_8));
                CeldaImprimeTexto.Colspan = 2;
                CeldaImprimeTexto.Border = Bordes_Inferior;
                CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_CENTER;
                tabla2.AddCell(CeldaImprimeTexto);

                CeldaImprimeTexto = new PdfPCell(new Phrase("Fecha", Fuente_Arial_Negra_8));
                CeldaImprimeTexto.Colspan = 3;
                CeldaImprimeTexto.Border = Bordes_Inferior;
                CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_CENTER;
                tabla2.AddCell(CeldaImprimeTexto);

                CeldaImprimeTexto = new PdfPCell(new Phrase("Producto", Fuente_Arial_Negra_8));
                CeldaImprimeTexto.Colspan = 3;
                CeldaImprimeTexto.Border = Bordes_Inferior;
                CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_CENTER;
                tabla2.AddCell(CeldaImprimeTexto);

                CeldaImprimeTexto = new PdfPCell(new Phrase("Total de Pollos", Fuente_Arial_Negra_8));
                CeldaImprimeTexto.Colspan = 2;
                CeldaImprimeTexto.Border = Bordes_Inferior;
                CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_CENTER;
                tabla2.AddCell(CeldaImprimeTexto);

                CeldaImprimeTexto = new PdfPCell(new Phrase("Observación", Fuente_Arial_Negra_8));
                CeldaImprimeTexto.Colspan = 4;
                CeldaImprimeTexto.Border = Bordes_Inferior;
                CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_CENTER;
                tabla2.AddCell(CeldaImprimeTexto);

                double kilosTotales = 0;
                double piezasTotales = 0;
                List<PedidosEncabezadoModel> documentsList = null;
                if (queryType == 0)
                {
                    documentsList = PedidosEncabezadoModel.getAllDocuments(queryPrepedido);
                }
                else
                {
                    documentsList = PedidosEncabezadoModel.getAllDocumentsWithParametersToSearch(queryPrepedido, parameterName1, parameterValue1,
                        parameterName2, parameterValue2);
                }
                if (documentsList != null)
                {
                    foreach (var documento in documentsList)
                    {
                        BaseColor colorIdentificador = BaseColor.BLUE;
                        String nombrePedido = "";
                        String query = "SELECT R." + LocalDatabase.CAMPO_NAME_RUTA + " FROM " + LocalDatabase.TABLA_RUTA + " R" +
                            " INNER JOIN " + LocalDatabase.TABLA_USUARIO + " U ON U." + LocalDatabase.CAMPO_RUTA_USER + " = R." +
                            LocalDatabase.CAMPO_CODE_RUTA + " INNER JOIN " + LocalDatabase.TABLA_PEDIDOENCABEZADO + " PE ON PE." +
                            LocalDatabase.CAMPO_CNOMBREAGENTECC_PE + " = U." + LocalDatabase.CAMPO_ID_USUARIO + " WHERE PE." +
                            LocalDatabase.CAMPO_CNOMBREAGENTECC_PE + " = " + documento.agenteId + " LIMIT 1";
                        String routeName = RutaModel.getStringValue(query);
                        if (documento.surtido == 0)
                        {
                            nombrePedido = "Pedido sin surtir\r\n" + routeName;
                        }
                        else
                        {
                            nombrePedido = "Pedido pausado\r\n" + routeName;
                            colorIdentificador = BaseColor.YELLOW;
                        }
                        CeldaImprimeTexto = new PdfPCell(new Phrase(nombrePedido, Fuente_Arial_Negra_8));
                        CeldaImprimeTexto.Colspan = 2;
                        CeldaImprimeTexto.Border = Bordes_Izquierdo;
                        CeldaImprimeTexto.BorderColor = colorIdentificador;
                        CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_CENTER;
                        tabla2.AddCell(CeldaImprimeTexto);

                        String customerName = CustomerModel.getName(documento.clienteId);
                        CeldaImprimeTexto = new PdfPCell(new Phrase(customerName, Fuente_Arial_Negra_8));
                        CeldaImprimeTexto.Colspan = 4;
                        CeldaImprimeTexto.Border = Bordes_Ninguno;
                        CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_LEFT;
                        tabla2.AddCell(CeldaImprimeTexto);

                        CeldaImprimeTexto = new PdfPCell(new Phrase(documento.folio.ToString(), Fuente_Arial_Negra_8));
                        CeldaImprimeTexto.Colspan = 2;
                        CeldaImprimeTexto.Border = Bordes_Ninguno;
                        CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_CENTER;
                        tabla2.AddCell(CeldaImprimeTexto);

                        CeldaImprimeTexto = new PdfPCell(new Phrase(documento.fechaHora.ToString(), Fuente_Arial_Negra_8));
                        CeldaImprimeTexto.Colspan = 3;
                        CeldaImprimeTexto.Border = Bordes_Ninguno;
                        CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_CENTER;
                        tabla2.AddCell(CeldaImprimeTexto);
                        String movimientos = "";
                        String cantidadPollos = "";
                        String observations = "";
                        if (documento.observation != null)
                            observations += documento.observation;
                        String capturedUnitsName = "";
                        String nonCapturedUnitsName = "";
                        List<PedidoDetalleModel> movementsList = PedidoDetalleModel.getAllMovementsFromAnOrder(documento.idDocumento);
                        if (movementsList != null)
                        {
                            foreach (var movement in movementsList)
                            {
                                String itemName = ItemModel.getTheNameOfAnItem(movement.itemId);
                                capturedUnitsName = UnitsOfMeasureAndWeightModel.getNameOfAndUnitMeasureWeight(movement.unidadCapturadaId);
                                if (documento.surtido == 0)
                                {
                                    movimientos += itemName + "\r\n";
                                }
                                else
                                {
                                    query = "SELECT " + LocalDatabase.CAMPO_ID_DOC + " FROM " + LocalDatabase.TABLA_DOCUMENTOVENTA + " WHERE " +
                                    LocalDatabase.CAMPO_CIDDOCTOPEDIDOCC_DOC + " = " + documento.idDocumento;
                                    int idDocumento = DocumentModel.getIntValue(query);
                                    String queryMov = "SELECT * FROM "+LocalDatabase.TABLA_MOVIMIENTO+" WHERE "+
                                    LocalDatabase.CAMPO_DOCUMENTOID_MOV+" = "+idDocumento;
                                    MovimientosModel movimiento = MovimientosModel.getAMovement(queryMov);
                                    if (movimiento != null)
                                    {
                                        movimientos += itemName + "\r\nPeso Neto: " + movimiento.capturedUnits + " " + capturedUnitsName + "\r\n";
                                        kilosTotales += movimiento.capturedUnits;
                                    } else
                                    {
                                        movimientos += itemName + "\r\n";
                                    }
                                }
                                nonCapturedUnitsName = UnitsOfMeasureAndWeightModel.getNameOfAndUnitMeasureWeight(movement.unidadNoConvertibleId);
                                cantidadPollos = movement.unidadesNoConvertibles + " " + nonCapturedUnitsName;
                                piezasTotales += movement.unidadesNoConvertibles;
                                observations += "\r\n" + movement.observation;
                            }
                        }
                        CeldaImprimeTexto = new PdfPCell(new Phrase(movimientos, Fuente_Arial_Negra_8));
                        CeldaImprimeTexto.Colspan = 3;
                        CeldaImprimeTexto.Border = Bordes_Ninguno;
                        CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_CENTER;
                        tabla2.AddCell(CeldaImprimeTexto);

                        CeldaImprimeTexto = new PdfPCell(new Phrase(cantidadPollos, Fuente_Arial_Negra_8));
                        CeldaImprimeTexto.Colspan = 2;
                        CeldaImprimeTexto.Border = Bordes_Ninguno;
                        CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_RIGHT;
                        tabla2.AddCell(CeldaImprimeTexto);

                        CeldaImprimeTexto = new PdfPCell(new Phrase(observations, Fuente_Arial_Negra_8));
                        CeldaImprimeTexto.Colspan = 4;
                        CeldaImprimeTexto.Border = Bordes_Ninguno;
                        CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_CENTER;//Element.ALIGN_RIGHT;
                        tabla2.AddCell(CeldaImprimeTexto);

                    }
                }

                PdfPTable Tabla3 = new PdfPTable(10);
                Tabla3.TotalWidth = dimension;
                Tabla3.LockedWidth = true;

                #region ENCABEZADO
                CeldaImprimeTexto = new PdfPCell(new Phrase("", Fuente_Arial_Azul_Bold_12));
                CeldaImprimeTexto.Colspan = 1;
                CeldaImprimeTexto.Border = Bordes_Ninguno;
                CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_LEFT;
                Tabla3.AddCell(CeldaImprimeTexto);

                CeldaImprimeTexto = new PdfPCell(new Phrase("", Fuente_Arial_Azul_Bold_10));
                CeldaImprimeTexto.Colspan = 4;
                CeldaImprimeTexto.Border = Bordes_Ninguno;
                CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_CENTER;
                Tabla3.AddCell(CeldaImprimeTexto);

                CeldaImprimeTexto = new PdfPCell(new Phrase("Piezas Totales", Fuente_Arial_Azul_Bold_12));
                CeldaImprimeTexto.Colspan = 3;
                CeldaImprimeTexto.Border = Bordes_Ninguno;
                CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_CENTER;
                Tabla3.AddCell(CeldaImprimeTexto);

                CeldaImprimeTexto = new PdfPCell(new Phrase("Kilos Netos Totales", Fuente_Arial_Azul_Bold_12));
                CeldaImprimeTexto.Colspan = 4;
                CeldaImprimeTexto.Border = Bordes_Ninguno;
                CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_CENTER;
                Tabla3.AddCell(CeldaImprimeTexto);

                CeldaImprimeTexto = new PdfPCell(new Phrase("", Fuente_Arial_Azul_Bold_12));
                CeldaImprimeTexto.Colspan = 1;
                CeldaImprimeTexto.Border = Bordes_Ninguno;
                CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_LEFT;
                Tabla3.AddCell(CeldaImprimeTexto);

                CeldaImprimeTexto = new PdfPCell(new Phrase("", Fuente_Arial_Azul_Bold_10));
                CeldaImprimeTexto.Colspan = 4;
                CeldaImprimeTexto.Border = Bordes_Ninguno;
                CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_CENTER;
                Tabla3.AddCell(CeldaImprimeTexto);

                CeldaImprimeTexto = new PdfPCell(new Phrase(piezasTotales.ToString() + " Pzs", Fuente_Arial_Azul_Bold_12));
                CeldaImprimeTexto.Colspan = 3;
                CeldaImprimeTexto.Border = Bordes_Superior;
                CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_CENTER;
                Tabla3.AddCell(CeldaImprimeTexto);

                CeldaImprimeTexto = new PdfPCell(new Phrase(kilosTotales.ToString() + " Kg", Fuente_Arial_Azul_Bold_12));
                CeldaImprimeTexto.Colspan = 4;
                CeldaImprimeTexto.Border = Bordes_Superior;
                CeldaImprimeTexto.HorizontalAlignment = Element.ALIGN_CENTER;
                Tabla3.AddCell(CeldaImprimeTexto);
                #endregion

                //Agrega las tablas con la información al documento.
                document.Add(TablaEncabezado);
                document.Add(TablaEspacio);
                document.Add(tabla2);
                document.Add(TablaEspacio);
                document.Add(TablaEspacio);
                document.Add(TablaEspacio);
                document.Add(Tabla3);
                //document.Add(Tabla4);
                //document.Add(Tabla5);

                document.Close();
                try
                {
                    if (File.Exists(destinationFile))
                    {
                        File.Delete(destinationFile);
                    }
                    File.Move(sourceFile, destinationFile);
                    created = true;
                }
                catch (Exception ex)
                {
                    SECUDOC.writeLog(ex.ToString());
                }
            });
            return created;
        }


    }
}
