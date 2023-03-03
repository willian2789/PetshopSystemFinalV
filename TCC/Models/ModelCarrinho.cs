using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TCC.Models
{
    public class ModelCarrinho
    {
        [Key]
        public Guid ItemPedidoID { get; set; }

        public string cd_carrinho { get; set; }
        public string cd_produto { get; set; }
        public string cd_pagamento { get; set; }
        public string nm_produto { get; set; }
        public string cd_compra { get; set; }
        public double qt_venda { get; set; }
        public double vl_unitario { get; set; }
        public double vl_parcial { get; set; }
        public double vl_total { get; set; }
    }
}