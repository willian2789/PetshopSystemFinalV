 using System;
using System.Collections.Generic;
using TCC.Models;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using System.Data;

namespace TCC.Dados
{
    public class AcoesCompra
    {
        Conexao con = new Conexao();

        public void inserirCompra(ModelCompra cm)
        {
            MySqlCommand cmd = new MySqlCommand("insert into tbl_compra values(default,@codPag,@codCli,@qt, @valor,@data)", con.MyConectarBD());

            cmd.Parameters.Add("@codPag", MySqlDbType.VarChar).Value = cm.cdPagamento;
            cmd.Parameters.Add("@codCli", MySqlDbType.VarChar).Value = cm.cdClienteCompra;
            cmd.Parameters.Add("@qt", MySqlDbType.VarChar).Value = cm.qt_Produto;
            cmd.Parameters.Add("@valor", MySqlDbType.VarChar).Value = cm.vlTotal;
            cmd.Parameters.Add("@data", MySqlDbType.VarChar).Value = cm.dtCompra;
            cmd.ExecuteNonQuery();
            con.MyDesconectarBD();
        }


        MySqlDataReader dr;
        public void buscaIdCompra(ModelCompra comp)
        {
            MySqlCommand cmd = new MySqlCommand("SELECT cd_Compra FROM tbl_Compra ORDER BY cd_Compra DESC limit 1", con.MyConectarBD());
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                comp.cdCompra = dr[0].ToString();
            }
            con.MyDesconectarBD();
        }

        public List<ModelCompra> GetCompra(ModelCliente cm)
        {
            List<ModelCompra> CompraList = new List<ModelCompra>();

            MySqlCommand cmd = new MySqlCommand("select * from compra_view where cd_cliente = @codCliente order by cd_compra desc;", con.MyConectarBD());
            cmd.Parameters.Add("@codCliente", MySqlDbType.VarChar).Value = cm.cdCliente;
            MySqlDataAdapter sd = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            sd.Fill(dt);
            con.MyDesconectarBD();

            foreach (DataRow dr in dt.Rows)
            {
                CompraList.Add(
                    new ModelCompra
                    {
                        cdCompra = Convert.ToString(dr["cd_compra"]),
                        vlTotal = Convert.ToDouble(dr["vl_total"]),
                        dtCompra = Convert.ToString(dr["dt_compra"]),
                        pagamento = Convert.ToString(dr["ds_pagamento"]),
                    });
            }
            return CompraList;
        }

        public List<ModelProdutoCompra> GetItensCompra(string id)
        {
            List<ModelProdutoCompra> ProdutosCompraList = new List<ModelProdutoCompra>();

            MySqlCommand cmd = new MySqlCommand("select * from vw_carrinho where cd_compra = @id;", con.MyConectarBD());
            cmd.Parameters.Add("@id", MySqlDbType.VarChar).Value = id;
            MySqlDataAdapter sd = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            sd.Fill(dt);
            con.MyDesconectarBD();

            foreach (DataRow dr in dt.Rows)
            {
                ProdutosCompraList.Add(
                    new ModelProdutoCompra
                    {
                        cd_compra = Convert.ToString(dr["cd_compra"]),
                        vl_total = Convert.ToString(dr["vl_total"]),
                        vl_produto = Convert.ToString(dr["vl_produto"]),
                        nm_produto = Convert.ToString(dr["nm_produto"]),
                        image_produto = Convert.ToString(dr["image_produto"]),
                        marca_produto = Convert.ToString(dr["marca_produto"]),
                        ds_categoria = Convert.ToString(dr["ds_categoria"])
                    });
            }
            return ProdutosCompraList;
        }

    }
}