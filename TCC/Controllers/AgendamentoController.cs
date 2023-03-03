using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TCC.Dados;
using TCC.Models;

namespace TCC.Controllers
{
    public class AgendamentoController : Controller
    {
        acAgendamento acAge = new acAgendamento();
        public ActionResult Index()
        {
            return View();
        }

        public void carregaCli()
        {
            List<SelectListItem> cliente = new List<SelectListItem>();

            using (MySqlConnection con = new MySqlConnection("Server=localhost;DataBase=bd_eAnimalcity;User=root;pwd=kauansql2727#"))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand("select * from tbl_cliente", con);
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    cliente.Add(new SelectListItem
                    {
                        Text = rdr[1].ToString(),
                        Value = rdr[0].ToString()
                    });
                }
                con.Close();

            }
            ViewBag.cliente = new SelectList(cliente, "Value", "Text");
        }

        public void carregaPag()
        {
            List<SelectListItem> pagamento = new List<SelectListItem>();

            using (MySqlConnection con = new MySqlConnection("Server=localhost;DataBase=bd_eAnimalcity;User=root;pwd=kauansql2727#"))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand("select * from tbl_pagamento", con);
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    pagamento.Add(new SelectListItem
                    {
                        Text = rdr[1].ToString(),
                        Value = rdr[0].ToString()
                    });
                }
                con.Close();

            }
            ViewBag.pagamento = new SelectList(pagamento, "Value", "Text");
        }

        public void carregaPet()
        {
            List<SelectListItem> pet = new List<SelectListItem>();

            using (MySqlConnection con = new MySqlConnection("Server=localhost;DataBase=bd_eAnimalcity;User=root;pwd=kauansql2727#"))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand("select * from tbl_Pet", con);
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    pet.Add(new SelectListItem
                    {
                        Text = rdr[1].ToString(),
                        Value = rdr[0].ToString()
                    });
                }
                con.Close();

            }
            ViewBag.pet = new SelectList(pet, "Value", "Text");
        }



        public ActionResult CadAge()
        {
            if (Session["usu"] == null)

            {
                return RedirectToAction("SemAcesso", "Home");
            }
            else
            {
                if (Session["usu"].ToString() == "admin")
                {
                    carregaCli();
                    carregaPet();
                    carregaPag();
                    return View();
                }
                if (Session["usu"].ToString() == "comum")
                {
                    carregaCli();
                    carregaPet();
                    carregaPag();
                    return View();
                }
                else
                {
                    return RedirectToAction("SemAcesso", "Home");
                }
            }
        }

        [HttpPost]
        public ActionResult CadAge(ModelAgendamento cmAge)
        {
            carregaCli();
            carregaPet();
            carregaPag();
            cmAge.cd_cliente = Request["cliente"];
            cmAge.cd_pagamento = Request["pagamento"];
            cmAge.cd_pet = Request["pet"];
            acAge.inserirAgendamento(cmAge);
            ViewBag.msgCad = "Agendamento feito com sucesso";
            return View();
        }

        public ActionResult ListarAgendamento()
        {
            return View(acAge.GetAgendamento());
        }

        public ActionResult ExcluirAgendamento(int id)
        {
            acAge.DeletaAgendamento(id);
            return RedirectToAction("ListarAgendamento");

        }
        public ActionResult EditarAgendamento(string id)
        {
            return View(acAge.GetAgendamento().Find(model => model.cd_agendamento == id));
        }



        [HttpPost]
        public ActionResult EditarAgendamento(int id, ModelAgendamento cm)
        {
            cm.cd_agendamento = id.ToString();
            acAge.AtualizaAgendamento(cm);
            return View();
        }
    }
}