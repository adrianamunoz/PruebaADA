using Prueba.Models;
using System.Web.Mvc;
using System.Text;
using System.Security.Cryptography;
using System.Data;
using System.Data.SqlClient;
using System;
using Prueba.Datos;
using Datos;

namespace Prueba.Controllers
{
    public class UsuarioPruebaController : Controller
    {
        UsuarioDatos _UsuarioDatos = new UsuarioDatos();
        static string Conexion = "Persist Security Info=False;User ID=Ubd-Pru-appsweb;Password=Oprh73.6LnQw;Initial Catalog=LC-PRU-Enla_com;Server=TEST1\\CLBDSTEST01";
        // GET: UusarioPrueba
        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Registro()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Consultar()
        {
            var ListarUsuario = _UsuarioDatos.ListarUsuario();
            return View(ListarUsuario);
        }
        [HttpPost]
        public ActionResult Registro(Usuario usuario)
        {
            bool registrado;
            string mensaje;

            if (usuario.Contrasena == usuario.ConfirmarContrasena)
            {
                usuario.Contrasena = EncriptarContrasena(usuario.Contrasena);
            }
            else
            {
                ViewData["Mensaje"] = "Las contraseñas no coinciden";
                return View();
            }
            using (SqlConnection cn = new SqlConnection(Conexion))
            {
                SqlCommand cmd = new SqlCommand("spRegistrarUsuarioPrueba", cn);
                cmd.Parameters.AddWithValue("Nombre", usuario.Nombre);
                cmd.Parameters.AddWithValue("Direccion", usuario.Direccion);
                cmd.Parameters.AddWithValue("Telefono", usuario.Telefono);
                cmd.Parameters.AddWithValue("UsuarioP", usuario.UsuarioP);
                cmd.Parameters.AddWithValue("Identificacion", usuario.Identificacion);
                cmd.Parameters.AddWithValue("Contrasena", usuario.Contrasena);
                cmd.Parameters.AddWithValue("idTipoUsuario", usuario.idTipoUsuario);
                cmd.Parameters.Add("Registrado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                //cmd.Parameters.Add("Mensaje", SqlDbType.VarChar).Direction = ParameterDirection.Output;
                cmd.CommandType = CommandType.StoredProcedure;

                cn.Open();
                cmd.ExecuteNonQuery();
                registrado = Convert.ToBoolean(cmd.Parameters["Registrado"].Value);
                mensaje = cmd.Parameters["Registrado"].Value.ToString();
            }
            ViewData["Mensaje"] = mensaje;
            if (registrado == true)
            {
                ViewData["Mensaje"] = "Usuario registrado exitosamente!";
            }
            else
            {
                ViewData["Mensaje"] = "Usuario no fue posible registrar, posiblemente ya existe!";
            }
            if (registrado)
            {
                return RedirectToAction("Login", "UsuarioPrueba");
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult Login(Usuario usuario)
        {
            usuario.Contrasena = EncriptarContrasena(usuario.Contrasena);

            using (SqlConnection cn = new SqlConnection(Conexion))
            {
                SqlCommand cmd = new SqlCommand("spValidarUsuarioPrueba", cn);
                cmd.Parameters.AddWithValue("Usuario", usuario.UsuarioP);
                cmd.Parameters.AddWithValue("contrasena", usuario.Contrasena);
                cmd.CommandType = CommandType.StoredProcedure;

                cn.Open();
                //usuario.Identificacion = (cmd.ExecuteScalar().ToString());
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        usuario.IdUsuario = Convert.ToInt32(dr["IdUsuario"]);
                        usuario.Identificacion = dr["Identificacion"].ToString();
                        usuario.idTipoUsuario = Convert.ToInt32(dr["idTipoUsuario"]);
                    };
                }

            }
            int IdUsuario = Convert.ToInt32(usuario.IdUsuario);
            int Identificacion = Convert.ToInt32(usuario.Identificacion);
            int IdTipoUsuario = Convert.ToInt32(usuario.idTipoUsuario);
            Session["IdTipoUsuario"] = IdTipoUsuario;
            Session["IdUsuario"] = IdUsuario;
            if (Identificacion != 0)
            {
                Session["usuario"] = usuario;
                return RedirectToAction("About", "ProductoPrueba");
            }
            else
            {
                ViewData["Mensaje"] = "Usuario no encontrado";
                return View();
            }
        }

        public ActionResult Actualizar(string Identificacion)
        {
            var usuario = _UsuarioDatos.ConsultarUsuarioId(Identificacion);
            return View(usuario);
        }
        [HttpPost]
        public ActionResult Actualizar(Usuario usuario)
        {
            if (!ModelState.IsValid)
                return View();

            var respuesta = _UsuarioDatos.ActualizarUsuario(usuario);
            if (respuesta)
            {
                return RedirectToAction("Consultar");
            }
            else
            {
                return View();
            }
        }

        public static string EncriptarContrasena(string contrasena)
        {
            StringBuilder Sb = new StringBuilder();
            using (SHA256 hash = SHA256Managed.Create())
            {
                Encoding enc = Encoding.UTF8;
                byte[] result = hash.ComputeHash(enc.GetBytes(contrasena));

                foreach (byte b in result)
                    Sb.Append(b.ToString("x2"));
            }
            return Sb.ToString();
        }

        public ActionResult CerrarSesion()
        {
            Session["usuario"] = null;
            return RedirectToAction("Login","UsuarioPrueba");
        }


    }
}