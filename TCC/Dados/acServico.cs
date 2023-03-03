using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using TCC.Models;

namespace TCC.Dados
{
    public class acServico
    {
        Conexao con = new Conexao();

        public void inserirServico(ModelServico cmCat)
        {
            MySqlCommand cmd = new MySqlCommand("insert into tbl_servicos values (default,@valor,@nome)", con.MyConectarBD());
            cmd.Parameters.Add("@valor", MySqlDbType.VarChar).Value = cmCat.vl_servico;
            cmd.Parameters.Add("@nome", MySqlDbType.VarChar).Value = cmCat.nm_servico;
            cmd.ExecuteNonQuery();
            con.MyDesconectarBD();
        }

        public List<ModelServico> GetServico()
        {
            List<ModelServico> ServicoList = new List<ModelServico>();

            MySqlCommand cmd = new MySqlCommand("select * from tbl_servicos", con.MyConectarBD());
            MySqlDataAdapter sd = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            sd.Fill(dt);
            con.MyDesconectarBD();

            foreach (DataRow dr in dt.Rows)
            {
                ServicoList.Add(
                    new ModelServico
                    {
                        cd_servicos = Convert.ToString(dr["cd_servicos"]),
                        vl_servico = Convert.ToString(dr["vl_servico"]),
                        nm_servico = Convert.ToString(dr["nm_servico"]),

                    });
            }
            return ServicoList;
        }
        public void GetServicoId(ModelServico cm)
        {

            MySqlCommand cmd = new MySqlCommand("select * from tbl_servicos where cd_servicos=@id ", con.MyConectarBD());
            cmd.Parameters.Add("@id", MySqlDbType.VarChar).Value = cm.cd_servicos;
            MySqlDataReader dr;

            dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    cm.cd_servicos = Convert.ToString(dr["cd_servicos"]);
                    cm.vl_servico = Convert.ToString(dr["vl_servico"]);
                    cm.nm_servico = Convert.ToString(dr["nm_servico"]);
                }
            }
            else
            {

            }
        }

        public bool AtualizaServico(ModelServico cm)
        {
            MySqlCommand cmd = new MySqlCommand("update tbl_servicos set vl_servico=@valor, nm_servico=@nome where cd_servicos=@cod", con.MyConectarBD());


            cmd.Parameters.AddWithValue("@valor", cm.vl_servico);
            cmd.Parameters.AddWithValue("@nome", cm.nm_servico);
            cmd.Parameters.AddWithValue("@cod", cm.cd_servicos);


            int i = cmd.ExecuteNonQuery();
            con.MyDesconectarBD();

            if (i >= 1)
                return true;
            else
                return false;
        }

        public bool DeleteServico(int id)
        {
            MySqlCommand cmd = new MySqlCommand("delete from tbl_servicos where cd_servicos=@id", con.MyConectarBD());

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