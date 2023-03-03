using System.Web.Mvc;
using TCC.Dados;
using TCC.Models;

namespace TCC.Controllers
{
    public class ServicoController : Controller
    {

        acServico acServ = new acServico();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CadServ()
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
        public ActionResult CadServ(ModelServico cmServ)
        {

            acServ.inserirServico(cmServ);
            ViewBag.msgCad = "Cadastro Efetuado com sucesso";
            return View();
        }

        public ActionResult ListarServico()
        {
            ViewBag.usuName = Session["usuName"];
            ViewBag.usuImg = Session["usuImg"];
            return View(acServ.GetServico());
        }

        public ActionResult ExcluirServico(int id)
        {
            acServ.DeleteServico(id);
            return RedirectToAction("ListarServico");

        }
        public ActionResult EditarServico(ModelServico cm,string id)
        {
            ViewBag.usuName = Session["usuName"];
            ViewBag.usuImg = Session["usuImg"];
            cm.cd_servicos = id;
            acServ.GetServicoId(cm);
            ViewBag.nm_servico = cm.nm_servico;
            ViewBag.vl_servico = cm.vl_servico;
            return View();
        }



        [HttpPost]
        public ActionResult EditarServico(int id, ModelServico cm)
        {
            ViewBag.usuName = Session["usuName"];
            ViewBag.usuImg = Session["usuImg"];
            cm.cd_servicos = id.ToString();
            acServ.AtualizaServico(cm);
            return View();
        }

    }
}