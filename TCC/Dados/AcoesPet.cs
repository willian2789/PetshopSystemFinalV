using MySql.Data.MySqlClient;
using TCC.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace TCC.Dados
{
    public class AcoesPet
    {
        Conexao con = new Conexao();

        public List<ModelPet> GetPet(ModelCliente cm)
        {
            List<ModelPet> Petlist = new List<ModelPet>();

            MySqlCommand cmd = new MySqlCommand("select * from pet_view where cd_cliente = @codCliente order by cd_pet desc;", con.MyConectarBD());
            cmd.Parameters.Add("@codCliente", MySqlDbType.VarChar).Value = cm.cdCliente;
            MySqlDataAdapter sd = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            sd.Fill(dt);
            con.MyDesconectarBD();

            foreach (DataRow dr in dt.Rows)
            {
                Petlist.Add(
                    new ModelPet
                    {
                        codPet = Convert.ToString(dr["cd_pet"]),
                        nomePet = Convert.ToString(dr["nm_pet"]),
                        imagePet = Convert.ToString(dr["image_pet"]),
                        racaPet = Convert.ToString(dr["raca_pet"]),
                        portePet = Convert.ToString(dr["porte_pet"]),
                        sexoPet = Convert.ToString(dr["sexo_pet"]),
                        especiePet = Convert.ToString(dr["nm_especie"]),
                    });
            }
            return Petlist;
        }

        public void GetPetForEdit(ModelPet cm)
        {
            MySqlCommand cmd = new MySqlCommand("select * from pet_view where cd_pet = @id;", con.MyConectarBD());
            cmd.Parameters.Add("@id", MySqlDbType.VarChar).Value = cm.codPet;
            MySqlDataReader dr;

            dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    cm.codPet = Convert.ToString(dr["cd_pet"]);
                    cm.nomePet = Convert.ToString(dr["nm_pet"]);
                    cm.imagePet = Convert.ToString(dr["image_pet"]);
                    cm.racaPet = Convert.ToString(dr["raca_pet"]);
                    cm.portePet = Convert.ToString(dr["porte_pet"]);
                    cm.sexoPet = Convert.ToString(dr["sexo_pet"]);
                    cm.codEspeciePet = Convert.ToString(dr["cd_especie"]);
                    cm.especiePet = Convert.ToString(dr["nm_especie"]);
                }
            }
            else
            {

            }
        }


        public void ApagarPet(string id)
        {
            MySqlCommand cmd = new MySqlCommand("delete from tbl_pet where cd_pet = @id;", con.MyConectarBD());
            cmd.Parameters.Add("@id", MySqlDbType.VarChar).Value = id;
            cmd.ExecuteNonQuery();
            con.MyDesconectarBD();
        }


        public void inserirPet(ModelPet modelPet)
        {
            MySqlCommand cmd = new MySqlCommand("call sp_inserirPet(@nome, @image, @raca, @sexo, @porte, @especie, @donoPet);", con.MyConectarBD());



            cmd.Parameters.Add("@nome", MySqlDbType.VarChar).Value = modelPet.nomePet;
            cmd.Parameters.Add("@image", MySqlDbType.VarChar).Value = modelPet.imagePet;
            cmd.Parameters.Add("@raca", MySqlDbType.VarChar).Value = modelPet.racaPet;
            cmd.Parameters.Add("@sexo", MySqlDbType.VarChar).Value = modelPet.sexoPet;
            cmd.Parameters.Add("@porte", MySqlDbType.VarChar).Value = modelPet.portePet;
            cmd.Parameters.Add("@especie", MySqlDbType.VarChar).Value = modelPet.codEspeciePet;
            cmd.Parameters.Add("@donoPet", MySqlDbType.VarChar).Value = modelPet.codClientePet;

            cmd.ExecuteNonQuery();
            con.MyDesconectarBD();
        }

        public void editarPet(ModelPet modelPet, string id)
        {
            MySqlCommand cmd = new MySqlCommand("update tbl_pet set nm_pet=@nome, image_pet=@image, raca_pet=@raca, sexo_pet=@sexo, porte_pet=@porte, cd_especie=@especie where cd_pet = @id", con.MyConectarBD());

            cmd.Parameters.Add("@id", MySqlDbType.VarChar).Value = id;
            cmd.Parameters.Add("@nome", MySqlDbType.VarChar).Value = modelPet.nomePet;
            cmd.Parameters.Add("@image", MySqlDbType.VarChar).Value = modelPet.imagePet;
            cmd.Parameters.Add("@raca", MySqlDbType.VarChar).Value = modelPet.racaPet;
            cmd.Parameters.Add("@sexo", MySqlDbType.VarChar).Value = modelPet.sexoPet;
            cmd.Parameters.Add("@porte", MySqlDbType.VarChar).Value = modelPet.portePet;
            cmd.Parameters.Add("@especie", MySqlDbType.VarChar).Value = modelPet.codEspeciePet;
            cmd.Parameters.Add("@donoPet", MySqlDbType.VarChar).Value = modelPet.codClientePet;
            cmd.ExecuteNonQuery();
            con.MyDesconectarBD();
        }


    }
}