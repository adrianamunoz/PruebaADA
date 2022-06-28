using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Prueba.Datos;
using Prueba.Models;

namespace Prueba.Controllers
{
    public class ProductoPruebaController : Controller
    {
        //static string Conexion = "Persist Security Info=False;User ID=Ubd-Pru-appsweb;Password=Oprh73.6LnQw;Initial Catalog=LC-PRU-Enla_com;Server=TEST1\\CLBDSTEST01";
        ProductoDatos _ProductoDatos = new ProductoDatos();
        // GET: ProductoPrueba
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Consultar()
        {
            var ListarProducto = _ProductoDatos.ListarProducto();
            return View(ListarProducto);
        }
        public ActionResult ConsultarCompraTotal()
        {
            var ListarCompras = _ProductoDatos.ListarCompras();
            return View(ListarCompras);
        }
        public ActionResult ConsultarCompraEfectiva()
        {
            int IdUsuario = Convert.ToInt32(Session["IdUsuario"]);
            var ListarProducto = _ProductoDatos.ConsultarCompraEfectiva(IdUsuario);
            return View(ListarProducto);
        }

        public ActionResult ConsultarCompra()
        {
            var ListarProducto = _ProductoDatos.ListarProducto();
            return View(ListarProducto);
        }
        
        public ActionResult HacerCompra(int IdProducto)
        {
            int IdUsuario = Convert.ToInt32(Session["IdUsuario"]);
            Session["IdProducto"] = IdProducto;
            var producto = _ProductoDatos.ConsultarProductoId(IdProducto);
            Session["CantidadDisponible"] = producto[0].CantidadDisponible;
            return View(producto);
            //var producto = _ProductoDatos.ComprarProducto(IdProducto, IdUsuario);
        }
        public class Cantidad
        {
            public int CantidadCompra { get; set; }
        }
        public ActionResult RealizarCompra(ListProducto producto)
        {
            int IdUsuario = Convert.ToInt32(Session["IdUsuario"]);
            int IdProducto = Convert.ToInt32(Session["IdProducto"]);
            int Cantidad = producto.CantidadComprar;
            if(Cantidad >= Convert.ToInt32(Session["CantidadDisponible"]))
            {
                ViewData["Mensaje"] = "Cantidad no disponible";
                return RedirectToAction("About");
            }
            else
            {
                var Compra = _ProductoDatos.ComprarProducto(IdProducto, IdUsuario, Cantidad);
                return RedirectToAction("ConsultarCompra");
            }
            
            
        }
        [HttpPost]
        public ActionResult Index(ListProducto producto)
        {
            if (!ModelState.IsValid)
                return View();

            var respuesta = _ProductoDatos.ResgistrarProducto(producto);
            return View(producto);
        }
        //[HttpPost]
        //public ActionResult Index(ListProducto producto)
        //{
        //    bool registrado;
        //    string mensaje;

        //    using (SqlConnection cn = new SqlConnection(Conexion))
        //    {
        //        SqlCommand cmd = new SqlCommand("spRegistrarProductoPrueba", cn);
        //        cmd.Parameters.AddWithValue("Nombre", producto.NombreProducto);
        //        cmd.Parameters.AddWithValue("Cantidad", producto.CantidadDisponible);
        //        cmd.Parameters.AddWithValue("Descripcion", producto.Descripcion);
        //        cmd.Parameters.Add("Registrado", SqlDbType.Bit).Direction = ParameterDirection.Output;
        //        cmd.CommandType = CommandType.StoredProcedure;

        //        cn.Open();
        //        cmd.ExecuteNonQuery();
        //        registrado = Convert.ToBoolean(cmd.Parameters["Registrado"].Value);
        //        mensaje = cmd.Parameters["Registrado"].Value.ToString();
        //    }
        //    if (mensaje.Equals(false))
        //    {
        //        ViewData["Mensaje"] = "Producto no pudo ser registrado";
        //    }
        //    else
        //    {
        //        ViewData["Mensaje"] = "Producto registrado con exito!";
        //    }

        //    if (registrado)
        //    {
        //        return RedirectToAction("About", "ProductoPrueba");
        //    }
        //    else
        //    {
        //        return View();
        //    }
        //}
        
        public ActionResult Actualizar(int IdProducto)
        {
            Session["IdProducto"] = IdProducto;
            var producto = _ProductoDatos.ConsultarProductoId(IdProducto);
            ViewData["Nombre"] = producto[0].NombreProducto;
            ViewData["Cantidad"] = producto[0].CantidadDisponible;
            ViewData["Descripcion"] = producto[0].Descripcion;

            return View(producto);
        }
        [HttpPost]
        public ActionResult Actualizar(ListProducto producto)
        {
            if (!ModelState.IsValid)
                return View();
            int IdProducto = Convert.ToInt32(Session["IdProducto"]);
            var respuesta = _ProductoDatos.ActualizarProducto(producto, IdProducto);
            if (respuesta)
            {
                return RedirectToAction("Consultar");
            }
            else
            {
                return View();
            }
        }
        public ActionResult Eliminar(int IdProducto)
        {
            var producto = _ProductoDatos.EliminarProducto(IdProducto);
            return RedirectToAction("Consultar");
        }
    }


}