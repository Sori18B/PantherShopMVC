using Producto3.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Producto3.Logica
{
    public class funcComprador
    {

        private Datos objDatos = new Datos();


        public List<productos> ObtenerProductos()
        {
            List<productos> listaProductos = new List<productos>();

            string query = "SELECT idArt, nombre, descripcion, precio, imagen FROM productos";

            SqlDataReader reader = objDatos.ObtenerDatos(query);

            while (reader.Read())
            {
                productos producto = new productos
                {
                    idArt = Convert.ToInt32(reader["idArt"]),
                    nombre = reader["nombre"].ToString(),
                    descripcion = reader["descripcion"].ToString(),
                    precio = Convert.ToDecimal(reader["precio"]),
                    imagen = reader["imagen"].ToString()
                };

                listaProductos.Add(producto);
            }
            reader.Close();
            return listaProductos;
        }


        public string AgregarAlCarrito(int idArticulo, string nombre, int cantidad, decimal precio, int userId)
        {
            SqlParameter[] pars = new SqlParameter[]
            {
        new SqlParameter("@idArticulo", idArticulo),
        new SqlParameter("@nombre", nombre),
        new SqlParameter("@cantidad", cantidad),
        new SqlParameter("@precio", precio),
        new SqlParameter("@total", cantidad * precio),
        new SqlParameter("@idUsuario", userId)
            };

            int nc = objDatos.EjecutaSqlENQ("INSERT INTO carrito (idArt, nombre, cantidad, precio, total, idUsuario) VALUES (@idArticulo, @nombre, @cantidad, @precio, @total, @idUsuario)", pars);
            if (nc == 1)
            {
                return "El producto ha sido agregado al carrito";
            }
            else
            {
                return "No se agregó el producto al carrito";
            }
        }


        public productos ObtenerProductoPorId(int id)
        {
            SqlParameter[] parametros = new SqlParameter[]
            {
        new SqlParameter("@id", id)
            };

            SqlDataReader reader = objDatos.ObtenerDatos2arg("SELECT * FROM productos WHERE idArt = @id", parametros);
            productos producto = null;
            if (reader.Read())
            {
                producto = new productos();
                producto.idArt = reader.GetInt32(reader.GetOrdinal("idArt")); 
                producto.nombre = reader["nombre"].ToString();
                producto.descripcion = reader["descripcion"].ToString();
                producto.precio = reader.GetDecimal(reader.GetOrdinal("precio"));
                producto.imagen = reader["imagen"].ToString();
            }
            reader.Close();
            return producto;
        }



        public List<carrito> ObtenerCarrito(int userId)
        {
            List<carrito> carrito = new List<carrito>();

            SqlParameter[] pars = new SqlParameter[]
            {
        new SqlParameter("@idUsuario", userId)
            };

            SqlDataReader reader = objDatos.ObtenerDatos2arg("SELECT idArt, nombre, cantidad, precio, total FROM carrito WHERE idUsuario = @idUsuario;", pars);
            while (reader.Read())
            {
                carrito producto = new carrito
                {
                    idArt = reader.GetInt32(reader.GetOrdinal("idArt")),
                    nombre = reader["nombre"].ToString(),
                    cantidad = reader.GetInt32(reader.GetOrdinal("cantidad")),
                    precio = reader.GetDecimal(reader.GetOrdinal("precio")),
                    total = reader.GetDecimal(reader.GetOrdinal("total"))
                };
                carrito.Add(producto);
            }
            reader.Close();

            return carrito;
        }


        public void EliminarProductoCarrito(int idArticulo)
        {
            SqlParameter[] pars = new SqlParameter[]
            {
        new SqlParameter("@idArticulo", idArticulo)
            };

            objDatos.EjecutaSqlENQ("DELETE FROM carrito WHERE idArt = @idArticulo", pars);
        }

        public void LimpiarCarrito(int userId)
        {
            SqlParameter[] pars = new SqlParameter[]
            {
        new SqlParameter("@idUsuario", userId)
            };

            objDatos.EjecutaSqlENQ("DELETE FROM carrito WHERE idUsuario = @idUsuario", pars);
        }

        public void RealizarCompra(List<carrito> carrito, decimal totalGeneral, int userId)
        {
            foreach (var item in carrito)
            {
                SqlParameter[] pars = new SqlParameter[]
                {
            new SqlParameter("@idArt", item.idArt),
            new SqlParameter("@nombre", item.nombre),
            new SqlParameter("@cantidad", item.cantidad),
            new SqlParameter("@precio", item.precio),
            new SqlParameter("@totalGeneral", totalGeneral),
            new SqlParameter("@fechaCompra", DateTime.Now),
            new SqlParameter("@idUsuario", userId)
                };

                objDatos.EjecutaSql("INSERT INTO pedidos (idArt, nombre, cantidad, precio, totalGeneral, fechaCompra, idUsuario) VALUES (@idArt, @nombre, @cantidad, @precio, @totalGeneral, @fechaCompra, @idUsuario)", pars);
            }
        }



        public List<pedidos> VerPedidos(int userId)
        {
            List<pedidos> pedidos = new List<pedidos>();

            SqlParameter[] pars = new SqlParameter[]
            {
        new SqlParameter("@userId", userId)
            };

            SqlDataReader reader = objDatos.ObtenerDatos2arg("SELECT idPedido, idArt, nombre, cantidad, precio, totalGeneral, fechaCompra, idUsuario FROM pedidos WHERE idUsuario = @userId;", pars);
            while (reader.Read())
            {
                pedidos pedido = new pedidos
                {
                    idPedido = reader.GetInt32(reader.GetOrdinal("idPedido")),
                    idArt = reader.GetInt32(reader.GetOrdinal("idArt")),
                    nombre = reader["nombre"].ToString(),
                    cantidad = reader.GetInt32(reader.GetOrdinal("cantidad")),
                    precio = reader.GetDecimal(reader.GetOrdinal("precio")),
                    totalGeneral = reader.GetDecimal(reader.GetOrdinal("totalGeneral")),
                    fechaCompra = reader.GetDateTime(reader.GetOrdinal("fechaCompra")),
                    idUsuario = reader.GetInt32(reader.GetOrdinal("idUsuario")) 
                };
                pedidos.Add(pedido);
            }
            reader.Close();

            return pedidos;
        }


    }
}

