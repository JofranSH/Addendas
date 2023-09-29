using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
using WebApplication1.Models.ViewModels.Addendas;
using static WebApplication1.Models.ViewModels.Enum;

namespace WebApplication1.Controllers
{
    public class AddendasController : Controller
    {
        // GET: Addendas
        public ActionResult Index()
        {
            List<ListarAddendas> lista = new List<ListarAddendas>();

            using (Cer_AddendasEntities1 context = new Cer_AddendasEntities1())
            {
                lista = (from CA in context.Addendas
                         select new ListarAddendas
                         {
                             IdAddenda = CA.IdAddenda,
                             NombreAddenda = CA.NombreAddenda,
                             XML = CA.XML,
                             FechaModificacion = CA.FechaModificacion,
                             Usuario = CA.Usuario,
                             Estado = CA.Estado,

                         }).ToList();
            }
            return View(lista);
        }

        public ActionResult NuevoRegistro()
        {
            
            return View();
        }

        [HttpPost]
        public ActionResult NuevoRegistro(AgregarRegistro model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (Cer_AddendasEntities1 context = new Cer_AddendasEntities1())
                    {
                        var re = new Addendas();
                        re.IdAddenda = model.IdAddenda;
                        re.NombreAddenda = model.NombreAddenda;
                        re.XML = model.XML;
                        re.FechaModificacion = model.FechaModificacion;
                        re.Usuario = model.Usuario;
                        re.Estado = model.Estado;



                        context.Addendas.Add(re);
                        context.SaveChanges();
                        Alert("Registro guardado con éxito", NotificationType.succes);
                    }
                    return Redirect("~/Addendas");
                }

                Alert("Verifique la información", NotificationType.warning);
                return View(model);
            }
            catch (Exception ex)
            {
                Alert("A ocurrido un eror: " + ex.Message, NotificationType.error);
                return View(model);
            }
        }



        public ActionResult EditarRegistro(Guid id)
        {
            Addendas re = new Addendas();
            using (Cer_AddendasEntities1 db = new Cer_AddendasEntities1())
            {
                re = db.Addendas.Where(x => x.IdAddenda == id).FirstOrDefault();
            }


            ViewBag.Title = "Editar Registro No°: " + re.IdAddenda;

            return View(re);
        }

        [HttpPost]
        public ActionResult EditarRegistro(Addendas model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (Cer_AddendasEntities1 db = new Cer_AddendasEntities1())
                    {
                        var re = new Addendas();
                        re.IdAddenda = model.IdAddenda;
                        re.NombreAddenda = model.NombreAddenda;
                        re.XML = model.XML;
                        re.FechaModificacion = model.FechaModificacion;
                        re.Usuario = model.Usuario;
                        re.Estado = model.Estado;

                        db.Entry(re).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                        Alert("Registro guardado con éxito", NotificationType.succes);
                    }
                    return Redirect("~/Addendas");
                }
                Alert("Verificar la informacion", NotificationType.warning);
                return View(model);
            }
            catch (Exception ex)
            {
                //throw new Exception(ex.Message);
                Alert("A ocurrido un eror: " + ex.Message, NotificationType.error);
                return View(model);
            }
        }

        [HttpGet]
        public ActionResult CambiarEstado(Guid id)
        {
            try
            {
                using (Cer_AddendasEntities1 db = new Cer_AddendasEntities1())
                {
                    Addendas re = db.Addendas.FirstOrDefault(x => x.IdAddenda == id);
                    if (re != null)
                    {
                        re.Estado = false; // Cambia el estado a false en lugar de eliminar
                        Alert("Registro Desactivado con éxito", NotificationType.succes);
                        db.SaveChanges();
                    }
                    else
                    {
                        Alert("El registro no se encontró", NotificationType.warning);
                    }
                }
            }
            catch (Exception ex)
            {
                Alert("Error: " + ex.Message, NotificationType.error);
            }

            return Redirect("~/Addendas");
        }



        public void Alert(string message, NotificationType notificationType)
        {
            var msg = "<script language='javascript'>Swal.fire('" +
                notificationType.ToString().ToUpper() + "','" + message + "','" +
                notificationType + "')" + " </script>";

            TempData["notification"] = msg;
        }
    }

        


}