using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TCC.Models;

namespace TCC.Dados
{
    public class acCarrinho
    {
        Conexao con = new Conexao();
        public void inserirCarrinho(ModelCarrinho cm)
        {
            MySqlCommand cmd = new MySqlCommand("insert into tbl_carrinho values(default,@codProd,@codCompra,@codPag,@qtVenda,@vlParcial,@vlTotal)", con.MyConectarBD());

            cmd.Parameters.Add("@codProd", MySqlDbType.VarChar).Value = cm.cd_produto;
            cmd.Parameters.Add("@codCompra", MySqlDbType.VarChar).Value = cm.cd_compra;
            cmd.Parameters.Add("@codPag", MySqlDbType.VarChar).Value = cm.cd_pagamento;
            cmd.Parameters.Add("@qtVenda", MySqlDbType.VarChar).Value = cm.qt_venda;
            cmd.Parameters.Add("@vlParcial", MySqlDbType.VarChar).Value = cm.vl_parcial;
            cmd.Parameters.Add("@vlTotal", MySqlDbType.VarChar).Value = cm.vl_total;
            cmd.ExecuteNonQuery();
            con.MyDesconectarBD();
        }
    }
}