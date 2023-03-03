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
    public class LojaController : Controller
    {

        AcoesProduto acProd = new AcoesProduto();
        AcoesLoginCliente acCliente = new AcoesLoginCliente();

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

        public void carregaCategoria()
        {
            List<SelectListItem> categoria = new List<SelectListItem>();

            using (MySqlConnection con = new MySqlConnection("Server=localhost; DataBase=bd_eAnimalcity; User=root; pwd=kauansql2727#"))
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

        public ActionResult Index(ModelProduto cmP, ModelCliente cm)
        {
            if (Session["logged"] == null)
            {
                carregaCategoria();
                return View(acProd.GetProduto(cmP));
            }
            else
            {
                carregaCategoria();
                ViewBag.usuName = Session["usuName"];
                ViewBag.usuImage = Session["usuImg"];
                carregaCategoria();
                return View(acProd.GetProduto(cmP));

            }
        }

        [HttpPost]
        public ActionResult Index(ModelProduto cmP, ModelCliente cm, string id)
        {
            if (Session["logged"] == null)
            {
                carregaCategoria();

                if(Request["categoria"] == "")
                {
                    return View(acProd.GetProduto(cmP));
                }

                cmP.cd_categoria = Request["categoria"];
                return View(acProd.GetProdutoPorCategoria(cmP));
            }
            else
            {
                carregaCategoria();
                ViewBag.usuName = Session["usuName"];
                ViewBag.usuImage = Session["usuImg"];
                carregaCategoria();
                if (Request["categoria"] == "")
                {
                    return View(acProd.GetProduto(cmP));
                }
                cmP.cd_categoria = Request["categoria"];
                return View(acProd.GetProdutoPorCategoria(cmP));

            }
        }

        [HttpPost]
        public ActionResult BuscaProduto(ModelProduto cm, FormCollection frm)
        {
            if (Session["logged"] == null)
            {
                carregaCategoria();

                cm.nm_produto = frm["textBusca"];
                return View(acProd.GetProdutoPorBusca(cm));
            }
            else
            {
                carregaCategoria();
                ViewBag.usuName = Session["usuName"];
                ViewBag.usuImage = Session["usuImg"];
                carregaCategoria();
                cm.nm_produto = frm["textBusca"];
                return View(acProd.GetProdutoPorBusca(cm));

            }
        }


        public ActionResult ProductDetails(ModelProduto cm, string id)
        {
            if (Session["logged"] == null)
            {
                cm.cd_produto = id;
                acProd.ShowProductDetails(cm);
                ViewBag.nmProd = cm.nm_produto;
                ViewBag.imageProd = cm.image_produto;
                ViewBag.marcaProd = cm.marca_produto;
                ViewBag.dsProd = cm.ds_prod;
                ViewBag.precoProd = cm.vl_produto;
                return View();
            }
            else
            {
                cm.cd_produto = id;
                acProd.ShowProductDetails(cm);
                ViewBag.nmProd = cm.nm_produto;
                ViewBag.imageProd = cm.image_produto;
                ViewBag.marcaProd = cm.marca_produto;
                ViewBag.dsProd = cm.ds_prod;
                ViewBag.precoProd = cm.vl_produto;
                ViewBag.usuName = Session["usuName"];
                ViewBag.usuImage = Session["usuImg"];
                return View();

            }
        }


    }
}