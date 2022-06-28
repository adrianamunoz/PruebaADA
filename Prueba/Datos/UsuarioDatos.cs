using Prueba.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Prueba.Datos
{
    public class UsuarioDatos
    {
        public List<Usuario> ListarUsuario()
        {
            var ListaUsuario = new List<Usuario>();
            var cn = new Conexion();

            using (var conexion = new SqlConnection(cn.getCadenaSql()))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("spConsultaUsuario", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        ListaUsuario.Add(new Usuario()
                        {
                            Nombre = dr["Nombre"].ToString(),
                            Direccion = dr["Direccion"].ToString(),
                            Telefono = Convert.ToInt32(dr["Telefono"]),
                            UsuarioP = dr["Usuario"].ToString(),
                            Identificacion = dr["Identificacion"].ToString(),
                            Contrasena = dr["contraseña"].ToString(),
                            idTipoUsuario = Convert.ToInt32(dr["idTipoUsuario"])
                        });
                    }
                }
            }
            return ListaUsuario;
        }

        public List<Usuario> ConsultarUsuarioId(string Identificacion)
        {
            var UsuarioId = new List<Usuario>();
            var cn = new Conexion();

            using (var conexion = new SqlConnection(cn.getCadenaSql()))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("spConsultaUsuarioId", conexion);
                cmd.Parameters.AddWithValue("Identificacion", Identificacion);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        UsuarioId.Add(new Usuario()
                        {
                            Nombre = dr["Nombre"].ToString(),
                            Direccion = dr["Direccion"].ToString(),
                            Telefono = Convert.ToInt32(dr["Telefono"]),
                            UsuarioP = dr["UsuarioP"].ToString(),
                            Identificacion = dr["Identificacion"].ToString(),
                            Contrasena = dr["Contrasena"].ToString(),
                            idTipoUsuario = Convert.ToInt32(dr["idTipoUsuario"])
                        });
                    }
                }
            }
            return UsuarioId;
        }

        public bool ResgistrarUsuario(Usuario usuario)
        {
            bool respuesta;
            try
            {
                var cn = new Conexion();
                using (var conexion = new SqlConnection(cn.getCadenaSql()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("spRegistrarUsuarioPrueba", conexion);
                    cmd.Parameters.AddWithValue("Nombre", usuario.Nombre);
                    cmd.Parameters.AddWithValue("Direccion", usuario.Direccion);
                    cmd.Parameters.AddWithValue("Telefono", usuario.Telefono);
                    cmd.Parameters.AddWithValue("UsuarioP", usuario.UsuarioP);
                    cmd.Parameters.AddWithValue("Identificacion", usuario.Identificacion);
                    cmd.Parameters.AddWithValue("Contrasena", usuario.Contrasena);
                    cmd.Parameters.AddWithValue("idTipoUsuario", usuario.idTipoUsuario);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                    //bool registrado = Convert.ToBoolean(cmd.Parameters["Registrado"].Value);
                    string mensaje = cmd.Parameters["Registrado"].Value.ToString();
                }
                respuesta = true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                respuesta = false;
            }
            return respuesta;
        }

        public bool ActualizarUsuario(Usuario usuario)
        {
            bool respuesta;
            try
            {
                var cn = new Conexion();
                using (var conexion = new SqlConnection(cn.getCadenaSql()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("spActualizarUsuarioPrueba", conexion);
                    cmd.Parameters.AddWithValue("Nombre", usuario.Nombre);
                    cmd.Parameters.AddWithValue("Direccion", usuario.Direccion);
                    cmd.Parameters.AddWithValue("Telefono", usuario.Telefono);
                    cmd.Parameters.AddWithValue("UsuarioP", usuario.UsuarioP);
                    cmd.Parameters.AddWithValue("Identificacion", usuario.Identificacion);
                    cmd.Parameters.AddWithValue("Contrasena", usuario.Contrasena);
                    cmd.Parameters.AddWithValue("idTipoUsuario", usuario.idTipoUsuario);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                }
                respuesta = true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                respuesta = false;
            }
            return respuesta;
        }

        public bool EliminarUsuario(string Identificacion)
        {
            bool respuesta;
            try
            {
                var cn = new Conexion();
                using (var conexion = new SqlConnection(cn.getCadenaSql()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("spEliminarUsuarioPrueba", conexion);
                    cmd.Parameters.AddWithValue("Identificacion", Identificacion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                }
                respuesta = true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                respuesta = false;
            }
            return respuesta;
        }
    }
}