using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TCC.Dados;
using TCC.Models;

namespace TCC.Controllers
{
    public class CategoriaController : Controller
    {
        acCategoria acCat = new acCategoria();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CadCat()
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
        public ActionResult CadCat(ModelCategoria cmCat)
        {
            acCat.inserirCategoria(cmCat);
            ViewBag.msgCad = "Cadastro Efetuado com sucesso";
            return View();
        }

        public ActionResult ListarCategoria()
        {
            ViewBag.usuName = Session["usuName"];
            ViewBag.usuImg = Session["usuImg"];
            return View(acCat.GetCategoria());
        }

        public ActionResult ExcluirCategoria(int id)
        {
            acCat.DeleteCategoria(id);
            return RedirectToAction("ListarCategoria");

        }
        public ActionResult EditarCategoria(ModelCategoria cm,string id)
        {
            ViewBag.usuName = Session["usuName"];
            ViewBag.usuImg = Session["usuImg"];
            cm.cd_categoria = id;
            acCat.GetCategoriaId(cm);
            ViewBag.ds_categoria = cm.ds_categoria;
            return View();

        }



        [HttpPost]
        public ActionResult EditarCategoria(int id, ModelCategoria cm)
        {
            cm.cd_categoria = id.ToString();
            acCat.AtualizaCategoria(cm);
            return View();
        }
    }
}