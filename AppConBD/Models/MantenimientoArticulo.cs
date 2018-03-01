
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

        //Metodo: Agregar un nuevo dato
        public int Alta(Articulo art)
        {
            Conectar();
            MySqlCommand comando = new MySqlCommand("insert into articulo(codigo, descripcion, precio) values (@codigo, @descripcion, @precio)", con);
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

        //Metodo: Mostrar todos los datos
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

        //Metodo: --- Recupear?
        public Articulo Recuperar(int codigo)
        {
            Conectar();
            MySqlCommand comando = new MySqlCommand("Select codigo, descripcion, precio from articulo", con);
            comando.Parameters.Add("@codigo", MySqlDbType.Int16);
            comando.Parameters["@codigo"].Value = codigo;

            con.Open();
            MySqlDataReader registro = comando.ExecuteReader();
            Articulo articulo = new Articulo();

            if (registro.Read())
            {
                articulo.Codigo = int.Parse(registro["codigo"].ToString());
                articulo.Descripcion = registro["descripcion"].ToString();
                articulo.Precio = float.Parse(registro["precio"].ToString());
            }

            con.Close();
            return articulo;
        }

        //Metodo: Modificar un dato 
        public int Modificar(Articulo art)
        {
            Conectar();

            MySqlCommand comando = new MySqlCommand("Update articulo set descripcion=@descripcion, precio=@precio where codigo=@codigo", con);

            comando.Parameters.Add("@descripcion", MySqlDbType.VarChar);
            comando.Parameters["@descripcion"].Value = art.Descripcion;

            comando.Parameters.Add("@precio", MySqlDbType.Float);
            comando.Parameters["@precio"].Value = art.Precio;

            comando.Parameters.Add("@codigo", MySqlDbType.Int16);
            comando.Parameters["@codigo"].Value = art.Codigo;

            con.Open();
            int i = comando.ExecuteNonQuery();
            con.Close();

            return i;
        }

        //Metodo: Eliminar dato
        public int Borrar(int codigo)
        {
            Conectar();
            MySqlCommand comando = new MySqlCommand("Delete from articulo where codigo=@codigo", con);
            comando.Parameters.Add("@codigo", MySqlDbType.Int16);
            comando.Parameters["@codigo"].Value = codigo;

            con.Open();
            int i = comando.ExecuteNonQuery();
            con.Close();

            return i;
        }
    }
}