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
    public class FuncionarioController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.usuName = Session["usuName"];
            ViewBag.usuImg = Session["usuImg"];
            return View();
        }

        acFuncionario acFunc = new acFuncionario();


        public ActionResult CadFunc()
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
                else
                {
                    return RedirectToAction("SemAcesso", "Cliente");
                }
            }
        }

        [HttpPost]
        public ActionResult CadFunc(ModelFuncionario cmFunc, HttpPostedFileBase file)
        {
            string arquivo, path, file2;
            if (file == null)
            {
                arquivo = Path.GetFileName("userAnonymous.png");
                path = Path.Combine(Server.MapPath("~/Images/Clientes/"), arquivo);
                file2 = "/Images/Clientes/" + arquivo;
                cmFunc.image_func = file2;
            }
            else
            {
                arquivo = Path.GetFileName(file.FileName);
                path = Path.Combine(Server.MapPath("~/Images/Clientes/"), arquivo);
                file2 = "/Images/Clientes/" + arquivo;
                file.SaveAs(path);
                cmFunc.image_func = file2;
            }
            cmFunc.nivel_func = Request["nivel"];
            acFunc.inserirFuncionario(cmFunc);
            ViewBag.msgCad = "Cadastro Efetuado com sucesso";
            return View();
        }

        public ActionResult ListarFuncionario()
        {
            ViewBag.usuName = Session["usuName"];
            ViewBag.usuImg = Session["usuImg"];
            return View(acFunc.GetFuncionario());
        }

        public ActionResult ExcluirFuncionario(int id)
        {
            acFunc.DeleteFuncionario(id);
            return RedirectToAction("ListarFuncionario");

        }
        public ActionResult EditarFuncionario(ModelFuncionario cm, string id)
        {
            ViewBag.usuName = Session["usuName"];
            ViewBag.usuImg = Session["usuImg"];
            cm.cd_func = id;
            acFunc.GetFuncionarioId(cm);
            ViewBag.nm_func = cm.nm_func;
            ViewBag.login = cm.login;
            ViewBag.senha = cm.senha;
            ViewBag.nivel_func = cm.nivel_func;
            Session["imageFunc"] = cm.image_func;
            return View();
        }


        [HttpPost]
        public ActionResult EditarFuncionario(int id, ModelFuncionario cm, HttpPostedFileBase file)
        {
            cm.cd_func = id.ToString();
            cm.nivel_func = Request["nivel"];

            string arquivo, path, file2;
            if (file == null)
            {
                cm.image_func = Session["imageFunc"].ToString();
            }
            else
            {
                arquivo = Path.GetFileName(file.FileName);
                path = Path.Combine(Server.MapPath("~/Images/Funcionario/"), arquivo);
                file2 = "/Images/Funcionario/" + arquivo;
                file.SaveAs(path);
                cm.image_func = file2;
            }

            acFunc.AtualizaFuncionario(cm);
            return View();
        }

    }
}

