using SyncTPV.Controllers;
using SyncTPV.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SyncTPV.Views.Extras
{
    public partial class FormImagen : Form
    {
        private int modelType = 0;
        private int idModel = 0;
        private bool serverModeLAN = false;

        public FormImagen(int modelType, int idModel)
        {
            this.modelType = modelType;
            this.idModel = idModel;
            serverModeLAN = ConfiguracionModel.isLANPermissionActivated();
            InitializeComponent();
            btnClose.Image = MetodosGenerales.redimencionarImagenes(Properties.Resources.back_white, 45, 45);
        }

        private async void FormImagen_Load(object sender, EventArgs e)
        {
            String rutaLocal = await downloadImage();
            loadImage(rutaLocal);
        }

        private async Task<String> downloadImage()
        {
            String rutaLocal = "";
            String nomenclatura = "";
            await Task.Run(async () =>
            {
                if (serverModeLAN)
                {
                    String carpetaServer = "";
                    if (modelType == 0)
                    {
                        carpetaServer = "items";
                        rutaLocal = MetodosGenerales.rootDirectory + "\\Imagenes\\items";
                    } else if (modelType == 1)
                    {
                        carpetaServer = "customers";
                        rutaLocal = MetodosGenerales.rootDirectory + "\\Imagenes\\customers";
                    }
                    await SubirImagenesController.getImageLAN(carpetaServer, rutaLocal, idModel);
                }
                else
                {
                    if (modelType == 0)
                    {
                        nomenclatura = "-1";
                        rutaLocal = MetodosGenerales.rootDirectory + "\\Imagenes\\items";
                    }
                    else if (modelType == 1)
                    {
                        nomenclatura = "-c";
                        rutaLocal = MetodosGenerales.rootDirectory + "\\Imagenes\\customers";
                    }
                    await SubirImagenesController.getImage(nomenclatura, idModel);
                }
            });
            return rutaLocal;
        }

        private async Task loadImage(String rutaLocal)
        {
            FileStream fs = null;
            await Task.Run(async () =>
            {
                if (modelType == 0)
                {
                    rutaLocal += "\\" + idModel + "-1.jpg";
                }
                else if (modelType == 1)
                {
                    rutaLocal += "\\" + idModel + "-c.jpg";
                }
                if (!File.Exists(rutaLocal))
                    rutaLocal = MetodosGenerales.rootDirectory + "\\Imagenes\\Estaticas\\SyncTPV.png";
                fs = new FileStream(rutaLocal, FileMode.Open, FileAccess.Read);
            });
            if (fs != null)
            {
                img.Image = MetodosGenerales.redimencionarImagenes(Image.FromStream(fs), Image.FromStream(fs).Width, Image.FromStream(fs).Height);
                fs.Close();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        
    }
}
