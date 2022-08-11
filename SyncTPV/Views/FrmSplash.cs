using SyncTPV.Helpers.SqliteDatabaseHelper;
using SyncTPV.Models;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SyncTPV.Views
{
    public partial class FrmSplash : Form
    {
        public static bool firtsTime = true;
        public FrmSplash()
        {
            InitializeComponent();
            firtsTime = true;
        }

        private void FrmSplash_Load(object sender, EventArgs e)
        {
            this.BackgroundImage = MetodosGenerales.redimencionarBitmap(Properties.Resources.splashsynctpv2, 835, 430);
            validation();
        }

        private async Task validation()
        {
            bool validated = await verifySQLiteDatabase();
            if (validated)
            {
                this.Close();
                firtsTime = false;
            }
        }

        private async Task<Boolean> verifySQLiteDatabase()
        {
            bool created = false;
            await Task.Run(() =>
            {
                ClsSQLiteDbHelper dh = new ClsSQLiteDbHelper();
                created = dh.validateDb();
                ClsInitialRecords.loadInitialValuesInitialLoad();
                Thread.Sleep(1000);
            });
            return created;
        }
    }
}
