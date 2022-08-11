using SyncTPV.Controllers;
using SyncTPV.Helpers.SqliteDatabaseHelper;
using SyncTPV.Models;
using SyncTPV.Views.Mostrador;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Threading;
using Tulpep.NotificationWindow;
using wsROMClase;

namespace SyncTPV.Views
{
    public partial class FormPayCart : Form
    {
        private FormVenta formVenta;
        public static readonly int CALL_GET_DOCUMENT_DATA = 0;
        public static readonly int CALL_UPDATE_OBSERVATION_DOCUMENT_DATA = 1;
        public static readonly int CALL_END_DOCUMENT = 2;
        private static readonly String ARG_PARAM_ID_DOCUMENT = "param_id_document";
        public int idDocument = 0, tipoDeDocumento = 0;
        private Boolean changePaymentMethod = true;
        public static String documentObservation = "";
        private FormWaiting formWaiting;
        private int activateOpcionFacturar = 1;
        private bool permissionPrepedido = false, pesoBrutoCapturado = false, serverModeLAN = false;
        private bool useFiscalField = false, webActive = false;
        private int positionFiscalItemField = 6;
        private bool cotmosActive = false;

        public FormPayCart(FormVenta formVenta, int idDocument, int documentType, int activateOpcionFacturar,
            bool webActive, bool cotmosActive)
        {
            this.formVenta = formVenta;
            this.idDocument = idDocument;
            this.tipoDeDocumento = documentType;
            this.activateOpcionFacturar = activateOpcionFacturar;
            this.webActive = webActive;
            this.cotmosActive = cotmosActive;
            InitializeComponent();
            serverModeLAN = ConfiguracionModel.isLANPermissionActivated();
            permissionPrepedido = UserModel.doYouHavePermissionPrepedido();
            checkBoxInvoiceFrmPayCart.CheckedChanged += new EventHandler(checkBoxInvoiceFrmPayCart_CheckedChanged);
            btnObservacionesFrmPayCart.Click += new EventHandler(btnObservacionesFrmPayCart_Click);
            positionFiscalItemField = ConfiguracionModel.getPositionFiscalItemField();
        }

        private async void FrmPayCart_Load(object sender, EventArgs e)
        {
            int numCopias = await getNumerOfCopiesFromTheLocalDb();
            if (permissionPrepedido)
            {
                if (DocumentModel.isItDocumentFromAPrepedido(idDocument))
                    editNumeroCopias.Text = "" + numCopias;
                else editNumeroCopias.Text = "0";
            } else
            {
                editNumeroCopias.Text = "" + numCopias;
            }
            dataGridViewFcFrmPayCArt.AutoResizeColumns();
            if (tipoDeDocumento != 1)
            {
                checkBoxCotizacionMostrador.Enabled = false;
                checkBoxCotizacionMostrador.Checked = false;
                checkBoxCotizacionMostrador.Visible = false;
            }
            await buttonsEvents();
            await validateIfUserHaveSellOnCreditPermission();
            await validateUseFiscalProductField();
            if (permissionPrepedido)
            {
                this.Height = 329;
                panelFirstSectionFrmPayCart.Height = 0;
                panelGenerarFactura.Height = 0;
                textInfoSubtotalFrmPayCart.Visible = false;
                textSubtotalFrmPayCart.Visible = false;
                textInfoDescuentoFrmPayCart.Visible = false;
                textDescuentoFrmPayCart.Visible = false;
                textInfoPendienteFrmPayCart.Visible = false;
                textPendienteFrmPayCart.Visible = false;
                textInfoCambioFrmPayCart.Visible = false;
                //textCambioFrmPayCart.Visible = false;
                checkBoxCreditoFrmPayCart.Checked = true;
                if (DocumentModel.isItDocumentFromAPrepedido(idDocument))
                {
                    String query = "UPDATE " + LocalDatabase.TABLA_DOCUMENTOVENTA + " SET " + LocalDatabase.CAMPO_FORMACOBROID_DOC + " = 71 WHERE " +
                    LocalDatabase.CAMPO_ID_DOC + " = " + idDocument;
                    DocumentModel.updateARecord(query);
                } else
                {
                    String query = "UPDATE " + LocalDatabase.TABLA_DOCUMENTOVENTA + " SET " + LocalDatabase.CAMPO_FORMACOBROID_DOC + " = 0 WHERE " +
                    LocalDatabase.CAMPO_ID_DOC + " = " + idDocument;
                    DocumentModel.updateARecord(query);
                    String queryTipo = "UPDATE " + LocalDatabase.TABLA_DOCUMENTOVENTA + " SET " + LocalDatabase.CAMPO_TIPODOCUMENTO_DOC + " = "+
                        DocumentModel.TIPO_PREPEDIDO+" WHERE " +
                   LocalDatabase.CAMPO_ID_DOC + " = " + idDocument;
                    DocumentModel.updateARecord(queryTipo);
                }
                if (tipoDeDocumento == DocumentModel.TIPO_VENTA || tipoDeDocumento == DocumentModel.TIPO_REMISION)
                {
                    bool taraPesada = DocumentModel.pesoTaraCapturado(idDocument);
                    if (!taraPesada)
                    {
                        pesoBrutoCapturado = DocumentModel.pesoBrutoCapturado(idDocument);
                        if (!pesoBrutoCapturado)
                        {
                            textInfoTotalFrmPayCart.Text = "Peso Bruto No Capturado";
                            textTotalFrmPayCart.Text = "";
                            checkBoxCotizacionMostrador.Text = "Peso Bruto Faltante";
                            checkBoxCotizacionMostrador.Visible = true;
                            checkBoxCotizacionMostrador.Enabled = false;
                            checkBoxCotizacionMostrador.Checked = false;
                        }
                        else
                        {
                            checkBoxCotizacionMostrador.Text = "Peso de Taras Descontado";
                            checkBoxCotizacionMostrador.Visible = true;
                            checkBoxCotizacionMostrador.Enabled = true;
                            checkBoxCotizacionMostrador.Checked = false;
                        }
                    }
                    else
                    {
                        pesoBrutoCapturado = DocumentModel.pesoBrutoCapturado(idDocument);
                        if (!pesoBrutoCapturado)
                        {
                            textInfoTotalFrmPayCart.Text = "Peso Bruto No Capturado";
                            textTotalFrmPayCart.Text = "";
                            checkBoxCotizacionMostrador.Text = "Peso Bruto Faltante";
                            checkBoxCotizacionMostrador.Visible = true;
                            checkBoxCotizacionMostrador.Enabled = false;
                            checkBoxCotizacionMostrador.Checked = false;
                        }
                        else
                        {
                            textInfoTotalFrmPayCart.Text = "Peso Bruto No Capturado";
                            checkBoxCotizacionMostrador.Text = "Peso de Taras Descontado";
                            checkBoxCotizacionMostrador.Visible = true;
                            checkBoxCotizacionMostrador.Enabled = true;
                            checkBoxCotizacionMostrador.Checked = true;
                        }
                    }
                } else if (tipoDeDocumento == DocumentModel.TIPO_PREPEDIDO)
                {
                    this.Text = "Generar Prepedido";
                    textInfoTotalFrmPayCart.Text = "Prepedido";
                    textTotalFrmPayCart.Text = "";
                    checkBoxCotizacionMostrador.Text = "Peso Bruto Faltante";
                    checkBoxCotizacionMostrador.Visible = true;
                    checkBoxCotizacionMostrador.Enabled = false;
                    checkBoxCotizacionMostrador.Checked = false;
                }
            }
            await fillDocumentInformation(CALL_GET_DOCUMENT_DATA, 0);
            await validateCotmosPermissionProcess();
        }

        private async Task  validateUseFiscalProductField()
        {
            bool useFical = false;
            await Task.Run(async () =>
            {
                useFical = ConfiguracionModel.useFiscalFieldValueActivated();
            });
            useFiscalField = useFical;
        }

        private async Task validateCotmosPermissionProcess()
        {
            if (cotmosActive)
            {
                String description = "";
                String nombreAgente = "";
                await Task.Run(async () =>
                {
                    int idAgente = DocumentModel.getAgentIdOfADocument(idDocument);
                    if(idAgente == 0)
                    {
                        description = "Agente: No asignado";
                    }
                    else
                    {
                        nombreAgente = UserModel.getNameUser(idAgente);
                        description = "Agente: "+nombreAgente;
                    }
                });
                txtNombreAgente.Text = description;
                checkBoxCotizacionMostrador.Enabled = false;
                FormCodigoAgente frmCod = new FormCodigoAgente(this, idDocument, nombreAgente);
                frmCod.ShowDialog();
            }
        }

        private async Task<int> getNumerOfCopiesFromTheLocalDb()
        {
            int copias = 0;
            await Task.Run(async () =>
            {
                copias = ConfiguracionModel.getNumCopias();
                if (copias == 0)
                    copias = 1;
            });
            return copias;
        }

        private async Task validateIfUserHaveSellOnCreditPermission()
        {
            dynamic responseSellOnCredit = UserModel.doYouHavePermissionToSellCredit();
            if (responseSellOnCredit.value == 1)
            {
                textCreditoFrmPayCart.Visible = true;
                checkBoxCreditoFrmPayCart.Visible = true;
            } else if (responseSellOnCredit.value == 0)
            {
                textCreditoFrmPayCart.Visible = false;
                checkBoxCreditoFrmPayCart.Visible = false;
            } else
            {
                textCreditoFrmPayCart.Visible = false;
                checkBoxCreditoFrmPayCart.Visible = false;
                FormMessage formMessage = new FormMessage("Exception", responseSellOnCredit.description, 3);
                formMessage.ShowDialog();
            }
        }

