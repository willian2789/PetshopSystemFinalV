using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;
using TCC.Dados;
using TCC.Models;

namespace TCC2.Controllers
{
    public class FornecedorController : Controller
    {
        acFornecedor acForn = new acFornecedor();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CadForn()
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
                    return View();
                }
                if (Session["usu"].ToString() == "comum")
                {
                    return View();
                }
                else
                {
                    return RedirectToAction("SemAcesso", "Cliente");
                }
            }
        }

        [HttpPost]
        public ActionResult CadForn(ModelFornecedor cmForn)
        {
            acForn.inserirFornecedor(cmForn);
            ViewBag.msgCad = "Cadastro Efetuado com sucesso";
            return View();
        }

        public ActionResult ListarFornecedor()
        {
            ViewBag.usuName = Session["usuName"];
            ViewBag.usuImg = Session["usuImg"];
            return View(acForn.GetFornecedor());
        }

        public ActionResult ExcluirFornecedor(int id)
        {
            acForn.DeleteFornecedor(id);
            return RedirectToAction("ListarFornecedor");

        }
        public ActionResult EditarFornecedor(ModelFornecedor cm,string id)
        {
            ViewBag.usuName = Session["usuName"];
            ViewBag.usuImg = Session["usuImg"];
            cm.cd_fornecedor = id;
            acForn.GetFornecedorId(cm);
            ViewBag.nm_fornecedor = cm.nm_fornecedor;
            ViewBag.no_telefone = cm.no_telefone;
            ViewBag.email_fornecedor = cm.email_fornecedor;
            ViewBag.cnpj_fornecedor = cm.cnpj_fornecedor;
            return View();
        }



        [HttpPost]
        public ActionResult EditarFornecedor(int id, ModelFornecedor cm)
        {
            cm.cd_fornecedor = id.ToString();
            acForn.AtualizaFornecedor(cm);
            return View();
        }

    }
}