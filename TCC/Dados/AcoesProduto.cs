using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using TCC.Models;

namespace TCC.Dados
{
    public class AcoesProduto
    {
        Conexao con = new Conexao();

        public void inserirProdutos(ModelProduto cmProd)
        {
            MySqlCommand cmd = new MySqlCommand("insert into tbl_Produto values (default,@cod_forn,@codCat,@nm_prod,@image,@marca,@qt,@valor,@ds_Prod)", con.MyConectarBD());
            cmd.Parameters.Add("@cod_forn", MySqlDbType.VarChar).Value = cmProd.cd_fornecedor;
            cmd.Parameters.Add("@nm_prod", MySqlDbType.VarChar).Value = cmProd.nm_produto;
            cmd.Parameters.Add("@image", MySqlDbType.VarChar).Value = cmProd.image_produto;
            cmd.Parameters.Add("@codCat", MySqlDbType.VarChar).Value = cmProd.cd_categoria;
            cmd.Parameters.Add("@marca", MySqlDbType.VarChar).Value = cmProd.marca_produto;
            cmd.Parameters.Add("@qt", MySqlDbType.VarChar).Value = cmProd.qt_produto;
            cmd.Parameters.Add("@valor", MySqlDbType.VarChar).Value = cmProd.vl_produto;
            cmd.Parameters.Add("@ds_Prod", MySqlDbType.VarChar).Value = cmProd.ds_prod;
            cmd.ExecuteNonQuery();
            con.MyDesconectarBD();
        }

        //Arrumar problema de conflito ao alterar
        public bool AtualizaProduto(ModelProduto cm)
        {
            MySqlCommand cmd = new MySqlCommand("update tbl_Produto set cd_fornecedor=@codforn,nm_produto=@nm_prod,cd_categoria=@codCat,qt_produto=@qt,vl_produto=@valor,marca_produto=@marca,ds_prod=@ds  where cd_produto=@cod", con.MyConectarBD());


            cmd.Parameters.AddWithValue("@codforn", cm.cd_fornecedor);
            cmd.Parameters.AddWithValue("@nm_prod", cm.nm_produto);
            cmd.Parameters.AddWithValue("@image", cm.image_produto);
            cmd.Parameters.AddWithValue("@codCat", cm.cd_categoria);
            cmd.Parameters.AddWithValue("@qt", cm.qt_produto);
            cmd.Parameters.AddWithValue("@valor", Convert.ToDecimal(cm.vl_produto));
            cmd.Parameters.AddWithValue("@marca", cm.marca_produto);
            cmd.Parameters.AddWithValue("@ds", cm.ds_prod);
            cmd.Parameters.AddWithValue("@cod", cm.cd_produto);


            int i = cmd.ExecuteNonQuery();
            con.MyDesconectarBD();

            if (i >= 1)
                return true;
            else
                return false;
        }
        public bool DeletaProduto(int id)
        {
            MySqlCommand cmd = new MySqlCommand("delete from tbl_Produto where cd_produto=@id", con.MyConectarBD());

            cmd.Parameters.AddWithValue("@id", id);


            int i = cmd.ExecuteNonQuery();
            con.MyDesconectarBD();

            if (i >= 1)
                return true;
            else
                return false;
        }
        public List<ModelProduto> GetConsProd(int id)
        {
            List<ModelProduto> Produtolist = new List<ModelProduto>();

            MySqlCommand cmd = new MySqlCommand("select * from tbl_Produto where cd_produto=@cod", con.MyConectarBD());
            cmd.Parameters.AddWithValue("@cod", id);
            MySqlDataAdapter sd = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            sd.Fill(dt);
            con.MyDesconectarBD();

            foreach (DataRow dr in dt.Rows)
            {
                Produtolist.Add(
                    new ModelProduto
                    {
                        cd_produto = Convert.ToString(dr["cd_produto"]),
                        cd_fornecedor = Convert.ToString(dr["cd_fornecedor"]),
                        cd_categoria = Convert.ToString(dr["cd_categoria"]),
                        nm_produto = Convert.ToString(dr["nm_produto"]),
                        image_produto = Convert.ToString(dr["image_produto"]),
                        marca_produto = Convert.ToString(dr["marca_produto"]),
                        qt_produto = Convert.ToString(dr["qt_produto"]),
                        vl_produto = Convert.ToString(dr["vl_produto"]),
                        ds_prod = Convert.ToString(dr["ds_prod"]),
                    });
            }
            return Produtolist;
        }

        public List<ModelProduto> GetProduto(ModelProduto cm)
        {
            List<ModelProduto> ProdutoList = new List<ModelProduto>();

            MySqlCommand cmd = new MySqlCommand("select * from vw_produto order by cd_produto desc;", con.MyConectarBD());
            MySqlDataAdapter sd = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            sd.Fill(dt);
            con.MyDesconectarBD();

            foreach (DataRow dr in dt.Rows)
            {
                ProdutoList.Add(
                    new ModelProduto
                    {
                        cd_produto = Convert.ToString(dr["cd_produto"]),
                        nm_produto = Convert.ToString(dr["nm_produto"]),
                        image_produto = Convert.ToString(dr["image_produto"]),
                        marca_produto = Convert.ToString(dr["marca_produto"]),
                        qt_produto = Convert.ToString(dr["qt_produto"]),
                        vl_produto = Convert.ToString(dr["vl_produto"]),
                        ds_prod = Convert.ToString(dr["ds_prod"]),
                        ds_categoria = Convert.ToString(dr["ds_categoria"]),
                        nm_fornecedor = Convert.ToString(dr["nm_fornecedor"])
            });
            }
            return ProdutoList;
        }

