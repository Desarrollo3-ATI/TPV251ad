using System;

namespace SyncTPV
{
    public static class clsSoaps
    {
        public static String DTUSUARIOS =
            @"<?xml version=""1.0"" encoding=""utf-8""?>
            <soap:Envelope xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" 
            xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns:soap=""http://schemas.xmlsoap.org/soap/envelope/"">            
                <soap:Body>              
                    <DTUSUARIOS xmlns=""http://tempuri.org/""/>              
                </soap:Body>
            </soap:Envelope>";

        public static string DTARTICULOS(string Ruta, int UltimoId)
        {
            string soap =
            @"<?xml version=""1.0"" encoding=""utf-8""?>
            <soap:Envelope xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance""
            xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns:soap=""http://schemas.xmlsoap.org/soap/envelope/"">
                <soap:Body>
                    <DTArticulos2 xmlns=""http://tempuri.org/"">
                        <ruta>" + Ruta + @"</ruta>
                        <UltimoIdArticulo>" + UltimoId + @"</UltimoIdArticulo>
                    </DTArticulos2>
                </soap:Body>
            </soap:Envelope>";
            return soap;
        }


        public static string DTCLIENTES(string ruta, int UltimoId)
        {
            string soap =
            @"<?xml version=""1.0"" encoding=""utf-8""?>
            <soap:Envelope xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns:soap=""http://schemas.xmlsoap.org/soap/envelope/"">
                <soap:Body>
                    <DTCLIENTES xmlns=""http://tempuri.org/"">
                        <ruta>" + ruta + @"</ruta>
                        <UltimoId>" + UltimoId + @"</UltimoId>
                    </DTCLIENTES>
                </soap:Body>
            </soap:Envelope>";
            return soap;
        }
        public static string OBTENERCLIENTETPV()
        {
            string soap =
            @"<?xml version=""1.0"" encoding=""utf-8""?>
            <soap:Envelope xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns:soap=""http://schemas.xmlsoap.org/soap/envelope/"">
              <soap:Body>
                <OBTENERCLIENTETPV xmlns=""http://tempuri.org/""/>
              </soap:Body>
            </soap:Envelope>";
            return soap;
        }
        public static string DTFORMASCOBRO()
        {
            string soap =
            @"<?xml version=""1.0"" encoding=""utf-8""?>
            <soap:Envelope xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns:soap=""http://schemas.xmlsoap.org/soap/envelope/"">
                <soap:Body>
                    <DTFORMASCOBRO xmlns= ""http://tempuri.org/""/>
                </soap:Body>
            </soap:Envelope>";
            return soap;
        }
        public static string DTGRUPOS(string Ruta)
        {
            string soap =
            @"<?xml version=""1.0"" encoding=""utf-8""?>
            <soap:Envelope xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns:soap=""http://schemas.xmlsoap.org/soap/envelope/"">
                <soap:Body>
                    <DTGRUPOS xmlns=""http://tempuri.org/"">
                        <ruta>" + Ruta + @"</ruta>
                    </DTGRUPOS>
                </soap:Body>
           </soap:Envelope>";
            return soap;
        }
        public static string DATOSTICKET()
        {
            string soap =
            @"<?xml version=""1.0"" encoding=""utf-8""?>
            <soap:Envelope xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns:soap=""http://schemas.xmlsoap.org/soap/envelope/"">
                <soap:Body>
                    <DatosTicket xmlns=""http://tempuri.org/""/>
                </soap:Body>
            </soap:Envelope>";
            return soap;
        }

