using SyncTPV.Controllers;
using SyncTPV.Helpers.SqliteDatabaseHelper;
using SyncTPV.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SyncTPV.Views.Extras
{
    public partial class FormAddField : Form
    {
        private FormConfiguracionWS frmConfiguracionWS;
        private FormWaiting frmWaiting;

        public FormAddField(FormConfiguracionWS frmConfiguracionWS)
        {
            InitializeComponent();
            btnCerrar.Image = MetodosGenerales.redimencionarImagenes(Properties.Resources.close, 35, 35);
            btnSeePassPanel.Image = MetodosGenerales.redimencionarImagenes(Properties.Resources.eye_black, 17, 17);
            btnSeePassCom.Image = MetodosGenerales.redimencionarImagenes(Properties.Resources.eye_black, 17, 17);
            this.frmConfiguracionWS = frmConfiguracionWS;
        }

        private void FormAddField_Load(object sender, EventArgs e)
        {
            String query = "SELECT * FROM "+LocalDatabase.TABLA_INSTANCESQLSE+" WHERE "+LocalDatabase.CAMPO_ID_INSTANCESQLSE+" > 0";
            List<InstanceSQLSEModel> instancesList = InstanceSQLSEModel.getAllInstances(query);
            if (instancesList != null)
            {
                foreach (InstanceSQLSEModel instance in instancesList)
                {
                    if (instance.id == 2)
                    {
                        editIpServerPanel.Text = instance.IPServer;
                        editNombreInstanciaPanel.Text = instance.instance;
                        editDbNamePanel.Text = instance.dbName;
                        editUserInstanciaPanel.Text = instance.user;
                        editPassInstanciaPanel.Text = instance.pass;
                    } else
                    {
                        editIpServidorComercial.Text = instance.IPServer;
                        editNombreInstanciaComercial.Text = instance.instance;
                        editDbNameComercial.Text = instance.dbName;
                        editDbNameComercial.ForeColor = Color.Black;
                        editUserInstanciaComercial.Text = instance.user;
                        editPassInstanciaComercial.Text = instance.pass;
                    }
                }
            } else
            {
                editIpServerPanel.Text = "127.0.0.1";
                editIpServidorComercial.Text = "127.0.0.1";
            }
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void editIpServer_KeyPress(object sender, KeyPressEventArgs e)
        {
            char signo_decimal = (char)46;
            //Para obligar a que sólo se introduzcan números
            if (Char.IsDigit(e.KeyChar) || e.KeyChar == signo_decimal)
            {
                e.Handled = false;
            }
            else
              if (Char.IsControl(e.KeyChar)) //permitir teclas de control como retroceso
            {
                e.Handled = false;
            }
            else
            {
                //el resto de teclas pulsadas se desactivan
                e.Handled = true;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            frmWaiting = new FormWaiting(this, 0);
            frmWaiting.ShowDialog();
            //processToSaveIP();
        }

        public async Task processToSaveIP()
        {
            String ipServerPanel = editIpServerPanel.Text.Trim();
            String nombreInstanciaPanel = editNombreInstanciaPanel.Text.Trim();
            String dbNamePanel = editDbNamePanel.Text.Trim();
            String userPanel = editUserInstanciaPanel.Text.Trim();
            String passPanel = editPassInstanciaPanel.Text.Trim();
            if (ipServerPanel.Equals("") && nombreInstanciaPanel.Equals("") && dbNamePanel.Equals("")  && userPanel.Equals("") && passPanel.Equals(""))
            {
                if (frmWaiting != null)
                    frmWaiting.Close();
                FormMessage formMessage = new FormMessage("Datos Faltantes", "No pudimos obtener información de la instancia", 2);
                formMessage.ShowDialog();
            } else
            {
                if (checkBoxMismaInstancia.Checked)
                {
                    String dbNameComercial = editDbNameComercial.Text.Trim();
                    if (dbNameComercial.Equals(""))
                    {
                        if (frmWaiting != null)
                            frmWaiting.Close();
                        FormMessage formMessage = new FormMessage("Datos Faltantes", "El nombre de la base de datos de Comercial No puede ir vacio", 2);
                        formMessage.ShowDialog();
                    } else
                    {
                        int response = await getConnectionLAN(1, ipServerPanel, nombreInstanciaPanel, dbNameComercial, userPanel, passPanel);
                        if (response == 1)
                        {
                            response = await getConnectionLAN(2, ipServerPanel, nombreInstanciaPanel, dbNamePanel, userPanel, passPanel);
                            if (response == 1)
                            {
                                String msgUsers = "";
                                dynamic responseCajas = await CajaController.getAllCajasLAN();
                                if (responseCajas.value == 1)
                                {
                                    dynamic usersSaved = await UsersController.getAllUsersLAN();
                                    if (usersSaved.value == 1)
                                    {
                                        msgUsers = "Usuarios Actualizados";
                                    }
                                    else
                                    {
                                        msgUsers = "Validar que existan usuarios";
                                    }
                                    if (frmWaiting != null)
                                        frmWaiting.Close();
                                    FormMessage fm = new FormMessage("Genial", "La conexión local se realizó correctamente\n " + msgUsers, 1);
                                    fm.ShowDialog();
                                    this.Close();
                                    if (frmConfiguracionWS != null)
                                        frmConfiguracionWS.Close();
                                } else
                                {
                                    if (frmWaiting != null)
                                        frmWaiting.Close();
                                    FormMessage fm = new FormMessage("Cajas", responseCajas.description, 2);
                                    fm.ShowDialog();
                                }
                             
             
                                
                            }
                            else
                            {
                                if (frmWaiting != null)
                                    frmWaiting.Close();
                                FormMessage fm = new FormMessage("Exception", "No pudimos encontrar la Instancia de PanelROM, validar datos ingresados", 2);
                                fm.ShowDialog();
                            }
                        }
                        else
                        {
                            if (frmWaiting != null)
                                frmWaiting.Close();
                            FormMessage fm = new FormMessage("Exception", "No pudimos encontrar la instancia de Comercial, validar datos ingresados", 2);
                            fm.ShowDialog();
                        }
                    }
                }
                else
                {
                    String ipServerCom = editIpServidorComercial.Text.Trim();
                    String nombreInstanciaCom = editNombreInstanciaComercial.Text.Trim();
                    String dbNameComercial = editDbNameComercial.Text.Trim();
                    String userCom = editUserInstanciaComercial.Text.Trim();
                    String passCom = editPassInstanciaComercial.Text.Trim();
                    if (ipServerCom.Equals("") && nombreInstanciaCom.Equals("") && dbNameComercial.Equals("") && userCom.Equals("") && passCom.Equals(""))
                    {
                        if (frmWaiting != null)
                            frmWaiting.Close();
                        FormMessage formMessage = new FormMessage("Datos Faltantes", "No pudimos obtener información de la instancia", 2);
                        formMessage.ShowDialog();
                    } else
                    {
                        int response = await getConnectionLAN(1, ipServerCom, nombreInstanciaCom, dbNameComercial, userCom, passCom);
                        if (response == 1)
                        {
                            response = await getConnectionLAN(2, ipServerPanel, nombreInstanciaPanel, dbNamePanel, userPanel, passPanel);
                            if(response == 1)
                            {
                                String msgUsers = "";
                                dynamic usersSaved = await UsersController.getAllUsersLAN();
                                if (usersSaved.value == 1)
                                {
                                    msgUsers = "Usuarios Actualizados";
                                }
                                else
                                {
                                    msgUsers = "Validar que existan usuarios";
                                }
                                if (frmWaiting != null)
                                    frmWaiting.Close();
                                FormMessage fm = new FormMessage("Genial", "La conexión local se realizó correctamente\n "+ msgUsers, 1);
                                fm.ShowDialog();
                                this.Close();
                                if (frmConfiguracionWS != null)
                                    frmConfiguracionWS.Close();
                            } else
                            {
                                if (frmWaiting != null)
                                    frmWaiting.Close();
                                FormMessage fm = new FormMessage("Exception", "No pudimos encontrar la Instancia de PanelROM, validar datos ingresados", 2);
                                fm.ShowDialog();
                            }
                        }
                        else
                        {
                            if (frmWaiting != null)
                                frmWaiting.Close();
                            FormMessage fm = new FormMessage("Exception", "No pudimos encontrar la instancia de Comercial, validar datos ingresados", 2);
                            fm.ShowDialog();
                        }                        
                    }
                }

                String panelInstance = InstanceSQLSEModel.getStringPanelInstance();
                String ComInstance = InstanceSQLSEModel.getStringComInstance();
                dynamic responseConceptos = ConceptoModel.ActualizarConceptosEImpuestos(panelInstance, ComInstance);
            }
        }

        private async Task<int> getConnectionLAN(int idInstance, String ip, String instance, String dbName, String user, String pass)
        {
            int resp = 0;
            if (ConfiguracionModel.configurationExist(1))
            {
                String textInstance = @"Server=" + ip + "\\" + instance + ";Database=" + dbName + ";User Id=" + user + ";Pwd=" + pass + ";Connect Timeout=20;";
                if (idInstance == 1)
                {
                    int response = await ConfigurationWsController.validateInstances(1, textInstance);
                    if (response >= 1)
                    {
                        if (InstanceSQLSEModel.exist(idInstance))
                            InstanceSQLSEModel.updateInstance(idInstance, ip, instance, dbName, user, pass);
                        else InstanceSQLSEModel.insertARecord(idInstance, instance, dbName, user, pass, ip);
                        resp = 1;
                    }
                } else
                {
                    int response = await ConfigurationWsController.validateInstances(2, textInstance);
                    if (response >= 1)
                    {
                        if (InstanceSQLSEModel.exist(idInstance))
                            InstanceSQLSEModel.updateInstance(idInstance, ip, instance, dbName, user, pass);
                        else InstanceSQLSEModel.insertARecord(idInstance, instance, dbName, user, pass, ip);
                        resp = 1;
                    }
                }
            } else
            {
                dynamic responseConfig = ConfiguracionModel.saveLinkWs("");
                if (responseConfig.value == 1)
                {
                    String textInstance = @"Server=" + ip + "\\" + instance + ";Database=" + dbName + ";User Id=" + user + ";Pwd=" + pass + ";Connect Timeout=20;";
                    if (idInstance == 1)
                    {
                        int response = await ConfigurationWsController.validateInstances(1, textInstance);
                        if (response >= 1)
                        {
                            if (InstanceSQLSEModel.exist(idInstance))
                                InstanceSQLSEModel.updateInstance(idInstance, ip, instance, dbName, user, pass);
                            else InstanceSQLSEModel.insertARecord(idInstance, instance, dbName, user, pass, ip);
                            resp = 1;
                        }
                    }
                    else
                    {
                        int response = await ConfigurationWsController.validateInstances(2, textInstance);
                        if (response >= 1)
                        {
                            if (InstanceSQLSEModel.exist(idInstance))
                                InstanceSQLSEModel.updateInstance(idInstance, ip, instance, dbName, user, pass);
                            else InstanceSQLSEModel.insertARecord(idInstance, instance, dbName, user, pass, ip);
                            resp = 1;
                        }
                    }
                } else
                {
                    FormMessage formMessage = new FormMessage("configuration", responseConfig.description, 3);
                    formMessage.ShowDialog();
                }
            }
            return resp;
        }

        private void checkBoxMismaInstancia_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxMismaInstancia.Focused)
            {
                if (checkBoxMismaInstancia.Checked)
                {
                    groupBoxPanelInstance.Text = "Intancia de PanelROM y Comercial";
                    textInfoIpComercial.Enabled = false;
                    editIpServidorComercial.Enabled = false;
                    textInfoInstanciaComercial.Enabled = false;
                    editNombreInstanciaComercial.Enabled = false;
                    textInfoUserComercial.Enabled = false;
                    editUserInstanciaComercial.Enabled = false;
                    textInfoPassComercial.Enabled = false;
                    editPassInstanciaComercial.Enabled = false;
                } else
                {
                    groupBoxPanelInstance.Text = "Intancia de PanelROM";
                    textInfoIpComercial.Enabled = true;
                    editIpServidorComercial.Enabled = true;
                    textInfoInstanciaComercial.Enabled = true;
                    editNombreInstanciaComercial.Enabled = true;
                    textInfoUserComercial.Enabled = true;
                    editUserInstanciaComercial.Enabled = true;
                    textInfoPassComercial.Enabled = true;
                    editPassInstanciaComercial.Enabled = true;
                }
            }
        }

        private void editDbNameComercial_Enter(object sender, EventArgs e)
        {
            if (editDbNameComercial.Text.Trim().Equals("Nombre_BDatos_Comercial_01"))
            {
                editDbNameComercial.Text = "";
                editDbNameComercial.ForeColor = Color.Black;
            }
        }

        private void editDbNameComercial_Leave(object sender, EventArgs e)
        {
            if (editDbNameComercial.Text.Trim().Equals(""))
            {
                editDbNameComercial.Text = "Nombre_BDatos_Comercial_01";
                editDbNameComercial.ForeColor = Color.Silver;
            }
        }

        private void btnSeePassPanel_Click(object sender, EventArgs e)
        {
            if (editPassInstanciaPanel.PasswordChar == '*')
            {
                editPassInstanciaPanel.PasswordChar = '\0';
                btnSeePassPanel.Image = MetodosGenerales.redimencionarImagenes(Properties.Resources.eye_off_black, 15, 17);
            } else
            {
                editPassInstanciaPanel.PasswordChar = '*';
                btnSeePassPanel.Image = MetodosGenerales.redimencionarImagenes(Properties.Resources.eye_black, 15, 17);
            }
        }

        private void btnSeePassCom_Click(object sender, EventArgs e)
        {
            if (editPassInstanciaComercial.PasswordChar == '*')
            {
                btnSeePassCom.Image = MetodosGenerales.redimencionarImagenes(Properties.Resources.eye_off_black, 15, 17);
                editPassInstanciaComercial.PasswordChar = '\0';
            }
            else
            {
                btnSeePassCom.Image = MetodosGenerales.redimencionarImagenes(Properties.Resources.eye_black, 15, 17);
                editPassInstanciaComercial.PasswordChar = '*';
            }
        }
    }
}