        public void GetProdutoId(ModelProduto cm)
        {

            MySqlCommand cmd = new MySqlCommand("select * from vw_produto where cd_produto=@id ", con.MyConectarBD());
            cmd.Parameters.Add("@id", MySqlDbType.VarChar).Value = cm.cd_produto;
            MySqlDataReader dr;

            dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    cm.cd_produto = Convert.ToString(dr["cd_produto"]);
                    cm.nm_produto = Convert.ToString(dr["nm_produto"]);
                    cm.image_produto = Convert.ToString(dr["image_produto"]);
                    cm.marca_produto = Convert.ToString(dr["marca_produto"]);
                    cm.qt_produto = Convert.ToString(dr["qt_produto"]);
                    cm.vl_produto = Convert.ToString(dr["vl_produto"]);   
                    cm.ds_prod = Convert.ToString(dr["ds_prod"]);
                    cm.nm_fornecedor = Convert.ToString(dr["nm_fornecedor"]);
                    cm.ds_categoria = Convert.ToString(dr["ds_categoria"]);
                }
            }
            else
            {

            }
        }

        public List<ModelProduto> GetProdutoPorCategoria(ModelProduto cm)
        {
            List<ModelProduto> ProdutoList = new List<ModelProduto>();

            MySqlCommand cmd = new MySqlCommand("select * from vw_produto where cd_categoria = @cat order by cd_produto desc;", con.MyConectarBD());
            cmd.Parameters.Add("@cat", MySqlDbType.VarChar).Value = cm.cd_categoria;
            MySqlDataAdapter sd = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            sd.Fill(dt);
            con.MyDesconectarBD();

            foreach (DataRow dr in dt.Rows)
            {
                ProdutoList.Add(
                    new ModelProduto
                    {
                        cd_produto = Convert.ToString(dr["cd_produto"]),
                        nm_produto = Convert.ToString(dr["nm_produto"]),
                        image_produto = Convert.ToString(dr["image_produto"]),
                        marca_produto = Convert.ToString(dr["marca_produto"]),
                        qt_produto = Convert.ToString(dr["qt_produto"]),
                        vl_produto = Convert.ToString(dr["vl_produto"]),
                        ds_prod = Convert.ToString(dr["ds_prod"]),
                        nm_fornecedor = Convert.ToString(dr["nm_fornecedor"]),
                        ds_categoria = Convert.ToString(dr["ds_categoria"])
                    });
            }
            return ProdutoList;
        }

        public List<ModelProduto> GetProdutoPorBusca(ModelProduto cm)
        {
            List<ModelProduto> ProdutoList = new List<ModelProduto>();

            MySqlCommand cmd = new MySqlCommand("select * from vw_produto where nm_produto like " +
                "'%" + cm.nm_produto +"%' order by cd_produto desc;", con.MyConectarBD());
            cmd.Parameters.Add("@texto", MySqlDbType.VarChar).Value = cm.nm_produto;
            MySqlDataAdapter sd = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            sd.Fill(dt);
            con.MyDesconectarBD();

            foreach (DataRow dr in dt.Rows)
            {
                ProdutoList.Add(
                    new ModelProduto
                    {
                        cd_produto = Convert.ToString(dr["cd_produto"]),
                        nm_produto = Convert.ToString(dr["nm_produto"]),
                        image_produto = Convert.ToString(dr["image_produto"]),
                        marca_produto = Convert.ToString(dr["marca_produto"]),
                        qt_produto = Convert.ToString(dr["qt_produto"]),
                        vl_produto = Convert.ToString(dr["vl_produto"]),
                        ds_prod = Convert.ToString(dr["ds_prod"]),
                        ds_categoria = Convert.ToString(dr["ds_categoria"]),
                        nm_fornecedor = Convert.ToString(dr["nm_fornecedor"])
                    });
            }
            return ProdutoList;
        }

        public void ShowProductDetails(ModelProduto cm)
        {
            MySqlCommand cmd = new MySqlCommand("select * from vw_produto where cd_produto = @id;", con.MyConectarBD());
            cmd.Parameters.Add("@id", MySqlDbType.VarChar).Value = cm.cd_produto;
            MySqlDataReader dr;

            dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    cm.cd_produto = Convert.ToString(dr["cd_produto"]);
                    cm.nm_produto = Convert.ToString(dr["nm_produto"]);
                    cm.image_produto = Convert.ToString(dr["image_produto"]);
                    cm.marca_produto = Convert.ToString(dr["marca_produto"]);
                    cm.qt_produto = Convert.ToString(dr["qt_produto"]);
                    cm.vl_produto = Convert.ToString(dr["vl_produto"]);
                    cm.ds_prod = Convert.ToString(dr["ds_prod"]);
                    cm.ds_categoria = Convert.ToString(dr["ds_categoria"]);
                    cm.nm_fornecedor = Convert.ToString(dr["nm_fornecedor"]);
                }
            }
            else
            {

            }
        }


    }
}