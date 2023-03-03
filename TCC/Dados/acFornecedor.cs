using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using TCC.Models;

namespace TCC.Dados
{
    public class acFornecedor
    {
        Conexao con = new Conexao();

        public void inserirFornecedor(ModelFornecedor cmForn)
        {
            MySqlCommand cmd = new MySqlCommand("insert into tbl_fornecedor values (default,@nome,@tel,@email,@cnpj)", con.MyConectarBD());
            cmd.Parameters.Add("@nome", MySqlDbType.VarChar).Value = cmForn.nm_fornecedor;
            cmd.Parameters.Add("@tel", MySqlDbType.VarChar).Value = cmForn.no_telefone;
            cmd.Parameters.Add("@email", MySqlDbType.VarChar).Value = cmForn.email_fornecedor;
            cmd.Parameters.Add("@cnpj", MySqlDbType.VarChar).Value = cmForn.cnpj_fornecedor;
            cmd.ExecuteNonQuery();
            con.MyDesconectarBD();
        }

        public List<ModelFornecedor> GetFornecedor()
        {
            List<ModelFornecedor> FornecedorList = new List<ModelFornecedor>();

            MySqlCommand cmd = new MySqlCommand("select * from tbl_fornecedor", con.MyConectarBD());
            MySqlDataAdapter sd = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            sd.Fill(dt);
            con.MyDesconectarBD();

            foreach (DataRow dr in dt.Rows)
            {
                FornecedorList.Add(
                    new ModelFornecedor
                    {
                        cd_fornecedor = Convert.ToString(dr["cd_fornecedor"]),
                        nm_fornecedor = Convert.ToString(dr["nm_fornecedor"]),
                        no_telefone = Convert.ToString(dr["no_telefone"]),
                        email_fornecedor = Convert.ToString(dr["email_fornecedor"]),
                        cnpj_fornecedor = Convert.ToString(dr["cnpj_fornecedor"]),
                    });
            }
            return FornecedorList;
        }

        public void GetFornecedorId(ModelFornecedor cm)
        {

            MySqlCommand cmd = new MySqlCommand("select * from tbl_fornecedor where cd_fornecedor=@id ", con.MyConectarBD());
            cmd.Parameters.Add("@id", MySqlDbType.VarChar).Value = cm.cd_fornecedor;
            MySqlDataReader dr;

            dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    cm.cd_fornecedor = Convert.ToString(dr["cd_fornecedor"]);
                    cm.nm_fornecedor = Convert.ToString(dr["nm_fornecedor"]);
                    cm.no_telefone = Convert.ToString(dr["no_telefone"]);
                    cm.email_fornecedor = Convert.ToString(dr["email_fornecedor"]);
                    cm.cnpj_fornecedor = Convert.ToString(dr["cnpj_fornecedor"]);
                }
            }
            else
            {

            }
        }

        public bool AtualizaFornecedor(ModelFornecedor cm)
        {
            MySqlCommand cmd = new MySqlCommand("update tbl_fornecedor set nm_fornecedor=@nome, email_fornecedor=@email, cnpj_fornecedor=@cnpj where cd_fornecedor=@cod", con.MyConectarBD());


            cmd.Parameters.AddWithValue("@nome", cm.nm_fornecedor);
            cmd.Parameters.AddWithValue("@email", cm.email_fornecedor);
            cmd.Parameters.AddWithValue("@cnpj", cm.cnpj_fornecedor);
            cmd.Parameters.AddWithValue("@cod", cm.cd_fornecedor);


            int i = cmd.ExecuteNonQuery();
            con.MyDesconectarBD();

            if (i >= 1)
                return true;
            else
                return false;
        }

        public bool DeleteFornecedor(int id)
        {
            MySqlCommand cmd = new MySqlCommand("delete from tbl_fornecedor where cd_fornecedor=@id", con.MyConectarBD());

            cmd.Parameters.AddWithValue("@id", id);


            int i = cmd.ExecuteNonQuery();
            con.MyDesconectarBD();

            if (i >= 1)
                return true;
            else
                return false;
        }

    }
}