using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TCC.Models
{
    public class ModelFuncionario
    {
        public string cd_func { get; set; }
        public string nm_func { get; set; }
        public string nivel_func { get; set; }
        public string login { get; set; }
        public string senha { get; set; }
        public string conf_senha { get; set; }
        public string image_func { get; set; }
    }
}