        private async Task buttonsEvents()
        {
            if (LicenseModel.isItTPVLicense())
            {
                if (!DocumentModel.isItACreditSaleDocument(idDocument))
                {
                    if (tipoDeDocumento == DocumentModel.TIPO_DEVOLUCION)
                    {
                        checkBoxInvoiceFrmPayCart.Visible = true;
                        textFacturaFrmPayCart.Visible = true;
                    } else {
                        if (await ConfigurationsTpvController.checkIfUseFiscalValueActivated())
                        {
                            int fiscalMovements = await validateIfDocumentHaveFiscalMovements();
                            if (fiscalMovements == 2 && activateOpcionFacturar == 0)
                            {
                                checkBoxCreditoFrmPayCart.Visible = false;
                                textCreditoFrmPayCart.Visible = false;
                            }
                            if (fiscalMovements > 0)
                            {
                                if (tipoDeDocumento == DocumentModel.TIPO_PREPEDIDO)
                                {

                                }
                                else if (tipoDeDocumento == DocumentModel.TIPO_COTIZACION)
                                {
                                    checkBoxInvoiceFrmPayCart.Visible = true;
                                    checkBoxInvoiceFrmPayCart.Enabled = true;
                                    checkBoxInvoiceFrmPayCart.Checked = false;
                                    checkBoxInvoiceFrmPayCart.Text = "No se facturará";
                                    textFacturaFrmPayCart.Visible = true;
                                } else
                                {
                                    DocumentModel.updateGenerarFactura(idDocument, DocumentModel.FISCAL_FACTURAR);
                                    checkBoxInvoiceFrmPayCart.Visible = true;
                                    checkBoxInvoiceFrmPayCart.Enabled = true;
                                    checkBoxInvoiceFrmPayCart.Checked = true;
                                    checkBoxInvoiceFrmPayCart.Text = "Sí se facturará";
                                    textFacturaFrmPayCart.Visible = true;
                                }
                            }
                            else
                            {
                                DocumentModel.updateGenerarFactura(idDocument, DocumentModel.NO_FISCAL_NO_FACTURAR);
                                checkBoxInvoiceFrmPayCart.Enabled = false;
                                checkBoxInvoiceFrmPayCart.Visible = true;
                                checkBoxInvoiceFrmPayCart.Checked = false;
                                checkBoxInvoiceFrmPayCart.Text = "No se facturará";
                                textFacturaFrmPayCart.Visible = true;
                            }
                        } else
                        {
                            if (DocumentModel.seGeneraraFacturaSinFiscal(idDocument))
                            {
                                DocumentModel.updateGenerarFactura(idDocument, 1);
                                checkBoxInvoiceFrmPayCart.Visible = true;
                                checkBoxInvoiceFrmPayCart.Enabled = true;
                                checkBoxInvoiceFrmPayCart.Checked = true;
                                checkBoxInvoiceFrmPayCart.Text = "Sí se facturará";
                                textFacturaFrmPayCart.Visible = true;
                            } else
                            {
                                DocumentModel.updateGenerarFactura(idDocument, 0);
                                checkBoxInvoiceFrmPayCart.Visible = true;
                                checkBoxInvoiceFrmPayCart.Enabled = true;
                                checkBoxInvoiceFrmPayCart.Checked = false;
                                checkBoxInvoiceFrmPayCart.Text = "No se facturará";
                                textFacturaFrmPayCart.Visible = true;
                            }
                        }
                    }
                } else
                {
                    
                }
            }
        }

