using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TCC.Models;
using TCC.Dados;
using System.IO;
using System.Web.Security;
using System.Text.RegularExpressions;
using MySql.Data.MySqlClient;

namespace TCC.Controllers
{
    public class ClienteController : Controller
    {
        public void selecionaCliente(ModelCliente cm)
        {
            cm.cdCliente = Session["codCliente"].ToString();
            acCliente.ObterInformacoesCliente(cm);
            ViewBag.nome = cm.nmCliente;
            ViewBag.email = cm.emailCliente;
            ViewBag.senha = cm.senha;
            ViewBag.img = cm.imageCliente;
            ViewBag.cpf = cm.cpf_cliente;

            ViewBag.tel = cm.noTelefone;

            ViewBag.cep = cm.noCep;
            ViewBag.nmLog = cm.nmlogradouro;
            ViewBag.noLog = cm.noLogradouro;

            ViewBag.bairro = cm.nmBairro;
            ViewBag.comp = cm.dsComplemento;
        }

        public void selecionaPet(ModelPet cm)
        {
            ViewBag.nomePet = cm.nomePet;
            ViewBag.imagePet = cm.imagePet;
            ViewBag.racaPet = cm.racaPet;
            ViewBag.portePet = cm.portePet;
            ViewBag.sexoPet = cm.sexoPet;
            ViewBag.codEspecie = cm.codEspeciePet;
            ViewBag.especiePet = cm.especiePet;  
        }


        AcoesLoginCliente acCliente = new AcoesLoginCliente();
        AcoesPet acPet = new AcoesPet();
        AcoesCompra acCompra = new AcoesCompra();

        public ActionResult CadastroCliente()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CadastroCliente(ModelCliente cm, HttpPostedFileBase file)
        {
            acCliente.TestarClienteExistente(cm);

            if (cm.confEmailDisponivel == "0")
            {
                ViewBag.msg = "Email já cadastrado!";
                ViewBag.color = "";

            }
            else if (cm.senha != cm.confSenha)
            {
                ViewBag.msg = "Senhas não conferem";
                ViewBag.color = "";
            }
            else
            {
                //string arquivo = Path.GetFileName(file.FileName = "userAnonymous.png");
                string arquivo, path, file2;
                if(file == null)
                {
                    arquivo = Path.GetFileName("userAnonymous.png");
                    path = Path.Combine(Server.MapPath("~/Images/Clientes/"), arquivo);
                    file2 = "/Images/Clientes/" + arquivo;
                    cm.imageCliente = file2;
                }
                else
                {
                    arquivo = Path.GetFileName(file.FileName);
                    path = Path.Combine(Server.MapPath("~/Images/Clientes/"), arquivo);
                    file2 = "/Images/Clientes/" + arquivo;
                    file.SaveAs(path);
                    cm.imageCliente = file2;
                }



                acCliente.inserirCliente(cm);
                ViewBag.color = "success";
                ViewBag.msg = "Cadastro realizado com sucesso";
            }

            return View();
        }

        
        public ActionResult LoginCliente()
        {
            if(Session["logged"] == null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }

        }

        [HttpPost]
        public ActionResult LoginCliente(ModelCliente cm)
        {
            acCliente.LoginCliente(cm); // Verificar se usu e senha estão corretos

            if (cm.emailCliente != null && cm.senha != null)
            {
                FormsAuthentication.SetAuthCookie(cm.nmCliente, false);

                Session["logged"] = "yes";
                Session["usuName"] = cm.nmCliente;
                Session["usuImg"] = cm.imageCliente;
                Session["codCliente"] = cm.cdCliente;
                        
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.msg = "Usuário Incorreto. Verifique o nome de usuário e a senha";
                return View();
            }

        }

        public ActionResult Logout()
        {
            Session["logged"] = null;
            Session["usuName"] = null;
            Session["usuImg"] = null;
            Session["codCliente"] = null;
            return RedirectToAction("Index", "Home");
        }


        public ActionResult MeuPerfil(ModelCliente cm)
        {
            if (Session["logged"] == null)
            {
                return RedirectToAction("SemAcesso", "Cliente");
            }
            else
            {
                cm.cdCliente = Session["codCliente"].ToString();
                acCliente.ObterInformacoesCliente(cm);
                selecionaCliente(cm);


                return View();
            }
        }
        public static string msg;

        public ActionResult EditarPerfil(ModelCliente cm)
        {
            if (Session["logged"] == null)
            {
                return RedirectToAction("SemAcesso", "Cliente");
            }
            else

            {
                ViewBag.msg = msg;
                cm.cdCliente = Session["codCliente"].ToString();
                acCliente.ObterInformacoesCliente(cm);
                selecionaCliente(cm);
                return View();
            }
        }

        [HttpPost]
        public ActionResult EditarPerfil(ModelCliente cm, HttpPostedFileBase file)
        {
            if (Session["logged"] == null)
            {
                return RedirectToAction("SemAcesso", "Cliente");
            }
            else if (cm.senha != cm.confSenha)
            {
                ViewBag.msg = "Senhas não conferem";
                msg = ViewBag.msg;
                ViewBag.color = "";
                return RedirectToAction("EditarPerfil", "Cliente");
            }
            else
            {
                string arquivo, path, file2;
                if (file == null)
                {
                    cm.imageCliente = Session["usuImg"].ToString();
                }
                else
                {
                    arquivo = Path.GetFileName(file.FileName);
                    path = Path.Combine(Server.MapPath("~/Images/Clientes/"), arquivo);
                    file2 = "/Images/Clientes/" + arquivo;
                    file.SaveAs(path);
                    cm.imageCliente = file2;
                }
                cm.cdCliente = Session["codCliente"].ToString();
                acCliente.editarCliente(cm);
                ViewBag.msg = "Alteração concluida com sucesso";
                return RedirectToAction("MeuPerfil", "Cliente");

            }
        }

