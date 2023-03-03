using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TCC.Models
{
    public class ModelAgendamento
    {
        public string cd_agendamento { get; set; }
        public string cd_cliente { get; set; }
        public string cd_pagamento { get; set; }
        public string cd_pet { get; set; }
        public string dt_agendamento { get; set; }
        public string vl_total { get; set; }
        public string ds_agendamento { get; set; }
    }
}