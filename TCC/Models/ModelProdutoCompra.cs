using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TCC.Models
{
    public class ModelProdutoCompra
    {
        public string cd_compra { get; set; }
        public string cd_produto { get; set; }
        public string vl_total { get; set; }
        public string vl_produto { get; set; }
        public string nm_produto { get; set; }
        public string image_produto { get; set; }
        public string marca_produto { get; set; }
        public string ds_categoria { get; set; }
        public string qt_produto { get; set; }
        public string ds_prod { get; set; }
    }
}