using Livraria.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Livraria.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Usuarios u)
        {
            // esta action trata o post (login)
            if (ModelState.IsValid) //verifica se é válido
            {
                using (bd_livrariaEntities1 dc = new bd_livrariaEntities1())
                {
                    var v = dc.Usuarios.Where(a => a.nome.Equals(u.nome) && a.senha.Equals(u.senha)).FirstOrDefault();
                    if (v != null)
                    {
                        Session["usuarioLogadoID"] = v.cpf.ToString();
                        Session["nomeUsuarioLogado"] = v.nome.ToString();
                        Session["tipoUsuario"] = v.admin.ToString();
                        return RedirectToAction("Index");
                    }
                }
            }
            return View(u);
        }






        public ActionResult Index()
        {
            if (Session["usuarioLogadoID"] != null)
            {
                return RedirectToAction("Index","Selecao");
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        public ActionResult About()
        {
            ViewBag.Message = "Sistema de gerenciamento de uma livraria.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}