        public static string DTPRECIOSARTICULOS(int UltimoId)
        {
            string soap =
            @"<?xml version=""1.0"" encoding=""utf-8""?>
            <soap:Envelope xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns:soap=""http://schemas.xmlsoap.org/soap/envelope/"">
                <soap:Body>
                    <DTPRECIOSARTICULOS xmlns=""http://tempuri.org/"">
                        <UltimoId>" + UltimoId + @"</UltimoId>
                    </DTPRECIOSARTICULOS>
               </soap:Body>
           </soap:Envelope>";
            return soap;
        }
        public static string DTZONASCLIENTES(string Ruta)
        {
            string soap =
            @"<?xml version=""1.0"" encoding=""utf-8""?>
            <soap:Envelope xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns:soap=""http://schemas.xmlsoap.org/soap/envelope/"">
                <soap:Body>
                    <DTZONASCLIENTES xmlns =""http://tempuri.org/"">
                        <ruta>" + Ruta + @"</ruta>
                    </DTZONASCLIENTES>
                </soap:Body>
            </soap:Envelope>";
            return soap;
        }
        public static string Ping =
            @"<?xml version=""1.0"" encoding=""utf-8""?>
                <soap:Envelope xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" 
                    xmlns:soap=""http://schemas.xmlsoap.org/soap/envelope/"">
                  <soap:Body>
                    <Ping xmlns=""http://tempuri.org/""/>
                  </soap:Body>
                </soap:Envelope>";
        public static string DTESTADOS(string Ruta)
        {
            string soap =
            @"<?xml version=""1.0"" encoding=""utf-8""?>
            <soap:Envelope xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns:soap=""http://schemas.xmlsoap.org/soap/envelope/"">
                <soap:Body>
                    <DTESTADOS xmlns=""http://tempuri.org/"">
                        <ruta>" + Ruta + @"</ruta>
                    </DTESTADOS>
                </soap:Body>
            </soap:Envelope>";
            return soap;
        }
        public static string DTPAGOSCREDITO(string Ruta, int UltimoId)
        {
            string soap =
            @"<?xml version=""1.0"" encoding=""utf-8""?>
            <soap:Envelope xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns:soap=""http://schemas.xmlsoap.org/soap/envelope/"">
                <soap:Body>
                    <DTPAGOSCREDITO xmlns=""http://tempuri.org/"">
                         <ruta>" + Ruta + @"</ruta>
                         <UltimoId>" + UltimoId + @"</UltimoId>
                    </DTPAGOSCREDITO>
                </soap:Body>
            </soap:Envelope>";
            return soap;
        }



        public static string CARGAPEDIDOENCABEZADO(string IdUsuario)
        {
            string valor = @"<?xml version=""1.0"" encoding=""utf-8""?>
            <soap:Envelope xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns:soap=""http://schemas.xmlsoap.org/soap/envelope/"">
                <soap:Body>
                    <FCARGAPEDIDOENCABEZADO xmlns=""http://tempuri.org/"">
                        <USUARIO_ID>" + IdUsuario + @"</USUARIO_ID>
                    </FCARGAPEDIDOENCABEZADO>
                </soap:Body>
            </soap:Envelope>";
            return valor;
        }

        public static string Insertar_PedidosEncabezado(clsDocumento doc)
        {
            string valor = @"<?xml version=""1.0"" encoding=""utf-8""?>
                <soap:Envelope xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" 
                    xmlns:soap=""http://schemas.xmlsoap.org/soap/envelope/"">
                  <soap:Body>
                    <Insertar_PedidosEncabezado xmlns = ""http://tempuri.org/"">
                      <clave_cliente>" + doc.clave_cliente + @"</clave_cliente>
                      <cliente_id>" + doc.cliente_id + @"</cliente_id>
                      <descuento>" + doc.descuento + @"</descuento>
                      <total>" + doc.total + @"</total>
                      <NOMBREU>" + doc.NOMBREU + @"</NOMBREU>
                      <ALMACEN_ID>" + doc.ALMACE_ID + @"</ALMACEN_ID>
                      <ANTICIPO>" + doc.ANTICIPO + @"</ANTICIPO>
                      <TIPO_DOCUMENTO>" + doc.TIPO_DOCUMENTO + @"</TIPO_DOCUMENTO>
                      <FORMA_COBRO_ID>" + doc.FORMA_COBRO_ID + @"</FORMA_COBRO_ID>
                      <FACTURA>" + doc.FACTURA + @"</FACTURA>
                      <observacion>" + doc.observacion + @"</observacion>
                      <DEV>" + doc.DEV + @"</DEV>
                      <FVENTA>" + doc.FVENTA + @"</FVENTA>
                      <FECHAHORAMOV>" + doc.FECHAHORAMOV + @"</FECHAHORAMOV>
                      <USUARIO_ID>" + doc.USUARIO_ID + @"</USUARIO_ID>
                      <FORMA_COBRO_ID_ABONO>" + doc.FORMA_COBRO_ID_ABONO + @"</FORMA_COBRO_ID_ABONO>
                      <CIDDOCTOPEDIDOCC>" + doc.CIDDOCTOPEDIDOCC + @"</CIDDOCTOPEDIDOCC>
                    </Insertar_PedidosEncabezado>
                  </soap:Body>
                </soap:Envelope>";
            return valor;
        }

