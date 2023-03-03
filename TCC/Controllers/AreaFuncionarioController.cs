using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using TCC.Dados;
using TCC.Models;

namespace TCC.Controllers
{
    public class AreaFuncionarioController : Controller
    {
        acFuncionario acF = new acFuncionario();

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(ModelFuncionario cm)
        {
            acF.TestarUsuario(cm); // Verificar se usu e senha estão corretos

            if (cm.login != null && cm.senha != null)
            {

                FormsAuthentication.SetAuthCookie(cm.login, false);
                
                Session["usu"] = cm.nivel_func;
                Session["usuName"] = cm.nm_func;
                Session["usuImg"] = cm.image_func;


                if (cm.nivel_func == "admin")
                {
                    Session["admin"] = cm.nivel_func;
                }
                else
                {
                    Session["comum"] = cm.nivel_func;
                }


                return RedirectToAction("Index", "Funcionario");
            }
            else
            {
                ViewBag.msgLogar = "Login Incorreto. Verifique o login e a senha";
                return View();
            }
        }

        public ActionResult Logout()
        {
            Session["usu"] = null;
            Session["usuName"] = null;
            Session["usuImg"] = null;
            return RedirectToAction("Index", "AreaFuncionario");
        }

        public ActionResult About()
        {
            ViewBag.usuName = Session["usuName"];
            ViewBag.usuImg = Session["usuImg"];
            if (Session["usu"] == null)

            {
                return RedirectToAction("SemAcesso", "Cliente");
            }
            else
            {
                ViewBag.Message = "Your application description page.";
                ViewBag.usuarioLogado = Session["usu"];
                return View();
            }
        }

        public ActionResult Contact()
        {
            ViewBag.usuName = Session["usuName"];
            ViewBag.usuImg = Session["usuImg"];
            if (Session["usu"] == null)

            {
                return RedirectToAction("SemAcesso", "Cliente");
            }
            else
            {
                if (Session["comum"] == null)
                {
                    ViewBag.Message = "Your contact page.";

                    return View();
                }
                else
                {
                    return RedirectToAction("SemAcesso", "Cliente");
                }
            }
        }

        public ActionResult SemAcesso()
        {
            return View();
        }

    }
}