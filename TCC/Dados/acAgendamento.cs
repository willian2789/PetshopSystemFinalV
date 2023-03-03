using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using TCC.Models;

namespace TCC.Dados
{
    public class acAgendamento
    {
        Conexao con = new Conexao();


        public void inserirAgendamento(ModelAgendamento cmProd)
        {
            MySqlCommand cmd = new MySqlCommand("insert into tbl_agendamento values (default,@codCli,@codPag,@codPet,@data,@valor,@ds)", con.MyConectarBD());
            cmd.Parameters.Add("@codCli", MySqlDbType.VarChar).Value = cmProd.cd_cliente;
            cmd.Parameters.Add("@codPag", MySqlDbType.VarChar).Value = cmProd.cd_pagamento;
            cmd.Parameters.Add("@codPet", MySqlDbType.VarChar).Value = cmProd.cd_pet;
            cmd.Parameters.Add("@data", MySqlDbType.VarChar).Value = cmProd.dt_agendamento;
            cmd.Parameters.Add("@valor", MySqlDbType.VarChar).Value = cmProd.vl_total;
            cmd.Parameters.Add("@ds", MySqlDbType.VarChar).Value = cmProd.ds_agendamento;
            cmd.ExecuteNonQuery();
            con.MyDesconectarBD();
        }

        public List<ModelAgendamento> GetAgendamento()
        {
            List<ModelAgendamento> AgendamentoList = new List<ModelAgendamento>();

            MySqlCommand cmd = new MySqlCommand("select * from tbl_agendamento", con.MyConectarBD());
            MySqlDataAdapter sd = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            sd.Fill(dt);
            con.MyDesconectarBD();

            foreach (DataRow dr in dt.Rows)
            {
                AgendamentoList.Add(
                    new ModelAgendamento
                    {
                        cd_agendamento = Convert.ToString(dr["cd_agendamento"]),
                        cd_cliente = Convert.ToString(dr["cd_cliente"]),
                        cd_pagamento = Convert.ToString(dr["cd_pagamento"]),
                        cd_pet = Convert.ToString(dr["cd_pet"]),
                        dt_agendamento = Convert.ToString(dr["dt_agendamento"]),
                        vl_total = Convert.ToString(dr["vl_total"]),
                        ds_agendamento = Convert.ToString(dr["ds_agendamento"]),

                    });
            }
            return AgendamentoList;
        }

        public bool AtualizaAgendamento(ModelAgendamento cm)
        {
            MySqlCommand cmd = new MySqlCommand("update tbl_agendamento set  cd_cliente=@codCli,cd_pagamento=@codPag,cd_pet=@codPet,dt_agendamento=@data,vl_total=@vl,ds_agendamento=@ds where cd_agendamento=@cod", con.MyConectarBD());


            cmd.Parameters.AddWithValue("@codCli", cm.cd_cliente);
            cmd.Parameters.AddWithValue("@codPag", cm.cd_pagamento);
            cmd.Parameters.AddWithValue("@codPet", cm.cd_pet);
            cmd.Parameters.AddWithValue("@data", cm.dt_agendamento);
            cmd.Parameters.AddWithValue("@vl", cm.vl_total);
            cmd.Parameters.AddWithValue("@ds", cm.ds_agendamento);
            cmd.Parameters.AddWithValue("@cod", cm.cd_agendamento);


            int i = cmd.ExecuteNonQuery();
            con.MyDesconectarBD();

            if (i >= 1)
                return true;
            else
                return false;
        }


        public bool DeletaAgendamento(int id)
        {
            MySqlCommand cmd = new MySqlCommand("delete from tbl_agendamento where cd_agendamento=@id", con.MyConectarBD());

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