        /*
        public static string Insertar_PedidosDetalle(clsMovimientos mov, int IdDocto)
        {
            string valor = @"<?xml version=""1.0"" encoding=""utf-8""?>
                <soap:Envelope xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" 
                    xmlns:soap=""http://schemas.xmlsoap.org/soap/envelope/"">         
                   <soap:Body>
                      <Insertar_PedidosDetalle xmlns=""http://tempuri.org/""> 
                         <DOCTO_ID_PEDIDO>"+IdDocto+@"</DOCTO_ID_PEDIDO>
                         <CLAVE_ART>"+mov.CLAVE_ART+@"</CLAVE_ART>
                         <ARTICULO_ID>"+mov.ARTICULO_ID+@"</ARTICULO_ID>
                         <unidades>"+mov.unidades+@"</unidades>
                         <precio>"+mov.precio+@"</precio>
                         <TOTAL>"+mov.TOTAL+@"</TOTAL>
                         <POSICION>"+mov.POSICION+@"</POSICION>
                         <TIPO_DOCUMENTO>"+mov.TIPO_DOCUMENTO+@"</TIPO_DOCUMENTO>
                         <NOMBREU>"+mov.NOMBREU+@"</NOMBREU>
                         <FACTURA>"+mov.FACTURA+@"</FACTURA>
                         <DESCUENTO>"+mov.DESCUENTO+@"</DESCUENTO>
                         <OBSERVACIONES>"+mov.OBSERVACIONES+@"</OBSERVACIONES>
                         <IDDEV>"+mov.IDDEV+@"</IDDEV>
                         <COMENTARIO> "+mov.COMENTARIO+@"</COMENTARIO>
                         <USUARIO_ID>"+mov.USUARIO_ID+@"</USUARIO_ID>
                       </Insertar_PedidosDetalle>
                     </soap:Body>
                    </soap:Envelope>";
            return valor;
        }
        */
        public static string CARGAPEDIDODETALLE(int id)
        {
            string Valor = @"<?xml version=""1.0"" encoding=""utf-8""?>
            <soap:Envelope xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns:soap=""http://schemas.xmlsoap.org/soap/envelope/"">
                <soap:Body>
                    <FCARGAPEDIDODETALLE xmlns=""http://tempuri.org/"">
                        <DOCTO_ID>" + id + @"</DOCTO_ID>
                    </FCARGAPEDIDODETALLE>
                </soap:Body>
            </soap:Envelope>";

            return Valor;
        }

        public static String DTBANCOS()
        {
            string soap =
           @"<?xml version=""1.0"" encoding=""utf-8""?>
            <soap:Envelope xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" 
            xmlns:soap=""http://schemas.xmlsoap.org/soap/envelope/"">
                <soap:Body>
                    <DTBANCOS xmlns = ""http://tempuri.org/"" />
                </soap:Body>
            </soap:Envelope>";
            return soap;
        }
        public static String DTHISTORIALVENTA(String Ruta, int Id)
        {
            string soap =
            @"<?xml version=""1.0"" encoding=""utf-8""?>
            <soap:Envelope xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns:soap=""http://schemas.xmlsoap.org/soap/envelope/"">
                <soap:Body>
                    <DTHISTORIALVENTA xmlns=""http://tempuri.org/"">
                       <ruta>" + Ruta + @"</ruta>
                       <ListaIdClientes>" + Id + @"</ListaIdClientes>
                    </DTHISTORIALVENTA>
                </soap:Body>
            </soap:Envelope>";
            return soap;
        }

        public static String DTPRECIOSEMPRESA()
        {
            string soap =
             @"<?xml version=""1.0"" encoding=""utf-8""?>
            <soap:Envelope xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" 
                xmlns:soap=""http://schemas.xmlsoap.org/soap/envelope/"">
              <soap:Body>
                <DTPRECIOSEMPRESA xmlns = ""http://tempuri.org/""/>
              </soap:Body>
            </soap:Envelope>";
            return soap;
        }

