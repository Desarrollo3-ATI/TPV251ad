using System;
using System.Collections.Generic;

namespace SyncTPV
{
    public class ResponseIE
    {
        public string valor { set; get; }
        public string iddev { set; get; }
        public string descripcion { set; get; }
    }

    public class ResponseDocPendingList
    {
        public List<ResponseValorDescripcion> response = new List<ResponseValorDescripcion>();
    }

    public class ResponseValorDescripcion
    {
        public int valor { set; get; }
        public String descripcion { set; get; }
    }

    public class ResponseActivacion
    {
        public string valor { set; get; }
        public string descripcion { set; get; }
    }
    public class ResponseValidacion
    {
        public string descripcion { set; get; }
        public string valor { set; get; }
    }
}