        public ActionResult MeusPets(ModelCliente cm)
        {
            if (Session["logged"] == null)
            {
                return RedirectToAction("SemAcesso", "Cliente");
            }
            else
            {
                cm.cdCliente = Session["codCliente"].ToString();
                acCliente.ObterInformacoesCliente(cm);
                selecionaCliente(cm);
                return View(acPet.GetPet(cm));
            }
        }

        public ActionResult SemAcesso()
        {
            return View();
        }


        // ============ [ PARTE DO PET ] ================

        public void carregaEspecie()
        {
            List<SelectListItem> especie = new List<SelectListItem>();

            using (MySqlConnection con = new MySqlConnection("Server=localhost; DataBase=bd_eAnimalcity; User=root; pwd=kauansql2727#"))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand("select * from tbl_especie", con);
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    especie.Add(new SelectListItem
                    {
                        Text = rdr[1].ToString(),
                        Value = rdr[0].ToString()
                    });
                }
                con.Close();

            }

            ViewBag.especie = new SelectList(especie, "Value", "Text");
        }


        public ActionResult AdicionarPet()
        {
            if (Session["logged"] == null)
            {
               return RedirectToAction("SemAcesso", "Cliente");
            }
            else
            {
                carregaEspecie();
                return View();
            }
        }

        [HttpPost]
        public ActionResult AdicionarPet(ModelPet cm, HttpPostedFileBase file)
        {


            if (Session["logged"] == null)
            {
                return RedirectToAction("SemAcesso", "Cliente");
            }
            else
            {
                carregaEspecie();
                string arquivo, path, file2;
                if (file == null)
                {
                    arquivo = Path.GetFileName("petAnonymous.png");
                    path = Path.Combine(Server.MapPath("~/Images/Pets/"), arquivo);
                    file2 = "/Images/Pets/" + arquivo;
                    cm.imagePet = file2;
                }
                else
                {
                    arquivo = Path.GetFileName(file.FileName);
                    path = Path.Combine(Server.MapPath("~/Images/Pets/"), arquivo);
                    file2 = "/Images/Pets/" + arquivo;
                    file.SaveAs(path);
                    cm.imagePet = file2;
                }

                cm.codClientePet = Session["codCliente"].ToString();
                cm.sexoPet = Request["sexo"];
                cm.codEspeciePet = Request["especie"];
                cm.portePet = Request["porte"];

                acPet.inserirPet(cm);
                ViewBag.color = "success";
                ViewBag.msg = "Cadastro realizado com sucesso";
            }

            return View();
        }


        public ActionResult EditarPet(ModelPet cm, string id)
        {
            carregaEspecie();


            if (Session["logged"] == null)
            {
                return RedirectToAction("SemAcesso", "Cliente");
            }
            else
            {
                cm.codPet = id;
                acPet.GetPetForEdit(cm);
                ViewBag.codEsp = cm.codEspeciePet;
                ViewBag.nmEsp = cm.especiePet;
                ViewBag.sexo = cm.sexoPet;
                ViewBag.porte = cm.portePet;

                Session["fotoPet"] = cm.imagePet;
                Session["codPet"] = cm.codPet;
                selecionaPet(cm);
                return View();
            }
        }

        [HttpPost]
        public ActionResult EditarPet(ModelPet cm, HttpPostedFileBase file, FormCollection frm)
        {
            carregaEspecie();

            if (Session["logged"] == null)
            {
                return RedirectToAction("SemAcesso", "Cliente");
            }
            else
            {
                string arquivo, path, file2;
                if (file == null)
                {
                    cm.imagePet = Session["fotoPet"].ToString();
                }
                else
                {
                    arquivo = Path.GetFileName(file.FileName);
                    path = Path.Combine(Server.MapPath("~/Images/Pets/"), arquivo);
                    file2 = "/Images/Pets/" + arquivo;
                    file.SaveAs(path);
                    cm.imagePet = file2;

                }
                cm.codEspeciePet = frm["slEspecie"];
                cm.sexoPet = Request["sexo"];
                cm.portePet = Request["porte"];
                string id = Session["codPet"].ToString();
                acPet.editarPet(cm, id);
                ViewBag.color = "success";
                    ViewBag.msg = "Cadastro realizado com sucesso";

                return RedirectToAction("MeusPets", "Cliente");
            }
        }

        public ActionResult ApagarPet(string id)
        {
            if (Session["logged"] == null)
            { 
                return RedirectToAction("SemAcesso", "Cliente");
            }
            else
            {
            acPet.ApagarPet(id);
            return RedirectToAction("MeusPets", "Cliente");
            }
        }


        // ============ [ PARTE DE COMPRAS ] ================

        public ActionResult ListarCompras(ModelCliente cm)
        {
            if (Session["logged"] == null)
            {
                return RedirectToAction("SemAcesso", "Cliente");
            }
            else
            {
                cm.cdCliente = Session["codCliente"].ToString();
                acCliente.ObterInformacoesCliente(cm);
                selecionaCliente(cm);
                return View(acCompra.GetCompra(cm));
            }
        }

        public ActionResult ProdutosDaCompra(ModelCliente cm, string id)
        {
            if (Session["logged"] == null)
            {
                return RedirectToAction("SemAcesso", "Cliente");
            }
            else
            {
                
                cm.cdCliente = Session["codCliente"].ToString();
                acCliente.ObterInformacoesCliente(cm);
                selecionaCliente(cm);
                return View(acCompra.GetItensCompra(id));
            }
        }




    }
}