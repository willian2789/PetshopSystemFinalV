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
    public class CompraController : Controller
    {
        AcoesProduto acProd = new AcoesProduto();
        acCarrinho acCar = new acCarrinho();
        AcoesCompra acComp = new AcoesCompra();
        
     
        

        public ActionResult Index()
        {
            return View();
        }

        public static string codigo;
        public static double Qt;

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


        public ActionResult AdicionarCarrinho(int id, double pre)
        {
            if (Session["logged"] == null)
            {
                return RedirectToAction("LoginCliente", "Cliente");

            }
            ViewBag.usuName = Session["usuName"];
            ViewBag.usuImage = Session["usuImg"];

            ModelCompra carrinho = Session["Carrinho"] != null ? (ModelCompra)Session["Carrinho"] : new ModelCompra();
            var produto = acProd.GetConsProd(id);
            codigo = id.ToString();

            ModelProduto prod = new ModelProduto();


            if (produto != null)
            {
                var itemPedido = new ModelCarrinho();
                itemPedido.ItemPedidoID = Guid.NewGuid();
                itemPedido.cd_produto = id.ToString();
                itemPedido.nm_produto = produto[0].nm_produto;
                itemPedido.qt_venda = 1;
                itemPedido.vl_unitario = pre;

                List<ModelCarrinho> x = carrinho.ItensPedido.FindAll(l => l.nm_produto == itemPedido.nm_produto);

                if (x.Count != 0)
                {
                    carrinho.ItensPedido.FirstOrDefault(p => p.nm_produto == produto[0].nm_produto).qt_venda += 1;
                    itemPedido.vl_parcial = itemPedido.qt_venda * itemPedido.vl_unitario;
                    carrinho.vlTotal += itemPedido.vl_parcial;
                    carrinho.ItensPedido.FirstOrDefault(p => p.nm_produto == produto[0].nm_produto).vl_parcial = carrinho.ItensPedido.FirstOrDefault(p => p.nm_produto == produto[0].nm_produto).qt_venda * itemPedido.vl_unitario;
                }

                else
                {
                    itemPedido.vl_parcial = itemPedido.qt_venda * itemPedido.vl_unitario;
                    carrinho.vlTotal += itemPedido.vl_parcial;
                    carrinho.ItensPedido.Add(itemPedido);
                }

                /*carrinho.ValorTotal = carrinho.ItensPedido.Select(i => i.Produto).Sum(d => d.Valor);*/

                Session["Carrinho"] = carrinho;
            }

            return RedirectToAction("Carrinho");
        }
        public ActionResult RetirarCarrinho(int id, double pre)
        {
            ModelCompra carrinho = Session["Carrinho"] != null ? (ModelCompra)Session["Carrinho"] : new ModelCompra();
            var produto = acProd.GetConsProd(id);
            codigo = id.ToString();

            ModelProduto prod = new ModelProduto();

            if (produto != null)
            {
                var itemPedido = new ModelCarrinho();
                itemPedido.ItemPedidoID = Guid.NewGuid();
                itemPedido.cd_produto = id.ToString();
                itemPedido.nm_produto = produto[0].nm_produto;
                itemPedido.qt_venda = 1;
                itemPedido.vl_unitario = pre;

                List<ModelCarrinho> x = carrinho.ItensPedido.FindAll(l => l.nm_produto == itemPedido.nm_produto);

                if (x.Count != 0)
                {
                    if (carrinho.ItensPedido.FirstOrDefault(p => p.nm_produto == produto[0].nm_produto).qt_venda > 1)
                    {
                        carrinho.ItensPedido.FirstOrDefault(p => p.nm_produto == produto[0].nm_produto).qt_venda -= 1;
                        itemPedido.vl_parcial = itemPedido.qt_venda * itemPedido.vl_unitario;
                        carrinho.vlTotal -= itemPedido.vl_parcial;
                        carrinho.ItensPedido.FirstOrDefault(p => p.nm_produto == produto[0].nm_produto).vl_parcial = carrinho.ItensPedido.FirstOrDefault(p => p.nm_produto == produto[0].nm_produto).qt_venda * itemPedido.vl_unitario;
                    }
                }
                else
                {
                    itemPedido.vl_parcial = itemPedido.qt_venda * itemPedido.vl_unitario;
                    carrinho.vlTotal += itemPedido.vl_parcial;
                    carrinho.ItensPedido.Add(itemPedido);
                }

                /*carrinho.ValorTotal = carrinho.ItensPedido.Select(i => i.Produto).Sum(d => d.Valor);*/

                Session["Carrinho"] = carrinho;
            }


            return RedirectToAction("Carrinho");
        }

        public ActionResult Carrinho(ModelCompra x)
        {
            ViewBag.usuName = Session["usuName"];
            ViewBag.usuImage = Session["usuImg"];
            ModelCompra carrinho = Session["Carrinho"] != null ? (ModelCompra)Session["Carrinho"] : new ModelCompra();
            carregaPag();
            x.cdPagamento = Request["pagamento"];
            x.vlTotal = carrinho.vlTotal;
            return View(carrinho);
        }

        public ActionResult ExcluirItem(Guid id)
        {
            var carrinho = Session["Carrinho"] != null ? (ModelCompra)Session["Carrinho"] : new ModelCompra();
            var itemExclusao = carrinho.ItensPedido.FirstOrDefault(i => i.ItemPedidoID == id);

            carrinho.vlTotal = itemExclusao.vl_parcial;

            carrinho.ItensPedido.Remove(itemExclusao);

            Session["Carrinho"] = carrinho;
            return RedirectToAction("Carrinho");
        }

        public ActionResult SalvarCarrinho(ModelCompra x)
        {
            carregaPag();
            if (Session["logged"] == null)
            {
                return RedirectToAction("LoginCliente", "Cliente");

            }
            else
            {
                ViewBag.usuName = Session["usuName"];
                ViewBag.usuImage = Session["usuImg"];
                var carrinho = Session["Carrinho"] != null ? (ModelCompra)Session["Carrinho"] : new ModelCompra();

                ModelCompra md = new ModelCompra();
                ModelCarrinho mdV = new ModelCarrinho();

                md.dtCompra = DateTime.Now.ToLocalTime().ToString("dd/MM/yyyy");
                md.cdClienteCompra = Session["codCliente"].ToString();
                md.vlTotal = carrinho.vlTotal;
                md.cdPagamento = Request["pagamento"];
                acComp.inserirCompra(md);


                acComp.buscaIdCompra(x);
              

                for (int i = 0; i < carrinho.ItensPedido.Count; i++)
                { 
                    mdV.cd_compra = x.cdCompra;
                    mdV.cd_pagamento = Request["pagamento"];
                    mdV.cd_produto = carrinho.ItensPedido[i].cd_produto;
                    mdV.qt_venda = carrinho.ItensPedido[i].qt_venda;
                    mdV.vl_parcial = carrinho.ItensPedido[i].vl_parcial;
                    mdV.vl_total = md.vlTotal;
                    acCar.inserirCarrinho(mdV);
                }

                carrinho.ItensPedido.Clear();
                return RedirectToAction("confVenda");
            }
        }

        public ActionResult confVenda()
        {
            return View();
        }



    }
}