        private async void checkBoxCreditoFrmPayCart_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxCreditoFrmPayCart.Focused)
            {
                if (changePaymentMethod)
                {
                    changePaymentMethod = false;
                    if (checkBoxCreditoFrmPayCart.Checked)
                    {
                        await callChangeSaleTypeTask(ChangeSaleTypeController.FROM_CASH_TO_CREDIT, 0);
                    }
                    else
                    {
                        await callChangeSaleTypeTask(ChangeSaleTypeController.FROM_CREDIT_TO_CASH, 0);
                    }
                }
                changePaymentMethod = true;
            }
        }

        private async Task callChangeSaleTypeTask(int callType, int creditLimitExcedeedAccepted)
        {
            ChangeSaleTypeController cstc = new ChangeSaleTypeController();
            int response = await cstc.doInBackground(idDocument, callType);
            if (response == ChangeSaleTypeController.thereAreFormsOfPaymentAdded) {
                if (permissionPrepedido)
                {

                } else
                {
                    FrmConfirmation fc = new FrmConfirmation("Confirmación", "Para cambiar el tipo de venta vamos a eliminar los abonos realizados anteriormente, " +
                        "¿estas seguro de efectuar el cambio?");
                    fc.ShowDialog();
                    if (FrmConfirmation.confirmation)
                    {
                        callDeleteWaysToCollectFromADocumentTask(0);
                    }
                    else
                    {
                        changePaymentMethod = false;
                        if (checkBoxCreditoFrmPayCart.Checked)
                            checkBoxCreditoFrmPayCart.Checked = false;
                        else checkBoxCreditoFrmPayCart.Checked = true;
                    }
                }
            }
            else if (response == ChangeSaleTypeController.CREDIT_LIMIT_EXCEEDED)
            {
                if (UserModel.doYouHavePermissionToLimitCredit())
                {
                    FrmConfirmation fc = new FrmConfirmation("Límite de Crédito Excedido", "Oops lo sentimos " +
                            "no puedes terminar el documento a crédito, tienes que quitar algunos productos");
                    fc.ShowDialog();
                    if (FrmConfirmation.confirmation)
                    {
                        callDeleteWaysToCollectFromADocumentTask(1);
                    }
                }
                else
                {
                    FrmConfirmation fc = new FrmConfirmation("Límite de Crédito Excedido", "El límite de crédito del cliente " +
                            "está a punto de ser excedido, pero tienes la facultad de poder terminar la venta, ¿quieres continuar?");
                    fc.ShowDialog();
                    if (FrmConfirmation.confirmation)
                    {
                        callDeleteWaysToCollectFromADocumentTask(2);
                    }
                }
            }
            else if (response == ChangeSaleTypeController.DOCUMENT_CONVERTED_TO_CREDIT)
            {
                //Toast.makeText(activity, "Venta a Crédito", Toast.LENGTH_SHORT).show();
                checkBoxInvoiceFrmPayCart.Visible = true;
                textFacturaFrmPayCart.Visible = true;
                if (await ConfigurationsTpvController.checkIfUseFiscalValueActivated())
                {
                    int fiscalMovements = await validateIfDocumentHaveFiscalMovements();
                    if (fiscalMovements == 1)
                    {
                        if (DocumentModel.updateGenerarFactura(idDocument, DocumentModel.FISCAL_FACTURAR))
                        {
                            checkBoxInvoiceFrmPayCart.Visible = true;
                            checkBoxInvoiceFrmPayCart.Checked = true;
                            checkBoxInvoiceFrmPayCart.Text = "Sí se facturará";
                            textFacturaFrmPayCart.Visible = true;
                        }
                    }
                    else if (fiscalMovements == 0)
                    {
                        if (DocumentModel.updateGenerarFactura(idDocument, DocumentModel.NO_FISCAL_NO_FACTURAR))
                        {
                            checkBoxInvoiceFrmPayCart.Visible = false;
                            checkBoxInvoiceFrmPayCart.Checked = false;
                            checkBoxInvoiceFrmPayCart.Text = "No se facturará";
                            textFacturaFrmPayCart.Visible = false;
                        }
                    }
                    else if (fiscalMovements == 2)
                    {

                    }
                } else
                {

                }
                if (LicenseModel.isItTPVLicense())
                {
                    formVenta.btnPayWithCashFrmVenta.Visible = false;
                }
                if (UserModel.doYouHavePermissionToFacturarCredito())
                {
                    checkBoxInvoiceFrmPayCart.Visible = false;
                    checkBoxInvoiceFrmPayCart.Checked = false;
                    checkBoxInvoiceFrmPayCart.Text = "No se facturará";
                    textFacturaFrmPayCart.Visible = false;
                } else
                {
                    checkBoxInvoiceFrmPayCart.Visible = true;
                    checkBoxInvoiceFrmPayCart.Checked = false;
                    checkBoxInvoiceFrmPayCart.Text = "No se facturará";
                    textFacturaFrmPayCart.Visible = true;
                }
                if (activateOpcionFacturar == 0) {
                    checkBoxInvoiceFrmPayCart.Enabled = false;
                    //checkBoxCreditoFrmPayCart.Visible = false;
                    //textCreditoFrmPayCart.Visible = false;
                }
                await fillDocumentInformation(CALL_GET_DOCUMENT_DATA, 0);
            } else if (response == ChangeSaleTypeController.DOCUMENT_CONVERTED_TO_CASH) {
                if (LicenseModel.isItTPVLicense())
                {
                    checkBoxInvoiceFrmPayCart.Visible = false;
                    textFacturaFrmPayCart.Visible = false;
                    if (await ConfigurationsTpvController.checkIfUseFiscalValueActivated())
                    {
                        int fiscalMovements = await validateIfDocumentHaveFiscalMovements();
                        if (fiscalMovements == 0 && activateOpcionFacturar == 1)
                        {
                            if (activateOpcionFacturar == 1)
                            {
                                if (DocumentModel.updateGenerarFactura(idDocument, DocumentModel.NO_FISCAL_NO_FACTURAR))
                                {
                                    checkBoxInvoiceFrmPayCart.Checked = false;
                                    checkBoxInvoiceFrmPayCart.Text = "No se facturará";
                                    checkBoxInvoiceFrmPayCart.Visible = false;
                                    textFacturaFrmPayCart.Visible = false;
                                }
                            } else if (activateOpcionFacturar == 0)
                            {
                                if (DocumentModel.updateGenerarFactura(idDocument, DocumentModel.FISCAL_FACTURAR))
                                {
                                    DocumentModel.updateQuoteDocumentType(idDocument, DocumentModel.TIPO_VENTA);
                                    checkBoxInvoiceFrmPayCart.Checked = true;
                                    checkBoxInvoiceFrmPayCart.Text = "Sí se facturará";
                                    checkBoxInvoiceFrmPayCart.Visible = true;
                                    textFacturaFrmPayCart.Visible = true;
                                }
                            }
                        }
                        else if (fiscalMovements == 1)
                        {
                            if (activateOpcionFacturar == 1)
                            {
                                if (DocumentModel.updateGenerarFactura(idDocument, DocumentModel.FISCAL_FACTURAR))
                                {
                                    checkBoxInvoiceFrmPayCart.Checked = true;
                                    checkBoxInvoiceFrmPayCart.Text = "Sí se facturará";
                                    checkBoxInvoiceFrmPayCart.Visible = true;
                                    textFacturaFrmPayCart.Visible = true;
                                }
                            } else if (activateOpcionFacturar == 0)
                            {
                                if (DocumentModel.updateGenerarFactura(idDocument, DocumentModel.FISCAL_FACTURAR))
                                {
                                    DocumentModel.updateQuoteDocumentType(idDocument, DocumentModel.TIPO_VENTA);
                                    checkBoxInvoiceFrmPayCart.Checked = true;
                                    checkBoxInvoiceFrmPayCart.Text = "Sí se facturará";
                                    checkBoxInvoiceFrmPayCart.Visible = true;
                                    textFacturaFrmPayCart.Visible = true;

                                }
                            }
                        }
                        else if (fiscalMovements == 2)
                        {

                        }
                    } else
                    {
                        if (DocumentModel.seGeneraraFacturaSinFiscal(idDocument))
                        {
                            DocumentModel.updateGenerarFactura(idDocument, 1);
                            checkBoxInvoiceFrmPayCart.Checked = true;
                            checkBoxInvoiceFrmPayCart.Enabled = true;
                            checkBoxInvoiceFrmPayCart.Text = "Sí se facturará";
                            checkBoxInvoiceFrmPayCart.Visible = true;
                            textFacturaFrmPayCart.Visible = true;
                        } else
                        {
                            DocumentModel.updateGenerarFactura(idDocument, 0);
                            checkBoxInvoiceFrmPayCart.Checked = false;
                            checkBoxInvoiceFrmPayCart.Enabled = true;
                            checkBoxInvoiceFrmPayCart.Text = "No se facturará";
                            checkBoxInvoiceFrmPayCart.Visible = true;
                            textFacturaFrmPayCart.Visible = true;
                        }
                    }
                    if (tipoDeDocumento == DocumentModel.TIPO_DEVOLUCION)
                    {
                        checkBoxInvoiceFrmPayCart.Visible = true;
                        textFacturaFrmPayCart.Visible = true;
                    }
                    formVenta.btnPayWithCashFrmVenta.Visible = true;
                }
                if (activateOpcionFacturar == 0) {
                    checkBoxInvoiceFrmPayCart.Enabled = false;
                    //checkBoxCreditoFrmPayCart.Visible = false;
                    //textCreditoFrmPayCart.Visible = false;
                }
                await fillDocumentInformation(CALL_GET_DOCUMENT_DATA, 0);
            }
        }

        private async Task<int> validateIfDocumentHaveFiscalMovements()
        {
            int fiscalProducts = 1;
            await Task.Run(async () => {
                if (tipoDeDocumento == DocumentModel.TIPO_VENTA || tipoDeDocumento ==  DocumentModel.TIPO_REMISION ||
                tipoDeDocumento == DocumentModel.TIPO_COTIZACION) {
                    //buscar del documento todos los movimientos fiscales
                    //buscar del documento todos los movimientos no fiscales
                    //realizar la comparacion
                    int countF = 0;
                    int counsNF = 0;
                    String fiscalField = await ItemModel.getTableNameForFiscalItemField(positionFiscalItemField);
                    counsNF = MovimientosModel.getTotalNumberOfMovimientosFiscalesDiferentesAlQueIntentamosAgregar(idDocument, 0,fiscalField,serverModeLAN);
                    countF = MovimientosModel.getTotalNumberOfMovimientosFiscalesDiferentesAlQueIntentamosAgregar(idDocument, 1, fiscalField,serverModeLAN);
                    if (countF > 0 && counsNF == 0)
                    {
                        fiscalProducts = 1;
                    }
                    else if (countF == 0 && counsNF > 0)
                    {
                        fiscalProducts = 0;
                    }
                    else
                    {
                        fiscalProducts = 2;
                    }
                }
            });
            return fiscalProducts;
        }

        private async Task callDeleteWaysToCollectFromADocumentTask(int creditLimitExceededAccepted)
        {
            DeleteWaysToCollectFromADocumentController dwcc = new DeleteWaysToCollectFromADocumentController(creditLimitExceededAccepted);
            int response = await dwcc.doInBackground(idDocument);
            if (response == 1)
            {
                if (LicenseModel.isItROMLicense())
                {
                    DocumentModel.changeDocumentFromCashToCreditOrViceVersa(idDocument, 0, 71, 0);
                }
                else if (LicenseModel.isItTPVLicense())
                {
                    DocumentModel.changeDocumentFromCashToCreditOrViceVersa(idDocument, 2, 71, 0);
                }
                await fillDocumentInformation(CALL_GET_DOCUMENT_DATA, 0);
            }
        }

        public async Task fillDocumentInformation(int call, int printTicket)
        {
            if (call == CALL_GET_DOCUMENT_DATA)
            {
                int license = FormVenta.licenciaActivadaVigente;
                CarritoRomTaskController cc = new CarritoRomTaskController(license);
                dynamic response = await cc.doInBackground(idDocument, call, printTicket, serverModeLAN);
                DocumentModel dvm = response.dvm;
                List<FormasDeCobroModel> fcList = response.fcList;
                double descuento = response.discountDocument;
                double pendiente = response.pending;
                double cambio = response.change;
                bool bill = response.billPermission;
                validateResponseOne(dvm, descuento, pendiente, cambio, fcList, bill);
            }
            else if (call == CALL_UPDATE_OBSERVATION_DOCUMENT_DATA)
            {
                String obs = await DocumentController.updateObervationInCurrentDocument(idDocument, call, documentObservation);
                if (!obs.Equals(""))
                    btnObservacionesFrmPayCart.Text = obs;
                else btnObservacionesFrmPayCart.Text = "Observación o Comentario";
            }
            else if (call == CALL_END_DOCUMENT)
            {
                int license = FormVenta.licenciaActivadaVigente;
                CarritoRomTaskController cc = new CarritoRomTaskController(license);
                dynamic response = await cc.doInBackground(idDocument, call, printTicket, serverModeLAN);
                validateResponseEndDocument(response);
            }
        }

        private void validateResponseOne(DocumentModel dvm, double discountDocument, double pending, double change, List<FormasDeCobroModel> fcList, Boolean billPermission) {
            if (dvm != null)
            {
                documentObservation = dvm.observacion;
                tipoDeDocumento = dvm.tipo_documento;
                if (tipoDeDocumento == DocumentModel.TIPO_DEVOLUCION)
                {
                    this.Text = "Pagar Devolución";
                    panelVentaACredito.Height = 0;
                    textCreditoFrmPayCart.Visible = false;
                    checkBoxCreditoFrmPayCart.Visible = false;
                    if (billPermission || dvm.factura == DocumentModel.FISCAL_FACTURAR) {
                        textFacturaFrmPayCart.Visible = true;
                        checkBoxInvoiceFrmPayCart.Visible = true;
                        if (dvm.factura == 1)
                        {
                            checkBoxInvoiceFrmPayCart.Checked = true;
                            checkBoxInvoiceFrmPayCart.Text = "Sí se facturará";
                        }
                        else
                        {
                            checkBoxInvoiceFrmPayCart.Checked = false;
                            checkBoxInvoiceFrmPayCart.Text = "No se facturará";
                        }
                    } else {
                        textFacturaFrmPayCart.Visible = false;
                        checkBoxInvoiceFrmPayCart.Visible = false;
                        checkBoxInvoiceFrmPayCart.Text = "No se facturará";
                    }
                } 
                else if (tipoDeDocumento == DocumentModel.TIPO_VENTA || tipoDeDocumento == DocumentModel.TIPO_REMISION) {
                    if (dvm.forma_cobro_id == DocumentModel.FORMA_PAGO_CREDITO)
                    {
                        dynamic responseSellCredit = UserModel.doYouHavePermissionToSellCredit();
                        if (responseSellCredit.value == 1)
                            checkBoxCreditoFrmPayCart.Checked = true;
                        else if (responseSellCredit.value == 0)
                            checkBoxCreditoFrmPayCart.Checked = false;
                        else if (responseSellCredit.value == 0)
                        {
                            checkBoxCreditoFrmPayCart.Checked = false;
                            FormMessage formMessage = new FormMessage("Exception", responseSellCredit.description, 3);
                            formMessage.ShowDialog();
                        }
                    }
                    if (checkBoxCreditoFrmPayCart.Checked) {
                        this.Text = "Abonar Venta a Crédito";
                    } else {
                        this.Text = "Cobrar Venta al Contado";
                    }
                    if (billPermission && dvm.forma_cobro_id != DocumentModel.FORMA_PAGO_CREDITO) {
                        textFacturaFrmPayCart.Visible = true;
                        checkBoxInvoiceFrmPayCart.Visible = true;
                        if (dvm.factura == 1 || dvm.factura == DocumentModel.FISCAL_FACTURAR) {
                            if (checkBoxCreditoFrmPayCart.Checked) {
                                checkBoxInvoiceFrmPayCart.Checked = true;
                                checkBoxInvoiceFrmPayCart.Text = "Sí se facturará";
                            } else {
                                checkBoxInvoiceFrmPayCart.Checked = true;
                                checkBoxInvoiceFrmPayCart.Text = "Sí se facturará";
                            }
                        }
                        else if (dvm.factura == DocumentModel.FISCAL_NO_FACTURAR)
                        {
                            if (checkBoxCreditoFrmPayCart.Checked)
                            {
                                checkBoxInvoiceFrmPayCart.Checked = true;
                                checkBoxInvoiceFrmPayCart.Text = "Sí se facturará";
                            } else {
                                checkBoxInvoiceFrmPayCart.Checked = false;
                                checkBoxInvoiceFrmPayCart.Text = "No se facturará";
                            }
                        } else if (dvm.factura == DocumentModel.NO_FISCAL_NO_FACTURAR) {
                            checkBoxInvoiceFrmPayCart.Checked = false;
                            checkBoxInvoiceFrmPayCart.Text = "No se facturará";
                        } else if (dvm.factura == 0) {
                            checkBoxInvoiceFrmPayCart.Checked = false;
                            checkBoxInvoiceFrmPayCart.Text = "No se facturará";
                        }
                    } else {
                        textFacturaFrmPayCart.Visible = false;
                        checkBoxInvoiceFrmPayCart.Visible = false;
                        checkBoxInvoiceFrmPayCart.Text = "No se facturará";
                    }
                    if (dvm.forma_cobro_id == DocumentModel.FORMA_PAGO_CREDITO) {
                        String query = "SELECT " + LocalDatabase.CAMPO_PERMFACTURARACREDITO_USUARIO + " FROM " + LocalDatabase.TABLA_USUARIO + " WHERE " +
                            LocalDatabase.CAMPO_ID_USUARIO + " = " + ClsRegeditController.getIdUserInTurn();
                        int permisoFacturarCredito = UserModel.getIntValue(query);
                        if (permisoFacturarCredito == 0) {
                            checkBoxInvoiceFrmPayCart.Checked = false;
                            checkBoxInvoiceFrmPayCart.Text = "No se facturará";
                            textFacturaFrmPayCart.Visible = false;
                        }
                    }
                    if (activateOpcionFacturar == 0)
                    {
                        checkBoxInvoiceFrmPayCart.Enabled = false;
                        //checkBoxInvoiceFrmPayCart.Checked = false;
                        //textFacturaFrmPayCart.Visible = false;
                    }
                }
                else if (tipoDeDocumento == DocumentModel.TIPO_COTIZACION)
                {
                    this.Text = "Terminar Cotización";
                    panelVentaACredito.Height = 0;
                    panelFirstSectionFrmPayCart.Height = 0;
                    panelGenerarFactura.Visible = true;
                    textInfoFcFrmPayCart.Height = 0;
                    panelDvFcFrmPayCart.Height = 0;
                    this.Height = this.Height - 220;
                    checkBoxCreditoFrmPayCart.Visible = false;
                    textCreditoFrmPayCart.Visible = false;
                    //textInfoFormaDeCobro.setVisibility(View.GONE);
                    dataGridViewFcFrmPayCArt.Visible = false;
                    textPendienteFrmPayCart.Visible = false;
                    textInfoPendienteFrmPayCart.Visible = false;
                    textCambioFrmPayCart.Visible = false;
                    textInfoCambioFrmPayCart.Visible = false;
                    checkBoxInvoiceFrmPayCart.Visible = true;
                    textFacturaFrmPayCart.Visible = true;
                    checkBoxCotizacionMostrador.Visible = true;
                    checkBoxCotizacionMostrador.Enabled = true;
                    btnObservacionesFrmPayCart.Visible = true;
                    this.MaximizeBox = false;
                    this.MinimizeBox = false;
                    this.FormBorderStyle = FormBorderStyle.FixedDialog;
                }
                else if (tipoDeDocumento == DocumentModel.TIPO_PREPEDIDO)
                {
                    this.Text = "Terminar Pedido";
                    panelDvFcFrmPayCart.Height = 0;
                    this.Height = this.Height - 50;
                    checkBoxCreditoFrmPayCart.Visible = false;
                    textCreditoFrmPayCart.Visible = false;
                    dataGridViewFcFrmPayCArt.Visible = false;
                    textPendienteFrmPayCart.Visible = false;
                    textInfoPendienteFrmPayCart.Visible = false;
                    textCambioFrmPayCart.Visible = false;
                    textInfoCambioFrmPayCart.Visible = false;
                    checkBoxInvoiceFrmPayCart.Visible = false;
                    textFacturaFrmPayCart.Visible = false;
                    this.MaximizeBox = false;
                    this.MinimizeBox = false;
                    this.FormBorderStyle = FormBorderStyle.FixedDialog;
                }
                else
                {
                    this.Text = "Terminar Pedido";
                    panelDvFcFrmPayCart.Height = 0;
                    this.Height = this.Height - 265;
                    checkBoxCreditoFrmPayCart.Visible = false;
                    textCreditoFrmPayCart.Visible = false;
                    //textInfoFormaDeCobro.setVisibility(View.GONE);
                    dataGridViewFcFrmPayCArt.Visible = false;
                    textPendienteFrmPayCart.Visible = false;
                    textInfoPendienteFrmPayCart.Visible = false;
                    textCambioFrmPayCart.Visible = false;
                    textInfoCambioFrmPayCart.Visible = false;
                    checkBoxInvoiceFrmPayCart.Visible = false;
                    textFacturaFrmPayCart.Visible = false;
                    this.MaximizeBox = false;
                    this.MinimizeBox = false;
                    this.FormBorderStyle = FormBorderStyle.FixedDialog;
                }
                if (fcList != null) {
                    fillDataGridFormasDeCobro(fcList);
                }
                if (documentObservation.Equals(""))
                    btnObservacionesFrmPayCart.Text = "Observación o Comentario";
                else btnObservacionesFrmPayCart.Text = documentObservation;
                textSubtotalFrmPayCart.Text = (dvm.total + dvm.descuento).ToString("C", CultureInfo.CurrentCulture) + " MXN";
                textDescuentoFrmPayCart.Text = discountDocument.ToString("C", CultureInfo.CurrentCulture) + " MXN";
                textTotalFrmPayCart.Text = dvm.total.ToString("C", CultureInfo.CurrentCulture) + " MXN";
                textPendienteFrmPayCart.Text = pending.ToString("C", CultureInfo.CurrentCulture) + " MXN";
                textCambioFrmPayCart.Text = change.ToString("C", CultureInfo.CurrentCulture) + " MXN";
                if (permissionPrepedido)
                {
                    if (DocumentModel.isItDocumentFromAPrepedido(idDocument))
                    {
                        String query = "SELECT * FROM " + LocalDatabase.TABLA_MOVIMIENTO + " WHERE " + LocalDatabase.CAMPO_DOCUMENTOID_MOV + " = " +
                    idDocument;
                        List<MovimientosModel> movesList = MovimientosModel.getAllMovements(query);
                        if (movesList != null)
                        {
                            query = "SELECT * FROM " + LocalDatabase.TABLA_PESO + " WHERE " + LocalDatabase.CAMPO_MOVIMIENTOID_PESO + " = " + movesList[0].id;
                            WeightModel wm = WeightModel.getAWeight(query);
                            if (wm != null)
                            {
                                String unitNoConvertibleName = UnitsOfMeasureAndWeightModel.getNameOfAndUnitMeasureWeight(movesList[0].nonConvertibleUnitId);
                                String unitConvertibleName = UnitsOfMeasureAndWeightModel.getNameOfAndUnitMeasureWeight(movesList[0].capturedUnitId);
                                textInfoTotalFrmPayCart.Text = "Total: " + MetodosGenerales.obtieneDosDecimales(movesList[0].nonConvertibleUnits) + " " +
                                    unitNoConvertibleName + " Equivalente a " + wm.pesoNeto + " " + unitConvertibleName + "\n En " + wm.cajas + " Cajas";
                                btnAceptarFrmPayCart.Enabled = true;
                                textCambioFrmPayCart.Text = "Total: " + dvm.total.ToString("C", CultureInfo.CurrentCulture) + " MXN";
                            }
                            else
                            {
                                checkBoxCotizacionMostrador.Checked = false;
                                textInfoTotalFrmPayCart.Text = "Pesos en Kg No Registrados";
                                btnAceptarFrmPayCart.Enabled = false;
                            }
                        }
                        textTotalFrmPayCart.Visible = false;
                    } else
                    {
                        checkBoxCotizacionMostrador.Checked = false;
                        textInfoTotalFrmPayCart.Text = "Generando Prepedido";
                        btnAceptarFrmPayCart.Enabled = true;
                        textCambioFrmPayCart.Text = "";
                        textTotalFrmPayCart.Visible = false;
                    }
                }
            } else {
                FormMessage fm = new FormMessage("Faltan Datos", "No hay documento", 2);
                fm.ShowDialog();
            }
        }

        private async Task validateResponseEndDocument(dynamic resp)
        {
            DocumentModel.updateToPauseTheDocument(resp.idDocument, 0);
            int idPedCot = DocumentModel.getIntValue("SELECT " + LocalDatabase.CAMPO_CIDDOCTOPEDIDOCC_DOC + " FROM " + LocalDatabase.TABLA_DOCUMENTOVENTA + " WHERE " +
                LocalDatabase.CAMPO_ID_DOC + " = " + resp.idDocument);
            if (idPedCot != 0)
            {
                PedidosEncabezadoModel.marcarPedidoComoListoONo(idPedCot, 1);
            }
            if (resp.response == 1)
            {
                int response = await SendAdditionalCustomerController.enviarClientesAdicionales(resp.idCustomer);
                if (response == 1)
                {
                    validateAdditionalCustomer(resp);
                } else
                {
                    FormMessage formMessage = new FormMessage("Error","No pudimos subir al nuevo cliente", 2);
                    formMessage.ShowDialog();
                }
            }
            else if (resp.response == 2)
            {
                validateAdditionalCustomer(resp);
            }
            else if (resp.response == 3)
            {
                cerrarActivitiesFinalizarEsteYVerificarADondeIr(resp.idDocument, 0, 0, resp.printTicket);
            }
        }

        private async Task validateAdditionalCustomer(dynamic resp)
        {
            List<int> idDoclist = new List<int>();
            idDoclist.Add(resp.idDocument);
            String idDocumento = resp.idDocument + "-" + 0;
            enviarDocDirectamenteAlWs(idDocumento, 1, 0, 0, idDoclist);
        }

        private async Task enviarDocDirectamenteAlWs(String idDocumento, int method, int envioDeDatos, int peticiones, 
            List<int> idDoclist)
        {
            Cursor.Current = Cursors.WaitCursor;
            btnAceptarFrmPayCart.Enabled = false;
            btnCancelFrmPayCart.Enabled = false;
            SendSomeDocsJSONService ssdj = new SendSomeDocsJSONService();
            dynamic response = await ssdj.startActionSendDocument(idDocumento, method, envioDeDatos, peticiones, idDoclist,
                this,
                permissionPrepedido);
            if (response != null)
            {
                int error = response.error;
                int valor = response.value;
                String description = response.description;
                int methodr = response.method;
                //int envioDedatos = response.envioDedatos;
                String idDocument = response.idDocumento;
                List<int> idsDocsList = response.idsDocsList;
                peticiones = response.peticiones;
                validateSendDocumentsResponse(error, valor, description, methodr, idDocument, idsDocsList, peticiones);
            }
            Cursor.Current = Cursors.Default;
        }

        private void validateSendDocumentsResponse(int error, int valor, String description, int method, 
            String idDocumento, List<int> idsDocList, int peticiones)
        {
            if (formWaiting != null)
            {
                formWaiting.Dispose();
                formWaiting.Close();
            }
            if (error != 1)
            {
                dynamic sumsMap = FormVenta.getCurrentSumsFromDocument(idDocument);
                FrmConfirmSale fcs = new FrmConfirmSale(sumsMap.total, FormasDeCobroDocumentoModel.getCambioOfTheDcoument(idDocument));
                fcs.StartPosition = FormStartPosition.CenterScreen;
                fcs.ShowDialog();
                formVenta.resetearTodosLosValores(true);
                idDocument = 0;
                FormVenta.idDocument = idDocument;
                if (FormVenta.pausedFragment == 1)
                {
                    this.Close();
                } else
                {
                    this.Close();
                }
            }
            else
            {
                if (method < 3) {
                    enviarDocDirectamenteAlWs(idDocumento, method, 0, peticiones, idsDocList);
                }
                if (valor >= 100 || method == 3)
                {
                    dynamic sumsMap = FormVenta.getCurrentSumsFromDocument(idDocument);
                    FrmConfirmSale fcs = new FrmConfirmSale(sumsMap.total, 
                        FormasDeCobroDocumentoModel.getCambioOfTheDcoument(idDocument));
                    fcs.StartPosition = FormStartPosition.CenterScreen;
                    fcs.ShowDialog();
                    formVenta.resetearTodosLosValores(true);
                    idDocument = 0;
                    FormVenta.idDocument = idDocument;
                    if (FormVenta.pausedFragment == 1) {
                        this.Close();
                    } else
                    { 
                        this.Close();
                    }
                }
            }
        }

        private async Task cerrarActivitiesFinalizarEsteYVerificarADondeIr(int idDocument, int idPedido, int idPosition, int printTicket)
        {
            if (printTicket == 0) {
                this.Close();
            } else {
                String originalCopia = "";
                String numeroCopiasText = editNumeroCopias.Text.Trim();
                if (numeroCopiasText.Equals("") || numeroCopiasText.Equals("0"))
                {
                    if (permissionPrepedido)
                    {
                        if (DocumentModel.isItDocumentFromAPrepedido(idDocument))
                        {
                            String textoOriginal = PrinterModel.getTextoOriginal();
                            clsTicket ct = new clsTicket();
                            await ct.CrearTicket(idDocument, true, permissionPrepedido, tipoDeDocumento, textoOriginal, serverModeLAN);
                        }
                    } else
                    {
                        String textoOriginal = PrinterModel.getTextoOriginal();
                        clsTicket ct = new clsTicket();
                        await ct.CrearTicket(idDocument, true, permissionPrepedido, tipoDeDocumento, textoOriginal, serverModeLAN);
                    }
                } else
                {
                    String textoOriginal = PrinterModel.getTextoOriginal();
                    String textoCopia = PrinterModel.getTextoCopia();
                    int numeroCopias = Convert.ToInt32(numeroCopiasText);
                    if (numeroCopias >= 1 && numeroCopias <= 10)
                    {
                        for (int i = 0; i < numeroCopias; i++)
                        {
                            if (i == 0)
                            {
                                clsTicket ct = new clsTicket();
                                await ct.CrearTicket(idDocument, true, permissionPrepedido, tipoDeDocumento, textoOriginal, serverModeLAN);
                            } else
                            {
                                clsTicket ct = new clsTicket();
                                await ct.CrearTicket(idDocument, true, permissionPrepedido, tipoDeDocumento, textoCopia, serverModeLAN);
                            }
                        }
                    }
                }
            }
        }

        private void btnCancelFrmPayCart_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAceptarFrmPayCart_Click(object sender, EventArgs e)
        {
            formWaiting = new FormWaiting(this, 0); //callTerminateDocumentTask
            formWaiting.ShowDialog();
        }
 
        public async Task callTerminateDocumentTask()
        {
            if (permissionPrepedido)
            {
                if (DocumentModel.isItDocumentFromAPrepedido(idDocument))
                {
                    String query = "SELECT COUNT(*) FROM Documentos D " +
                    "INNER JOIN Movimientos M On D.id = M.DOCTO_ID_PEDIDO " +
                    "INNER JOIN Weight W ON M.id = W.movementId " +
                    "WHERE D.id = " + idDocument + " AND W.gross_weight != 0";
                    int documentoConPeso = DocumentModel.getIntValue(query);
                    if (documentoConPeso > 0)
                    {
                        if (!pesoBrutoCapturado)
                        {
                            if (formWaiting != null) {
                                formWaiting.Dispose();
                                formWaiting.Close();
                            }
                            FormMessage formMessage = new FormMessage("Peso Faltante", "Para enviar el pedido mínimo necesitas el peso bruto", 2);
                            formMessage.ShowDialog();
                        }
                        else
                        {
                            bool cotizacionMostrador = false;
                            if (checkBoxCotizacionMostrador.Enabled && checkBoxCotizacionMostrador.Checked &&
                                (tipoDeDocumento == DocumentModel.TIPO_COTIZACION || tipoDeDocumento == DocumentModel.TIPO_COTIZACION_MOSTRADOR))
                                cotizacionMostrador = true;
                            else if (!checkBoxCotizacionMostrador.Enabled &&
                                (tipoDeDocumento != DocumentModel.TIPO_COTIZACION || tipoDeDocumento != DocumentModel.TIPO_COTIZACION_MOSTRADOR))
                                cotizacionMostrador = true;
                            else if (checkBoxCotizacionMostrador.Checked &&
                                (tipoDeDocumento != DocumentModel.TIPO_COTIZACION || tipoDeDocumento != DocumentModel.TIPO_COTIZACION_MOSTRADOR)
                                && permissionPrepedido)
                                cotizacionMostrador = true;
                            TerminateDocumentController tdc = new TerminateDocumentController();
                            dynamic response = await tdc.doInBackground(TerminateDocumentController.TERMINATE_DOCUMENT_ROM, 
                                idDocument, cotizacionMostrador, serverModeLAN, positionFiscalItemField);
                            int resp = response.valor;
                            int idCustomer = response.idCustomer;
                            validateResponseTerminateDocument(resp, idCustomer, cotizacionMostrador);
                        }
                    }
                    else
                    {
                        if (formWaiting != null)
                        {
                            formWaiting.Dispose();
                            formWaiting.Close();
                        }
                        FormMessage formMessage = new FormMessage("Peso Faltante", "Para enviar el pedido mínimo necesitas el peso bruto", 2);
                        formMessage.ShowDialog();
                    }
                } else
                {
                    TerminateDocumentController tdc = new TerminateDocumentController();
                    dynamic response = await tdc.doInBackground(TerminateDocumentController.TERMINATE_DOCUMENT_ROM, 
                        idDocument, false, serverModeLAN, positionFiscalItemField);
                    int resp = response.valor;
                    int idCustomer = response.idCustomer;
                    validateResponseTerminateDocument(resp, idCustomer, false);
                }
            } 
            else
            {
                if (cotmosActive)
                {
                    int idAgenteDocumento = DocumentModel.getAgentIdOfADocument(idDocument);
                    if (idAgenteDocumento != 0)
                    {
                        bool cotizacionMostrador = false;
                        if (checkBoxCotizacionMostrador.Enabled && checkBoxCotizacionMostrador.Checked &&
                            (tipoDeDocumento == DocumentModel.TIPO_COTIZACION || tipoDeDocumento == DocumentModel.TIPO_COTIZACION_MOSTRADOR))
                            cotizacionMostrador = true;
                        else if (!checkBoxCotizacionMostrador.Enabled &&
                            (tipoDeDocumento != DocumentModel.TIPO_COTIZACION || tipoDeDocumento != DocumentModel.TIPO_COTIZACION_MOSTRADOR))
                            cotizacionMostrador = true;
                        else if (checkBoxCotizacionMostrador.Checked &&
                            (tipoDeDocumento != DocumentModel.TIPO_COTIZACION || tipoDeDocumento != DocumentModel.TIPO_COTIZACION_MOSTRADOR)
                            && permissionPrepedido)
                            cotizacionMostrador = true;
                        TerminateDocumentController tdc = new TerminateDocumentController();
                        dynamic response = await tdc.doInBackground(TerminateDocumentController.TERMINATE_DOCUMENT_ROM, idDocument,
                            cotizacionMostrador, serverModeLAN, positionFiscalItemField);
                        int resp = response.valor;
                        int idCustomer = response.idCustomer;
                        validateResponseTerminateDocument(resp, idCustomer, cotizacionMostrador);
                    }
                    else
                    {
                        if (formWaiting != null)
                        {
                            formWaiting.Dispose();
                            formWaiting.Close();
                        }
                        FormMessage formMessage = new FormMessage("Terminar Documento", "Antes de terminar el documento asegurate de ingresar el código del agente!",
                            3);
                        formMessage.ShowDialog();
                    }
                } else
                {
                    bool cotizacionMostrador = false;
                    if (checkBoxCotizacionMostrador.Enabled && checkBoxCotizacionMostrador.Checked &&
                        (tipoDeDocumento == DocumentModel.TIPO_COTIZACION || tipoDeDocumento == DocumentModel.TIPO_COTIZACION_MOSTRADOR))
                        cotizacionMostrador = true;
                    else if (!checkBoxCotizacionMostrador.Enabled &&
                        (tipoDeDocumento != DocumentModel.TIPO_COTIZACION || tipoDeDocumento != DocumentModel.TIPO_COTIZACION_MOSTRADOR))
                        cotizacionMostrador = true;
                    else if (checkBoxCotizacionMostrador.Checked &&
                        (tipoDeDocumento != DocumentModel.TIPO_COTIZACION || tipoDeDocumento != DocumentModel.TIPO_COTIZACION_MOSTRADOR)
                        && permissionPrepedido)
                        cotizacionMostrador = true;
                    TerminateDocumentController tdc = new TerminateDocumentController();
                    dynamic response = await tdc.doInBackground(TerminateDocumentController.TERMINATE_DOCUMENT_ROM, idDocument,
                        cotizacionMostrador, serverModeLAN, positionFiscalItemField);
                    int resp = response.valor;
                    int idCustomer = response.idCustomer;
                    validateResponseTerminateDocument(resp, idCustomer, cotizacionMostrador);
                }
            }
        }

        private async void validateResponseTerminateDocument(int response, int idCustomer, bool cotizacionMostrador)
        {
            if (response == 0) {
                if (formWaiting != null)
                {
                    formWaiting.Dispose();
                    formWaiting.Close();
                }
                FormMessage fm = new FormMessage("Importe Invalido", "No se puede liquidar un documento a crédito!", 2);
                fm.ShowDialog();
            } else if (response == 2) {
                if (formWaiting != null)
                {
                    formWaiting.Dispose();
                    formWaiting.Close();
                }
                FormMessage fm = new FormMessage("Documento No Cobrado", "Falta cobrar o pagar el total del documento...", 2);
                fm.ShowDialog();
            } else if (response == 1) {
                if (LicenseModel.isItROMLicense()) {
                    if (formWaiting != null)
                    {
                        formWaiting.Dispose();
                        formWaiting.Close();
                    }
                } else if (LicenseModel.isItTPVLicense()) {
                    if (ConfiguracionModel.printPermissionIsActivated()) {
                        String textoOriginal = PrinterModel.getTextoOriginal();
                        String textoCopia = PrinterModel.getTextoCopia();
                        if (permissionPrepedido)
                        {
                            bool enviado = DocumentModel.isItDocumentPrepedidoSendedToTheCustomer(idDocument);
                            if (!enviado)
                            {
                                String numeroCopiasText = editNumeroCopias.Text.Trim();
                                if (numeroCopiasText.Equals("") || numeroCopiasText.Equals("0"))
                                {
                                    if (permissionPrepedido)
                                    {
                                        if (DocumentModel.isItDocumentFromAPrepedido(idDocument))
                                        {
                                            clsTicket ct = new clsTicket();
                                            await ct.CrearTicket(idDocument, true, permissionPrepedido, tipoDeDocumento, textoOriginal, serverModeLAN);
                                        }
                                    } else
                                    {
                                        clsTicket ct = new clsTicket();
                                        await ct.CrearTicket(idDocument, true, permissionPrepedido, tipoDeDocumento, textoOriginal, serverModeLAN);
                                    }
                                }
                                else
                                {
                                    int numeroCopias = Convert.ToInt32(numeroCopiasText);
                                    if (numeroCopias >= 1 && numeroCopias <= 10)
                                    {
                                        for (int i = 0; i < numeroCopias; i++)
                                        {
                                            if (i == 0)
                                            {
                                                clsTicket ct = new clsTicket();
                                                await ct.CrearTicket(idDocument, true, permissionPrepedido, tipoDeDocumento, textoOriginal, 
                                                    serverModeLAN);
                                            } else
                                            {
                                                clsTicket ct = new clsTicket();
                                                await ct.CrearTicket(idDocument, true, permissionPrepedido, tipoDeDocumento, textoCopia, 
                                                    serverModeLAN);
                                            }
                                        }
                                    }
                                }
                            }
                        } else
                        {
                            String numeroCopiasText = editNumeroCopias.Text.Trim();
                            if (numeroCopiasText.Equals("") || numeroCopiasText.Equals("0"))
                            {
                                if (permissionPrepedido)
                                {
                                    if (DocumentModel.isItDocumentFromAPrepedido(idDocument))
                                    {
                                        clsTicket ct = new clsTicket();
                                        await ct.CrearTicket(idDocument, true, permissionPrepedido, tipoDeDocumento, textoOriginal, serverModeLAN);
                                    }
                                } else
                                {
                                    clsTicket ct = new clsTicket();
                                    await ct.CrearTicket(idDocument, true, permissionPrepedido, tipoDeDocumento, textoOriginal, serverModeLAN);
                                }
                            }
                            else
                            {
                                int numeroCopias = Convert.ToInt32(numeroCopiasText);
                                if (numeroCopias >= 1 && numeroCopias <= 10)
                                {
                                    for (int i = 0; i < numeroCopias; i++)
                                    {
                                        if (i == 0)
                                        {
                                            clsTicket ct = new clsTicket();
                                            await ct.CrearTicket(idDocument, true, permissionPrepedido, tipoDeDocumento, textoOriginal, 
                                                serverModeLAN);
                                        } else
                                        {
                                            clsTicket ct = new clsTicket();
                                            await ct.CrearTicket(idDocument, true, permissionPrepedido, tipoDeDocumento, textoCopia, 
                                                serverModeLAN);
                                        }
                                    }
                                }
                            }
                        }
                    }                     
                    if (!cotizacionMostrador && (tipoDeDocumento == DocumentModel.TIPO_VENTA || 
                        tipoDeDocumento == DocumentModel.TIPO_REMISION))
                    {
                        int idPedido = DocumentModel.getCiddoctopedidoFromADocument(idDocument);
                        if (idPedido != 0) {
                            PedidosEncabezadoModel.marcarPedidoComoListoONo(idPedido, 1);
                        }
                        dynamic sumsMap = FormVenta.getCurrentSumsFromDocument(idDocument);
                        FrmConfirmSale fcs = new FrmConfirmSale(sumsMap.total, 
                            FormasDeCobroDocumentoModel.getCambioOfTheDcoument(idDocument));
                        fcs.StartPosition = FormStartPosition.CenterScreen;
                        fcs.ShowDialog();
                        formVenta.resetearTodosLosValores(true);
                        idDocument = 0;
                        FormVenta.idDocument = idDocument;
                        if (formWaiting != null)
                        {
                            formWaiting.Dispose();
                            formWaiting.Close();
                        }
                        if (FormVenta.pausedFragment == 1) {
                            this.Close();
                        }
                        else
                        {
                            this.Close();
                        }
                    } else {
                        int idPedido = DocumentModel.getCiddoctopedidoFromADocument(idDocument);
                        if (idPedido != 0) {
                            PedidosEncabezadoModel.marcarPedidoComoListoONo(idPedido, 1);
                        }
                        DocumentModel.updateToPauseTheDocument(idDocument, 0);
                        if (serverModeLAN)
                        {
                            if (idCustomer < 0)
                            {
                                if (CustomerADCModel.isTheCustomerSendedByIdPanel(idCustomer))
                                {
                                    List<int> idDoclist = new List<int>();
                                    idDoclist.Add(idDocument);
                                    enviarDocDirectamenteAlWs(idDocument + "-" + 0, 1, 0, 0, idDoclist);
                                }
                                else
                                {
                                    dynamic responseCliente = await SendAdditionalCustomerController.enviarClientesAdicionales(idCustomer);
                                    if (responseCliente.value == 1)
                                    {
                                        List<int> idDoclist = new List<int>();
                                        idDoclist.Add(idDocument);
                                        enviarDocDirectamenteAlWs(idDocument + "-" + 0, 1, 0, 0, idDoclist);
                                    }
                                    else
                                    {
                                        if (formWaiting != null)
                                        {
                                            formWaiting.Dispose();
                                            formWaiting.Close();
                                        }
                                        FormMessage formMessage = new FormMessage("Error", responseCliente.description, 2);
                                        formMessage.ShowDialog();
                                        List<int> idDoclist = new List<int>();
                                        idDoclist.Add(idDocument);
                                        enviarDocDirectamenteAlWs(idDocument + "-" + 0, 1, 0, 0, idDoclist);
                                    }
                                }
                            }
                            else
                            {
                                List<int> idDoclist = new List<int>();
                                idDoclist.Add(idDocument);
                                enviarDocDirectamenteAlWs(idDocument + "-" + 0, 1, 0, 0, idDoclist);
                            }
                        } else
                        {
                            if (webActive)
                            {
                                if (idCustomer < 0)
                                {
                                    if (CustomerADCModel.isTheCustomerSendedByIdPanel(idCustomer))
                                    {
                                        List<int> idDoclist = new List<int>();
                                        idDoclist.Add(idDocument);
                                        enviarDocDirectamenteAlWs(idDocument + "-" + 0, 1, 0, 0, idDoclist);
                                    }
                                    else
                                    {
                                        dynamic responseCliente = await SendAdditionalCustomerController.enviarClientesAdicionales(idCustomer);
                                        if (responseCliente.value == 1)
                                        {
                                            List<int> idDoclist = new List<int>();
                                            idDoclist.Add(idDocument);
                                            enviarDocDirectamenteAlWs(idDocument + "-" + 0, 1, 0, 0, idDoclist);
                                        }
                                        else
                                        {
                                            if (formWaiting != null)
                                            {
                                                formWaiting.Dispose();
                                                formWaiting.Close();
                                            }
                                            FormMessage formMessage = new FormMessage("Error", responseCliente.description, 2);
                                            formMessage.ShowDialog();
                                            List<int> idDoclist = new List<int>();
                                            idDoclist.Add(idDocument);
                                            enviarDocDirectamenteAlWs(idDocument + "-" + 0, 1, 0, 0, idDoclist);
                                        }
                                    }
                                }
                                else
                                {
                                    List<int> idDoclist = new List<int>();
                                    idDoclist.Add(idDocument);
                                    enviarDocDirectamenteAlWs(idDocument + "-" + 0, 1, 0, 0, idDoclist);
                                }
                            } 
                            else
                            {
                                validateSendDocumentsResponse(1, 100, "", 3, idDocument + "", null, 0);
                            }
                        }
                    }
                }
            }
        }

        private void dataGridViewFcFrmPayCArt_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) {
                DataGridViewRow row = dataGridViewFcFrmPayCArt.Rows[e.RowIndex];
                int idFc = Convert.ToInt32(row.Cells[0].Value.ToString());
                FrmAddAbono faa = new FrmAddAbono(this, idDocument, idFc);
                faa.StartPosition = FormStartPosition.CenterScreen;
                faa.ShowDialog();
            }
        }

        private void btnObservacionesFrmPayCart_Click(object sender, EventArgs e)
        {
            FrmAddObservation fao = new FrmAddObservation(this);
            fao.StartPosition = FormStartPosition.CenterScreen;
            fao.ShowDialog();
        }

        private void dataGridViewFcFrmPayCArt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) {
                e.SuppressKeyPress = true;
                int rowIndex = 0;
                if (dataGridViewFcFrmPayCArt.CurrentRow != null)
                    rowIndex = dataGridViewFcFrmPayCArt.CurrentRow.Index;
                if (rowIndex >= 0)
                {
                    if (dataGridViewFcFrmPayCArt.Rows.Count > 0)
                    {
                        DataGridViewRow row = dataGridViewFcFrmPayCArt.Rows[rowIndex];
                        int idFc = Convert.ToInt32(row.Cells[0].Value.ToString());
                        FrmAddAbono faa = new FrmAddAbono(this, idDocument, idFc);
                        faa.StartPosition = FormStartPosition.CenterScreen;
                        faa.ShowDialog();
                    } else
                    {
                        FormMessage formMessage = new FormMessage("Cobrando Documentos", "La forma de pago no fue " +
                            "encontrada, validar elegir la correcta!", 3);
                        formMessage.ShowDialog();
                    }
                }
            }
            else if (e.KeyCode == Keys.F10)
            {
                formWaiting = new FormWaiting(this, 0);
                formWaiting.ShowDialog();
                //callTerminateDocumentTask();
            } else if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void FrmPayCart_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F10)
            {
                formWaiting = new FormWaiting(this, 0);
                formWaiting.ShowDialog();
                //callTerminateDocumentTask();
            }
        }

        private void checkBoxInvoiceFrmPayCart_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxInvoiceFrmPayCart.Focused == true) {
                validateInvoiceInDocuments();
            }
        }

        private async Task validateInvoiceInDocuments()
        {
            if (checkBoxInvoiceFrmPayCart.Checked) {
                if (tipoDeDocumento == DocumentModel.TIPO_VENTA || tipoDeDocumento == DocumentModel.TIPO_REMISION) {
                    if (checkBoxCreditoFrmPayCart.Checked) {
                        int fiscalMovement = await validateIfDocumentHaveFiscalMovements();
                        if (fiscalMovement == 0) {
                            if (DocumentModel.updateGenerarFactura(idDocument, DocumentModel.NO_FISCAL_NO_FACTURAR)) {
                                checkBoxInvoiceFrmPayCart.Checked = false;
                                checkBoxInvoiceFrmPayCart.Text = "No se facturará";
                                textFacturaFrmPayCart.Visible = false;
                                FormMessage fm = new FormMessage("Documento con Atículos No Fiscales", "No puedes facturar un documento con artículos No Fiscales", 3);
                                fm.ShowDialog();
                            }
                        } else if (fiscalMovement == 1) {
                            if (DocumentModel.updateGenerarFactura(idDocument, DocumentModel.FISCAL_NO_FACTURAR)) {
                                checkBoxInvoiceFrmPayCart.Checked = true;
                                checkBoxInvoiceFrmPayCart.Text = "Sí se facturará";
                                textFacturaFrmPayCart.Visible = true;
                            }
                        }
                    } else {
                        int fiscalMovement = await validateIfDocumentHaveFiscalMovements();
                        if (fiscalMovement == 0) {
                            /*if (ClsDocumentModel.updateGenerarFactura(idDocument, ClsDocumentModel.NO_FISCAL_NO_FACTURAR)) {
                                checkBoxInvoiceFrmPayCart.Checked = false;
                                checkBoxInvoiceFrmPayCart.Text = "No";
                                textFacturaFrmPayCart.Visible = false;
                                FormMessage fm = new FormMessage("Oops no puedes facturar un documento con artículos No Fiscales", "Documento con Atículos No Fiscales", 3);
                                fm.ShowDialog();
                            }*/
                            int fiscalDocument = DocumentModel.getIntValue("SELECT " + LocalDatabase.CAMPO_FACTURA_DOC + " FROM " + LocalDatabase.TABLA_DOCUMENTOVENTA +
                        " WHERE " + LocalDatabase.CAMPO_ID_DOC + " = " + idDocument);
                            if (fiscalDocument == DocumentModel.FISCAL_NO_FACTURAR)
                            {
                                fiscalDocument = DocumentModel.FISCAL_FACTURAR;
                            }
                            else if (fiscalDocument == 0)
                            {
                                fiscalDocument = 1;
                            } else if (fiscalDocument == DocumentModel.NO_FISCAL_NO_FACTURAR)
                            {
                                fiscalDocument = DocumentModel.FISCAL_FACTURAR;
                            }
                            DocumentModel.updateGenerarFactura(idDocument, fiscalDocument);
                            DocumentModel.updateQuoteDocumentType(idDocument, DocumentModel.TIPO_VENTA);
                            checkBoxInvoiceFrmPayCart.Checked = true;
                            checkBoxInvoiceFrmPayCart.Text = "Sí se facturará";
                            textFacturaFrmPayCart.Visible = true;
                        } else if (fiscalMovement > 0) {
                            int fiscalDocument = DocumentModel.getIntValue("SELECT " + LocalDatabase.CAMPO_FACTURA_DOC + " FROM " + LocalDatabase.TABLA_DOCUMENTOVENTA +
                        " WHERE " + LocalDatabase.CAMPO_ID_DOC + " = " + idDocument);
                            if (fiscalDocument == DocumentModel.FISCAL_NO_FACTURAR) {
                                fiscalDocument = DocumentModel.FISCAL_FACTURAR;
                                checkBoxInvoiceFrmPayCart.Checked = true;
                                checkBoxInvoiceFrmPayCart.Text = "Sí se facturará";
                                textFacturaFrmPayCart.Visible = true;
                            }
                            else if (fiscalDocument == 0)
                            {
                                fiscalDocument = 1;
                                checkBoxInvoiceFrmPayCart.Checked = true;
                                checkBoxInvoiceFrmPayCart.Text = "Sí se facturará";
                                textFacturaFrmPayCart.Visible = true;
                            }
                            else if (fiscalDocument == DocumentModel.FISCAL_FACTURAR && activateOpcionFacturar == 0)
                            {
                                fiscalDocument = DocumentModel.FISCAL_NO_FACTURAR;
                                checkBoxInvoiceFrmPayCart.Checked = false;
                                checkBoxInvoiceFrmPayCart.Text = "No se facturará";
                                textFacturaFrmPayCart.Visible = true;
                            } else if (fiscalDocument == DocumentModel.FISCAL_FACTURAR && activateOpcionFacturar == 1)
                            {
                                fiscalDocument = DocumentModel.FISCAL_NO_FACTURAR;
                                checkBoxInvoiceFrmPayCart.Checked = false;
                                checkBoxInvoiceFrmPayCart.Text = "No se facturará";
                                textFacturaFrmPayCart.Visible = true;
                            }
                            DocumentModel.updateGenerarFactura(idDocument, fiscalDocument);
                            DocumentModel.updateQuoteDocumentType(idDocument, DocumentModel.TIPO_VENTA);
                        }
                    }
                } else {
                    if (tipoDeDocumento == DocumentModel.TIPO_COTIZACION) {
                        DocumentModel.updateGenerarFactura(idDocument, DocumentModel.FISCAL_FACTURAR);
                        checkBoxInvoiceFrmPayCart.Checked = true;
                        checkBoxInvoiceFrmPayCart.Text = "Sí se facturará";
                    }
                }
            } else {
                if (tipoDeDocumento == DocumentModel.TIPO_VENTA || tipoDeDocumento == DocumentModel.TIPO_REMISION) {
                    if (checkBoxCreditoFrmPayCart.Checked) {
                        int fiscalMovement = await validateIfDocumentHaveFiscalMovements();
                        if (fiscalMovement == 0) {
                            if (DocumentModel.updateGenerarFactura(idDocument, DocumentModel.NO_FISCAL_NO_FACTURAR)) {
                                checkBoxInvoiceFrmPayCart.Checked = false;
                                checkBoxInvoiceFrmPayCart.Text = "No se facturará";
                                textFacturaFrmPayCart.Visible = false;
                                FormMessage fm = new FormMessage("Documento con Atículos No Fiscales", "No puedes facturar un documento con artículos No Fiscales", 3);
                                fm.ShowDialog();
                            }
                        } else if (fiscalMovement == 1) {
                            if (DocumentModel.updateGenerarFactura(idDocument, DocumentModel.FISCAL_NO_FACTURAR)) {
                                checkBoxInvoiceFrmPayCart.Checked = true;
                                checkBoxInvoiceFrmPayCart.Text = "Sí se facturará";
                                textFacturaFrmPayCart.Visible = true;
                            }
                        }
                    } else {
                        int fiscalDocument = DocumentModel.getIntValue("SELECT " + LocalDatabase.CAMPO_FACTURA_DOC + " FROM " + LocalDatabase.TABLA_DOCUMENTOVENTA +
                                            " WHERE " + LocalDatabase.CAMPO_ID_DOC + " = " + idDocument);
                        if (fiscalDocument == DocumentModel.FISCAL_FACTURAR) {
                            int fiscalMovement = await validateIfDocumentHaveFiscalMovements();
                            if (fiscalMovement == 0)
                            {
                                fiscalDocument = DocumentModel.NO_FISCAL_NO_FACTURAR;
                                checkBoxInvoiceFrmPayCart.Checked = false;
                                checkBoxInvoiceFrmPayCart.Text = "No se facturará";
                                textFacturaFrmPayCart.Visible = true;
                                DocumentModel.updateQuoteDocumentType(idDocument, DocumentModel.TIPO_REMISION);
                            } else if (fiscalMovement == 1)
                            {
                                fiscalDocument = DocumentModel.FISCAL_NO_FACTURAR;
                                checkBoxInvoiceFrmPayCart.Checked = false;
                                checkBoxInvoiceFrmPayCart.Text = "No se facturará";
                                textFacturaFrmPayCart.Visible = true;
                                DocumentModel.updateQuoteDocumentType(idDocument, DocumentModel.TIPO_VENTA);
                            }                            
                        } else if (fiscalDocument == 1) {
                            fiscalDocument = 0;
                            checkBoxInvoiceFrmPayCart.Checked = false;
                            checkBoxInvoiceFrmPayCart.Text = "No se facturará";
                            textFacturaFrmPayCart.Visible = true;
                        } else if (fiscalDocument == DocumentModel.FISCAL_NO_FACTURAR && activateOpcionFacturar == 0) {
                            fiscalDocument = DocumentModel.FISCAL_FACTURAR;
                            checkBoxInvoiceFrmPayCart.Checked = true;
                            checkBoxInvoiceFrmPayCart.Text = "Sí se facturará";
                            textFacturaFrmPayCart.Visible = true;
                        } else if (fiscalDocument == DocumentModel.FISCAL_NO_FACTURAR && activateOpcionFacturar == 1) {
                            checkBoxInvoiceFrmPayCart.Checked = false;
                            checkBoxInvoiceFrmPayCart.Text = "No se facturará";
                            textFacturaFrmPayCart.Visible = true;
                        } else if (fiscalDocument == DocumentModel.NO_FISCAL_NO_FACTURAR && activateOpcionFacturar == 0) {
                            fiscalDocument = DocumentModel.FISCAL_FACTURAR;
                            DocumentModel.updateQuoteDocumentType(idDocument, DocumentModel.TIPO_VENTA);
                            checkBoxInvoiceFrmPayCart.Checked = true;
                            checkBoxInvoiceFrmPayCart.Text = "Sí se facturará";
                            textFacturaFrmPayCart.Visible = true;
                        }
                        else if (fiscalDocument == DocumentModel.NO_FISCAL_NO_FACTURAR && activateOpcionFacturar == 1)
                        {
                            fiscalDocument = DocumentModel.FISCAL_FACTURAR;
                            DocumentModel.updateQuoteDocumentType(idDocument, DocumentModel.TIPO_VENTA);
                            checkBoxInvoiceFrmPayCart.Checked = true;
                            checkBoxInvoiceFrmPayCart.Text = "Sí se facturará";
                            textFacturaFrmPayCart.Visible = true;
                        }
                        DocumentModel.updateGenerarFactura(idDocument, fiscalDocument);
                    }
                } else {
                    DocumentModel.updateGenerarFactura(idDocument, 0);
                    checkBoxInvoiceFrmPayCart.Checked = false;
                    checkBoxInvoiceFrmPayCart.Text = "No se facturará";
                }
            }
        }

        private void editNumeroCopias_KeyPress(object sender, KeyPressEventArgs e)
        {
            char signo_decimal = (char)46;
            //Para obligar a que sólo se introduzcan números
            if (Char.IsDigit(e.KeyChar) || e.KeyChar == signo_decimal)
            {
                e.Handled = false;
            } else if (Char.IsControl(e.KeyChar)) //permitir teclas de control como retroceso
            {
                e.Handled = false;
            } else
            {
                //el resto de teclas pulsadas se desactivan
                e.Handled = true;
            }
        }

        private void checkBoxInvoiceFrmPayCart_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F10)
            {
                formWaiting = new FormWaiting(this, 0);
                formWaiting.ShowDialog();
                //callTerminateDocumentTask();
            }
        }

        private void btnCancelFrmPayCart_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F10)
            {
                formWaiting = new FormWaiting(this, 0);
                formWaiting.ShowDialog();
                //callTerminateDocumentTask();
            }
        }

        private void checkBoxCotizacionMostrador_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F10)
            {
                formWaiting = new FormWaiting(this, 0);
                formWaiting.ShowDialog();
                //callTerminateDocumentTask();
            }
        }

        private void btnAceptarFrmPayCart_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F10)
            {
                formWaiting = new FormWaiting(this, 0);
                formWaiting.ShowDialog();
                //callTerminateDocumentTask();
            }
        }

        private void checkBoxCreditoFrmPayCart_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F10)
            {
                formWaiting = new FormWaiting(this, 0);
                formWaiting.ShowDialog();
                //callTerminateDocumentTask();
            }
        }

        private void editNumeroCopias_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F10)
            {
                formWaiting = new FormWaiting(this, 0);
                formWaiting.ShowDialog();
                //callTerminateDocumentTask();
            }
        }

        private void btnObservacionesFrmPayCart_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F10)
            {
                formWaiting = new FormWaiting(this, 0);
                formWaiting.ShowDialog();
                //callTerminateDocumentTask();
            }
        }

        private void editNumeroCopias_TextChanged(object sender, EventArgs e)
        {
            String numero = editNumeroCopias.Text.Trim();
            if (!numero.Equals(""))
            {
                if (Convert.ToInt32(numero) > 10)
                {
                    FormMessage formMessage = new FormMessage("Número Máximo Excedido", 
                        "El número máximo de copias del ticket es de 10 por documento", 2);
                    formMessage.ShowDialog();
                    editNumeroCopias.Text = "10";
                }
            }
        }

        private void FrmPayCart_FormClosing(object sender, FormClosingEventArgs e)
        {
            
        }

        private void fillDataGridFormasDeCobro(List<FormasDeCobroModel> fcList)
        {
            if (fcList != null)
            {
                dataGridViewFcFrmPayCArt.Refresh();
                dataGridViewFcFrmPayCArt.Rows.Clear();
                //progress += itemsListTemp.Count;
                //itemsList.AddRange(itemsListTemp);
                if (fcList.Count > 0 && dataGridViewFcFrmPayCArt.ColumnHeadersVisible == false)
                    dataGridViewFcFrmPayCArt.ColumnHeadersVisible = true;
                for (int i = 0; i < fcList.Count; i++)
                {
                    int n = dataGridViewFcFrmPayCArt.Rows.Add();
                    dataGridViewFcFrmPayCArt.Rows[n].Cells[0].Value = fcList[i].FORMA_COBRO_ID + "";
                    dataGridViewFcFrmPayCArt.Columns["idDgvFc"].Visible = false;
                    dataGridViewFcFrmPayCArt.Rows[n].Cells[1].Value = fcList[i].NOMBRE;
                    double importeAbonado = FormasDeCobroDocumentoModel.getTheImportAbonadoForAFormaDeCobroOfADocument(
                                fcList[i].FORMA_COBRO_ID, idDocument);
                    dataGridViewFcFrmPayCArt.Rows[n].Cells[2].Value = importeAbonado;
                }
                dataGridViewFcFrmPayCArt.PerformLayout();
                //fcListTemp.Clear();
                //lastId = Convert.ToInt32(fcList[fcList.Count - 1].FORMA_COBRO_CC_ID);
                imgSinDatosFrmPayCart.Visible = false;
                dataGridViewFcFrmPayCArt.Focus();
            }
            else
            {
                imgSinDatosFrmPayCart.Visible = true;
            }
        }


    }
}