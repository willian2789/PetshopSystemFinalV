using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using TCC.Models;

namespace TCC.Dados
{
    public class acFuncionario
    {
        Conexao con = new Conexao();

        public void inserirFuncionario(ModelFuncionario cmFunc)
        {
            MySqlCommand cmd = new MySqlCommand("insert into tbl_funcionario values (default,@nome,@nivel,@login,@senha,@img)", con.MyConectarBD());
            cmd.Parameters.Add("@nome", MySqlDbType.VarChar).Value = cmFunc.nm_func;
            cmd.Parameters.Add("@nivel", MySqlDbType.VarChar).Value = cmFunc.nivel_func;
            cmd.Parameters.Add("@login", MySqlDbType.VarChar).Value = cmFunc.login;
            cmd.Parameters.Add("@senha", MySqlDbType.VarChar).Value = cmFunc.senha;
            cmd.Parameters.Add("@img", MySqlDbType.VarChar).Value = cmFunc.image_func;
            cmd.ExecuteNonQuery();
            con.MyDesconectarBD();
        }

        public void TestarUsuario(ModelFuncionario user)
        {
            MySqlCommand cmd = new MySqlCommand("select * from tbl_funcionario where login = @login and senha = @senha", con.MyConectarBD());

            cmd.Parameters.Add("@login", MySqlDbType.VarChar).Value = user.login;
            cmd.Parameters.Add("@senha", MySqlDbType.VarChar).Value = user.senha;

            MySqlDataReader leitor;

            leitor = cmd.ExecuteReader();

            if (leitor.HasRows)
            {
                while (leitor.Read())
                {
                    user.login = Convert.ToString(leitor["login"]);
                    user.senha = Convert.ToString(leitor["senha"]);
                    user.nivel_func = Convert.ToString(leitor["nivel_func"]);
                    user.nm_func = Convert.ToString(leitor["nm_func"]);
                    user.image_func = Convert.ToString(leitor["image_func"]);
                }
            }
            else
            {
                user.login = null;
                user.senha = null;
                user.nivel_func = null;
            }

            con.MyDesconectarBD();
        }

        public List<ModelFuncionario> GetFuncionario()
        {
            List<ModelFuncionario> FuncionarioList = new List<ModelFuncionario>();

            MySqlCommand cmd = new MySqlCommand("select * from tbl_funcionario", con.MyConectarBD());
            MySqlDataAdapter sd = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            sd.Fill(dt);
            con.MyDesconectarBD();

            foreach (DataRow dr in dt.Rows)
            {
                FuncionarioList.Add(
                    new ModelFuncionario
                    {
                        cd_func = Convert.ToString(dr["cd_func"]),
                        nm_func = Convert.ToString(dr["nm_func"]),
                        nivel_func = Convert.ToString(dr["nivel_func"]),
                        login = Convert.ToString(dr["login"]),
                        senha = Convert.ToString(dr["senha"]),
                        image_func = Convert.ToString(dr["image_func"]),
                    });
            }
            return FuncionarioList;
        }

        public void GetFuncionarioId(ModelFuncionario cm)
        {

            MySqlCommand cmd = new MySqlCommand("select * from tbl_funcionario where cd_func=@id ", con.MyConectarBD());
            cmd.Parameters.Add("@id", MySqlDbType.VarChar).Value = cm.cd_func;
            MySqlDataReader dr;

            dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    cm.cd_func = Convert.ToString(dr["cd_func"]);
                    cm.nm_func = Convert.ToString(dr["nm_func"]);
                    cm.nivel_func = Convert.ToString(dr["nivel_func"]);
                    cm.login = Convert.ToString(dr["login"]);
                    cm.senha = Convert.ToString(dr["senha"]);
                    cm.image_func = Convert.ToString(dr["image_func"]);
                }
            }
            else
            {

            }
        }

        public bool AtualizaFuncionario(ModelFuncionario cm)
        {
            MySqlCommand cmd = new MySqlCommand("update tbl_funcionario set nm_func=@nome, login=@login, senha=@senha, image_func=@img where cd_func=@cod", con.MyConectarBD());


            cmd.Parameters.AddWithValue("@nome", cm.nm_func);
            cmd.Parameters.AddWithValue("@login", cm.login);
            cmd.Parameters.AddWithValue("@senha", cm.senha);
            cmd.Parameters.AddWithValue("@img", cm.image_func);
            cmd.Parameters.AddWithValue("@cod", cm.cd_func);


            int i = cmd.ExecuteNonQuery();
            con.MyDesconectarBD();

            if (i >= 1)
                return true;
            else
                return false;
        }

        public bool DeleteFuncionario(int id)
        {
            MySqlCommand cmd = new MySqlCommand("delete from tbl_funcionario where cd_func=@id", con.MyConectarBD());

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
