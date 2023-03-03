using MySql.Data.MySqlClient;
using TCC.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace TCC.Dados
{
    public class AcoesLoginCliente
    {
        Conexao con = new Conexao();

        public void inserirCliente(ModelCliente modelCliente)
        {
            MySqlCommand cmd = new MySqlCommand("call sp_InserirCliente (@nome , @email, @senha, @noCpf, @imagem, @tel, @nomeLogradouro, @cep, @complemento, @bairro, @numeroLogradouro, 1)", con.MyConectarBD());
            
            cmd.Parameters.Add("@nome", MySqlDbType.VarChar).Value = modelCliente.nmCliente;
            cmd.Parameters.Add("@email", MySqlDbType.VarChar).Value = modelCliente.emailCliente;
            cmd.Parameters.Add("@senha", MySqlDbType.VarChar).Value = modelCliente.senha;
            cmd.Parameters.Add("@imagem", MySqlDbType.VarChar).Value = modelCliente.imageCliente;
            cmd.Parameters.Add("@tel", MySqlDbType.VarChar).Value = modelCliente.noTelefone;
            cmd.Parameters.Add("@nomeLogradouro", MySqlDbType.VarChar).Value = modelCliente.nmlogradouro;
            cmd.Parameters.Add("@cep", MySqlDbType.VarChar).Value = modelCliente.noCep;
            cmd.Parameters.Add("@complemento", MySqlDbType.VarChar).Value = modelCliente.dsComplemento;
            cmd.Parameters.Add("@bairro", MySqlDbType.VarChar).Value = modelCliente.nmBairro;
            cmd.Parameters.Add("@numeroLogradouro", MySqlDbType.VarChar).Value = modelCliente.noLogradouro;
            cmd.Parameters.Add("@noCpf", MySqlDbType.VarChar).Value = modelCliente.cpf_cliente;
            cmd.ExecuteNonQuery();
            con.MyDesconectarBD();
        }

        public void TestarClienteExistente(ModelCliente cm) //verificar se a agenda está reservada
        {
            MySqlCommand cmd = new MySqlCommand("select * from tbl_cliente where email_cliente = @email", con.MyConectarBD());

            cmd.Parameters.Add("@email", MySqlDbType.VarChar).Value = cm.emailCliente;

            MySqlDataReader leitor;

            leitor = cmd.ExecuteReader();

            if (leitor.HasRows)
            {
                while (leitor.Read())
                {
                    cm.confEmailDisponivel = "0";
                }

            }

            else
            {
                cm.confEmailDisponivel = "1";
            }

            con.MyDesconectarBD();
        }



        public DataTable ConsultaCliente()
        {
            MySqlCommand cmd = new MySqlCommand("select * from tbl_cliente", con.MyDesconectarBD());
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            DataTable login = new DataTable();
            da.Fill(login);
            con.MyDesconectarBD();
            return login;
        }

        public void LoginCliente(ModelCliente modelCliente)
        {
            MySqlCommand cmd = new MySqlCommand("select * from tbl_cliente where email_cliente = @email and senha = @senha", con.MyConectarBD());

            cmd.Parameters.Add("@email", MySqlDbType.VarChar).Value = modelCliente.emailCliente;
            cmd.Parameters.Add("@senha", MySqlDbType.VarChar).Value = modelCliente.senha;


            MySqlDataReader leitor;

            leitor = cmd.ExecuteReader();

            if (leitor.HasRows)
            {
                while (leitor.Read())
                {
                    modelCliente.cdCliente = Convert.ToString(leitor["cd_cliente"]);
                    modelCliente.nmCliente = Convert.ToString(leitor["nm_cliente"]);
                    modelCliente.emailCliente = Convert.ToString(leitor["email_cliente"]);
                    modelCliente.imageCliente = Convert.ToString(leitor["image_cliente"]);
                    
                }
            }
            else
            {
                modelCliente.emailCliente = null;
                modelCliente.nmCliente = null;
                modelCliente.imageCliente = null;
                modelCliente.senha = null;
                modelCliente.noTelefone = null;
                modelCliente.nmlogradouro = null;
                modelCliente.noCep = null;
                modelCliente.dsComplemento = null;
                modelCliente.nmBairro = null;
                modelCliente.noLogradouro = null;
            }

            con.MyDesconectarBD();
        }

        public void ObterInformacoesCliente(ModelCliente modelCliente)
        {
            MySqlCommand cmd = new MySqlCommand("select * from tbl_cliente where cd_cliente = @cod", con.MyConectarBD());

            cmd.Parameters.Add("@cod", MySqlDbType.VarChar).Value = modelCliente.cdCliente;
           
            MySqlDataReader leitor;

            leitor = cmd.ExecuteReader();

            if (leitor.HasRows)
            {
                while (leitor.Read())
                {
                    modelCliente.cdCliente = Convert.ToString(leitor["cd_cliente"]);
                    modelCliente.nmCliente = Convert.ToString(leitor["nm_cliente"]);
                    modelCliente.emailCliente = Convert.ToString(leitor["email_cliente"]);
                    modelCliente.senha = Convert.ToString(leitor["senha"]);
                    modelCliente.imageCliente = Convert.ToString(leitor["image_cliente"]);
                    modelCliente.noTelefone = Convert.ToString(leitor["no_telefone"]);
                    modelCliente.nmlogradouro = Convert.ToString(leitor["nm_logradouro"]);
                    modelCliente.noCep = Convert.ToString(leitor["no_Cep"]);
                    modelCliente.cpf_cliente = Convert.ToString(leitor["cpf_cliente"]);
                    modelCliente.dsComplemento = Convert.ToString(leitor["ds_Complemento"]);
                    modelCliente.nmBairro = Convert.ToString(leitor["nm_Bairro"]);
                    modelCliente.noLogradouro = Convert.ToString(leitor["no_Logradouro"]);

                }
            }
            else
            {
                modelCliente.emailCliente = null;
                modelCliente.nmCliente = null;
                modelCliente.imageCliente = null;
                modelCliente.senha = null;
                modelCliente.noTelefone = null;
                modelCliente.nmlogradouro = null;
                modelCliente.noCep = null;
                modelCliente.dsComplemento = null;
                modelCliente.nmBairro = null;
                modelCliente.noLogradouro = null;
            }
            con.MyDesconectarBD();
        }

        public void editarCliente(ModelCliente modelCliente)
        {
            MySqlCommand cmd = new MySqlCommand("update tbl_cliente set nm_cliente=@nome, email_cliente=@email, senha=@senha, cpf_cliente=@noCpf, image_cliente=@imagem, no_telefone=@tel, nm_logradouro=@nomeLogradouro, no_cep=@cep, ds_complemento=@complemento, nm_Bairro=@bairro, no_logradouro=@numeroLogradouro, sg_StatusCli=1 where cd_cliente = @cod", con.MyConectarBD());

            cmd.Parameters.Add("@nome", MySqlDbType.VarChar).Value = modelCliente.nmCliente;
            cmd.Parameters.Add("@email", MySqlDbType.VarChar).Value = modelCliente.emailCliente;
            cmd.Parameters.Add("@senha", MySqlDbType.VarChar).Value = modelCliente.senha;
            cmd.Parameters.Add("@imagem", MySqlDbType.VarChar).Value = modelCliente.imageCliente;
            cmd.Parameters.Add("@tel", MySqlDbType.VarChar).Value = modelCliente.noTelefone;
            cmd.Parameters.Add("@nomeLogradouro", MySqlDbType.VarChar).Value = modelCliente.nmlogradouro;
            cmd.Parameters.Add("@cep", MySqlDbType.VarChar).Value = modelCliente.noCep;
            cmd.Parameters.Add("@complemento", MySqlDbType.VarChar).Value = modelCliente.dsComplemento;
            cmd.Parameters.Add("@bairro", MySqlDbType.VarChar).Value = modelCliente.nmBairro;
            cmd.Parameters.Add("@numeroLogradouro", MySqlDbType.VarChar).Value = modelCliente.noLogradouro;
            cmd.Parameters.Add("@noCpf", MySqlDbType.VarChar).Value = modelCliente.cpf_cliente;
            cmd.Parameters.Add("@cod", MySqlDbType.VarChar).Value = modelCliente.cdCliente;
            cmd.ExecuteNonQuery();
            con.MyDesconectarBD();
        }



    }


}