        public static String DTFORMASPAGOVENTAS()
        {
            string soap =
            @"<?xml version=""1.0"" encoding=""utf-8""?>
            <soap:Envelope xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" 
            xmlns:soap=""http://schemas.xmlsoap.org/soap/envelope/"">
                <soap:Body>
                    <DTFORMASPAGOVENTAS xmlns = ""http://tempuri.org/"" />
                </soap:Body>
            </soap:Envelope>";
            return soap;
        }
        public static String DTCIUDADES(string Ruta)
        {
            string soap =
            @"<?xml version=""1.0"" encoding=""utf-8""?>
            <soap:Envelope xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" 
            xmlns:soap=""http://schemas.xmlsoap.org/soap/envelope/"">
              <soap:Body>
                <DTCIUDADES xmlns = ""http://tempuri.org/"" >
                  <ruta>" + Ruta + @"</ruta>
                </DTCIUDADES >
              </soap:Body>
            </soap:Envelope>";
            return soap;
        }
        public static String DTBENEFICIARIO()
        {
            string soap =
             @"<?xml version=""1.0"" encoding=""utf-8""?>
            <soap:Envelope xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" 
            xmlns:soap=""http://schemas.xmlsoap.org/soap/envelope/"">
              <soap:Body>
                <DTBENEFICIARIO xmlns = ""http://tempuri.org/""/>
              </soap:Body>
            </soap:Envelope>";
            return soap;
        }

        public static String DTCREDITOS(string Ruta)
        {
            string soap =
             @"<?xml version=""1.0"" encoding=""utf-8""?>
            <soap:Envelope xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" 
            xmlns:soap=""http://schemas.xmlsoap.org/soap/envelope/"">
              <soap:Body>
                <DTCREDITOS xmlns = ""http://tempuri.org/"">
                  <ruta>" + Ruta + @"</ruta>
                </DTCREDITOS >
              </soap:Body>
            </soap:Envelope>";
            return soap;
        }

        public static String DTLINEAS(string Ruta)
        {
            string soap =
           @"<?xml version=""1.0"" encoding=""utf-8""?>
            <soap:Envelope xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" 
            xmlns:soap=""http://schemas.xmlsoap.org/soap/envelope/"">
              <soap:Body>
                <DTLINEAS xmlns=""http://tempuri.org/"">
                  <RUTA>" + Ruta + @"</RUTA>
                </DTLINEAS>
              </soap:Body>
            </soap:Envelope>";
            return soap;
        }
        public static String DTCATVISITAS()
        {
            string soap =
           @"<?xml version=""1.0"" encoding=""utf-8""?>
            <soap:Envelope xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" 
            xmlns:soap=""http://schemas.xmlsoap.org/soap/envelope/"">
              <soap:Body>
                <DTCATVISITAS xmlns = ""http://tempuri.org/""/>
              </soap:Body>
            </soap:Envelope>";
            return soap;
        }

        //Codigo para cambiar el estado de los documentos en la tabla del SQL para procesar los documentos
        public static String AplicaDoctoVenta(int id)
        {
            string valor = @"<?xml version=""1.0"" encoding=""utf-8""?>
            <soap:Envelope xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns:soap=""http://schemas.xmlsoap.org/soap/envelope/"">
              <soap:Body>
                <APLICA_DOCTO_VENTA xmlns=""http://tempuri.org/"">
                  <DOCTO_ID_PEDIDO>" + id + @"</DOCTO_ID_PEDIDO>
                </APLICA_DOCTO_VENTA>
              </soap:Body>
            </soap:Envelope>";
            return valor;
        }






        //Validación de Licencia

        //public static string activarLicencia(string codigo, string activacion)
        //{
        //    String Valor = @"<?xml version=""1.0"" encoding=""utf-8""?>
        //    <soap:Envelope xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" 
        //      xmlns:soap=""http://schemas.xmlsoap.org/soap/envelope/"">
        //      <soap:Body>
        //        <activarLicencia xmlns = ""http://tempuri.org/"">
        //          <codigo>"+codigo+@"</codigo>
        //          <synckey>"+activacion+@"</synckey>
        //        </activarLicencia>
        //      </soap:Body>
        //    </soap:Envelope>";
        //    return Valor;
        //}



    }
}
