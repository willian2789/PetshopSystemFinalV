using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace TCC.Models
{
    public class ModelCliente
    {
        public string cdCliente { get; set; }

        [DisplayName("Nome")]
        public string nmCliente { get; set; }

        [DisplayName("Email")]
        public string emailCliente { get; set; }

        [DisplayName("Senha")]
        public string senha { get; set; }

        [DisplayName("Imagem")]
        public string imageCliente { get; set; }

        [DisplayName("Telefone")]
        public string noTelefone { get; set; }

        [DisplayName("Logradouro")]
        public string nmlogradouro { get; set; }

        [DisplayName("Cep")]
        public string noCep { get; set; }

        [DisplayName("Complemento")]
        public string dsComplemento { get; set; }

        [DisplayName("Bairro")]
        public string nmBairro { get; set; }

        [DisplayName("Número")]
        public string noLogradouro { get; set; }
        public string sgStatusCli { get; set; }

        [DisplayName("Confirmar senha")]
        public string confSenha { get; set; }


        [DisplayName("CPF")]
        public string cpf_cliente { get; set; }



        //Confirmar se já existe o usuário Disponivel
        public string confEmailDisponivel { get; set; }




    }
}