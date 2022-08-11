namespace SyncTPV
{
    class clsCargaPedidoEncabezado
    {
        public int CIDDOCTOPEDIDOCC { get; set; }
        public int CLIENTE_ID { get; set; }
        public string CNOMBRECLIENTE { get; set; }
        public string CNOMBREAGENTECC { get; set; }
        public string CFECHA { get; set; }
        public string CFOLIO { get; set; }
        public double CSUBTOTAL { get; set; }
        public double CDESCUENTO { get; set; }
        public double CTOTAL { get; set; }
        public int SURTIDO { get; set; }

        public ClsClienteModel Cliente { get; set; } = new ClsClienteModel();
    }
}
