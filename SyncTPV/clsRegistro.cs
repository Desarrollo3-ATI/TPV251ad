using AdminDll;
using Cripto;
namespace SyncTPV
{
    public class clsRegistro
    {
        public string DR { set; get; }
        public string EID { set; get; }
        public string FA { set; get; }
        public string FV { set; get; }
        public string SK { set; get; }
        public string TL { set; get; }
        public string TS { set; get; }
        public string IDE { set; get; }

        public void Encriptar()
        {
            DR = BajoNivel.Encriptar(DR);
            EID = BajoNivel.Encriptar(EID);
            FA = BajoNivel.Encriptar(FA);
            FV = BajoNivel.Encriptar(FV);
            SK = BajoNivel.Encriptar(SK);
            TL = BajoNivel.Encriptar(TL);
            TS = BajoNivel.Encriptar(TS);
            IDE = BajoNivel.Encriptar(IDE);
        }
        public void Desencriptar()
        {
            DR = AES.Desencriptar(DR);
            EID = AES.Desencriptar(EID);
            FA = AES.Desencriptar(FA);
            FV = AES.Desencriptar(FV);
            SK = AES.Desencriptar(SK);
            TS = AES.Desencriptar(TS);
            TL = AES.Desencriptar(TL);
            IDE = AES.Desencriptar(IDE);
        }
    }
}
