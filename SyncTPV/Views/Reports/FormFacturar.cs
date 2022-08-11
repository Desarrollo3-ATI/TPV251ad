using SyncTPV.Controllers;
using SyncTPV.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SyncTPV.Views.Reports
{
    public partial class FormFacturar : Form
    {
        private int idDocumentoLocal = 0, idDocumentoServer;
        private DocumentModel documentModel = null;
        private FormWaiting frmWaiting;
        private bool permissionPrepedido = false;
        private bool serverModeLAN = false;

        public FormFacturar(int idDocumentoLocal, int idDocumentoServer)
        {
            this.idDocumentoLocal = idDocumentoLocal;
            this.idDocumentoServer = idDocumentoServer;
            permissionPrepedido = UserModel.doYouHavePermissionPrepedido();
            InitializeComponent();
            initializeElements();
            serverModeLAN = ConfiguracionModel.isLANPermissionActivated();
        }

        private void FormFacturar_Load(object sender, EventArgs e)
        {
            frmWaiting = new FormWaiting(this, FormWaiting.CALL_DOWNLOAD_DIRECTORIOSERVER, 0);
            frmWaiting.StartPosition = FormStartPosition.CenterScreen;
            frmWaiting.ShowDialog();
        }

        private async Task showInformationAboutPdfAndXml()
        {
            documentModel = DocumentModel.getAllDataDocumento(idDocumentoLocal);
            if (documentModel != null)
            {
                String codigoCliente = "";
                if (documentModel.cliente_id > 0)
                {
                    codigoCliente = documentModel.clave_cliente;
                } else
                {
                    FormMessage formMessage = new FormMessage("Información del Cliente Desactualizada", "1.- Si el cliente es nuevo validar que esté correctamente enviado a comercial.\r\n" +
                        "2.- Posteriormente entrar al Perfil del cliente para que la información sea actualizada.",3);
                    formMessage.ShowDialog();
                    codigoCliente = documentModel.clave_cliente;
                }
            }
            DirectoriosModel dm = DirectoriosModel.getDirectorioByType(DirectoriosModel.TIPO_FACTURAS_SERVER);
            if (dm != null)
            {
                FoliosDigitalesModel fdm = FoliosDigitalesModel.getFolioDigital(idDocumentoServer);
                if (fdm != null)
                {
                    String folio = Convert.ToInt32(fdm.folio).ToString("D10");
                    String nombreArchivo = fdm.rfcEmisor + "F" + fdm.serieConcepto + "" + folio;
                    String filePathPdf = @""+dm.ruta + "\\" + nombreArchivo + ".pdf";
                    if (File.Exists(filePathPdf))
                    {
                        editPdf.Text = filePathPdf;
                        //Process.Start("Explorer.exe", "/select, " + filePath);
                    }
                    String filePathXml = @""+dm.ruta + "\\" + nombreArchivo + ".pdf";
                    if (File.Exists(filePathXml))
                    {
                        editXml.Text = filePathXml;
                    }
                } else
                {
                    FormMessage formMessage = new FormMessage("Datos No encontrados", "Los datos de la factura no fueron encontrados, intenta descargar los archivos", 3);
                    formMessage.ShowDialog();
                }
            } else
            {
                editPdf.Text = "Agregar Ruta de la Carpeta de Factura en el PanelROM";
                editXml.Text = "Agregar Ruta de la Carpeta de Factura en el PanelROM";
                btnDownloadPfd.Enabled = false;
                btnDownloadXml.Enabled = false;
            }
            dm = DirectoriosModel.getDirectorioByType(DirectoriosModel.TIPO_FACTURAS_TERMINAL);
            if (dm != null)
                editRutaTerminal.Text = dm.ruta;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnDownloadPfd_Click(object sender, EventArgs e)
        {
            processToDownloadPdfOrXmlFiles(0);
        }

        private async Task processToDownloadPdfOrXmlFiles(int tipo)
        {
            String serverDirectory = DirectoriosModel.getRutaByType(DirectoriosModel.TIPO_FACTURAS_SERVER);
            String localDirectory = DirectoriosModel.getRutaByType(DirectoriosModel.TIPO_FACTURAS_TERMINAL);
            if (serverDirectory.Equals(""))
            {
                FormMessage formMessage = new FormMessage("Ruta Servidor No Encontrada", "Tienes que agregar una ruta de " +
                    "almacenamiento donde se guardaran las facturas generadas por Comercial esto en el PanelROM",3);
                formMessage.ShowDialog();
            }
            else
            {
                if (localDirectory.Equals(""))
                {
                    FormMessage formMessage = new FormMessage("Ruta Local No Encontrada", "Tienes que agregar una ruta de " +
                    "almacenamiento donde se guardaran las facturas descargadas del servidor", 3);
                    formMessage.ShowDialog();
                } else
                {
                    dynamic response = null;
                    if (ConfiguracionModel.isLANPermissionActivated())
                        response = await FolioDigitalController.getFolioDigitalLAN(idDocumentoServer);
                    else response = await FolioDigitalController.getFolioDigitalWs(idDocumentoServer);
                    if (response.valor >= 1)
                    {
                        logicToDownloadInvoiceFiles(tipo);
                    } else if (response.valor == 0)
                    {
                        FormMessage formMessage = new FormMessage("Exception", "" + response.description, 3);
                        formMessage.ShowDialog();
                    } else
                    {
                        FormMessage formMessage = new FormMessage("Exception",""+response.description,2);
                        formMessage.ShowDialog();
                    }
                }
            }
        }

        private async Task logicToDownloadInvoiceFiles(int tipo)
        {
            frmWaiting = new FormWaiting(this, 2, tipo);
            frmWaiting.StartPosition = FormStartPosition.CenterScreen;
            frmWaiting.ShowDialog();
        }

        public async Task getInvoiceFiles(String extension)
        {
            FoliosDigitalesModel fdm = FoliosDigitalesModel.getFolioDigital(idDocumentoServer);
            if (fdm != null)
            {
                dynamic response = null;
                if (serverModeLAN)
                    response = await UploadAndDownloadFilesController.getInvoiceFileLAN(fdm.serieConcepto, Convert.ToInt32(fdm.folio), fdm.rfcEmisor, extension);
                else response = await UploadAndDownloadFilesController.getInvoiceFileWs(fdm.serieConcepto, Convert.ToInt32(fdm.folio), fdm.rfcEmisor, extension);
                if (frmWaiting != null)
                    frmWaiting.Close();
                if (response.valor >= 1)
                {
                    FormMessage formMessage = new FormMessage("Descarga Completa", "Puedes encontrar el archivo con el nombre de: \r\n" +
                        ""+response.description, 1);
                    formMessage.ShowDialog();
                    if (extension.Equals("pdf"))
                        editPdf.Text = response.description;
                    else editXml.Text = response.description;
                }
                else if (response.valor == 0)
                {
                    FormMessage formMessage = new FormMessage("Exception", "" + response.description, 3);
                    formMessage.ShowDialog();
                }
                else
                {
                    FormMessage formMessage = new FormMessage("Exception", "" + response.description, 2);
                    formMessage.ShowDialog();
                }
            } else
            {
                if (frmWaiting != null)
                    frmWaiting.Close();
                FormMessage formMessage = new FormMessage("Información de la Factura Faltante", "Los datos de la factura (Folios Digitales) " +
                    "No se pudieron encontrar", 3);
                formMessage.ShowDialog();
            }
        }

        public async Task processToDownloadRouteInvoice()
        {
            /* Descargar Ruta del directorio facturas */
            dynamic response = null;
            bool modeLAN = ConfiguracionModel.isLANPermissionActivated();
            if (modeLAN)
                response = await DirectoriosController.getDirectoriesRoutesLAN(DirectoriosModel.TIPO_FACTURAS_SERVER);
            else response = await DirectoriosController.getDirectoriesRoutesWs(DirectoriosModel.TIPO_FACTURAS_SERVER);
            if (frmWaiting != null)
                frmWaiting.Close();
            if (response.valor >= 1)
            {
                await showInformationAboutPdfAndXml();
            } else if (response.valor == 0)
            {
                FormMessage formMessage = new FormMessage("Datos No Encontrados", "No encontramos ninguna ruta del directorio para facturas\r\n" +
                    "Debes agregar una en el módulo Datos Extras del PanelROM", 3);
                formMessage.ShowDialog();
            } else
            {
                FormMessage formMessage = new FormMessage("Exception",response.description,3);
                formMessage.ShowDialog();
            }
        }

        private void btnDownloadXml_Click(object sender, EventArgs e)
        {
            processToDownloadPdfOrXmlFiles(1);
        }

        private void btnVerPdf_Click(object sender, EventArgs e)
        {
            DirectoriosModel dm = DirectoriosModel.getDirectorioByType(DirectoriosModel.TIPO_FACTURAS_TERMINAL);
            if (dm != null)
            {
                FoliosDigitalesModel fdm = FoliosDigitalesModel.getFolioDigital(idDocumentoServer);
                if (fdm != null)
                {
                    String nombreArchivo = fdm.serieConcepto + "" + fdm.folio;
                    String filePath = @""+dm.ruta + "\\" + nombreArchivo + ".pdf";
                    if (File.Exists(filePath))
                    {
                        Process.Start("Explorer.exe", "/select, " + filePath);
                    }
                } else
                {
                    FormMessage formMessage = new FormMessage("Datos No encontrados", "Los datos de la factura no fueron encontrados, intenta descargasrlos", 3);
                    formMessage.ShowDialog();
                }
            } else
            {
                FormMessage formMessage = new FormMessage("Datos No encontrados", "La ruta de almacenamiento local no fue encontrada", 3);
                formMessage.ShowDialog();
            }
        }

        private void btnVerXml_Click(object sender, EventArgs e)
        {
            DirectoriosModel dm = DirectoriosModel.getDirectorioByType(DirectoriosModel.TIPO_FACTURAS_TERMINAL);
            if (dm != null)
            {
                FoliosDigitalesModel fdm = FoliosDigitalesModel.getFolioDigital(idDocumentoServer);
                if (fdm != null)
                {
                    String nombreArchivo = fdm.serieConcepto + "" + fdm.folio;
                    String filePath = @"" + dm.ruta + "\\" + nombreArchivo + ".xml";
                    if (File.Exists(filePath))
                    {
                        Process.Start("Explorer.exe", "/select, " + filePath);
                    } else
                    {
                        FormMessage formMessage = new FormMessage("Datos No encontrados", "Los datos de la factura XML en la terminal no fueron encontrados, intenta descargarlos", 3);
                        formMessage.ShowDialog();
                    }
                }
                else
                {
                    FormMessage formMessage = new FormMessage("Datos No encontrados", "Los datos de la factura en la terminal no fueron encontrados, intenta descargarlos", 3);
                    formMessage.ShowDialog();
                }
            }
            else
            {
                FormMessage formMessage = new FormMessage("Datos No encontrados", "La ruta de almacenamiento local no fue encontrada", 3);
                formMessage.ShowDialog();
            }
        }

        private void btnImprimirTicket_Click(object sender, EventArgs e)
        {
            frmWaiting = new FormWaiting(this, 3, 0);
            frmWaiting.StartPosition = FormStartPosition.CenterScreen;
            frmWaiting.ShowDialog();
        }

        public async Task processtoValidatePrintInvoice()
        {
            String serverDirectory = DirectoriosModel.getRutaByType(DirectoriosModel.TIPO_FACTURAS_SERVER);
            String localDirectory = DirectoriosModel.getRutaByType(DirectoriosModel.TIPO_FACTURAS_TERMINAL);
            if (serverDirectory.Equals(""))
            {
                FormMessage formMessage = new FormMessage("Ruta Servidor No Encontrada", "Tienes que agregar una ruta de " +
                    "almacenamiento donde se guardaran las facturas generadas por Comercial esto en el PanelROM", 3);
                formMessage.ShowDialog();
            }
            else
            {
                if (localDirectory.Equals(""))
                {
                    FormMessage formMessage = new FormMessage("Ruta Local No Encontrada", "Tienes que agregar una ruta de " +
                    "almacenamiento donde se guardaran las facturas descargadas del servidor", 3);
                    formMessage.ShowDialog();
                }
                else
                {
                    dynamic response = null;
                    if (ConfiguracionModel.isLANPermissionActivated())
                        response = await FolioDigitalController.getFolioDigitalLAN(idDocumentoServer);
                    else response = await FolioDigitalController.getFolioDigitalWs(idDocumentoServer);
                    if (frmWaiting != null)
                        frmWaiting.Close();
                    if (response.valor >= 1)
                    {
                        clsTicket ticket = new clsTicket();
                        ticket.imprimirTicketFactura(idDocumentoLocal, false, permissionPrepedido, idDocumentoServer, serverModeLAN);
                    }
                    else if (response.valor == 0)
                    {
                        FormMessage formMessage = new FormMessage("Exception", "" + response.description, 3);
                        formMessage.ShowDialog();
                    }
                    else
                    {
                        FormMessage formMessage = new FormMessage("Exception", "" + response.description, 2);
                        formMessage.ShowDialog();
                    }
                }
            }
        }

        private void initializeElements()
        {
            btnClose.Image = MetodosGenerales.redimencionarBitmap(Properties.Resources.back_white, 40, 40);
            btnDownloadPfd.Image = MetodosGenerales.redimencionarBitmap(Properties.Resources.download, 30, 30);
            btnDownloadXml.Image = MetodosGenerales.redimencionarBitmap(Properties.Resources.download, 30, 30);
            btnDownloadPfd.ImageAlign = ContentAlignment.MiddleLeft;
            btnDownloadPfd.TextAlign = ContentAlignment.MiddleRight;
            btnDownloadXml.ImageAlign = ContentAlignment.MiddleLeft;
            btnDownloadXml.TextAlign = ContentAlignment.MiddleRight;
            btnVerPdf.Image = MetodosGenerales.redimencionarBitmap(Properties.Resources.eye_black, 25, 25);
            btnVerPdf.ImageAlign = ContentAlignment.MiddleLeft;
            btnVerPdf.TextAlign = ContentAlignment.MiddleRight;
            btnVerXml.Image = MetodosGenerales.redimencionarBitmap(Properties.Resources.eye_black, 25, 25);
            btnVerXml.ImageAlign = ContentAlignment.MiddleLeft;
            btnVerXml.TextAlign = ContentAlignment.MiddleRight;
        }

        private void btnSeleccionarRutaTerminal_Click(object sender, EventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    editRutaTerminal.Text = fbd.SelectedPath;
                    String newRoute = editRutaTerminal.Text.Trim();
                    if (newRoute.Equals(""))
                    {
                        int records = DirectoriosModel.validateIfDirectoryTypeExist(DirectoriosModel.TIPO_FACTURAS_TERMINAL);
                        if (records > 0)
                        {
                            DirectoriosModel.deleteDirectoryByType(DirectoriosModel.TIPO_FACTURAS_TERMINAL);
                        }
                    } else
                    {
                        int records = DirectoriosModel.validateIfDirectoryTypeExist(DirectoriosModel.TIPO_FACTURAS_TERMINAL);
                        if (records == 1)
                        {
                            DirectoriosModel dm = DirectoriosModel.getDirectorioByType(DirectoriosModel.TIPO_FACTURAS_TERMINAL);
                            DirectoriosModel.updateDirectory(dm.id, DirectoriosModel.TIPO_FACTURAS_TERMINAL, dm.nombre, newRoute, dm.empresaId);
                        } else if (records > 1)
                        {
                            if (DirectoriosModel.deleteDirectoryByType(DirectoriosModel.TIPO_FACTURAS_TERMINAL))
                            {
                                int lastId = DirectoriosModel.getLastId();
                                lastId++;
                                DirectoriosModel.createDirectory(lastId, DirectoriosModel.TIPO_FACTURAS_TERMINAL, "Ruta Facturación Terminal",
                                    newRoute, 0);
                            }
                        }
                        else
                        {
                            int lastId = DirectoriosModel.getLastId();
                            lastId++;
                            DirectoriosModel.createDirectory(lastId, DirectoriosModel.TIPO_FACTURAS_TERMINAL, "Ruta Facturación Terminal",
                                newRoute, 0);
                        }
                    }
                    //string[] files = Directory.GetFiles(fbd.SelectedPath);
                    //System.Windows.Forms.MessageBox.Show("Files found: " + files.Length.ToString(), "Message");
                }
            }
        }
    }
}
