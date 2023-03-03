using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TCC.Dados;
using TCC.Models;

namespace TCC2.Controllers
{
    public class ProdutoController : Controller
    {
        AcoesProduto acProd = new AcoesProduto();
        public ActionResult Index()
        {
            return View();
        }
        public void carregaForn()
        {
            List<SelectListItem> fornecedor = new List<SelectListItem>();

            using (MySqlConnection con = new MySqlConnection("Server=localhost;DataBase=bd_eAnimalcity;User=root;pwd=kauansql2727#"))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand("select * from tbl_fornecedor", con);
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    fornecedor.Add(new SelectListItem
                    {
                        Text = rdr[1].ToString(),
                        Value = rdr[0].ToString()
                    });
                }
                con.Close();

            }
            ViewBag.fornecedor = new SelectList(fornecedor, "Value", "Text");
        }

        public void carregaCat()
        {
            List<SelectListItem> categoria = new List<SelectListItem>();

            using (MySqlConnection con = new MySqlConnection("Server=localhost;DataBase=bd_eAnimalcity;User=root;pwd=kauansql2727#"))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand("select * from tbl_categoria", con);
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    categoria.Add(new SelectListItem
                    {
                        Text = rdr[1].ToString(),
                        Value = rdr[0].ToString()
                    });
                }
                con.Close();

            }
            ViewBag.categoria = new SelectList(categoria, "Value", "Text");
        }

        public ActionResult CadProd()
        {
            if (Session["usu"] == null)

            {
                return RedirectToAction("SemAcesso", "Cliente");
            }
            else
            {
                ViewBag.usuName = Session["usuName"];
                ViewBag.usuImg = Session["usuImg"];
                if (Session["usu"].ToString() == "admin")
                {
                    carregaCat();
                    carregaForn();
                    return View();
                }
                if (Session["usu"].ToString() == "comum")
                {
                    carregaCat();
                    carregaForn();
                    return View();
                }
                else
                {
                    return RedirectToAction("SemAcesso", "Cliente");
                }
            }
        }

        [HttpPost]
        public ActionResult CadProd(ModelProduto cmProd, HttpPostedFileBase file)
        {
            string arquivo, path, file2;
            if (file == null)
            {
                arquivo = Path.GetFileName("petAnonymous.png");
                path = Path.Combine(Server.MapPath("~/Images/Funcionario/"), arquivo);
                file2 = "/Images/Funcionario/" + arquivo;
                cmProd.image_produto = file2;
            }
            else
            {
                arquivo = Path.GetFileName(file.FileName);
                path = Path.Combine(Server.MapPath("~/Images/Funcionario/"), arquivo);
                file2 = "/Images/Funcionario/" + arquivo;
                file.SaveAs(path);
                cmProd.image_produto = file2;
            }
            carregaCat();
            carregaForn();
            cmProd.cd_categoria = Request["categoria"];
            cmProd.cd_fornecedor = Request["fornecedor"];
            acProd.inserirProdutos(cmProd);
            ViewBag.msgCad = "Cadastro Efetuado com sucesso";
            return View();
        }

        public ActionResult ListarProduto(ModelProduto cm)
        {
            ViewBag.usuName = Session["usuName"];
            ViewBag.usuImg = Session["usuImg"];
            return View(acProd.GetProduto(cm));
        }

        public ActionResult ExcluirProduto(int id)
        {
            acProd.DeletaProduto(id);
            return RedirectToAction("ListarProduto");

        }
        public ActionResult EditarProduto(string id, ModelProduto cm)
        {
            ViewBag.usuName = Session["usuName"];
            ViewBag.usuImg = Session["usuImg"];
            cm.cd_produto = id;          
            acProd.GetProdutoId(cm);
            ViewBag.nm_produto = cm.nm_produto;
            Session["imageProd"] = cm.image_produto;
            Session["codProdCat"] = cm.cd_categoria;
            Session["codProdForn"] = cm.cd_fornecedor;
            ViewBag.marca_produto = cm.marca_produto;
            ViewBag.qt_produto = cm.qt_produto;
            ViewBag.vl_produto = cm.vl_produto;
            ViewBag.ds_prod = cm.ds_prod;
            ViewBag.nm_fornecedor = cm.nm_fornecedor;
            ViewBag.ds_categoria = cm.ds_categoria;
            return View();
        }


        [HttpPost]
        public ActionResult EditarProduto(int id, ModelProduto cm, HttpPostedFileBase file)
        {
            string arquivo, path, file2;
            if (file == null)
            {
                cm.image_produto = Session["imageProd"].ToString();
            }
            else
            {
                arquivo = Path.GetFileName(file.FileName);
                path = Path.Combine(Server.MapPath("~/Images/Funcionario/"), arquivo);
                file2 = "/Images/Funcionario/" + arquivo;
                file.SaveAs(path);
                cm.image_produto = file2;
            }
            cm.cd_produto = id.ToString();
            carregaCat();
            carregaForn();
            cm.cd_categoria = Session["codProdCat"].ToString();
            cm.cd_fornecedor = Session["codProdForn"].ToString();
            acProd.AtualizaProduto(cm);
            return View();
        }

    }
}