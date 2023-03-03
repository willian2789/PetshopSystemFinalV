using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TCC.Models
{
    public class ModelCompra
    {
        public string cdCompra { get; set; }
        public double vlTotal { get; set; }
        public string dtCompra { get; set; }
        public string usuId { get; set; }

        public string cdPagamento { get; set; }
        public string pagamento { get; set; }
        
        public string cdClienteCompra { get; set; }
        public string nmClienteCompra { get; set; }
        public string cpfClienteCompra { get; set; }
        public string qt_Produto { get; set; }
        public double vl_unitario { get; set; }
        public double vl_parcial { get; set; }

        public List<ModelCarrinho> ItensPedido = new List<ModelCarrinho>();

    }
}