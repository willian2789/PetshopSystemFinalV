using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using TCC.Models;

namespace TCC.Dados
{
    public class acCategoria
    {
        Conexao con = new Conexao();


        public void inserirCategoria(ModelCategoria cmCat)
        {
            MySqlCommand cmd = new MySqlCommand("insert into tbl_categoria values (default,@categoria)", con.MyConectarBD());
            cmd.Parameters.Add("@categoria", MySqlDbType.VarChar).Value = cmCat.ds_categoria;
            cmd.ExecuteNonQuery();
            con.MyDesconectarBD();
        }

        public List<ModelCategoria> GetCategoria()
        {
            List<ModelCategoria> CategoriaList = new List<ModelCategoria>();

            MySqlCommand cmd = new MySqlCommand("select * from tbl_categoria", con.MyConectarBD());
            MySqlDataAdapter sd = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            sd.Fill(dt);
            con.MyDesconectarBD();

            foreach (DataRow dr in dt.Rows)
            {
                CategoriaList.Add(
                    new ModelCategoria
                    {
                        cd_categoria = Convert.ToString(dr["cd_categoria"]),
                        ds_categoria = Convert.ToString(dr["ds_categoria"]),

                    });
            }
            return CategoriaList;
        }

        public void GetCategoriaId(ModelCategoria cm)
        {

            MySqlCommand cmd = new MySqlCommand("select * from tbl_categoria where cd_categoria=@id ", con.MyConectarBD());
            cmd.Parameters.Add("@id", MySqlDbType.VarChar).Value = cm.cd_categoria;
            MySqlDataReader dr;

            dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    cm.cd_categoria = Convert.ToString(dr["cd_categoria"]);
                    cm.ds_categoria = Convert.ToString(dr["ds_categoria"]);
                }
            }
            else
            {

            }
        }

        public bool AtualizaCategoria(ModelCategoria cm)
        {
            MySqlCommand cmd = new MySqlCommand("update tbl_categoria set ds_categoria=@categoria where cd_categoria=@cod", con.MyConectarBD());


            cmd.Parameters.AddWithValue("@categoria", cm.ds_categoria);
            cmd.Parameters.AddWithValue("@cod", cm.cd_categoria);


            int i = cmd.ExecuteNonQuery();
            con.MyDesconectarBD();

            if (i >= 1)
                return true;
            else
                return false;
        }

        public bool DeleteCategoria(int id)
        {
            MySqlCommand cmd = new MySqlCommand("delete from tbl_categoria where cd_categoria=@id", con.MyConectarBD());

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