using System;
using System.Collections.Generic;

namespace SyncTPV.Models
{
    public class ClsInitialRecords
    {

        public static void loadInitialValuesInitialLoad()
        {
            List<String> ciNameMethods = new List<String>();
            ciNameMethods.Add("Clientes");
            ciNameMethods.Add("Artículos");
            ciNameMethods.Add("Cuentas por Cobrar");
            ciNameMethods.Add("Motivos No Visitas");
            ciNameMethods.Add("Bancos");
            ciNameMethods.Add("Clasificaciones");
            ciNameMethods.Add("Formas de Cobro");
            ciNameMethods.Add("Clasificacion Valor");
            ciNameMethods.Add("Promociones");
            ciNameMethods.Add("Datos Ticket");
            ciNameMethods.Add("Precios Empresa");
            ciNameMethods.Add("Pedidos CallCenter");
            ciNameMethods.Add("Formas de Pago");
            ciNameMethods.Add("Zonas de Clientes");
            ciNameMethods.Add("Ciudades");
            ciNameMethods.Add("Estados");
            ciNameMethods.Add("Pagos Créditos");
            ciNameMethods.Add("Pedidos");
            ciNameMethods.Add("UnitMeasureWeight");
            ciNameMethods.Add("ConversionsUnit");
            int agregado = ClsBanderasCargaInicialModel.createRecordsForCI(ciNameMethods);
        }

    }
}
