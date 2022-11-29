using SyncTPV.Controllers;
using SyncTPV.Helpers.SqliteDatabaseHelper;
using SyncTPV.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Globalization;
using System.Linq;
//Librerias a usar para poder imprimir el ticket
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SyncTPV
{
    public class clsTicket
    {
        const string ESC = "\u001B";
        const string BoldOn = ESC + "E" + "\u0001";
        const string BoldOff = ESC + "E" + "\0";
        StringBuilder linea = new StringBuilder();
        int maxCaracteres = 40, cortar, Error = 0;
        string query = "", MensajeError = "";
        DateTime TicketCorteFechaHora = new DateTime(1899,1,1);
        DateTime TicketIngresoFechaHora = new DateTime(1899,1,1);

        //Metodod para dibujar una linea de guón en el ticket
        public string LineasGuion()
        {
            string lineasGuion = "";
            for (int i = 0; i < maxCaracteres; i++)
            {
                lineasGuion += "_";
            }
            return linea.AppendLine(lineasGuion).ToString();
        }

        public async Task EncabezadoVenta(int valor, bool permissionPrepedido)
        { 
            if (valor == 1)
            {
                if (permissionPrepedido)
                {
                    linea.AppendLine(await sustituirAcentos("#  U  Descripcion       Precio  Desc.   Total"));
                } else
                {
                    linea.AppendLine(await sustituirAcentos("#  U  Descripcion       Precio  Desc.   Total"));
                }
            }
            else
                linea.AppendLine("Nombre              Cant    $   Subtotal");

        }
        //Creamos un metodo para el texto a la izquierda
        public void TextoIzquierda(string Texto)
        {
            //Realizare el proceso para cortar las cadenas de caracteres y bajarlas en otra linea
            if (Texto.Length > maxCaracteres)
            {
                int caracterActual = 0;
                for (int longitudTexto = Texto.Length; longitudTexto > maxCaracteres; longitudTexto -= maxCaracteres)
                {
                    linea.AppendLine(Texto.Substring(caracterActual, maxCaracteres));
                }
                //AGregamos el fragmento restante
                linea.AppendLine(Texto.Substring(caracterActual, Texto.Length - caracterActual));
            }
            else
            {
                //Si no esmayor solo agregarlo
                linea.AppendLine(Texto);
            }
        }
        public void TextoDerecha(string Texto)
        {
            //Realizare el proceso para cortar las cadenas de caracteres y bajarlas en otra linea
            if (Texto.Length > maxCaracteres)
            {
                int caracterActual = 0;
                for (int longitudTexto = Texto.Length; longitudTexto > maxCaracteres; longitudTexto -= maxCaracteres)
                {
                    linea.AppendLine(Texto.Substring(caracterActual, maxCaracteres));
                    caracterActual += maxCaracteres;
                }
                //variable para poner espacios
                string espacios = "";

                //Obtenemos la longitud del texto restante
                for (int i = 0; i < (maxCaracteres - Texto.Substring(caracterActual, Texto.Length - caracterActual).Length); i++)
                {
                    espacios += " ";
                }
                //AGregamos el fragmento restante
                linea.AppendLine(espacios + Texto.Substring(caracterActual, Texto.Length - caracterActual));
            }
            else
            {
                string espacios = "";

                //Obtenemos la longitud del texto restante
                for (int i = 0; i < (maxCaracteres - Texto.Length); i++)
                {
                    espacios += " ";
                }
                //Si no esmayor solo agregarlo
                linea.AppendLine(espacios + Texto);
            }
        }
        public void TextoCentro(string TextoCentro)
        {
            List<string> ListaTexto = new List<string>();
            if (TextoCentro != "")
                ListaTexto = TextoCentro.Split(new[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries).ToList();
            else
                ListaTexto.Add(TextoCentro);
            foreach (var Texto in ListaTexto)
            {
                if (Texto.Length > maxCaracteres)
                {
                    int caracterActual = 0;
                    for (int longitudTexto = Texto.Length; longitudTexto > maxCaracteres; longitudTexto -= maxCaracteres)
                    {
                        linea.AppendLine(Texto.Substring(caracterActual, maxCaracteres));
                        caracterActual += maxCaracteres;
                    }
                    //variable para poner espacios
                    string espacios = "";
                    //Sacamos la cantidad de espacion en blanco y lo dividimos en 2
                    int centrar = (maxCaracteres - Texto.Substring(caracterActual, Texto.Length - caracterActual).Length) / 2;
                    //Obtenemos la longitud del texto restante
                    for (int i = 0; i < centrar; i++)
                    {
                        espacios += " ";
                    }
                    //AGregamos el fragmento restante
                    linea.AppendLine(espacios + Texto.Substring(caracterActual, Texto.Length - caracterActual));
                }
                else
                {
                    string espacios = "";
                    //Sacamos la cantidad de espacion en blanco y lo dividimos en 2
                    int centrar = (maxCaracteres - Texto.Length) / 2;
                    //Obtenemos la longitud del texto restante
                    for (int i = 0; i < centrar; i++)
                    {
                        espacios += " ";
                    }
                    //AGregamos el fragmento restante
                    linea.AppendLine(espacios + BoldOn + Texto + BoldOff);
                }
            }
        }

        public void TextoExtremos(string textoIzquierdo, String textoDerecho)
        {
            string textoIzq, textDer, textoCompleto = "", espacios = "";

            //Si el texto que va a utilizar es mayor a 18, cortamos el texto
            if (textoIzquierdo.Length > 18)
            {
                cortar = textoIzquierdo.Length - 18;
                textoIzq = textoIzquierdo.Remove(18, cortar);
            }
            else
            {
                textoIzq = textoIzquierdo;
            }
            textoCompleto = textoIzq;

            if (textoDerecho.Length > 20)//Si es mayor a 20 lo cortamos
            {
                cortar = textoDerecho.Length - 20;
                textDer = textoDerecho.Remove(20, cortar);
            }
            else
            {
                textDer = textoDerecho;
            }

            //obtenemos el numero de espacios restantes para poner texto a la derecha final 
            int nroEspacios = maxCaracteres - (textoIzq.Length + textDer.Length);
            for (int i = 0; i < nroEspacios; i++)
            {
                espacios += " "; //Agrega los espacios para poner texto derecho al final
            }
            textoCompleto += espacios + textoDerecho;//Agregamos el segundo texto con los espacios para alinearlo a la derecha 
            linea.AppendLine(textoCompleto); //Agregamos la linea al ticket en si
        }

        public void AgregarTotales(string texto, decimal total)
        {
            //Variables que usaremos 
            string resumen, valor, textoCompleto, espacios = "";
            if (texto.Length > 25)//Si es mayor a 25 lo cortamos 
            {
                cortar = texto.Length - 25;
                resumen = texto.Remove(25, cortar);
            }
            else
            {
                resumen = texto;
            }
            textoCompleto = resumen;
            valor = total.ToString("#,#.00");//Agregamos el total previo formateo

            //Obtenemos el numero de espacios restantes para alinearlos  a la derecha 
            int nroEspacios = maxCaracteres - (resumen.Length + valor.Length);
            //Agregamos los espacios
            for (int i = 0; i < nroEspacios; i++)
            {
                espacios = " ";
            }
            textoCompleto += espacios + valor;
            linea.AppendLine(textoCompleto);
        }

        //Método para agregar Articulos al ticket
        public void AgregaArticulo(double unidadesCapturadas, String capturedUnitName, String itemCode, string itemName, string precio, String descuento, string total)
        {
            linea.AppendLine(unidadesCapturadas.ToString() + " " + capturedUnitName + " " + itemName);
            linea.AppendLine(itemCode + "    " + precio + "  " + descuento + "  " + total);
        }


        //Método para agregar Articulos al ticket
        public void AgregaGranTotalArticulos(string textoFinal, string importe)
        {
            string articulo = "";
            string cant = " ";
            int aux = cant.Length;
            int aux2 = textoFinal.Length;
            int aux3 = importe.Length;
            if (cant.ToString().Length <= 5 && textoFinal.ToString().Length <= 10 && importe.ToString().Length <= 9)//22
            {
                string elemento = "", espacios = "";
                bool bandera = false;//Para validar si el nombre del articulo no es mas grande que los 40 caracteres
                int nroEspacios = 0;

                //SI el nombre o descripcion del articulo es mayor a 20, bajar a la sigiente linea
                if (articulo.Length > 18)
                {
                    //Colocar la cantidad a la derecha
                    nroEspacios = (5 - cant.Length);
                    espacios = "";
                    for (int i = 0; i < nroEspacios; i++)
                    {
                        espacios += " ";//Generamos los espacios necearios para alinear a la derecha
                    }
                    elemento += espacios + cant;

                    //Colocar el precio a la derecha
                    nroEspacios = (10 - textoFinal.ToString().Length);
                    espacios = "";
                    for (int i = 0; i < nroEspacios; i++)
                    {
                        espacios += " ";
                    }

                    elemento += espacios + textoFinal.ToString(); //Agregamos el precio a la variable elemento

                    //Colocar el importe a la derecha
                    nroEspacios = (9 - importe.ToString().Length);
                    espacios = "";
                    for (int i = 0; i < nroEspacios; i++)
                    {
                        espacios += " ";
                    }
                    elemento += espacios + importe.ToString(); //Agregamos el importe alineado a la derecha

                    int caracterActual = 0;
                    for (int longitudTexto = articulo.Length; longitudTexto > 18; longitudTexto = -18)
                    {
                        if (bandera == false)
                        {
                            linea.AppendLine(articulo.Substring(caracterActual, 18) + elemento);
                            bandera = true;
                        }
                        else
                        {
                            linea.AppendLine(articulo.Substring(caracterActual, 18));//Solo agrego el nombre del articulo
                        }
                        caracterActual += 18; //Incrementa en 20 el valor de la variable caracterActual
                    }
                    //Agrega el resto del fragmento del nombre del articulo
                    linea.AppendLine(articulo.Substring(caracterActual, articulo.Length - caracterActual));
                }
                else  //si no es mayor solo agregarlo, sin dar saltos de linea
                {
                    for (int i = 0; i < (18 - articulo.Length); i++)
                    {
                        espacios += " ";//Agrega espacios para alinear a la derecha
                    }
                    elemento = articulo + espacios;

                    //Colocar la cantidad a  al derecha
                    nroEspacios = (3 - cant.ToString().Length);
                    espacios = "";
                    for (int i = 0; i < nroEspacios; i++)
                    {
                        espacios += " ";
                    }
                    elemento += espacios + cant.ToString();

                    //Colocar el precio a la derecha
                    nroEspacios = (8 - textoFinal.ToString().Length);
                    espacios = "";
                    for (int i = 0; i < nroEspacios; i++)
                    {
                        espacios += " ";
                    }
                    elemento += espacios + textoFinal.ToString();

                    //COlocar el importe a al derecha
                    nroEspacios = (9 - importe.ToString().Length);
                    espacios = "";
                    for (int i = 0; i < nroEspacios; i++)
                    {
                        espacios += " ";
                    }
                    elemento += espacios + importe.ToString();

                    linea.AppendLine(elemento);//Agregamos todo el elemento
                }
            }
            else
            {
                linea.AppendLine("Los Valores ingresados para esta fila");
                linea.AppendLine("Superan las columnas soportadas por esta");
                throw new Exception("Los valores ingresados para algunas filas del ticket\n superan las columnas ");
            }
        }

        public void AgregaArqueo(string nombre, string montoCompleto, string montoCorteCaja, string Arqueo)
        {
            if (montoCompleto.ToString().Length <= 10 && montoCorteCaja.ToString().Length <= 10 && Arqueo.ToString().Length <= 9)//29
            {
                string elemento = "", espacios = "";
                bool bandera = false;//Para validar si el nombre del articulo no es mas grande que los 40 caracteres
                int nroEspacios = 0;

                //SI el nombre o descripcion del articulo es mayor a 20, bajar a la sigiente linea
                if (nombre.Length > 11)
                {
                    //Colocar la cantidad a la derecha
                    nroEspacios = (10 - montoCompleto.ToString().Length);
                    espacios = "";
                    for (int i = 0; i < nroEspacios; i++)
                    {
                        espacios += " ";//Generamos los espacios necearios para alinear a la derecha
                    }
                    elemento += espacios + montoCompleto.ToString();

                    //Colocar el precio a la derecha
                    nroEspacios = (10 - montoCorteCaja.ToString().Length);
                    espacios = "";
                    for (int i = 0; i < nroEspacios; i++)
                    {
                        espacios += " ";
                    }

                    elemento += espacios + montoCorteCaja.ToString(); //Agregamos el precio a la variable elemento

                    //Colocar el importe a la derecha
                    nroEspacios = (9 - Arqueo.ToString().Length);
                    espacios = "";
                    for (int i = 0; i < nroEspacios; i++)
                    {
                        espacios += " ";
                    }
                    elemento += espacios + Arqueo.ToString(); //Agregamos el importe alineado a la derecha

                    int caracterActual = 0;
                    for (int longitudTexto = nombre.Length; longitudTexto > 11; longitudTexto = -11)
                    {
                        if (bandera == false)
                        {
                            linea.AppendLine(nombre.Substring(caracterActual, 11) + elemento);
                            bandera = true;
                        }
                        else
                        {
                            linea.AppendLine(nombre.Substring(caracterActual, 11));//Solo agrego el nombre del articulo
                        }
                        caracterActual += 11; //Incrementa en 20 el valor de la variable caracterActual
                    }
                    //Agrega el resto del fragmento del nombre del articulo
                    linea.AppendLine(nombre.Substring(caracterActual, nombre.Length - caracterActual));
                }
                else  //si no es mayor solo agregarlo, sin dar saltos de linea
                {
                    for (int i = 0; i < (11 - nombre.Length); i++)
                    {
                        espacios += " ";//Agrega espacios para alinear a la derecha
                    }
                    elemento = nombre + espacios;

                    //Colocar la cantidad a  al derecha
                    nroEspacios = (10 - montoCompleto.ToString().Length);
                    espacios = "";
                    for (int i = 0; i < nroEspacios; i++)
                    {
                        espacios += " ";
                    }
                    elemento += espacios + montoCompleto.ToString();

                    //Colocar el precio a la derecha
                    nroEspacios = (10 - montoCorteCaja.ToString().Length);
                    espacios = "";
                    for (int i = 0; i < nroEspacios; i++)
                    {
                        espacios += " ";
                    }
                    elemento += espacios + montoCorteCaja.ToString();

                    //COlocar el importe a al derecha
                    nroEspacios = (9 - Arqueo.ToString().Length);
                    espacios = "";
                    for (int i = 0; i < nroEspacios; i++)
                    {
                        espacios += " ";
                    }
                    elemento += espacios + Arqueo.ToString();

                    linea.AppendLine(elemento);//Agregamos todo el elemento
                }
            }
            else
            {
                linea.AppendLine("Los Valores ingresados para esta fila");
                linea.AppendLine("Superan las columnas soportadas por esta");
                throw new Exception("Los valores ingresados para algunas filas del ticket\n superan las columnas ");
            }
        }


        //Metodos para enviar secuencias de escape a la impresora
        //para cortar el ticket
        public void cutTicket()
        {
            linea.AppendLine("\x1B" + "m"); //Caracteres de corte. Estos comandos varia según el tipo de impresora
            linea.AppendLine("\x1B" + "d" + "\x01");//Avanza 9 renglones. Tambien varia segun el tipo de impresora
        }

        public void abrirCajonDinero()
        {
            //<70><30><14><14>
            //< 1B > p0 < 1414 >
            //Estos tambien varian, tienen que ver el manual de la impresora para poner los correctos
            //linea.AppendLine("\x1B" + "p" + "\x00" + "x0F" + "\x96");//Caracteres de apertura cajon 0            
            linea.AppendLine("\x1B" + "p0" + "\x1414");//Caracteres de apertura cajon 0          <1B>p0<1414>  
                                                       //linea.AppendLine("\x1B" + "p" + "\x00" + "x0F" + "\x96");//Caracteres de apertura cajon 1
                                                       //Para mandar a imprimir el texto a la impresora que le indicamos 
        }

        public int printTicket(string tipoTicketTPV = "", string claveTicketTPV = "")
        {
            int response = 0;
            try
            {
                string printer = "";
                PrinterModel pm = PrinterModel.getallDataFromAPrinter();
                if (pm != null)
                {
                    printer = pm.nombre;
                    //Este método recibe el nombre de la impresora a la cual se mandara a imprimir y el texto se imprime
                    //Usaremos un código que nos proporciona microsotf https://support.microsoft.com/es-es/kb/322091
                    string auxLinea = linea.ToString();
                    if (printer == "Microsoft XPS Document Writer")
                    {
                        //string encriptado = AdminDll.BajoNivel.Encriptar(auxLinea);
                        //string desencriptado = AdminDll.BajoNivel.DesEncriptarAES(encriptado);
                        //if (auxLinea == desencriptado)
                        //{
                        //    string GuardarEnDbEncriptado = "se puede obtener de la BD y así poder reimprimir los tickets...";
                        //}
                        auxLinea= auxLinea.Replace("\u001bE\u0001", "").Replace("\u001bE\0", "").Replace("\u001bm", "").Replace("\u001bd\u0001", "").Replace("\u001bp0ᐔ", "");
                    }
                    RawPrinterHelper.SendStringToPrinter(printer, auxLinea);//Imprime texto
                    if(tipoTicketTPV == "")
                    {
                        claveTicketTPV = "CLAVE-" + DateTime.Now.ToString("yyyyMMdd-HHmmss");
                    }
                    else
                    {
                        if (claveTicketTPV == "")
                        {
                            claveTicketTPV = tipoTicketTPV + "-" + DateTime.Now.ToString("yyyyMMdd-HHmmss");
                        }
                        else
                        {
                            claveTicketTPV = tipoTicketTPV + "-" + claveTicketTPV;
                        }
                    }
                    //Imprime Ticket de respaldo en TPV.
                    SECUDOC.TicketTPV(auxLinea, claveTicketTPV);
                    linea.Clear();//Al acabar de imprimir la impresora limpia la linea del texto agregado 
                    response = 1;
                }
            }
            catch (Exception Ex)
            {
                SECUDOC.writeLog(Ex.ToString());
                MensajeError = Ex.Message;
                response = -1;
            }
            return response;
        }

        //Método para imprimir el reporte de corte caja del usuario
        public async Task CorteCaja(int retiroId, bool serverModeLAN)
        {
            Double total = 0;
            string fecha = "", fechaHora;
            String date = RetiroModel.getDateFromAWithdrawal(retiroId);
            if (date == null || date.Equals(""))
                fechaHora = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt");
            else
                fechaHora = date;

            await Encabezado("Corte de Caja", "Corte de Caja", fechaHora, retiroId, "", "", 0, false, serverModeLAN, 0);
            List<MontoRetiroModel> montosList = MontoRetiroModel.getAllAmountsFromAWithdrawal(retiroId);
            if (montosList != null)
            {
                foreach (var item in montosList)
                {
                    double montoAretirar = MontoRetiroModel.getTotalWithdrawnFromAPaymentMethod(item.formaCobroId, retiroId);
                    String nombreFc = FormasDeCobroModel.getAStringValueByFormaDeCobro("SELECT " + LocalDatabase.CAMPO_NOMBRE_FORMASCOBRO + " FROM " + LocalDatabase.TABLA_FORMASCOBRO +
                        " WHERE " + LocalDatabase.CAMPO_ID_FORMASCOBRO + " = " + item.formaCobroId);
                    TextoExtremos(MetodosGenerales.quitarAsentos(nombreFc), "$" + ConvertirCantidades(montoAretirar.ToString())+" MXN");
                    total += montoAretirar;
                    fecha = date;
                }
            }
            TextoCentro("---------------------");
            TextoDerecha("Total Retirado: " + total.ToString("C", CultureInfo.CurrentCulture) + " MXN");
            TextoCentro("");
            TextoCentro("");
            TextoCentro("Firma Cajero");
            TextoCentro("");
            TextoCentro("_________________");
            TextoCentro("");
            TextoCentro("");
            TextoCentro("");
            TextoCentro("");
            cutTicket();
            string codigoCaja = UserModel.getCodeBox(ClsRegeditController.getIdUserInTurn());
            string codigoCajero = UserModel.getCode(ClsRegeditController.getIdUserInTurn());
            //printTicket("RETIRO", "CLAVERETIRO");
            printTicket(tipoTicketTPV: "RETIRO", claveTicketTPV: codigoCaja + "-" + codigoCajero + "-" + fechaHora.Replace("-", "").Replace(":", "").Replace(" ", "-"));
        }

        public async Task imprimirTicketCobranza(int repIdLocal, bool permissionPrepedido, bool serverModeLAN)
        {
            int idPago = CuentasXCobrarModel.getIntValue("SELECT "+LocalDatabase.CAMPO_DOCTO_CC_ID_CXC+" FROM "+
                LocalDatabase.TABLA_CXC+" WHERE "+LocalDatabase.CAMPO_ID_CXC+" = "+repIdLocal);
            await Encabezado("Pago del Cliente", "Pago del Cliente", "", 0, "", "", 0, permissionPrepedido, serverModeLAN,0);
            double totalPagado = 0;
            CuentasXCobrarModel cxcm = CuentasXCobrarModel.getPagoDelCliente(idPago);
            if (cxcm != null)
            {
                TextoDerecha("Saldo Anterior: " + cxcm.saldo_actual.ToString("C", CultureInfo.CurrentCulture) + " MXN");
                TextoDerecha(FormasDeCobroModel.getANameFrromAFomaDeCobroWithId(cxcm.formOfPaymentIdCredit)+": " +
                    cxcm.amount.ToString("C") + " MXN");
                totalPagado += cxcm.amount;
                TextoDerecha("Saldo Pendiente: " + (cxcm.saldo_actual - cxcm.amount).ToString("C", CultureInfo.CurrentCulture) + " MXN");
            }
            TextoCentro("---------------------");
            TextoDerecha("Total Pagado: " + totalPagado.ToString("C", CultureInfo.CurrentCulture) + " MXN");
            TextoCentro("");
            TextoCentro("");
            DatosTicketModel dtm = DatosTicketModel.getAllData();
            if (dtm != null)
            {
                if (dtm.PIE_TICKETCOBRANZA != null)
                    TextoCentro(await sustituirAcentos(dtm.PIE_TICKETCOBRANZA));
                else TextoCentro("Gracias por su Pago");
            } else
            {
                dynamic responseDtm = null;
                if (serverModeLAN)
                    responseDtm = await DatosTicketController.downloadAllDatosTicketLAN();
                else responseDtm = await DatosTicketController.downloadAllDatosTicketAPI();
                if (responseDtm.value == 1)
                {
                    dtm = DatosTicketModel.getAllData();
                    if (dtm.PIE_TICKETCOBRANZA != null)
                        TextoCentro(await sustituirAcentos(dtm.PIE_TICKETCOBRANZA));
                    else TextoCentro("Gracias por su Pago");
                }
            }
            TextoCentro("");
            TextoCentro("");
            TextoCentro("");
            TextoCentro("");
            cutTicket();
            printTicket("COBRANZA");
            abrirCajonDinero();
        }

        public async Task IngresoACaja(int idIngreso, bool serverModeLAN)
        {
            Double total = 0;
            string fecha = "", FechaActual;
            String date = IngresoModel.getDateFromAnEntry(idIngreso);
            if (date == null || date.Equals(""))
                FechaActual = MetodosGenerales.getCurrentDateAndHour();
            else
                FechaActual = date;
            await Encabezado("Ingreso a Caja", "Ingreso a Caja", FechaActual, idIngreso, "", "", 0, false, serverModeLAN, 0);
            List<MontoIngresoModel> montosList = MontoIngresoModel.getAllAmountsFromAnEntry(idIngreso);
            if (montosList != null)
            {
                foreach (var item in montosList)
                {
                    double montoAIngresar = MontoIngresoModel.getTotalEntryFromAPaymentMethod(item.formaCobroId, idIngreso);
                    String nombreFc = FormasDeCobroModel.getAStringValueByFormaDeCobro("SELECT " + LocalDatabase.CAMPO_NOMBRE_FORMASCOBRO + 
                        " FROM " + LocalDatabase.TABLA_FORMASCOBRO +
                        " WHERE " + LocalDatabase.CAMPO_ID_FORMASCOBRO + " = " + item.formaCobroId);
                    TextoExtremos(await sustituirAcentos(nombreFc), montoAIngresar.ToString("C", CultureInfo.CurrentCulture) + " MXN");
                    total += montoAIngresar;
                    fecha = date;
                }
            }
            TextoCentro("---------------------");
            TextoDerecha("Total Ingresado: " + total.ToString("C", CultureInfo.CurrentCulture) + " MXN");
            TextoCentro("");
            TextoCentro("");
            TextoCentro("");
            TextoCentro("Firma Cajero");
            TextoCentro("");
            TextoCentro("_________________");
            TextoCentro("");
            TextoCentro("");
            TextoCentro("");
            TextoCentro("");
            cutTicket();
            abrirCajonDinero();
            string codigoCaja = UserModel.getCodeBox(ClsRegeditController.getIdUserInTurn());
            string codigoCajero = UserModel.getCode(ClsRegeditController.getIdUserInTurn());
            printTicket(tipoTicketTPV: "INGRESO", claveTicketTPV: codigoCaja + "-" + codigoCajero + "-" + TicketIngresoFechaHora.ToString("yyyyMMdd-HHmmss"));
        }

        public async Task<int> CrearTicket(int idDocument, Boolean openCashDrawer, bool permissionPrepedido, int documentType, String originalCopia,
            bool serverModeLAN)
        {
            int response = 0;
            await Task.Run(async () => {
                List<MovimientosModel> movesList = MovimientosModel.getAllMovementsFromADocument(idDocument);
                if (movesList != null)
                {
                    DatosTicketModel dtm = DatosTicketModel.getAllData();
                    if (dtm == null)
                    {
                        dynamic responseDtm = null;
                        if (serverModeLAN)
                            responseDtm = await DatosTicketController.downloadAllDatosTicketLAN();
                        else responseDtm = await DatosTicketController.downloadAllDatosTicketAPI();
                        if (responseDtm.value == 1)
                        {
                            dtm = DatosTicketModel.getAllData();
                        }
                    }
                    String nameDocumentType = "";
                    String pieTicket = "";
                    if (documentType == 1)
                    {
                        nameDocumentType = "Cotización";
                        if (dtm != null)
                            pieTicket = dtm.PIE_TICKETCOTIZACION;
                    }
                    else if (documentType == 2)
                    {
                        nameDocumentType = "Venta";
                        if (dtm != null)
                            pieTicket = dtm.PIE_TICKETVENTAS;
                    }
                    else if (documentType == 3)
                    {
                        nameDocumentType = "Pedido";
                        if (dtm != null)
                            pieTicket = dtm.PIE_TICKETPEDIDO;
                    }
                    else if (documentType == 4)
                    {
                        nameDocumentType = "Venta";
                        if (dtm != null)
                            pieTicket = dtm.PIE_TICKETVENTAS;
                    }
                    else if (documentType == 5)
                    {
                        nameDocumentType = "Devolución";
                        if (dtm != null)
                            pieTicket = dtm.PIE_TICKETDEVOLUCION;
                    }
                    else if (documentType == 51)
                    {
                        nameDocumentType = "Cotización de Mostrador";
                        if (dtm != null)
                            pieTicket = dtm.PIE_TICKETCOTIZACION;
                    }
                    if (permissionPrepedido)
                    {
                        if (documentType == DocumentModel.TIPO_PREPEDIDO)
                            nameDocumentType = "Pedido";
                        else nameDocumentType = "Entrega";
                    }
                    String folio = DocumentModel.getFolioVentaForADocument(idDocument);
                    String customerName = "";
                    if (serverModeLAN)
                    {
                        dynamic responseCustomerName = await CustomersController.getNameLAN(DocumentModel.getCustomerIdOfADocument(idDocument));
                        if (responseCustomerName.value == 1)
                            customerName = responseCustomerName.name;
                    } else
                    {
                        customerName = CustomerModel.getName(DocumentModel.getCustomerIdOfADocument(idDocument));
                    }
                    String fechaDocumento = DocumentModel.getFechaHora(idDocument);
                    await Encabezado("Ticket Documento", nameDocumentType, fechaDocumento, 0, folio, customerName, idDocument, permissionPrepedido, serverModeLAN,
                        documentType);
                    await EncabezadoVenta(1, permissionPrepedido);
                    //Articulos
                    double totalDescuentoSinIVA = 0;
                    double totalDocumento = 0;
                    double subtotal = 0;
                    double ahorroTotal = 0;
                    int totalDeMovimientos = 0;
                    double totalDeArticulos = 0;
                    double netoSinImpuesto = 0;
                    double ivaTotal = 0;
                    foreach (var movimiento in movesList)
                    {
                        String capturedUnitName = "";
                        if (serverModeLAN)
                        {
                            dynamic responseUnit = await UnitsOfMeasureAndWeightController.getNameOfAndUnitMeasureWeightLAN(movimiento.capturedUnitId);
                            if (responseUnit.value == 1)
                                capturedUnitName = responseUnit.name;
                        } else
                        {
                            dynamic responseUnit = await UnitsOfMeasureAndWeightController.getNameOfAndUnitMeasureWeightAPI(movimiento.capturedUnitId);
                            if (responseUnit.value == 1)
                                capturedUnitName = responseUnit.name;
                            else capturedUnitName = UnitsOfMeasureAndWeightModel.getNameOfAndUnitMeasureWeight(movimiento.capturedUnitId);
                        }
                        String itemName = "";
                        if (serverModeLAN)
                        {
                            dynamic responseItem = await ItemsController.getItemNameLAN(movimiento.itemId);
                            if (responseItem.value == 1)
                                itemName = responseItem.name;
                        } else itemName = ItemModel.getTheNameOfAnItem(movimiento.itemId);
                        if (!permissionPrepedido)
                        {
                            PrinterModel pm = PrinterModel.getallDataFromAPrinter();
                            if (pm != null)
                            {
                                if (pm.showPorcentajeDescuentoMovimiento == 1)
                                {
                                    AgregaArticulo(Convert.ToDouble(movimiento.capturedUnits), capturedUnitName, movimiento.itemCode,
                                    await sustituirAcentos(itemName),
                                    movimiento.price.ToString("C", CultureInfo.CurrentCulture),
                                    movimiento.descuentoPorcentaje + "% " + movimiento.descuentoImporte.ToString("C", CultureInfo.CurrentCulture),
                                    movimiento.total.ToString("C", CultureInfo.CurrentCulture) + "MXN");
                                }
                                else
                                {
                                    AgregaArticulo(Convert.ToDouble(movimiento.capturedUnits), capturedUnitName, movimiento.itemCode,
                                    await sustituirAcentos(itemName),
                                    movimiento.price.ToString("C", CultureInfo.CurrentCulture),
                                    movimiento.descuentoImporte.ToString("C", CultureInfo.CurrentCulture),
                                    movimiento.total.ToString("C", CultureInfo.CurrentCulture) + "MXN");
                                }

                            }
                            else
                            {
                                AgregaArticulo(Convert.ToDouble(movimiento.capturedUnits), capturedUnitName, movimiento.itemCode,
                                await sustituirAcentos(itemName),
                                movimiento.price.ToString("C", CultureInfo.CurrentCulture),
                                movimiento.descuentoPorcentaje + "% " + movimiento.descuentoImporte.ToString("C", CultureInfo.CurrentCulture),
                                movimiento.total.ToString("C", CultureInfo.CurrentCulture) + "MXN");
                            }



                        }
                        else
                        {
                            if (documentType == DocumentModel.TIPO_PREPEDIDO)
                            {
                                String unitName = "";
                                if (serverModeLAN)
                                {
                                    dynamic responseUnit = await UnitsOfMeasureAndWeightController.getNameOfAndUnitMeasureWeightLAN(movimiento.capturedUnitId);
                                    if (responseUnit.value == 1)
                                        unitName = responseUnit.name;
                                }
                                else
                                {
                                    unitName = UnitsOfMeasureAndWeightModel.getNameOfAndUnitMeasureWeight(movimiento.nonConvertibleUnitId);
                                }
                                AgregaArticulo(Convert.ToDouble(movimiento.nonConvertibleUnits), unitName, "Pendiente de surtir",
                                    await sustituirAcentos(itemName), "",
                                "0 % $ 0.00 MXN",
                                "$ 0.00 MXN");
                            } else
                            {
                                int numeroDeCajas = 0;
                                WeightModel wm = WeightModel.getAWeight(movimiento.id);
                                if (wm != null)
                                {
                                    numeroDeCajas = wm.cajas;
                                }
                                String unitName = "";
                                if (serverModeLAN)
                                {
                                    dynamic responseUnit = await UnitsOfMeasureAndWeightController.getNameOfAndUnitMeasureWeightLAN(movimiento.capturedUnitId);
                                    if (responseUnit.value == 1)
                                        unitName = responseUnit.name;
                                }
                                else
                                {
                                    unitName = UnitsOfMeasureAndWeightModel.getNameOfAndUnitMeasureWeight(movimiento.nonConvertibleUnitId);
                                }
                                AgregaArticulo(Convert.ToDouble(movimiento.nonConvertibleUnits), unitName, "Cajas: " + numeroDeCajas,
                                    await sustituirAcentos(itemName), "",
                                movimiento.descuentoPorcentaje + "% " + movimiento.descuentoImporte.ToString("C", CultureInfo.CurrentCulture),
                                movimiento.total.ToString("C", CultureInfo.CurrentCulture) + "MXN");
                            }
                        }
                        ahorroTotal += movimiento.descuentoImporte;
                        double impuestoItem = 16;
                        if (serverModeLAN)
                        {
                            dynamic responseImp = await ItemsController.getImpuestoLAN(movimiento.itemId, 1);
                            if (responseImp.value == 1)
                                impuestoItem = responseImp.impuesto;
                        } else
                        {
                            impuestoItem = ItemModel.getImpuesto(movimiento.itemId, 1);
                        }
                        double impuestoPorcentaje = (impuestoItem / 100);
                        double precioSinIVA = movimiento.price / (1 + impuestoPorcentaje);
                        totalDescuentoSinIVA += (movimiento.descuentoImporte / (1 + impuestoPorcentaje));
                        double montoSubtotal = ((precioSinIVA * movimiento.capturedUnits) - (movimiento.descuentoImporte / (1 + impuestoPorcentaje)));
                        subtotal += montoSubtotal;
                        ivaTotal += (montoSubtotal * impuestoPorcentaje);
                        netoSinImpuesto += (precioSinIVA * movimiento.capturedUnits);
                        totalDeArticulos += movimiento.capturedUnits;
                        totalDocumento += movimiento.total;
                        totalDeMovimientos++;
                    }
                    double pesoExcedente = 0;
                    if (permissionPrepedido)
                    {
                        if (documentType == DocumentModel.TIPO_PREPEDIDO) {
                           
                        }
                        else
                        {
                            MovimientosModel mm = MovimientosModel.getAMovementFromADocument(idDocument);
                            if (mm != null)
                            {
                                WeightModel wm = WeightModel.getWeightReal(mm.id);
                                if (wm != null)
                                {
                                    TextoIzquierda("Peso Bruto: " + MetodosGenerales.obtieneDosDecimales(wm.pesoBruto) + " KG");
                                    double pesoTara = wm.pesoCaja;
                                    double pesoPolloLesionado = wm.pesoPolloLesionado;
                                    double pesoPolloMuerto = wm.pesoPolloMuerto;
                                    double pesoPolloBajoPeso = wm.pesoPolloBajoDePeso;
                                    double pesoPolloGolpeado = wm.pesoPolloGolpeado;
                                    if (pesoTara != 0)
                                        TextoIzquierda("Peso Tara: " + MetodosGenerales.obtieneDosDecimales(pesoTara) + " KG");
                                    if (pesoPolloLesionado != 0)
                                        TextoIzquierda("Peso Pollo Lesionado: " + MetodosGenerales.obtieneDosDecimales(pesoPolloLesionado) + " KG");
                                    if (pesoPolloMuerto != 0)
                                        TextoIzquierda("Peso Pollo Muerto: " + MetodosGenerales.obtieneDosDecimales(pesoPolloMuerto) + " KG");
                                    if (pesoPolloBajoPeso != 0)
                                        TextoIzquierda("Peso Pollo Muerto: " + MetodosGenerales.obtieneDosDecimales(pesoPolloBajoPeso) + " KG");
                                    if (pesoPolloGolpeado != 0)
                                        TextoIzquierda("Peso Pollo Golpeado: " + MetodosGenerales.obtieneDosDecimales(pesoPolloGolpeado) + " KG");
                                    pesoExcedente = (Math.Abs(pesoTara) + Math.Abs(pesoPolloLesionado) + Math.Abs(pesoPolloMuerto) +
                                    Math.Abs(pesoPolloBajoPeso) + Math.Abs(pesoPolloGolpeado));
                                    double pesoNeto = (wm.pesoBruto - Math.Abs(pesoExcedente));
                                    if (pesoNeto == wm.pesoNeto)
                                        pesoNeto = wm.pesoNeto;
                                    TextoIzquierda("Peso Neto: " + MetodosGenerales.obtieneDosDecimales(pesoNeto) + " KG");
                                }
                            }
                        }
                    }
                    TextoCentro("");
                    await TraerFormasPago(idDocument, subtotal, netoSinImpuesto, totalDescuentoSinIVA, ivaTotal, totalDocumento, documentType);
                    String obervationDocumento = DocumentModel.getDocumentObservation(idDocument);
                    TextoCentro(obervationDocumento);
                    TextoCentro("");
                    TextoIzquierda("Total de Movimientos: " + totalDeMovimientos);
                    if (permissionPrepedido)
                        TextoIzquierda("Total de Pollos: " + totalDeArticulos+" KG");
                    else TextoIzquierda("Total de Articulos: " + totalDeArticulos);
                    if (documentType == DocumentModel.TIPO_PREPEDIDO)
                    {

                    } else
                    {
                        TextoIzquierda("Usted Ahorro: " + ahorroTotal.ToString("C", CultureInfo.CurrentCulture));
                    }
                    TextoCentro("");
                    if (permissionPrepedido)
                    {
                        if (documentType == DocumentModel.TIPO_PREPEDIDO)
                        {

                        } else
                        {
                            bool documentoEnviadoAlCliente = DocumentModel.isItDocumentPrepedidoSendedToTheCustomerAndPendienteDeDestarar(idDocument);
                            if (documentoEnviadoAlCliente)
                            {
                                if (pesoExcedente == 0)
                                {
                                    TextoCentro("Nombre y firma del Cliente");
                                    TextoCentro("");
                                    TextoCentro("");
                                    TextoCentro("__________________________");
                                    TextoCentro("");
                                }
                                else
                                {
                                    TextoCentro("Nombre y firma de quien Destara");
                                    TextoCentro("");
                                    TextoCentro("");
                                    TextoCentro("__________________________");
                                    TextoCentro("");
                                }
                            }
                            else
                            {
                                TextoCentro("Nombre y firma del Cliente");
                                TextoCentro("");
                                TextoCentro("");
                                TextoCentro("__________________________");
                                TextoCentro("");
                            }
                            TextoCentro("Datos de Peso del Cliente");
                            TextoIzquierda("");
                            TextoIzquierda("Peso Bruto:_____________kg");
                            TextoIzquierda("");
                            TextoIzquierda("Peso Taras:_____________kg");
                            TextoIzquierda("");
                            TextoIzquierda("Peso Neto:______________kg");
                            TextoIzquierda("");
                            TextoIzquierda("Peso Pollo Mal:_________kg");
                            TextoCentro("");
                        }
                    }
                    if (!pieTicket.Equals(""))
                    {
                        TextoCentro(pieTicket);
                    }
                    TextoCentro("");
                    if (originalCopia.Equals(""))
                    {
                        TextoCentro("* * * Impresión original * * *");
                    }
                    else
                    {
                        TextoCentro("* * * " + originalCopia + " * * *");
                    }
                    TextoIzquierda(DateTime.Now.ToString());
                    TextoCentro("");
                    TextoCentro("");
                    TextoCentro("");
                    TextoCentro("");
                    cutTicket();
                    if (openCashDrawer)
                        abrirCajonDinero();
                    printTicket(tipoTicketTPV: "VENTA", claveTicketTPV: folio);
                }
            });
            return response;
        }

        public async Task<int> imprimirTicketFactura(int idDocument, Boolean openCashDrawer, bool permissionPrepedido, int idDocumentoServer,
            bool serverModeLAN)
        {
            int response = 0;
            await Task.Run(async () => {
                List<MovimientosModel> movesList = MovimientosModel.getAllMovementsFromADocument(idDocument);
                if (movesList != null)
                {
                    DatosTicketModel dtm = DatosTicketModel.getAllData();
                    if (dtm == null)
                    {
                        dynamic responseDtm = null;
                        if (serverModeLAN)
                            responseDtm = await DatosTicketController.downloadAllDatosTicketLAN();
                        else responseDtm = await DatosTicketController.downloadAllDatosTicketAPI();
                        if (responseDtm.value == 1)
                            dtm = DatosTicketModel.getAllData();
                    }
                    String nameDocumentType = "";
                    String pieTicket = "";
                    int documentType = DocumentModel.getDocumentType(idDocument);
                    if (documentType == 1)
                    {
                        nameDocumentType = "Cotización";
                        if (dtm != null)
                            pieTicket = dtm.PIE_TICKETCOTIZACION;
                    }
                    else if (documentType == 2)
                    {
                        nameDocumentType = "Factura";
                        if (dtm != null)
                            pieTicket = dtm.PIE_TICKETVENTAS;
                    }
                    else if (documentType == 3)
                    {
                        nameDocumentType = "Pedido";
                        if (dtm != null)
                            pieTicket = dtm.PIE_TICKETPEDIDO;
                    }
                    else if (documentType == 4)
                    {
                        nameDocumentType = "Factura";
                        if (dtm != null)
                            pieTicket = dtm.PIE_TICKETVENTAS;
                    }
                    else if (documentType == 5)
                    {
                        nameDocumentType = "Devolución";
                        if (dtm != null)
                            pieTicket = dtm.PIE_TICKETDEVOLUCION;
                    }
                    else if (documentType == 51)
                    {
                        nameDocumentType = "Cotización de Mostrador";
                        if (dtm != null)
                            pieTicket = dtm.PIE_TICKETCOTIZACION;
                    }
                    if (permissionPrepedido)
                    {
                        if (documentType == DocumentModel.TIPO_PREPEDIDO)
                            nameDocumentType = "Pedido";
                        else nameDocumentType = "Factura";
                    }
                    String folio = DocumentModel.getFolioVentaForADocument(idDocument);
                    String customerName = "";
                    if (serverModeLAN)
                    {
                        dynamic responseCustomerName = await CustomersController.getNameLAN(DocumentModel.getCustomerIdOfADocument(idDocument));
                        if (responseCustomerName.value == 1)
                            customerName = responseCustomerName.name;
                    }
                    else
                    {
                        customerName = CustomerModel.getName(DocumentModel.getCustomerIdOfADocument(idDocument));
                    }
                    String fechaDocumento = DocumentModel.getFechaHora(idDocument);
                    await Encabezado("Ticket Documento", nameDocumentType, fechaDocumento, 0, folio, customerName, idDocument, permissionPrepedido, serverModeLAN,
                        documentType);
                    await EncabezadoVenta(1, permissionPrepedido);
                    //Articulos
                    double totalDescuentoSinIVA = 0;
                    double totalDocumento = 0;
                    double subtotal = 0;
                    double ahorroTotal = 0;
                    int totalDeMovimientos = 0;
                    double totalDeArticulos = 0;
                    double netoSinImpuesto = 0;
                    double ivaTotal = 0;
                    foreach (var movimiento in movesList)
                    {
                        String capturedUnitName = "";
                        if (serverModeLAN)
                        {
                            dynamic responseUnit = await UnitsOfMeasureAndWeightController.getNameOfAndUnitMeasureWeightLAN(movimiento.capturedUnitId);
                            if (responseUnit.value == 1)
                                capturedUnitName = responseUnit.name;
                        }
                        else
                        {
                            capturedUnitName = UnitsOfMeasureAndWeightModel.getNameOfAndUnitMeasureWeight(movimiento.capturedUnitId);
                        }
                        String itemName = "";
                        if (serverModeLAN)
                        {
                            dynamic responseItem = await ItemsController.getItemNameLAN(movimiento.itemId);
                            if (responseItem.value == 1)
                                itemName = responseItem.name;
                        }
                        else itemName = ItemModel.getTheNameOfAnItem(movimiento.itemId);
                        if (!permissionPrepedido)
                        {
                            AgregaArticulo(Convert.ToDouble(movimiento.capturedUnits), capturedUnitName, movimiento.itemCode,
                                await sustituirAcentos(itemName),
                            movimiento.price.ToString("C", CultureInfo.CurrentCulture),
                            movimiento.descuentoPorcentaje+"% "+movimiento.descuentoImporte.ToString("C", CultureInfo.CurrentCulture),
                            movimiento.total.ToString("C", CultureInfo.CurrentCulture) + "MXN");
                        }
                        else
                        {
                            if (documentType == DocumentModel.TIPO_PREPEDIDO)
                            {
                                String unitName = "";
                                if (serverModeLAN)
                                {
                                    dynamic responseUnit = await UnitsOfMeasureAndWeightController.getNameOfAndUnitMeasureWeightLAN(movimiento.capturedUnitId);
                                    if (responseUnit.value == 1)
                                        unitName = responseUnit.name;
                                }
                                else
                                {
                                    unitName = UnitsOfMeasureAndWeightModel.getNameOfAndUnitMeasureWeight(movimiento.nonConvertibleUnitId);
                                }
                                AgregaArticulo(Convert.ToDouble(movimiento.nonConvertibleUnits), unitName, "Pendiente de surtir",
                                    await sustituirAcentos(itemName),"",
                                "0 % $ 0.00 MXN",
                                "$ 0.00 MXN");
                            } else
                            {
                                int numeroDeCajas = 0;
                                WeightModel wm = WeightModel.getAWeight(movimiento.id);
                                if (wm != null)
                                    numeroDeCajas = wm.cajas;
                                String unitName = "";
                                if (serverModeLAN)
                                {
                                    dynamic responseUnit = await UnitsOfMeasureAndWeightController.getNameOfAndUnitMeasureWeightLAN(movimiento.capturedUnitId);
                                    if (responseUnit.value == 1)
                                        unitName = responseUnit.name;
                                }
                                else
                                {
                                    unitName = UnitsOfMeasureAndWeightModel.getNameOfAndUnitMeasureWeight(movimiento.nonConvertibleUnitId);
                                }
                                AgregaArticulo(Convert.ToDouble(movimiento.nonConvertibleUnits), unitName, "Cajas: " + numeroDeCajas,
                                    await sustituirAcentos(itemName),"",
                                movimiento.descuentoPorcentaje + "% " + movimiento.descuentoImporte.ToString("C", CultureInfo.CurrentCulture),
                                movimiento.total.ToString("C", CultureInfo.CurrentCulture) + "MXN");
                            }
                        }
                        ahorroTotal += movimiento.descuentoImporte;
                        double impuestoItem = 16;
                        if (serverModeLAN)
                        {
                            dynamic responseImp = await ItemsController.getImpuestoLAN(movimiento.itemId, 1);
                            if (responseImp.value == 1)
                                impuestoItem = responseImp.impuesto;
                        }
                        else
                        {
                            impuestoItem = ItemModel.getImpuesto(movimiento.itemId, 1);
                        }
                        double impuestoPorcentaje = (impuestoItem / 100);
                        double precioSinIVA = movimiento.price / (1 + impuestoPorcentaje);
                        totalDescuentoSinIVA += (movimiento.descuentoImporte / (1 + impuestoPorcentaje));
                        double montoSubtotal = ((precioSinIVA * movimiento.capturedUnits) - (movimiento.descuentoImporte / (1 + impuestoPorcentaje)));
                        subtotal += montoSubtotal;
                        ivaTotal += (montoSubtotal * impuestoPorcentaje);
                        netoSinImpuesto += (precioSinIVA * movimiento.capturedUnits);
                        totalDeArticulos += movimiento.capturedUnits;
                        totalDocumento += movimiento.total;
                        totalDeMovimientos++;
                    }
                    double pesoExcedente = 0;
                    if (permissionPrepedido)
                    {
                        if (documentType == DocumentModel.TIPO_PREPEDIDO)
                        {

                        } else
                        {
                            MovimientosModel mm = MovimientosModel.getAMovementFromADocument(idDocument);
                            if (mm != null)
                            {
                                WeightModel wm = WeightModel.getWeightReal(mm.id);
                                if (wm != null)
                                {
                                    TextoIzquierda("Peso Bruto: " + MetodosGenerales.obtieneDosDecimales(wm.pesoBruto) + " KG");
                                    double pesoTara = wm.pesoCaja;
                                    double pesoPolloLesionado = wm.pesoPolloLesionado;
                                    double pesoPolloMuerto = wm.pesoPolloMuerto;
                                    double pesoPolloBajoPeso = wm.pesoPolloBajoDePeso;
                                    double pesoPolloGolpeado = wm.pesoPolloGolpeado;
                                    if (pesoTara != 0)
                                        TextoIzquierda("Peso Tara: " + MetodosGenerales.obtieneDosDecimales(pesoTara) + " KG");
                                    if (pesoPolloLesionado != 0)
                                        TextoIzquierda("Peso Pollo Lesionado: " + MetodosGenerales.obtieneDosDecimales(pesoPolloLesionado) + " KG");
                                    if (pesoPolloMuerto != 0)
                                        TextoIzquierda("Peso Pollo Muerto: " + MetodosGenerales.obtieneDosDecimales(pesoPolloMuerto) + " KG");
                                    if (pesoPolloBajoPeso != 0)
                                        TextoIzquierda("Peso Pollo Muerto: " + MetodosGenerales.obtieneDosDecimales(pesoPolloBajoPeso) + " KG");
                                    if (pesoPolloGolpeado != 0)
                                        TextoIzquierda("Peso Pollo Golpeado: " + MetodosGenerales.obtieneDosDecimales(pesoPolloGolpeado) + " KG");
                                    pesoExcedente = (Math.Abs(pesoTara) + Math.Abs(pesoPolloLesionado) + Math.Abs(pesoPolloMuerto) +
                                    Math.Abs(pesoPolloBajoPeso) + Math.Abs(pesoPolloGolpeado));
                                    double pesoNeto = (wm.pesoBruto - Math.Abs(pesoExcedente));
                                    if (pesoNeto == wm.pesoNeto)
                                        pesoNeto = wm.pesoNeto;
                                    TextoIzquierda("Peso Neto: " + MetodosGenerales.obtieneDosDecimales(pesoNeto) + " KG");
                                }
                            }
                        }
                    }
                    TextoCentro("");
                    //Vamos a validar de donde traeremos la información. Si de la BD o del FORM
                    //Para el caso de las formas de pago y el total. 
                    await TraerFormasPago(idDocument, subtotal, netoSinImpuesto, totalDescuentoSinIVA, ivaTotal, totalDocumento, documentType);
                    String obervationDocumento = DocumentModel.getDocumentObservation(idDocument);
                    TextoCentro(obervationDocumento);
                    TextoCentro("");
                    TextoIzquierda("Total de Movimientos: " + totalDeMovimientos);
                    if (permissionPrepedido)
                        TextoIzquierda("Total de Pollos: " + totalDeArticulos + " KG");
                    else TextoIzquierda("Total de Articulos: " + totalDeArticulos);
                    TextoIzquierda("Usted Ahorro: " + ahorroTotal.ToString("C", CultureInfo.CurrentCulture));
                    TextoCentro("");
                    if (permissionPrepedido)
                    {
                        if (documentType == DocumentModel.TIPO_PREPEDIDO)
                        {

                        } else
                        {
                            bool documentoEnviadoAlCliente = DocumentModel.isItDocumentPrepedidoSendedToTheCustomerAndPendienteDeDestarar(idDocument);
                            if (documentoEnviadoAlCliente)
                            {
                                if (pesoExcedente == 0)
                                {
                                    TextoCentro("Nombre y firma del Cliente");
                                    TextoCentro("");
                                    TextoCentro("");
                                    TextoCentro("__________________________");
                                    TextoCentro("");
                                }
                                else
                                {
                                    TextoCentro("Nombre y firma de quien Destara");
                                    TextoCentro("");
                                    TextoCentro("");
                                    TextoCentro("__________________________");
                                    TextoCentro("");
                                }
                            }
                            else
                            {
                                TextoCentro("Nombre y firma del Cliente");
                                TextoCentro("");
                                TextoCentro("");
                                TextoCentro("__________________________");
                                TextoCentro("");
                            }
                            TextoCentro("Datos de Peso del Cliente");
                            TextoIzquierda("");
                            TextoIzquierda("Peso Bruto:_____________kg");
                            TextoIzquierda("");
                            TextoIzquierda("Peso Taras:_____________kg");
                            TextoIzquierda("");
                            TextoIzquierda("Peso Neto:______________kg");
                            TextoIzquierda("");
                            TextoIzquierda("Peso Pollo Mal:_________kg");
                            TextoCentro("");
                        }
                    }
                    FoliosDigitalesModel fdm = FoliosDigitalesModel.getFolioDigital(idDocumentoServer);
                    if (fdm != null)
                    {
                        TextoCentro("Folio Fiscal");
                        TextoCentro(fdm.uuid);
                    }
                    else
                    {
                        dynamic responseDtm = null;
                        dtm = DatosTicketModel.getAllData();
                        if (dtm == null)
                        {
                            if (serverModeLAN)
                                responseDtm = await DatosTicketController.downloadAllDatosTicketLAN();
                            else responseDtm = await DatosTicketController.downloadAllDatosTicketAPI();
                            if (responseDtm.value == 1)
                            {
                                dtm = DatosTicketModel.getAllData();
                                TextoCentro("Folio Fiscal");
                                TextoCentro(fdm.uuid);
                            }
                        }
                        else
                        {
                            TextoCentro("Folio Fiscal");
                            TextoCentro(fdm.uuid);
                        }
                    }
                    TextoCentro("");
                    TextoCentro(pieTicket);
                    TextoCentro(DateTime.Now.ToString());
                    TextoCentro("");
                    TextoCentro("");
                    TextoCentro("");
                    TextoCentro("");
                    TextoCentro("");
                    cutTicket();
                    if (openCashDrawer)
                        abrirCajonDinero();
                    printTicket("FACTURA");
                }
            });
            return response;
        }

        public async Task createAndPrintReporteCajaTicket(bool serverModeLAN, bool showDocs, bool showCredits, bool showPagos, bool showIngresos,
            bool showRetiros)
        {
            DatosTicketModel dtm = DatosTicketModel.getAllData();
            if (dtm == null)
            {
                dynamic response = null;
                if (serverModeLAN)
                    response = await DatosTicketController.downloadAllDatosTicketLAN();
                else response = await DatosTicketController.downloadAllDatosTicketAPI();
                if (response.value == 1)
                {
                    dtm = DatosTicketModel.getAllData();
                }
            }
            //Encabezado
            await Encabezado("Reporte de Caja", "", "", 0, "", "", 0, false, serverModeLAN, 0);
            TextoCentro("");
            TextoCentro("* * * Importe de Apertura * * *");
            double totalDeApertura = AperturaTurnoModel.getImporteDeAperturaActual();
            TextoDerecha("Total Ingresado: " + totalDeApertura.ToString("C") + " MXN");
            double totalVendido = 0, totalRetirado = 0, totalIngresado = 0, faltante = 0;
            TextoCentro("* * * Totales por Formas de Cobro * * *");
            TextoCentro("");
            String totalsByFc = await getTotalsByFc();
            TextoCentro("");
            TextoDerecha(totalsByFc);
            if (showDocs)
            {
                TextoCentro("");
                TextoCentro(" * Documentos por Formas de Cobros *");
                TextoCentro("");
            }
            String documentsByDc = await getAllDocumentsByFc(showDocs);
            TextoDerecha(documentsByDc);
            totalVendido = await getTotalVendido();
            /* Créditos desglosados por documento de clientes */
            if (showCredits)
                TextoCentro("* * * Creditos * * *");
            double abonosTotalesCreditos = await getAllCreditsDocumentsByCustomers(showCredits);
            double totalCobrado = await getAllAbonos(showPagos);
            List<IngresoModel> ingresosList = await getAllIngresosRepCaja();
            if (ingresosList != null)
            {
                if (showIngresos)
                    TextoCentro("* * * Ingresos de Dinero * * *");
                foreach (var ingreso in ingresosList)
                {
                    double total = MontoIngresoModel.getTotalOfAnEntry(ingreso.id);
                    if (showIngresos)
                    {
                        TextoIzquierda(await sustituirAcentos(ingreso.concept));
                        TextoExtremos("Ingreso" + ingreso.number, " " + total.ToString("C") + " MXN");
                    }
                    totalIngresado += total;
                }
            }
            List<RetiroModel> retirosList = await getAllWithdrawalsRepCaja();
            if (retirosList != null)
            {
                if (showRetiros)
                    TextoCentro("* * * Retiros * * *");
                foreach (var retiro in retirosList)
                {
                    double total = MontoRetiroModel.getTotalOfAWithdrawal(retiro.id);
                    if (showRetiros)
                    {
                        TextoIzquierda(await sustituirAcentos(retiro.concept));
                        TextoExtremos("Retiro" + retiro.number, " " + total.ToString("C") + " MXN");
                    }
                    totalRetirado += total;
                }
            }
            //faltante = totalVendido - totalRetirado;
            double totalEnCaja = 0;
            TextoCentro("* * Totales Netos * *");
            TextoCentro("");
            String totalCredits = await getTotalOfCredits();
            TextoDerecha(totalCredits);
            TextoCentro("");
            TextoDerecha("Importe de Apertura: " + totalDeApertura.ToString("C") + " MXN");
            TextoDerecha("Total Vendido y Abonado: " + totalVendido.ToString("C") + " MXN");
            TextoDerecha("Total Cobrado: " + totalCobrado.ToString("C") + " MXN");
            TextoDerecha("Total en Ingresos: " + totalIngresado.ToString("C") + " MXN");
            TextoCentro("");
            totalEnCaja = totalDeApertura + totalVendido + totalCobrado + totalIngresado;
            TextoDerecha("Total En Caja: " + totalEnCaja.ToString("C") + " MXN");
            TextoCentro("");
            TextoDerecha("Total Retirado: " + totalRetirado.ToString("C") + " MXN");
            TextoCentro("");
            TextoCentro("----------------------------");
            faltante = totalEnCaja - totalRetirado;
            if (faltante >= 0)
            {
                TextoDerecha("Faltante: " + faltante.ToString("C") + " MXN");
            }
            else
            {
                TextoDerecha("Sobrante: " + Math.Abs(faltante).ToString("C") + " MXN");
            }
            TextoCentro("");
            TextoCentro(DateTime.Now.ToString());
            TextoCentro("Firma Del Cajero");
            TextoCentro("");
            TextoCentro("");
            TextoCentro("__________________________");
            TextoCentro("");
            TextoCentro("");
            TextoCentro("");
            TextoCentro("");
            cutTicket();
            string codigoCaja = UserModel.getCodeBox(ClsRegeditController.getIdUserInTurn());
            string codigoCajero = UserModel.getCode(ClsRegeditController.getIdUserInTurn());
            printTicket(tipoTicketTPV: "CORTE", claveTicketTPV: codigoCaja + "-" + codigoCajero + "-" + TicketCorteFechaHora.ToString("yyyyMMdd-HHmmss"));
        }

        private async Task<double> getAllAbonos(bool showPagos)
        {
            double total = 0;
            if (showPagos)
                TextoCentro("* * Pagos de Clientes * *");
            await Task.Run(() =>
            {
                List<CuentasXCobrarModel> pagosList = CuentasXCobrarModel.getAllPagos();
                if (pagosList != null)
                {
                    foreach (CuentasXCobrarModel pago in pagosList)
                    {
                        if (showPagos)
                        {
                            TextoIzquierda("Folio: " + pago.creditDocumentFolio);
                            TextoDerecha("Total Cobrado: " + pago.amount.ToString("C") + " MXN");
                        }
                        total += pago.amount;
                    }
                }
            });
            return total;
        }

        private async Task<String> getTotalsByFc()
        {
            String formatototales = "";
            List<FormasDeCobroModel> formasList = null;
            double totalVendidoYCobrado = 0;
            await Task.Run(() => {
                formasList = FormasDeCobroModel.getAllFormasDeCobro();
                List<FormasDeCobroModel> formaList = null;
                if (formasList != null)
                {
                    FormasDeCobroModel formasCobro = new FormasDeCobroModel();
                    formasCobro.FORMA_COBRO_ID = 0;
                    formasCobro.NOMBRE = "Sin asignar";
                    formaList = new List<FormasDeCobroModel>();
                    formaList.Add(formasCobro);
                    formasList.AddRange(formaList);
                }
                else
                {
                    FormasDeCobroModel formasCobro = new FormasDeCobroModel();
                    formasCobro.FORMA_COBRO_ID = 0;
                    formasCobro.NOMBRE = "Sin asignar";
                    formaList = new List<FormasDeCobroModel>();
                    formaList.Add(formasCobro);
                    formasList.AddRange(formaList);
                }
                double sumaTXT = 0;
                double sumaCambio = 0;
                double sumaDos = 0;
                double sumasDeImporte = 0.0;
                if (formasList != null)
                {
                    for (int i = 0; i < formasList.Count; i++)
                    {
                        if (formasList[i].FORMA_COBRO_ID != 71)
                        {
                            sumaTXT = FormasDeCobroDocumentoModel.getAllTotalForAFormaDePagoInReporteDeCorte(formasList[i].FORMA_COBRO_ID);
                            sumaCambio = FormasDeCobroDocumentoModel.getSumCambioForAFormaDePagoInReporteDeCorte(formasList[i].FORMA_COBRO_ID);
                            sumaDos = CuentasXCobrarModel.getAllTotalForAFormaDePagoAbono(formasList[i].FORMA_COBRO_ID);
                            sumasDeImporte = sumaTXT + sumaDos;
                            sumasDeImporte -= sumaCambio;
                            totalVendidoYCobrado += sumasDeImporte;
                            if (sumasDeImporte != 0)
                            {
                                TextoIzquierda("> " + formasList[i].FORMA_COBRO_ID + " " + MetodosGenerales.quitarAsentos(formasList[i].NOMBRE) + " = $" +
                                        MetodosGenerales.obtieneDosDecimales(sumasDeImporte) + " MXN");
                            }
                            sumaTXT = 0;
                            sumaCambio = 0;
                            sumaDos = 0;
                            sumasDeImporte = 0;
                        }
                    }
                    TextoDerecha("-------------------");
                    TextoDerecha("Total: $ " + MetodosGenerales.obtieneDosDecimales(totalVendidoYCobrado) + " MXN");
                }
            });
            return formatototales;
        }

        private async Task<String> getAllDocumentsByFc(bool showDocs)
        {
            String response = "";
            double totalVendidoYCobrado = 0;
            List<FormasDeCobroModel> formasList = null;
            await Task.Run(async () => {
                List<FormasDeCobroModel> formaList = new List<FormasDeCobroModel>();
                formasList = FormasDeCobroModel.getAllFormasDeCobro();
                double sumaCambio = 0;
                double totalDocument = 0;
                double sumasDeImporte = 0.0;
                if(formasList != null)
                {
                    FormasDeCobroModel formasCobro = new FormasDeCobroModel();
                    formasCobro.FORMA_COBRO_ID = 0;
                    formasCobro.NOMBRE = "Sin asignar";
                    formaList = new List<FormasDeCobroModel>();
                    formaList.Add(formasCobro);
                    formasList.AddRange(formaList);
                }
                else
                {
                    FormasDeCobroModel formasCobro = new FormasDeCobroModel();
                    formasCobro.FORMA_COBRO_ID = 0;
                    formasCobro.NOMBRE = "Sin asignar";
                    formaList = new List<FormasDeCobroModel>();
                    formaList.Add(formasCobro);
                    formasList.AddRange(formaList);
                }
                if (formasList != null) {
                    int countEncabezadoFc = 0;
                    int lastIdFc = 0;
                    int lastFcId = 0;
                    for (int i = 0; i < formasList.Count; i++) {
                        lastFcId = formasList[i].FORMA_COBRO_ID;
                        if (formasList[i].FORMA_COBRO_ID != 71)
                        {
                            String query = "SELECT * FROM " + LocalDatabase.TABLA_DOCUMENTOVENTA +
                                " WHERE (" + LocalDatabase.CAMPO_TIPODOCUMENTO_DOC + " = " + 2 + " OR " +
                                LocalDatabase.CAMPO_TIPODOCUMENTO_DOC + " = " + 4 + ") AND " +
                                LocalDatabase.CAMPO_PAUSAR_DOC + " = " + 0 + " AND "+ LocalDatabase.CAMPO_CANCELADO_DOC + " = "+ 0;
                            List<DocumentModel>  documentsList = DocumentModel.getAllDocuments(query);
                            if (documentsList != null) {
                                int count = 0;
                                foreach (DocumentModel document in documentsList) {
                                    if (document.forma_cobro_id == lastFcId) {
                                        if (showDocs)
                                        {
                                            if (count == 0)
                                                TextoIzquierda("> " + lastFcId + " " + await sustituirAcentos(formasList[i].NOMBRE));
                                        }
                                        count++;
                                        totalVendidoYCobrado += sumasDeImporte;
                                        //sumasDeImporte -= sumaCambio;
                                        if (showDocs)
                                        {
                                            if (document.estado == 1)
                                                TextoIzquierda("X Cancelado Folio #" + document.fventa + " ");
                                            else TextoIzquierda("Folio #" + document.fventa + " ");
                                        }
                                        String queryTotal = "SELECT sum(" + LocalDatabase.CAMPO_ANTICIPO_DOC + ") FROM " + LocalDatabase.TABLA_DOCUMENTOVENTA + " WHERE " +
                                        "(" + LocalDatabase.CAMPO_TIPODOCUMENTO_DOC + " = " + 2 + " OR " + LocalDatabase.CAMPO_TIPODOCUMENTO_DOC + " = " + 4 + ") AND " +
                                    LocalDatabase.CAMPO_PAUSAR_DOC + " = " + 0 + " AND " + LocalDatabase.CAMPO_CANCELADO_DOC + " = " + 0 +
                                        " AND " + LocalDatabase.CAMPO_ID_DOC + " = " + document.id;
                                        totalDocument = DocumentModel.getDoubleValue(queryTotal);
                                        if (document.estado == 0)
                                            sumasDeImporte += totalDocument;
                                        //response += "Total: $" + ClsMetodosGenerales.obtieneDosDecimales(totalDocument) + " MXN\n";
                                        if (showDocs)
                                        {
                                            if (document.estado == 1)
                                                TextoDerecha("Total Cancelado: " + totalDocument.ToString("C") + " MXN");
                                            else TextoDerecha("Total: " + totalDocument.ToString("C") + " MXN");
                                        }
                                    }
                                }
                                if (showDocs)
                                {
                                    if (sumasDeImporte != 0)
                                        TextoDerecha("Suma " + await sustituirAcentos(formasList[i].NOMBRE) + ": " +
                                            sumasDeImporte.ToString("C") + " MXN");
                                }
                                sumaCambio = 0;
                                totalDocument = 0;
                            }
                            //sumaCambio = ClsFormasDeCobroDocumentoModel.getSumCambioForAFormaDePagoInReporteDeCorte(formasList[i].FORMA_COBRO_CC_ID);
                            sumasDeImporte = 0;
                        }
                    }
                }
            });
            return response;
        }

        private async Task<double> getAllCreditsDocumentsByCustomers(bool showCredits)
        {
            double sumasAbonados = 0;
            await Task.Run(async () => {
                String query = "SELECT * FROM " + LocalDatabase.TABLA_DOCUMENTOVENTA +" D"+
                " WHERE (D." + LocalDatabase.CAMPO_TIPODOCUMENTO_DOC + " = " + 2 + " OR D." +LocalDatabase.CAMPO_TIPODOCUMENTO_DOC + " = " + 4 + ") " +
                "AND D." +LocalDatabase.CAMPO_PAUSAR_DOC + " = " + 0 + " AND D." + LocalDatabase.CAMPO_FORMACOBROID_DOC + " = " + 71;
                List<DocumentModel> documentsList = DocumentModel.getAllDocuments(query);
                if (documentsList != null)
                {
                    foreach (DocumentModel document in documentsList)
                    {
                        if (showCredits)
                        {
                            String queryCustomer = "SELECT " + LocalDatabase.CAMPO_NOMBRECLIENTE + " FROM " + LocalDatabase.TABLA_CLIENTES + " WHERE " +
                        LocalDatabase.CAMPO_ID_CLIENTE + " = " + document.cliente_id;
                            String customerName = CustomerModel.getAStringValueFromACustomer(queryCustomer);
                            TextoIzquierda(await sustituirAcentos(customerName));
                            if (document.estado == 1)
                                TextoIzquierda(" X Cancelado Folio #" + document.fventa + " ");
                            else TextoIzquierda(" Folio #" + document.fventa + " ");
                        }
                        String queryTotal = "SELECT SUM("+ LocalDatabase.CAMPO_ANTICIPO_DOC+") FROM "+LocalDatabase.TABLA_DOCUMENTOVENTA +" WHERE " +
                                        "("+LocalDatabase.CAMPO_TIPODOCUMENTO_DOC+" = "+2+" OR "+LocalDatabase.CAMPO_TIPODOCUMENTO_DOC+" = "+4+") AND " +
                                    LocalDatabase.CAMPO_PAUSAR_DOC + " = "+ 0 +" AND " + LocalDatabase.CAMPO_ID_DOC + " = " + document.id;
                        double anticipoDocument = DocumentModel.getDoubleValue(queryTotal);
                        sumasAbonados += anticipoDocument;
                        //response += "Total: $" + ClsMetodosGenerales.obtieneDosDecimales(totalDocument) + " MXN\n";
                        if (showCredits) {
                            if (document.estado == 1)
                            {
                                TextoDerecha("Total Cancelado: " + document.total.ToString("C") + " MXN");
                            }
                            else
                            {
                                TextoDerecha("Total: " + document.total.ToString("C") + " MXN");
                                TextoDerecha("Abonado: " + anticipoDocument.ToString("C") + " MXN");
                            }
                        }
                    }
                }
            });
            return sumasAbonados;
        }

        private async Task<double> getTotalVendido()
        {
            double value = 0;
            List<DocumentModel> documentsList = await getAllSales();
            await Task.Run(() => {
                if (documentsList != null) {
                    foreach (var document in documentsList)
                    {
                        if (document.estado == 0)
                            value += document.anticipo;
                    }
                }
            });
            return value;
        }

        private async Task<List<DocumentModel>> getAllCreditsDocuments()
        {
            List<DocumentModel> documentsList = null;
            await Task.Run(async () =>
            {
                String query = "SELECT * FROM " + LocalDatabase.TABLA_DOCUMENTOVENTA +
                        " WHERE (" + LocalDatabase.CAMPO_TIPODOCUMENTO_DOC + " = " + 2 + " OR " +
                        LocalDatabase.CAMPO_TIPODOCUMENTO_DOC + " = " + 4 + ") AND " +
                        LocalDatabase.CAMPO_PAUSAR_DOC + " = " + 0 + " AND " + LocalDatabase.CAMPO_FORMACOBROID_DOC + " = " + 71;
                documentsList = DocumentModel.getAllDocuments(query);
            });
            return documentsList;
        }

        private async Task<List<DocumentModel>> getAllSales()
        {
            List<DocumentModel> documentsList = null;
            await Task.Run(async () =>
            {
                String query = "SELECT * FROM " + LocalDatabase.TABLA_DOCUMENTOVENTA + " WHERE (" +
                LocalDatabase.CAMPO_TIPODOCUMENTO_DOC+" = "+2+" OR "+LocalDatabase.CAMPO_TIPODOCUMENTO_DOC + " = " + 4 + ") " +
                "AND " + LocalDatabase.CAMPO_CANCELADO_DOC + " = " + 0 + " AND " +LocalDatabase.CAMPO_PAUSAR_DOC + " = " + 0;
                documentsList = DocumentModel.getAllDocuments(query);
            });
            return documentsList;
        }

        private async Task<List<RetiroModel>> getAllWithdrawalsRepCaja()
        {
            List<RetiroModel> retirosList = null;
            await Task.Run(async () =>
            {
                String query = "SELECT * FROM " + LocalDatabase.TABLA_RETIROS;
                retirosList = RetiroModel.getAllWithdrawals(query);
            });
            return retirosList;
        }

        private async Task<List<IngresoModel>> getAllIngresosRepCaja()
        {
            List<IngresoModel> ingresosList = null;
            await Task.Run(async () =>
            {
                String query = "SELECT * FROM " + LocalDatabase.TABLA_INGRESO ;
                ingresosList = IngresoModel.getAllEntries(query);
            });
            return ingresosList;
        }

        private async Task<String> getTotalOfCredits ()
        {
            String response = "";
            await Task.Run(async () =>
            {
                String query = "SELECT sum(d." + LocalDatabase.CAMPO_TOTAL_DOC + ") FROM " + LocalDatabase.TABLA_DOCUMENTOVENTA + " d " +
                    " INNER JOIN " + LocalDatabase.TABLA_CLIENTES + " c ON d." + LocalDatabase.CAMPO_CLIENTEID_DOC + " = c." +
                    LocalDatabase.CAMPO_ID_CLIENTE + " WHERE d." + LocalDatabase.CAMPO_TIPODOCUMENTO_DOC + " = " + 2 + " AND d." +
                    LocalDatabase.CAMPO_CANCELADO_DOC + " = " + 0 + " AND d." + LocalDatabase.CAMPO_PAUSAR_DOC + " = " + 0 +
                    " AND d." + LocalDatabase.CAMPO_FORMACOBROID_DOC + " = " + 71;
                double totalACredito = DocumentModel.getDoubleValue(query);
                response = "Total a Crédito: $" + MetodosGenerales.obtieneDosDecimales(totalACredito) + " MXN";
            });
            return response;
        }

        public async Task<int> TraerFormasPago(int idDocumento,double subtotal, double netoSinImpuesto, double descuentoSinIVA,
            double ivaTotal, double totalDocumento, int tipoDeDocumento)
        {
            Error = 0;
            decimal total = 0.0M, cambio = 0.0M;
            List<FormasDePagoModel> lFormasPago = new List<FormasDePagoModel>();
            var db = new SQLiteConnection();
            try
            {
                db.ConnectionString = ClsSQLiteDbHelper.instanceSQLite;
                db.Open();
                query = "SELECT FCD." + LocalDatabase.CAMPO_IMPORTE_FORMACOBRODOC + " AS Importe, FCD." + LocalDatabase.CAMPO_CAMBIO_FORMACOBRODOC +
                " AS Cambio, FCM." + LocalDatabase.CAMPO_NOMBRE_FORMASCOBRO + " As Nombre from " + LocalDatabase.TABLA_FORMA_COBRO_DOCUMENTO + " FCD " +
                "inner join " + LocalDatabase.TABLA_FORMASCOBRO + " FCM ON FCD." + LocalDatabase.CAMPO_FORMACOBROIDABONO_FORMACOBRODOC +
                " = FCM." + LocalDatabase.CAMPO_ID_FORMASCOBRO + " WHERE " + LocalDatabase.CAMPO_DOCID_FORMACOBRODOC + " = " + idDocumento;
                using (SQLiteCommand command = new SQLiteCommand(query, db))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                FormasDePagoModel Fpago = new FormasDePagoModel();
                                Fpago.NOMBRE = reader["Nombre"].ToString().Trim();
                                Fpago.Total = Convert.ToDecimal(reader["Importe"]);
                                cambio += Convert.ToDecimal(reader["Cambio"]);
                                lFormasPago.Add(Fpago);
                            }
                        }
                        if (reader != null && !reader.IsClosed)
                            reader.Close();
                    }
                }
                if (tipoDeDocumento == DocumentModel.TIPO_PREPEDIDO)
                {
                    TextoCentro("");
                    TextoExtremos("Neto :", netoSinImpuesto.ToString("C", CultureInfo.CurrentCulture) + " MXN");
                    TextoExtremos("Descuento :", "$ 0.00 MXN");
                    TextoExtremos("Subtotal :", "$ 0.00 MXN");
                    String nameImp1 = DatosTicketModel.getNameImp(1);
                    if (nameImp1.Equals(""))
                        TextoExtremos("IVA :", ivaTotal.ToString("C", CultureInfo.CurrentCulture) + " MXN");
                    else TextoExtremos(nameImp1 + " :", ivaTotal.ToString("C", CultureInfo.CurrentCulture) + " MXN");
                    TextoCentro("");
                    TextoExtremos("Total :", "$ 0 MXN");
                }
                else
                {
                    TextoCentro("");
                    TextoExtremos("Neto :", netoSinImpuesto.ToString("C", CultureInfo.CurrentCulture) + " MXN");
                    TextoExtremos("Descuento :", descuentoSinIVA.ToString("C", CultureInfo.CurrentCulture) + " MXN");
                    TextoExtremos("Subtotal :", subtotal.ToString("C", CultureInfo.CurrentCulture) + " MXN");
                    String nameImp1 = DatosTicketModel.getNameImp(1);
                    if (nameImp1.Equals(""))
                        TextoExtremos("IVA :", ivaTotal.ToString("C", CultureInfo.CurrentCulture) + " MXN");
                    else TextoExtremos(nameImp1 + " :", ivaTotal.ToString("C", CultureInfo.CurrentCulture) + " MXN");
                    TextoCentro("");
                    TextoExtremos("Total :", totalDocumento.ToString("C", CultureInfo.CurrentCulture) + " MXN");
                }
                var dvm = DocumentModel.getAllDataDocumento(idDocumento);
                if (dvm != null)
                {
                    if (dvm.tipo_documento != DocumentModel.TIPO_COTIZACION_MOSTRADOR && dvm.tipo_documento != DocumentModel.TIPO_COTIZACION &&
                dvm.tipo_documento != DocumentModel.TIPO_PEDIDO)
                    {
                        if (lFormasPago != null && lFormasPago.Count > 0)
                        {
                            foreach (var item in lFormasPago)
                            {
                                TextoExtremos(await sustituirAcentos(item.NOMBRE), item.Total.ToString("C", CultureInfo.CurrentCulture) + " MXN");
                                total += item.Total;
                            }
                        }
                    }
                }
                if (tipoDeDocumento == DocumentModel.TIPO_PREPEDIDO)
                {
                    TextoCentro("");
                } else
                {
                    TextoExtremos("Cambio :", cambio.ToString("C", CultureInfo.CurrentCulture) + " MXN");
                    TextoCentro(Convertidor(totalDocumento.ToString()));
                    TextoCentro("");
                }
            }
            catch (Exception Ex)
            {
                Error = 1;
                SECUDOC.writeLog(Ex.ToString());
                MensajeError = Ex.ToString();
            }
            finally
            {
                if (db != null && db.State == ConnectionState.Open)
                    db.Close();
            }
            return Error;
        }

        public string ConvertirCantidades(string valor)
        {
            string Total = "";
            if (valor != null)
            {
                if (valor.ToString() != "")
                {
                    string[] MONTO = (valor).ToString().Split('.');
                    if (MONTO.Length > 1)
                    {

                        if (MONTO[1].Length > 2)
                            MONTO[1] = MONTO[1].Substring(0, 2);


                        if (MONTO[1].Length < 2)
                            MONTO[1] = MONTO[1] + "0";

                        Total = "" + MONTO[0] + "." + MONTO[1];
                    }
                    else
                    {
                        Total = "" + MONTO[0] + ".00";
                    }
                }
            }
            return Total;
        }

        public async Task Encabezado(string Titulo, String documentType, String fechaHoraDocumento, int idRetiroOIngreso, String folio, 
            String customerName, int idDocumento, bool permissionPrepedido, bool serverModeLAN, int tipoDeDocumento)
        {
            DatosTicketModel dtm = DatosTicketModel.getAllData();
            if (dtm == null)
            {
                dynamic response = null;
                if (serverModeLAN)
                    response = await DatosTicketController.downloadAllDatosTicketLAN();
                else response = await DatosTicketController.downloadAllDatosTicketAPI();
                if (response.value == 1)
                {
                    dtm = DatosTicketModel.getAllData();
                    TextoCentro(await sustituirAcentos(dtm.EMPRESA));
                }
                else
                {
                    TextoCentro(await sustituirAcentos("Validar Información de la empresa en SyncROM Panel"));
                }
            }
            else
            {
                if (!dtm.EMPRESA.Equals(""))
                {
                    TextoCentro(await sustituirAcentos(dtm.EMPRESA));
                }
                    TextoCentro(await sustituirAcentos(documentType));
                if (!dtm.DIRECCION.Equals(""))
                {
                    TextoCentro(await sustituirAcentos(dtm.DIRECCION));
                }
                if (!dtm.RFC.Equals(""))
                {
                    TextoCentro("RFC:" + await sustituirAcentos(dtm.RFC));
                }
                if (!dtm.EXPEDIDO.Equals(""))
                {
                    TextoCentro("EXPED:" + await sustituirAcentos(dtm.EXPEDIDO));
                }
            }
            PrinterModel pm = PrinterModel.getallDataFromAPrinter();
            if (Titulo.Equals("Ticket Documento"))
            {
                if (pm != null)
                {
                    if (pm.showFolio == 1)
                    {
                        TextoCentro("");
                        TextoIzquierda("Folio: " + folio);
                    }
                } else
                {
                    TextoCentro("");
                    TextoIzquierda("Folio: " + folio);
                }
            }
            if (permissionPrepedido)
            {
                if (tipoDeDocumento == DocumentModel.TIPO_PREPEDIDO)
                {
                    TextoIzquierda("Agente: " + await sustituirAcentos(UserModel.getAStringValueForAnyUser("SELECT " + LocalDatabase.CAMPO_NOMBRE_USER + " FROM " + LocalDatabase.TABLA_USUARIO + " WHERE " + LocalDatabase.CAMPO_ID_USUARIO + " = " +
                    ClsRegeditController.getIdUserInTurn())));
                    String agentROMName = UserModel.getTheNameOfTheUserWhoGeneratedThePreorder(idDocumento);
                    if (!agentROMName.Equals(""))
                        TextoIzquierda("Agente: " + agentROMName);
                } else
                {
                    TextoIzquierda("Surtidor: " + await sustituirAcentos(UserModel.getAStringValueForAnyUser("SELECT " + LocalDatabase.CAMPO_NOMBRE_USER + " FROM " + LocalDatabase.TABLA_USUARIO + " WHERE " + LocalDatabase.CAMPO_ID_USUARIO + " = " +
                    ClsRegeditController.getIdUserInTurn())));
                    String agentROMName = UserModel.getTheNameOfTheUserWhoGeneratedThePreorder(idDocumento);
                    if (!agentROMName.Equals(""))
                        TextoIzquierda("Agente: " + agentROMName);
                }
            }
            else
            {
                if (pm != null)
                {
                    if (pm.showCodigoCaja == 1)
                        TextoIzquierda("Caja: " + UserModel.getCodeBox(ClsRegeditController.getIdUserInTurn()));
                    if (pm.showCodigoUsuario == 1)
                        TextoIzquierda("Clave: " + UserModel.getCode(ClsRegeditController.getIdUserInTurn()));
                    if (pm.showNombreUsuario == 1)
                        TextoIzquierda("Cajero: " + UserModel.getNameUser(ClsRegeditController.getIdUserInTurn()));
                } else
                {
                    TextoIzquierda("Caja: " + UserModel.getCodeBox(ClsRegeditController.getIdUserInTurn()));
                    TextoIzquierda("Cajero: " + UserModel.getNameUser(ClsRegeditController.getIdUserInTurn()));
                    TextoIzquierda("Clave: " + UserModel.getCode(ClsRegeditController.getIdUserInTurn()));
                }
            }
            if (!fechaHoraDocumento.Equals(""))
            { 
                if (pm != null)
                {
                    //if (pm.showFechaHora == 1)
                    //    TextoIzquierda("Fecha y Hora: " + fechaHoraDocumento);
                    if (pm.showFechaHora == 1)
                    {
                        TicketIngresoFechaHora = DateTime.Now;
                        TextoIzquierda("Fecha y Hora: " + TicketIngresoFechaHora.ToString("yyyy-MM-dd HH:mm:ss"));
                    }
                } else TextoIzquierda("Fecha y Hora: " + fechaHoraDocumento);
            }
            else
            {
                if (pm != null)
                {
                    if (pm.showFechaHora == 1)
                    {
                        //TextoIzquierda("Fecha y Hora: " + MetodosGenerales.getCurrentDateAndHour());
                        TicketCorteFechaHora = DateTime.Now;
                        TextoIzquierda("Fecha y Hora: " + TicketCorteFechaHora.ToString("yyyy-MM-dd HH:mm:ss"));
                    }
                } else TextoIzquierda("Fecha y Hora: " + MetodosGenerales.getCurrentDateAndHour());
            }
            TextoCentro("");
            if (Titulo.Equals("Ticket Documento"))
            {
                if (permissionPrepedido)
                {
                    if (customerName.Length > 31)
                        TextoIzquierda("Cliente: " + customerName.Substring(0,31));
                    else TextoIzquierda("Cliente: " + customerName);
                    TextoCentro("");
                    if (tipoDeDocumento == DocumentModel.TIPO_PREPEDIDO)
                        TextoCentro("* * * Productos de Prepedido * * *");
                    else TextoCentro("* * * Productos de Entrega * * *");
                }
                else
                {
                    if (customerName.Length > 31)
                        TextoIzquierda("Cliente: " + customerName.Substring(0, 31));
                    else TextoIzquierda("Cliente: " + customerName);
                    TextoCentro("");
                    TextoCentro("* * * Productos * * *");
                }
            }
            else if (Titulo.Equals("Corte de Caja"))
            {
                TextoCentro(await sustituirAcentos("Retiro Número: " + RetiroModel.getWithdrawalNumber(idRetiroOIngreso)));
                await getAWithdrawal(idRetiroOIngreso);
                TextoCentro("* * * Montos Retirados * * *");
            }
            else if (Titulo.Equals("Ingreso a Caja"))
            {
                TextoCentro(await sustituirAcentos("Ingreso Número: " + IngresoModel.getEntryNumber(idRetiroOIngreso)));
                await getAnEntry(idRetiroOIngreso);
                TextoCentro("* * * Montos Ingresados * * *");
            }
            else if (Titulo.Equals("Pago del Cliente"))
            {
                TextoCentro("* * * Pago del Cliente * * *");
            }
            else
            {
                TextoCentro(Titulo);
            }
        }

        private async Task getAWithdrawal(int idRetiro)
        {
            String query = "SELECT * FROM " + LocalDatabase.TABLA_RETIROS + " WHERE " + LocalDatabase.CAMPO_ID_RETIRO + " = " + idRetiro;
            RetiroModel rm = RetiroModel.getARecord(query);
            if (rm != null)
            {
                TextoCentro(""+rm.concept);
                TextoIzquierda("" + rm.description);
            }
        }

        private async Task getAnEntry(int idIngreso)
        {
            String query = "SELECT * FROM " + LocalDatabase.TABLA_INGRESO + " WHERE " + LocalDatabase.CAMPO_ID_INGRESO + " = " + idIngreso;
            IngresoModel im = IngresoModel.getARecord(query);
            if (im != null)
            {
                TextoCentro("" + im.concept);
                TextoIzquierda("" + im.description);
            }
        }

        private async Task<string> sustituirAcentos(String cadena)
        {
            char valuea = 'á';
            char valuee = 'é';
            char valuei = 'í';
            char valueo = 'ó';
            char valueu = 'ú';
            char valueA = 'Á';
            char valueE = 'É';
            char valueI = 'Í';
            char valueO = 'Ó';
            char valueU = 'Ú';
            char valueene = 'ñ';
            char valueEne = 'Ñ';
            char valueArroba = '@';
            cadena.Replace(valuea, (char)160).Replace(valueA, (char)181);
            cadena.Replace(valuee, (char)130).Replace(valueE, (char)144);
            cadena.Replace(valuei, (char)161).Replace(valueI, (char)214);
            cadena.Replace(valueo, (char)162).Replace(valueO, (char)224);
            cadena.Replace(valueu, (char)163).Replace(valueU, (char)233).Replace(valueArroba, (char)64)
                .Replace(valueene, (char)164).Replace(valueEne, (char)165);
            return cadena;
        }

        private string Convertidor(string LetrasNumero)
        {
            //string LetrasNumero = "";
            string[] numero = LetrasNumero.Split('.');
            if (numero.Length > 2)
                numero[1] = numero[1].Substring(0, 2);
            LetrasNumero = ConvertirEnteroTexto(numero[0]);
            if (numero.Length > 1)
                LetrasNumero = LetrasNumero + " PESOS " + numero[1].Substring(0,2) + "/100 M.N.";
            else
                LetrasNumero = LetrasNumero + " PESOS M.N.";
            return LetrasNumero;
        }

        private string ConvertirEnteroTexto(string num)
        {
            string res, dec = "";
            Int64 entero;
            int decimales;
            double nro;

            try
            {
                nro = Convert.ToDouble(num);
            }
            catch
            {
                return "";
            }

            entero = Convert.ToInt64(Math.Truncate(nro));
            decimales = Convert.ToInt32(Math.Round((nro - entero) * 100, 2));
            if (decimales > 0)
            {
                dec = " CON " + decimales.ToString() + "/100";
            }

            res = toText(Convert.ToDouble(entero)) + dec;
            return res;
        }

        private string toText(double value)
        {
            string Num2Text = "";
            value = Math.Truncate(value);
            if (value == 0) Num2Text = "CERO";
            else if (value == 1) Num2Text = "UNO";
            else if (value == 2) Num2Text = "DOS";
            else if (value == 3) Num2Text = "TRES";
            else if (value == 4) Num2Text = "CUATRO";
            else if (value == 5) Num2Text = "CINCO";
            else if (value == 6) Num2Text = "SEIS";
            else if (value == 7) Num2Text = "SIETE";
            else if (value == 8) Num2Text = "OCHO";
            else if (value == 9) Num2Text = "NUEVE";
            else if (value == 10) Num2Text = "DIEZ";
            else if (value == 11) Num2Text = "ONCE";
            else if (value == 12) Num2Text = "DOCE";
            else if (value == 13) Num2Text = "TRECE";
            else if (value == 14) Num2Text = "CATORCE";
            else if (value == 15) Num2Text = "QUINCE";
            else if (value < 20) Num2Text = "DIECI" + toText(value - 10);
            else if (value == 20) Num2Text = "VEINTE";
            else if (value < 30) Num2Text = "VEINTI" + toText(value - 20);
            else if (value == 30) Num2Text = "TREINTA";
            else if (value == 40) Num2Text = "CUARENTA";
            else if (value == 50) Num2Text = "CINCUENTA";
            else if (value == 60) Num2Text = "SESENTA";
            else if (value == 70) Num2Text = "SETENTA";
            else if (value == 80) Num2Text = "OCHENTA";
            else if (value == 90) Num2Text = "NOVENTA";
            else if (value < 100) Num2Text = toText(Math.Truncate(value / 10) * 10) + " Y " + toText(value % 10);
            else if (value == 100) Num2Text = "CIEN";
            else if (value < 200) Num2Text = "CIENTO " + toText(value - 100);
            else if ((value == 200) || (value == 300) || (value == 400) || (value == 600) || (value == 800)) Num2Text = toText(Math.Truncate(value / 100)) + "CIENTOS";
            else if (value == 500) Num2Text = "QUINIENTOS";
            else if (value == 700) Num2Text = "SETECIENTOS";
            else if (value == 900) Num2Text = "NOVECIENTOS";
            else if (value < 1000) Num2Text = toText(Math.Truncate(value / 100) * 100) + " " + toText(value % 100);
            else if (value == 1000) Num2Text = "MIL";
            else if (value < 2000) Num2Text = "MIL " + toText(value % 1000);
            else if (value < 1000000)
            {
                Num2Text = toText(Math.Truncate(value / 1000)) + " MIL";
                if ((value % 1000) > 0) Num2Text = Num2Text + " " + toText(value % 1000);
            }

            else if (value == 1000000) Num2Text = "UN MILLON";
            else if (value < 2000000) Num2Text = "UN MILLON " + toText(value % 1000000);
            else if (value < 1000000000000)
            {
                Num2Text = toText(Math.Truncate(value / 1000000)) + " MILLONES ";
                if ((value - Math.Truncate(value / 1000000) * 1000000) > 0) Num2Text = Num2Text + " " + toText(value - Math.Truncate(value / 1000000) * 1000000);
            }

            else if (value == 1000000000000) Num2Text = "UN BILLON";
            else if (value < 2000000000000) Num2Text = "UN BILLON " + toText(value - Math.Truncate(value / 1000000000000) * 1000000000000);

            else
            {
                Num2Text = toText(Math.Truncate(value / 1000000000000)) + " BILLONES";
                if ((value - Math.Truncate(value / 1000000000000) * 1000000000000) > 0) Num2Text = Num2Text + " " + toText(value - Math.Truncate(value / 1000000000000) * 1000000000000);
            }
            return Num2Text;
        }

        public async Task<int> PaginaPrueba(bool serverModeLAN)
        {
            int value = 0;
            await Task.Run(async () =>
            {
                TextoCentro("");
                TextoCentro("");
                DatosTicketModel dtm = DatosTicketModel.getAllData();
                if (dtm == null)
                {
                    dynamic response = null;
                    if (serverModeLAN)
                        response = await DatosTicketController.downloadAllDatosTicketLAN();
                    else response = await DatosTicketController.downloadAllDatosTicketAPI();
                    if (response.value == 1)
                    {
                        dtm = DatosTicketModel.getAllData();
                    }
                }
                String cuerpoTicket = "";
                ConfiguracionModel cm = ConfiguracionModel.getConfiguration();
                if (cm != null)
                {
                    if (serverModeLAN)
                        cuerpoTicket += "Modo LAN Activado\r\n";
                    else cuerpoTicket += "Modo LAN Desactivado\r\n";
                    cuerpoTicket += "Numero de copias: " + cm.numCopias + "\r\n";

                }
                PrinterModel pm = PrinterModel.getallDataFromAPrinter();
                if (pm != null)
                {
                    cuerpoTicket += "Leyenda Original: " + pm.original + "\r\n";
                    cuerpoTicket += "Leyenda Copia: " + pm.copia + "\r\n";
                }
                await Encabezado("Impresion de Prueba", "Prueba", "", 0, "", "", 0, false, serverModeLAN, 0);
                TextoCentro("");
                TextoCentro(cuerpoTicket);
                TextoCentro("");
                TextoCentro(dtm.PIE_TICKETVENTAS);
                TextoCentro(DateTime.Now.ToString());
                TextoCentro("");
                TextoCentro("");
                TextoCentro("");
                TextoCentro("");
                TextoCentro("");
                TextoCentro("");
                cutTicket();
                printTicket("PRUEBA");
                value = 1;
            });
            return value;
        }
    }



    //Clase para mandara a imprimir texto plano a la impresora
    public class RawPrinterHelper
    {
        // Structure and API declarions:
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public class DOCINFOA
        {
            [MarshalAs(UnmanagedType.LPStr)]
            public string pDocName;
            [MarshalAs(UnmanagedType.LPStr)]
            public string pOutputFile;
            [MarshalAs(UnmanagedType.LPStr)]
            public string pDataType;
        }
        [DllImport("winspool.Drv", EntryPoint = "OpenPrinterA", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern bool OpenPrinter([MarshalAs(UnmanagedType.LPStr)] string szPrinter, out IntPtr hPrinter, IntPtr pd);

        [DllImport("winspool.Drv", EntryPoint = "ClosePrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern bool ClosePrinter(IntPtr hPrinter);

        [DllImport("winspool.Drv", EntryPoint = "StartDocPrinterA", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern bool StartDocPrinter(IntPtr hPrinter, Int32 level, [In, MarshalAs(UnmanagedType.LPStruct)] DOCINFOA di);

        [DllImport("winspool.Drv", EntryPoint = "EndDocPrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern bool EndDocPrinter(IntPtr hPrinter);

        [DllImport("winspool.Drv", EntryPoint = "StartPagePrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern bool StartPagePrinter(IntPtr hPrinter);

        [DllImport("winspool.Drv", EntryPoint = "EndPagePrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern bool EndPagePrinter(IntPtr hPrinter);

        [DllImport("winspool.Drv", EntryPoint = "WritePrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern bool WritePrinter(IntPtr hPrinter, IntPtr pBytes, Int32 dwCount, out Int32 dwWritten);

        // SendBytesToPrinter()
        // When the function is given a printer name and an unmanaged array
        // of bytes, the function sends those bytes to the print queue.
        // Returns true on success, false on failure.
        public static bool SendBytesToPrinter(string szPrinterName, IntPtr pBytes, Int32 dwCount)
        {
            Int32 dwError = 0, dwWritten = 0;
            IntPtr hPrinter = new IntPtr(0);
            DOCINFOA di = new DOCINFOA();
            bool bSuccess = false; // Assume failure unless you specifically succeed.
            try
            {
                di.pDocName = "Ticket de Venta";//Este es el nombre con el que guarda el archivo en caso de no imprimir a la impresora fisica.
                di.pDataType = "RAW";//de tipo texto plano

                // Open the printer.
                if (OpenPrinter(szPrinterName.Normalize(), out hPrinter, IntPtr.Zero))
                {
                    // Start a document.
                    if (StartDocPrinter(hPrinter, 1, di))
                    {
                        // Start a page.
                        if (StartPagePrinter(hPrinter))
                        {
                            // Write your bytes.
                            bSuccess = WritePrinter(hPrinter, pBytes, dwCount, out dwWritten);
                            EndPagePrinter(hPrinter);
                        }
                        EndDocPrinter(hPrinter);
                    }
                    ClosePrinter(hPrinter);
                }
                // If you did not succeed, GetLastError may give more information
                // about why not.
                if (bSuccess == false)
                {
                    dwError = Marshal.GetLastWin32Error();
                }
            } catch (Exception e)
            {
                SECUDOC.writeLog(e.ToString());
            }
            return bSuccess;
        }

        public static bool SendStringToPrinter(string szPrinterName, string szString)
        {
            bool response = false;
            try
            {
                // How many characters are in the string?
                Int32 dwCount = szString.Length;
                // Assume that the printer is expecting ANSI text, and then convert
                // the string to ANSI text.
                IntPtr pBytes = Marshal.StringToCoTaskMemAnsi(szString);
                // Send the converted ANSI string to the printer.
                response = SendBytesToPrinter(szPrinterName, pBytes, dwCount);
                Marshal.FreeCoTaskMem(pBytes);
            } catch (Exception e)
            {
                SECUDOC.writeLog(e.ToString());
            }
            return response;
        }
    }

}
