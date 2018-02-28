
using System.Collections.Generic;
using System.Configuration;
using System.Data;

using MySql.Data.MySqlClient;

namespace AppConBD.Models
{
    public class MantenimientoArticulo
    {
        private MySqlConnection con;

        private void Conectar()
        {
            string constr = ConfigurationManager.ConnectionStrings["localMysql"].ConnectionString;
            con = new MySqlConnection(constr);
        }

        public int Alta(Articulo art)
        {
            Conectar();
            MySqlCommand comando = new MySqlCommand("insert into articulos(codigo, descripcion, precio) values (@codio, @descripcion, @precio)", con);
            comando.Parameters.Add("@codigo", MySqlDbType.Int16);
            comando.Parameters.Add("@descripcion", MySqlDbType.VarChar);
            comando.Parameters.Add("@precio", MySqlDbType.Float);

            comando.Parameters["@codigo"].Value = art.Codigo;
            comando.Parameters["@descripcion"].Value = art.Descripcion;
            comando.Parameters["@precio"].Value = art.Precio;

            con.Open();
            int i = comando.ExecuteNonQuery();
            con.Close();

            return i;
        }

        public List<Articulo> MostrarTodos()
        {
            Conectar();
            List<Articulo> articulos = new List<Articulo>();

            MySqlCommand com = new MySqlCommand("Select codigo, descripcion, precio from articulo", con);
            con.Open();

            MySqlDataReader registros = com.ExecuteReader();
            while (registros.Read())
            {
                Articulo art = new Articulo
                {
                    Codigo = int.Parse(registros["codigo"].ToString()),
                    Descripcion = registros["descripcion"].ToString(),
                    Precio = float.Parse(registros["precio"].ToString())
                };

                articulos.Add(art);
            }
            con.Close();
            return articulos;
        }
    }
}