using Prueba.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Prueba.Datos
{
    public class ProductoDatos
    {
        public List<UsuarioCompra> ListarCompras()
        {
            var Lista = new List<UsuarioCompra>();
            var cn = new Conexion();

            using (var conexion = new SqlConnection(cn.getCadenaSql()))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("spConsultaCompraTotal", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Lista.Add(new UsuarioCompra()
                        {
                            IdUsuarioProducto = Convert.ToInt32(dr["IdUsuarioProducto"]),
                            IdUsuario = Convert.ToInt32(dr["IdUsuario"]),
                            Usuario = dr["Nombre"].ToString(),
                            IdProducto = Convert.ToInt32(dr["IdProducto"]),
                            Producto = dr["NombreProducto"].ToString(),
                            Cantidad = Convert.ToInt32(dr["Cantidad"]),
                            FechaCompra = Convert.ToDateTime(dr["FechaCompra"])
                        });
                    }
                }
            }
            return Lista;
        }
        public List<ListProducto> ListarProducto()
        {
            var Lista = new List<ListProducto>();
            var cn = new Conexion();

            using (var conexion = new SqlConnection(cn.getCadenaSql()))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("spConsultaProducto", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Lista.Add(new ListProducto()
                        {
                            IdProducto = Convert.ToInt32(dr["IdProducto"]),
                            NombreProducto = dr["NombreProducto"].ToString(),
                            CantidadDisponible = Convert.ToInt32(dr["CantidadDisponible"]),
                            Descripcion = dr["Descripcion"].ToString()
                        });
                    }
                }
            }
            return Lista;
        }
        public List<UsuarioCompra> ConsultarCompraEfectiva(int IdUsuario)
        {
            var ListaCompra = new List<UsuarioCompra>();
            var cn = new Conexion();

            using (var conexion = new SqlConnection(cn.getCadenaSql()))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("spConsultaCompraEfectiva", conexion);
                cmd.Parameters.AddWithValue("IdUsuario", IdUsuario);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        ListaCompra.Add(new UsuarioCompra()
                        {
                            IdUsuarioProducto = Convert.ToInt32(dr["IdUsuarioProducto"]),
                            IdUsuario = Convert.ToInt32(dr["IdUsuario"]),
                            Usuario = dr["Nombre"].ToString(),
                            IdProducto = Convert.ToInt32(dr["IdProducto"]),
                            Producto = dr["NombreProducto"].ToString(),
                            Cantidad = Convert.ToInt32(dr["Cantidad"]),
                            FechaCompra = Convert.ToDateTime(dr["FechaCompra"])
                        });
                    }
                }
            }
            return ListaCompra;
        }

        public List<ListProducto> ConsultarProductoId(int IdProducto)
        {
            var productoId = new List<ListProducto>();
            var cn = new Conexion();

            using (var conexion = new SqlConnection(cn.getCadenaSql()))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("spConsultaProductoId", conexion);
                cmd.Parameters.AddWithValue("IdProducto", IdProducto);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        productoId.Add(new ListProducto()
                        {
                            IdProducto = Convert.ToInt32(dr["IdProducto"]),
                            NombreProducto = dr["NombreProducto"].ToString(),
                            CantidadDisponible = Convert.ToInt32(dr["CantidadDisponible"]),
                            Descripcion = dr["Descripcion"].ToString()
                        });
                    }
                }
            }
            return productoId;
        }

        public bool ResgistrarProducto(ListProducto Producto)
        {
            bool respuesta;
            try
            {
                var cn = new Conexion();
                using (var conexion = new SqlConnection(cn.getCadenaSql()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("spRegistrarProductoPrueba", conexion);
                    cmd.Parameters.AddWithValue("Nombre", Producto.NombreProducto);
                    cmd.Parameters.AddWithValue("Cantidad", Producto.CantidadDisponible);
                    cmd.Parameters.AddWithValue("Descripcion", Producto.Descripcion);
                    cmd.Parameters.Add("Registrado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                    bool registrado = Convert.ToBoolean(cmd.Parameters["Registrado"].Value);
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

        public bool ActualizarProducto(ListProducto Producto, int IdProducto)
        {
            bool respuesta;
            try
            {
                var cn = new Conexion();
                using (var conexion = new SqlConnection(cn.getCadenaSql()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("spActualizarProductoPrueba", conexion);
                    cmd.Parameters.AddWithValue("IdProducto", IdProducto);
                    cmd.Parameters.AddWithValue("Nombre", Producto.NombreProducto);
                    cmd.Parameters.AddWithValue("Cantidad", Producto.CantidadDisponible);
                    cmd.Parameters.AddWithValue("Descripcion", Producto.Descripcion);
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

        public bool ComprarProducto(int IdProducto, int IdUsuario, int Cantidad)
        {
            bool respuesta;
            try
            {
                var cn = new Conexion();
                using (var conexion = new SqlConnection(cn.getCadenaSql()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("spComprarProductoPrueba", conexion);
                    cmd.Parameters.AddWithValue("IdProducto", IdProducto);
                    cmd.Parameters.AddWithValue("IdUsuario", IdUsuario);
                    cmd.Parameters.AddWithValue("Cantidad", Cantidad);
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
        public bool EliminarProducto(int IdProducto)
        {
            bool respuesta;
            try
            {
                var cn = new Conexion();
                using (var conexion = new SqlConnection(cn.getCadenaSql()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("spEliminarProductoPrueba", conexion);
                    cmd.Parameters.AddWithValue("IdProducto", IdProducto);
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