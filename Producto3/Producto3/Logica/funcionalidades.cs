using Producto3.Models;
using System.Collections.Generic;
using System;
using System.Data.SqlClient;
using System.Web.UI.WebControls.WebParts;

namespace Producto3.Logica
{
    public class funcionalidades
    {
        private Datos objDatos = new Datos();

        
        public string RegistraProducto(productos nuevoProducto)
        {
            SqlParameter[] pars = new SqlParameter[]
            {
        new SqlParameter("@nombre", nuevoProducto.nombre),
        new SqlParameter("@descripcion", nuevoProducto.descripcion),
        new SqlParameter("@precio", nuevoProducto.precio),
        new SqlParameter("@imagen", nuevoProducto.imagen)
            };

            int nc = objDatos.EjecutaSqlENQ("INSERT INTO productos (nombre, descripcion, precio, imagen) VALUES (@nombre, @descripcion, @precio, @imagen)", pars);
            if (nc == 1)
                return "El producto ha sido registrado";
            else
                return "No se registró el producto";
        }


        public List<productos> ObtenerListaProductos()
        {
            List<productos> listaProductos = new List<productos>();

            SqlDataReader reader = objDatos.ObtenerDatos("SELECT * FROM productos;");
            while (reader.Read())
            {
                productos producto = new productos();
                producto.idArt = reader.GetInt32(reader.GetOrdinal("idArt")); // Obtener el valor como int
                producto.nombre = reader["nombre"].ToString();
                producto.descripcion = reader["descripcion"].ToString();
                producto.precio = reader.GetDecimal(reader.GetOrdinal("precio"));
                producto.imagen = reader["imagen"].ToString();
                listaProductos.Add(producto);
            }
            reader.Close();

            return listaProductos;
        }

        public List<productos> Obtener3Productos()
        {
            List<productos> listaProductos = new List<productos>();

            SqlDataReader reader = objDatos.ObtenerDatos("SELECT idArt, nombre, precio FROM productos;");
            while (reader.Read())
            {
                productos producto = new productos();
                producto.idArt = reader.GetInt32(reader.GetOrdinal("idArt")); // Obtener el valor como int
                producto.nombre = reader["nombre"].ToString();
                producto.precio = reader.GetDecimal(reader.GetOrdinal("precio"));
                listaProductos.Add(producto);
            }
            reader.Close();

            return listaProductos;
        }


        public void EliminarProducto(int idArt)
        {
            objDatos.EjecutaSql("DELETE FROM productos WHERE idArt = @idArt", new SqlParameter[] { new SqlParameter("@idArt", idArt) });
        }

        public List<usuarios> ObtenerListaUsuarios()
        {
            List<usuarios> listaUsuarios = new List<usuarios>();

            SqlDataReader reader = objDatos.ObtenerDatos("SELECT * FROM usuarios;");
            while (reader.Read())
            {
                usuarios usuario = new usuarios();
                usuario.id = reader.GetInt32(reader.GetOrdinal("id")); // Obtener el valor como int
                usuario.nomCompleto = reader["nomCompleto"].ToString();
                usuario.correo = reader["correo"].ToString();
                usuario.contra = reader["contra"].ToString();
                usuario.rol = reader["rol"].ToString();
                listaUsuarios.Add(usuario);
            }
            reader.Close();

            return listaUsuarios;
        }


        public string ModificarProducto(int idArt, string nuevoNombre, string nuevaDescripcion, decimal nuevoPrecio)
        {
            SqlParameter[] pars = new SqlParameter[]
            {
        new SqlParameter("@nombreAct", nuevoNombre),
        new SqlParameter("@descripcionAct", nuevaDescripcion),
        new SqlParameter("@precioAct", nuevoPrecio),
        new SqlParameter("@idArt", idArt)
            };

            int nc = objDatos.EjecutaSqlENQ("UPDATE productos SET nombre = @nombreAct, descripcion = @descripcionAct, precio = @precioAct WHERE idArt = @idArt", pars);
            if (nc == 1)
            {
                return "El producto ha sido modificado";
            }
            else
            {
                return "No se modificó el producto";
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


        public List<ClientePorArticulo> ObtenerClientesPorArticulo(int idArticulo)
        {
            List<ClientePorArticulo> clientesPorArticulo = new List<ClientePorArticulo>();

            string query = "SELECT u.nomCompleto AS NombreCliente, p.cantidad AS Cantidad, p.fechaCompra AS FechaCompra " +
                           "FROM pedidos p " +
                           "JOIN usuarios u ON p.idUsuario = u.id " +
                           "WHERE p.idArt = @IdArticulo";

            SqlParameter[] parameters = new SqlParameter[]
            {
        new SqlParameter("@IdArticulo", idArticulo)
            };

            SqlDataReader reader = objDatos.ObtenerDatos2arg(query, parameters);

            while (reader.Read())
            {
                ClientePorArticulo cliente = new ClientePorArticulo
                {
                    NombreCliente = reader["NombreCliente"].ToString(),
                    Cantidad = Convert.ToInt32(reader["Cantidad"]),
                    FechaCompra = Convert.ToDateTime(reader["FechaCompra"])
                };
                clientesPorArticulo.Add(cliente);
            }
            reader.Close();

            return clientesPorArticulo;
        }







    }
}
